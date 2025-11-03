using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Summary description for BillingIntermStatementPatientIncCompPaymentReceiptUnion.
    /// </summary>
    public partial class BillingIntermStatementPatientIncCompPaymentReceiptUnion : Telerik.Reporting.Report
    {
        public BillingIntermStatementPatientIncCompPaymentReceiptUnion(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //Helper.InitializeLogoOnlySizeModeNormal(this.pageHeaderSection1);

            var byr = new TransPaymentReceipt();
            byr.LoadByPrimaryKey(printJobParameters.FindByParameterName("PaymentReceiptNo").ValueString);
            textBox151.Value = byr.PaymentReceiptNo;
            textBox15.Value = byr.PrintReceiptAsName;

            //string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = Common.Helper.MergeBilling.GetMergeRegistration(byr.RegistrationNo);

            var regs = new RegistrationCollection();
            regs.Query.Where(regs.Query.RegistrationNo.In(registrationNoList));
            regs.LoadAll();

            textBox131.Value = printJobParameters.FindByParameterName("UserID").ValueString;

            var regis = new Registration();
            regis.LoadByPrimaryKey(byr.RegistrationNo);

            string srRegType = regis.SRRegistrationType;

            var pp = new Patient();
            pp.LoadByPrimaryKey(regis.PatientID);
            textBox10.Value = pp.PatientName;
            textBox12.Value = byr.RegistrationNo + " / " + pp.MedicalNo;
            textBox116.Value = string.Format("{0:dd-MMM-yyyy}", pp.DateOfBirth);


         

            var dr = new Paramedic();
            if (regis.ParamedicID != null)
            {
                if (dr.LoadByPrimaryKey(regis.ParamedicID))
                {
                    textBox117.Value = dr.ParamedicName;
                }
            }
           
            var svu = new ServiceUnit();
            if(svu.LoadByPrimaryKey(regis.ServiceUnitID)){
                textBox9.Value = svu.ServiceUnitName;
            }
            // kelas, no tempat tidur
            var cls = new Temiang.Avicenna.BusinessObject.Class();
            if(cls.LoadByPrimaryKey(regis.ClassID)){
                if(srRegType == "IPR"){
                    textBox9.Value = cls.ClassName + " / " + regis.BedID;
                }
            }
           
            // penjamin
            textBox54.Value = (regis.GuarantorID == Common.AppSession.Parameter.SelfGuarantor) ? "PRIBADI" : "PERUSAHAAN / ASURANSI";
            var guar = new Guarantor();
            if (guar.LoadByPrimaryKey(regis.GuarantorID))
            {
                var guarhd = new Guarantor();
                if (guarhd.LoadByPrimaryKey(guar.GuarantorHeaderID))
                {
                    if (guarhd.GuarantorName.ToUpper().Contains("KUMPULAN"))
                    {
                        textBox120.Value = guar.GuarantorName;
                    }
                    else
                    {
                        textBox120.Value = guarhd.GuarantorName;
                    }
                }
            }

            //textBox39.Value = Common.AppSession.Parameter.PicHeadOfAdmitting;
            textBox48.Value = string.Format("{0:dd-MMM-yyyy}", regis.RegistrationDate);
           
            // header tgl mulai sampai
           
            textBox131.Visible = (srRegType == "IPR");

            textBox13.Value ="KWITANSI RAWAT JALAN"; // + svu.ServiceUnitName);
            textBox50.Value = srRegType == "IPR" ? "Tanggal Masuk" : "Tanggal Periksa";
           
            // kelas ruang
            textBox4.Value = srRegType == "IPR" ? "Kelas / No.TT" : "Poliklinik";

            var healthcare = new Healthcare();
            healthcare.LoadByPrimaryKey("001");
            textBox64.Value = healthcare.HealthcareName;
            textBox132.Value = healthcare.AddressLine1 + (srRegType == "IPR" ? " " + healthcare.City : "");
            textBox133.Value = srRegType == "IPR" ? "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo : healthcare.City;
            var Email = string.Empty;
            var EmParam = new AppParameter();
            if (EmParam.LoadByPrimaryKey("HealthcareFinanceEmailAddr"))
                Email = EmParam.ParameterValue;
            textBox139.Value = srRegType == "IPR" ? "Email: " + Email : "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;
            textBox111.Value = healthcare.City +", " +  string.Format("{0:dd-MMM-yyyy}",DateTime.Now);

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            this.DataSource = DataSource;
            //this.DataSource = table;
            //var tpr = new TransPaymentReceiptItemQuery("a");
            //var tp = new TransPaymentItemQuery("b");

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

                var payments = new TransPaymentItemCollection();
                var qtpi = new TransPaymentItemQuery("a");
                var qtp = new TransPaymentQuery("b");
                qtpi.InnerJoin(qtp).On(qtpi.PaymentNo == qtp.PaymentNo && qtp.TransactionCode.In("016", "017") &&
                                     qtpi.PaymentNo == c.PaymentNo && qtpi.IsFromDownPayment == false);
                payments.Load(qtpi);
                total += payments.Sum(p => (p.Amount ?? 0));

                payments = new TransPaymentItemCollection();
                qtpi = new TransPaymentItemQuery("a");
                qtp = new TransPaymentQuery("b");
                qtpi.InnerJoin(qtp).On(qtpi.PaymentNo == qtp.PaymentNo && qtp.TransactionCode.In("018", "019") &&
                                     qtp.PaymentReferenceNo == c.PaymentNo && qtp.ReceiptIsReturned == true);
                payments.Load(qtpi);
                total += payments.Sum(p => (p.Amount ?? 0));

                payments = new TransPaymentItemCollection();
                qtpi = new TransPaymentItemQuery("a");
                qtp = new TransPaymentQuery("b");
                qtpi.InnerJoin(qtp).On(qtpi.PaymentNo == qtp.PaymentNo && qtp.TransactionCode == "018" &&
                                     qtp.PaymentNo == c.PaymentNo);
                payments.Load(qtpi);
                total += payments.Sum(p => (p.Amount ?? 0));
            }

            if (total > 0)
            { 
              textBox110.Value = "Terbilang : " + (new Common.Convertion()).NumericToWords(total);  
            }
            else
            {
                textBox110.Value = "Terbilang : Nol Rupiah";
            }
            
        }
    }
}