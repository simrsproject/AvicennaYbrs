using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class DiagnoseCtl2 : BaseAssessmentCtl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private string RegistrationType
        {
            get
            {
                return Request.QueryString["rt"];
            }
        }
        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            txtDiagnose.Text = assessment.Diagnose;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Set Assessment
            assessment.Diagnose = txtDiagnose.Text;

            // Save EpisodeDiagnose
            //epDiagCtl.Save();

            // Diagnose (A)
            rim.Info3 = txtDiagnose.Text;
            rim.Info3Entry = txtDiagnose.Text;

            // Override
            var diagSummary = EpisodeDiagnose.DiagnoseSummary(RegistrationNo);
            if (!string.IsNullOrWhiteSpace(rim.Info3) && !string.IsNullOrWhiteSpace(diagSummary))
                rim.Info3 = string.Concat(rim.Info3, Environment.NewLine, diagSummary);
            else if (string.IsNullOrWhiteSpace(rim.Info3) && !string.IsNullOrWhiteSpace(diagSummary))
                rim.Info3 = diagSummary;

        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            //epDiagCtl.Rebind(isEdited);
        }

        #endregion
    }
}