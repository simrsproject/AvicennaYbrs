using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RSCH.RLib_Slip.Assessment
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
    public partial class Jantung : Telerik.Reporting.Report
    {
        public Jantung(string programID, PrintJobParameterCollection printJobParameters)
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
                Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.Date), reg.AgeInYear,
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

            PopulatePhysicalExam(asses);

            txtDiagnosa.Value = asses.Diagnose;

            PopulateEducation(asses);
            PopulateTherapy(asses.RegistrationInfoMedicID);
            var par = new Paramedic();
            if (par.LoadByPrimaryKey(reg.ParamedicID))
            {
                txtParamedicName.Value = par.ParamedicName;
            }
        }

        private void PopulateEducation(PatientAssessment asses)
        {
            // Get Education
            if (string.IsNullOrEmpty(asses.Education)) return;

            // Convert to class w json
            try
            {
                var edus = JsonConvert.DeserializeObject<Educations>(asses.Education);
                if (edus.Items == null) return;
                foreach (var edu in edus.Items)
                {
                    if (edu.ID == "001")
                        chkEdu001.Value = true;
                    else if (edu.ID == "002")
                        chkEdu002.Value = true;
                    else if (edu.ID == "003")
                        chkEdu003.Value = true;
                    else if (edu.ID == "004")
                        chkEdu004.Value = true;
                    else if (edu.ID == "005")
                        chkEdu005.Value = true;
                    else if (edu.ID == "006")
                        chkEdu006.Value = true;
                    else if (edu.ID == "999")
                    {
                        chkEdu999.Value = true;
                        txtEdu999.Value = edu.Notes;
                    }
                }
            }
            catch (Exception)
            {
                // Nothing
            }
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

                txtJantung.Value = pexam.Jantung;
                txtInspeksi.Value = pexam.Inspeksi;
                txtPalpasi.Value = pexam.Palpasi;
                txtPalpasi.Value = pexam.Palpasi;
                chkLipPlus.Value = pexam.Lifting == "+";
                chkLipMin.Value = pexam.Lifting != "+";
                txtS1S2.Value = pexam.AuscultationS1S2;
                txtMurmur.Value = pexam.Murmur;
                txtParu.Value = pexam.Paru;
                txtAbdomen.Value = pexam.Abdomen;

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
        private void PopulateTherapy(string registrationInfoMedicID)
        {
            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(registrationInfoMedicID);
            txtTherapy.Value = rim.Info4;
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