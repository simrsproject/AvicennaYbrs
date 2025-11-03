using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient.Heart
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Heart_P2.
    /// </summary>
    public partial class Heart_P2 : Telerik.Reporting.Report
    {
        public Heart_P2(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            var patientID = printJobParameters.FindByParameterName("PatientID").ValueString;
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;


            var asses = new PatientAssessment();
            asses.LoadByPrimaryKey(rimid);

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(asses.RegistrationNo);
            
            
            txtAncillaryExam.Value = asses.OtherExam;

            PopulatePhysicalExam(asses);

            txtDiagnosa.Value = asses.Diagnose;
           
        }

     
        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<HeartPe>(asses.PhysicalExam);

                //<asp:ListItem Text="Mild" Value="Mild" Selected="True"></asp:ListItem>
                //   <asp:ListItem Text="Moderate" Value="Moderate"></asp:ListItem>
                //   <asp:ListItem Text="Severe" Value="Severe"></asp:ListItem>
                
                var gcs = pexam.Consciousness;
                
                chkLeherNormal.Value = !pexam.Leher.IsAbNormal;
                chkLeherAbnormal.Value = pexam.Leher.IsAbNormal;
                txtLeher.Value = pexam.Leher.Notes;

                chkThoraxNormal.Value = !pexam.Thorax.IsAbNormal;
                chkThoraxAbnormal.Value = pexam.Thorax.IsAbNormal;
                txtThorax.Value = pexam.Thorax.Notes;

                txtJantung.Value = pexam.Jantung;
                txtInspeksi.Value = pexam.Inspeksi;
                txtPalpasi.Value = pexam.Palpasi;
                txtPerkusi.Value = pexam.PerkusiLeft;
                txtPerkusiKanan.Value = pexam.PerkusiRight;

                chkLipPlus.Value = pexam.Lifting == "+";
                chkLipMin.Value = pexam.Lifting != "+";
                txtS1S2.Value = pexam.AuscultationS1S2;
                txtMurmur.Value = pexam.Murmur;

                txtParu.Value = pexam.Paru;
                txtParuInspeksi.Value = pexam.ParuInspeksi;
                txtParuPalpasi.Value = pexam.ParuPalpasi;
                txtParuPerkusi.Value = pexam.ParuPerkusi;
                txtParuAusk.Value = pexam.ParuAusk;

                txtAbdomen.Value = pexam.Abdomen;
                txtAbdoInspeksi.Value = pexam.AbdoInspeksi;
                txtAbdoPalpasi.Value = pexam.AbdoPalpasi;
                txtAbdoPerkusi.Value = pexam.AbdoPerkusi;

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