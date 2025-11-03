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
    public partial class HeartPeCtl : BaseAssessmentCtl
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
            txtGenitaliaAndAnus.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optGenitaliaAndAnus.ClientID + "'); }";
            txtEkstremitas.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optEkstremitas.ClientID + "'); }";
            txtKulit.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optKulit.ClientID + "'); }";
        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            HeartPe pExam;

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    pExam = JsonConvert.DeserializeObject<HeartPe>(asses.PhysicalExam);
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
            //gcsCtl.ConsciousnessNote = pExam.Consciousness.ConsciousnessNote;
            //gcsCtl.Consciousness = string.Format("{0} [{1}]", pExam.Consciousness.ConsciousnessDescription, pExam.Consciousness.ConsciousnessValue);
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

            txtJantung.Text = pExam.Jantung;
            txtInspeksi.Text = pExam.Inspeksi;
            txtPalpasi.Text = pExam.Palpasi;
            optLifting.SelectedValue = pExam.Lifting;
            optThrill.SelectedValue = pExam.Thrill;
            txtPerkusiLeft.Text = pExam.PerkusiLeft;
            txtPerkusiRight.Text = pExam.PerkusiRight;
            txtAuscultationS1S2.Text = pExam.AuscultationS1S2;
            txtMurmur.Text = pExam.Murmur;

            txtParu.Text = pExam.Paru;
            txtParuInspeksi.Text = pExam.ParuInspeksi;
            txtParuPalpasi.Text = pExam.ParuPalpasi;
            txtParuPerkusi.Text = pExam.ParuPerkusi;
            txtParuAusk.Text = pExam.ParuAusk;

            txtAbdomen.Text = pExam.Abdomen;
            txtAbdoInspeksi.Text = pExam.AbdoInspeksi;
            txtAbdoPalpasi.Text = pExam.AbdoPalpasi;
            txtAbdoPerkusi.Text = pExam.AbdoPerkusi;

            txtTataLaksana.Text = pExam.TataLaksana;
            txtLamaRawat.Text = pExam.LamaRawat;
            txtPrognosis.Text = pExam.Prognosis;

            optGenitaliaAndAnus.SelectedIndex = pExam.GenitaliaAndAnus.IsAbNormal ? 1 : 0;
            txtGenitaliaAndAnus.Text = pExam.GenitaliaAndAnus.Notes;

            optEkstremitas.SelectedIndex = pExam.Ekstremitas.IsAbNormal ? 1 : 0;
            txtEkstremitas.Text = pExam.Ekstremitas.Notes;

            optKulit.SelectedIndex = pExam.Kulit.IsAbNormal ? 1 : 0;
            txtKulit.Text = pExam.Kulit.Notes;
            txtPhysicalExamNotes.Text = pExam.Notes;
            optNutritionSkrinning.SelectedValue = pExam.NutritionSkrinning;
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var pExam = new HeartPe
            {
                Condition = gcsCtl.Condition,
                Consciousness = new Gcs { Eye = new GcsItem(), Motor = new GcsItem(), Verbal = new GcsItem(), PainScale = "0" }
            };

            pExam.Consciousness.Eye.SetValue(gcsCtl.Eye);
            pExam.Consciousness.Motor.SetValue(gcsCtl.Motor);
            pExam.Consciousness.Verbal.SetValue(gcsCtl.Verbal);
            pExam.Consciousness.PainScale = gcsCtl.PainScale;

            pExam.Kepala = new AbNormalAndNotes { IsAbNormal = optKepala.SelectedIndex == 1, Notes = txtKepala.Text };
            pExam.Mata = new AbNormalAndNotes { IsAbNormal = optMata.SelectedIndex == 1, Notes = txtMata.Text };
            pExam.Tht = new AbNormalAndNotes { IsAbNormal = optTht.SelectedIndex == 1, Notes = txtTht.Text };
            pExam.Mulut = new AbNormalAndNotes { IsAbNormal = optMulut.SelectedIndex == 1, Notes = txtMulut.Text };
            pExam.Leher = new AbNormalAndNotes { IsAbNormal = optLeher.SelectedIndex == 1, Notes = txtLeher.Text };
            pExam.Thorax = new AbNormalAndNotes { IsAbNormal = optThorax.SelectedIndex == 1, Notes = txtThorax.Text };

            pExam.Jantung = txtJantung.Text;
            pExam.Inspeksi = txtInspeksi.Text;
            pExam.Palpasi = txtPalpasi.Text;
            pExam.Lifting = optLifting.SelectedValue;
            pExam.Thrill = optThrill.SelectedValue;
            pExam.PerkusiLeft = txtPerkusiLeft.Text;
            pExam.PerkusiRight = txtPerkusiRight.Text;
            pExam.AuscultationS1S2 = txtAuscultationS1S2.Text;
            pExam.Murmur = txtMurmur.Text;

            pExam.Paru = txtParu.Text;
            pExam.ParuInspeksi = txtParuInspeksi.Text;
            pExam.ParuPalpasi = txtParuPalpasi.Text;
            pExam.ParuPerkusi = txtParuPerkusi.Text;
            pExam.ParuAusk = txtParuAusk.Text;

            pExam.Abdomen = txtAbdomen.Text;
            pExam.AbdoInspeksi = txtAbdoInspeksi.Text;
            pExam.AbdoPalpasi = txtAbdoPalpasi.Text;
            pExam.AbdoPerkusi = txtAbdoPerkusi.Text;

            pExam.TataLaksana = txtTataLaksana.Text;
            pExam.LamaRawat = txtLamaRawat.Text;
            pExam.Prognosis = txtPrognosis.Text;


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

            assessment.PhysicalExam = JsonConvert.SerializeObject(pExam);

            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(pExam));
            else
                rim.Info2 = GenerateSoapObjective(pExam);
        }

        private string GenerateSoapObjective(HeartPe pe)
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

            var jantungParts = new List<string>();
            if (!string.IsNullOrWhiteSpace(pe.Inspeksi))
            {
                jantungParts.Add("Inspeksi: " + pe.Inspeksi);
            }
            if (!string.IsNullOrWhiteSpace(pe.Palpasi))
            {
                var palpasiText = "Palpasi: " + pe.Palpasi;

                var additionalParts = new List<string>();
                if (!string.IsNullOrWhiteSpace(pe.Lifting))
                {
                    additionalParts.Add("Dorongan(" + pe.Lifting + ")");
                }
                if (!string.IsNullOrWhiteSpace(pe.Thrill))
                {
                    additionalParts.Add("Getaran(" + pe.Thrill + ")");
                }

                if (additionalParts.Any())
                {
                    palpasiText += ", " + string.Join(", ", additionalParts);
                }

                jantungParts.Add(palpasiText);
            }
            if (!string.IsNullOrWhiteSpace(pe.PerkusiLeft))
            {
                jantungParts.Add("Perkusi Kiri: " + pe.PerkusiLeft);
            }
            if (!string.IsNullOrWhiteSpace(pe.PerkusiRight))
            {
                jantungParts.Add("Perkusi Kanan: " + pe.PerkusiRight);
            }

            if (jantungParts.Any())
            {
                strBuilder.AppendFormat("Jantung: {0}", string.Join("; ", jantungParts));
                strBuilder.AppendLine();
            }

            var paruParts = new List<string>();
            if (!string.IsNullOrWhiteSpace(pe.ParuInspeksi))
            {
                paruParts.Add("Inspeksi: " + pe.ParuInspeksi);
            }
            if (!string.IsNullOrWhiteSpace(pe.ParuPalpasi))
            {
                paruParts.Add("Palpasi: " + pe.ParuPalpasi);
            }
            if (!string.IsNullOrWhiteSpace(pe.ParuPerkusi))
            {
                paruParts.Add("Palpasi: " + pe.ParuPerkusi);
            }
            if (!string.IsNullOrWhiteSpace(pe.ParuAusk))
            {
                paruParts.Add("Palpasi: " + pe.ParuAusk);
            }
            if (paruParts.Any())
            {
                strBuilder.AppendFormat("Paru: {0}", string.Join("; ", paruParts));
                strBuilder.AppendLine();
            }

            var abdomenParts = new List<string>();
            if (!string.IsNullOrWhiteSpace(pe.AbdoInspeksi))
            {
                abdomenParts.Add("Inspeksi: " + pe.AbdoInspeksi);
            }
            if (!string.IsNullOrWhiteSpace(pe.AbdoPalpasi))
            {
                abdomenParts.Add("Palpasi: " + pe.AbdoPalpasi);
            }
            if (!string.IsNullOrWhiteSpace(pe.AbdoPerkusi))
            {
                abdomenParts.Add("Perkusi: " + pe.AbdoPerkusi);
            }
            if (abdomenParts.Any())
            {
                strBuilder.AppendFormat("Abdomen: {0}", string.Join("; ", abdomenParts));
                strBuilder.AppendLine();
            }

            //if (pe.Jantung.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Jantung: Abnormal: {0}", pe.Jantung.Notes);
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (pe.Paru.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Paru: Abnormal: {0}", pe.Paru.Notes);
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (pe.Abdomen.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Abdomen: Abnormal: {0}", pe.Abdomen.Notes);
            //    strBuilder.AppendLine(string.Empty);
            //}
            //if (pe.Auskulatasi.IsAbNormal)
            //{
            //    strBuilder.AppendFormat("Auskulatasi: Abnormal: {0}", pe.Auskulatasi.Notes);
            //    strBuilder.AppendLine(string.Empty);
            //}
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