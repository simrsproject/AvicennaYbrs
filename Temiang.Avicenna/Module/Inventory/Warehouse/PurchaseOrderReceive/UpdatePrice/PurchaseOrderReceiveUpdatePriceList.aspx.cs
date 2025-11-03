using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class PurchaseOrderReceiveUpdatePriceList : BasePage
    {
        public bool isUpdatePrice
        {
            get
            {
                var UpdatePrice = Request.QueryString["uP"] ?? string.Empty;
                return string.IsNullOrEmpty(UpdatePrice) || UpdatePrice != "0";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
                ProgramID = isUpdatePrice ? 
                    AppConstant.Program.ReceivingOrderUpdatePrice:
                    AppConstant.Program.ReceivingOrderUpdateInvoiceSupplierNo;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboServiceUnitID, BusinessObject.Reference.TransactionCode.PurchaseOrderReceive, true);
                
                txtTransactionDate.SelectedDate = DateTime.Now;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = ItemTransactions;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string transNo = dataItem.GetDataKeyValue("TransactionNo").ToString();
            //Load record
            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == transNo);
            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.SequenceNo,
                    query.IsClosed,
                    iq.ItemName.As("ItemName"),
                    query.BatchNumber,
                    query.ExpiredDate,
                    query.Price,
                    query.Discount,
                    query.ConversionFactor,
                    query.IsBonusItem,
                    query.Description
                );

            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable ItemTransactions
        {
            get
            {
                string userId = AppSession.UserLogin.UserID;

                var query = new ItemTransactionQuery("a");
                var qryserviceunit = new ServiceUnitQuery("c");
                var sup = new SupplierQuery("b");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var userserviceunit = new AppUserServiceUnitQuery("e");
                var qusr = new AppUserServiceUnitQuery("u");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.ToServiceUnitID);
                query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
                query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                query.InnerJoin(userserviceunit).On(query.ToServiceUnitID == userserviceunit.ServiceUnitID &&
                                                    userserviceunit.UserID == userId);
                query.InnerJoin(qusr).On(query.ToServiceUnitID == qusr.ServiceUnitID &&
                                qusr.UserID == userId);
                query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrderReceive);

                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       query.ReferenceNo,
                       qryserviceunit.ServiceUnitName.As("TServiceUnitID"),
                       sup.SupplierName,
                       itemtype.ItemName,
                       query.IsApproved,
                       query.ReferenceNo,
                       query.Notes,
                       query.IsVoid,
                       query.InvoiceNo,
                       query.InvoiceSupplierDate,
                       query.ChargesAmount, query.TaxAmount, query.DiscountAmount
                   );
                query.OrderBy(query.ToServiceUnitID.Ascending, query.TransactionDate.Descending,
                              query.TransactionNo.Descending);

                if (!txtTransactionDate.IsEmpty)
                    query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.ToServiceUnitID == cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSupplierID.SelectedValue))
                    query.Where(query.BusinessPartnerID == cboSupplierID.SelectedValue);
                if (!string.IsNullOrEmpty(txtReferenceNo.Text))
                    query.Where(query.ReferenceNo == txtReferenceNo.Text);
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                    query.Where(query.InvoiceNo == txtInvoiceNo.Text);
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();

                query = new ItemTransactionQuery("a");
                qryserviceunit = new ServiceUnitQuery("c");
                sup = new SupplierQuery("b");
                itemtype = new AppStandardReferenceItemQuery("d");
                userserviceunit = new AppUserServiceUnitQuery("e");
                qusr = new AppUserServiceUnitQuery("u");

                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
                query.LeftJoin(itemtype).On(itemtype.ItemID == query.SRItemType && itemtype.StandardReferenceID == "ItemType");
                query.InnerJoin(userserviceunit).On(query.FromServiceUnitID == userserviceunit.ServiceUnitID &&
                                                    userserviceunit.UserID == userId);
                query.InnerJoin(qusr).On(query.FromServiceUnitID == qusr.ServiceUnitID &&
                                qusr.UserID == userId);
                query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.PurchaseOrderReturn);

                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       query.ReferenceNo,
                       qryserviceunit.ServiceUnitName.As("TServiceUnitID"),
                       sup.SupplierName,
                       itemtype.ItemName,
                       query.IsApproved,
                       query.ReferenceNo,
                       query.Notes,
                       query.IsVoid,
                       query.InvoiceNo,
                       query.ChargesAmount, query.TaxAmount, query.DiscountAmount
                   );
                query.OrderBy(query.FromServiceUnitID.Ascending, query.TransactionDate.Descending,
                              query.TransactionNo.Descending);

                if (!txtTransactionDate.IsEmpty)
                    query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.FromServiceUnitID == cboServiceUnitID.SelectedValue);
                if (!string.IsNullOrEmpty(cboSupplierID.SelectedValue))
                    query.Where(query.BusinessPartnerID == cboSupplierID.SelectedValue);
                if (!string.IsNullOrEmpty(txtReferenceNo.Text))
                    query.Where(query.ReferenceNo == txtReferenceNo.Text);
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                    query.Where(query.InvoiceNo == txtInvoiceNo.Text);
                if (!string.IsNullOrEmpty(txtTransactionNo.Text))
                    query.Where(query.TransactionNo == txtTransactionNo.Text);

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb2 = query.LoadDataTable();
                dtb.Merge(dtb2);

                foreach (DataRow row in dtb.Rows)
                {
                    var isQ = new InvoiceSupplierQuery("a");
                    var isiQ = new InvoiceSupplierItemQuery("b");
                    isQ.InnerJoin(isiQ).On(isQ.InvoiceNo == isiQ.InvoiceNo);
                    isQ.Where(isQ.IsVoid == false, isQ.IsInvoicePayment == false,
                                isiQ.TransactionNo == row["TransactionNo"].ToString());
                    isQ.Select(isQ.InvoiceNo);
                    DataTable isDt = isQ.LoadDataTable();
                    if (isDt.Rows.Count > 0)
                        row.Delete();
                }

                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboSupplier_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SupplierItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.SupplierItemDataBound(e);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();
        }
    }
}
