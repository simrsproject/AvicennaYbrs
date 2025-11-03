using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class SoapCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            txtSubjective.Text = rim.Info1Entry;
            txtObjective.Text = rim.Info2Entry;
            txtAssessment.Text = rim.Info3Entry;
            txtPlanning.Text = rim.Info4;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            rim.Info1Entry = txtSubjective.Text;

            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, txtObjective.Text);
            else
                rim.Info2 = txtObjective.Text;

            rim.Info2Entry = txtObjective.Text;
            rim.Info3Entry = txtAssessment.Text;
            rim.Info4 = txtPlanning.Text;

            //assessment.PhysicalExam = rim.Info2; // Remark karena bisa saling timpa dgn hasil entry di control ...Pe.ascx (Handono 2306)

            // Save EpisodeDiagnose
            epDiagCtl.Save(args);
            if (args.IsCancel)
                return;

            // Diagnose (A)
            var diagSummary = EpisodeDiagnose.DiagnoseSummary(RegistrationNo);
            rim.Info3 = string.Concat(txtAssessment.Text, Environment.NewLine, diagSummary);

        }

        protected override void OnDataModeChanged(bool isEdited)
        {
            epDiagCtl.Rebind(isEdited);
        }

        #endregion




    }
}