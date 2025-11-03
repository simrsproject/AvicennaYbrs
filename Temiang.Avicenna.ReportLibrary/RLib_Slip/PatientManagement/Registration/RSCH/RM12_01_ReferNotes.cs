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
    public partial class RM12_01_ReferNotes : Report
    {
        public RM12_01_ReferNotes(string programID, PrintJobParameterCollection printJobParameters)
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



            var refer = new ParamedicConsultRefer();
            refer.LoadByPrimaryKey(consultReferNo);

            var medic = new Paramedic();
            medic.LoadByPrimaryKey(refer.ToParamedicID);
            txtToParamedicName.Value = medic.ParamedicName;

            txtToSpecialty.Value = StandardReference.GetItemName(AppEnum.StandardReference.Specialty, medic.SRSpecialty);
            txtNotes.Value = string.Concat(refer.ActionExamTreatment, Environment.NewLine, refer.Notes);

            chkParamedicConsultType01.Value = refer.SRParamedicConsultType == "01";
            chkParamedicConsultType02.Value = refer.SRParamedicConsultType == "02";
            chkParamedicConsultType03.Value = refer.SRParamedicConsultType == "03";

            // Diagnose
            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                var epDiags = new RegistrationInfoMedicDiagnoseCollection();
                epDiags.Query.Where(epDiags.Query.RegistrationNo == regNo, epDiags.Query.SRDiagnoseType == "DiagnoseType-001");
                epDiags.Query.OrderBy(epDiags.Query.SequenceNo.Descending);
                epDiags.Query.es.Top = 1;
                epDiags.LoadAll();

                if (epDiags.Count > 0)
                {
                    txtDiagnosa.Value = string.Format(": {0}", epDiags[0].DiagnosisText);
                }
                else
                {
                    txtDiagnosa.Value = ":";
                }
            }
            else
            {
                var epDiags = new EpisodeDiagnoseCollection();
                epDiags.Query.Where(epDiags.Query.RegistrationNo == regNo, epDiags.Query.SRDiagnoseType == "DiagnoseType-001");
                epDiags.Query.OrderBy(epDiags.Query.SequenceNo.Descending);
                epDiags.Query.es.Top = 1;
                epDiags.LoadAll();

                if (epDiags.Count > 0)
                {
                    txtDiagnosa.Value = string.Format(": {0}", epDiags[0].DiagnosisText);
                }
                else
                {
                    txtDiagnosa.Value = ":";
                }
            }
            medic = new Paramedic();
            medic.LoadByPrimaryKey(refer.ParamedicID);
            txtFromParamedicName.Value = string.Format(": {0}", medic.ParamedicName);
            txtFromSpecialty.Value = string.Format(": {0}", StandardReference.GetItemName(AppEnum.StandardReference.Specialty, medic.SRSpecialty));

            txtReferDate.Value = string.Format(": {0}", refer.ConsultDateTime.Value.ToString("dd-MMM-yyyy"));
            txtReferTime.Value = string.Format(": {0} WIB", refer.ConsultDateTime.Value.ToString("HH:mm"));
        }
    }
}