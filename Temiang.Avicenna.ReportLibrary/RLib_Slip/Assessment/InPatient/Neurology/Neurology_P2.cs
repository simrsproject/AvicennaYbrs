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
    /// Summary description for Neurology_P2.
    /// </summary>
    public partial class Neurology_P2 : Telerik.Reporting.Report
    {
        public Neurology_P2(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoAndTextBottom(this.pageHeader);

            var patientID = printJobParameters.FindByParameterName("PatientID").ValueString;
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;


            PopulatePhysicalExam(asses, reg);
            PopulateNeuroExam(asses, reg);
        }

        private void PopulateNeuroExam(PatientAssessment asses, BusinessObject.Registration reg)
        {
            if (string.IsNullOrEmpty(asses.ReviewOfSystem)) return;
            try
            {
                // Convert to class w json
                var ne = JsonConvert.DeserializeObject<NeurologiRos>(asses.ReviewOfSystem);
                chkMotorikBlmDpt.Value = ne.Motorik.Contains("NOT");
                chkMotorikNormal.Value = ne.Motorik.Contains("NML");
                chkHemiparesi.Value = ne.Motorik.Contains("HEM");
                chkParaparesis.Value = ne.Motorik.Contains("PAR");
                chkTetraparesis.Value = ne.Motorik.Contains("TET");
                chkMonoparesis.Value = ne.Motorik.Contains("MON");

                // TODO: Kekuatan
                txtKelemahanLain.Value = ne.MotorikWeakness;
                chkSensorikBlmdptnilai.Value = ne.Sensorik.Contains("NML");
                txtParestesia.Value = ne.SensorikParestesia;
                txtGangguansensorik.Value = ne.SensorikOtherProblem;

                chkFungsiLuhurNML.Value = ne.FungsiLuhur.Contains("NML");
                chkFungsiLuhurNOT.Value = ne.FungsiLuhur.Contains("NOT");
                chkFungsiLuhurDEL.Value = ne.FungsiLuhur.Contains("DEL");
                chkFungsiLuhurMUT.Value = ne.FungsiLuhur.Contains("MUT");
                chkFungsiLuhurAFM.Value = ne.FungsiLuhur.Contains("AFM");
                chkFungsiLuhurAFS.Value = ne.FungsiLuhur.Contains("AFS");
                chkFungsiLuhurANO.Value = ne.FungsiLuhur.Contains("ANO");
                chkFungsiLuhurRDO.Value = ne.FungsiLuhur.Contains("RDO");
                chkFungsiLuhurWTD.Value = ne.FungsiLuhur.Contains("WTD");
                chkFungsiLuhurCLD.Value = ne.FungsiLuhur.Contains("CLD");
                chkFungsiLuhurRPT.Value = ne.FungsiLuhur.Contains("RPT");
                txtFungsiLuhurOther.Value = ne.FungsiLuhurOther;

                chkVegetatifNML.Value = ne.Vegetatif.Contains("NML");
                chkVegetatifRET.Value = ne.Vegetatif.Contains("RET");
                chkVegetatifINC.Value = ne.Vegetatif.Contains("INC");

            }
            catch (Exception)
            {
                //Nothing
            }
        
        }

        private void PopulatePhysicalExam(PatientAssessment asses, BusinessObject.Registration reg)
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
                
                txtSpO2.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.SpO2, asesDateTime);


            }

            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pe = JsonConvert.DeserializeObject<NeurologiPeIp>(asses.PhysicalExam);

                chkKardioNormal.Value = pe.Cardio.Contains("NML");
                chkangina.Value = pe.Cardio.Contains("ANG");
                chkplapitasi.Value = pe.Cardio.Contains("PAL");
                chkdyspnea.Value = pe.Cardio.Contains("DYS");
                chkorthopnea.Value = pe.Cardio.Contains("ORT");
                chkmurmur.Value = pe.Cardio.Contains("MGP");
                chkaritmia.Value = pe.Cardio.Contains("ART");
                chkbatuk.Value = pe.Cardio.Contains("COG");
                chkronchi.Value = pe.Cardio.Contains("RON");

                chkGasNormal.Value = pe.Gastro.Contains("NML");
                chkNyeriAbdo.Value = pe.Gastro.Contains("ABP");
                chkdiare.Value = pe.Gastro.Contains("DIA");
                chkkonstipasi.Value = pe.Gastro.Contains("CON");
                chkmelena.Value = pe.Gastro.Contains("MEL");
                chkhematemisis.Value = pe.Gastro.Contains("HEM");

                chkUroginitalNormal.Value = pe.Urogenital.Contains("NML");
                chkdisuria.Value = pe.Urogenital.Contains("DIS");
                chkfrequency.Value = pe.Urogenital.Contains("FREQ");
                chkhermaturia.Value = pe.Urogenital.Contains("HEM");
                chknocturia.Value = pe.Urogenital.Contains("NOC");
                chkretentio.Value = pe.Urogenital.Contains("RET");
                chkincontinentia.Value = pe.Urogenital.Contains("INC");

                chkEkstremitasNormal.Value = pe.Extremitas.Contains("NML");
                chkedema.Value = pe.Extremitas.Contains("EDE");
                chkanemia.Value = pe.Extremitas.Contains("ANE");


            }
            catch (Exception)
            {
                //Nothing
            }
        }

       

    }
}