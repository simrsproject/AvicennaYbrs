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
    /// Summary description for Psychiatry_P2.
    /// </summary>
    public partial class Psychiatry_P2 : Telerik.Reporting.Report
    {
        public Psychiatry_P2(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses, Patient pat)
        {
            //
            // Required for telerik Reporting designer support
            //
            
            PopulatePhysicalExam(asses, reg, pat);

        }



        private void PopulatePhysicalExam(PatientAssessment asses, BusinessObject.Registration reg, Patient pat)
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

            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pe = JsonConvert.DeserializeObject<PsychiatryPe>(asses.PhysicalExam);

                var gcs = pe.Consciousness;
                txtConsciousness.Value = gcs.ConsciousnessDescription;
                txtGcsEye.Value = gcs.Eye.Score.ToString();
                txtGcsMotor.Value = gcs.Motor.Score.ToString();
                txtGcsVerbal.Value = gcs.Verbal.Score.ToString();

                txtSensorik.Value = pe.Sensorik;
                txtMotorik.Value = pe.Motorik;
                txtOtonom.Value = pe.Otonom;
                txtneurologis.Value = pe.Neurologis;
                chkSMAnak.Value = pe.JnsUsia == "A";
                chkSMRemaja.Value = pe.JnsUsia == "R";
                chkSMOT.Value = pe.JnsUsia == "T";
                chkSMJKLaki.Value = pat.Sex == "M";
                chkSMJKPerempuan.Value = pat.Sex == "F";
                chkTampakSesuai.Value = pe.Penampilan == "LA";
                chkTdktampakSesuai.Value = pe.Penampilan == "NA";
                chkKurang.Value = pe.Sikap == "MIN";
                chkTidakKoop.Value = pe.Sikap == "UNC";
                chkMondar.Value = pe.Sikap == "BNA";
                txtSikapLainnya.Value = pe.Sikap;
                txtkondisiumum.Value = pe.KondisiUmum;
                txtkesadaran.Value = pe.Kesadaran;
                chkPusatPerhatianBaik.Value = pe.Concentration == "G";
                chkPusatPerhatianKrg.Value = pe.Concentration == "M";
                chkPusatPerhatianBuruk.Value = pe.Concentration == "B";

                chkThnPerhatianBaik.Value = pe.MaintainCon == "G";
                chkThnPerhatianKrg.Value = pe.MaintainCon == "M";
                chkThnPerhatianBuruk.Value = pe.MaintainCon == "B";

                chkDistractConG.Value = pe.DistractCon == "G";
                chkDistractConM.Value = pe.DistractCon == "M";
                chkDistractConB.Value = pe.DistractCon == "B";

                chkMemoryG.Value = pe.Memory == "G";
                chkMemoryM.Value = pe.Memory == "M";
                chkMemoryB.Value = pe.Memory == "B";

                chkJudgementG.Value = pe.Judgement == "G";
                chkJudgementM.Value = pe.Judgement == "M";
                chkJudgementB.Value = pe.Judgement == "B";

                txtInsight.Value = pe.Insight;
                txtMood.Value = pe.Mood;
                txtAfek.Value = pe.Afek;
                txtPersepsi.Value = pe.Perception;
                txtProsesPikir.Value = pe.ProsesPikir;
                txtArusPikir.Value = pe.ArusPikir;
                txtIsiPikir.Value = pe.IsiPikir;

            }
            catch (Exception)
            {
                //Nothing
            }


        }
      
    }
}