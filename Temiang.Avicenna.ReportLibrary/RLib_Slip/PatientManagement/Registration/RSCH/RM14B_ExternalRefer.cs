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
    public partial class RM14B_ExternalRefer : Report
    {
        public RM14B_ExternalRefer(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogoAndTextBottom(this.reportHeaderSection1);


            var hc = Healthcare.GetHealthcare();
            txtHealthcareCity.Value = hc.City;
            txtPrintDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
            txtPrintTime.Value = DateTime.Now.ToString("HH:mm");
            

            var regNo = printJobParameters.FindByParameterName("p_RegistrationNo").ValueString;



            var referExt = new ReferExternal();
            if (!referExt.LoadByPrimaryKey(regNo))
                return;

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(regNo);

            var guar = new Guarantor();
            guar.LoadByPrimaryKey(reg.GuarantorID);
            var isBpjs = (guar.SRBusinessMethod ==
                          AppParameter.GetParameterValue(AppParameter.ParameterItem.BusinessMethodBpjs));
            if (isBpjs)
                txtBpjsNo.Value = string.Format("No: {0}", reg.GuarantorCardNo);
            else
            {
                if (guar.SRGuarantorType ==
                    AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeSelf))
                    txtOtherGurantorCardNo.Value =
                        string.Format("Peserta asuransi kesehatan lain: {0}", guar.GuarantorName);
            }


            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            txtPatientName.Value = pat.PatientName;
            txtPatientName2.Value = pat.PatientName;
            txtMedicalNo.Value = pat.MedicalNo;
            txtAge.Value = string.Format("{0}T/Y {1}B/M ({2}/{3})",
                reg.AgeInYear, reg.AgeInMonth, pat.Sex == "M" ? "L" : "P", pat.Sex);

            switch (referExt.SRReferReason)
            {
                case "REFRES01":
                    chkREFRES01.Value = true;
                    break;
                case "REFRES02":
                    chkREFRES02.Value = true;
                    break;
                case "REFRES03":
                    chkREFRES03.Value = true;
                    break;
                case "REFRES99":
                    chkREFRES99.Value = true;
                    break;
            }

            var diagName = string.Empty;
            var epColl = new Temiang.Avicenna.BusinessObject.EpisodeDiagnoseCollection();
            epColl.Query.Where(epColl.Query.RegistrationNo == regNo,
                epColl.Query.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain, epColl.Query.IsVoid == false);
            epColl.LoadAll();
            if (epColl.Count > 0)
            {
                foreach (var ep in epColl)
                {
                    if (diagName == string.Empty)
                        diagName = ep.DiagnosisText;
                    else
                        diagName = diagName + "; " + ep.DiagnosisText;
                }
            }
            txtDiagnosa.Value = diagName;

            var medsum = new MedicalDischargeSummary();
            medsum.LoadByPrimaryKey(regNo);

            //txtDiagnosa.Value = medsum.FinalDiagnoseName1;

            txtChiefComplaint.Value =
                string.Concat(medsum.ChiefComplaint, Environment.NewLine, medsum.HistOfPresentIllness);
            txtExamAndAncillary.Value = string.Concat(medsum.PhysicalExam, Environment.NewLine, medsum.AncillaryExam);
            txtMedicationProcedure.Value = string.Concat(medsum.MedicalProcedures, Environment.NewLine, medsum.Medications);
            txtReferReasonOther.Value = referExt.ReferReasonOther;

            var referral = new Referral();
            referral.LoadByPrimaryKey(referExt.ReferralID);
            txtReferralName.Value = referral.ReferralName;

            txtReferralAgreedBy.Value = referExt.ReferralAgreedBy;
            if (referExt.ReferralAgreedTime != null)
                txtReferralAgreedTime.Value = referExt.ReferralAgreedTime.Value.ToString("HH:mm");

            txtMainParamedicName.Value = ParamedicTeam.DPJP(regNo).ParamedicName; 
        }
    }
}