using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Finance.Receivable.Customer
{
    public partial class VerificationList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_CUSTOMER_VERIFICATION;

            if (!IsPostBack)
            {
                txtFromDate.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                txtToDate.SelectedDate = DateTime.Now;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = InvoiceCustomers;
        }

        private DataTable InvoiceCustomers
        {
            get
            {
                var query = new InvoiceCustomerQuery("a");
                var cust = new CustomerQuery("b");
                var sr = new AppStandardReferenceItemQuery("c");
                var itm = new InvoiceCustomerItemQuery("d");

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select(
                           query.InvoiceNo,
                           query.InvoiceDate,
                           query.InvoiceDueDate,
                           cust.CustomerName,                           
                           query.IsApproved,
                           query.IsVoid,
                           query.InvoiceNotes,
                           sr.ItemName.As("refToAppStandardReference_ReceivableStatusName"),
                           @"<GETDATE() AS 'AgingDate'>",
                           "<SUM(d.Amount) AS TotalAmount>"
                       );
                query.InnerJoin(cust).On(query.CustomerID == cust.CustomerID);
                query.InnerJoin(sr).On
                    (
                        query.SRReceivableStatus == sr.ItemID && 
                        sr.StandardReferenceID == AppEnum.StandardReference.ReceivableStatus
                    );
                query.InnerJoin(itm).On(itm.InvoiceNo == query.InvoiceNo);

                if (!txtFromDate.IsEmpty && !txtFromDate.IsEmpty)
                    query.Where(query.InvoiceDate.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate));

                query.Where(query.SRReceivableStatus == AppSession.Parameter.ReceivableStatusProcess);
                query.GroupBy(query.InvoiceNo, query.InvoiceDate, query.InvoiceDueDate, cust.CustomerID, cust.CustomerName,
                                  query.IsApproved, query.IsVoid, query.InvoiceNotes, sr.ItemName);

                query.OrderBy(query.InvoiceNo.Descending);

                return query.LoadDataTable();
            }
        }

        protected void btnSearchInvoice_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.DataSource = InvoiceCustomers;
            grdList.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
                return;

            if (eventArgument == "process")
            {
                Validate();
                if (!IsValid)
                    return;

                Process();
                
                grdList.Rebind();
            }
        }

        private void Process()
        {
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                if ((dataItem.FindControl("processChkBox") as CheckBox).Checked)
                {
                    string invoiceNo = dataItem.GetDataKeyValue("InvoiceNo").ToString();
                    DateTime agingDate = ((RadDatePicker)dataItem.FindControl("txtAgingDate")).SelectedDate ?? DateTime.Now;

                    Save(invoiceNo, agingDate);
                }
            }
        }

        private void Save(string invoiceNo, DateTime agingDate)
        {
            InvoiceCustomer inv = new InvoiceCustomer();
            inv.LoadByPrimaryKey(invoiceNo);
            inv.SRReceivableStatus = AppSession.Parameter.ReceivableStatusVerify;
            inv.VerifyDate = DateTime.Now;
            inv.VerifyByUserID = AppSession.UserLogin.UserID;
            inv.LastUpdateByUserID = AppSession.UserLogin.UserID;
            inv.LastUpdateDateTime = DateTime.Now;
            inv.AgingDate = agingDate;
            inv.InvoiceDueDate = agingDate.AddDays((int)inv.InvoiceTOP);

            using (esTransactionScope trans = new esTransactionScope())
            {
                inv.Save();
                trans.Complete();
            }
        }
    }
}
