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
    public partial class IgdEsiPeCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Seting ReviewSystem Control
            var igd = new Igd();
            questAbdomenPelvis.QuestionGroupID = igd.AbdomenPelvis.QuestionGroupID;
            questAncillaryExam.QuestionGroupID = igd.AncillaryExam.QuestionGroupID;
            questDisabilitas.QuestionGroupID = igd.Disabilitas.QuestionGroupID;
            questEksposur.QuestionGroupID = igd.Eksposur.QuestionGroupID;
            questIntervensiPreHosp.QuestionGroupID = igd.InterventionPrehospital.QuestionGroupID;
            questJalanNapas.QuestionGroupID = igd.JalanNapas.QuestionGroupID;
            questKepalaLeher.QuestionGroupID = igd.KepalaLeher.QuestionGroupID;
            questPenilaianBayi.QuestionGroupID = igd.PenilaianBayi.QuestionGroupID;
            questPernapasan.QuestionGroupID = igd.Pernapasan.QuestionGroupID;
            questSirkulasi.QuestionGroupID = igd.Sirkulasi.QuestionGroupID;
            questThorax.QuestionGroupID = igd.Thorax.QuestionGroupID;
            questOth.QuestionGroupID = igd.Others.QuestionGroupID;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Get Education
            var asses = assessment;
            // Convert to class w json
            try
            {
                if (!string.IsNullOrEmpty(asses.PhysicalExam))
                {
                    var igd = JsonConvert.DeserializeObject<Igd>(asses.PhysicalExam);
                    questAbdomenPelvis.PopulateValue(igd.AbdomenPelvis);
                    questAncillaryExam.PopulateValue(igd.AncillaryExam);
                    questDisabilitas.PopulateValue(igd.Disabilitas);
                    questEksposur.PopulateValue(igd.Eksposur);
                    questIntervensiPreHosp.PopulateValue(igd.InterventionPrehospital);
                    questJalanNapas.PopulateValue(igd.JalanNapas);
                    questKepalaLeher.PopulateValue(igd.KepalaLeher);
                    questPenilaianBayi.PopulateValue(igd.PenilaianBayi);
                    questPernapasan.PopulateValue(igd.Pernapasan);
                    questSirkulasi.PopulateValue(igd.Sirkulasi);
                    questThorax.PopulateValue(igd.Thorax);
                    questOth.PopulateValue(igd.Others);

                    gcsCtl.Condition = igd.Condition;
                    gcsCtl.Gcs = igd.Consciousness;

                    triageEsiCtl.Triage5Level = igd.Triage;
                }

            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var igd = new Igd();
            igd.AbdomenPelvis = questAbdomenPelvis.GetQuestionAnswerValue();
            igd.AncillaryExam = questAncillaryExam.GetQuestionAnswerValue();
            igd.Disabilitas = questDisabilitas.GetQuestionAnswerValue();
            igd.Eksposur = questEksposur.GetQuestionAnswerValue();
            igd.InterventionPrehospital = questIntervensiPreHosp.GetQuestionAnswerValue();
            igd.JalanNapas = questJalanNapas.GetQuestionAnswerValue();
            igd.KepalaLeher = questKepalaLeher.GetQuestionAnswerValue();
            igd.PenilaianBayi = questPenilaianBayi.GetQuestionAnswerValue();
            igd.Pernapasan = questPernapasan.GetQuestionAnswerValue();
            igd.Sirkulasi = questSirkulasi.GetQuestionAnswerValue();
            igd.Thorax = questThorax.GetQuestionAnswerValue();
            igd.Others = questOth.GetQuestionAnswerValue();

            igd.Condition = gcsCtl.Condition;
            igd.Consciousness = gcsCtl.Gcs;

            igd.Triage = triageEsiCtl.Triage5Level;

            assessment.PhysicalExam = JsonConvert.SerializeObject(igd);

            // Objective
            rim.Info2 = GenerateSoapObjective(igd);
        }

        private string GenerateSoapObjective(Igd pe)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("PRIMARY SURVEY:");
            strBuilder.AppendFormat("Triase: {0}", StandardReference.GetItemName(AppEnum.StandardReference.Triage5Level, pe.Triage.TriageId.Code));
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat(" - Airway: {0}", string.IsNullOrEmpty(pe.Triage.AirwayDescription) ? string.Empty : string.Format("{0} [{1}]", pe.Triage.AirwayDescription, pe.Triage.TriageValue));
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat(" - Breathing: {0}", string.IsNullOrEmpty(pe.Triage.BreathingDescription) ? string.Empty : string.Format("{0} [{1}]", pe.Triage.BreathingDescription, pe.Triage.TriageValue));
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat(" - Circulation: {0}", string.IsNullOrEmpty(pe.Triage.CirculationDescription) ? string.Empty : string.Format("{0} [{1}]", pe.Triage.CirculationDescription, pe.Triage.TriageValue));
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat(" - Conscious: {0}", string.IsNullOrEmpty(pe.Triage.ConsciousDescription) ? string.Empty : string.Format("{0} [{1}]", pe.Triage.ConsciousDescription, pe.Triage.TriageValue));
            strBuilder.AppendLine(string.Empty);
            SoapObjectiveAppend("Jalan Nafas:", pe.JalanNapas.Summary, strBuilder);
            SoapObjectiveAppend("Pernafasan:", pe.Pernapasan.Summary, strBuilder);
            SoapObjectiveAppend("Sirkulasi:", pe.Sirkulasi.Summary, strBuilder);
            SoapObjectiveAppend("Penilaian Bayi Baru Lahir:", pe.PenilaianBayi.Summary, strBuilder);
            SoapObjectiveAppend("Disabilitas:", pe.Disabilitas.Summary, strBuilder);
            SoapObjectiveAppend("Exposur:", pe.Eksposur.Summary, strBuilder);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendLine("SECONDARY SURVEY:");
            strBuilder.AppendLine(pe.Consciousness.GetSoapObjective(pe.Condition));
            SoapObjectiveAppend("Kepala dan Leher:", pe.KepalaLeher.Summary, strBuilder);
            SoapObjectiveAppend("Thorax:", pe.Thorax.Summary, strBuilder);
            SoapObjectiveAppend("Abdomen & Pelvis:", pe.AbdomenPelvis.Summary, strBuilder);
            SoapObjectiveAppend("Lain-lain:", pe.Others.Summary, strBuilder);
            return strBuilder.ToString();
        }
        private static void SoapObjectiveAppend(string caption, string value, StringBuilder strBuilder)
        {
            if (!string.IsNullOrEmpty(value))
            {
                strBuilder.AppendLine(caption);
                strBuilder.AppendLine(value);
            }
        }
        #endregion


    }
}