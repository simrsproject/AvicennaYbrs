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
    public partial class SurgicalPeCtl : BaseAssessmentCtl
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
            txtJantung.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optJantung.ClientID + "'); }";
            txtParu.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optParu.ClientID + "'); }";
            txtAbdomen.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optAbdomen.ClientID + "'); }";
            txtGenitaliaAndAnus.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optGenitaliaAndAnus.ClientID + "'); }";
            txtAnus.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optAnus.ClientID + "'); }";
            txtEkstremitas.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optEkstremitas.ClientID + "'); }";
            txtEkstremitasLower.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optEkstremitasLower.ClientID + "'); }";
            txtKulit.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optKulit.ClientID + "'); }";
        }

        private void DefaultValue()
        {
            optKepala.SelectedIndex = 0;
            optMata.SelectedIndex = 0;
            optTht.SelectedIndex = 0;
            optMulut.SelectedIndex = 0;
            optLeher.SelectedIndex = 0;
            optThorax.SelectedIndex = 0;
            optJantung.SelectedIndex = 0;
            optParu.SelectedIndex = 0;
            optAbdomen.SelectedIndex = 0;
            optGenitaliaAndAnus.SelectedIndex = 0;
            optAnus.SelectedIndex = 0;
            optEkstremitas.SelectedIndex = 0;
            optEkstremitasLower.SelectedIndex = 0;
            optKulit.SelectedIndex = 0;
        }

        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            SurgicalPe pExam;

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    pExam = JsonConvert.DeserializeObject<SurgicalPe>(asses.PhysicalExam);
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

            gcsCtl.Condition = pExam.Condition;
            
            gcsCtl.Gcs = pExam.Consciousness;

            if (pExam.Kepala.IsAbNormal != null)
            {
                optKepala.SelectedIndex = (pExam.Kepala.IsAbNormal??false) ? 1 : 0;
                txtKepala.Text = pExam.Kepala.Notes;
            }

            if (pExam.Mata.IsAbNormal != null)
            {
                optMata.SelectedIndex = (pExam.Mata.IsAbNormal ?? false) ? 1 : 0;
                txtMata.Text = pExam.Mata.Notes;
            }

            if (pExam.Tht.IsAbNormal != null)
            {
                optTht.SelectedIndex = (pExam.Tht.IsAbNormal ?? false) ? 1 : 0;
                txtTht.Text = pExam.Tht.Notes;
            }

            if (pExam.Mulut.IsAbNormal != null)
            {
                optMulut.SelectedIndex = (pExam.Mulut.IsAbNormal ?? false) ? 1 : 0;
                txtMulut.Text = pExam.Mulut.Notes;
            }

            if (pExam.Leher.IsAbNormal != null)
            {
                optLeher.SelectedIndex = (pExam.Leher.IsAbNormal ?? false) ? 1 : 0;
                txtLeher.Text = pExam.Leher.Notes;
            }

            if (pExam.Thorax.IsAbNormal != null)
            {
                optThorax.SelectedIndex = (pExam.Thorax.IsAbNormal ?? false) ? 1 : 0;
                txtThorax.Text = pExam.Thorax.Notes;
            }

            if (pExam.Jantung.IsAbNormal != null)
            {
                optJantung.SelectedIndex = (pExam.Jantung.IsAbNormal ?? false) ? 1 : 0;
                txtJantung.Text = pExam.Jantung.Notes;
            }

            if (pExam.Paru.IsAbNormal != null)
            {
                optParu.SelectedIndex = (pExam.Paru.IsAbNormal ?? false) ? 1 : 0;
                txtParu.Text = pExam.Paru.Notes;
            }

            if (pExam.Abdomen.IsAbNormal != null)
            {
                optAbdomen.SelectedIndex = (pExam.Abdomen.IsAbNormal ?? false) ? 1 : 0;
                txtAbdomen.Text = pExam.Abdomen.Notes;
            }

            if (pExam.GenitaliaAndAnus.IsAbNormal != null)
            {
                optGenitaliaAndAnus.SelectedIndex = (pExam.GenitaliaAndAnus.IsAbNormal ?? false) ? 1 : 0;
                txtGenitaliaAndAnus.Text = pExam.GenitaliaAndAnus.Notes;
            }

            if (pExam.Anus.IsAbNormal != null)
            {
                optAnus.SelectedIndex = (pExam.Anus.IsAbNormal ?? false) ? 1 : 0;
                txtAnus.Text = pExam.Anus.Notes;
            }

            if (pExam.Ekstremitas.IsAbNormal != null)
            {
                optEkstremitas.SelectedIndex = (pExam.Ekstremitas.IsAbNormal ?? false) ? 1 : 0;
                txtEkstremitas.Text = pExam.Ekstremitas.Notes;
            }
            if (pExam.EkstremitasLower.IsAbNormal != null)
            {
                optEkstremitasLower.SelectedIndex = (pExam.EkstremitasLower.IsAbNormal ?? false) ? 1 : 0;
                txtEkstremitasLower.Text = pExam.EkstremitasLower.Notes;
            }

            if (pExam.Kulit.IsAbNormal != null)
            {
                optKulit.SelectedIndex = (pExam.Kulit.IsAbNormal ?? false) ? 1 : 0;
                txtKulit.Text = pExam.Kulit.Notes;
            }

            txtPhysicalExamNotes.Text = pExam.Notes;
            optNutritionSkrinning.SelectedValue = pExam.NutritionSkrinning;
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var pExam = new SurgicalPe
            {
                Condition = gcsCtl.Condition,
                Consciousness = gcsCtl.Gcs,

                Notes = txtPhysicalExamNotes.Text,
                NutritionSkrinning = optNutritionSkrinning.SelectedValue
            };

            if (optKepala.SelectedIndex > -1)
                pExam.Kepala = new AbNormalAndNotes2 { IsAbNormal = optKepala.SelectedIndex == 1, Notes = txtKepala.Text };

            if (optMata.SelectedIndex > -1)
                pExam.Mata = new AbNormalAndNotes2 { IsAbNormal = optMata.SelectedIndex == 1, Notes = txtMata.Text };

            if (optTht.SelectedIndex > -1)
                pExam.Tht = new AbNormalAndNotes2 { IsAbNormal = optTht.SelectedIndex == 1, Notes = txtTht.Text };

            if (optMulut.SelectedIndex > -1)
                pExam.Mulut = new AbNormalAndNotes2 { IsAbNormal = optMulut.SelectedIndex == 1, Notes = txtMulut.Text };

            if (optLeher.SelectedIndex > -1)
                pExam.Leher = new AbNormalAndNotes2 { IsAbNormal = optLeher.SelectedIndex == 1, Notes = txtLeher.Text };

            if (optThorax.SelectedIndex > -1)
                pExam.Thorax = new AbNormalAndNotes2 { IsAbNormal = optThorax.SelectedIndex == 1, Notes = txtThorax.Text };

            if (optJantung.SelectedIndex > -1)
                pExam.Jantung = new AbNormalAndNotes2 { IsAbNormal = optJantung.SelectedIndex == 1, Notes = txtJantung.Text };

            if (optParu.SelectedIndex > -1)
                pExam.Paru = new AbNormalAndNotes2 { IsAbNormal = optParu.SelectedIndex == 1, Notes = txtParu.Text };

            if (optAbdomen.SelectedIndex > -1)
                pExam.Abdomen = new AbNormalAndNotes2 { IsAbNormal = optAbdomen.SelectedIndex == 1, Notes = txtAbdomen.Text };

            if (optGenitaliaAndAnus.SelectedIndex > -1)
                pExam.GenitaliaAndAnus = new AbNormalAndNotes2
                {
                    IsAbNormal = optGenitaliaAndAnus.SelectedIndex == 1,
                    Notes = txtGenitaliaAndAnus.Text
                };

            if (optAnus.SelectedIndex > -1)
                pExam.Anus = new AbNormalAndNotes2
                {
                    IsAbNormal = optAnus.SelectedIndex == 1,
                    Notes = txtAnus.Text
                };

            if (optEkstremitas.SelectedIndex > -1)
                pExam.Ekstremitas = new AbNormalAndNotes2
                {
                    IsAbNormal = optEkstremitas.SelectedIndex == 1,
                    Notes = txtEkstremitas.Text
                };

            if (optEkstremitasLower.SelectedIndex > -1)
                pExam.EkstremitasLower = new AbNormalAndNotes2
                {
                    IsAbNormal = optEkstremitasLower.SelectedIndex == 1,
                    Notes = txtEkstremitasLower.Text
                };

            if (optKulit.SelectedIndex > -1)
                pExam.Kulit = new AbNormalAndNotes2 { IsAbNormal = optKulit.SelectedIndex == 1, Notes = txtKulit.Text };

            assessment.PhysicalExam = JsonConvert.SerializeObject(pExam);

            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(pExam));
            else
                rim.Info2 = GenerateSoapObjective(pExam);
        }

        private string GenerateSoapObjective(SurgicalPe pe)
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

            if (isIncludeNormal || (pe.Kepala.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Kepala: {1}: {0}", pe.Kepala.Notes, (pe.Kepala.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.Mata.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Mata: {1}: {0}", pe.Mata.Notes, (pe.Mata.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.Tht.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Tht: {1}: {0}", pe.Tht.Notes, (pe.Tht.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.Mulut.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Mulut: {1}: {0}", pe.Mulut.Notes, (pe.Mulut.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.Leher.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Leher: {1}: {0}", pe.Leher.Notes, (pe.Leher.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.Thorax.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Thorax: {1}: {0}", pe.Thorax.Notes, (pe.Thorax.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.Jantung.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Jantung: {1}: {0}", pe.Jantung.Notes, (pe.Jantung.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.Paru.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Paru: {1}: {0}", pe.Paru.Notes, (pe.Paru.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.Abdomen.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Abdomen: {1}: {0}", pe.Abdomen.Notes, (pe.Abdomen.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            //if (pe.Auskulatasi.IsAbNormal??false)
            //{
            //    strBuilder.AppendFormat("Auskulatasi: Abnormal: {0}", pe.Auskulatasi.Notes);
            //    strBuilder.AppendLine(string.Empty);
            //}
            if (isIncludeNormal || (pe.GenitaliaAndAnus.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("GenitaliaAndAnus: {1}: {0}", pe.GenitaliaAndAnus.Notes, (pe.GenitaliaAndAnus.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.Anus.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Anus: {1}: {0}", pe.Anus.Notes, (pe.Anus.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.Ekstremitas.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Ekstremitas: {1}: {0}", pe.Ekstremitas.Notes, (pe.Ekstremitas.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.EkstremitasLower.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("EkstremitasLower: {1}: {0}", pe.EkstremitasLower.Notes, (pe.EkstremitasLower.IsAbNormal ?? false) ? "Abnormal" : "Normal");
                strBuilder.AppendLine(string.Empty);
            }

            if (isIncludeNormal || (pe.Kulit.IsAbNormal ?? false))
            {
                strBuilder.AppendFormat("Kulit: {1}: {0}", pe.Kulit.Notes, (pe.Kulit.IsAbNormal ?? false) ? "Abnormal" : "Normal");
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