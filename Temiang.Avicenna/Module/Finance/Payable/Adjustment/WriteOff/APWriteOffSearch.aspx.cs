using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class APWriteOffSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AP_WRITEOFF;
        }

        public override bool OnButtonOkClicked()
        {

            var query = new InvoiceSupplierQuery("a");
            var supp = new SupplierQuery("b");

            if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                if (cboFilterInvoiceNo.SelectedIndex == 1)
                    query.Where(query.InvoiceNo == txtInvoiceNo.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtInvoiceNo.Text);
                    query.Where(query.InvoiceNo.Like(searchText));
                }
            }
            if (!txtInvoiceDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.InvoiceDate == txtInvoiceDate.SelectedDate);
            }
            if (!txtInvoiceReferenceNo.Text.Trim().Equals(string.Empty))
            {
                if (cboFilterInvoiceReferenceNo.SelectedIndex == 1)
                    query.Where(query.InvoiceReferenceNo == txtInvoiceReferenceNo);
                else
                {
                    string searchText = string.Format("%{0}%", txtInvoiceReferenceNo.Text);
                    query.Where(query.InvoiceReferenceNo.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtSupplierName.Text))
            {
                if (cboFilterSupplierName.SelectedIndex == 1)
                    query.Where(supp.SupplierName == txtSupplierName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtSupplierName.Text);
                    query.Where(supp.SupplierName.Like(searchText));
                }
            }
            
            query.InnerJoin(supp).On(query.SupplierID == supp.SupplierID);

            query.Select(
                          query.InvoiceNo,
                          query.InvoiceReferenceNo,
                          query.InvoiceDate.As("PaymentDate"),
                          supp.SupplierName,
                          query.IsApproved,
                          query.IsVoid
                      );

            query.Where(query.IsInvoicePayment == true, query.IsWriteOff == true);
            query.OrderBy(query.InvoiceNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
