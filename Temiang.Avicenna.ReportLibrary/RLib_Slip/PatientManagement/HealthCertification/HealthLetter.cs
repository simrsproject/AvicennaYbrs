using System;
using System.Linq;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Telerik.Reporting.Drawing;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.HealthCertification
{
    public partial class HealthLetter : Report
    {
        public HealthLetter(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            // Logo
            picHealthcareLogo.MimeType = "image/bmp";
            picHealthcareLogo.Sizing = ImageSizeMode.Stretch;
            picHealthcareLogo.Value =
                Helper.ResourceLogo(AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial));

            var registrationNo = printJobParameters[0].ValueString;
            var paramedicID = printJobParameters[1].ValueString;
            var letterType = "HL";


            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(registrationNo);
            var umur = string.Format("{0} tahun {1} bulan", reg.AgeInYear, reg.AgeInMonth);

            var pat = new BusinessObject.Patient();
            pat.LoadByPrimaryKey(reg.PatientID);
            var stri = new AppStandardReferenceItem();
            stri.LoadByPrimaryKey("Title", pat.SRSalutation);
            txtNama.Value = string.Format(": {0} {1} / {2}, umur : {3}", stri.ItemName, pat.PatientName,
                pat.Sex == "M" ? "L" : "P", umur);

            var letter = new Temiang.Avicenna.BusinessObject.SickLetter();
            letter.LoadByPrimaryKey(registrationNo, paramedicID, letterType);
            txtDes.Value = string.Format(": {0}", letter.Notes);

            txtDate.Value = string.Format(": {0}, telah diperiksa kesehatan fisik",
                Convert.ToDateTime(reg.RegistrationDate).ToString("dd-MMM-yyyy"));

            // Berat Badan
            var phrl = new PatientHealthRecordLine();
            var phrlq = new PatientHealthRecordLineQuery();
            phrlq.es.Top = 1;
            phrlq.Where(phrlq.RegistrationNo == reg.RegistrationNo, phrlq.QuestionID == "GEN.SGN.02"); // BB
            phrl.Load(phrlq);
            txtBeratBadan.Value = string.Format(": {0:n1} {1}", phrl.QuestionAnswerNum, phrl.QuestionAnswerSuffix);

            // Tinggi Badan
            phrl = new PatientHealthRecordLine();
            phrlq = new PatientHealthRecordLineQuery();
            phrlq.es.Top = 1;
            phrlq.Where(phrlq.RegistrationNo == reg.RegistrationNo, phrlq.QuestionID == "GEN.SGN.01"); // TB
            phrl.Load(phrlq);
            txtTinggiBadan.Value = string.Format(": {0:n0} {1}", phrl.QuestionAnswerNum, phrl.QuestionAnswerSuffix);

            // Tekanan Darah
            phrl = new PatientHealthRecordLine();
            phrlq = new PatientHealthRecordLineQuery();
            phrlq.es.Top = 1;
            phrlq.Where(phrlq.RegistrationNo == reg.RegistrationNo, phrlq.QuestionID == "VIT.SGN.01"); // Sistolic
            phrl.Load(phrlq);
            var sistolic = phrl.QuestionAnswerNum;

            phrl = new PatientHealthRecordLine();
            phrlq = new PatientHealthRecordLineQuery();
            phrlq.es.Top = 1;
            phrlq.Where(phrlq.RegistrationNo == reg.RegistrationNo, phrlq.QuestionID == "VIT.SGN.02"); // Diastolic
            phrl.Load(phrlq);
            txtTekananDarah.Value = string.Format(": {0:n0} / {1:n0} {2}", sistolic, phrl.QuestionAnswerNum,
                phrl.QuestionAnswerSuffix);


            var healthcare = Healthcare.GetHealthcare();
            txtHealthcareInfo.Value = healthcare.ReportTitle();
            txtCityAndDate.Value = healthcare.City + ", " + string.Format("{0:dd-MMM-yyyy}", DateTime.Now);

            var par = new Paramedic();
            par.LoadByPrimaryKey(paramedicID);
            txtParamedicName.Value = string.Format("( {0} )", par.ParamedicName);
        }
    }
}