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
    /// Summary description for Nursing_P1.
    /// </summary>
    public partial class Nursing_P1 : Telerik.Reporting.Report
    {
        public Nursing_P1(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg ,PatientAssessment asses)
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

            PopulateRiwayatPenyakitDahulu(patientID);

            PopulateRiwayatPenyakitKeluarga(patientID);

            PopulateAllergy(patientID);

            txtMedikamentosa.Value = asses.Medikamentosa;

            PopulatePhysicalExam(asses);

            var surgicalHist = new PastSurgicalHistory();
            surgicalHist.LoadByPrimaryKey(patientID);
            txtSurgery.Value = surgicalHist.SurgicalHistory;
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
                chkKeadaanRingan.Value = pexam.Condition == "Mild";
                chkKeadaanSedang.Value = pexam.Condition == "Moderate";
                chkKeadaanBerat.Value = pexam.Condition == "Severe";

                var gcs = pexam.Consciousness;
                txtConsciousness.Value = gcs.ConsciousnessDescription;
                txtGcsEye.Value = gcs.Eye.Score.ToString();
                txtGcsMotor.Value = gcs.Motor.Score.ToString();
                txtGcsVerbal.Value = gcs.Verbal.Score.ToString();


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