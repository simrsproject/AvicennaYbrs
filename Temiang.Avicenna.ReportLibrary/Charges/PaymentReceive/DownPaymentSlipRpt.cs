namespace Temiang.Avicenna.ReportLibrary.Charges
{
    using BusinessObject;
    using System;
    using System.Data;

    /// <summary>
    /// Summary description for PaymentReceiveDetail.
    /// </summary>
    public partial class DownPaymentSlipRpt : Telerik.Reporting.Report
    {
        public DownPaymentSlipRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeader);
            //printJobParameters.AddNew("p_PaymentNo", "PM100511-00029");

            var query = new TransPaymentQuery("a");
            var detail = new TransPaymentItemQuery("b");
            var reg = new RegistrationQuery("c");
            var patient = new PatientQuery("d");

            query.Select
                (
                    "<CASE WHEN a.IsPrinted = 1 THEN a.PaymentNo + ' (D)' ELSE a.PaymentNo END AS PaymentNo>",
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    query.PaymentDate,
                    query.PrintReceiptAsName,
                    @"<HealthcareName = (SELECT HealthcareName FROM Healthcare) >",
                    (detail.Amount.Sum() + detail.CardFeeAmount.Sum()).As("TotalAmount"),
                    query.LastUpdateByUserID
                );

            query.InnerJoin(detail).On(query.PaymentNo == detail.PaymentNo);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.Where
                (
                    query.PaymentNo == printJobParameters[0].ValueString,
                    query.IsApproved == true,
                    query.IsVoid == false
                );

            query.GroupBy
                (
                    query.IsPrinted,
                    query.PaymentNo,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    query.PaymentDate,
                    query.PrintReceiptAsName,
                    query.LastUpdateByUserID
                );

            var dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(Convert.ToDecimal(dtb.Rows[0]["TotalAmount"]));
                this.DataSource = dtb;

                var user = new AppUser();
                user.LoadByPrimaryKey(dtb.Rows[0]["LastUpdateByUserID"].ToString());
                txtUserName.Value = user.UserName;
            }

            var hd = new TransPayment();
            if (hd.LoadByPrimaryKey(printJobParameters[0].ValueString))
            {
                hd.PrintNumber++;
                if (!hd.IsPrinted ?? false)
                    hd.IsPrinted = true;

                hd.Save();
            }
        }
    }
}