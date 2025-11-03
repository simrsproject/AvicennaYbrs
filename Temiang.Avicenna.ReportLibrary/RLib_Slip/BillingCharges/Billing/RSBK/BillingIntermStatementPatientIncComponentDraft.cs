using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSBK
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Summary description for BillingStatementDetail.
    /// </summary>
    public partial class BillingIntermStatementPatientIncComponentDraft : Telerik.Reporting.Report
    {
        public BillingIntermStatementPatientIncComponentDraft(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoOnly(this.pageHeaderSection1);

            string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = new string[1];
            if (registrationNo.Contains(","))
                registrationNoList = registrationNo.Split(',');
            else
                registrationNoList.SetValue(registrationNo, 0);

            var regs = new RegistrationCollection();
            regs.Query.Where(regs.Query.RegistrationNo.In(registrationNoList));
            regs.LoadAll();

            var regis = new Registration();
            regis.LoadByPrimaryKey(printJobParameters.FindByParameterName("RegNo").ValueString);

            string srRegType = regis.SRRegistrationType;


            var rrp = new RegistrationResponsiblePerson();
            rrp.LoadByPrimaryKey(printJobParameters.FindByParameterName("RegNo").ValueString);
            textBox54.Value = rrp.NameOfTheResponsible;
            var p = new Patient();
            var sal = new AppStandardReferenceItem();
            p.LoadByPrimaryKey(regis.PatientID);
            if (sal.LoadByPrimaryKey("Salutation", p.SRSalutation))
                textBox10.Value = sal.ItemName + " " + p.PatientName;
            else
                textBox10.Value = p.PatientName;
            textBox128.Value = p.Address;


            textBox12.Value = p.MedicalNo;


            // penjamin
            var guar = new Guarantor();
            if (guar.LoadByPrimaryKey(regis.GuarantorID))
            {
                var guarhd = new Guarantor();
                if (guarhd.LoadByPrimaryKey(guar.GuarantorHeaderID))
                {
                    textBox120.Value = guar.GuarantorName;
                    textBox30.Value = string.Format("Admin RI {0} % (Rp) :", regis.SRRegistrationType == "IPR" ? guar.AdminPercentage : guar.AdminPercentageOp);
                }
            }

            textBox11.Value = regis.RegistrationNo;



            textBox123.Value = string.Format("{0:dd-MMM-yyyy}", regis.DischargeDate.HasValue ? regis.DischargeDate.Value : (srRegType != "IPR" ? regis.RegistrationDate : DateTime.Now));


            // paramedic
            var dr = new Paramedic();
            if (regis.ParamedicID != null)
            {
                if (dr.LoadByPrimaryKey(regis.ParamedicID))
                {
                    textBox117.Value = dr.ParamedicName;
                }
            }

            // ICD
            var icdColl = new EpisodeDiagnoseCollection();
            icdColl.Query.Where(icdColl.Query.RegistrationNo == regis.RegistrationNo,
                icdColl.Query.SRDiagnoseType == "DiagnoseType-001", icdColl.Query.IsVoid == false);
            if (icdColl.LoadAll())
            {
                textBox48.Value = icdColl.First().DiagnosisText;
            }

            var healthCare = Healthcare.GetHealthcare();

            // NPWP
            textBox9.Value = healthCare.NPWP;
            textBox110.Value = healthCare.HealthcareName;

            textBox64.Value = healthCare.HealthcareName;
            textBox132.Value = healthCare.AddressLine1 + (srRegType == "IPR" ? " " + healthCare.City : "");
            textBox133.Value = srRegType == "IPR" ? "Telp. " + healthCare.PhoneNo + " Fax " + healthCare.FaxNo : healthCare.City;
            var Email = string.Empty;
            var EmParam = new AppParameter();
            if (EmParam.LoadByPrimaryKey("HealthcareFinanceEmailAddr"))
                Email = EmParam.ParameterValue;
            textBox139.Value = srRegType == "IPR" ? "Email: " + Email : "Telp. " + healthCare.PhoneNo + " Fax " + healthCare.FaxNo;

            //
            var dtb = new ReportDataSource().GetDataTable(programID, printJobParameters);
            this.DataSource = dtb;
            //this.DataSource = table;

            decimal sisa = 0;
            if (dtb.Rows.Count > 0)
            {
                sisa = Convert.ToDecimal(dtb.Rows[0]["SisaRS"]);
                //var sisa = System.Convert.ToDecimal(textBox87.Value);
                var PersonalARReplacementName = dtb.Rows[0]["PersonalARReplacementName"].ToString();
                if (!string.IsNullOrEmpty(PersonalARReplacementName))
                {
                    textBox120.Value = PersonalARReplacementName;
                }
            }

            textBox83.Value = (new Common.Convertion()).NumericToWords(sisa);
        }
    }
}