using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Receivable.Adjustment.WriteOff
{
    public partial class ARWriteOffSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_WRITEOFF;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new InvoicesQuery("a");
            var guar = new GuarantorQuery("b");

            if (!txtInvoiceNo.Text.Trim().Equals(string.Empty))
            {
                if (cboFilterInvoiceNo.SelectedIndex == 1)
                    query.Where(query.InvoiceNo == txtInvoiceNo.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtInvoiceNo.Text);
                    query.Where(query.InvoiceNo.Like(searchText));
                }
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
            if (!txtInvoiceDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.InvoiceDate == txtInvoiceDate.SelectedDate);
            }
            if (!txtGuarantorName.Text.Trim().Equals(string.Empty))
            {
                if (cboFilterGuarantorName.SelectedIndex == 1)
                    guar.Where(guar.GuarantorName == txtGuarantorName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtGuarantorName.Text);
                    guar.Where(guar.GuarantorName.Like(searchText));
                }
            }

            query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);

            query.Select(
                            query.InvoiceNo,
                            query.InvoiceReferenceNo,
                            query.PaymentDate,
                            guar.GuarantorName,
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
