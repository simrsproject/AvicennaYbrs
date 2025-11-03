using System;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class InternalPe2Ctl : BaseAssessmentCtl
    {
        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);

        //    // Seting ReviewSystem Control
        //    var ros = new InternalRos();
        //    revSysUmum.QuestionGroupID = ros.Umum.QuestionGroupID;
        //    revSysMata.QuestionGroupID = ros.Mata.QuestionGroupID;
        //    revSysTht.QuestionGroupID = ros.Tht.QuestionGroupID;
        //    revSysCardiovas.QuestionGroupID = ros.Cardiovas.QuestionGroupID;
        //    revSysRespirasi.QuestionGroupID = ros.Respirasi.QuestionGroupID;
        //    revSysGastrointestinal.QuestionGroupID = ros.Gastrointestinal.QuestionGroupID;
        //    revSysSaluranKencing.QuestionGroupID = ros.SaluranKencing.QuestionGroupID;
        //    revSysMuscle.QuestionGroupID = ros.Muscle.QuestionGroupID;
        //    revSysHematologi.QuestionGroupID = ros.Hematologi.QuestionGroupID;
        //    revSysEndokrin.QuestionGroupID = ros.Endokrin.QuestionGroupID;
        //    revSysDermatologi.QuestionGroupID = ros.Dermatologi.QuestionGroupID;
        //    revSysNeurologi.QuestionGroupID = ros.Neurologi.QuestionGroupID;
        //    revSysPsikiatri.QuestionGroupID = ros.Psikiatri.QuestionGroupID;
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Get Education
            var asses = assessment;
            //txtOtherExam.Text = asses.OtherExam;


            // Convert to class w json
            try
            {
                //if (!string.IsNullOrEmpty(asses.ReviewOfSystem))
                //{
                //    var ros = JsonConvert.DeserializeObject<InternalRos>(asses.ReviewOfSystem);
                //    // Review System
                //    revSysUmum.PopulateValue(ros.Umum);
                //    revSysMata.PopulateValue(ros.Mata);
                //    revSysTht.PopulateValue(ros.Tht);
                //    revSysCardiovas.PopulateValue(ros.Cardiovas);
                //    revSysRespirasi.PopulateValue(ros.Respirasi);
                //    revSysGastrointestinal.PopulateValue(ros.Gastrointestinal);
                //    revSysSaluranKencing.PopulateValue(ros.SaluranKencing);
                //    revSysMuscle.PopulateValue(ros.Muscle);
                //    revSysHematologi.PopulateValue(ros.Hematologi);
                //    revSysEndokrin.PopulateValue(ros.Endokrin);
                //    revSysDermatologi.PopulateValue(ros.Dermatologi);
                //    revSysNeurologi.PopulateValue(ros.Neurologi);
                //    revSysPsikiatri.PopulateValue(ros.Psikiatri);
                //}

                if (!string.IsNullOrEmpty(asses.PhysicalExam))
                {
                    // Psycal Exam
                    var pExam = JsonConvert.DeserializeObject<InternalPe>(asses.PhysicalExam);
                    gcsCtl.Condition = pExam.Condition;
                    gcsCtl.Gcs = pExam.Consciousness;
                    optKepala.SelectedIndex = pExam.Kepala.IsAbNormal ? 1 : 0;
                    txtKepala.Text = pExam.Kepala.Notes;

                    optMata.SelectedIndex = pExam.Mata.IsAbNormal ? 1 : 0;
                    txtMata.Text = pExam.Mata.Notes;

                    optTht.SelectedIndex = pExam.Tht.IsAbNormal ? 1 : 0;
                    txtTht.Text = pExam.Tht.Notes;

                    optMulut.SelectedIndex = pExam.Mulut.IsAbNormal ? 1 : 0;
                    txtMulut.Text = pExam.Mulut.Notes;

                    optLeher.SelectedIndex = pExam.Leher.IsAbNormal ? 1 : 0;
                    txtLeher.Text = pExam.Leher.Notes;

                    optThorax.SelectedIndex = pExam.Thorax.IsAbNormal ? 1 : 0;
                    txtThorax.Text = pExam.Thorax.Notes;

                    optJantung.SelectedIndex = pExam.Jantung.IsAbNormal ? 1 : 0;
                    txtJantung.Text = pExam.Jantung.Notes;

                    optParu.SelectedIndex = pExam.Paru.IsAbNormal ? 1 : 0;
                    txtParu.Text = pExam.Paru.Notes;

                    optAbdomen.SelectedIndex = pExam.Abdomen.IsAbNormal ? 1 : 0;
                    txtAbdomen.Text = pExam.Abdomen.Notes;

                    optAuskulatasi.SelectedIndex = pExam.Auskulatasi.IsAbNormal ? 1 : 0;
                    txtAuskulatasi.Text = pExam.Auskulatasi.Notes;

                    optGenitaliaAndAnus.SelectedIndex = pExam.GenitaliaAndAnus.IsAbNormal ? 1 : 0;
                    txtGenitaliaAndAnus.Text = pExam.GenitaliaAndAnus.Notes;

                    optEkstremitas.SelectedIndex = pExam.Ekstremitas.IsAbNormal ? 1 : 0;
                    txtEkstremitas.Text = pExam.Ekstremitas.Notes;

                    optKulit.SelectedIndex = pExam.Kulit.IsAbNormal ? 1 : 0;
                    txtKulit.Text = pExam.Kulit.Notes;
                    txtPhysicalExamNotes.Text = pExam.Notes;
                }
            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            //var ros = new InternalRos();
            //// Review System
            //ros.Umum = revSysUmum.GetQuestionAnswerValue();
            //ros.Mata = revSysMata.GetQuestionAnswerValue();
            //ros.Tht = revSysTht.GetQuestionAnswerValue();
            //ros.Cardiovas = revSysCardiovas.GetQuestionAnswerValue();
            //ros.Respirasi = revSysRespirasi.GetQuestionAnswerValue();
            //ros.Gastrointestinal = revSysGastrointestinal.GetQuestionAnswerValue();
            //ros.SaluranKencing = revSysSaluranKencing.GetQuestionAnswerValue();
            //ros.Muscle = revSysMuscle.GetQuestionAnswerValue();
            //ros.Hematologi = revSysHematologi.GetQuestionAnswerValue();
            //ros.Psikiatri = revSysPsikiatri.GetQuestionAnswerValue();
            //ros.Endokrin = revSysEndokrin.GetQuestionAnswerValue();
            //ros.Dermatologi = revSysDermatologi.GetQuestionAnswerValue();
            //ros.Neurologi = revSysNeurologi.GetQuestionAnswerValue();


            // Pysical Exam
            var pExam = new InternalPe
            {
                Condition = gcsCtl.Condition,
                Consciousness = gcsCtl.Gcs,
                Kepala = new AbNormalAndNotes { IsAbNormal = optKepala.SelectedIndex == 1, Notes = txtKepala.Text },
                Mata = new AbNormalAndNotes { IsAbNormal = optMata.SelectedIndex == 1, Notes = txtMata.Text },
                Tht = new AbNormalAndNotes { IsAbNormal = optTht.SelectedIndex == 1, Notes = txtTht.Text },
                Mulut = new AbNormalAndNotes { IsAbNormal = optMulut.SelectedIndex == 1, Notes = txtMulut.Text },
                Leher = new AbNormalAndNotes { IsAbNormal = optLeher.SelectedIndex == 1, Notes = txtLeher.Text },
                Thorax = new AbNormalAndNotes { IsAbNormal = optThorax.SelectedIndex == 1, Notes = txtThorax.Text },
                Jantung = new AbNormalAndNotes { IsAbNormal = optJantung.SelectedIndex == 1, Notes = txtJantung.Text },
                Paru = new AbNormalAndNotes { IsAbNormal = optParu.SelectedIndex == 1, Notes = txtParu.Text },
                Abdomen = new AbNormalAndNotes { IsAbNormal = optAbdomen.SelectedIndex == 1, Notes = txtAbdomen.Text },
                Auskulatasi = new AbNormalAndNotes { IsAbNormal = optAuskulatasi.SelectedIndex == 1, Notes = txtAuskulatasi.Text },

                GenitaliaAndAnus = new AbNormalAndNotes
                {
                    IsAbNormal = optGenitaliaAndAnus.SelectedIndex == 1,
                    Notes = txtGenitaliaAndAnus.Text
                },
                Ekstremitas = new AbNormalAndNotes
                {
                    IsAbNormal = optEkstremitas.SelectedIndex == 1,
                    Notes = txtEkstremitas.Text
                },
                Kulit = new AbNormalAndNotes { IsAbNormal = optKulit.SelectedIndex == 1, Notes = txtKulit.Text },
                Notes = txtPhysicalExamNotes.Text
            };



            //assessment.OtherExam = txtOtherExam.Text;
            //assessment.ReviewOfSystem = JsonConvert.SerializeObject(ros);
            assessment.PhysicalExam = JsonConvert.SerializeObject(pExam);

            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(pExam));
            else
                rim.Info2 = GenerateSoapObjective(pExam);
        }

        private string GenerateSoapObjective(InternalPe pe)
        {
            var strBuilder = new StringBuilder();
            //if (!string.IsNullOrEmpty(pe.Condition))
            //{
            //    strBuilder.AppendFormat("Keadaan Umum: Sakit {0}", pe.Condition == "Mild" ? "Ringan" : pe.Condition == "Moderate" ? "Sedang" : "Berat");
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (!string.IsNullOrEmpty(pe.Consciousness.ConsciousnessDescription))
            //{
            //    strBuilder.AppendFormat("Kesadaran: {0} GCS: E: {1} M: {2} V: {3}",
            //        pe.Consciousness.ConsciousnessDescription, pe.Consciousness.Eye.Score, pe.Consciousness.Motor.Score,
            //        pe.Consciousness.Verbal.Score);
            //    strBuilder.AppendLine(string.Empty);
            //}
            strBuilder.AppendLine(pe.Consciousness.GetSoapObjective(pe.Condition));
            strBuilder.AppendLine("Tinjauan Sistem:");
            //SoapObjectiveAppend("Umum", ros.Umum.Summary, strBuilder);
            //SoapObjectiveAppend("Mata", ros.Mata.Summary, strBuilder);
            //SoapObjectiveAppend("THT", ros.Tht.Summary, strBuilder);
            //SoapObjectiveAppend("Mata", ros.Cardiovas.Summary, strBuilder);
            //SoapObjectiveAppend("Cardiovas", ros.Respirasi.Summary, strBuilder);
            //SoapObjectiveAppend("Gastrointestinal", ros.Gastrointestinal.Summary, strBuilder);
            //SoapObjectiveAppend("Saluran Kencing", ros.SaluranKencing.Summary, strBuilder);
            //SoapObjectiveAppend("Muscle Skeletal", ros.Muscle.Summary, strBuilder);
            //SoapObjectiveAppend("Hematologi", ros.Hematologi.Summary, strBuilder);
            //SoapObjectiveAppend("Endokrin", ros.Endokrin.Summary, strBuilder);
            //SoapObjectiveAppend("Dermatologi", ros.Dermatologi.Summary, strBuilder);
            //SoapObjectiveAppend("Neurologi", ros.Neurologi.Summary, strBuilder);
            //SoapObjectiveAppend("Psikiatri", ros.Psikiatri.Summary, strBuilder);

            strBuilder.AppendLine("");
            strBuilder.AppendLine("Pemeriksaan Fisik:");
            var isIncludeNormal = AppParameter.IsYes(AppParameter.ParameterItem.IsSoapFromPysicalExamIncludeNormalValue);

            if (isIncludeNormal || pe.Kepala.IsAbNormal)
            {
                strBuilder.AppendFormat("Kepala: {1}: {0}", pe.Kepala.Notes, pe.Kepala.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Mata.IsAbNormal)
            {
                strBuilder.AppendFormat("Mata: {1}: {0}", pe.Mata.Notes, pe.Mata.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Tht.IsAbNormal)
            {
                strBuilder.AppendFormat("THT: {1}: {0}", pe.Tht.Notes, pe.Tht.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Mulut.IsAbNormal)
            {
                strBuilder.AppendFormat("Mulut: {1}: {0}", pe.Mulut.Notes, pe.Mulut.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Leher.IsAbNormal)
            {
                strBuilder.AppendFormat("Leher: {1}:{0}", pe.Leher.Notes, pe.Leher.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Thorax.IsAbNormal)
            {
                strBuilder.AppendFormat("Thorax: {1}: {0}", pe.Thorax.Notes, pe.Thorax.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Jantung.IsAbNormal)
            {
                strBuilder.AppendFormat("Jantung: {1}: {0}", pe.Jantung.Notes, pe.Jantung.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Paru.IsAbNormal)
            {
                strBuilder.AppendFormat("Paru: {1}: {0}", pe.Paru.Notes, pe.Paru.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Abdomen.IsAbNormal)
            {
                strBuilder.AppendFormat("Abdomen: {1}: {0}", pe.Abdomen.Notes, pe.Abdomen.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Auskulatasi.IsAbNormal)
            {
                strBuilder.AppendFormat("Auskulatasi: {1}: {0}", pe.Auskulatasi.Notes, pe.Auskulatasi.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.GenitaliaAndAnus.IsAbNormal)
            {
                strBuilder.AppendFormat("Genitalia & Anus: {1} : {0}", pe.GenitaliaAndAnus.Notes, pe.GenitaliaAndAnus.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Ekstremitas.IsAbNormal)
            {
                strBuilder.AppendFormat("Ekstremitas: {1}: {0}", pe.Ekstremitas.Notes, pe.Ekstremitas.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Ekstremitas.IsAbNormal)
            {
                strBuilder.AppendFormat("Kulit: {1}: {0}", pe.Kulit.Notes, pe.Kulit.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (!string.IsNullOrEmpty(pe.Notes))
            {
                strBuilder.AppendFormat("{0}", pe.Notes);
                strBuilder.AppendLine(string.Empty);
            }

            return strBuilder.ToString();
        }
        private static void SoapObjectiveAppend(string caption, string value, StringBuilder strBuilder)
        {
            if (!string.IsNullOrEmpty(value))
            {
                strBuilder.AppendFormat("{0}: {1}", caption, value);
                strBuilder.AppendLine();
            }
        }
        #endregion


    }
}