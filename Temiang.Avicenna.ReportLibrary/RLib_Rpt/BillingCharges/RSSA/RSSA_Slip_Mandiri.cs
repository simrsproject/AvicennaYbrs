using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
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
    public partial class RSSA_Slip_Mandiri : Telerik.Reporting.Report
    {
        public RSSA_Slip_Mandiri(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //DateTime? startDate = printJobParameters.FindByParameterName("p_FromDateTime").ValueDateTime;
            //DateTime? endDate = printJobParameters.FindByParameterName("p_ToDateTime").ValueDateTime;
            //string userid = printJobParameters.FindByParameterName("p_UserID").ValueString;

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
            //        item.SRPaymentType.NotIn("PaymentType-003", "PaymentType-004"),
            //        "<CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112) + ' ' + a.PaymentTime AS DATETIME ) >= '" + startDate +
            //                         "' AND CAST(CONVERT(VARCHAR(10), a.PaymentDate, 112)   + ' ' + a.PaymentTime AS DATETIME ) <= '" + endDate + "'>",
            //    //query.PaymentDate.Date().Between(startDate.Value.Date, endDate.Value.Date),
            //        query.CreatedBy == userid
            //    );

            //DataTable dtb = query.LoadDataTable();

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            var healthcare = Healthcare.GetHealthcare();
            
            textBox3.Value = healthcare.AddressLine1;

            var user = new AppUser();
            user.LoadByPrimaryKey(printJobParameters.FindByParameterName("p_UserID").ValueString);
            textBox30.Value = user.UserName;

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