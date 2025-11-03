using System;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
namespace Temiang.Avicenna.ReportLibrary.RSCH.RLib_Slip.Assessment
{


    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class Syaraf : Telerik.Reporting.Report
    {
        public Syaraf(string programID, PrintJobParameterCollection printJobParameters)
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
                Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.Date), reg.AgeInYear,
                reg.AgeInMonth);

            txtRegistrationDate.Value = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.Date);
            txtRegistrationTime.Value = reg.RegistrationTime;
            txtHandleTime.Value = Convert.ToDateTime(asses.AssessmentDateTime).ToString("HH:mm");

            PopulatePhysicalExam(asses);

            txtDiagnosa.Value = asses.Diagnose;
            PopulateTherapy(asses.RegistrationInfoMedicID);

            PopulateEducation(asses);

            var par = new Paramedic();
            if (par.LoadByPrimaryKey(reg.ParamedicID))
            {
                txtParamedicName.Value = par.ParamedicName;
            }
        }
        private void PopulateTherapy(string registrationInfoMedicID)
        {
            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(registrationInfoMedicID);
            txtTherapy.Value = rim.Info4;
        }

        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<NeurologiPe>(asses.PhysicalExam);
                txtNeurologis.Value = pexam.Neurologis;
            }
            catch (Exception)
            {
                //Nothing
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

    }
}