using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class ConsignmentReturnPickList : BasePageDialog
    {
        private DataTable ConsignmentReceives
        {
            get
            {
                string itemType = Request.QueryString["it"];
                string supplierId = Request.QueryString["supid"];
                string serviceUnitId = Request.QueryString["suid"];
                
                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("b");
                var qrItem = new ItemTransactionItemQuery("c");
                var qrLoc = new LocationQuery("d");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);
                query.InnerJoin(qrItem).On(query.TransactionNo == qrItem.TransactionNo);
                query.InnerJoin(qrLoc).On(query.ToLocationID == qrLoc.LocationID);

                query.Select(
                    query.TransactionNo,
                    query.ReferenceNo,
                    query.TransactionDate,
                    query.ToServiceUnitID,
                    qryserviceunit.ServiceUnitName,
                    qrLoc.LocationName,
                    query.Notes
                    );

                query.Where(
                    query.TransactionCode == TransactionCode.ConsignmentReceive,
                    query.BusinessPartnerID == supplierId,
                    query.SRItemType == itemType,
                    query.ToServiceUnitID == serviceUnitId,
                    query.IsApproved == true
                    );
                if (!string.IsNullOrEmpty(cboItemID.SelectedValue))
                    query.Where(qrItem.ItemID == cboItemID.SelectedValue);

                query.es.Distinct = true;
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = ConsignmentReceives;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["ConsignmentReturnDetail" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["ConsignmentReturnDetail" + Request.UserHostName];
        }
        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSourceDetail();
        }

        private void UpdateDataSourceDetail()
        {
            DataTable dtb = (DataTable)ViewState["ConsignmentReturnDetail" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                string seqNo = dataItem.GetDataKeyValue("SequenceNo").ToString();
                double qty = ((RadNumericTextBox)dataItem.FindControl("txtQtyInput")).Value ?? 0;
                double balance = ((RadNumericTextBox)dataItem.FindControl("txtBalance")).Value ?? 0;

                foreach (DataRow row in dtb.Rows)
                {
                    if (row["SequenceNo"].Equals(seqNo))
                    {
                        if (qty > balance)
                            row["QtyInput"] = balance;
                        else
                            row["QtyInput"] = qty;
                        
                        break;
                    }
                }

                ViewState["ConsignmentReturnDetail" + Request.UserHostName] = dtb;
            }
        }
        private void InitializeDataDetail(string transactionNo)
        {
            DataTable dtb;
            using (new esTransactionScope())
            {
                string locId = Request.QueryString["locid"];

                var itemTransaction = new ItemTransaction();
                itemTransaction.LoadByPrimaryKey(transactionNo);

                var query = new ItemTransactionItemQuery("a");
                var iq = new ItemQuery("b");
                var balq = new ItemBalanceQuery("c");
                if (itemTransaction.SRItemType == ItemType.Medical)
                {
                    var itemProductMedic = new ItemProductMedicQuery("p");
                    query.InnerJoin(itemProductMedic).On(query.ItemID == itemProductMedic.ItemID);
                }
                else if (itemTransaction.SRItemType == ItemType.NonMedical)
                {
                    var itemNonProductMedic = new ItemProductNonMedicQuery("p");
                    query.InnerJoin(itemNonProductMedic).On(query.ItemID == itemNonProductMedic.ItemID);
                }

                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.InnerJoin(balq).On(query.ItemID == balq.ItemID && balq.LocationID == locId);
                query.Where(query.TransactionNo == transactionNo, query.IsClosed == false, query.IsBonusItem == false);

                query.OrderBy(query.ItemID.Ascending);

                query.Select(
                    query.TransactionNo,
                    query.SequenceNo,
                    string.Format("<'{0}' as ToServiceUnitID>", itemTransaction.ToServiceUnitID),
                    query.ItemID,
                    "<p.SRItemUnit>",
                    "<CASE WHEN a.ConversionFactor=0 THEN a.Quantity ELSE Quantity*a.ConversionFactor END as Quantity>",
                    query.QuantityFinishInBaseUnit,
                    balq.Balance,
                    "<(CASE WHEN a.ConversionFactor=0 THEN a.Quantity ELSE Quantity*a.ConversionFactor END) - a.QuantityFinishInBaseUnit AS QtyInput>",
                    "<(CASE WHEN a.ConversionFactor=0 THEN a.Quantity ELSE Quantity*a.ConversionFactor END) - a.QuantityFinishInBaseUnit AS QtyPending>",
                    query.ConversionFactor,
                    iq.ItemName,
                    string.Format("<'{0}' as SRItemType>", itemTransaction.SRItemType),
                    "<CASE WHEN a.ConversionFactor=0 THEN a.Price ELSE a.Price / a.ConversionFactor END as Price>",
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    "<ISNULL(a.IsDiscountInPercent, 0) AS IsDiscountInPercent>",
                    "<CASE WHEN a.ConversionFactor=0 THEN a.Discount ELSE a.Discount / a.ConversionFactor END as Discount>",
                    "<CASE WHEN a.ConversionFactor=0 THEN a.PriceInCurrency ELSE a.PriceInCurrency / a.ConversionFactor END as PriceInCurrency>",
                    "<CASE WHEN a.ConversionFactor=0 THEN a.DiscountInCurrency ELSE a.DiscountInCurrency / a.ConversionFactor END as DiscountInCurrency>"
                    );
                dtb = query.LoadDataTable();
            }
            ViewState["ConsignmentReturnDetail" + Request.UserHostName] = dtb;
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
            DataTable dtb = (DataTable)ViewState["ConsignmentReturnDetail" + Request.UserHostName];
            using (new esTransactionScope())
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (Convert.ToDouble(row["QtyInput"]) > Convert.ToDouble(row["QtyPending"]))
                    {
                        ShowMessage(string.Format("Return Qty for item {0} can not be greather than {1}", row["Description"], Convert.ToDouble(row["QtyPending"]).ToString()));
                        return false;
                    }
                }
            }

            Session["ConsignmentReturnItemSelected" + Request.UserHostName] = ViewState["ConsignmentReturnDetail" + Request.UserHostName];
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
            string searchText = string.Format("%{0}%", parameter);
            var query = new ItemQuery("a");
            var itiq = new ItemTransactionItemQuery("b");
            var itq = new ItemTransactionQuery("c");

            query.InnerJoin(itiq).On(query.ItemID == itiq.ItemID);
            query.InnerJoin(itq).On(itiq.TransactionNo == itq.TransactionNo &&
                                    itq.TransactionCode == TransactionCode.ConsignmentReceive &&
                                    itq.BusinessPartnerID == Request.QueryString["supid"]);

            query.es.Top = 30;
            query.es.Distinct = true;
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );

            query.Where
                (
                    query.Or
                        (
                            query.ItemName.Like(searchText),
                            query.ItemID.Like(searchText)
                        )
                );
            query.OrderBy(query.ItemName.Ascending);

            var tbl = query.LoadDataTable();

            return tbl;
        }

        #endregion
    }
}
