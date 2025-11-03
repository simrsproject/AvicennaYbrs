using System.Linq;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSCH
{
    using Telerik.Reporting;
    using BusinessObject;
    
    public partial class PaymentReceiveReceipt : Report
    {
        public PaymentReceiveReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            string regNo = printJobParameters.FindByParameterName("RegNo").ValueString;

            textBox14.Value = printJobParameters[0].ValueString;

            var reg = new Registration();
            reg.LoadByPrimaryKey(regNo);
            textBox18.Value = string.Format("{0:dd-MMM-yyyy}", reg.RegistrationDate);
            if (reg.SRRegistrationType == "IPR")
            {
                textBox20.Value = string.Format("{0:dd-MMM-yyyy}", reg.DischargeDate);
                textBox5.Value = "Tanggal Dirawat";
            }
            else
            {
                textBox20.Value = "";
                textBox5.Value = "Tanggal Periksa";
            }
            var ptn = new Patient();
            ptn.LoadByPrimaryKey(reg.PatientID);
            textBox15.Value = reg.RegistrationNo + "   /   " + ptn.MedicalNo;
            textBox27.Value = ptn.FirstName + ' ' + ptn.MiddleName + ' ' + ptn.LastName;

            var pay = new TransPaymentReceipt();
            pay.LoadByPrimaryKey(printJobParameters[0].ValueString);
            textBox22.Value = string.Format("{0:dd-MMM-yyyy}", pay.PaymentReceiptDate) + " " + pay.PaymentReceiptTime;
            textBox16.Value = pay.Notes;

            decimal total = 0;
            var isToPatient = false;
            var coll = new TransPaymentReceiptItemCollection();
            var query = new TransPaymentReceiptItemQuery("a");
            var hd = new TransPaymentReceiptQuery("b");
            query.InnerJoin(hd).On(query.PaymentReceiptNo == hd.PaymentReceiptNo && hd.IsApproved == true &&
                                   hd.PaymentReceiptNo == printJobParameters[0].ValueString);
            coll.Load(query);
            foreach (var c in coll)
            {
                var tp = new TransPayment();
                tp.LoadByPrimaryKey(c.PaymentNo);
                if (tp.IsToGuarantor == false)
                    isToPatient = true;

                total += (c.Amount ?? 0);

                //var payments = new TransPaymentItemCollection();
                //var qtpi = new TransPaymentItemQuery("a");
                //var qtp = new TransPaymentQuery("b");
                //qtpi.InnerJoin(qtp).On(qtpi.PaymentNo == qtp.PaymentNo && qtp.TransactionCode.In("016", "017") &&
                //                     qtpi.PaymentNo == c.PaymentNo && qtpi.IsFromDownPayment == false);
                //payments.Load(qtpi);
                //total += payments.Sum(p => (p.Amount ?? 0));

                //payments = new TransPaymentItemCollection();
                //qtpi = new TransPaymentItemQuery("a");
                //qtp = new TransPaymentQuery("b");
                //qtpi.InnerJoin(qtp).On(qtpi.PaymentNo == qtp.PaymentNo && qtp.TransactionCode.In("018", "019") &&
                //                     qtp.PaymentReferenceNo == c.PaymentNo && qtp.ReceiptIsReturned == true);
                //payments.Load(qtpi);
                //total += payments.Sum(p => (p.Amount ?? 0));

                //payments = new TransPaymentItemCollection();
                //qtpi = new TransPaymentItemQuery("a");
                //qtp = new TransPaymentQuery("b");
                //qtpi.InnerJoin(qtp).On(qtpi.PaymentNo == qtp.PaymentNo && qtp.TransactionCode == "018" &&
                //                     qtp.PaymentNo == c.PaymentNo);
                //payments.Load(qtpi);
                //total += payments.Sum(p => (p.Amount ?? 0));
            }

            var healthcare = Healthcare.GetHealthcare();
            
            TxtCityRS.Value = healthcare.City;

            txtTotalAmountInWords.Value = (new Common.Convertion()).NumericToWords(total);
            if (total > 0)
            {
                textBox1.Value = "Kwitansi Pembayaran";
                TxtAmount.Value = string.Format("{0:n0}", (total));
                textBox30.Value = pay.PrintReceiptAsName;
            }
            else
            {
                textBox1.Value = "Kwitansi Pengembalian Uang";
                TxtAmount.Value = string.Format("{0:n0}", (-total));
                textBox30.Value = healthcare.HealthcareName;
            }

            if (isToPatient)
            {
                var user = new AppUser();
                user.LoadByPrimaryKey(printJobParameters.FindByParameterName("UserID").ValueString);
                TxtUserName.Value = user.UserName;
            }
            else
            {
                TxtUserName.Value = "Sr. M. Tarsisia, Fch";
            }
        }
    }
}