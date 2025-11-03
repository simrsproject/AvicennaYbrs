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
    public partial class LungPeCtl : BaseAssessmentCtl
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

            txtKepala.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optKepala.ClientID + "'); }";
            txtMata.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optMata.ClientID + "'); }";
            txtTht.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optTht.ClientID + "'); }";
            txtMulut.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optMulut.ClientID + "'); }";
            txtLeher.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optLeher.ClientID + "'); }";
            txtThorax.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optThorax.ClientID + "'); }";
            txtInspeksiGerakan.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optInspeksiGerakan.ClientID + "'); }";
            txtInspeksiSelaIga.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optInspeksiSelaIga.ClientID + "'); }";
            txtPalpasiFremitus.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optPalpasiFremitus.ClientID + "'); }";
            txtPalpasiNyeri.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optPalpasiNyeri.ClientID + "'); }";
            txtPalpasiKrepitasi.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optPalpasiKrepitasi.ClientID + "'); }";
            txtBising.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optBising.ClientID + "'); }";
            txtGenitaliaAndAnus.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optGenitaliaAndAnus.ClientID + "'); }";
            txtEkstremitas.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optEkstremitas.ClientID + "'); }";
            txtKulit.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optKulit.ClientID + "'); }";
        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            var ent = new LungPe();

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    ent = JsonConvert.DeserializeObject<LungPe>(asses.PhysicalExam);
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


            //gcsCtl.Condition = ent.Condition;
            //gcsCtl.Eye = ent.Consciousness.Eye.Code;
            //gcsCtl.Motor = ent.Consciousness.Motor.Code;
            //gcsCtl.Verbal = ent.Consciousness.Verbal.Code;
            //gcsCtl.ConsciousnessNote = ent.Consciousness.ConsciousnessNote;
            //gcsCtl.Consciousness = string.Format("{0} [{1}]", ent.Consciousness.ConsciousnessDescription, ent.Consciousness.ConsciousnessValue);

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

            #region Item Paru
            // Inspeksi
            optInspeksiGerakan.SelectedIndex = ent.Paru.Inspeksi.Respiratory.IsAbNormal ? 1 : 0;
            txtInspeksiGerakan.Text = ent.Paru.Inspeksi.Respiratory.Notes;

            optInspeksiSelaIga.SelectedIndex = ent.Paru.Inspeksi.SelaIga.IsAbNormal ? 1 : 0;
            txtInspeksiSelaIga.Text = ent.Paru.Inspeksi.SelaIga.Notes;

            // Palpasi
            optPalpasiFremitus.SelectedIndex = ent.Paru.Palpasi.Fremitus.IsAbNormal ? 1 : 0;
            txtPalpasiFremitus.Text = ent.Paru.Palpasi.Fremitus.Notes;

            optPalpasiNyeri.SelectedIndex = ent.Paru.Palpasi.NyeriTekan.IsExist ? 1 : 0;
            txtPalpasiNyeri.Text = ent.Paru.Palpasi.NyeriTekan.Notes;

            optPalpasiKrepitasi.SelectedIndex = ent.Paru.Palpasi.Krepitasi.IsExist ? 1 : 0;
            txtPalpasiKrepitasi.Text = ent.Paru.Palpasi.Krepitasi.Notes;

            // Perkuisi
            optPerkusi.SelectedValue = ent.Paru.Perkusi.Condition;
            txtPerkusiLocation.Text = ent.Paru.Perkusi.Location;

            // Auskultasi
            // Vesikular
            optVesikular.SelectedValue = ent.Paru.Auskultasi.Vesikular.Condition;
            txtVesikularLocation.Text = ent.Paru.Auskultasi.Vesikular.Location;

            // Ronchi
            optRonchi.SelectedValue = ent.Paru.Auskultasi.Ronchi.Condition;
            txtRonchiLocation.Text = ent.Paru.Auskultasi.Ronchi.Location;
            optSedangKasar.SelectedValue = ent.Paru.Auskultasi.RonchiLevel;
            optBising.SelectedIndex = ent.Paru.Auskultasi.Bising.IsExist ? 1 : 0;
            txtBising.Text = ent.Paru.Auskultasi.Ronchi.Location;

            #endregion

            txtAuscultationWheezing.Text = ent.AuscultationWheezing;
            txtJantung.Text = ent.Jantung;
            txtAbdomen.Text = ent.Abdomen.Notes;

            optGenitaliaAndAnus.SelectedIndex = ent.GenitaliaAndAnus.IsAbNormal ? 1 : 0;
            txtGenitaliaAndAnus.Text = ent.GenitaliaAndAnus.Notes;

            optEkstremitas.SelectedIndex = ent.Ekstremitas.IsAbNormal ? 1 : 0;
            txtEkstremitas.Text = ent.Ekstremitas.Notes;

            optKulit.SelectedIndex = ent.Kulit.IsAbNormal ? 1 : 0;
            txtKulit.Text = ent.Kulit.Notes;
            txtPhysicalExamNotes.Text = ent.Notes;
            optNutritionSkrinning.SelectedValue = ent.NutritionSkrinning;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var pExam = new LungPe();
            //{
            //    Condition = gcsCtl.Condition,
            //    Consciousness = new Gcs { Eye = new GcsItem(), Motor = new GcsItem(), Verbal = new GcsItem() }
            //};

            pExam.Consciousness.Eye.SetValue(gcsCtl.Eye);
            pExam.Consciousness.Motor.SetValue(gcsCtl.Motor);
            pExam.Consciousness.Verbal.SetValue(gcsCtl.Verbal);

            pExam.Kepala = new AbNormalAndNotes { IsAbNormal = optKepala.SelectedIndex == 1, Notes = txtKepala.Text };
            pExam.Mata = new AbNormalAndNotes { IsAbNormal = optMata.SelectedIndex == 1, Notes = txtMata.Text };
            pExam.Tht = new AbNormalAndNotes { IsAbNormal = optTht.SelectedIndex == 1, Notes = txtTht.Text };
            pExam.Mulut = new AbNormalAndNotes { IsAbNormal = optMulut.SelectedIndex == 1, Notes = txtMulut.Text };
            pExam.Leher = new AbNormalAndNotes { IsAbNormal = optLeher.SelectedIndex == 1, Notes = txtLeher.Text };
            pExam.Thorax = new AbNormalAndNotes { IsAbNormal = optThorax.SelectedIndex == 1, Notes = txtThorax.Text };

            #region Item Paru
            // Inspeksi
            pExam.Paru.Inspeksi.Respiratory.IsAbNormal = optInspeksiGerakan.SelectedIndex == 1;
            pExam.Paru.Inspeksi.Respiratory.Notes = txtInspeksiGerakan.Text;

            pExam.Paru.Inspeksi.SelaIga.IsAbNormal = optInspeksiSelaIga.SelectedIndex == 1;
            pExam.Paru.Inspeksi.SelaIga.Notes = txtInspeksiSelaIga.Text;

            // Palpasi
            pExam.Paru.Palpasi.Fremitus.IsAbNormal = optPalpasiFremitus.SelectedIndex == 1;
            pExam.Paru.Palpasi.Fremitus.Notes = txtPalpasiFremitus.Text;

            pExam.Paru.Palpasi.NyeriTekan.IsExist = optPalpasiNyeri.SelectedIndex == 1;
            pExam.Paru.Palpasi.NyeriTekan.Notes = txtPalpasiNyeri.Text;

            pExam.Paru.Palpasi.Krepitasi.IsExist = optPalpasiKrepitasi.SelectedIndex == 1;
            pExam.Paru.Palpasi.Krepitasi.Notes = txtPalpasiKrepitasi.Text;

            // Perkuisi
            pExam.Paru.Perkusi.Condition = optPerkusi.SelectedValue;
            pExam.Paru.Perkusi.Location = txtPerkusiLocation.Text;

            // Auskultasi
            // Vesikular
            pExam.Paru.Auskultasi.Vesikular.Condition = optVesikular.SelectedValue;
            pExam.Paru.Auskultasi.Vesikular.Location = txtVesikularLocation.Text;

            // Ronchi
            pExam.Paru.Auskultasi.Ronchi.Condition = optRonchi.SelectedValue;
            pExam.Paru.Auskultasi.Ronchi.Location = txtRonchiLocation.Text;
            pExam.Paru.Auskultasi.RonchiLevel = optSedangKasar.SelectedValue;
            pExam.Paru.Auskultasi.Bising.IsExist = optBising.SelectedIndex == 1;
            pExam.Paru.Auskultasi.Ronchi.Location = txtBising.Text;

            #endregion

            pExam.AuscultationWheezing = txtAuscultationWheezing.Text;
            pExam.Jantung = txtJantung.Text;
            pExam.Abdomen.Notes = txtAbdomen.Text;

            pExam.GenitaliaAndAnus = new AbNormalAndNotes
            {
                IsAbNormal = optGenitaliaAndAnus.SelectedIndex == 1,
                Notes = txtGenitaliaAndAnus.Text
            };
            pExam.Ekstremitas = new AbNormalAndNotes
            {
                IsAbNormal = optEkstremitas.SelectedIndex == 1,
                Notes = txtEkstremitas.Text
            };
            pExam.Kulit = new AbNormalAndNotes { IsAbNormal = optKulit.SelectedIndex == 1, Notes = txtKulit.Text };
            pExam.Notes = txtPhysicalExamNotes.Text;
            pExam.NutritionSkrinning = optNutritionSkrinning.SelectedValue;
            pExam.Condition = gcsCtl.Condition;
            pExam.Consciousness = gcsCtl.Gcs;

            //assessment.ReviewOfSystem = JsonConvert.SerializeObject(pExam); // Waduh gawat
            assessment.PhysicalExam = JsonConvert.SerializeObject(pExam);

            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(pExam));
            else
                rim.Info2 = GenerateSoapObjective(pExam);
        }

        private string GenerateSoapObjective(LungPe pe)
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
            //strBuilder.AppendFormat("Pain Scale: {0}", pe.Consciousness.PainScale);
            //strBuilder.AppendLine(string.Empty);
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
                strBuilder.AppendFormat("Leher: {1}:{0}", pe.Leher.Notes, pe.Leher.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || pe.Thorax.IsAbNormal)
            {
                strBuilder.AppendFormat("Thorax: {1}: {0}", pe.Thorax.Notes, pe.Thorax.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            // Movement On Breathing

            // Inspeksi
            if (isIncludeNormal || pe.Paru.Inspeksi.Respiratory.IsAbNormal)
            {
                strBuilder.AppendFormat("Movement On Breathing: {1}: {0}", pe.Paru.Inspeksi.Respiratory.Notes, pe.Paru.Inspeksi.Respiratory.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Paru.Inspeksi.SelaIga.IsAbNormal)
            {
                strBuilder.AppendFormat("Between Ribs: {1}: {0}", pe.Paru.Inspeksi.SelaIga.Notes, pe.Paru.Inspeksi.SelaIga.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }


            // Palpasi
            if (isIncludeNormal || pe.Paru.Palpasi.Fremitus.IsAbNormal)
            {
                strBuilder.AppendFormat("Fremitus Vote: {1}:{0}", pe.Paru.Palpasi.Fremitus.Notes, pe.Paru.Palpasi.Fremitus.IsAbNormal ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }
            if (pe.Paru.Palpasi.NyeriTekan.IsExist)
            {
                strBuilder.AppendFormat("Tenderness: Exist:{0}", pe.Paru.Palpasi.NyeriTekan.Notes);
                strBuilder.AppendLine(string.Empty);
            }

            if (pe.Paru.Palpasi.Krepitasi.IsExist)
            {
                strBuilder.AppendFormat("Crepitations: Exist:{0}", pe.Paru.Palpasi.Krepitasi.Notes);
                strBuilder.AppendLine(string.Empty);
            }


            // Perkuisi
            if (!string.IsNullOrWhiteSpace(pe.Paru.Perkusi.Condition))
            {
                strBuilder.AppendFormat("Percussion: {0}: {1}", pe.Paru.Perkusi.Condition, pe.Paru.Perkusi.Location);
                strBuilder.AppendLine(string.Empty);
            }

            // Auskultasi
            // Vesikular
            if (!string.IsNullOrWhiteSpace(pe.Paru.Auskultasi.Vesikular.Condition))
            {
                strBuilder.AppendFormat("Auscultation Vesicular: {0}: {1}", pe.Paru.Auskultasi.Vesikular.Condition, pe.Paru.Auskultasi.Vesikular.Location);
                strBuilder.AppendLine(string.Empty);
            }

            // Ronchi
            if (!string.IsNullOrWhiteSpace(pe.Paru.Auskultasi.Ronchi.Condition))
            {
                strBuilder.AppendFormat("Auscultation Ronchi: {0}: {1}, Loc: {2} ", pe.Paru.Auskultasi.Ronchi.Condition, pe.Paru.Auskultasi.RonchiLevel, pe.Paru.Auskultasi.Ronchi.Location);
                strBuilder.AppendLine(string.Empty);
            }

            if (pe.Paru.Auskultasi.Bising.IsExist)
            {
                strBuilder.AppendFormat("Swipe noisy pleura: Exist: {0}", pe.Paru.Auskultasi.Ronchi.Location);
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