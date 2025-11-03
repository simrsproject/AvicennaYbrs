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
    /// Summary description for Nursing_P2.
    /// </summary>
    public partial class Nursing_P2 : Telerik.Reporting.Report
    {
        public Nursing_P2(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoAndTextBottom(this.pageHeader);

            PopulatePhysicalExam(asses);

            PopulateLocalistStatus(asses);
        }


        private void PopulateLocalistStatus(PatientAssessment asses)
        {
            // Reset Image
            picLocalistStatus01.Value = null;

            // Update Image
            var loc = new RegistrationInfoMedicBodyDiagramCollection();
            loc.Query.Where(loc.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            if (loc.LoadAll())
            {
                if (loc.Count>0 && loc[0] != null)
                {
                    picLocalistStatus01.Value = (new ImageHelper()).ConvertByteArrayToImage(loc[0].BodyImage);
                }
            }
        }

        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<NursingPe>(asses.PhysicalExam);

                //<asp:ListItem Text="Mild" Value="Mild" Selected="True"></asp:ListItem>
                //   <asp:ListItem Text="Moderate" Value="Moderate"></asp:ListItem>
                //   <asp:ListItem Text="Severe" Value="Severe"></asp:ListItem>
                

                chkKepalaNormal.Value = !pexam.Kepala.IsAbNormal;
                chkKepalaAbnormal.Value = pexam.Kepala.IsAbNormal;
                txtKepala.Value = pexam.Kepala.Notes;

                chkMataNormal.Value = !pexam.Mata.IsAbNormal;
                chkMataAbnormal.Value = pexam.Mata.IsAbNormal;
                txtMata.Value = pexam.Mata.Notes;

                chkThtNormal.Value = !pexam.Tht.IsAbNormal;
                chkThtAbnormal.Value = pexam.Tht.IsAbNormal;
                txtTht.Value = pexam.Tht.Notes;

                chkMulutNormal.Value = !pexam.Mulut.IsAbNormal;
                chkMulutAbnormal.Value = pexam.Mulut.IsAbNormal;
                txtMulut.Value = pexam.Mulut.Notes;

                chkLeherNormal.Value = !pexam.Leher.IsAbNormal;
                chkLeherAbnormal.Value = pexam.Leher.IsAbNormal;
                txtLeher.Value = pexam.Leher.Notes;

                chkThoraxNormal.Value = !pexam.Thorax.IsAbNormal;
                chkThoraxAbnormal.Value = pexam.Thorax.IsAbNormal;
                txtThorax.Value = pexam.Thorax.Notes;

                txtJantung.Value = pexam.Jantung.Notes;
                txtJantungInspeksi.Value = pexam.JantungInspeksi;
                txtJantungPalpasi.Value = pexam.JantungPalpasi;
                txtJantungPerkusi.Value = pexam.JantungPerkusi;
                txtJantungAusk.Value = pexam.JantungAusk;

                txtParu.Value = pexam.Paru.Notes;
                txtParuInspeksi.Value = pexam.ParuInspeksi;
                txtParuPalpasi.Value = pexam.ParuPalpasi;
                txtParuPerkusi.Value = pexam.ParuPerkusi;
                txtParuAusk.Value = pexam.ParuAusk;


                txtAbdomen.Value = pexam.Abdomen.Notes;
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