using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RSCH.RLib_Slip.Assessment.MCU
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for THT.
    /// </summary>
    public partial class THT : Telerik.Reporting.Report
    {
        public THT(string programID, PrintJobParameterCollection printJobParameters)
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

            txtKeluhan.Value = reg.Complaint;

            var par = new Paramedic();
            if (par.LoadByPrimaryKey(reg.ParamedicID))
            {
                txtParamedicName.Value = par.ParamedicName;
            }
        }
        private void PopulatePhysicalExam(string registrationInfoMedicID)
        {
            var asses = new PatientAssessment();
            asses.LoadByPrimaryKey(registrationInfoMedicID);

            if (!string.IsNullOrEmpty(asses.PhysicalExam))
            {
                var tht = JsonConvert.DeserializeObject<ThtMcu>(asses.PhysicalExam);
                txtDaunKn.Value = tht.DaunTelingaKanan;
                txtDaunKr.Value = tht.DaunTelingaKiri;
                txtLiangKn.Value = tht.LiangTelingaKanan;
                txtLiangKr.Value = tht.LiangTelingaKiri;
                txtTympaniKn.Value = tht.MembranTympaniKanan;
                txtTympaniKr.Value = tht.MembranTympaniKiri;
                txtAudioKn.Value = tht.AudiogramKanan;
                txtAudioKr.Value = tht.AudiogramKiri;

                //questTht.PopulateValue(tht.Tht);
            }
        }
              
    }
}