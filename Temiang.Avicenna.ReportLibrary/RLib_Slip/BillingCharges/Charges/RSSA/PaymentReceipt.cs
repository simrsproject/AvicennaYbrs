using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSSA
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using Temiang.Avicenna.BusinessObject.Reference;
    /// <summary>
    /// Summary description for PaymentReceiveReceipt.
    /// </summary>
    public partial class PaymentReceipt : Report
    {
        public PaymentReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            Helper.InitializeNoLogoBigFont(this.pageHeader);

            string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;

            txtPaymentNo.Value = printJobParameters[0].ValueString;

            var reg = new Registration();
            reg.LoadByPrimaryKey(regNo);
            txtRegNo.Value = reg.RegistrationNo;
            txtDateReg.Value = string.Format("{0:dd-MMM-yyyy}", reg.RegistrationDate) + " " + reg.RegistrationTime;

            txtDateDis.Value = string.Format("{0:dd-MMM-yyyy}", reg.DischargeDate) + " " + reg.DischargeTime;

            var ptn = new Patient();
            ptn.LoadByPrimaryKey(reg.PatientID);
            txtRM.Value = ptn.MedicalNo;
            txtPatientName.Value = ptn.FirstName + ' ' + ptn.MiddleName + ' ' + ptn.LastName;
            TxtAddress.Value = ptn.StreetName + ' ' + ptn.City;
            
            var pay = new TransPaymentReceipt();
            pay.LoadByPrimaryKey(printJobParameters[0].ValueString);
            txtPayDate.Value = string.Format("{0:dd-MMM-yyyy}", pay.PaymentReceiptDate) + " " + pay.PaymentReceiptTime;
            textBox21.Value = pay.Notes;


            //#region fromPaymentDirect

            //var pyh = new TransPaymentReceiptQuery("a");
            //var pyd = new TransPaymentReceiptItemQuery("b");
            //var pydi = new TransPaymentItemOrderQuery("c");
            //pyh.InnerJoin(pyd).On(pyh.PaymentReceiptNo == pyd.PaymentReceiptNo && pyh.IsApproved == true &&
            //pyh.PaymentReceiptNo == printJobParameters[0].ValueString);
            //pyh.InnerJoin(pydi).On(pyd.PaymentNo == pydi.PaymentNo && pydi.IsPaymentReturned == false);
            //pyh.Select(@"<ISNULL(SUM(c.Price * c.Qty), 0) AS 'Amount'>");

            //DataTable dtbpy = pyh.LoadDataTable();
            //#endregion


            //#region fromPaymentIntermBill
            //pyh = new TransPaymentReceiptQuery("a");
            //var pyd1 = new TransPaymentReceiptItemQuery("b");
            //var pytp = new TransPaymentQuery("c");
            //pyh.InnerJoin(pyd1).On(pyh.PaymentReceiptNo == pyd1.PaymentReceiptNo && pyh.IsApproved == true &&
            //pyh.PaymentReceiptNo == printJobParameters[0].ValueString);
            //pyh.InnerJoin(pytp).On(pyd1.PaymentNo == pytp.PaymentNo && pytp.IsApproved == true);
            //pyh.Select((pytp.TotalPaymentAmount).Sum().As("Amount"));

            //dtbpy.Merge(pyh.LoadDataTable());
            //#endregion

            //decimal total = 0;

            //if (dtbpy.Rows.Count > 0)
            //{
            //    int i = 0;
            //    foreach (var s in dtbpy.Rows)
            //    {
            //        total += Convert.ToDecimal(dtbpy.Rows[i]["Amount"]);

            //        i += 1;
            //    }
            //}

            var ph = new TransPaymentReceiptQuery("a");
            var pd = new TransPaymentReceiptItemQuery("b");
            var pyd = new TransPaymentItemQuery("c");
            ph.InnerJoin(pd).On(ph.PaymentReceiptNo == pd.PaymentReceiptNo && ph.IsApproved == true &&
                                ph.PaymentReceiptNo == printJobParameters[0].ValueString);
            ph.InnerJoin(pyd).On(pd.PaymentNo == pyd.PaymentNo);
            ph.Select(pyd.Amount.Sum().As("Amount"));
            DataTable dtbpy = ph.LoadDataTable();

            decimal total = 0;

            if (dtbpy.Rows.Count > 0)
            {
                total += Convert.ToDecimal(dtbpy.Rows[0]["Amount"]);
            }
            
            var healthcare = new Healthcare();
            healthcare.LoadByPrimaryKey("001");
            TxtCityRS.Value = healthcare.City;

            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            if (total > 0)
            {
                textBox1.Value = "Kwitansi Pembayaran";
                TxtAmount.Value = string.Format("{0:n0}", (total));
                textBox17.Value = "Untuk pembayaran biaya perawatan & pengobatan sesuai rincian terlampir.";
                textBox30.Value = pay.PrintReceiptAsName;

            }
            else
            {
                textBox1.Value = "Kwitansi Pengembalian Uang";
                TxtAmount.Value = string.Format("{0:n0}", (-total));
                textBox30.Value = healthcare.HealthcareName;
                textBox17.Value = string.Empty;
            }

            var user = new AppUser();
            user.LoadByPrimaryKey(printJobParameters.FindByParameterName("UserID").ValueString);
            TxtUserName.Value = user.UserName;
        }
    }
}