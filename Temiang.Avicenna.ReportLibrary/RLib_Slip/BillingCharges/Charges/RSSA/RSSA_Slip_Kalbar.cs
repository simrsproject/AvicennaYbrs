using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSSA
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;
    /// <summary>
    /// Summary description for PaymentReceiveReceipt.
    /// </summary>
    public partial class RSSA_Slip_Kalbar : Telerik.Reporting.Report
    {
        public RSSA_Slip_Kalbar(string programID, PrintJobParameterCollection printJobParameters)
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
            TxtAmount.Value = string.Format("{0:n0}", (total));
            DataSource = dtb;

            var app = new AppParameter();
            app.LoadByPrimaryKey("BankAccNoForSlipBankKalbar");
           
            //var tmp = app.ParameterValue.Length;
            textBox12.Value = app.ParameterValue;
            txt1.Value = app.ParameterValue.Substring(0, 1);
            textBox2.Value = app.ParameterValue.Substring(1,1);
            textBox4.Value = app.ParameterValue.Substring(2,1);
            textBox5.Value ="-";
            textBox6.Value = app.ParameterValue.Substring(3,1);
            textBox7.Value = app.ParameterValue.Substring(4, 1);
            textBox8.Value = app.ParameterValue.Substring(5, 1);
            textBox9.Value = "-";
            textBox10.Value = app.ParameterValue.Substring(6, 1);
            textBox11.Value = app.ParameterValue.Substring(7, 1);
            textBox13.Value = app.ParameterValue.Substring(8, 1);
            textBox14.Value = app.ParameterValue.Substring(9, 1);
            app = new AppParameter();
            app.LoadByPrimaryKey("BankAccNameForSlipBankKalbar");
            txtBankAccName.Value = app.ParameterValue;

            app = new AppParameter();
            app.LoadByPrimaryKey("BankNameForSlipBankKalbar");
            txtBankName.Value = app.ParameterValue;
        }
    }
}