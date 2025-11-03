namespace Temiang.Avicenna.ReportLibrary.Charges
{
    using BusinessObject;
    using System;
    using System.Data;

    /// <summary>
    /// Summary description for PaymentReceiptRpt.
    /// </summary>
    public partial class PaymentReceiptRpt : Telerik.Reporting.Report
    {
        public PaymentReceiptRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            string paymentNo = string.Empty;
            int i = 0;
            TransPaymentReceiptItemQuery q = new TransPaymentReceiptItemQuery("x");
            TransPaymentReceiptQuery r = new TransPaymentReceiptQuery("y");
            q.InnerJoin(r).On(q.PaymentReceiptNo == r.PaymentReceiptNo);
            q.Select(q.PaymentNo);
            q.Where(q.PaymentReceiptNo == printJobParameters[0].ValueString, r.IsVoid == false);
            q.OrderBy(q.PaymentNo.Ascending);

            var dtbQ = q.LoadDataTable();
            if (dtbQ.Rows.Count > 0)
            {
                while (i < dtbQ.Rows.Count)
                {
                    if (i == 0)
                        paymentNo = dtbQ.Rows[i]["PaymentNo"].ToString();
                    else
                        paymentNo = paymentNo + "; " + dtbQ.Rows[i]["PaymentNo"].ToString();
                    i += 1;
                }
            }
            
            var query = new TransPaymentReceiptQuery("a");
            var detail = new TransPaymentReceiptItemQuery("b");
            var payment = new TransPaymentQuery("c");
            var reg = new RegistrationQuery("d");
            var patient = new PatientQuery("e");
            var paymentItem = new TransPaymentItemQuery("f");
            
            query.Select
                (
                    "<CASE WHEN a.IsPrinted = 1 THEN a.PaymentReceiptNo + ' (D)' ELSE a.PaymentReceiptNo END AS PaymentReceiptNo>",
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    query.PaymentReceiptDate,
                    query.PrintReceiptAsName,
                    paymentItem.Amount.Sum().As("TotalPaymentAmount"),
                    query.LastUpdateByUserID,
                    @"<'" + paymentNo + @"' AS 'PaymentNo'>"
                );

            query.InnerJoin(detail).On(query.PaymentReceiptNo == detail.PaymentReceiptNo);
            query.InnerJoin(payment).On(detail.PaymentNo == payment.PaymentNo);
            query.InnerJoin(paymentItem).On(detail.PaymentNo == paymentItem.PaymentNo);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.Where
                (
                    query.PaymentReceiptNo == printJobParameters[0].ValueString,
                    query.IsVoid == false,
                    query.IsApproved == true
                );
            query.GroupBy
                (
                    query.IsPrinted,
                    query.PaymentReceiptNo,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    query.PaymentReceiptDate,
                    query.PrintReceiptAsName,
                    query.LastUpdateByUserID
                );
           
            var dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(Convert.ToDecimal(dtb.Rows[0]["TotalPaymentAmount"]));
                this.DataSource = dtb;

                var user = new AppUser();
                user.LoadByPrimaryKey(dtb.Rows[0]["LastUpdateByUserID"].ToString());
                txtUserName.Value = user.UserName;
            }

            var hd = new TransPaymentReceipt();
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