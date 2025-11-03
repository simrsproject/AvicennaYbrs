using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSCH.GDBaru
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
    /// Summary description for BillingIntermStatementPatientIncCompPaymentReceiptOp.
    /// </summary>
    public partial class BillingIntermStatementPatientIncCompPaymentReceiptOp : Telerik.Reporting.Report
    {
        public BillingIntermStatementPatientIncCompPaymentReceiptOp(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //Helper.InitializeLogoOnlySizeModeNormal(this.pageHeaderSection1);

            var byr = new TransPayment();
            byr.LoadByPrimaryKey(printJobParameters.FindByParameterName("PaymentNo").ValueString);
            textBox151.Value = byr.PaymentNo;
            textBox15.Value = byr.PrintReceiptAsName;


            //string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = Common.Helper.MergeBilling.GetMergeRegistration(byr.RegistrationNo);

            var regs = new RegistrationCollection();
            regs.Query.Where(regs.Query.RegistrationNo.In(registrationNoList));
            regs.LoadAll();

            textBox131.Value = printJobParameters.FindByParameterName("UserName").ValueString;

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
            //textBox54.Value = (regis.GuarantorID == Common.AppSession.Parameter.SelfGuarantor) ? "PRIBADI" : "PERUSAHAAN / ASURANSI";
            var guar = new Guarantor();
            if (guar.LoadByPrimaryKey(regis.GuarantorID))
            {
                textBox54.Value = (guar.GuarantorHeaderID == Common.AppSession.Parameter.SelfGuarantor) ? "PRIBADI" : "PERUSAHAAN / ASURANSI";
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
            else
            {
                textBox54.Value = (regis.GuarantorID == Common.AppSession.Parameter.SelfGuarantor) ? "PRIBADI" : "PERUSAHAAN / ASURANSI";
            }

            //textBox39.Value = Common.AppSession.Parameter.PicHeadOfAdmitting;
            textBox48.Value = string.Format("{0:dd-MMM-yyyy}", regis.RegistrationDate);
           
            // header tgl mulai sampai
           
            textBox131.Visible = (srRegType == "IPR");

            textBox13.Value ="KWITANSI RAWAT JALAN"; // + svu.ServiceUnitName);
            textBox50.Value = srRegType == "IPR" ? "Tanggal Masuk" : "Tanggal Periksa";
           
            // kelas ruang
            textBox4.Value = srRegType == "IPR" ? "Kelas / No.TT" : "Poliklinik";

            var healthcare = Healthcare.GetHealthcare();
            
            textBox64.Value = healthcare.HealthcareName;
            textBox132.Value = healthcare.AddressLine1 + (srRegType == "IPR" ? " " + healthcare.City : "");
            textBox133.Value = srRegType == "IPR" ? "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo : healthcare.City;
            var Email = string.Empty;
            var EmParam = new AppParameter();
            if (EmParam.LoadByPrimaryKey("HealthcareFinanceEmailAddr"))
                Email = EmParam.ParameterValue;
            textBox139.Value = srRegType == "IPR" ? "Email: " + Email : "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;


            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            this.DataSource = DataSource;
            //this.DataSource = table;

            var tp = new TransPaymentItemCollection();
            tp.Query.Where(tp.Query.PaymentNo == byr.PaymentNo, tp.Query.SRPaymentType =="PaymentType-002");
            tp.LoadAll();
            decimal total = tp.Sum(item => item.Amount ?? 0);
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