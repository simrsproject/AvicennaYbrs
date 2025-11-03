using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientResearchSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PatientResearch;
        }

        public override bool OnButtonOkClicked()
        {
            var qPatient = new PatientQuery("a");
            var sal = new AppStandardReferenceItemQuery("sal");
            qPatient.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qPatient.SRSalutation == sal.ItemID);

            qPatient.es.Top = AppSession.Parameter.MaxResultRecord;
            qPatient.Where(qPatient.IsNonPatient == false, qPatient.IsActive == true);
            
            qPatient.Select
                (
                    qPatient.PatientID,
                    qPatient.MedicalNo,
                    @"<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) AS 'PatientName'>",
                    qPatient.Sex,
                    qPatient.DateOfBirth,
                    @"<a.StreetName+' '+a.City+' '+a.County +' '+ISNULL(a.ZipCode, '') AS 'Address'>",
                    sal.ItemName.As("SalutationName")
                 );
            qPatient.OrderBy(qPatient.PatientID.Ascending);

            if (!string.IsNullOrEmpty(txtPatientName.Text))
            {
                if (cboFilterPatientName.SelectedIndex == 1)
                {
                    qPatient.Where(string.Format("<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) LIKE '{0}'>", txtPatientName.Text));
                }
                else
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    qPatient.Where(string.Format("<LTRIM(RTRIM(LTRIM(a.FirstName + ' ' + a.MiddleName)) + ' ' + a.LastName) LIKE '{0}'>", searchPatient));
                }
            }
            if (!string.IsNullOrEmpty(txtMedicalNo.Text))
            {
                if (cboFilterMedicalNo.SelectedIndex == 1)
                    qPatient.Where(qPatient.MedicalNo == txtMedicalNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMedicalNo.Text);
                    qPatient.Where(qPatient.MedicalNo.Like(searchTextContain));
                }
            }
            if (!txtDoB.IsEmpty)
                qPatient.Where(qPatient.DateOfBirth == txtDoB.SelectedDate);

            Session[SessionNameForQuery] = qPatient;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}