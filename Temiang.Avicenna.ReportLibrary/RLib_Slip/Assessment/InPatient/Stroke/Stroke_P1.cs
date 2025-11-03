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
    /// Summary description for Stroke_P1.
    /// </summary>
    public partial class Stroke_P1 : Telerik.Reporting.Report
    {
        public Stroke_P1(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
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

            txtBirthDateAge.Value = string.Format("{0} - {1}Y {2}M",
                Convert.ToDateTime(pat.DateOfBirth).ToString(AppConstant.DisplayFormat.Date), reg.AgeInYear,
                reg.AgeInMonth);

            txtRegistrationDate.Value = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.Date);
            txtRegistrationTime.Value = reg.RegistrationTime;
            txtHandleTime.Value = Convert.ToDateTime(asses.AssessmentDateTime).ToString("HH:mm");
            PopulateRiwayatPenyakitDahulu(patientID);
            
            txtMedikamentosa.Value = asses.Medikamentosa;

            PopulatePhysicalExam(asses);


        }
    
        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<StrokePe>(asses.PhysicalExam);

                //<asp:ListItem Text="Mild" Value="Mild" Selected="True"></asp:ListItem>
                //   <asp:ListItem Text="Moderate" Value="Moderate"></asp:ListItem>
                //   <asp:ListItem Text="Severe" Value="Severe"></asp:ListItem>

                var gcs = pexam.Consciousness;

            }
            catch (Exception)
            {
                //Nothing
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
                    //case "012":
                    //    chkRpdTrauma.Value = true;
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