using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ReceiptOfSubstituteItemPickList : BasePageDialog
    {
        bool _isTxUsingEdDetail;
        protected void Page_Init(object sender, EventArgs e)
        {
            _isTxUsingEdDetail = AppSession.Parameter.IsTxUsingEdDetail;
        }

        private DataTable PurchaseOrderReturns
        {
            get
            {
                string itemType = Request.QueryString["it"];
                string supplierID = Request.QueryString["supid"];
                string serviceUnitID = Request.QueryString["suid"];

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);

                var qrItem = new ItemTransactionItemQuery("d");
                query.InnerJoin(qrItem).On(query.TransactionNo == qrItem.TransactionNo);

                query.Select(
                    query.TransactionNo,
                    query.ReferenceNo,
                    query.TransactionDate,
                    query.FromServiceUnitID,
                    qryserviceunit.ServiceUnitName,
                    query.Notes
                    );

                query.Where(
                    query.TransactionCode == TransactionCode.PurchaseOrderReturn,
                    query.SRPurchaseReturnType == "XX",
                    query.BusinessPartnerID == supplierID,
                    query.SRItemType == itemType,
                    query.FromServiceUnitID == serviceUnitID,
                    query.IsApproved == true,
                    query.IsConsignment == false
                    );
                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                    query.Where(qrItem.ItemID == cboItemID.SelectedValue);
                if (!string.IsNullOrEmpty(txtReturnedNo.Text))
                    query.Where(query.TransactionNo == txtReturnedNo.Text);
                

                query.es.Distinct = true;
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppSession.Parameter.IsTxUsingEdDetail)
            {
                grdDetail.Columns[6].Visible = false;
                grdDetail.Columns[7].Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = PurchaseOrderReturns;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["POSubstitute:Detail" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["POSubstitute:Detail" + Request.UserHostName];
        }

        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSourceDetail();
        }

        private void UpdateDataSourceDetail()
        {
            var dtb = (DataTable)ViewState["POSubstitute:Detail" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                string seqNo = dataItem.GetDataKeyValue("SequenceNo").ToString();
                double qty = ((RadNumericTextBox)dataItem.FindControl("txtQtyInput")).Value ?? 0;
                string batchNumber = ((RadTextBox)dataItem.FindControl("txtBatchNumber")).Text ?? string.Empty;
                DateTime? expiredDate = ((RadDatePicker)dataItem.FindControl("txtExpiredDate")).SelectedDate;
                
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["SequenceNo"].Equals(seqNo))
                    {
                        row["QtyInput"] = qty;
                        row["BatchNumber"] = batchNumber;
                        if (expiredDate != null)
                            row["ExpiredDate"] = expiredDate;

                        break;
                    }
                }

                ViewState["POSubstitute:Detail" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeDataDetail(string transactionNo)
        {
            DataTable dtb;
            using (new esTransactionScope())
            {
                var itemTransaction = new ItemTransaction();
                itemTransaction.LoadByPrimaryKey(transactionNo);

                var query = new ItemTransactionItemQuery("a");
                var iq = new ItemQuery("b");
                if (itemTransaction.SRItemType == ItemType.Medical)
                {
                    var itemProductMedic = new ItemProductMedicQuery("p");
                    query.LeftJoin(itemProductMedic).On(query.ItemID == itemProductMedic.ItemID);
                }
                else if (itemTransaction.SRItemType == ItemType.NonMedical)
                {
                    var itemNonProductMedic = new ItemProductNonMedicQuery("p");
                    query.LeftJoin(itemNonProductMedic).On(query.ItemID == itemNonProductMedic.ItemID);
                }
                else if (itemTransaction.SRItemType == ItemType.Kitchen)
                {
                    var itemKitchen = new ItemKitchenQuery("p");
                    query.LeftJoin(itemKitchen).On(query.ItemID == itemKitchen.ItemID);
                }

                query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
                query.Where(query.TransactionNo == transactionNo, query.IsClosed == false, query.IsBonusItem == false);

                query.OrderBy(query.ItemID.Ascending);

                query.Select(
                    query.TransactionNo,
                    query.SequenceNo,
                    string.Format("<'{0}' as ToServiceUnitID>", itemTransaction.ToServiceUnitID),
                    query.ItemID,
                    "<CASE WHEN a.SRItemUnit = '' THEN CASE WHEN a.ConversionFactor > 1 THEN p.SRPurchaseUnit ELSE p.SRItemUnit END ELSE a.SRItemUnit END AS SRItemUnit>",
                    query.Quantity,
                    "<CASE WHEN a.ConversionFactor = 0 THEN a.QuantityFinishInBaseUnit ELSE (a.QuantityFinishInBaseUnit / a.ConversionFactor) END  AS QuantityFinishInBaseUnit>",
                    "<CASE WHEN a.ConversionFactor=0 THEN (a.Quantity - a.QuantityFinishInBaseUnit) ELSE (a.Quantity - (a.QuantityFinishInBaseUnit / a.ConversionFactor)) END AS QtyInput>",
                    "<CASE WHEN a.ConversionFactor=0 THEN (a.Quantity - a.QuantityFinishInBaseUnit) ELSE (a.Quantity - (a.QuantityFinishInBaseUnit / a.ConversionFactor)) END AS QtyPending>",
                    query.ConversionFactor,
                    query.Description,
                    string.Format("<'{0}' as SRItemType>", itemTransaction.SRItemType),
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.IsDiscountInPercent,
                    query.Discount,
                    query.PriceInCurrency,
                    query.DiscountInCurrency,
                    @"<'' AS BatchNumber>",
                    @"<CAST('1/1/1900' AS DATETIME) AS ExpiredDate>",
                    @"<ISNULL(p.IsControlExpired, 0) AS IsControlExpired>"
                    );
                dtb = query.LoadDataTable();
            }
            ViewState["POSubstitute:Detail" + Request.UserHostName] = dtb;
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is RadGrid)) return;
            RadGrid grd = (RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grddetail": // Populate Detail
                    string[] pars = eventArgument.Split('|');
                    string transNo = pars[0].Split(':')[1];
                    InitializeDataDetail(transNo);
                    break;
            }
        }

        public override bool OnButtonOkClicked()
        {
            UpdateDataSourceDetail();

            //Check data
            DataTable dtb = (DataTable)ViewState["POSubstitute:Detail" + Request.UserHostName];
            using (new esTransactionScope())
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (_isTxUsingEdDetail == false)
                    {
                        bool isControlExpired = false;
                        var item = new Item();
                        if (item.LoadByPrimaryKey(row["ItemID"].ToString()))
                        {
                            if (item.SRItemType == ItemType.Medical)
                            {
                                var product = new ItemProductMedic();
                                if (product.LoadByPrimaryKey(item.ItemID))
                                    isControlExpired = product.IsControlExpired ?? false;
                            }
                            else if (item.SRItemType == ItemType.NonMedical)
                            {
                                var product = new ItemProductNonMedic();
                                if (product.LoadByPrimaryKey(item.ItemID))
                                    isControlExpired = product.IsControlExpired ?? false;
                            }
                            else if (item.SRItemType == ItemType.Kitchen)
                            {
                                var product = new ItemKitchen();
                                if (product.LoadByPrimaryKey(item.ItemID))
                                    isControlExpired = product.IsControlExpired ?? false;
                            }
                        }

                        if (isControlExpired && Convert.ToDouble(row["QtyInput"]) > 0)
                        {
                            if (Convert.ToDateTime(row["ExpiredDate"]) < (new DateTime()).NowAtSqlServer())
                            {
                                ShowMessage(string.Format("Expired date for item {0} must greather than transaction date.", row["Description"]));
                                return false;
                            }
                        }
                    }

                    if (Convert.ToDouble(row["QtyInput"]) > Convert.ToDouble(row["QtyPending"]))
                    {
                        ShowMessage(string.Format("Received Qty for item {0} can not be greather than {1}", row["Description"], Convert.ToDouble(row["QtyPending"]).ToString()));
                        return false;
                    }
                }
            }

            Session["POSubstitute:ItemSelected" + Request.UserHostName] = ViewState["POSubstitute:Detail" + Request.UserHostName];
            return true;
        }

        #region Search Item
        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();

            grdDetail.DataSource = null;
            grdDetail.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var tbl = PopulateItem(e.Text);
            cboItemID.DataSource = tbl.Rows.Count == 0 ? PopulateItem(e.Text) : tbl;
            cboItemID.DataBind();
        }

        private DataTable PopulateItem(string parameter)
        {
            string searchTextContain = string.Format("%{0}%", parameter);
            var query = new ItemQuery("a");
            query.Where(query.SRItemType == Request.QueryString["it"], query.IsActive == true);

            query.es.Top = 30;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );

            query.Where
                (
                    query.Or
                        (
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.ItemName.Ascending);

            var tbl = query.LoadDataTable();

            return tbl;
        }

        #endregion
    }
}
