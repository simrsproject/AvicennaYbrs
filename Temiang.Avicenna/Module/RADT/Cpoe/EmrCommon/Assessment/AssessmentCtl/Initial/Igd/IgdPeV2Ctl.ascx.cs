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
    public partial class IgdPeV2Ctl : BaseAssessmentCtl
    {

        protected override void OnInit(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(ddlEsi, "Triage5Level", true);


            // Seting ReviewSystem Control
            var igd = new Igd();
            questAncillaryExam.QuestionGroupID = igd.AncillaryExam.QuestionGroupID;
            questDisabilitas.QuestionGroupID = igd.Disabilitas.QuestionGroupID;
            questEksposur.QuestionGroupID = igd.Eksposur.QuestionGroupID;
            questPenilaianBayi.QuestionGroupID = igd.PenilaianBayi.QuestionGroupID;
            questPernapasan.QuestionGroupID = igd.Pernapasan.QuestionGroupID;
            questSirkulasi.QuestionGroupID = igd.Sirkulasi.QuestionGroupID;

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var igd = new Igd();

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    igd = JsonConvert.DeserializeObject<Igd>(asses.PhysicalExam);



                    // Triage
                    if(!string.IsNullOrEmpty(igd.Esi.Id))
                    {
                        var esi = igd.Esi;
                        ddlEsi.SelectedValue = esi.Id;

                        PopulateEsiReason(esi.Id);
                        var condition = string.Join(";", esi.ConditionIds);
                        foreach (Telerik.Web.UI.ButtonListItem item in cblEsiCondition.Items)
                        {
                            item.Selected = condition.Contains(item.Value);
                        }
                    }

                    //questAbdomenPelvis.PopulateValue(igd.AbdomenPelvis);
                    questAncillaryExam.PopulateValue(igd.AncillaryExam);
                    questDisabilitas.PopulateValue(igd.Disabilitas);
                    questEksposur.PopulateValue(igd.Eksposur);
                    //questJalanNapas.PopulateValue(igd.JalanNapas);
                    //questKepalaLeher.PopulateValue(igd.KepalaLeher);
                    questPenilaianBayi.PopulateValue(igd.PenilaianBayi);
                    questPernapasan.PopulateValue(igd.Pernapasan);
                    questSirkulasi.PopulateValue(igd.Sirkulasi);
                    //questThorax.PopulateValue(igd.Thorax);
                    //questOth.PopulateValue(igd.Others);

                    gcsCtl.Condition = igd.Condition;
                    gcsCtl.Gcs = igd.Consciousness;


                    optPaten.SelectedIndex = igd.Paten.IsAbNormal ? 1 : 0;
                    txtPaten.Text = igd.Paten.Notes;
                    optObsPartial.SelectedIndex = igd.ObsPartial.IsAbNormal ? 1 : 0;
                    txtObsPartial.Text = igd.ObsPartial.Notes;
                    optObsTotal.SelectedIndex = igd.ObsTotal.IsAbNormal ? 1 : 0;
                    txtObsTotal.Text = igd.ObsTotal.Notes;
                    optTrauma.SelectedIndex = igd.Trauma.IsAbNormal ? 1 : 0;
                    txtTrauma.Text = igd.Trauma.Notes;
                    optResiko.SelectedIndex = igd.Resiko.IsAbNormal ? 1 : 0;
                    txtResiko.Text = igd.Resiko.Notes;
                    optBendaAsing.SelectedIndex = igd.BendaAsing.IsAbNormal ? 1 : 0;
                    txtBendaAsing.Text = igd.BendaAsing.Notes;
                    optKesimpulan.SelectedIndex = igd.Kesimpulan.IsAbNormal ? 1 : 0;
                    txtKesimpulan.Text = igd.Kesimpulan.Notes;
                    optKepala.SelectedIndex = igd.Kepala.IsAbNormal ? 1 : 0;
                    txtKepala.Text = igd.Kepala.Notes;
                    optKonjungtiva.SelectedIndex = igd.Konjungtiva.IsAbNormal ? 1 : 0;
                    txtKonjungtiva.Text = igd.Konjungtiva.Notes;
                    optSklera.SelectedIndex = igd.Sklera.IsAbNormal ? 1 : 0;
                    txtSklera.Text = igd.Sklera.Notes;
                    optBibirLidah.SelectedIndex = igd.BibirLidah.IsAbNormal ? 1 : 0;
                    txtBibirLidah.Text = igd.BibirLidah.Notes;
                    optMukosa.SelectedIndex = igd.Mukosa.IsAbNormal ? 1 : 0;
                    txtMukosa.Text = igd.Mukosa.Notes;
                    optMata.SelectedIndex = igd.Mata.IsAbNormal ? 1 : 0;
                    txtMata.Text = igd.Mata.Notes;
                    optKondisiKepala.SelectedIndex = igd.KondisiKepala.IsAbNormal ? 1 : 0;
                    txtKondisiKepala.Text = igd.KondisiKepala.Notes;
                    optLeher.SelectedIndex = igd.Leher.IsAbNormal ? 1 : 0;
                    txtLeher.Text = igd.Leher.Notes;
                    optTrakea.SelectedIndex = igd.Trakea.IsAbNormal ? 1 : 0;
                    txtTrakea.Text = igd.Trakea.Notes;
                    optJvp.SelectedIndex = igd.Jvp.IsAbNormal ? 1 : 0;
                    txtJvp.Text = igd.Jvp.Notes;
                    optLNN.SelectedIndex = igd.LNN.IsAbNormal ? 1 : 0;
                    txtLNN.Text = igd.LNN.Notes;
                    optTiroid.SelectedIndex = igd.Tiroid.IsAbNormal ? 1 : 0;
                    txtTiroid.Text = igd.Tiroid.Notes;
                    optKondisiLeher.SelectedIndex = igd.KondisiLeher.IsAbNormal ? 1 : 0;
                    txtKondisiLeher.Text = igd.KondisiLeher.Notes;
                    optThorax.SelectedIndex = igd.Thorax2.IsAbNormal ? 1 : 0;
                    txtThorax.Text = igd.Thorax2.Notes;
                    optJantung.SelectedIndex = igd.Jantung.IsAbNormal ? 1 : 0;
                    txtJantung.Text = igd.Jantung.Notes;
                    optParu.SelectedIndex = igd.Paru.IsAbNormal ? 1 : 0;
                    txtParu.Text = igd.Paru.Notes;
                    optAbdomen.SelectedIndex = igd.Abdomen2.IsAbNormal ? 1 : 0;
                    txtAbdomen.Text = igd.Abdomen2.Notes;
                    optPunggung.SelectedIndex = igd.Punggung.IsAbNormal ? 1 : 0;
                    txtPunggung.Text = igd.Punggung.Notes;
                    optEkstremitas.SelectedIndex = igd.Ekstremitas.IsAbNormal ? 1 : 0;
                    txtEkstremitas.Text = igd.Ekstremitas.Notes;
                    optGenitalia.SelectedIndex = igd.Genitalia.IsAbNormal ? 1 : 0;
                    txtGenitalia.Text = igd.Genitalia.Notes;
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
        }
        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var igd = new Igd();
            //igd.AbdomenPelvis = questAbdomenPelvis.GetQuestionAnswerValue();
            igd.AncillaryExam = questAncillaryExam.GetQuestionAnswerValue();
            igd.Disabilitas = questDisabilitas.GetQuestionAnswerValue();
            igd.Eksposur = questEksposur.GetQuestionAnswerValue();
            //igd.JalanNapas = questJalanNapas.GetQuestionAnswerValue();
            //igd.KepalaLeher = questKepalaLeher.GetQuestionAnswerValue();
            igd.PenilaianBayi = questPenilaianBayi.GetQuestionAnswerValue();
            igd.Pernapasan = questPernapasan.GetQuestionAnswerValue();
            igd.Sirkulasi = questSirkulasi.GetQuestionAnswerValue();
            //igd.Thorax = questThorax.GetQuestionAnswerValue();
            //igd.Others = questOth.GetQuestionAnswerValue();

            // Triage
            var esi = new Esi();
            esi.Id = ddlEsi.SelectedValue;
            esi.ConditionIds = cblEsiCondition.SelectedValues;
            igd.Esi = esi;

            igd.Paten = new AbNormalAndNotes { IsAbNormal = optPaten.SelectedIndex == 1, Notes = txtPaten.Text };
            igd.ObsPartial = new AbNormalAndNotes { IsAbNormal = optObsPartial.SelectedIndex == 1, Notes = txtObsPartial.Text };
            igd.ObsTotal = new AbNormalAndNotes { IsAbNormal = optObsTotal.SelectedIndex == 1, Notes = txtObsTotal.Text };
            igd.Trauma = new AbNormalAndNotes { IsAbNormal = optTrauma.SelectedIndex == 1, Notes = txtTrauma.Text };
            igd.Resiko = new AbNormalAndNotes { IsAbNormal = optResiko.SelectedIndex == 1, Notes = txtResiko.Text };
            igd.BendaAsing = new AbNormalAndNotes { IsAbNormal = optBendaAsing.SelectedIndex == 1, Notes = txtBendaAsing.Text };
            igd.Kesimpulan = new AbNormalAndNotes { IsAbNormal = optKesimpulan.SelectedIndex == 1, Notes = txtKesimpulan.Text };
            igd.Kepala = new AbNormalAndNotes { IsAbNormal = optKepala.SelectedIndex == 1, Notes = txtKepala.Text };
            igd.Konjungtiva = new AbNormalAndNotes { IsAbNormal = optKonjungtiva.SelectedIndex == 1, Notes = txtKonjungtiva.Text };
            igd.Sklera = new AbNormalAndNotes { IsAbNormal = optSklera.SelectedIndex == 1, Notes = txtSklera.Text };
            igd.BibirLidah = new AbNormalAndNotes { IsAbNormal = optBibirLidah.SelectedIndex == 1, Notes = txtBibirLidah.Text };
            igd.Mukosa = new AbNormalAndNotes { IsAbNormal = optMukosa.SelectedIndex == 1, Notes = txtMukosa.Text };
            igd.Mata = new AbNormalAndNotes { IsAbNormal = optMata.SelectedIndex == 1, Notes = txtMata.Text };
            igd.KondisiKepala = new AbNormalAndNotes { IsAbNormal = optKondisiKepala.SelectedIndex == 1, Notes = txtKondisiKepala.Text };
            igd.Leher = new AbNormalAndNotes { IsAbNormal = optLeher.SelectedIndex == 1, Notes = txtLeher.Text };
            igd.Trakea = new AbNormalAndNotes { IsAbNormal = optTrakea.SelectedIndex == 1, Notes = txtTrakea.Text };
            igd.Jvp = new AbNormalAndNotes { IsAbNormal = optJvp.SelectedIndex == 1, Notes = txtJvp.Text };
            igd.LNN = new AbNormalAndNotes { IsAbNormal = optLNN.SelectedIndex == 1, Notes = txtLNN.Text };
            igd.Tiroid = new AbNormalAndNotes { IsAbNormal = optTiroid.SelectedIndex == 1, Notes = txtTiroid.Text };
            igd.KondisiLeher = new AbNormalAndNotes { IsAbNormal = optKondisiLeher.SelectedIndex == 1, Notes = txtKondisiLeher.Text };
            igd.Thorax2 = new AbNormalAndNotes { IsAbNormal = optThorax.SelectedIndex == 1, Notes = txtThorax.Text };
            igd.Jantung = new AbNormalAndNotes { IsAbNormal = optJantung.SelectedIndex == 1, Notes = txtJantung.Text };
            igd.Paru = new AbNormalAndNotes { IsAbNormal = optParu.SelectedIndex == 1, Notes = txtParu.Text };
            igd.Abdomen2 = new AbNormalAndNotes { IsAbNormal = optAbdomen.SelectedIndex == 1, Notes = txtAbdomen.Text };
            igd.Punggung = new AbNormalAndNotes { IsAbNormal = optPunggung.SelectedIndex == 1, Notes = txtPunggung.Text };
            igd.Ekstremitas = new AbNormalAndNotes { IsAbNormal = optEkstremitas.SelectedIndex == 1, Notes = txtEkstremitas.Text };
            igd.Genitalia = new AbNormalAndNotes { IsAbNormal = optGenitalia.SelectedIndex == 1, Notes = txtGenitalia.Text };


            igd.Condition = gcsCtl.Condition;
            igd.Consciousness = gcsCtl.Gcs;


            assessment.PhysicalExam = JsonConvert.SerializeObject(igd);



            // Objective
            if (!string.IsNullOrWhiteSpace(rim.Info2))
                rim.Info2 = String.Concat(rim.Info2, Environment.NewLine, GenerateSoapObjective(igd));
            else
                rim.Info2 = GenerateSoapObjective(igd);
        }

        private string GenerateSoapObjective(Igd pe)
        {
            var strBuilder = new StringBuilder();



            strBuilder.AppendLine("PRIMARY SURVEY:");
            strBuilder.AppendLine("TRIAGE:");
            strBuilder.AppendLine(string.Empty);


            // ESI
            var esi = StandardReference.LoadStandardReferenceItem(AppEnum.StandardReference.Triage5Level, pe.Esi.Id);
            if (esi.ItemID != null)
            {
                strBuilder.AppendFormat("{0}: ", esi.ItemName);

                if (pe.Esi.ConditionIds != null && pe.Esi.ConditionIds.Length > 0)
                {
                    var stdi = new AppStandardReferenceItemCollection();
                    stdi.Query.Where(stdi.Query.StandardReferenceID == pe.Esi.Id, stdi.Query.ItemID.In(pe.Esi.ConditionIds));
                    stdi.LoadAll();
                    foreach (var item in stdi)
                    {
                        strBuilder.AppendFormat("{0} | ", item.ItemName.Trim());
                    }
                }
                strBuilder.AppendLine(string.Empty);
            }


            SoapObjectiveAppend("Pernafasan:", pe.Pernapasan.Summary, strBuilder);
            SoapObjectiveAppend("Sirkulasi:", pe.Sirkulasi.Summary, strBuilder);
            SoapObjectiveAppend("Penilaian Bayi Baru Lahir:", pe.PenilaianBayi.Summary, strBuilder);
            SoapObjectiveAppend("Disabilitas:", pe.Disabilitas.Summary, strBuilder);
            SoapObjectiveAppend("Exposur:", pe.Eksposur.Summary, strBuilder);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendLine("SECONDARY SURVEY:");
            strBuilder.AppendLine(pe.Consciousness.GetSoapObjective(pe.Condition));
            SoapObjectiveAppend("Lain-lain:", pe.Others.Summary, strBuilder);

            var isIncludeNormal = AppParameter.IsYes(AppParameter.ParameterItem.IsSoapFromPysicalExamIncludeNormalValue);

            if (isIncludeNormal || pe.Paten.IsAbNormal)
            {
                strBuilder.AppendFormat("Paten : {1}: {0}", pe.Paten.Notes, (pe.Paten.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.ObsPartial.IsAbNormal)
            {
                strBuilder.AppendFormat("Obstruksi Partial : {1}: {0}", pe.ObsPartial.Notes, (pe.ObsPartial.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.ObsTotal.IsAbNormal)
            {
                strBuilder.AppendFormat("Obstruksi total : {1}: {0}", pe.ObsTotal.Notes, (pe.ObsTotal.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Trauma.IsAbNormal)
            {
                strBuilder.AppendFormat("Trauma jalan napas : {1}: {0}", pe.Trauma.Notes, (pe.Trauma.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Resiko.IsAbNormal)
            {
                strBuilder.AppendFormat("Resiko aspirasi : {1}: {0}", pe.Resiko.Notes, (pe.Resiko.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.BendaAsing.IsAbNormal)
            {
                strBuilder.AppendFormat("Benda asing : {1}: {0}", pe.BendaAsing.Notes, (pe.BendaAsing.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Kesimpulan.IsAbNormal)
            {
                strBuilder.AppendFormat("Kesimpulan : {1}: {0}", pe.Kesimpulan.Notes, (pe.Kesimpulan.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Kepala.IsAbNormal)
            {
                strBuilder.AppendFormat("Kepala : {1}: {0}", pe.Kepala.Notes, (pe.Kepala.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Konjungtiva.IsAbNormal)
            {
                strBuilder.AppendFormat("Konjungtiva  : {1}: {0}", pe.Konjungtiva.Notes, (pe.Konjungtiva.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Sklera.IsAbNormal)
            {
                strBuilder.AppendFormat("Sklera : {1}: {0}", pe.Sklera.Notes, (pe.Sklera.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.BibirLidah.IsAbNormal)
            {
                strBuilder.AppendFormat("Bibir / Lidah  : {1}: {0}", pe.BibirLidah.Notes, (pe.BibirLidah.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Mukosa.IsAbNormal)
            {
                strBuilder.AppendFormat("Mukosa : {1}: {0}", pe.Mukosa.Notes, (pe.Mukosa.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Mata.IsAbNormal)
            {
                strBuilder.AppendFormat("Mata : {1}: {0}", pe.Mata.Notes, (pe.Mata.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.KondisiKepala.IsAbNormal)
            {
                strBuilder.AppendFormat("Kondisi Kepala : {1}: {0}", pe.KondisiKepala.Notes, (pe.KondisiKepala.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Leher.IsAbNormal)
            {
                strBuilder.AppendFormat("Leher : {1}: {0}", pe.Leher.Notes, (pe.Leher.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Trakea.IsAbNormal)
            {
                strBuilder.AppendFormat("Trakea : {1}: {0}", pe.Trakea.Notes, (pe.Trakea.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Jvp.IsAbNormal)
            {
                strBuilder.AppendFormat("JVP : {1}: {0}", pe.Jvp.Notes, (pe.Jvp.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.LNN.IsAbNormal)
            {
                strBuilder.AppendFormat("LNN : {1}: {0}", pe.LNN.Notes, (pe.LNN.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Tiroid.IsAbNormal)
            {
                strBuilder.AppendFormat("Tiroid : {1}: {0}", pe.Tiroid.Notes, (pe.Tiroid.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.KondisiLeher.IsAbNormal)
            {
                strBuilder.AppendFormat("KondisiLeher : {1}: {0}", pe.KondisiLeher.Notes, (pe.KondisiLeher.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Thorax2.IsAbNormal)
            {
                strBuilder.AppendFormat("Thorax : {1}: {0}", pe.Thorax2.Notes, (pe.Thorax2.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Jantung.IsAbNormal)
            {
                strBuilder.AppendFormat("Jantung: {1}: {0}", pe.Jantung.Notes, (pe.Jantung.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Paru.IsAbNormal)
            {
                strBuilder.AppendFormat("Paru : {1}: {0}", pe.Paru.Notes, (pe.Paru.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Abdomen2.IsAbNormal)
            {
                strBuilder.AppendFormat("Abdomen  : {1}: {0}", pe.Abdomen2.Notes, (pe.Abdomen2.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Punggung.IsAbNormal)
            {
                strBuilder.AppendFormat("Punggung  : {1}: {0}", pe.Punggung.Notes, (pe.Punggung.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Ekstremitas.IsAbNormal)
            {
                strBuilder.AppendFormat("Ekstremitas  : {1}: {0}", pe.Ekstremitas.Notes, (pe.Ekstremitas.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }
            if (isIncludeNormal || pe.Genitalia.IsAbNormal)
            {
                strBuilder.AppendFormat("Genitalia : {1}: {0}", pe.Genitalia.Notes, (pe.Genitalia.IsAbNormal ? "Abnormal" : "Normal"));
                strBuilder.AppendLine(string.Empty);
            }




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

        protected void ddlEsi_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            PopulateEsiReason(e.Value);
        }

        private void PopulateEsiReason(string esiId)
        {
            cblEsiCondition.Items.Clear();
            var coll = StandardReference.LoadStandardReferenceItemCollection(esiId, String.Empty, true);
            foreach (var stdi in coll)
            {
                cblEsiCondition.Items.Add(new ButtonListItem(stdi.ItemName, stdi.ItemID));
            }
        }

        #endregion
    }
}