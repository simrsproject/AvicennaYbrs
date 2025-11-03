using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.Finance.Receivable.PAC
{
    using BusinessObject;
    using System;
    using System.Data;

    /// <summary>
    /// Summary description for AR_InvoicingRpt.
    /// </summary>
    public partial class AR_InvoicingRpt : Telerik.Reporting.Report
    {
        public AR_InvoicingRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeLogo(pageHeader);
            PopulateHealthcareInfo();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            var query = new InvoicesQuery("a");
            var detail = new InvoicesItemQuery("b");
            var guar = new GuarantorQuery("c");
            var reg = new RegistrationQuery("d");
            var pat = new PatientQuery("e");

            query.Select
                (
                    query.InvoiceNo,
                    query.InvoiceDate,
                    query.InvoiceDueDate,
                    query.GuarantorID,
                    guar.GuarantorName,
                    @"<RTRIM(c.StreetName) + ' ' + RTRIM(c.District) + ' ' + RTRIM(c.County) AS Address1>",
                    @"<RTRIM(c.State) + ' ' + RTRIM(c.ZipCode) AS Address2>",
                    guar.ContactPerson,
                    guar.PhoneNo,
                    detail.RegistrationNo,
                    @"<RTRIM(e.FirstName) + ' ' + RTRIM(e.MiddleName) + ' ' + RTRIM(e.LastName) AS PatientName>",
                    detail.PaymentNo,
                    detail.PaymentDate,
                    detail.Amount,
                    detail.LastUpdateByUserID,
                    @"<GETDATE() AS PrintedDate>"
                );

            query.InnerJoin(detail).On(query.InvoiceNo == detail.InvoiceNo);
            query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);
            query.InnerJoin(reg).On(detail.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            query.Where
                (
                    query.InvoiceNo == printJobParameters[0].ValueString,
                    query.IsVoid == false,
                    query.IsApproved == true
                );

            var dtb = query.LoadDataTable();
            double total = 0D;
            foreach (DataRow row in dtb.Rows)
            {
                total += Convert.ToDouble(row["Amount"]);
            }

            if (dtb.Rows.Count > 0)
            {
                this.DataSource = dtb;
                txtUserName.Value = AppSession.Parameter.FinanceHead;
                txtAmountInWords.Value = (new Common.ConvertionToEnglish()).NumericToWords(Convert.ToDecimal(total));
            }
        }

        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
        }
    }
}