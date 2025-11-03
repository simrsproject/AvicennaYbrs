using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Receivable.Customer
{
    public partial class PaymentSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_CUSTOMER_PAYMENT;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new InvoiceCustomerQuery("a");
            var cust = new CustomerQuery("b");
            var sr = new AppStandardReferenceItemQuery("c");
            var itm = new InvoiceCustomerItemQuery("d");

            query.InnerJoin(itm).On(query.InvoiceNo == itm.InvoiceNo);
            query.LeftJoin(cust).On(query.CustomerID == cust.CustomerID);
            query.LeftJoin(sr).On(
                query.SRReceivableStatus == sr.ItemID &&
                sr.StandardReferenceID == AppEnum.StandardReference.ReceivableStatus
                );

            query.Select(
                   query.InvoiceNo,
                   query.InvoiceReferenceNo,
                   query.PaymentDate,
                   cust.CustomerName,
                   query.IsApproved,
                   query.IsVoid,
                   sr.ItemName.As("refToAppStandardReference_ReceivableStatusName"),
                   "<SUM(d.PaymentAmount) AS Total>"
               );

            query.Where(query.IsInvoicePayment == true);
            query.GroupBy(query.InvoiceNo, query.InvoiceReferenceNo, query.PaymentDate, cust.CustomerName,
                          query.IsApproved, query.IsVoid, sr.ItemName);
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
            if (!string.IsNullOrEmpty(txtInvoiceReferenceNo.Text))
            {
                if (cboFilterInvoiceReferenceNo.SelectedIndex == 1)
                    query.Where(query.InvoiceReferenceNo == txtInvoiceReferenceNo);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtInvoiceReferenceNo.Text);
                    query.Where(query.InvoiceReferenceNo.Like(searchTextContain));
                }
            }
            if (!txtInvoiceDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.InvoiceDate == txtInvoiceDate.SelectedDate);
            }
            if (!txtPaymentDate.IsEmpty)
            {
                query.Where(query.PaymentDate == txtPaymentDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                if (cboFilterCustomerName.SelectedIndex == 1)
                    query.Where(cust.CustomerName == txtCustomerName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCustomerName.Text);
                    query.Where(cust.CustomerName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
