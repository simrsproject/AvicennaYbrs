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
    public partial class IgdPeCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var triageStdRefId = AppParameter.GetParameterValue(AppParameter.ParameterItem.TriageStdRefId);
            if (string.IsNullOrWhiteSpace(triageStdRefId))
                StandardReference.InitializeIncludeSpace(ddlTriage, AppEnum.StandardReference.Triage, true);
            else
                StandardReference.InitializeIncludeSpace(ddlTriage, triageStdRefId, true);


            // Seting ReviewSystem Control
            var igd = new Igd();
            questAbdomenPelvis.QuestionGroupID = igd.AbdomenPelvis.QuestionGroupID;
            questAncillaryExam.QuestionGroupID = igd.AncillaryExam.QuestionGroupID;
            questDisabilitas.QuestionGroupID = igd.Disabilitas.QuestionGroupID;
            questEksposur.QuestionGroupID = igd.Eksposur.QuestionGroupID;
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
                    questJalanNapas.PopulateValue(igd.JalanNapas);
                    questKepalaLeher.PopulateValue(igd.KepalaLeher);
                    questPenilaianBayi.PopulateValue(igd.PenilaianBayi);
                    questPernapasan.PopulateValue(igd.Pernapasan);
                    questSirkulasi.PopulateValue(igd.Sirkulasi);
                    questThorax.PopulateValue(igd.Thorax);
                    questOth.PopulateValue(igd.Others);

                    gcsCtl.Condition = igd.Condition;
                    gcsCtl.Gcs = igd.Consciousness;

                    // Triage
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(assessment.RegistrationNo))
                    {
                        ComboBox.SelectedValue(ddlTriage, reg.SRTriage);
                    }
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
            igd.JalanNapas = questJalanNapas.GetQuestionAnswerValue();
            igd.KepalaLeher = questKepalaLeher.GetQuestionAnswerValue();
            igd.PenilaianBayi = questPenilaianBayi.GetQuestionAnswerValue();
            igd.Pernapasan = questPernapasan.GetQuestionAnswerValue();
            igd.Sirkulasi = questSirkulasi.GetQuestionAnswerValue();
            igd.Thorax = questThorax.GetQuestionAnswerValue();
            igd.Others = questOth.GetQuestionAnswerValue();

            igd.Condition = gcsCtl.Condition;
            igd.Consciousness = gcsCtl.Gcs;


            assessment.PhysicalExam = JsonConvert.SerializeObject(igd);

            // Triage
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(assessment.RegistrationNo))
            {
                reg.SRTriage = ddlTriage.SelectedValue;
                reg.Save();
            }

            // Objective
            rim.Info2 = GenerateSoapObjective(igd);
        }

        private string GenerateSoapObjective(Igd pe)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine("PRIMARY SURVEY:");
            strBuilder.AppendFormat("Triase: {0}", ddlTriage.SelectedText);
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