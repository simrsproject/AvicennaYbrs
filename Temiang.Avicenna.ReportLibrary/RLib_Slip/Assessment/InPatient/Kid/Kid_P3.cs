//TODO: Riwayat Tumbuh kembang
//TODO: Riwayat Imunisasi
using System;
using System.Collections.Generic;
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
    /// Summary description for Kid_P3.
    /// </summary>
    public partial class Kid_P3 : Telerik.Reporting.Report
    {
        public Kid_P3(string programID, PrintJobParameterCollection printJobParameters, PatientAssessment asses, BusinessObject.Registration reg)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoAndTextBottom(this.pageHeader);
            PopulatePhysicalExam(asses);
            txtAncillaryExam.Value = asses.OtherExam;
            txtDiagnosa.Value = asses.Diagnose;
        }


        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<KidPe>(asses.PhysicalExam);

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

                chkGrmAda.Value = pexam.StatusNeurogis.Grm;
                chkGrmTidak.Value = !pexam.StatusNeurogis.Grm;

                chkFisiologisNormal.Value = !pexam.StatusNeurogis.Fisiologis.IsAbNormal;
                chkFisiologisAbnormal.Value = pexam.StatusNeurogis.Fisiologis.IsAbNormal;
                txtFisiologis.Value = pexam.StatusNeurogis.Fisiologis.Notes;

                chkPatologisAda.Value = pexam.StatusNeurogis.Patologis;
                chkPatologisTidak.Value = !pexam.StatusNeurogis.Patologis;

                chkMotorikNormal.Value = !pexam.StatusNeurogis.Motorik.IsAbNormal;
                chkMotorikAbnormal.Value = pexam.StatusNeurogis.Motorik.IsAbNormal;
                txtMotorik.Value = pexam.StatusNeurogis.Motorik.Notes;

            }
            catch (Exception)
            {
                //Nothing
            }
        }

    }
}