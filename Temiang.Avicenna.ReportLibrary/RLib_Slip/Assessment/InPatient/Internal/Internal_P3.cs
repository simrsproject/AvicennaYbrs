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
    /// Summary description for Internal_P3.
    /// </summary>
    public partial class Internal_P3 : Telerik.Reporting.Report
    {
        public Internal_P3(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            PopulatePhysicalExam(asses, reg);

            PopulateVitalSign(asses, reg);

            txtAncillaryExam.Value = asses.OtherExam;

            txtLamaRawat.Value = string.Format("{0:n0}", asses.EstimatedDayInPatient);

            txtDiagnosa.Value = asses.Diagnose;
            PopulateTherapy(asses.RegistrationInfoMedicID);
            txtParamedicName.Value = ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName; 

        }
        private void PopulateTherapy(string registrationInfoMedicID)
        {
            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(registrationInfoMedicID);
            txtTherapy.Value = rim.Info4;
        }

        private void PopulateVitalSign(PatientAssessment asses, BusinessObject.Registration reg)
        {
            if (asses.AssessmentDateTime != null)
            {
                var asesDateTime = asses.AssessmentDateTime.Value;
                txtTekananDarah.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.BloodPressure, asesDateTime);

                txtNadi.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.HeartRate, asses.AssessmentDateTime.Value);

                txtPernafasan.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.RespiratoryRate, asesDateTime);

                txtSuhu.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.Temperature, asesDateTime);

                txtSkorNyeri.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.PainScale, asesDateTime);
            }

        }

        private void PopulatePhysicalExam(PatientAssessment asses, BusinessObject.Registration reg)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<InternalPe>(asses.PhysicalExam);

                chkKeadaanRingan.Value = pexam.Condition == "Mild";
                chkKeadaanSedang.Value = pexam.Condition == "Moderate";
                chkKeadaanBerat.Value = pexam.Condition == "Severe";

                var gcs = pexam.Consciousness;
                txtConsciousness.Value = gcs.ConsciousnessDescription;
                txtGcsEye.Value = gcs.Eye.Score.ToString();
                txtGcsMotor.Value = gcs.Motor.Score.ToString();
                txtGcsVerbal.Value = gcs.Verbal.Score.ToString();

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

                txtJantung_Notes.Value = pexam.JantungMethod.AbNormalAndNotes.Notes;
                txtJantung_Inspeksi.Value = pexam.JantungMethod.Inspeksi;
                txtJantung_Palpasi.Value = pexam.JantungMethod.Palpasi;
                txtJantung_Perkusi.Value = pexam.JantungMethod.Perkusi;
                txtJantung_Auskultasi.Value = pexam.JantungMethod.Auskultasi;

                txtParu.Value = pexam.ParuMethod.AbNormalAndNotes.Notes;
                txtParuInspeksi.Value = pexam.ParuMethod.Inspeksi;
                txtParuPalpasi.Value = pexam.ParuMethod.Palpasi;
                txtParuPerkusi.Value = pexam.ParuMethod.Perkusi;
                txtParuAusk.Value = pexam.ParuMethod.Auskultasi;

                txtAbdomen.Value = pexam.AbdomenMethod.AbNormalAndNotes.Notes;
                txtAbdoInspeksi.Value = pexam.AbdomenMethod.Inspeksi;
                txtAbdoPalpasi.Value = pexam.AbdomenMethod.Palpasi;
                txtAbdoPerkusi.Value = pexam.AbdomenMethod.Perkusi;


                chkGenitaliaNormal.Value = !pexam.GenitaliaAndAnus.IsAbNormal;
                chkGenitaliaAbnormal.Value = pexam.GenitaliaAndAnus.IsAbNormal;
                txtGenitalia.Value = pexam.GenitaliaAndAnus.Notes;

                chkAuskulatasiNormal.Value = !pexam.Auskulatasi.IsAbNormal;
                chkAuskulatasiAbnormal.Value = pexam.Auskulatasi.IsAbNormal;
                txtAuskulatasi.Value = pexam.Auskulatasi.Notes;

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