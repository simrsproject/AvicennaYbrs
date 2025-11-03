using System;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.Registration.RSCH
{

    /// <summary>
    /// Summary description for ReferNotes.
    /// </summary>
    public partial class RM12_02_ReferNotes : Report
    {
        public RM12_02_ReferNotes(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogoAndTextBottom(this.pageHeader);

            var regNo = printJobParameters.FindByParameterName("RegistrationNo").ValueString;
            var consultReferNo = printJobParameters.FindByParameterName("ConsultReferNo").ValueString;

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(regNo);

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            txtPatientName.Value = pat.PatientName;
            txtMedicalNo.Value = pat.MedicalNo;
            txtBirthDateAge.Value = string.Format("{0} - {1}Y {2}M",
                Convert.ToDateTime(pat.DateOfBirth).ToString(AppConstant.DisplayFormat.Date), reg.AgeInYear,
                reg.AgeInMonth);

            var refer = new Temiang.Avicenna.BusinessObject.ParamedicConsultRefer();
            refer.LoadByPrimaryKey(consultReferNo);

            txtNotes.Value = refer.Answer;

            var medic = new Paramedic();
            medic.LoadByPrimaryKey(refer.ToParamedicID);
            txtToParamedicName.Value = string.Format(": {0}",medic.ParamedicName);
            txtToSpecialty.Value = string.Format(": {0}", StandardReference.GetItemName(AppEnum.StandardReference.Specialty, medic.SRSpecialty));

            txtAnswerDate.Value = string.Format(": {0}", refer.AnswerDateTime.Value.ToString("dd-MMM-yyyy"));
            txtAnswerTime.Value = string.Format(": {0} WIB", refer.AnswerDateTime.Value.ToString("HH:mm"));

        }
    }
}