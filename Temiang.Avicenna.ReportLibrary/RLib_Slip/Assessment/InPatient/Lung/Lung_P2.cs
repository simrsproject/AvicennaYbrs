using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Lung_P2.
    /// </summary>
    public partial class Lung_P2 : Telerik.Reporting.Report
    {
        public Lung_P2(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses, Patient pat)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoOnly(this.pageHeader);

            PopulatePhysicalExam(asses);

            txtAncillaryExam.Value = asses.OtherExam;
        }
       
        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<LungPe>(asses.PhysicalExam);

              
                chkLeherNormal.Value = !pexam.Leher.IsAbNormal;
                chkLeherAbnormal.Value = pexam.Leher.IsAbNormal;
                txtLeher.Value = pexam.Leher.Notes;

                chkThoraxNormal.Value = !pexam.Thorax.IsAbNormal;
                chkThoraxAbnormal.Value = pexam.Thorax.IsAbNormal;
                txtThorax.Value = pexam.Thorax.Notes;

                // Paru
                var paru = pexam.Paru;
                chkRespAbnorm.Value = paru.Inspeksi.Respiratory.IsAbNormal;
                chkRespNorm.Value = !paru.Inspeksi.Respiratory.IsAbNormal;
                txtResp.Value = paru.Inspeksi.Respiratory.Notes;

                chkSelaIgaAbnor.Value = paru.Inspeksi.SelaIga.IsAbNormal;
                chkSelaIgaNorm.Value = !paru.Inspeksi.SelaIga.IsAbNormal;
                txtSelaIga.Value = paru.Inspeksi.SelaIga.Notes;

                chkFremitusAbnor.Value = paru.Palpasi.Fremitus.IsAbNormal;
                chkFremitusNor.Value = !paru.Palpasi.Fremitus.IsAbNormal;
                txtFremitus.Value = paru.Palpasi.Fremitus.Notes;

                chkNyeriTekanTidak.Value = !paru.Palpasi.NyeriTekan.IsExist;
                chkNyeriTekanAda.Value = paru.Palpasi.NyeriTekan.IsExist;
                txtNyeriTekan.Value = paru.Palpasi.NyeriTekan.Notes;

                chkKrepitasiTidak.Value = !paru.Palpasi.Krepitasi.IsExist;
                chkKrepitasiAda.Value = paru.Palpasi.Krepitasi.IsExist;
                txtKrepitasi.Value = paru.Palpasi.Krepitasi.Notes;

                chkPerkusiSonor.Value = paru.Perkusi.Condition == "Sonor";
                chkPerkusiHipersonor.Value = paru.Perkusi.Condition == "Hypersonor";
                chkPerkusiRedup.Value = paru.Perkusi.Condition == "Dim";
                chkPerkusiPekak.Value = paru.Perkusi.Condition == "Deaf";
                txtPerkusiLocation.Value = paru.Perkusi.Location;

                chkVesikularBronkial.Value = paru.Auskultasi.Vesikular.Condition == "Bronkial";
                chkVesikularSubBronkial.Value = paru.Auskultasi.Vesikular.Condition == "Sub Bronkial";
                txtVesikularLocation.Value = paru.Auskultasi.Vesikular.Location;

                chkRonchiKering.Value = paru.Auskultasi.Ronchi.Condition == "Dry";
                chkRonchiBasah.Value = paru.Auskultasi.Ronchi.Condition == "Wet";
                txtRonchiLocation.Value = paru.Auskultasi.Ronchi.Location;

                chkBisingTidak.Value = !paru.Auskultasi.Bising.IsExist;
                chkBisingAda.Value = paru.Auskultasi.Bising.IsExist;
                txtBising.Value = paru.Auskultasi.Bising.Notes;

                // end Paru

                txtJantung_Notes.Value = pexam.JantungMethod.AbNormalAndNotes.Notes;
                txtJantung_Inspeksi.Value = pexam.JantungMethod.Inspeksi;
                txtJantung_Palpasi.Value = pexam.JantungMethod.Palpasi;
                txtJantung_Perkusi.Value = pexam.JantungMethod.Perkusi;
                txtJantung_Auskultasi.Value = pexam.JantungMethod.Auskultasi;

                txtAbdomen_Notes.Value = pexam.AbdomenMethod.AbNormalAndNotes.Notes;
                txtAbdomen_Inspeksi.Value = pexam.AbdomenMethod.Inspeksi;
                txtAbdomen_Palpasi.Value = pexam.AbdomenMethod.Palpasi;
                txtAbdomen_Perkusi.Value = pexam.AbdomenMethod.Perkusi;
                txtAbdomen_Auskultasi.Value = pexam.AbdomenMethod.Auskultasi;

                chkGenitaliaNormal.Value = !pexam.GenitaliaAndAnus.IsAbNormal;
                chkGenitaliaAbnormal.Value = pexam.GenitaliaAndAnus.IsAbNormal;
                txtGenitalia.Value = pexam.GenitaliaAndAnus.Notes;

                chkEkstremitasNormal.Value = !pexam.Ekstremitas.IsAbNormal;
                chkEkstremitasAbnormal.Value = pexam.Ekstremitas.IsAbNormal;
                txtEkstremitas.Value = pexam.Ekstremitas.Notes;

                chkKulitNormal.Value = !pexam.Kulit.IsAbNormal;
                chkKulitAbnormal.Value = pexam.Kulit.IsAbNormal;
                txtKulit.Value = pexam.Kulit.Notes;


            }
            catch (Exception)
            {
                //Nothing
            }
        }

     
    }
}