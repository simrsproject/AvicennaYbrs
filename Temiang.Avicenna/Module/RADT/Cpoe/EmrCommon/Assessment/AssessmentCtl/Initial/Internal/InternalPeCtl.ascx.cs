using System;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class InternalPeCtl : BaseAssessmentCtl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Seting ReviewSystem Control
            var ros = new InternalRos();
            revSysUmum.QuestionGroupID = ros.Umum.QuestionGroupID;
            revSysMata.QuestionGroupID = ros.Mata.QuestionGroupID;
            revSysTht.QuestionGroupID = ros.Tht.QuestionGroupID;
            revSysCardiovas.QuestionGroupID = ros.Cardiovas.QuestionGroupID;
            revSysRespirasi.QuestionGroupID = ros.Respirasi.QuestionGroupID;
            revSysGastrointestinal.QuestionGroupID = ros.Gastrointestinal.QuestionGroupID;
            revSysSaluranKencing.QuestionGroupID = ros.SaluranKencing.QuestionGroupID;
            revSysMuscle.QuestionGroupID = ros.Muscle.QuestionGroupID;
            revSysHematologi.QuestionGroupID = ros.Hematologi.QuestionGroupID;
            revSysEndokrin.QuestionGroupID = ros.Endokrin.QuestionGroupID;
            revSysDermatologi.QuestionGroupID = ros.Dermatologi.QuestionGroupID;
            revSysNeurologi.QuestionGroupID = ros.Neurologi.QuestionGroupID;
            revSysPsikiatri.QuestionGroupID = ros.Psikiatri.QuestionGroupID;
            revSysSistemPernafasan.QuestionGroupID = ros.SistemPernafasan.QuestionGroupID;
            revSysSistemKardiovaskular.QuestionGroupID = ros.SistemKardiovaskular.QuestionGroupID;
            revSysSistemPersyarafan.QuestionGroupID = ros.SistemPersyarafan.QuestionGroupID;
            revSysSistemEkskresi.QuestionGroupID = ros.SistemEkskresi.QuestionGroupID;
            revSysSistemPencernaan.QuestionGroupID = ros.SistemPencernaan.QuestionGroupID;
            revSysSistemMuskuloskeletal.QuestionGroupID = ros.SistemMuskuloskeletal.QuestionGroupID;
            revSysSistemReproduksi.QuestionGroupID = ros.SistemReproduksi.QuestionGroupID;
            revSysDataPsikoSosiSpi.QuestionGroupID = ros.DataPsikoSosiSpi.QuestionGroupID;
            revSysHambatanDiri.QuestionGroupID = ros.HambatanDiri.QuestionGroupID;

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
            txtJantung.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optJantung.ClientID + "'); }";
            txtParu.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optParu.ClientID + "'); }";
            txtAbdomen.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optAbdomen.ClientID + "'); }";
            txtAuskulatasi.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optAuskulatasi.ClientID + "'); }";
            txtGenitaliaAndAnus.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optGenitaliaAndAnus.ClientID + "'); }";
            txtEkstremitas.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optEkstremitas.ClientID + "'); }";
            txtKulit.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optKulit.ClientID + "'); }";
        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            // Get Education
            var asses = assessment;
            txtOtherExam.Text = asses.OtherExam;


            // Convert to class w json
            try
            {
                if (!string.IsNullOrEmpty(asses.ReviewOfSystem))
                {
                    var ros = JsonConvert.DeserializeObject<InternalRos>(asses.ReviewOfSystem);
                    // Review System
                    revSysUmum.PopulateValue(ros.Umum);
                    revSysMata.PopulateValue(ros.Mata);
                    revSysTht.PopulateValue(ros.Tht);
                    revSysCardiovas.PopulateValue(ros.Cardiovas);
                    revSysRespirasi.PopulateValue(ros.Respirasi);
                    revSysGastrointestinal.PopulateValue(ros.Gastrointestinal);
                    revSysSaluranKencing.PopulateValue(ros.SaluranKencing);
                    revSysMuscle.PopulateValue(ros.Muscle);
                    revSysHematologi.PopulateValue(ros.Hematologi);
                    revSysEndokrin.PopulateValue(ros.Endokrin);
                    revSysDermatologi.PopulateValue(ros.Dermatologi);
                    revSysNeurologi.PopulateValue(ros.Neurologi);
                    revSysPsikiatri.PopulateValue(ros.Psikiatri);
                    revSysSistemPernafasan.PopulateValue(ros.SistemPernafasan);
                    revSysSistemKardiovaskular.PopulateValue(ros.SistemKardiovaskular);
                    revSysSistemPersyarafan.PopulateValue(ros.SistemPersyarafan);
                    revSysSistemPencernaan.PopulateValue(ros.SistemPencernaan);
                    revSysSistemMuskuloskeletal.PopulateValue(ros.SistemMuskuloskeletal);
                    revSysSistemReproduksi.PopulateValue(ros.SistemReproduksi);
                    revSysDataPsikoSosiSpi.PopulateValue(ros.DataPsikoSosiSpi);
                    revSysHambatanDiri.PopulateValue(ros.HambatanDiri);
                }

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
                    optNutritionSkrinning.SelectedValue = pExam.NutritionSkrinning;
                }
            }
            catch (Exception)
            {
                // Nothing 
            }
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var ros = new InternalRos();
            // Review System
            ros.Umum = revSysUmum.GetQuestionAnswerValue();
            ros.Mata = revSysMata.GetQuestionAnswerValue();
            ros.Tht = revSysTht.GetQuestionAnswerValue();
            ros.Cardiovas = revSysCardiovas.GetQuestionAnswerValue();
            ros.Respirasi = revSysRespirasi.GetQuestionAnswerValue();
            ros.Gastrointestinal = revSysGastrointestinal.GetQuestionAnswerValue();
            ros.SaluranKencing = revSysSaluranKencing.GetQuestionAnswerValue();
            ros.Muscle = revSysMuscle.GetQuestionAnswerValue();
            ros.Hematologi = revSysHematologi.GetQuestionAnswerValue();
            ros.Psikiatri = revSysPsikiatri.GetQuestionAnswerValue();
            ros.Endokrin = revSysEndokrin.GetQuestionAnswerValue();
            ros.Dermatologi = revSysDermatologi.GetQuestionAnswerValue();
            ros.Neurologi = revSysNeurologi.GetQuestionAnswerValue();
            ros.SistemPernafasan = revSysSistemPernafasan.GetQuestionAnswerValue();
            ros.SistemKardiovaskular = revSysSistemKardiovaskular.GetQuestionAnswerValue();
            ros.SistemPersyarafan = revSysSistemPersyarafan.GetQuestionAnswerValue();
            ros.SistemEkskresi = revSysSistemEkskresi.GetQuestionAnswerValue();
            ros.SistemPencernaan = revSysSistemPencernaan.GetQuestionAnswerValue();
            ros.SistemMuskuloskeletal = revSysSistemMuskuloskeletal.GetQuestionAnswerValue();
            ros.SistemReproduksi = revSysSistemReproduksi.GetQuestionAnswerValue();
            ros.DataPsikoSosiSpi = revSysDataPsikoSosiSpi.GetQuestionAnswerValue();
            ros.HambatanDiri = revSysHambatanDiri.GetQuestionAnswerValue();


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
                Notes = txtPhysicalExamNotes.Text,
                NutritionSkrinning = optNutritionSkrinning.SelectedValue
            };



            assessment.OtherExam = txtOtherExam.Text;
            assessment.ReviewOfSystem = JsonConvert.SerializeObject(ros);
            assessment.PhysicalExam = JsonConvert.SerializeObject(pExam);

            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(ros, pExam));
            else
                rim.Info2 = GenerateSoapObjective(ros, pExam);
        }

        private string GenerateSoapObjective(InternalRos ros, InternalPe pe)
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

            var strHeaderTs = new StringBuilder();
            strHeaderTs.AppendLine("Tinjauan Sistem:");
            var headTs = strHeaderTs.ToString();

            var strDetailTs = new StringBuilder();
            SoapObjectiveAppend("Umum", ros.Umum.Summary, strDetailTs);
            SoapObjectiveAppend("Mata", ros.Mata.Summary, strDetailTs);
            SoapObjectiveAppend("THT", ros.Tht.Summary, strDetailTs);
            SoapObjectiveAppend("Mata", ros.Cardiovas.Summary, strDetailTs);
            SoapObjectiveAppend("Cardiovas", ros.Respirasi.Summary, strDetailTs);
            SoapObjectiveAppend("Gastrointestinal", ros.Gastrointestinal.Summary, strDetailTs);
            SoapObjectiveAppend("Saluran Kencing", ros.SaluranKencing.Summary, strDetailTs);
            SoapObjectiveAppend("Muscle Skeletal", ros.Muscle.Summary, strDetailTs);
            SoapObjectiveAppend("Hematologi", ros.Hematologi.Summary, strDetailTs);
            SoapObjectiveAppend("Endokrin", ros.Endokrin.Summary, strDetailTs);
            SoapObjectiveAppend("Dermatologi", ros.Dermatologi.Summary, strDetailTs);
            SoapObjectiveAppend("Neurologi", ros.Neurologi.Summary, strDetailTs);
            SoapObjectiveAppend("Psikiatri", ros.Psikiatri.Summary, strDetailTs);
            SoapObjectiveAppend("Sistem Pernafasan", ros.SistemPernafasan.Summary, strDetailTs);
            SoapObjectiveAppend("Sistem Kardiovaskular", ros.SistemKardiovaskular.Summary, strDetailTs);
            SoapObjectiveAppend("Sistem Persyarafan", ros.SistemPersyarafan.Summary, strDetailTs);
            SoapObjectiveAppend("Sistem Ekskresi", ros.SistemEkskresi.Summary, strDetailTs);
            SoapObjectiveAppend("Sistem Pencernaan", ros.SistemPencernaan.Summary, strDetailTs);
            SoapObjectiveAppend("Sistem Muskuloskeletal", ros.SistemMuskuloskeletal.Summary, strDetailTs);
            SoapObjectiveAppend("Sistem Reproduksi", ros.SistemReproduksi.Summary, strDetailTs);
            SoapObjectiveAppend("Data Psikologis, Sosiologis, dan Spritual", ros.DataPsikoSosiSpi.Summary, strDetailTs);
            SoapObjectiveAppend("Hambatan Diri", ros.HambatanDiri.Summary, strDetailTs);

            var detailTs = strDetailTs.ToString();
            if (!string.IsNullOrEmpty(detailTs))
            {
                strBuilder.AppendLine(headTs);
                strBuilder.AppendLine(detailTs);
            }

            var strHeaderPf = new StringBuilder();
            strHeaderPf.AppendLine("Pemeriksaan Fisik:");
            var headerPf = strHeaderPf.ToString();

            var strDetailPf = new StringBuilder();
            var isIncludeNormal = AppParameter.IsYes(AppParameter.ParameterItem.IsSoapFromPysicalExamIncludeNormalValue);
            if (isIncludeNormal || pe.Kepala.IsAbNormal)
            {
                strDetailPf.AppendFormat("Kepala: {1}: {0}", pe.Kepala.Notes, pe.Kepala.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Mata.IsAbNormal)
            {
                strDetailPf.AppendFormat("Mata: {1}: {0}", pe.Mata.Notes, pe.Mata.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Tht.IsAbNormal)
            {
                strDetailPf.AppendFormat("THT: {1}: {0}", pe.Tht.Notes, pe.Tht.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Mulut.IsAbNormal)
            {
                strDetailPf.AppendFormat("Mulut: {1}: {0}", pe.Mulut.Notes, pe.Mulut.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Leher.IsAbNormal)
            {
                strDetailPf.AppendFormat("Leher: {1}:{0}", pe.Leher.Notes, pe.Leher.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Thorax.IsAbNormal)
            {
                strDetailPf.AppendFormat("Thorax: {1}: {0}", pe.Thorax.Notes, pe.Thorax.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Jantung.IsAbNormal)
            {
                strDetailPf.AppendFormat("Jantung: {1}: {0}", pe.Jantung.Notes, pe.Jantung.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Paru.IsAbNormal)
            {
                strDetailPf.AppendFormat("Paru: {1}: {0}", pe.Paru.Notes, pe.Paru.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Abdomen.IsAbNormal)
            {
                strDetailPf.AppendFormat("Abdomen: {1}: {0}", pe.Abdomen.Notes, pe.Abdomen.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Auskulatasi.IsAbNormal)
            {
                strDetailPf.AppendFormat("Auskulatasi: {1}: {0}", pe.Auskulatasi.Notes, pe.Auskulatasi.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.GenitaliaAndAnus.IsAbNormal)
            {
                strDetailPf.AppendFormat("Genitalia & Anus: {1} : {0}", pe.GenitaliaAndAnus.Notes, pe.GenitaliaAndAnus.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Ekstremitas.IsAbNormal)
            {
                strDetailPf.AppendFormat("Ekstremitas: {1}: {0}", pe.Ekstremitas.Notes, pe.Ekstremitas.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Ekstremitas.IsAbNormal)
            {
                strDetailPf.AppendFormat("Kulit: {1}: {0}", pe.Kulit.Notes, pe.Kulit.IsAbNormal ? "Abnormal" : "Normal");
                strDetailPf.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.NutritionSkrinning))
            {
                strBuilder.AppendFormat("Skrinning Gizi: {0}", pe.NutritionSkrinning);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Notes))
            {
                strDetailPf.AppendFormat("{0}", pe.Notes);
                strDetailPf.AppendLine(string.Empty);
            }

            var detailPf = strDetailPf.ToString();
            if (!string.IsNullOrEmpty(detailPf))
            {
                strBuilder.AppendLine(headerPf);
                strBuilder.AppendLine(detailPf);
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