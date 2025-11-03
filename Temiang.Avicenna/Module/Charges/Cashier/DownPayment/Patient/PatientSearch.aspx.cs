using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Charges.DownPayment
{
    public partial class PatientSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] == "deposit")
                ProgramID = AppConstant.Program.PatientDepositReceive;
            else
                ProgramID = AppConstant.Program.PatientDepositReturn;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new TransPaymentPatientQuery("a");
            var patient = new PatientQuery("b");
            query.Select
                (
                    query,
                    patient.MedicalNo,
                    patient.PatientName,
                    "<ISNULL((SELECT SUM(tppi.Amount) FROM TransPaymentPatientItem AS tppi WHERE tppi.PaymentNo = a.PaymentNo), 0) AS Amount>"
                );
            query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
            if (cboFilterMedicalNo.SelectedIndex == 0)
            {
                string search = "%" + txtMedicalNo.Text + "%";

                query.Where(
                    query.Or(
                        string.Format("<b.MedicalNo LIKE '{0}'>", search),
                        string.Format("< OR LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '{0}'>", search)
                        )
                    );
            }
            else
                query.Where(
                    query.Or(
                        patient.MedicalNo == txtMedicalNo.Text,
                        patient.PatientName == txtMedicalNo.Text
                        )
                    );
            if (ProgramID == AppConstant.Program.PatientDepositReceive)
                query.Where(query.TransactionCode == Temiang.Avicenna.BusinessObject.Reference.TransactionCode.DownPayment);
            else
                query.Where(query.TransactionCode == Temiang.Avicenna.BusinessObject.Reference.TransactionCode.DownPaymentReturn);
            query.OrderBy(query.PaymentNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
