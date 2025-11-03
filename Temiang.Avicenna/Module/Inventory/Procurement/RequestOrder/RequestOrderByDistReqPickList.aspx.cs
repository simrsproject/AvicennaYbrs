using DevExpress.Data.Filtering.Helpers;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class RequestOrderByDistReqPickList : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                trRequestType.Visible = AppSession.Parameter.IsVisibleRequestTypeOnPurchaseRequestPicklist;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["RequestOrderDetail" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["RequestOrderDetail" + Request.UserHostName];
        }

        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["RequestOrderDetail" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["ItemID"].Equals(dataItem.GetDataKeyValue("ItemID").ToString()))
                    {
                        row["QtyOrder"] = ((RadNumericTextBox)dataItem.FindControl("txtQtyOrder")).Value ?? 0;
                        break;
                    }
                }

                ViewState["RequestOrderDetail" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeData()
        {
            var colMin = grdDetail.Columns.FindByDataField("Minimum");
            colMin.HeaderText = "Minimum";

            var colMax = grdDetail.Columns.FindByDataField("Maximum");
            colMax.HeaderText = "Maximum";

            if (string.IsNullOrEmpty(txtTransactionNo.Text))
                grdDetail.DataSource = InitializeDataFromItemBalance();
            else
                grdDetail.DataSource = InitializeDataFromReferenceNo(txtTransactionNo.Text);

            grdDetail.DataBind();
        }

        private DataTable InitializeDataFromItemBalance()
        {
            var query = new ItemBalanceQuery("a");

            if (Request.QueryString["it"] == ItemType.Medical)
            {
                var qrItemMed = new ItemProductMedicQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit,
                        qrItemMed.SRPurchaseUnit,
                        qrItemMed.ConversionFactor
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem == bool.Parse(Request.QueryString["inv"]) &&
                                              qrItemMed.IsConsignment == false);
            }
            else if (Request.QueryString["it"] == ItemType.NonMedical)
            {
                var qrItemMed = new ItemProductNonMedicQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit,
                        qrItemMed.SRPurchaseUnit,
                        qrItemMed.ConversionFactor
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem == bool.Parse(Request.QueryString["inv"]) &&
                                              qrItemMed.IsConsignment == false);
            }
            else
            {
                var qrItemMed = new ItemKitchenQuery("b");
                query.Select
                    (
                        qrItemMed.SRItemUnit,
                        qrItemMed.SRPurchaseUnit,
                        qrItemMed.ConversionFactor
                    );
                query.InnerJoin(qrItemMed).On(query.ItemID == qrItemMed.ItemID &&
                                              qrItemMed.IsInventoryItem == bool.Parse(Request.QueryString["inv"]));
            }

            var qrItem = new ItemQuery("d");
            query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);
            query.Where
                (
                    query.LocationID == Request.QueryString["lid"],
                    qrItem.IsActive == true
                );
            if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                query.Where(qrItem.ItemGroupID == cboItemGroupID.SelectedValue);

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPorBySupplierItem) &&
                !string.IsNullOrEmpty(Request.QueryString["suppid"]))
            {
                var itemSupp = new SupplierItemQuery("siq");
                query.InnerJoin(itemSupp).On(query.ItemID == itemSupp.ItemID &&
                                             itemSupp.SupplierID == Request.QueryString["suppid"]);
            }

            if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory && !string.IsNullOrEmpty(Request.QueryString["itmcat"]))
            {
                query.Where(qrItem.SRItemCategory == Request.QueryString["itmcat"]);
            }

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPorByProductAccount) &&
                !string.IsNullOrEmpty(Request.QueryString["paid"]))
                query.Where(qrItem.ProductAccountID == Request.QueryString["paid"]);

            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == query.ItemID);

            //Sub Query
            var dt = new ItemTransactionItemQuery("dt");
            var hd = new ItemTransactionQuery("hd");
            if (rbtRequestType.SelectedValue == "DR")
            {
                // Sub Query Distribution Request
                dt.Select(@"<ISNULL(SUM(dt.Quantity*dt.ConversionFactor), 0) AS 'QtyRequest'>");
                dt.InnerJoin(hd).On(hd.TransactionNo == dt.TransactionNo &
                    hd.TransactionCode == TransactionCode.DistributionRequest &
                    hd.ToServiceUnitID == Request.QueryString["fu"] &
                    hd.ToLocationID == Request.QueryString["lid"] &
                    hd.IsApproved == true);
                dt.Where(dt.ItemID == query.ItemID, dt.IsClosed == false);
            }
            else
            {
                // Sub Query Inventory Issue Request
                dt.Select(@"<ISNULL(SUM((dt.Quantity*dt.ConversionFactor)-dt.QuantityFinishInBaseUnit), 0) AS 'QtyRequest'>");
                dt.InnerJoin(hd).On(hd.TransactionNo == dt.TransactionNo &
                    hd.TransactionCode == TransactionCode.InventoryIssueRequestOut &
                    hd.ToServiceUnitID == Request.QueryString["fu"] &
                    hd.ToLocationID == Request.QueryString["lid"] &
                    hd.IsApproved == true);
                dt.Where(dt.ItemID == query.ItemID, dt.IsClosed == false);
            }

            query.Select
                (
                    query.ItemID,
                    query.Minimum,
                    query.Maximum,
                    query.Balance,
                    itemBalTot.Select().As("BalanceTotal"),
                    dt.Select().As("QtyRequest"),
                    @"<0 AS QtyOrder>",
                    qrItem.ItemName.As("ItemName"),
                    @"<'' AS Unit>"
                );

            query.OrderBy(qrItem.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();

            var isUsingItemUnit = AppParameter.IsYes(AppParameter.ParameterItem.IsPurchaseRequestsUsingItemUnit);
            if (isUsingItemUnit)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (Convert.ToDecimal(row["QtyRequest"]) == 0 || Convert.ToDecimal(row["Balance"]) >= Convert.ToDecimal(row["QtyRequest"]))
                        row.Delete();
                    else
                    {
                        row["QtyOrder"] = Convert.ToDecimal(row["QtyRequest"]) - Convert.ToDecimal(row["Balance"]);
                        row["ConversionFactor"] = 1;
                        row["SRPurchaseUnit"] = row["SRItemUnit"];
                        row["Unit"] = row["SRItemUnit"] + "/1";
                    }
                }
            }
            else
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (Convert.ToDecimal(row["QtyRequest"]) == 0 || Convert.ToDecimal(row["Balance"]) >= Convert.ToDecimal(row["QtyRequest"]))
                        row.Delete();
                    else
                    {
                        row["QtyOrder"] =
                        Math.Ceiling(((Convert.ToDecimal(row["QtyRequest"])) -
                                      (Convert.ToDecimal(row["Balance"]))) /
                                     (Convert.ToDecimal(row["ConversionFactor"])));
                        row["Unit"] = row["SRPurchaseUnit"] + "/" +
                                      string.Format("{0:n0}", (Convert.ToDecimal(row["ConversionFactor"])));
                    }
                }
            }

            dtb.AcceptChanges();

            ViewState["RequestOrderDetail" + Request.UserHostName] = dtb;
            return dtb;
        }

        private DataTable InitializeDataFromReferenceNo(string transNo)
        {
            //main Query
            var dt = new ItemTransactionItemQuery("dt");
            var hd = new ItemTransactionQuery("hd");
            if (rbtRequestType.SelectedValue == "DR")
            {
                //Distribution Request
                dt.Select(
                    dt.ItemID,
                    dt.Quantity,
                    @"<dt.Quantity AS QtyOrder>",
                    @"<dt.Quantity AS QtyRequest>",
                    dt.ConversionFactor,
                    dt.SRItemUnit,
                    dt.SRItemUnit.As("SRPurchaseUnit")
                    );
                dt.InnerJoin(hd).On(hd.TransactionNo == dt.TransactionNo);
                dt.Where(dt.TransactionNo == transNo, 
                    hd.TransactionCode == TransactionCode.DistributionRequest, 
                    hd.ToServiceUnitID == Request.QueryString["fu"], 
                    hd.ToLocationID == Request.QueryString["lid"], 
                    hd.IsApproved == true);
            }
            else
            {
                //Inventory Issue Request
                dt.Select(
                    dt.ItemID,
                    dt.Quantity,
                    @"<dt.Quantity AS QtyOrder>",
                    @"<dt.Quantity AS QtyRequest>",
                    dt.ConversionFactor,
                    dt.SRItemUnit,
                    dt.SRItemUnit.As("SRPurchaseUnit")
                    );
                dt.InnerJoin(hd).On(hd.TransactionNo == dt.TransactionNo);
                dt.Where(dt.TransactionNo == transNo, 
                    hd.TransactionCode == TransactionCode.InventoryIssueRequestOut,
                    hd.ToServiceUnitID == Request.QueryString["fu"],
                    hd.ToLocationID == Request.QueryString["lid"],
                    hd.IsApproved == true);
            }

            var query = new ItemBalanceQuery("a");
            dt.InnerJoin(query).On(query.ItemID == dt.ItemID & query.LocationID == hd.ToLocationID);

            var qrItem = new ItemQuery("d");
            dt.InnerJoin(qrItem).On(qrItem.ItemID == dt.ItemID && qrItem.IsActive == true);
            
            if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
                dt.Where(qrItem.ItemGroupID == cboItemGroupID.SelectedValue);

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPorBySupplierItem) &&
                !string.IsNullOrEmpty(Request.QueryString["suppid"]))
            {
                var itemSupp = new SupplierItemQuery("siq");
                dt.InnerJoin(itemSupp).On(query.ItemID == itemSupp.ItemID &&
                                             itemSupp.SupplierID == Request.QueryString["suppid"]);
            }

            if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory && !string.IsNullOrEmpty(Request.QueryString["itmcat"]))
            {
                dt.Where(qrItem.SRItemCategory == Request.QueryString["itmcat"]);
            }

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPorByProductAccount) &&
                !string.IsNullOrEmpty(Request.QueryString["paid"]))
                dt.Where(qrItem.ProductAccountID == Request.QueryString["paid"]);

            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == dt.ItemID);

            dt.Select
                (
                    query.Minimum,
                    query.Maximum,
                    query.Balance,
                    itemBalTot.Select().As("BalanceTotal"),
                    qrItem.ItemName.As("ItemName"),
                    @"<'' AS Unit>"
                );

            dt.OrderBy(dt.SequenceNo.Ascending);
            DataTable dtb = dt.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                row["Unit"] = row["SRItemUnit"] + "/" + string.Format("{0:n0}", (Convert.ToDecimal(row["ConversionFactor"])));
            }

            dtb.AcceptChanges();

            ViewState["RequestOrderDetail" + Request.UserHostName] = dtb;
            return dtb;
        }

        private ItemTransactionItem FindItemTransactionItem(string itemID)
        {
            var coll = (ItemTransactionItemCollection)Session["RequestOrderItems" + Request.UserHostName];
            foreach (ItemTransactionItem entity in coll)
            {
                if (entity.ItemID == itemID)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            DataTable tbl = (DataTable)ViewState["RequestOrderDetail" + Request.UserHostName];

            var coll = (ItemTransactionItemCollection)Session["RequestOrderItems" + Request.UserHostName];
            string seqNo = coll != null && coll.HasData ? coll[coll.Count - 1].SequenceNo : "000";

            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qtyOrder = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyOrder")).Value);
                if (qtyOrder <= 0) continue;

                ItemTransactionItem entity = FindItemTransactionItem(dataItem["ItemID"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();

                    entity.SequenceNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.SequenceNo;
                    entity.Quantity = qtyOrder;
                }
                else
                    entity.Quantity += qtyOrder;

                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.ReferenceNo = string.Empty;
                entity.ReferenceSequenceNo = string.Empty;
                
                entity.SRItemUnit = dataItem["SRPurchaseUnit"].Text;
                entity.ConversionFactor = decimal.Parse(dataItem["ConversionFactor"].Text);
                if (entity.ConversionFactor == 0) entity.ConversionFactor = 1;
                entity.QuantityFinishInBaseUnit = 0;
                entity.PageNo = 0;
                entity.CostPrice = 0;

                entity.BatchNumber = string.Empty;
                entity.str.ExpiredDate = string.Empty;
                entity.IsPackage = false;
                entity.IsBonusItem = false;
                entity.IsClosed = false;
                entity.Description = dataItem["ItemName"].Text;
                entity.Minimum = dataItem["Minimum"].Text.ToDecimal();
                entity.Maximum = dataItem["Maximum"].Text.ToDecimal();
                entity.Balance = dataItem["Balance"].Text.ToDecimal();
                entity.SRMasterBaseUnit = dataItem["SRItemUnit"].Text;

                if (AppSession.Parameter.IsShowPriceInPurchaseRequest)
                    ProcurementUtils.PopulateWithHistPrice(entity, Request.QueryString["it"], Request.QueryString["suppid"]);
                else
                {
                    entity.Discount1Percentage = 0;
                    entity.Discount2Percentage = 0;
                    entity.Price = 0;
                    entity.Discount = 0;
                }
            }

            ViewState["RequestOrderDetail" + Request.UserHostName] = null;
            return true;
        }


        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        protected void btnByItemGroupID_Click(object sender, ImageClickEventArgs e)
        {
            InitializeData();
        }

        protected void cboItemGroupID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ItemGroupQuery query = new ItemGroupQuery();
            query.es.Top = 10;
            query.Select
            (
                query.ItemGroupID,
                query.ItemGroupName
            );
            query.Where
            (
                query.Or
                (
                    query.ItemGroupID.Like(searchTextContain),
                    query.ItemGroupName.Like(searchTextContain)
                ),
                query.IsActive == true,
                query.SRItemType == Request.QueryString["it"]
            );

            cboItemGroupID.DataSource = query.LoadDataTable();
            cboItemGroupID.DataBind();
        }

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }
    }
}