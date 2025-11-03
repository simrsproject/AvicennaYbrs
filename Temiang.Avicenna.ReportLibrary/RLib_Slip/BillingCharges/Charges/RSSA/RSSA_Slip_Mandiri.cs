using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;
    /// <summary>
    /// Summary description for PaymentReceiveReceipt.
    /// </summary>
    public partial class RSSA_Slip_Mandiri : Telerik.Reporting.Report
    {
        public RSSA_Slip_Mandiri(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            var query = new TransPaymentQuery("a");
            var item = new TransPaymentItemQuery("b");

            query.Select
                (
                    query.PaymentNo,
                    query.PaymentDate,
                    query.PrintReceiptAsName,
                    query.TotalPaymentAmount,
                    item.IsFromDownPayment,
                    @"<Case WHEN b.Balance > 0 then b.balance
                    else
                    (b.Amount + b.CardFeeAmount) END As 'Amount'>"
                );

            query.InnerJoin(item).On(query.PaymentNo == item.PaymentNo);
            query.Where
                (
                    query.Or(item.IsFromDownPayment == false, item.Balance > 0),
                    query.IsVoid == 0,
                    query.PaymentNo == printJobParameters[0].ValueString
                );

            query.OrderBy(item.SequenceNo, esOrderByDirection.Ascending);

            DataTable dtb = query.LoadDataTable();

            decimal total = 0;
            foreach (DataRow row in dtb.Rows)
            {
                total = Convert.ToDecimal(row["Amount"]);
            }

            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            TxtAmount.Value = string.Format("{0:n0}",(total));
            DataSource = dtb;

            var app = new AppParameter();
            app.LoadByPrimaryKey("BankAccNoForSlipBank");
            txtBankAccNo.Value = app.ParameterValue;

            app = new AppParameter();
            app.LoadByPrimaryKey("BankAccNameForSlipBank");
            txtBankAccName.Value = app.ParameterValue;

            app = new AppParameter();
            app.LoadByPrimaryKey("BankNameForSlipBank");
            txtBankName.Value = app.ParameterValue;
        }
    }
}