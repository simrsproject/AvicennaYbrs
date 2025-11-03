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
    public partial class Gigi : Telerik.Reporting.Report
    {
        public Gigi(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoOnly(this.pageHeader);

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
            txtRegistrationDate1.Value = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.Date);
            txtRegistrationTime1.Value = reg.RegistrationTime;
            txtHandleTime1.Value = Convert.ToDateTime(asses.AssessmentDateTime).ToString("HH:mm");
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

            PopulateTherapy(asses.RegistrationInfoMedicID);
            PopulateEducation(asses);

            var par = new Paramedic();
            if (par.LoadByPrimaryKey(reg.ParamedicID))
            {
                txtParamedicName.Value = par.ParamedicName;
            }

            PopulateOdontogram(asses.PatientID, asses.RegistrationNo);
        }

        private void PopulateOdontogram(string patientID, string registrationNo)
        {
            var odon = new PatientOdontogram();
            odon.LoadByPrimaryKey(patientID, registrationNo);

            txt1151.Value = odon.T1151Notes;
            txt1252.Value = odon.T1252Notes;
            txt1353.Value = odon.T1353Notes;
            txt1454.Value = odon.T1454Notes;
            txt1555.Value = odon.T1555Notes;
            txt16.Value = odon.T16Notes;
            txt17.Value = odon.T17Notes;
            txt18.Value = odon.T18Notes;
            txt6121.Value = odon.T6121Notes;
            txt6222.Value = odon.T6222Notes;
            txt6323.Value = odon.T6323Notes;
            txt6424.Value = odon.T6424Notes;
            txt6525.Value = odon.T6525Notes;
            txt26.Value = odon.T26Notes;
            txt27.Value = odon.T27Notes;
            txt28.Value = odon.T28Notes;
            txt48.Value = odon.T48Notes;
            txt47.Value = odon.T47Notes;
            txt46.Value = odon.T46Notes;
            txt4585.Value = odon.T4585Notes;
            txt4484.Value = odon.T4484Notes;
            txt4383.Value = odon.T4383Notes;
            txt4282.Value = odon.T4282Notes;
            txt4181.Value = odon.T4181Notes;
            txt38.Value = odon.T38Notes;
            txt37.Value = odon.T37Notes;
            txt36.Value = odon.T36Notes;
            txt7535.Value = odon.T7535Notes;
            txt7434.Value = odon.T7434Notes;
            txt7333.Value = odon.T7333Notes;
            txt7232.Value = odon.T7232Notes;
            txt7131.Value = odon.T7131Notes;

            t11.Value = odon.T11;
            t51.Value = odon.T51;
            t12.Value = odon.T12;
            t52.Value = odon.T52;
            t13.Value = odon.T13;
            t53.Value = odon.T53;
            t14.Value = odon.T14;
            t54.Value = odon.T54;
            t15.Value = odon.T15;
            t55.Value = odon.T55;
            t16.Value = odon.T16;
            t17.Value = odon.T17;
            t18.Value = odon.T18;
            t61.Value = odon.T61;
            t21.Value = odon.T21;
            t62.Value = odon.T62;
            t22.Value = odon.T22;
            t63.Value = odon.T63;
            t23.Value = odon.T23;
            t64.Value = odon.T64;
            t24.Value = odon.T24;
            t65.Value = odon.T65;
            t25.Value = odon.T25;
            t26.Value = odon.T26;
            t27.Value = odon.T27;
            t28.Value = odon.T28;
            t48.Value = odon.T48;
            t47.Value = odon.T47;
            t46.Value = odon.T46;
            t45.Value = odon.T45;
            t85.Value = odon.T85;
            t44.Value = odon.T44;
            t84.Value = odon.T84;
            t43.Value = odon.T43;
            t83.Value = odon.T83;
            t42.Value = odon.T42;
            t82.Value = odon.T82;
            t41.Value = odon.T41;
            t81.Value = odon.T81;
            t38.Value = odon.T38;
            t37.Value = odon.T37;
            t36.Value = odon.T36;
            t75.Value = odon.T75;
            t35.Value = odon.T35;
            t74.Value = odon.T74;
            t34.Value = odon.T34;
            t73.Value = odon.T73;
            t33.Value = odon.T33;
            t72.Value = odon.T72;
            t32.Value = odon.T32;
            t71.Value = odon.T71;
            t31.Value = odon.T31;
        }

        private void PopulateTherapy(string registrationInfoMedicID)
        {
            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(registrationInfoMedicID);
            txtTherapy.Value = rim.Info4;
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
                var pexam = JsonConvert.DeserializeObject<DentisPe>(asses.PhysicalExam);

                txtExtraOral.Value = pexam.ExtraOral;
                txtBibir.Value = pexam.IntraOral.Bibir;
                txtPalatum.Value = pexam.IntraOral.Palatum;
                txtLidah.Value = pexam.IntraOral.Lidah;
                txtDasarMulut.Value = pexam.IntraOral.DasarMulut;
                txtVestibulum.Value = pexam.IntraOral.Vestibulum;
                txtGinggiva.Value = pexam.IntraOral.Ginggiva;
                txtMukosa.Value = pexam.IntraOral.MukosaBukal;
                txtTonsil.Value = pexam.IntraOral.Tonsil;

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