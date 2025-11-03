using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Utils.Extensions;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class KidPeCtl : BaseAssessmentCtl
    {
        public override EntryGroupEnum EntryGroup
        {
            get { return EntryGroupEnum.PhysicalExam; }
        }

        public override ColumnEnum Column
        {
            get { return ColumnEnum.Left; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            trNutriSkrinning.Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSSTJ";
            if(AppSession.Parameter.HealthcareInitialAppsVersion == "RSJKT")
            {
                optReflexPatologis.SelectedValue = "A";
            }

            txtKepala.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optKepala.ClientID + "'); }";
            txtMata.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optMata.ClientID + "'); }";
            txtTht.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optTht.ClientID + "'); }";
            txtMulut.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optMulut.ClientID + "'); }";
            txtLeher.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optLeher.ClientID + "'); }";
            txtThorax.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optThorax.ClientID + "'); }";
            txtJantung.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optJantung.ClientID + "'); }";
            txtJantungBunyi.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optJantungBunyi.ClientID + "'); }";
            txtParu.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optParu.ClientID + "'); }";
            txtParuPergerakan.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optParuPergerakan.ClientID + "'); }";
            txtParuPerkusi.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optParuPerkusi.ClientID + "'); }";
            txtParuPernapasan.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optParuPernapasan.ClientID + "'); }";
            txtAbdomenKelainan.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optAbdomenKelainan.ClientID + "'); }";
            txtAbdomenBenjolan.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optAbdomenBenjolan.ClientID + "'); }";
            txtAbdomenNyeriTekan.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optAbdomenNyeriTekan.ClientID + "'); }";
            txtAbdomenHernia.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optAbdomenHernia.ClientID + "'); }";
            txtAbdomenBisingUsus.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optAbdomenBisingUsus.ClientID + "'); }";
            txtAbdomenDistensi.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optAbdomenDistensi.ClientID + "'); }";
            txtSpineLimb.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optSpineLimb.ClientID + "'); }";
            txtGenitaliaAndAnus.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optGenitaliaAndAnus.ClientID + "'); }";
            txtGenitaliaPenis.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optGenitaliaPenis.ClientID + "'); }";
            txtGenitaliaTestis.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optGenitaliaTestis.ClientID + "'); }";
            txtGenitaliaLabiaMinor.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optGenitaliaLabiaMinor.ClientID + "'); }";
            txtGenitaliaAnus.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optGenitaliaAnus.ClientID + "'); }";
            txtEkstremitas.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optEkstremitas.ClientID + "'); }";
            txtEkstremitasEdema.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optEkstremitasEdema.ClientID + "'); }";
            txtEkstremitasCrt.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optEkstremitasCrt.ClientID + "'); }";
            txtKulit.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optKulit.ClientID + "'); }";
        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            var ent = new KidPe();

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    ent = JsonConvert.DeserializeObject<KidPe>(asses.PhysicalExam);
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

            gcsCtl.Condition = ent.Condition;
            gcsCtl.Gcs = ent.Consciousness;

            optKepala.SelectedIndex = ent.Kepala.IsAbNormal ? 1 : 0;
            txtKepala.Text = ent.Kepala.Notes;

            optMata.SelectedIndex = ent.Mata.IsAbNormal ? 1 : 0;
            txtMata.Text = ent.Mata.Notes;

            optTht.SelectedIndex = ent.Tht.IsAbNormal ? 1 : 0;
            txtTht.Text = ent.Tht.Notes;

            optMulut.SelectedIndex = ent.Mulut.IsAbNormal ? 1 : 0;
            txtMulut.Text = ent.Mulut.Notes;

            optLeher.SelectedIndex = ent.Leher.IsAbNormal ? 1 : 0;
            txtLeher.Text = ent.Leher.Notes;

            optThorax.SelectedIndex = ent.Thorax.IsAbNormal ? 1 : 0;
            txtThorax.Text = ent.Thorax.Notes;

            optJantung.SelectedIndex = ent.Jantung.IsAbNormal ? 1 : 0;
            txtJantung.Text = ent.Jantung.Notes;
            optJantungIrama.SelectedIndex = ent.JantungIrama.IsAbNormal ? 1 : 0;
            txtJantungIrama.Text = ent.JantungIrama.Notes;
            optJantungBunyi.SelectedIndex = ent.JantungBunyi.IsAbNormal ? 1 : 0;
            txtJantungBunyi.Text = ent.JantungBunyi.Notes;
            txtJantungInspeksi.Text = ent.JantungPem.Inspeksi;
            txtJantungPalpasi.Text = ent.JantungPem.Palpasi;
            txtJantungPerkusi.Text = ent.JantungPem.Perkusi;
            txtJantungAuskulatasi.Text = ent.JantungPem.Auskultasi;

            optParu.SelectedIndex = ent.Paru.IsAbNormal ? 1 : 0;
            txtParu.Text = ent.Paru.Notes;
            optParuPergerakan.SelectedIndex = ent.ParuPergerakan.IsAbNormal ? 1 : 0;
            txtParuPergerakan.Text = ent.ParuPergerakan.Notes;
            optParuPerkusi.SelectedIndex = ent.ParuPerkusi.IsAbNormal ? 1 : 0;
            optParuPernapasan.SelectedIndex = ent.ParuPernapasan.IsAbNormal ? 1 : 0;
            optParuRonchi.SelectedIndex = ent.ParuRonchi.IsAbNormal ? 0 : 1;
            optParuWheezing.SelectedIndex = ent.ParuWheezing.IsAbNormal ? 0 : 1;
            txtParuInspeksi.Text = ent.ParuPem.Inspeksi;
            txtParuPalpasi.Text = ent.ParuPem.Palpasi;
            txtParuPerkusi.Text = ent.ParuPem.Perkusi;
            txtParuPernapasan.Text = ent.ParuPernapasan.Notes;
            txtParuAuskulatasi.Text = ent.ParuPem.Auskultasi;

            optAbdomen.SelectedIndex = ent.Abdomen.IsAbNormal ? 1 : 0;
            txtAbdomen.Text = ent.Abdomen.Notes;
            optAbdomenKelainan.SelectedIndex = ent.AbdomenKelainan.IsAbNormal ? 1 : 0;
            txtAbdomenKelainan.Text = ent.AbdomenKelainan.Notes;
            optAbdomenBenjolan.SelectedIndex = ent.AbdomenBenjolan.IsAbNormal ? 1 : 0;
            txtAbdomenBenjolan.Text = ent.AbdomenBenjolan.Notes;
            optAbdomenNyeriTekan.SelectedIndex = ent.AbdomenNyeriTekan.IsAbNormal ? 1 : 0;
            txtAbdomenNyeriTekan.Text = ent.AbdomenNyeriTekan.Notes;
            optAbdomenHernia.SelectedIndex = ent.AbdomenHernia.IsAbNormal ? 1 : 0;
            txtAbdomenHernia.Text = ent.AbdomenHernia.Notes;
            optAbdomenBisingUsus.SelectedIndex = ent.AbdomenBisingUsus.IsAbNormal ? 1 : 0;
            txtAbdomenBisingUsus.Text = ent.AbdomenBisingUsus.Notes;
            optAbdomenDistensi.SelectedIndex = ent.AbdomenDistensi.IsAbNormal ? 1 : 0;
            txtAbdomenDistensi.Text = ent.AbdomenDistensi.Notes;
            txtAbdomenInspeksi.Text = ent.AbdomenPem.Inspeksi;
            txtAbdomenPalpasi.Text = ent.AbdomenPem.Palpasi;
            txtAbdomenPerkusi.Text = ent.AbdomenPem.Perkusi;
            txtAbdomenAuskulatasi.Text = ent.AbdomenPem.Auskultasi;

            optSpineLimb.SelectedIndex = ent.SpineLimb.IsAbNormal ? 1 : 0;
            txtSpineLimb.Text = ent.SpineLimb.Notes;

            optGenitaliaAndAnus.SelectedIndex = ent.GenitaliaAndAnus.IsAbNormal ? 1 : 0;
            txtGenitaliaAndAnus.Text = ent.GenitaliaAndAnus.Notes;
            optGenitaliaPenis.SelectedIndex = ent.GenitaliaPenis.IsAbNormal ? 1 : 0;
            txtGenitaliaPenis.Text = ent.GenitaliaPenis.Notes;
            optGenitaliaTestis.SelectedIndex = ent.GenitaliaTestis.IsAbNormal ? 1 : 0;
            txtGenitaliaTestis.Text = ent.GenitaliaTestis.Notes;
            optGenitaliaLabiaMinor.SelectedIndex = ent.GenitaliaLabiaMinor.IsAbNormal ? 1 : 0;
            txtGenitaliaLabiaMinor.Text = ent.GenitaliaLabiaMinor.Notes;
            optGenitaliaAnus.SelectedIndex = ent.GenitaliaAnus.IsAbNormal ? 1 : 0;
            txtGenitaliaAnus.Text = ent.GenitaliaAnus.Notes;

            optEkstremitas.SelectedIndex = ent.Ekstremitas.IsAbNormal ? 1 : 0;
            txtEkstremitas.Text = ent.Ekstremitas.Notes;
            optEkstremitasEdema.SelectedIndex = ent.EkstremitasEdema.IsAbNormal ? 1 : 0;
            txtEkstremitasEdema.Text = ent.EkstremitasEdema.Notes;
            optEkstremitasCrt.SelectedIndex = ent.EkstremitasCrt.IsAbNormal ? 1 : 0;
            txtEkstremitasCrt.Text = ent.EkstremitasCrt.Notes;

            optKulit.SelectedIndex = ent.Kulit.IsAbNormal ? 1 : 0;
            txtKulit.Text = ent.Kulit.Notes;

            txtOther.Text = ent.Other;
            txtPhysicalExamNotes.Text = ent.Notes;
            optNutritionSkrinning.SelectedValue = ent.NutritionSkrinning;

            optGrm.SelectedIndex = ent.StatusNeurogis.Grm ? 1 : 0;
            optReflexFisiologis.SelectedIndex = ent.StatusNeurogis.Fisiologis.IsAbNormal ? 1 : 0;
            txtReflexFisiologis.Text = ent.StatusNeurogis.Fisiologis.Notes;
            optReflexPatologis.SelectedIndex = ent.StatusNeurogis.Patologis ? 1 : 0;
            optMotoric.SelectedIndex = ent.StatusNeurogis.Motorik.IsAbNormal ? 1 : 0;
            txtMotoric.Text = ent.StatusNeurogis.Motorik.Notes;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var pExam = new KidPe
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
                JantungIrama = new AbNormalAndNotes { IsAbNormal = optJantungIrama.SelectedIndex == 1, Notes = txtJantungIrama.Text },
                JantungBunyi = new AbNormalAndNotes { IsAbNormal = optJantungBunyi.SelectedIndex == 1, Notes = txtJantungBunyi.Text },
                JantungPem = new PhysicalExamMetod { Inspeksi = txtJantungInspeksi.Text, Palpasi = txtJantungPalpasi.Text, Perkusi = txtJantungPerkusi.Text, Auskultasi = txtJantungAuskulatasi.Text },

                Paru = new AbNormalAndNotes { IsAbNormal = optParu.SelectedIndex == 1, Notes = txtParu.Text },
                ParuPergerakan = new AbNormalAndNotes { IsAbNormal = optParuPergerakan.SelectedIndex == 1, Notes = txtParuPergerakan.Text },
                ParuPerkusi = new AbNormalAndNotes { IsAbNormal = optParuPerkusi.SelectedIndex == 1, Notes = txtParuPerkusi.Text },
                ParuPernapasan = new AbNormalAndNotes { IsAbNormal = optParuPernapasan.SelectedIndex == 1, Notes = txtParuPernapasan.Text },
                ParuRonchi = new AbNormalAndNotes { IsAbNormal = optParuRonchi.SelectedIndex == 0, Notes = string.Empty },
                ParuWheezing = new AbNormalAndNotes { IsAbNormal = optParuWheezing.SelectedIndex == 0, Notes = string.Empty },
                ParuPem = new PhysicalExamMetod { Inspeksi = txtParuInspeksi.Text, Palpasi = txtParuPalpasi.Text, Perkusi = txtParuPerkusi.Text, Auskultasi = txtParuAuskulatasi.Text },

                Abdomen = new AbNormalAndNotes { IsAbNormal = optAbdomen.SelectedIndex == 1, Notes = txtAbdomen.Text },
                AbdomenKelainan = new AbNormalAndNotes { IsAbNormal = optAbdomenKelainan.SelectedIndex == 1, Notes = txtAbdomenKelainan.Text },
                AbdomenBenjolan = new AbNormalAndNotes { IsAbNormal = optAbdomenBenjolan.SelectedIndex == 1, Notes = txtAbdomenBenjolan.Text },
                AbdomenNyeriTekan = new AbNormalAndNotes { IsAbNormal = optAbdomenNyeriTekan.SelectedIndex == 1, Notes = txtAbdomenNyeriTekan.Text },
                AbdomenHernia = new AbNormalAndNotes { IsAbNormal = optAbdomenHernia.SelectedIndex == 1, Notes = txtAbdomenHernia.Text },
                AbdomenBisingUsus = new AbNormalAndNotes { IsAbNormal = optAbdomenBisingUsus.SelectedIndex == 1, Notes = txtAbdomenBisingUsus.Text },
                AbdomenDistensi = new AbNormalAndNotes { IsAbNormal = optAbdomenDistensi.SelectedIndex == 1, Notes = txtAbdomenDistensi.Text },
                AbdomenPem = new PhysicalExamMetod { Inspeksi = txtAbdomenInspeksi.Text, Palpasi = txtAbdomenPalpasi.Text, Perkusi = txtAbdomenPerkusi.Text, Auskultasi = txtAbdomenAuskulatasi.Text },

                SpineLimb = new AbNormalAndNotes { IsAbNormal = optSpineLimb.SelectedIndex == 1, Notes = txtSpineLimb.Text },

                GenitaliaAndAnus = new AbNormalAndNotes
                {
                    IsAbNormal = optGenitaliaAndAnus.SelectedIndex == 1,
                    Notes = txtGenitaliaAndAnus.Text
                },
                GenitaliaPenis = new AbNormalAndNotes { IsAbNormal = optGenitaliaPenis.SelectedIndex == 1, Notes = txtGenitaliaPenis.Text },
                GenitaliaTestis = new AbNormalAndNotes { IsAbNormal = optGenitaliaTestis.SelectedIndex == 1, Notes = txtGenitaliaTestis.Text },
                GenitaliaLabiaMinor = new AbNormalAndNotes { IsAbNormal = optGenitaliaLabiaMinor.SelectedIndex == 1, Notes = txtGenitaliaLabiaMinor.Text },
                GenitaliaAnus = new AbNormalAndNotes { IsAbNormal = optGenitaliaAnus.SelectedIndex == 1, Notes = txtGenitaliaAnus.Text },

                Ekstremitas = new AbNormalAndNotes
                {
                    IsAbNormal = optEkstremitas.SelectedIndex == 1,
                    Notes = txtEkstremitas.Text
                },
                EkstremitasEdema = new AbNormalAndNotes { IsAbNormal = optEkstremitasEdema.SelectedIndex == 1, Notes = txtEkstremitasEdema.Text },
                EkstremitasCrt = new AbNormalAndNotes { IsAbNormal = optEkstremitasCrt.SelectedIndex == 1, Notes = txtEkstremitasCrt.Text },

                Kulit = new AbNormalAndNotes { IsAbNormal = optKulit.SelectedIndex == 1, Notes = txtKulit.Text },
                StatusNeurogis = new StatusNeurogis { Grm = optGrm.SelectedValue == "Y", Fisiologis = new AbNormalAndNotes() { IsAbNormal = optReflexFisiologis.SelectedValue == "A", Notes = txtReflexFisiologis.Text }, Patologis = optReflexPatologis.SelectedValue == "Y", Motorik = new AbNormalAndNotes() { IsAbNormal = optMotoric.SelectedValue == "A", Notes = txtMotoric.Text } },

                Other = txtOther.Text,
                Notes = txtPhysicalExamNotes.Text,
                NutritionSkrinning = optNutritionSkrinning.SelectedValue
            };

            assessment.PhysicalExam = JsonConvert.SerializeObject(pExam);

            // Objective
            rim.Info2 = GenerateSoapObjective(pExam);
        }

        private string GenerateSoapObjective(KidPe pe)
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
            if (isIncludeNormal || pe.Jantung.IsAbNormal)
            {
                strBuilder.AppendFormat("Jantung: {1}: {0}", pe.Jantung.Notes, pe.Jantung.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.JantungBunyi.IsAbNormal)
            {
                strBuilder.AppendFormat("JantungBunyi: {1}: {0}", pe.JantungBunyi.Notes, pe.JantungBunyi.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Paru.IsAbNormal)
            {
                strBuilder.AppendFormat("Paru: {1}: {0}", pe.Paru.Notes, pe.Paru.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.ParuPergerakan.IsAbNormal)
            {
                strBuilder.AppendFormat("ParuPergerakan: {1}: {0}", pe.ParuPergerakan.Notes, pe.ParuPergerakan.IsAbNormal ? "Asymmetry" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.ParuPerkusi.IsAbNormal)
            {
                strBuilder.AppendFormat("ParuPerkusi: {1}: {0}", pe.ParuPerkusi.Notes, pe.ParuPerkusi.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.ParuPernapasan.IsAbNormal)
            {
                strBuilder.AppendFormat("ParuPernapasan: {1}: {0}", pe.ParuPernapasan.Notes, pe.ParuPernapasan.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Abdomen.IsAbNormal)
            {
                strBuilder.AppendFormat("Abdomen: {1}: {0}", pe.Abdomen.Notes, pe.Abdomen.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.AbdomenKelainan.IsAbNormal)
            {
                strBuilder.AppendFormat("Abnormalities: {1}: {0}", pe.AbdomenKelainan.Notes, pe.AbdomenKelainan.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            
            if (isIncludeNormal || pe.AbdomenBisingUsus.IsAbNormal)
            {
                strBuilder.AppendFormat("Bowel Sound: {1}: {0}", pe.AbdomenBisingUsus.Notes, pe.AbdomenBisingUsus.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.SpineLimb.IsAbNormal)
            {
                strBuilder.AppendFormat("Spine & Limb: {1}: {0}", pe.SpineLimb.Notes, pe.SpineLimb.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.GenitaliaAndAnus.IsAbNormal)
            {
                strBuilder.AppendFormat("Genitalia & Anus: {1}: {0}", pe.GenitaliaAndAnus.Notes, pe.GenitaliaAndAnus.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.GenitaliaPenis.IsAbNormal)
            {
                strBuilder.AppendFormat("Penis: {1}: {0}", pe.GenitaliaPenis.Notes, pe.GenitaliaPenis.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.GenitaliaTestis.IsAbNormal)
            {
                strBuilder.AppendFormat("Testis: {1}: {0}", pe.GenitaliaTestis.Notes, pe.GenitaliaTestis.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.GenitaliaLabiaMinor.IsAbNormal)
            {
                strBuilder.AppendFormat("Labia Minor: {1}: {0}", pe.GenitaliaLabiaMinor.Notes, pe.GenitaliaLabiaMinor.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.GenitaliaAnus.IsAbNormal)
            {
                strBuilder.AppendFormat("Anus: {1}: {0}", pe.GenitaliaAnus.Notes, pe.GenitaliaAnus.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Ekstremitas.IsAbNormal)
            {
                strBuilder.AppendFormat("Ekstremitas: {1}: {0}", pe.Ekstremitas.Notes, pe.Ekstremitas.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.EkstremitasCrt.IsAbNormal)
            {
                strBuilder.AppendFormat("CRT: {1}: {0}", pe.EkstremitasCrt.Notes, pe.EkstremitasCrt.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Kulit.IsAbNormal)
            {
                strBuilder.AppendFormat("Kulit: {1}: {0}", pe.Kulit.Notes, pe.Kulit.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.StatusNeurogis.Fisiologis.IsAbNormal)
            {
                strBuilder.AppendFormat("NeurogisFisiologis: {1}: {0}", pe.StatusNeurogis.Fisiologis.Notes, pe.StatusNeurogis.Fisiologis.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.StatusNeurogis.Motorik.IsAbNormal)
            {
                strBuilder.AppendFormat("NeurogisMotorik: {1}: {0}", pe.StatusNeurogis.Motorik.Notes, pe.StatusNeurogis.Motorik.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.NutritionSkrinning))
            {
                strBuilder.AppendFormat("Skrinning Gizi: {0}", pe.NutritionSkrinning);
                strBuilder.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(pe.Notes))
            {
                strBuilder.AppendFormat("{0}", pe.Notes);
                strBuilder.AppendLine(string.Empty);
            }

            return strBuilder.ToString();
        }


        #endregion


    }
}