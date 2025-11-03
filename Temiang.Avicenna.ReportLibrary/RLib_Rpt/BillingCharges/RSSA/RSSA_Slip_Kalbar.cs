using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Rpt.BillingCharges.RSSA
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;
    /// <summary>
    /// Summary description for RSSA_Slip_Kalbar.
    /// </summary>
    public partial class RSSA_Slip_Kalbar : Report
    {
        public RSSA_Slip_Kalbar(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //DateTime? startDate = printJobParameters.FindByParameterName("p_FromDateTime").ValueDateTime;
            //DateTime? endDate = printJobParameters.FindByParameterName("p_ToDateTime").ValueDateTime;
            //string userid = printJobParameters.FindByParameterName("p_UserID").ValueString;
            string bankNo = printJobParameters.FindByParameterName("p_AccountNo").ValueString;

            //var query = new TransPaymentQuery("a");
            //var item = new TransPaymentItemQuery("b");

            //query.Select
            //    ("<ISNULL(SUM(CASE WHEN b.SRPaymentMethod = 'PaymentMethod-001' AND b.SRPaymentType <> 'PaymentType-005'" +
            //        " AND a.TransactionCode <> '019' AND a.TransactionCode <> '017' AND b.IsFromDownPayment = 0 THEN " +
            //        " CASE WHEN a.TransactionCode = '016' AND b.Amount   < 0 THEN 0 ELSE b.Amount END ELSE 0  END) + " +
            //    "SUM(CASE  WHEN b.SRPaymentMethod = 'PaymentMethod-001' AND b.SRPaymentType <> 'PaymentType-005' " +
            //    " AND a.TransactionCode = '017' AND b.IsFromDownPayment = 0 THEN " +
            //     " b.Amount  ELSE CASE WHEN a.TransactionCode = '016' AND b.Amount < 0 THEN b.Amount ELSE 0 END END )  - " +
            //     "SUM(CASE  WHEN ( b.SRPaymentMethod = 'PaymentMethod-001' AND b.IsFromDownPayment = 1 ) THEN " +
            //     " b.balance WHEN (b.SRPaymentMethod = 'PaymentMethod-001' AND b.IsFromDownPayment = 0 AND a.TransactionCode = '019' ) THEN " +
            //     " b.Amount ELSE 0 END), 0) AS total >"
            //    );

            //query.InnerJoin(item).On(query.PaymentNo == item.PaymentNo);
            //query.Where
            //    (
            //        query.IsApproved == true,
            //        query.IsVoid == false,
            //        item.SRPaymentType.NotIn("PaymentType-003","PaymentType-004"),
            //        "<CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112) + ' ' + a.PaymentTime AS DATETIME ) >= '" + startDate +
            //                         "' AND CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112)   + ' ' + a.PaymentTime AS DATETIME ) <= '" + endDate + "'>",
            //        //query.PaymentDate.Date().Between(startDate.Value.Date, endDate.Value.Date),
            //        query.CreatedBy == userid
            //    );

            var user = new AppUser();
            user.LoadByPrimaryKey(printJobParameters.FindByParameterName("p_UserID").ValueString);
            textBox30.Value = user.UserName;


            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            //decimal total = 0;
            //foreach (DataRow row in dtb.Rows)
            //{
            //    total = Convert.ToDecimal(row["total"]);
            //}

            //txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            //TxtAmount.Value = string.Format("{0:n0}", (total));
            //DataSource = dtb;


            var healthcare = Healthcare.GetHealthcare();
            
            textBox3.Value = healthcare.AddressLine1;
            textBox15.Value = healthcare.PhoneNo;

            var accNo = new AppStandardReferenceItem();
            if (accNo.LoadByPrimaryKey("BankAccountNo", bankNo))
            {
                var pAccNo = accNo.ItemName.Replace("-", "");

                textBox12.Value = pAccNo;
                txt1.Value = pAccNo.Substring(0, 1);
                textBox2.Value = pAccNo.Substring(1, 1);
                textBox4.Value = pAccNo.Substring(2, 1);
                textBox5.Value = "-";
                textBox6.Value = pAccNo.Substring(3, 1);
                textBox7.Value = pAccNo.Substring(4, 1);
                textBox8.Value = pAccNo.Substring(5, 1);
                textBox9.Value = "-";
                textBox10.Value = pAccNo.Substring(6, 1);
                textBox11.Value = pAccNo.Substring(7, 1);
                textBox13.Value = pAccNo.Substring(8, 1);
                textBox14.Value = pAccNo.Substring(9, 1);
            }

            var app = new AppParameter();
            app.LoadByPrimaryKey("BankAccNameForSlipBankKalbar");
            txtBankAccName.Value = app.ParameterValue;

            app = new AppParameter();
            app.LoadByPrimaryKey("BankNameForSlipBankKalbar");
            txtBankName.Value = app.ParameterValue;
        }
    }
}