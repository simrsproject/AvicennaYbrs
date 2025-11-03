using System;
using System.Collections;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Entrian Diagnosa untuk Rawat Inap
    /// </summary>
    /// Diagnosa Rawat Inap terdiri dari :
    /// 1. Diagnosa Awal : direkan ke RegistrationInfoMedicDiagnose 
    /// 2. Diagnosa Kerja : direkan ke RegistrationInfoMedicDiagnose
    /// 3. Diagnosa Akhir : direkan ke EpisodeDiagnose di entrian MEdical Discharge Summary
    /// ------------------------------------------------------------
    /// Created By : Handono (Timika Desmber 2019)
    /// -------------------------------------------------------------
    /// MODIF HIST:
    /// 09-09-2023 Fahri (Base on CR RSUD Cideres)
    /// - Tambah Different Diagnose
    /// - Tambah status visible unvisible Diagnose Text & Different Diagnose Text

    public partial class WorkDiagnoseCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            diagnoseText.Visible = AppParameter.IsYes(AppParameter.ParameterItem.IsEmrAssementDiagnoseTextVisible);
            diagnoseDiffText.Visible = AppParameter.IsYes(AppParameter.ParameterItem.IsEmrAssementDiffDiagnoseTextVisible);
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
            txtDiagnoseDiff.Text = assessment.DiagnoseDiff;
            regInMedDiagnoseCtl.Rebind(rim.RegistrationInfoMedicID, DataModeCurrent != AppEnum.DataMode.Read);
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Save EpisodeDiagnose
            regInMedDiagnoseCtl.Save(args, assessment.RegistrationNo, assessment.RegistrationInfoMedicID, rim.ParamedicID, assessment.AssessmentDateTime.Value);

            if (args.IsCancel)
                return;

            // Set Assessment
            assessment.Diagnose = txtDiagnose.Text;
            assessment.DiagnoseDiff = txtDiagnoseDiff.Text;


            // Diagnose (A)
            rim.Info3 = txtDiagnose.Text;
            rim.Info3Entry = txtDiagnose.Text;

            // Override
            var diagSummary = RegistrationInfoMedicDiagnose.DiagnoseSummaryCurrentSoap(rim.RegistrationInfoMedicID);
            if (!string.IsNullOrWhiteSpace(rim.Info3) && !string.IsNullOrWhiteSpace(diagSummary))
                rim.Info3 = string.Concat(rim.Info3, Environment.NewLine, diagSummary);
            else if (string.IsNullOrWhiteSpace(rim.Info3) && !string.IsNullOrWhiteSpace(diagSummary))
                rim.Info3 = diagSummary;

        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            regInMedDiagnoseCtl.Rebind(RegistrationInfoMedicID, isEdited);
        }

        #endregion

    }
}