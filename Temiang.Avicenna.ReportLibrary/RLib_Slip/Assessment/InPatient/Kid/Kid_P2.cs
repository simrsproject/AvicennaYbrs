//TODO: Riwayat Tumbuh kembang

using System;
using System.Data;
using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{
    /// <summary>
    /// Summary description for Kid_P2.
    /// </summary>
    public partial class Kid_P2 : Telerik.Reporting.Report
    {
        public Kid_P2(string programID, PrintJobParameterCollection printJobParameters, PatientAssessment asses, BusinessObject.Registration reg)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            var patientID = printJobParameters.FindByParameterName("PatientID").ValueString;
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;


            PopulateBirtHistory(asses.PatientID);
            
            PopulatePhysicalExam(asses, reg);

            PopulateImmunizationHist(patientID);
        }

        private void PopulateImmunizationHist(string patientID)
        {
            var dtb = GetImmunizationHist(patientID);
            if (dtb == null) return;

            tblImmunization.DataSource = dtb;

            var othImu = new PatientImmunizationOther();
            if (othImu.LoadByPrimaryKey(patientID) && !string.IsNullOrWhiteSpace(othImu.Imunization))
                txtOtherImunization.Value = string.Format("Lainnya: {0}", othImu.Imunization);
        }

        private DataTable GetImmunizationHist(string patientID)
        {
            var query = new ImmunizationQuery("a");
            query.Select(query.ImmunizationID, query.ImmunizationName, query.MaxCount);
            query.OrderBy(query.IndexNo.Ascending);
            var dtb = query.LoadDataTable();
            for (int i = 1; i < 6; i++)
            {
                dtb.Columns.Add(string.Format("Immun0{0}", i), typeof(string));
            }

            dtb.PrimaryKey = new[] {dtb.Columns["ImmunizationID"]};

            if (dtb.Rows == null || dtb.Rows.Count == 0) return null;

            // Populate Imunization Date
            var qrImun = new PatientImmunizationQuery("b");
            qrImun.Where(qrImun.PatientID == patientID);
            qrImun.Select(qrImun.ImmunizationID, qrImun.ImmunizationNo, qrImun.ImmunizationDate);
            qrImun.OrderBy(qrImun.ImmunizationID.Ascending, qrImun.ImmunizationNo.Ascending);

            var dtbPatientImun = qrImun.LoadDataTable();
            var rowHd = dtb.Rows[0];
            foreach (DataRow row in dtbPatientImun.Rows)
            {
                if (rowHd["ImmunizationID"].ToString() != row["ImmunizationID"].ToString())
                    rowHd = dtb.Rows.Find(row["ImmunizationID"].ToString());

                if (rowHd == null) continue;

                for (int i = 1; i < 6; i++)
                {
                    if (i.Equals(row["ImmunizationNo"]))
                    {
                        rowHd[string.Format("Immun0{0}", i)] = "V";
                        break;
                    }
                }
            }

            return dtb;
        }

        private void PopulateBirtHistory(string patientID)
        {
            var pbr = new PatientBirthRecord();
            if (pbr.LoadByPrimaryKey(patientID))
            {
                txtasi.Value = pbr.AsiToMonthAge == null? string.Empty: Convert.ToString(pbr.AsiToMonthAge);
                txtDiet.Value = pbr.CurrentDiet;
                txtTengkurap.Value = pbr.AsiToMonthAge == null ? string.Empty : Convert.ToString(pbr.ProneAtMonthAge);
                txtDuduk.Value = pbr.SitAtMonthAge == null ? string.Empty : Convert.ToString(pbr.SitAtMonthAge);
                txtMerangkak.Value = pbr.CrawlAtMonthAge == null ? string.Empty : Convert.ToString(pbr.CrawlAtMonthAge);
                txtBerdiri.Value = pbr.StandUpAtMonthAge == null ? string.Empty : Convert.ToString(pbr.StandUpAtMonthAge);
                txtBerjalan.Value = pbr.WalkAtMonthAge == null ? string.Empty : Convert.ToString(pbr.WalkAtMonthAge);
                txtBicara36.Value = pbr.Speak3WordAtMonthAge == null ? string.Empty : Convert.ToString(pbr.Speak3WordAtMonthAge);
                txtBicaraKalimat.Value = pbr.Speak2SentAtMonthAge == null ? string.Empty : Convert.ToString(pbr.Speak2SentAtMonthAge);
                txtKelas.Value = pbr.SchoolClass;
                txtPrestasiKelas.Value = pbr.SchoolAchievement;
                txtTumbuh.Value = pbr.GrowthNotes;
            }
        }


        private void PopulatePhysicalExam(PatientAssessment asses, BusinessObject.Registration reg)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<KidPe>(asses.PhysicalExam);

                //<asp:ListItem Text="Mild" Value="Mild" Selected="True"></asp:ListItem>
                //   <asp:ListItem Text="Moderate" Value="Moderate"></asp:ListItem>
                //   <asp:ListItem Text="Severe" Value="Severe"></asp:ListItem>
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


            }
            catch (Exception)
            {
                //Nothing
            }


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

                var bw = VitalSign.LastVitalSignValue(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.BodyWeight, asesDateTime);
                var bh = VitalSign.LastVitalSignValue(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.BodyHeight, asesDateTime);


                var bmi = bw / ((bh / 100) * (bh / 100));
                txtBB.Value = bw.ToString();
                txtTB.Value = bh.ToString();
                txtIMT.Value = bmi.ToString();
                chkObesitas.Value = bmi >= 23;
            }
        }

    }
}