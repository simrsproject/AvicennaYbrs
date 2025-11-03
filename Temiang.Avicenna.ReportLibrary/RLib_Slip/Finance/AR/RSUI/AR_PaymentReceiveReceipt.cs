using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Finance.AR.RSUI
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;
    using Temiang.Avicenna.Common;
    /// <summary>
    /// Summary description for AR_PaymentReceiveReceipt.
    /// </summary>
    public partial class AR_PaymentReceiveReceipt : Telerik.Reporting.Report
    {
        public AR_PaymentReceiveReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            //PopulateHealthcareInfo();

            var query = new InvoicesQuery("a");
            var detail = new InvoicesItemQuery("b");
            var guar = new GuarantorQuery("c");
            var bank = new BankQuery("d");
            var guargroup = new GuarantorQuery("f");

            query.Select
                (
                    query.InvoiceNo,
                    query.InvoiceDate,
                    query.PaymentDate.As ("tgl"),
                    query.PaymentApprovedDate,
                    query.TransferDate,
                    query.InvoiceNotes,
                    bank.BankName,
                    guar.GuarantorName,
                    guargroup.GuarantorName.As("GuarantorNm"),
                    @"<RTRIM(c.StreetName) + ' ' + RTRIM(c.District) + ' ' + RTRIM(c.County) AS Address1>",
                    @"<RTRIM(c.State) + ' ' + RTRIM(c.ZipCode) AS Address2>",
                    detail.PaymentNo,
                    detail.PaymentDate,
                    detail.PaymentAmount,
                    detail.LastUpdateByUserID,
                    @"<GETDATE() AS PrintedDate>"
                );

            query.InnerJoin(detail).On(query.InvoiceNo == detail.InvoiceNo);
            query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);
            query.InnerJoin(guargroup).On(guar.GuarantorHeaderID == guargroup.GuarantorID);
            query.LeftJoin(bank).On(bank.BankID== query.BankID);
            query.Where
                (
                    query.InvoiceNo == printJobParameters[0].ValueString,
                    query.IsVoid == false,
                    query.IsApproved == true
                );
            var dtb = query.LoadDataTable();
            decimal total = dtb.Rows.Cast<DataRow>().Sum(row => Convert.ToDecimal(row["PaymentAmount"]));

            DataSource = dtb;

            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.AddressLine2;

            //txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            //TxtAmount.Value = string.Format("Rp. {0:n2}", (total));

            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            TxtAmount.Value = string.Format("Rp. {0:n2}", total);
        }
        private void PopulateHealthcareInfo()
        {
            var healthcare = Healthcare.GetHealthcare();
            
            
            string finance = AppParameter.GetParameterValue(AppParameter.ParameterItem.FinanceHead);
            TxtUserName.Value = '(' + finance + ')';
        }
    }
}