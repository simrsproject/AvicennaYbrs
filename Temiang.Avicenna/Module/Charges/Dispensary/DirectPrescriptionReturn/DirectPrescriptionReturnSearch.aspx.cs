using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class DirectPrescriptionReturnSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DirectPrescriptionReturn;
        }

        public override bool OnButtonOkClicked()
        {
            RegistrationQuery query;
            if (Session[SessionNameForQuery] != null)
                query = (RegistrationQuery)Session[SessionNameForQuery];
            else
            {
                query = new RegistrationQuery("a");
                var trans = new TransPrescriptionQuery("b");
                var pat = new PatientQuery("c");

                query.Select
                    (
                        query.RegistrationNo,
                        query.RegistrationDate,
                        trans.PrescriptionNo,
                        pat.MedicalNo,
                        pat.PatientName,
                        pat.Sex,
                        pat.DateOfBirth
                    );
                query.InnerJoin(pat).On(query.PatientID == pat.PatientID);
                query.InnerJoin(trans).On(query.RegistrationNo == trans.RegistrationNo);

                if (txtPrescripionNo.Text != string.Empty)
                    query.Where(trans.PrescriptionNo == txtPrescripionNo.Text);
                if (txtMedicalNo.Text != string.Empty)
                    query.Where(
                            pat.Or(pat.MedicalNo == txtMedicalNo.Text,
                                    pat.OldMedicalNo == txtMedicalNo.Text
                                   )
                               ); 
                if (txtPatientName.Text != string.Empty)
                    query.Where
                        (
                            query.Or
                                (
                                    pat.FirstName.Like("%" + txtPatientName.Text + "%"),
                                    pat.MiddleName.Like("%" + txtPatientName.Text + "%"),
                                    pat.LastName.Like("%" + txtPatientName.Text + "%")
                                )
                        );

                query.Where
                    (
                        //query.ServiceUnitID == AppSession.Parameter.ServiceUnitPharmacyID,
                        query.IsDirectPrescriptionReturn == true,
                        trans.IsPrescriptionReturn == true
                    );
            }
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.OrderBy(query.RegistrationDate.Descending, query.RegistrationTime.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
