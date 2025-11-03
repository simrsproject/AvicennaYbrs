//TODO: Riwayat Tumbuh kembang
//TODO: Riwayat Imunisasi

using System.Data;
using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class AwalRawatJalanAnak : Telerik.Reporting.Report
    {
        public AwalRawatJalanAnak(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoAndTextBottom(this.pageHeader);

            var patientID = printJobParameters.FindByParameterName("PatientID").ValueString;
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;

            var pat = new Patient();
            pat.LoadByPrimaryKey(patientID);
            txtPatientName.Value = pat.PatientName;
            txtMedicalNo.Value = pat.MedicalNo;

            var asses = new PatientAssessment();
            asses.LoadByPrimaryKey(rimid);

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(asses.RegistrationNo);
            txtBirthDateAge.Value = string.Format("{0} - {1}Y {2}M",
                Convert.ToDateTime(pat.DateOfBirth).ToString(AppConstant.DisplayFormat.Date), reg.AgeInYear,
                reg.AgeInMonth);

            txtRegistrationDate.Value = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.Date);
            txtRegistrationTime.Value = reg.RegistrationTime;
            txtHandleTime.Value = Convert.ToDateTime(asses.AssessmentDateTime).ToString("HH:mm");
            chkAlloanamnesis.Value = !asses.IsAutoAnamnesis ?? false;
            chkIsAutoanamnesis.Value = asses.IsAutoAnamnesis ?? false;
            txtHpi.Value = asses.Hpi;
            txtChiefComplaint.Value = reg.Complaint;

            PopulateRiwayatPenyakitDahulu(patientID);

            PopulateRiwayatPenyakitKeluarga(patientID);

            PopulateAllergy(patientID);

            txtMedikamentosa.Value = asses.Medikamentosa;

            PopulateBirtHIstory(asses.PatientID);

            PopulateImmunizationHist(asses.PatientID);

            PopulatePhysicalExam(asses);

            txtDiagnosa.Value = asses.Diagnose;

            var par = new Paramedic();
            if (par.LoadByPrimaryKey(reg.ParamedicID))
            {
                txtParamedicName.Value = par.ParamedicName;
            }

            txtTherapy.Value = asses.Therapy;
        }

        private void PopulateImmunizationHist(string patientID)
        {
            var dtb = GetImmunizationHist(patientID);
            if (dtb==null) return;

            tblImmunization.DataSource = dtb;
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

        private void PopulateBirtHIstory(string patientID)
        {
            var pbr = new PatientBirthRecord();
            if (pbr.LoadByPrimaryKey(patientID))
            {
                chkSpontan.Value = pbr.BirthMethod == "SN";
                chkSC.Value = pbr.BirthMethod == "SC";
                txtScIndikasi.Value = pbr.BirthMethodScIndication;
                 
                txtAnakKe.Value = Convert.ToString(pbr.ChildNumber);
                txtDariBersaudara.Value = Convert.ToString(pbr.ChildNumberFrom);
                txtPanjangLahir.Value = Convert.ToString(pbr.Length);
                txtBBLahir.Value = Convert.ToString(pbr.Weight);

                txtasi.Value = pbr.AsiToMonthAge == null ? string.Empty : Convert.ToString(pbr.AsiToMonthAge);
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

                chkGrmAda.Value = pexam.StatusNeurogis.Grm;
                chkGrmTidak.Value = !pexam.StatusNeurogis.Grm;

                chkFisiologisNormal.Value=!pexam.StatusNeurogis.Fisiologis.IsAbNormal;
                chkFisiologisAbnormal.Value = pexam.StatusNeurogis.Fisiologis.IsAbNormal;
                txtFisiologis.Value = pexam.StatusNeurogis.Fisiologis.Notes;

                chkPatologisAda.Value = pexam.StatusNeurogis.Patologis;
                chkPatologisTidak.Value = !pexam.StatusNeurogis.Patologis;

                chkMotorikNormal.Value = !pexam.StatusNeurogis.Motorik.IsAbNormal;
                chkMotorikAbnormal.Value = pexam.StatusNeurogis.Motorik.IsAbNormal;
                txtMotorik.Value = pexam.StatusNeurogis.Motorik.Notes;

                if (asses.AssessmentDateTime != null)
                {
                    var asesDateTime = asses.AssessmentDateTime.Value;
                    txtTekananDarah.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, string.Empty,
                            VitalSign.VitalSignEnum.BloodPressure, asesDateTime);

                    txtNadi.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, string.Empty,
                        VitalSign.VitalSignEnum.HeartRate, asses.AssessmentDateTime.Value);

                    txtPernafasan.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, string.Empty,
                        VitalSign.VitalSignEnum.RespiratoryRate, asesDateTime);

                    txtSuhu.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, string.Empty,
                        VitalSign.VitalSignEnum.Temperature, asesDateTime);

                    txtSkorNyeri.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, string.Empty,
                        VitalSign.VitalSignEnum.PainScale, asesDateTime);

                    var bw = VitalSign.LastVitalSignValue(asses.RegistrationNo, string.Empty,
                        VitalSign.VitalSignEnum.BodyWeight, asesDateTime);
                    var bh = VitalSign.LastVitalSignValue(asses.RegistrationNo, string.Empty,
                        VitalSign.VitalSignEnum.BodyHeight, asesDateTime);


                    var bmi = bw / ((bh / 100) * (bh / 100));
                    txtBB.Value = bw.ToString();
                    txtTB.Value = bh.ToString();
                    txtIMT.Value = bmi.ToString();
                    chkObesitas.Value = bmi >= 23;
                }

            }
            catch (Exception)
            {
                //Nothing
            }
        }

        private void PopulateAllergy(string patientID)
        {
            var allgs = new PatientAllergyCollection();
            allgs.Query.Where(allgs.Query.PatientID == patientID,
                              allgs.Query.Or(allgs.Query.Allergen.Like("%DRUG%"), allgs.Query.Allergen.Like("%FOOD%")));
            allgs.LoadAll();
            var allergy = string.Empty;
            foreach (PatientAllergy allg in allgs)
            {
                allergy = string.Concat(allergy, ", ", allg.DescAndReaction);
            }
            if (!string.IsNullOrEmpty(allergy))
                allergy = allergy.Substring(2);

            txtAllergy.Value = allergy;
        }

        private void PopulateRiwayatPenyakitKeluarga(string patientID)
        {
            // Update value
            var famhColl = new BusinessObject.FamilyMedicalHistoryCollection();
            famhColl.Query.Where(famhColl.Query.PatientID == patientID);
            famhColl.LoadAll();
            foreach (var famh in famhColl)
            {
                switch (famh.SRMedicalDisease)
                {
                    case "001":
                        chk5Hypertensi.Value = true;
                        break;
                    case "002":
                        chk5Jantung.Value = true;
                        break;
                    case "003":
                        chk5Tumor.Value = true;
                        break;
                    case "004":
                        chk5Hepatitis.Value = true;
                        break;
                    case "005":
                        chk5TBParu.Value = true;
                        break;
                    case "006":
                        chk5Struma.Value = true;
                        break;
                    case "007":
                        chk5DM.Value = true;
                        break;
                    case "008":
                        chk5TBParu.Value = true;
                        break;
                    case "009":
                        chk5KelainanDarah.Value = true;
                        break;
                    case "010":
                        chk5Ginjal.Value = true;
                        break;
                    case "011":
                        chk5Stroke.Value = true;
                        break;
                    case "014":
                        chk5Lain.Value = true;
                        txt5Lain.Value = famh.Notes;
                        break;
                }
            }
        }

        private void PopulateRiwayatPenyakitDahulu(string patientID)
        {
            var pmhColl = new BusinessObject.PastMedicalHistoryCollection();
            pmhColl.Query.Where(pmhColl.Query.PatientID == patientID);
            pmhColl.LoadAll();
            foreach (var pmh in pmhColl)
            {
                switch (pmh.SRMedicalDisease)
                {
                    case "001":
                        chkRpdHypertensi.Value = true;
                        break;
                    case "002":
                        chkRpdJantung.Value = true;
                        break;
                    case "003":
                        chkRpdTumor.Value = true;
                        break;
                    case "004":
                        chkRpdHepatitis.Value = true;
                        break;
                    case "005":
                        chkRpdTBParu.Value = true;
                        break;
                    case "006":
                        chkRpdStruma.Value = true;
                        break;
                    case "007":
                        chkRpdDM.Value = true;
                        break;
                    case "008":
                        chkRpdAsma.Value = true;
                        break;
                    case "009":
                        chkRpdKelainanDarah.Value = true;
                        break;
                    case "010":
                        chkRpdGinjal.Value = true;
                        break;
                    case "011":
                        chkRpdStroke.Value = true;
                        break;
                    case "012":
                        chkRpdTrauma.Value = true;
                        break;
                    case "014":
                        chkRpdLain.Value = true;
                        txtRpdLain.Value = pmh.Notes;
                        break;
                }
            }
        }
    }
}