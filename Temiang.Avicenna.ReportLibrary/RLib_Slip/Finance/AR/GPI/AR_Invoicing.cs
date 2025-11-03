namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.AR.GPI
{
    using BusinessObject;
    using System;
    using System.Data;
    using Temiang.Avicenna.Common;

    public partial class AR_Invoicing : Telerik.Reporting.Report
    {
        public AR_Invoicing(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            PopulateHealthcareInfo();

            var query = new InvoicesQuery("a");
            var detail = new InvoicesItemQuery("b");
            var guar = new GuarantorQuery("c");
            var reg = new RegistrationQuery("d");
            var pat = new PatientQuery("e");
            var guargroup = new GuarantorQuery("f");

            query.Select
                (
                    query.InvoiceNo,
                    query.InvoiceDate,
                    query.InvoiceDueDate,
                    query.GuarantorID,
                    query.InvoiceTOP,
                    guar.GuarantorName,
                    guargroup.GuarantorName.As("GuarantorNm"),
                    @"<RTRIM(c.StreetName) + ' ' + RTRIM(c.District) + ' ' + RTRIM(c.County) AS Address1>",
                    @"<RTRIM(c.State) + ' ' + RTRIM ( ISNULL(c.ZipCode,'')) AS Address2>",
                    guar.ContactPerson,
                    guar.PhoneNo,
                    guar.FaxNo,
                    detail.RegistrationNo,
                    @"<RTRIM(e.FirstName) + ' ' + RTRIM(e.MiddleName) + ' ' + RTRIM(e.LastName) AS PatientName>",
                    detail.PaymentNo,
                    detail.PaymentDate,
                    detail.Amount,
                    detail.LastUpdateByUserID,
                    reg.SRRegistrationType,
                    @"<GETDATE() AS PrintedDate>",
                    @"<CASE WHEN d.SRRegistrationType = 'IPR' THEN d.DischargeDate ELSE d.RegistrationDate END AS DischargeDate>"
                );

            query.InnerJoin(detail).On(query.InvoiceNo == detail.InvoiceNo);
            query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);
            query.InnerJoin(reg).On(detail.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            query.LeftJoin(guargroup).On(reg.GuarantorID == guargroup.GuarantorID);
            query.Where
                (
                    query.InvoiceNo == printJobParameters[0].ValueString,
                    query.IsVoid == false,
                    query.IsApproved == true
                );

            var dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                this.DataSource = dtb;
                textBox38.Value = string.Format("{0:n0}", dtb.Rows.Count);
            }
            else
                textBox38.Value = "0";

            decimal total = 0;
            int invoiceTOP = 30;
            decimal RISum = 0;
            decimal RJSum = 0;

            foreach (DataRow row in dtb.Rows)
            {
                total += Convert.ToDecimal(row["Amount"]);
                invoiceTOP = Convert.ToInt32(row["InvoiceTOP"]);
                if (row["SRRegistrationType"].ToString() == "IPR")
                {
                    RISum += Convert.ToDecimal(row["Amount"]);
                }
                else
                {
                    RJSum += Convert.ToDecimal(row["Amount"]);
                }
            }

            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            TxtAmount.Value = string.Format("Rp. {0:n2}", (total));
        }

        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
            TxtHealthcareName.Value = healthcare.HealthcareName;
            textBox22.Value = healthcare.HealthcareName;
            string finance = AppParameter.GetParameterValue(AppParameter.ParameterItem.FinanceHead);
            txtFinance.Value = '(' + finance + ')';
            string BankAccNo = AppParameter.GetParameterValue(AppParameter.ParameterItem.BankAccNoForSlipBank);
            textBox34.Value = BankAccNo;
            string BankNameForSlipBank = AppParameter.GetParameterValue(AppParameter.ParameterItem.BankNameForSlipBank);
            textBox45.Value = BankNameForSlipBank;
        }
    }
}