using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    /// Created By : Fahri (13-Sep-2023) (Base on CR RSUD Cideres)

    public partial class RehabilitationPe2Ctl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ComboBox.StandardReferenceItem(cboGeneralCondition, "PatientInCondition");
            }
        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    var ent = JsonConvert.DeserializeObject<RehabilitationPe2>(asses.PhysicalExam);
                    cboGeneralCondition.Text = ent.GeneralCondition;
                    txtNeuromuskuloskeletal.Text = ent.Neuromuskuloskeletal;
                    txtrespiratory.Text = ent.Respiratory;
                    txtSummary.Text = ent.Summary;
                    optEvaluation.SelectedValue = ent.Evaluation;
                    txtEvaluationReason.Text = ent.EvaluationReason;

                    chkPhysicalActivity.Checked = ent.FunctionalProblem2.PhysicalActivity;
                    chkSwallowing.Checked = ent.FunctionalProblem2.Swallowing;
                    chkGait.Checked = ent.FunctionalProblem2.Gait;
                    chkCardiorespiratory.Checked = ent.FunctionalProblem2.Cardiorespiratory;
                    chkDefecation.Checked = ent.FunctionalProblem2.Defecation;
                    chkMicturition.Checked = ent.FunctionalProblem2.Micturition;
                    chkNoble.Checked = ent.FunctionalProblem2.Noble;
                    chkExecution.Checked = ent.FunctionalProblem2.Execution;
                    chkSensory.Checked = ent.FunctionalProblem2.Sensory;
                    chkCommunication.Checked = ent.FunctionalProblem2.Communication;
                    chkBalance.Checked = ent.FunctionalProblem2.Balance;
                    chkPosture.Checked = ent.FunctionalProblem2.Posture;
                    chkMuscle.Checked = ent.FunctionalProblem2.Muscle;
                    chkJoint.Checked = ent.FunctionalProblem2.Joint;
                    chkLocomotor.Checked = ent.FunctionalProblem2.Locomotor;

                    optFim.SelectedValue = ent.SpecialExamination2.Fim;
                    txtFimDesc.Text = ent.SpecialExamination2.FimDesc;
                    optBarthelIndex.SelectedValue = ent.SpecialExamination2.BarthelIndex;
                    txtBarthelIndexDesc.Text = ent.SpecialExamination2.BarthelIndexDesc;
                    optDisphagya.SelectedValue = ent.SpecialExamination2.Disphagya;
                    txtDisphagyaDesc.Text = ent.SpecialExamination2.DisphagyaDesc;
                    optMmse.SelectedValue = ent.SpecialExamination2.Mmse;
                    txtMmseDesc.Text = ent.SpecialExamination2.MmseDesc;
                    txtMocalnaDesc.Text = ent.SpecialExamination2.MocalnaDesc;
                    optReceptiveLanguage.SelectedValue = ent.SpecialExamination2.ReceptiveLanguage;
                    txtReceptiveLanguageDesc.Text = ent.SpecialExamination2.ReceptiveLanguageDesc;
                    optExpressiveLanguage.SelectedValue = ent.SpecialExamination2.ExpressiveLanguage;
                    txtExpressiveLanguageDesc.Text = ent.SpecialExamination2.ExpressiveLanguageDesc;
                    optSpeakWordandSentence.SelectedValue = ent.SpecialExamination2.SpeakWordandSentence;
                    txtSpeakWordandSentenceDesc.Text = ent.SpecialExamination2.SpeakWordandSentenceDesc;
                    optArticulation.SelectedValue = ent.SpecialExamination2.Articulation;
                    txtArticulationDesc.Text = ent.SpecialExamination2.ArticulationDesc;
                    optOrientation.SelectedValue = ent.SpecialExamination2.Orientation;
                    txtOrientationDesc.Text = ent.SpecialExamination2.OrientationDesc;
                    optRecall.SelectedValue = ent.SpecialExamination2.Recall;
                    txtRecallDesc.Text = ent.SpecialExamination2.RecallDesc;
                    optBergBalance.SelectedValue = ent.SpecialExamination2.BergBalance;
                    txtBergBalanceDesc.Text = ent.SpecialExamination2.BergBalanceDesc;
                    optSchober.SelectedValue = ent.SpecialExamination2.Schober;
                    txtSchoberDesc.Text = ent.SpecialExamination2.SchoberDesc;
                    optGoniometer.SelectedValue = ent.SpecialExamination2.Goniometer;
                    txtGoniometerDesc.Text = ent.SpecialExamination2.GoniometerDesc;
                    optTimeUpGoTest.SelectedValue = ent.SpecialExamination2.TimeUpGoTest;
                    txtNrs.Text = ent.SpecialExamination2.Nrs;
                    optGait.SelectedValue = ent.SpecialExamination2.Gait;
                    txtGaitDesc.Text = ent.SpecialExamination2.GaitDesc;
                    txtMuscleStrengthDesc.Text = ent.SpecialExamination2.MuscleStrengthDesc;
                    txtOthersDesc.Text = ent.SpecialExamination2.OthersDesc;
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
            {
                return;
            }


        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment asses, RegistrationInfoMedic rim)
        {
            var ent = new RehabilitationPe2
            {
                GeneralCondition = cboGeneralCondition.Text,
                Neuromuskuloskeletal = txtNeuromuskuloskeletal.Text,
                Respiratory = txtrespiratory.Text,
                Summary = txtSummary.Text,
                Evaluation = optEvaluation.SelectedValue,
                EvaluationReason = txtEvaluationReason.Text,
                FunctionalProblem2 = new FunctionalProblem2
                {
                    PhysicalActivity = chkPhysicalActivity.Checked,
                    Swallowing = chkSwallowing.Checked,
                    Gait = chkGait.Checked,
                    Cardiorespiratory = chkCardiorespiratory.Checked,
                    Defecation = chkDefecation.Checked,
                    Micturition = chkMicturition.Checked,
                    Noble = chkNoble.Checked,
                    Execution = chkExecution.Checked,
                    Sensory = chkSensory.Checked,
                    Communication = chkCommunication.Checked,
                    Balance = chkBalance.Checked,
                    Posture = chkPosture.Checked,
                    Muscle = chkMuscle.Checked,
                    Joint = chkJoint.Checked,
                    Locomotor = chkLocomotor.Checked
                },
                SpecialExamination2 = new SpecialExamination2
                {
                    Fim = optFim.SelectedValue,
                    FimDesc = txtFimDesc.Text,
                    BarthelIndex = optBarthelIndex.SelectedValue,
                    BarthelIndexDesc = txtBarthelIndexDesc.Text,
                    Disphagya = optDisphagya.SelectedValue,
                    DisphagyaDesc = txtDisphagyaDesc.Text,
                    Mmse = optMmse.SelectedValue,
                    MmseDesc = txtMmseDesc.Text,
                    MocalnaDesc = txtMocalnaDesc.Text,
                    ReceptiveLanguage = optReceptiveLanguage.SelectedValue,
                    ReceptiveLanguageDesc = txtReceptiveLanguageDesc.Text,
                    ExpressiveLanguage = optExpressiveLanguage.SelectedValue,
                    ExpressiveLanguageDesc = txtExpressiveLanguageDesc.Text,
                    SpeakWordandSentence = optSpeakWordandSentence.SelectedValue,
                    SpeakWordandSentenceDesc = txtSpeakWordandSentenceDesc.Text,
                    Articulation = optArticulation.SelectedValue,
                    ArticulationDesc = txtArticulationDesc.Text,
                    Orientation = optOrientation.SelectedValue,
                    OrientationDesc = txtOrientationDesc.Text,
                    Recall = optRecall.SelectedValue,
                    RecallDesc = txtRecallDesc.Text,
                    BergBalance = optBergBalance.SelectedValue,
                    BergBalanceDesc = txtBergBalanceDesc.Text,
                    Schober = optSchober.SelectedValue,
                    SchoberDesc = txtSchoberDesc.Text,
                    Goniometer = optGoniometer.SelectedValue,
                    GoniometerDesc = txtGoniometerDesc.Text,
                    TimeUpGoTest = optTimeUpGoTest.SelectedValue,
                    Nrs = txtNrs.Text,
                    Gait = optGait.SelectedValue,
                    GaitDesc = txtGaitDesc.Text,
                    MuscleStrengthDesc = txtMuscleStrengthDesc.Text,
                    OthersDesc = txtOthersDesc.Text
                }
            };


            asses.PhysicalExam = JsonConvert.SerializeObject(ent);

            // Objective
            rim.Info2 = GenerateSoapObjective(ent);
        }

        private string GenerateSoapObjective(RehabilitationPe2 pe)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendFormat("A. Umum : {0}", pe.GeneralCondition);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat("B. Neuromuskuloskeletal : {0}", pe.Neuromuskuloskeletal);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat("C. Respirasi : {0}", pe.Respiratory);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendLine("D. Fungsional :");
            strBuilder.AppendLine(string.Empty);

            if (pe.FunctionalProblem2.PhysicalActivity)
                strBuilder.AppendLine(" - Gangguan aktivitas fisik");
            if (pe.FunctionalProblem2.Swallowing)
                strBuilder.AppendLine(" - Gangguan fungsi menelan");
            if (pe.FunctionalProblem2.Gait)
                strBuilder.AppendLine(" - Gangguan gait");
            if (pe.FunctionalProblem2.Cardiorespiratory)
                strBuilder.AppendLine(" - Gangguan kardiorespirasi");
            if (pe.FunctionalProblem2.Defecation)
                strBuilder.AppendLine(" - Gangguan defekasi");
            if (pe.FunctionalProblem2.Micturition)
                strBuilder.AppendLine("- Gangguan berkemih");
            if (pe.FunctionalProblem2.Noble)
                strBuilder.AppendLine(" - Gangguan fungsi luhur");
            if (pe.FunctionalProblem2.Execution)
                strBuilder.AppendLine(" - Gangguan eksekusi");
            if (pe.FunctionalProblem2.Sensory)
                strBuilder.AppendLine(" - Gangguan sensoris");
            if (pe.FunctionalProblem2.Communication)
                strBuilder.AppendLine(" - Gangguan komunikasi");
            if (pe.FunctionalProblem2.Balance)
                strBuilder.AppendLine(" - Gangguan keseimbangan");
            if (pe.FunctionalProblem2.Posture)
                strBuilder.AppendLine(" - Gangguan kontrol postur");
            if (pe.FunctionalProblem2.Muscle)
                strBuilder.AppendLine(" - Gangguan kekuatan otot");
            if (pe.FunctionalProblem2.Joint)
                strBuilder.AppendLine(" - Gangguan fleksibilitas sendi");
            if (pe.FunctionalProblem2.Locomotor)
                strBuilder.AppendLine(" - Gangguan lokomotor");
            return strBuilder.ToString();
        }


        #endregion


    }
}