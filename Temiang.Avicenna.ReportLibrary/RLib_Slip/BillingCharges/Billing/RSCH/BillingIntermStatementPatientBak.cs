using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSCH
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
    public partial class BillingIntermStatementPatientBak : Telerik.Reporting.Report
    {
        public BillingIntermStatementPatientBak(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogo(this.reportHeaderSection1);

            string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = new string[1];
            if (registrationNo.Contains(","))
                registrationNoList = registrationNo.Split(',');
            else
                registrationNoList.SetValue(registrationNo, 0);


            var regs = new RegistrationCollection();
            regs.Query.Where(regs.Query.RegistrationNo.In(registrationNoList));
            regs.LoadAll();

            decimal adm = regs.Sum(r => r.AdministrationAmount ?? 0);
            //decimal total = table.AsEnumerable().Sum(t => t.Field<decimal>("Total")) +
            //                regs.Sum(r => r.AdministrationAmount ?? 0);
            //decimal tpatient = table.AsEnumerable().Sum(t => t.Field<decimal>("PatientAmount"));
            //decimal tguarantor = table.AsEnumerable().Sum(t => t.Field<decimal>("GuarantorAmount"));

            //textBox31.Value = string.Format("{0:N2}", adm);
            //textBox33.Value = string.Format("{0:N2}", total);
            textBox41.Value = string.Format("{0:N2}", Common.Helper.Payment.GetTotalDownPaymentOnly(registrationNoList) - Common.Helper.Payment.GetTotalDownPaymentReturn(registrationNoList));
            //textBox37.Value = string.Format("{0:N2}", total - (Common.Helper.Payment.GetTotalDownPaymentOnly(registrationNoList) - Common.Helper.Payment.GetTotalDownPaymentReturn(registrationNoList)));
            textBox39.Value = printJobParameters.FindByParameterName("UserName").ValueString;



            var regis = new Registration();
            if (regis.LoadByPrimaryKey(printJobParameters.FindByParameterName("RegNo").ValueString))
            {
                var p = new Patient();
                p.LoadByPrimaryKey(regis.PatientID);
                textBox11.Value = p.PatientName;
                textBox12.Value = p.MedicalNo;
            }

            var guar = new Guarantor();
            if (guar.LoadByPrimaryKey(regis.GuarantorID))
            {
                var guarhd = new Guarantor();
                if (guarhd.LoadByPrimaryKey(guar.GuarantorHeaderID))
                {
                    if (guarhd.GuarantorName.ToUpper().Contains("KUMPULAN"))
                    {
                        textBox9.Value = guar.GuarantorName;
                    }
                    else
                    {
                        textBox9.Value = guarhd.GuarantorName;
                    }
                }
            }

            textBox48.Value = string.Format("{0:dd-MMM-yyyy}", regis.RegistrationDate);
            textBox54.Value = string.Format("{0:dd-MMM-yyyy}", regis.DischargeDate);
            textBox10.Value = printJobParameters.FindByParameterName("RegNo").ValueString;

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            this.DataSource = DataSource;
            //this.DataSource = table;
        }
    }
}