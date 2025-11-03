using System;
using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class Surgery_P2 : Telerik.Reporting.Report
    {
        public Surgery_P2(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            PopulatePhysicalExam(asses);

            PopulateLocalistStatus(asses);
        }


        private void PopulateLocalistStatus(PatientAssessment asses)
        {
            // Reset Image
            picLocalistStatus01.Value = null;
            picLocalistStatus02.Value = null;

            // Update Image
            var loc = new RegistrationInfoMedicBodyDiagramCollection();
            loc.Query.Where(loc.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            if (loc.LoadAll())
            {
                if (loc.Count > 0 && loc[0] != null)
                {
                    picLocalistStatus01.Value = (new ImageHelper()).ConvertByteArrayToImage(loc[0].BodyImage);
                }

                if (loc.Count > 1 && loc[1] != null)
                {
                    picLocalistStatus02.Value = (new ImageHelper()).ConvertByteArrayToImage(loc[1].BodyImage);
                }
            }
        }

        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<SurgicalPe>(asses.PhysicalExam);

                //<asp:ListItem Text="Mild" Value="Mild" Selected="True"></asp:ListItem>
                //   <asp:ListItem Text="Moderate" Value="Moderate"></asp:ListItem>
                //   <asp:ListItem Text="Severe" Value="Severe"></asp:ListItem>

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

                txtJantung_Notes.Value = pexam.JantungMethod.AbNormalAndNotes.Notes;
                txtJantung_Inspeksi.Value = pexam.JantungMethod.Inspeksi;
                txtJantung_Palpasi.Value = pexam.JantungMethod.Palpasi;
                txtJantung_Perkusi.Value = pexam.JantungMethod.Perkusi;
                txtJantung_Auskultasi.Value = pexam.JantungMethod.Auskultasi;

                txtParu_Notes.Value = pexam.ParuMethod.AbNormalAndNotes.Notes;
                txtParu_Inspeksi.Value = pexam.ParuMethod.Inspeksi;
                txtParu_Palpasi.Value = pexam.ParuMethod.Palpasi;
                txtParu_Perkusi.Value = pexam.ParuMethod.Perkusi;
                txtParu_Auskultasi.Value = pexam.ParuMethod.Auskultasi;

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