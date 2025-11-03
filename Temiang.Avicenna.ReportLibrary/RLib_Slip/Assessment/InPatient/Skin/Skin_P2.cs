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
    /// Summary description for Skin_P2.
    /// </summary>
    public partial class Skin_P2 : Telerik.Reporting.Report
    {
        public Skin_P2(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoOnly(this.pageHeader);

            
            PopulatePhysicalExam(asses);

            PopulateLocalistStatus(asses);

            txtAncillaryExam.Value = asses.OtherExam;

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
                var pexam = JsonConvert.DeserializeObject<SkinPe>(asses.PhysicalExam);
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

                chkJantungNormal.Value = !pexam.Jantung.IsAbNormal;
                chkJantungAbnormal.Value = pexam.Jantung.IsAbNormal;
                txtJantung.Value = pexam.Jantung.Notes;

                chkParuNormal.Value = !pexam.Paru.IsAbNormal;
                chkParuAbnormal.Value = pexam.Paru.IsAbNormal;
                txtParu.Value = pexam.Paru.Notes;

                chkAbdomenNormal.Value = !pexam.Abdomen.IsAbNormal;
                chkAbdomenAbnormal.Value = pexam.Abdomen.IsAbNormal;
                txtAbdomen.Value = pexam.Abdomen.Notes;

                chkGenitaliaNormal.Value = !pexam.GenitaliaAndAnus.IsAbNormal;
                chkGenitaliaAbnormal.Value = pexam.GenitaliaAndAnus.IsAbNormal;
                txtGenitalia.Value = pexam.GenitaliaAndAnus.Notes;

                chkEkstremitasNormal.Value = !pexam.Ekstremitas.IsAbNormal;
                chkEkstremitasAbnormal.Value = pexam.Ekstremitas.IsAbNormal;
                txtEkstremitas.Value = pexam.Ekstremitas.Notes;

                chkKulitNormal.Value = !pexam.Kulit.IsAbNormal;
                chkKulitAbnormal.Value = pexam.Kulit.IsAbNormal;
                txtKulit.Value = pexam.Kulit.Notes;

                txtDermatologikus.Value = pexam.Dermatologikus;
            }
            catch (Exception)
            {
                //Nothing
            }
        }

       
    }
}