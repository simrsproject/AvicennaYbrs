using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class DiagnoseCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            // Validasi tipe registration
            // Ditambahkan ModeType agar bisa "View Assessment" IGD nya dari regis IPR
            if (RegistrationType.Equals("IPR") && !ModeType.ToLower().Equals("view"))
                throw new Exception("Please contact IT Support, DiagnoseCtl can't use for Inpatient, use WorkDiagnoseCtl instead.");
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            diagnoseText.Visible = AppParameter.IsYes(AppParameter.ParameterItem.IsEmrAssementDiagnoseTextVisible);
            diagnoseDiffText.Visible = AppParameter.IsYes(AppParameter.ParameterItem.IsEmrAssementDiffDiagnoseTextVisible);
        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            txtDiagnose.Text = assessment.Diagnose;
            txtDiagnoseDiff.Text = assessment.DiagnoseDiff;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Set Assessment
            assessment.Diagnose = txtDiagnose.Text;
            assessment.DiagnoseDiff = txtDiagnoseDiff.Text;

            // Save EpisodeDiagnose
            epDiagCtl.Save(args);
            if (args.IsCancel)
                return;

            // Diagnose (A)
            rim.Info3 = txtDiagnose.Text;
            rim.Info3Entry = txtDiagnose.Text;

            // Override
            var diagSummary = EpisodeDiagnose.DiagnoseSummary(RegistrationNo);
            if (!string.IsNullOrWhiteSpace(rim.Info3) && !string.IsNullOrWhiteSpace(diagSummary))
                rim.Info3 = string.Concat(rim.Info3, Environment.NewLine, diagSummary);
            else if (string.IsNullOrWhiteSpace(rim.Info3) && !string.IsNullOrWhiteSpace(diagSummary))
                rim.Info3 = diagSummary;

            if (!string.IsNullOrEmpty(assessment.DiagnoseDiff))
                rim.Info3 = string.Concat(rim.Info3, string.IsNullOrEmpty(rim.Info3) ? string.Empty : Environment.NewLine, "Diagnosa Banding: ", assessment.DiagnoseDiff);
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            epDiagCtl.Rebind(isEdited);
        }

        #endregion
    }
}