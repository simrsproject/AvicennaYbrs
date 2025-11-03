using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.Finance.Receivable.Muhaya
{
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Summary description for AR_InvoicingBCARpt.
    /// </summary>
    public partial class AR_InvoicingBCARpt : Telerik.Reporting.Report
    {
        public AR_InvoicingBCARpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

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
                    pat.Company,
                    reg.GuarantorCardNo,
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
            if (dtb.Rows.Count > 0)
            {
                this.DataSource = dtb;
                var user = new AppUser();
                user.LoadByPrimaryKey(dtb.Rows[0]["LastUpdateByUserID"].ToString());
                txtUserName.Value = user.UserName;
                txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(dtb.AsEnumerable().Select(d=>d.Field<decimal>("Amount")).Sum());
                txtFinanceHead.Value = AppSession.Parameter.FinanceHead;
            }
        }

        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
        }
    }
}