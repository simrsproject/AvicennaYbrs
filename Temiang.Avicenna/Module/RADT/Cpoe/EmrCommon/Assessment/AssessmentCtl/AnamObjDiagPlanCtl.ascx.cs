using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// <summary>
    /// Create Control baru untuk form Assesment Dokter dari control yang sudah ada yaitu SoapCtl.ascx 
    /// Fungsinya untuk menghilangkan duplikasi dari kolom S(Subjective) yang sudah terwakili dengan kolom Anamnesis, Kemudian kolom A(Assessment) yang sudah terwakili dengan ICD 10.
    /// Hanya untuk Assesment rawat jalan
    /// </summary>
    /// Create By : Fahri
    /// Create Date : Feb 2023
    /// Request : RS GPI
    public partial class AnamObjDiagPlanCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            //txtSubjective.Text = rim.Info1Entry;
            txtObjective.Text = rim.Info2;
            txtAssessment.Text = rim.Info3Entry;
            txtPlanning.Text = rim.Info4;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            //rim.Info1Entry = txtSubjective.Text;
            rim.Info2 = txtObjective.Text;
            //rim.Info3Entry = txtAssessment.Text;
            rim.Info4 = txtPlanning.Text;

            assessment.PhysicalExam = rim.Info2;

            // Save EpisodeDiagnose
            epDiagCtl.Save(args);
            if (args.IsCancel)
                return;

            // Diagnose (A)
            rim.Info3 = txtAssessment.Text;
            rim.Info3Entry = txtAssessment.Text;

            // Override
            var diagSummary = EpisodeDiagnose.DiagnoseSummary(RegistrationNo);
            if (!string.IsNullOrWhiteSpace(rim.Info3) && !string.IsNullOrWhiteSpace(diagSummary))
                rim.Info3 = string.Concat(rim.Info3, Environment.NewLine, diagSummary);
            else if (string.IsNullOrWhiteSpace(rim.Info3) && !string.IsNullOrWhiteSpace(diagSummary))
                rim.Info3 = diagSummary;

        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            epDiagCtl.Rebind(isEdited);
        }

        #endregion




    }
}