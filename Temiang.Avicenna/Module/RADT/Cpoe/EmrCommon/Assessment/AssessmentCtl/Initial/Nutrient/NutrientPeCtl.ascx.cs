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
    public partial class NutrientPeCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                trNutriSkrinning.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSTJ";
            }
        }


        #region override method

        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            NutrientPe pExam;

            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    pExam = JsonConvert.DeserializeObject<NutrientPe>(asses.PhysicalExam);
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

            //gcsCtl.Condition = pExam.Condition;
            //gcsCtl.Eye = pExam.Consciousness.Eye.Code;
            //gcsCtl.Motor = pExam.Consciousness.Motor.Code;
            //gcsCtl.Verbal = pExam.Consciousness.Verbal.Code;
            //gcsCtl.Consciousness = string.Format("{0} [{1}]", pExam.Consciousness.ConsciousnessDescription, pExam.Consciousness.ConsciousnessValue);

            gcsCtl.Condition = pExam.Condition;
            gcsCtl.Gcs = pExam.Consciousness;

            txtWeightUsually.Text = pExam.WeightUsually;
            txtWeightCurrent.Text = pExam.WeightCurrent;
            txtBodyLegth.Text = pExam.BodyLegth;
            txtBMI.Text = pExam.BMI;
            txtTime.Text = pExam.Time;
            ComboBox.SelectedValue(cboTimeType, pExam.TimeType);
            optVisitType.SelectedValue = pExam.VisitType;
            txtWeightChangeInSixMonth.Text = pExam.WeightChangeInSixMonth;
            txtPercentChangeInSixMonth.Text = pExam.PercentChangeInSixMonth;
            optChangeInSixMonth.SelectedValue = pExam.ChangeInSixMonth;
            optFoodIntake.SelectedValue = pExam.FoodIntake;
            optChangeFoodIntake.SelectedValue = pExam.ChangeFoodIntake;
            optGastrointestinal.SelectedValue = pExam.Gastrointestinal;
            txtFreqNausea.Text = pExam.FreqNausea;
            txtDurationNausea.Text = pExam.DurationNausea;
            txtFreqGag.Text = pExam.FreqGag;
            txtDurationGag.Text = pExam.DurationGag;
            txtFreqDiarrhea.Text = pExam.FreqDiarrhea;
            txtDurationDiarrhea.Text = pExam.DurationDiarrhea;
            txtFreqAnorexia.Text = pExam.FreqAnorexia;
            txtDurationAnorexia.Text = pExam.DurationAnorexia;
            optGastrointestinalChange.SelectedValue = pExam.GastrointestinalChange;
            txtDiagnose.Text = pExam.Diagnose;
            optMetabolic.SelectedValue = pExam.Metabolic;
            txtLostFat.Text = pExam.LostFat;
            txtLostMuscle.Text = pExam.LostMuscle;
            txtAnkle.Text = pExam.Ankle;
            txtAnasarca.Text = pExam.Anasarca;
            txtAscites.Text = pExam.Ascites;
            txtAncillaryExam.Text = pExam.AncillaryExam;
            optSga.SelectedValue = pExam.Sga;

            txtAncillaryExam.Text = pExam.AncillaryExam;
            txtPlanning.Text = rim.Info4;
            optNutritionSkrinning.SelectedValue = pExam.NutritionSkrinning;
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment asses, RegistrationInfoMedic rim)
        {
            var pExam = new NutrientPe
            {
                Condition = gcsCtl.Condition,
                Consciousness = new Gcs { Eye = new GcsItem(), Motor = new GcsItem(), Verbal = new GcsItem(), PainScale = "0" }
            };

            pExam.Consciousness.Eye.SetValue(gcsCtl.Eye);
            pExam.Consciousness.Motor.SetValue(gcsCtl.Motor);
            pExam.Consciousness.Verbal.SetValue(gcsCtl.Verbal);
            pExam.Consciousness.PainScale = gcsCtl.PainScale;

            pExam.WeightUsually = txtWeightUsually.Text;
            pExam.WeightCurrent = txtWeightCurrent.Text;
            pExam.BodyLegth = txtBodyLegth.Text;
            pExam.BMI = txtBMI.Text;
            pExam.Time = txtTime.Text;
            pExam.TimeType = cboTimeType.SelectedValue;
            pExam.VisitType = optVisitType.SelectedValue;
            pExam.WeightChangeInSixMonth = txtWeightChangeInSixMonth.Text;
            pExam.PercentChangeInSixMonth = txtPercentChangeInSixMonth.Text;
            pExam.ChangeInSixMonth = optChangeInSixMonth.SelectedValue;
            pExam.FoodIntake = optFoodIntake.SelectedValue;
            pExam.ChangeFoodIntake = optChangeFoodIntake.SelectedValue;
            pExam.Gastrointestinal = optGastrointestinal.SelectedValue;
            pExam.FreqNausea = txtFreqNausea.Text;
            pExam.DurationNausea = txtDurationNausea.Text;
            pExam.FreqGag = txtFreqGag.Text;
            pExam.DurationGag = txtDurationGag.Text;
            pExam.FreqDiarrhea = txtFreqDiarrhea.Text;
            pExam.DurationDiarrhea = txtDurationDiarrhea.Text;
            pExam.FreqAnorexia = txtFreqAnorexia.Text;
            pExam.DurationAnorexia = txtDurationAnorexia.Text;
            pExam.GastrointestinalChange = optGastrointestinalChange.SelectedValue;
            pExam.Diagnose = txtDiagnose.Text;
            pExam.Metabolic = optMetabolic.SelectedValue;
            pExam.LostFat = txtLostFat.Text;
            pExam.LostMuscle = txtLostMuscle.Text;
            pExam.Ankle = txtAnkle.Text;
            pExam.Anasarca = txtAnasarca.Text;
            pExam.Ascites = txtAscites.Text;
            pExam.AncillaryExam = txtAncillaryExam.Text;
            pExam.Sga = optSga.SelectedValue;
            pExam.NutritionSkrinning = optNutritionSkrinning.SelectedValue;

            asses.PhysicalExam = JsonConvert.SerializeObject(pExam);
            asses.OtherExam = txtAncillaryExam.Text;

            rim.Info2 = GenerateSoapObjective(pExam);
            rim.Info4 = txtPlanning.Text;
        }

        private string GenerateSoapObjective(NutrientPe pe)
        {
            var strBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(pe.Consciousness.PainScale))
            {
                Gcs gcs = new Gcs();
                strBuilder.AppendFormat("Skala Nyeri: ({0}) {1}",
                    pe.Consciousness.PainScale,
                    gcs.PainScaleDesc(pe.Consciousness.PainScale.ToInt()));
            }
            if (!string.IsNullOrEmpty(pe.LostFat))
            {
                strBuilder.AppendFormat("Hilang lemak subkutan (triseps, dada): {0} ", pe.LostFat);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.LostMuscle))
            {
                strBuilder.AppendFormat("Hilang massa otot (quadriseps, deltoids): {0} ", pe.LostMuscle);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Ankle))
            {
                strBuilder.AppendFormat("Oedema pergelangan kaki: {0} ", pe.Ankle);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Anasarca))
            {
                strBuilder.AppendFormat("Oedema anasarka: {0} ", pe.Anasarca);
                strBuilder.AppendLine(string.Empty);
            }

            if (!string.IsNullOrEmpty(pe.Ascites))
            {
                strBuilder.AppendFormat("Asites: {0} ", pe.Ascites);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Condition))
            {
                strBuilder.AppendFormat("Keadaan Umum: Sakit {0}", pe.Condition == "Mild" ? "Ringan" : pe.Condition == "Moderate" ? "Sedang" : "Berat");
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Consciousness.ConsciousnessDescription))
                strBuilder.AppendFormat("Kesadaran: {0} GCS: E: {1} M: {2} V: {3}", pe.Consciousness.ConsciousnessDescription, pe.Consciousness.Eye.Score, pe.Consciousness.Motor.Score, pe.Consciousness.Verbal.Score);
            if (!string.IsNullOrEmpty(pe.NutritionSkrinning))
            {
                strBuilder.AppendFormat("Skrinning Gizi: {0}", pe.NutritionSkrinning);
                strBuilder.AppendLine(string.Empty);
            }
            return strBuilder.ToString();
        }


        #endregion

    }
}