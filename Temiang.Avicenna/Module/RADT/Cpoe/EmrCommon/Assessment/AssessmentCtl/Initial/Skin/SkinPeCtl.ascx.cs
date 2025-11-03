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
    public partial class SkinPeCtl : BaseAssessmentCtl
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
            txtEkstremitas.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optEkstremitas.ClientID + "'); }";
            txtKulit.ClientEvents.OnBlur = "function (s,a){ autoCheckOnBlur(s,a,'" + optKulit.ClientID + "'); }";
        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            var ent = new SkinPe();

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    ent = JsonConvert.DeserializeObject<SkinPe>(asses.PhysicalExam);
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

            optJantung.SelectedIndex = ent.Jantung.IsAbNormal ? 1 : 0;
            txtJantung.Text = ent.Jantung.Notes;

            optParu.SelectedIndex = ent.Paru.IsAbNormal ? 1 : 0;
            txtParu.Text = ent.Paru.Notes;

            optAbdomen.SelectedIndex = ent.Abdomen.IsAbNormal ? 1 : 0;
            txtAbdomen.Text = ent.Abdomen.Notes;

            optGenitaliaAndAnus.SelectedIndex = ent.GenitaliaAndAnus.IsAbNormal ? 1 : 0;
            txtGenitaliaAndAnus.Text = ent.GenitaliaAndAnus.Notes;

            optEkstremitas.SelectedIndex = ent.Ekstremitas.IsAbNormal ? 1 : 0;
            txtEkstremitas.Text = ent.Ekstremitas.Notes;

            optKulit.SelectedIndex = ent.Kulit.IsAbNormal ? 1 : 0;
            txtKulit.Text = ent.Kulit.Notes;

            txtDermatologikus.Text = ent.Dermatologikus;

            txtPhysicalExamNotes.Text = ent.Notes;
            optNutritionSkrinning.SelectedValue = ent.NutritionSkrinning;
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            //var ent = new SkinPe
            //{
            //    Condition = gcsCtl.Condition,
            //    Consciousness = new Gcs { Eye = new GcsItem(), Motor = new GcsItem(), Verbal = new GcsItem() }
            //};

            //ent.Consciousness.Eye.SetValue(gcsCtl.Eye);
            //ent.Consciousness.Motor.SetValue(gcsCtl.Motor);
            //ent.Consciousness.Verbal.SetValue(gcsCtl.Verbal);

            var ent = new SkinPe
            {
                Condition = gcsCtl.Condition,
                Consciousness = gcsCtl.Gcs
            };
            ent.Kepala = new AbNormalAndNotes { IsAbNormal = optKepala.SelectedIndex == 1, Notes = txtKepala.Text };
            ent.Mata = new AbNormalAndNotes { IsAbNormal = optMata.SelectedIndex == 1, Notes = txtMata.Text };
            ent.Tht = new AbNormalAndNotes { IsAbNormal = optTht.SelectedIndex == 1, Notes = txtTht.Text };
            ent.Mulut = new AbNormalAndNotes { IsAbNormal = optMulut.SelectedIndex == 1, Notes = txtMulut.Text };
            ent.Leher = new AbNormalAndNotes { IsAbNormal = optLeher.SelectedIndex == 1, Notes = txtLeher.Text };
            ent.Thorax = new AbNormalAndNotes { IsAbNormal = optThorax.SelectedIndex == 1, Notes = txtThorax.Text };
            ent.Jantung = new AbNormalAndNotes { IsAbNormal = optJantung.SelectedIndex == 1, Notes = txtJantung.Text };
            ent.Paru = new AbNormalAndNotes { IsAbNormal = optParu.SelectedIndex == 1, Notes = txtParu.Text };
            ent.Abdomen = new AbNormalAndNotes { IsAbNormal = optAbdomen.SelectedIndex == 1, Notes = txtAbdomen.Text };

            ent.GenitaliaAndAnus = new AbNormalAndNotes
            {
                IsAbNormal = optGenitaliaAndAnus.SelectedIndex == 1,
                Notes = txtGenitaliaAndAnus.Text
            };
            ent.Ekstremitas = new AbNormalAndNotes
            {
                IsAbNormal = optEkstremitas.SelectedIndex == 1,
                Notes = txtEkstremitas.Text
            };
            ent.Kulit = new AbNormalAndNotes { IsAbNormal = optKulit.SelectedIndex == 1, Notes = txtKulit.Text };

            ent.Dermatologikus = txtDermatologikus.Text;
            ent.Notes = txtPhysicalExamNotes.Text;
            ent.NutritionSkrinning = optNutritionSkrinning.SelectedValue;

            assessment.PhysicalExam = JsonConvert.SerializeObject(ent);

            // Objective
            rim.Info2 = GenerateSoapObjective(ent);
        }

        private string GenerateSoapObjective(SkinPe pe)
        {
            var strBuilder = new StringBuilder();
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