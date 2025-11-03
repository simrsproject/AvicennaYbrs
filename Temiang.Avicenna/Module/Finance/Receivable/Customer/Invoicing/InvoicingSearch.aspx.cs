using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Receivable.Customer
{
    public partial class InvoicingSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_CUSTOMER_INVOICING;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new InvoiceCustomerQuery("a");
            var csQ = new CustomerQuery("b");
            var srQ = new AppStandardReferenceItemQuery("c");
            var itmQ = new InvoiceCustomerItemQuery("d");

            query.InnerJoin(itmQ).On(itmQ.InvoiceNo == query.InvoiceNo);
            query.LeftJoin(csQ).On(query.CustomerID == csQ.CustomerID);
            query.LeftJoin(srQ).On(
                query.SRReceivableStatus == srQ.ItemID &&
                srQ.StandardReferenceID == AppEnum.StandardReference.ReceivableStatus
                );

            query.Select(
                   query.InvoiceNo,
                   query.InvoiceDate,
                   query.InvoiceDueDate,
                   csQ.CustomerName,
                   query.IsApproved,
                   query.IsVoid,
                   srQ.ItemName.As("refToAppStandardReference_ReceivableStatusName"),
                   "<SUM(d.Amount) AS TotalAmount>"
               );

            query.Where(query.IsInvoicePayment == false, query.InvoiceReferenceNo.IsNull());
            query.GroupBy(query.InvoiceNo, query.InvoiceDate, query.InvoiceDueDate, csQ.CustomerName,
                          query.IsApproved, query.IsVoid, srQ.ItemName);
            query.OrderBy(query.InvoiceNo.Descending);

            if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                if (cboFilterInvoiceNo.SelectedIndex == 1)
                    query.Where(query.InvoiceNo == txtInvoiceNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtInvoiceNo.Text);
                    query.Where(query.InvoiceNo.Like(searchTextContain));
                }
            }
            if (!txtInvoiceDateFrom.IsEmpty && !txtInvoiceDateTo.IsEmpty)
            {
                query.Where(query.InvoiceDate.Between(txtInvoiceDateFrom.SelectedDate.Value.Date, txtInvoiceDateTo.SelectedDate.Value.Date));
            }
            if (!txtInvoiceDueDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.InvoiceDueDate == txtInvoiceDueDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                if (cboFilterCustomerName.SelectedIndex == 1)
                    query.Where(csQ.CustomerName == txtCustomerName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCustomerName.Text);
                    query.Where(csQ.CustomerName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(itmQ.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(itmQ.TransactionNo.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
