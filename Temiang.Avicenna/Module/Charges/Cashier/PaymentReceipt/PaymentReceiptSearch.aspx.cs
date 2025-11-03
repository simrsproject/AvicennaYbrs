using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class PaymentReceiptSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReceipt;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new TransPaymentReceiptQuery("a");
            var reg = new RegistrationQuery("b");
            var pat = new PatientQuery("c");
            var su = new ServiceUnitQuery("d");

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            query.InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID);

            if (!txtPaymentReceiptNo.Text.Trim().Equals(string.Empty))
            {
                if (cboFilterPaymentReceiptNo.SelectedIndex == 1)
                    query.Where(query.PaymentReceiptNo == txtPaymentReceiptNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPaymentReceiptNo.Text);
                    query.Where(query.PaymentReceiptNo.Like(searchTextContain));
                }
            }
            if (!txtPaymentReceiptDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.PaymentReceiptDate == txtPaymentReceiptDate.SelectedDate);
            }
            if (!txtRegistrationNo.Text.Trim().Equals(string.Empty))
            {
                if (cboFilterRegistrationNo.SelectedIndex == 1)
                    query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRegistrationNo.Text);
                    query.Where(query.RegistrationNo.Like(searchTextContain));
                }
            }
            if (!txtPatientName.Text.Trim().Equals(string.Empty))
            {
                if (cboFilterPatientName.SelectedIndex == 1)
                    query.Where(pat.PatientName == txtPatientName.Text);
                else
                {
                    string searchTextContain = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchTextContain)
                        );
                    //string searchTextContain = string.Format("%{0}%", txtPatientName.Text);
                    //query.Where(pat.PatientName.Like(searchTextContain));
                }
            }

            query.Select(
                   query.PaymentReceiptNo,
                   query.PaymentReceiptDate,
                   query.PaymentReceiptTime,
                   query.RegistrationNo,
                   pat.PatientName,
                   su.ServiceUnitName,
                   query.PrintReceiptAsName,
                   query.IsApproved,
                   query.IsVoid
               );

            query.OrderBy(query.PaymentReceiptNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
