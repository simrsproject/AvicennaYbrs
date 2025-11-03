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
    public partial class PsychiatryPeV2Ctl : BaseAssessmentCtl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgGenogram.DataValue = Genogram();
        }


        #region override method
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var ent = new PsychiatryPe();

            // Get Education
            var asses = assessment;
            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                // Convert to class w json
                try
                {
                    ent = JsonConvert.DeserializeObject<PsychiatryPe>(asses.PhysicalExam);
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

            txtSensorik.Text = ent.Sensorik;
            txtMotorik.Text = ent.Motorik;
            txtOtonom.Text = ent.Otonom;
            txtNeurologis.Text = ent.Neurologis;
            txtOtherExam.Text = asses.OtherExam;
            optJnsUsia.SelectedValue = ent.JnsUsia;
            optPenampilan.SelectedValue = ent.Penampilan;
            optSikap.SelectedValue = ent.Sikap;
            txtKondisiUmum.Text = ent.KondisiUmum;
            txtKesadaran.Text = ent.Kesadaran;
            optConcentration.SelectedValue = ent.Concentration;
            optMaintainCon.SelectedValue = ent.MaintainCon;
            optDistractCon.SelectedValue = ent.DistractCon;
            optMemory.SelectedValue = ent.Memory;
            optJudgement.SelectedValue = ent.Judgement;
            txtInsight.Text = ent.Insight;
            txtMood.Text = ent.Mood;
            txtAfek.Text = ent.Afek;
            txtPerception.Text = ent.Perception;
            txtProsesPikir.Text = ent.ProsesPikir;
            txtArusPikir.Text = ent.ArusPikir;
            txtIsiPikir.Text = ent.IsiPikir;
            txtPsikodinamik.Text = ent.Psikodinamik;
            txtOtherThing.Text = ent.OtherThing;

            txtAksis1.Text = ent.Aksis1;
            txtAksis2.Text = ent.Aksis2;
            txtAksis3.Text = ent.Aksis3;
            txtAksis4.Text = ent.Aksis4;
            //txtAksis5.Text = ent.Aksis5;

            //txtPsikofarmaka.Text = ent.Psikofarmaka;
            txtPsikoterapi.Text = ent.Psikoterapi;
            txtPsikoedukasi.Text = ent.Psikoedukasi;
            //txtPsikososial.Text = ent.Psikososial;

            txtVitam.Text = ent.Vitam;
            txtFunctionam.Text = ent.Functionam;
            txtSanation.Text = ent.Sanation;
            txtInsomnia.Text = ent.Insomnia;
            chkInsomnia.Checked = ent.IsInsomnia;
            txtPhysicalExamNotes.Text = ent.Notes;

            imgGenogram.DataValue = Genogram();
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {

            var ent = new PsychiatryPe
            {
                Condition = gcsCtl.Condition,
                Consciousness = gcsCtl.Gcs,

            };

            ent.Sensorik = txtSensorik.Text;
            ent.Motorik = txtMotorik.Text;
            ent.Otonom = txtOtonom.Text;
            ent.Neurologis = txtNeurologis.Text;
            ent.JnsUsia = optJnsUsia.SelectedValue;
            ent.Penampilan = optPenampilan.SelectedValue;
            ent.Sikap = optSikap.SelectedValue;
            ent.KondisiUmum = txtKondisiUmum.Text;
            ent.Kesadaran = txtKesadaran.Text;
            ent.Concentration = optConcentration.SelectedValue;
            ent.MaintainCon = optMaintainCon.SelectedValue;
            ent.DistractCon = optDistractCon.SelectedValue;
            ent.Memory = optMemory.SelectedValue;
            ent.Judgement = optJudgement.SelectedValue;
            ent.Insight = txtInsight.Text;
            ent.Mood = txtMood.Text;
            ent.Afek = txtAfek.Text;
            ent.Perception = txtPerception.Text;
            ent.ProsesPikir = txtProsesPikir.Text;
            ent.ArusPikir = txtArusPikir.Text;
            ent.IsiPikir = txtIsiPikir.Text;
            ent.Psikodinamik = txtPsikodinamik.Text;
            ent.OtherThing = txtOtherThing.Text;

            ent.Aksis1 = txtAksis1.Text;
            ent.Aksis2 = txtAksis2.Text;
            ent.Aksis3 = txtAksis3.Text;
            ent.Aksis4 = txtAksis4.Text;
            //ent.Aksis5 = txtAksis5.Text;

            //ent.Psikofarmaka = txtPsikofarmaka.Text;
            ent.Psikoterapi = txtPsikoterapi.Text;
            ent.Psikoedukasi = txtPsikoedukasi.Text;
            //ent.Psikososial = txtPsikososial.Text;

            ent.Vitam = txtVitam.Text;
            ent.Functionam = txtFunctionam.Text;
            ent.Sanation = txtSanation.Text;
            ent.Insomnia = txtInsomnia.Text;
            ent.IsInsomnia = chkInsomnia.Checked ;
            ent.Notes = txtPhysicalExamNotes.Text;

            assessment.OtherExam = txtOtherExam.Text;
            assessment.PhysicalExam = JsonConvert.SerializeObject(ent);
            if (Session["genogram"] != null)
                assessment.Genogram = (byte[])Session["genogram"];

            // Save last genogram
            if (Session["genogram"] != null)
            {
                var pg = new PatientGenogram();
                if (!pg.LoadByPrimaryKey(PatientID))
                {
                    pg.AddNew();
                    pg.PatientID = PatientID;
                }
                if (assessment.CreatedDateTime >= pg.CreatedDateTime) // hanya simpan jika bukan data lama
                {
                    pg.Genogram = assessment.Genogram;
                    pg.Save();
                }
            }
            //

            Session["genogram"] = null;

            // Objective
            rim.Info2 = GenerateSoapObjective(ent);
        }

        private string GenerateSoapObjective(PsychiatryPe pe)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendFormat("Status Neurologi: {0} GCS: E: {1} M: {2} V: {3}", pe.Consciousness.ConsciousnessDescription, pe.Consciousness.Eye.Score, pe.Consciousness.Motor.Score, pe.Consciousness.Verbal.Score);
            strBuilder.AppendLine(string.Empty);

            strBuilder.AppendFormat("• SENSORIK: {0}", pe.Sensorik);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat("• MOTORIK: {0}", pe.Motorik);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat("• OTONOM: {0}", pe.Otonom);
            strBuilder.AppendLine(string.Empty);
            strBuilder.AppendFormat("• Pemeriksaan Neurologis: {0}", pe.Neurologis);
            strBuilder.AppendLine(string.Empty);
            return strBuilder.ToString();
        }

        #endregion


        protected byte[] Genogram()
        {
            if (Session["genogram"] == null)
            {
                var assess = new PatientAssessment();
                if (assess.LoadByPrimaryKey(RegistrationInfoMedicID) && assess.Genogram != null)
                {
                    Session["genogram"] = assess.Genogram;
                }
                else
                {
                    // ambil dari Genogram terakhir ada di PatientGenogram 
                    var pg = new PatientGenogram();
                    if (pg.LoadByPrimaryKey(PatientID) && pg.Genogram != null)
                    {
                        Session["genogram"] = assess.Genogram;
                    }
                }

            }
            return (byte[])Session["genogram"];
        }
    }
}