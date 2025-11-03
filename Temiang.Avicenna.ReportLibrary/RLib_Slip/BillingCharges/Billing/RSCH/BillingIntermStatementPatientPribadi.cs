using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSCH
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
    /// Summary description for BillingStatementDetail.
    /// </summary>
    public partial class BillingIntermStatementPatientPribadi : Telerik.Reporting.Report
    {
        public BillingIntermStatementPatientPribadi(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogo(this.reportHeaderSection1);

            string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = new string[1];
            if (registrationNo.Contains(","))
                registrationNoList = registrationNo.Split(',');
            else
                registrationNoList.SetValue(registrationNo, 0);


            var regs = new RegistrationCollection();
            regs.Query.Where(regs.Query.RegistrationNo.In(registrationNoList));
            regs.LoadAll();

            decimal adm = regs.Sum(r => r.AdministrationAmount ?? 0);
            //decimal total = table.AsEnumerable().Sum(t => t.Field<decimal>("Total")) +
            //                regs.Sum(r => r.AdministrationAmount ?? 0);
            //decimal tpatient = table.AsEnumerable().Sum(t => t.Field<decimal>("PatientAmount"));
            //decimal tguarantor = table.AsEnumerable().Sum(t => t.Field<decimal>("GuarantorAmount"));

            //textBox31.Value = string.Format("{0:N2}", adm);
            //textBox33.Value = string.Format("{0:N2}", total);
            //textBox41.Value = string.Format("{0:N2}", Common.Helper.Payment.GetTotalDownPaymentOnly(registrationNoList) - Common.Helper.Payment.GetTotalDownPaymentReturn(registrationNoList));
            //textBox37.Value = string.Format("{0:N2}", total - (Common.Helper.Payment.GetTotalDownPaymentOnly(registrationNoList) - Common.Helper.Payment.GetTotalDownPaymentReturn(registrationNoList)));

            textBox131.Value = printJobParameters.FindByParameterName("UserName").ValueString;

            var regis = new Registration();
            regis.LoadByPrimaryKey(printJobParameters.FindByParameterName("RegNo").ValueString);
            var p = new Patient();
            p.LoadByPrimaryKey(regis.PatientID);
            textBox10.Value = p.PatientName;
            textBox12.Value = printJobParameters.FindByParameterName("RegNo").ValueString + " / " + p.MedicalNo;
            textBox116.Value = string.Format("{0:dd-MMM-yyyy}", p.DateOfBirth);

            var dr = new Paramedic();
            if(dr.LoadByPrimaryKey(regis.ParamedicID)){
                textBox117.Value = dr.ParamedicName;
            }
            var svr = new ServiceRoom();
            if(svr.LoadByPrimaryKey(regis.RoomID)){
                textBox11.Value = svr.RoomName;
            }
            // kelas, no tempat tidur
            var cls = new Temiang.Avicenna.BusinessObject.Class();
            if(cls.LoadByPrimaryKey(regis.ClassID)){
                textBox9.Value = cls.ClassName + " / " + regis.BedID;
            }
            // hak kelas
            if (cls.LoadByPrimaryKey(regis.CoverageClassID)) {
                textBox128.Value = cls.ClassName;
            }
            // header tgl mulai sampai
            textBox129.Value = "Periode Perawatan : " +
                string.Format("{0:dd-MMM-yyyy}", regis.RegistrationDate) + " s/d " +
                string.Format("{0:dd-MMM-yyyy}", regis.DischargeDate.HasValue ? regis.DischargeDate.Value : DateTime.Now );

            // penjamin
            textBox54.Value = (regis.GuarantorID == Common.AppSession.Parameter.SelfGuarantor) ? "PRIBADI" : "PERUSAHAAN";
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
            textBox123.Value = string.Format("{0:dd-MMM-yyyy}", regis.DischargeDate);

            // set title
            string ident = regis.RegistrationNo.Substring(4, 2).ToUpper();
            textBox13.Value += ident == "IP" ? " PERAWATAN" : " PEMERIKSAAN";
            textBox50.Value = ident == "IP" ? "Tanggal Masuk" : "Tanggal Periksa";
            textBox2.Value = ident == "IP" ? "Ruang Perawatan" : "Ruang Pemeriksaan";

            textBox121.Visible =
                textBox122.Visible =
                textBox123.Visible =
                textBox126.Visible =
                textBox127.Visible =
                textBox128.Visible =
                (ident == "IP");

            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            this.DataSource = DataSource;
            //this.DataSource = table;
        }
    }
}