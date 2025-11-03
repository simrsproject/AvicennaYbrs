using System;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class ObesyPeCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Seting ReviewSystem Control
            var ros = new ObesyRos();
            qstFitness.QuestionGroupID = ros.Fitness.QuestionGroupID;
            qstHabit.QuestionGroupID = ros.Habit.QuestionGroupID;
            qstMentalist.QuestionGroupID = ros.Mentalist.QuestionGroupID;
            qstNutrition.QuestionGroupID = ros.NutritionAnalisys.QuestionGroupID;
            qstObesytas.QuestionGroupID = ros.ObeHist.QuestionGroupID;
            qstParqTest.QuestionGroupID = ros.ParqTest.QuestionGroupID;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            trNutriSkrinning.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSTJ";

            txtKepala.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optKepala.ClientID + "'); }";
            txtMata.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optMata.ClientID + "'); }";
            txtTht.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optTht.ClientID + "'); }";
            txtMulut.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optMulut.ClientID + "'); }";
            txtLeher.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optLeher.ClientID + "'); }";
            txtThorax.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optThorax.ClientID + "'); }";
            txtGenitaliaAndAnus.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optGenitaliaAndAnus.ClientID + "'); }";
            txtEkstremitas.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optEkstremitas.ClientID + "'); }";
            txtKulit.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optKulit.ClientID + "'); }";
        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Convert to class w json
            if (!string.IsNullOrEmpty(assessment.ReviewOfSystem))
            {
                var ros = JsonConvert.DeserializeObject<ObesyRos>(assessment.ReviewOfSystem);
                // Review System
                qstFitness.PopulateValue(ros.Fitness);
                qstHabit.PopulateValue(ros.Habit);
                qstMentalist.PopulateValue(ros.Mentalist);
                qstNutrition.PopulateValue(ros.NutritionAnalisys);
                qstObesytas.PopulateValue(ros.ObeHist);
                qstParqTest.PopulateValue(ros.ParqTest);
            }

            if (!string.IsNullOrEmpty(assessment.PhysicalExam))
            {
                var pExam = JsonConvert.DeserializeObject<ObesyPe>(assessment.PhysicalExam);
                // Psycal Exam
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

                pemHeart.PhysicalExamMetod = pExam.Jantung;
                pemLung.PhysicalExamMetod = pExam.Paru;
                pemAbdomen.PhysicalExamMetod = pExam.Abdomen;
                optGenitaliaAndAnus.SelectedIndex = pExam.GenitaliaAndAnus.IsAbNormal ? 1 : 0;
                txtGenitaliaAndAnus.Text = pExam.GenitaliaAndAnus.Notes;

                optEkstremitas.SelectedIndex = pExam.Ekstremitas.IsAbNormal ? 1 : 0;
                txtEkstremitas.Text = pExam.Ekstremitas.Notes;

                optKulit.SelectedIndex = pExam.Kulit.IsAbNormal ? 1 : 0;
                txtKulit.Text = pExam.Kulit.Notes;

                txtNotes.Text = pExam.Notes;
                optNutritionSkrinning.SelectedValue = pExam.NutritionSkrinning;
            }

            txtAssessPsychology.Text = assessment.OtherExam;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment,
            RegistrationInfoMedic rim)
        {
            // Simpan
            var asses = assessment;

            // Pysical Exam
            var pExam = new ObesyPe
            {
                Condition = gcsCtl.Condition,
                Consciousness = gcsCtl.Gcs,
                Kepala = new AbNormalAndNotes { IsAbNormal = optKepala.SelectedIndex == 1, Notes = txtKepala.Text },
                Mata = new AbNormalAndNotes { IsAbNormal = optMata.SelectedIndex == 1, Notes = txtMata.Text },
                Tht = new AbNormalAndNotes { IsAbNormal = optTht.SelectedIndex == 1, Notes = txtTht.Text },
                Mulut = new AbNormalAndNotes { IsAbNormal = optMulut.SelectedIndex == 1, Notes = txtMulut.Text },
                Leher = new AbNormalAndNotes { IsAbNormal = optLeher.SelectedIndex == 1, Notes = txtLeher.Text },
                Thorax = new AbNormalAndNotes { IsAbNormal = optThorax.SelectedIndex == 1, Notes = txtThorax.Text },
                Jantung = pemHeart.PhysicalExamMetod,
                Paru = pemLung.PhysicalExamMetod,
                Abdomen = pemAbdomen.PhysicalExamMetod,
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
                Notes = txtNotes.Text,
                NutritionSkrinning = optNutritionSkrinning.SelectedValue
            };

            asses.PhysicalExam = JsonConvert.SerializeObject(pExam);

            // Review System
            var ros = new ObesyRos
            {
                Fitness = qstFitness.GetQuestionAnswerValue(),
                Habit = qstHabit.GetQuestionAnswerValue(),
                Mentalist = qstMentalist.GetQuestionAnswerValue(),
                NutritionAnalisys = qstNutrition.GetQuestionAnswerValue(),
                ObeHist = qstObesytas.GetQuestionAnswerValue(),
                ParqTest = qstParqTest.GetQuestionAnswerValue()
            };

            asses.ReviewOfSystem = JsonConvert.SerializeObject(ros);

            asses.OtherExam = txtAssessPsychology.Text;

            // Objective
            rim.Info2 = GenerateSoapObjective(pExam);
        }

        private string GenerateSoapObjective(ObesyPe pe)
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
                strBuilder.AppendFormat("Leher: {1}: {0}", pe.Leher.Notes, pe.Leher.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Thorax.IsAbNormal)
            {
                strBuilder.AppendFormat("Thorax: {1}: {0}", pe.Thorax.Notes, pe.Thorax.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            AddPhysicalExamMetod(pe.Jantung, strBuilder);
            AddPhysicalExamMetod(pe.Paru, strBuilder);
            AddPhysicalExamMetod(pe.Abdomen, strBuilder);

            if (isIncludeNormal || pe.GenitaliaAndAnus.IsAbNormal)
            {
                strBuilder.AppendFormat("Genitalia & Anus: {1}: {0}", pe.GenitaliaAndAnus.Notes, pe.GenitaliaAndAnus.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Ekstremitas.IsAbNormal)
            {
                strBuilder.AppendFormat("Ekstremitas: {1}: {0}", pe.Ekstremitas.Notes, pe.Ekstremitas.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Kulit.IsAbNormal)
            {
                strBuilder.AppendFormat("Kulit: {1}: {0}", pe.Kulit.Notes, pe.Kulit.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.NutritionSkrinning))
            {
                strBuilder.AppendFormat("Skrinning Gizi: {0}", pe.NutritionSkrinning);
                strBuilder.AppendLine(string.Empty);
            }
            return strBuilder.ToString();
        }


        private void AddPhysicalExamMetod(PhysicalExamMetod pem, StringBuilder strBuilder)
        {
            if (pem.AbNormalAndNotes.IsAbNormal)
            {
                strBuilder.AppendFormat("Jantung: Abnormal: {0}", pem.AbNormalAndNotes.Notes);
                strBuilder.AppendLine(string.Empty);
            }

            if (!string.IsNullOrEmpty(pem.Inspeksi))
            {
                strBuilder.AppendFormat("•	Inspeksi: {0}", pem.Inspeksi);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pem.Palpasi))
            {
                strBuilder.AppendFormat("•	Palpasi: {0}", pem.Palpasi);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pem.Perkusi))
            {
                strBuilder.AppendFormat("•	Perkusi: {0}", pem.Perkusi);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pem.Auskultasi))
            {
                strBuilder.AppendFormat("•	Auskultasi: {0}", pem.Auskultasi);
                strBuilder.AppendLine(string.Empty);
            }

        }
        #endregion


    }
}