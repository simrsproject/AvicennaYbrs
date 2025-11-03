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
    public partial class RehabilitationPeCtl : BaseAssessmentCtl
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
                    var ent = JsonConvert.DeserializeObject<RehabilitationPe>(asses.PhysicalExam);
                    cboGeneralCondition.Text = ent.GeneralCondition;
                    txtNeuromuskuloskeletal.Text = ent.Neuromuskuloskeletal;
                    txtcardiorespiratory.Text = ent.Cardiorespiratory;
                    txtAncillaryExam.Text = ent.AncillaryExam;
                    txtSummary.Text = ent.Summary;
                    txtRecomendation.Text = ent.Recomendation;
                    optEvaluation.SelectedValue = ent.Evaluation;
                    txtEvaluationReason.Text = ent.EvaluationReason;

                    chkPhysicalActivity.Checked = ent.FunctionalProblem.PhysicalActivity;
                    chkSwallowing.Checked = ent.FunctionalProblem.Swallowing;
                    chkGait.Checked = ent.FunctionalProblem.Gait;
                    chkCardiorespiratory.Checked = ent.FunctionalProblem.Cardiorespiratory;
                    chkDefecation.Checked = ent.FunctionalProblem.Defecation;
                    chkMicturition.Checked = ent.FunctionalProblem.Micturition;
                    chkNoble.Checked = ent.FunctionalProblem.Noble;
                    chkExecution.Checked = ent.FunctionalProblem.Execution;
                    chkSensory.Checked = ent.FunctionalProblem.Sensory;
                    chkCommunication.Checked = ent.FunctionalProblem.Communication;
                    chkBalance.Checked = ent.FunctionalProblem.Balance;
                    chkPosture.Checked = ent.FunctionalProblem.Posture;
                    chkMuscle.Checked = ent.FunctionalProblem.Muscle;
                    chkJoint.Checked = ent.FunctionalProblem.Joint;
                    chkLocomotor.Checked = ent.FunctionalProblem.Locomotor;

                    optFim.SelectedValue = ent.SpecialExamination.Fim;
                    txtFimDesc.Text = ent.SpecialExamination.FimDesc;
                    optBarthelIndex.SelectedValue = ent.SpecialExamination.BarthelIndex;
                    txtBarthelIndexDesc.Text = ent.SpecialExamination.BarthelIndexDesc;
                    optDisphagya.SelectedValue = ent.SpecialExamination.Disphagya;
                    txtDisphagyaDesc.Text = ent.SpecialExamination.DisphagyaDesc;
                    optMmse.SelectedValue = ent.SpecialExamination.Mmse;
                    txtMmseDesc.Text = ent.SpecialExamination.MmseDesc;
                    optToken.SelectedValue = ent.SpecialExamination.Token;
                    txtTokenDesc.Text = ent.SpecialExamination.TokenDesc;
                    optTadir.SelectedValue = ent.SpecialExamination.Tadir;
                    txtTadirDesc.Text = ent.SpecialExamination.TadirDesc;
                    optBergBalance.SelectedValue = ent.SpecialExamination.BergBalance;
                    txtBergBalanceDesc.Text = ent.SpecialExamination.BergBalanceDesc;
                    optSchober.SelectedValue = ent.SpecialExamination.Schober;
                    txtSchoberDesc.Text = ent.SpecialExamination.SchoberDesc;
                    optGoniometer.SelectedValue = ent.SpecialExamination.Goniometer;
                    txtGoniometerDesc.Text = ent.SpecialExamination.GoniometerDesc;
                    optTimeUpGoTest.SelectedValue = ent.SpecialExamination.TimeUpGoTest;
                    txtWongBaker.Text = ent.SpecialExamination.WongBaker;
                    txtVas.Text = ent.SpecialExamination.Vas;
                    txtNrs.Text = ent.SpecialExamination.Nrs;
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
            var ent = new RehabilitationPe
            {
                GeneralCondition = cboGeneralCondition.Text,
                Neuromuskuloskeletal = txtNeuromuskuloskeletal.Text,
                Cardiorespiratory = txtcardiorespiratory.Text,
                AncillaryExam = txtAncillaryExam.Text,
                Summary = txtSummary.Text,
                Recomendation = txtRecomendation.Text,
                Evaluation = optEvaluation.SelectedValue,
                EvaluationReason = txtEvaluationReason.Text,
                FunctionalProblem = new FunctionalProblem
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
                SpecialExamination = new SpecialExamination
                {
                    Fim = optFim.SelectedValue,
                    FimDesc = txtFimDesc.Text,
                    BarthelIndex = optBarthelIndex.SelectedValue,
                    BarthelIndexDesc = txtBarthelIndexDesc.Text,
                    Disphagya = optDisphagya.SelectedValue,
                    DisphagyaDesc = txtDisphagyaDesc.Text,
                    Mmse = optMmse.SelectedValue,
                    MmseDesc = txtMmseDesc.Text,
                    Token = optToken.SelectedValue,
                    TokenDesc = txtTokenDesc.Text,
                    Tadir = optTadir.SelectedValue,
                    TadirDesc = txtTadirDesc.Text,
                    BergBalance = optBergBalance.SelectedValue,
                    BergBalanceDesc = txtBergBalanceDesc.Text,
                    Schober = optSchober.SelectedValue,
                    SchoberDesc = txtSchoberDesc.Text,
                    Goniometer = optGoniometer.SelectedValue,
                    GoniometerDesc = txtGoniometerDesc.Text,
                    TimeUpGoTest = optTimeUpGoTest.SelectedValue,
                    WongBaker = txtWongBaker.Text,
                    Vas = txtVas.Text,
                    Nrs = txtNrs.Text
                }
            };


            asses.PhysicalExam = JsonConvert.SerializeObject(ent);

            // Objective
            rim.Info2 = GenerateSoapObjective(ent);
        }

        private string GenerateSoapObjective(RehabilitationPe pe)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendFormat("A. Umum : {0}", pe.GeneralCondition);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat("B. Neuromuskuloskeletal : {0}", pe.Neuromuskuloskeletal);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat("C. Kardiorespirasi : {0}", pe.Cardiorespiratory);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendLine("D. Fungsional :");
            strBuilder.AppendLine(string.Empty);

            if (pe.FunctionalProblem.PhysicalActivity)
                strBuilder.AppendLine(" - Gangguan aktivitas fisik");
            if (pe.FunctionalProblem.Swallowing)
                strBuilder.AppendLine(" - Gangguan fungsi menelan");
            if (pe.FunctionalProblem.Gait)
                strBuilder.AppendLine(" - Gangguan gait");
            if (pe.FunctionalProblem.Cardiorespiratory)
                strBuilder.AppendLine(" - Gangguan kardiorespirasi");
            if (pe.FunctionalProblem.Defecation)
                strBuilder.AppendLine(" - Gangguan defekasi");
            if (pe.FunctionalProblem.Micturition)
                strBuilder.AppendLine("- Gangguan berkemih");
            if (pe.FunctionalProblem.Noble)
                strBuilder.AppendLine(" - Gangguan fungsi luhur");
            if (pe.FunctionalProblem.Execution)
                strBuilder.AppendLine(" - Gangguan eksekusi");
            if (pe.FunctionalProblem.Sensory)
                strBuilder.AppendLine(" - Gangguan sensoris");
            if (pe.FunctionalProblem.Communication)
                strBuilder.AppendLine(" - Gangguan komunikasi");
            if (pe.FunctionalProblem.Balance)
                strBuilder.AppendLine(" - Gangguan keseimbangan");
            if (pe.FunctionalProblem.Posture)
                strBuilder.AppendLine(" - Gangguan kontrol postur");
            if (pe.FunctionalProblem.Muscle)
                strBuilder.AppendLine(" - Gangguan kekuatan otot");
            if (pe.FunctionalProblem.Joint)
                strBuilder.AppendLine(" - Gangguan fleksibilitas sendi");
            if (pe.FunctionalProblem.Locomotor)
                strBuilder.AppendLine(" - Gangguan lokomotor");
            return strBuilder.ToString();
        }


        #endregion


    }
}