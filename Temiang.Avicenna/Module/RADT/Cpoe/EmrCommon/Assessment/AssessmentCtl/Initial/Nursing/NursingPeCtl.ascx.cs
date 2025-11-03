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
    public partial class NursingPeCtl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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
        }



        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            NursingPe nursingPe;

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    nursingPe = JsonConvert.DeserializeObject<NursingPe>(asses.PhysicalExam);
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

            if (nursingPe != null)
            {
                gcsCtl.Condition = nursingPe.Condition;
                gcsCtl.Gcs = nursingPe.Consciousness;

                optKepala.SelectedIndex = nursingPe.Kepala.IsAbNormal ? 1 : 0;
                txtKepala.Text = nursingPe.Kepala.Notes;

                optMata.SelectedIndex = nursingPe.Mata.IsAbNormal ? 1 : 0;
                txtMata.Text = nursingPe.Mata.Notes;

                optTht.SelectedIndex = nursingPe.Tht.IsAbNormal ? 1 : 0;
                txtTht.Text = nursingPe.Tht.Notes;

                optMulut.SelectedIndex = nursingPe.Mulut.IsAbNormal ? 1 : 0;
                txtMulut.Text = nursingPe.Mulut.Notes;

                optLeher.SelectedIndex = nursingPe.Leher.IsAbNormal ? 1 : 0;
                txtLeher.Text = nursingPe.Leher.Notes;

                optThorax.SelectedIndex = nursingPe.Thorax.IsAbNormal ? 1 : 0;
                txtThorax.Text = nursingPe.Thorax.Notes;

                optJantung.SelectedIndex = nursingPe.Jantung.IsAbNormal ? 1 : 0;
                txtJantung.Text = nursingPe.Jantung.Notes;

                optParu.SelectedIndex = nursingPe.Paru.IsAbNormal ? 1 : 0;
                txtParu.Text = nursingPe.Paru.Notes;

                optAbdomen.SelectedIndex = nursingPe.Abdomen.IsAbNormal ? 1 : 0;
                txtAbdomen.Text = nursingPe.Abdomen.Notes;

                optGenitaliaAndAnus.SelectedIndex = nursingPe.GenitaliaAndAnus.IsAbNormal ? 1 : 0;
                txtGenitaliaAndAnus.Text = nursingPe.GenitaliaAndAnus.Notes;

                optEkstremitas.SelectedIndex = nursingPe.Ekstremitas.IsAbNormal ? 1 : 0;
                txtEkstremitas.Text = nursingPe.Ekstremitas.Notes;

                optKulit.SelectedIndex = nursingPe.Kulit.IsAbNormal ? 1 : 0;
                txtKulit.Text = nursingPe.Kulit.Notes;

                txtInspekulo.Text = nursingPe.Inspekulo;
                txtPhysicalExamNotes.Text = nursingPe.Notes;


            }
            txtOtherExam.Text = asses.OtherExam;
            optNutritionSkrinning.SelectedValue = nursingPe.NutritionSkrinning;
        }


        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            var ent = new NursingPe
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
                GenitaliaAndAnus = new AbNormalAndNotes { IsAbNormal = optGenitaliaAndAnus.SelectedIndex == 1, Notes = txtGenitaliaAndAnus.Text },
                Ekstremitas = new AbNormalAndNotes { IsAbNormal = optEkstremitas.SelectedIndex == 1, Notes = txtEkstremitas.Text },
                Kulit = new AbNormalAndNotes { IsAbNormal = optKulit.SelectedIndex == 1, Notes = txtKulit.Text },
                Inspekulo = txtInspekulo.Text,
                Notes = txtPhysicalExamNotes.Text,
                NutritionSkrinning = optNutritionSkrinning.SelectedValue
            };


            assessment.PhysicalExam = JsonConvert.SerializeObject(ent);
            assessment.OtherExam = txtOtherExam.Text;

            // Objective
            rim.Info2 = GenerateSoapObjective(ent);
        }

        private string GenerateSoapObjective(NursingPe pe)
        {
            var strBuilder = new StringBuilder();

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
            if (!string.IsNullOrEmpty(pe.Notes))
            {
                strBuilder.AppendFormat("Notes : {0}", pe.Notes);
                strBuilder.AppendLine(string.Empty);
            }

            // OtherExam untuk SOAP O akan ditambahkan di menu Save page utama, kalau ditambah disini jadinya akan double (Handono 230323)
            //if (!string.IsNullOrEmpty(txtOtherExam.Text))
            //{
            //    strBuilder.AppendLine(string.Empty);
            //    strBuilder.AppendFormat("Pemeriksaan Penunjang : {0}", txtOtherExam.Text);
            //    strBuilder.AppendLine(string.Empty);
            //}
            return strBuilder.ToString();
        }

        #endregion
    }
}