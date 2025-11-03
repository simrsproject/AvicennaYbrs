using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing
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
    /// Summary description for BillingIntermStatementPatientIncComponentPaid.
    /// </summary>
    public partial class BillingIntermStatementPatientIncComponentPaidRecap : Telerik.Reporting.Report
    {
        public BillingIntermStatementPatientIncComponentPaidRecap(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();



            Helper.InitializeLogoOnlySizeModeNormal(this.pageHeaderSection1);

            var byr = new TransPayment();
            byr.LoadByPrimaryKey(printJobParameters.FindByParameterName("PaymentNo").ValueString);

            //string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = Common.Helper.MergeBilling.GetMergeRegistration(byr.RegistrationNo);

            var regs = new RegistrationCollection();
            regs.Query.Where(regs.Query.RegistrationNo.In(registrationNoList));
            regs.LoadAll();

            textBox131.Value = printJobParameters.FindByParameterName("UserName").ValueString;

            var regis = new Registration();
            regis.LoadByPrimaryKey(byr.RegistrationNo);

            string srRegType = regis.SRRegistrationType;

            var dd = ((regis.DischargeDate ?? DateTime.Now) - regis.RegistrationDate.Value).Days;
            if (dd == 0) dd++;
            else if (AppSession.Parameter.IsBillingStatementLosCalculationWithAdd1Day) dd++;

            textBox33.Value = string.Format("{0} hari", dd.ToString());
            textBox28.Visible = textBox34.Visible = textBox33.Visible = (srRegType == "IPR");

            var p = new Patient();
            p.LoadByPrimaryKey(regis.PatientID);
            var sal = new AppStandardReferenceItem();
            if (sal.LoadByPrimaryKey("Salutation", p.SRSalutation))
                textBox10.Value = sal.ItemName + " " + p.PatientName;
            else
                textBox10.Value = p.PatientName;
            textBox12.Value = byr.RegistrationNo + " / " + p.MedicalNo;
            textBox116.Value = string.Format("{0:dd-MMM-yyyy}", p.DateOfBirth);
            textBox5.Value = p.StreetName + ' ' + p.City;

            var dr = new Paramedic();
            if (regis.ParamedicID != null)
            {
                if (dr.LoadByPrimaryKey(regis.ParamedicID))
                {
                    textBox117.Value = dr.ParamedicName;
                }
            }

            textBox2.Value = srRegType == "IPR" ? "R.Perawatan" : "R.Pemeriksa";
            var svr = new ServiceRoom();
            if (svr.LoadByPrimaryKey(regis.RoomID))
            {
                textBox11.Value = svr.RoomName;
            }
            var svu = new ServiceUnit();
            if (svu.LoadByPrimaryKey(regis.ServiceUnitID))
            {
                textBox9.Value = svu.ServiceUnitName;
            }

            // kelas ruang
            textBox4.Value = srRegType == "IPR" ? "Kelas / No.Bed" : "Poliklinik";
            // kelas, no tempat tidur
            var cls = new Temiang.Avicenna.BusinessObject.Class();
            if (cls.LoadByPrimaryKey(regis.ClassID))
            {
                if (srRegType == "IPR")
                {
                    textBox9.Value = string.IsNullOrEmpty(cls.ShortName) ? cls.ClassName : cls.ShortName + " / " + regis.BedID;
                }
                else {
                    textBox9.Value = svu.ServiceUnitName;
                }
            }
            // hak kelas
            var clsCvr = new Temiang.Avicenna.BusinessObject.Class();
            if (clsCvr.LoadByPrimaryKey(regis.CoverageClassID))
            {
                textBox128.Value = string.IsNullOrEmpty(clsCvr.ShortName) ? clsCvr.ClassName : clsCvr.ShortName;
            }

            // penjamin
            //textBox54.Value = (regis.GuarantorID == Common.AppSession.Parameter.SelfGuarantor) ? "PRIBADI" : "PERUSAHAAN / ASURANSI";
            var guar = new Guarantor();
            //if (guar.LoadByPrimaryKey(regis.GuarantorID))
            if (guar.LoadByPrimaryKey(byr.GuarantorID))
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
            textBox123.Value = string.Format("{0:dd-MMM-yyyy}", regis.DischargeDate.HasValue ?
                regis.DischargeDate.Value : (regis.AllowPatientCheckOutDateTime.HasValue ? 
                regis.AllowPatientCheckOutDateTime : DateTime.Now));

            // header tgl mulai sampai
            textBox129.Value = "Periode " + (srRegType == "IPR" ? "Perawatan" : "Pemeriksaan") + " : " +
                string.Format("{0:dd-MMM-yyyy}", regis.RegistrationDate) + " s/d " +
                string.Format("{0:dd-MMM-yyyy}", regis.DischargeDate.HasValue ? regis.DischargeDate.Value : DateTime.Now);
            textBox129.Visible = (srRegType == "IPR");
            textBox131.Visible = (srRegType == "IPR");

            textBox13.Value += srRegType == "IPR" ? " PERAWATAN" : (" PEMERIKSAAN RAWAT JALAN"); // + svu.ServiceUnitName);
            textBox50.Value = srRegType == "IPR" ? "Tgl. Masuk" : "Tgl. Periksa";

            var uLiftUp = textBox50.Height;
            textBox121.Visible =
                textBox122.Visible =
                textBox123.Visible =
                textBox126.Visible =
                textBox127.Visible =
                textBox128.Visible =
                (srRegType == "IPR");

            // cari no reg RI jika pasien dirujuk rawat inap.
            if (srRegType == "IPR")
            {

            }
            else
            {
                var mb = new MergeBilling();
                if (mb.LoadByPrimaryKey(regis.RegistrationNo))
                {
                    var r = mb.FromRegistrationNo;
                    var regTo = new Registration();
                    if (regTo.LoadByPrimaryKey(r))
                    {
                        textBox121.Visible =
                            textBox122.Visible =
                            textBox123.Visible =
                            textBox126.Visible =
                            textBox127.Visible =
                            textBox128.Visible = true;

                        // create border
                        textBox121.Style.BorderStyle.Top =
                            textBox122.Style.BorderStyle.Top =
                            textBox123.Style.BorderStyle.Top = BorderType.Dotted;
                        textBox121.Style.BorderStyle.Left =
                            textBox126.Style.BorderStyle.Left = BorderType.Dotted;
                        textBox123.Style.BorderStyle.Right =
                            textBox128.Style.BorderStyle.Right = BorderType.Dotted;
                        textBox126.Style.BorderStyle.Bottom =
                            textBox127.Style.BorderStyle.Bottom =
                            textBox128.Style.BorderStyle.Bottom = BorderType.Dotted;

                        textBox121.Value = (regTo.SRRegistrationType == "IPR") ? "No.Reg Rawat Inap" : "No.Reg Asal";
                        textBox123.Value = regTo.RegistrationNo;

                        textBox126.Value = (regTo.SRRegistrationType == "IPR") ? "Ruang Rawat / Bed" : "Ruang";
                        var svuri = new ServiceUnit();
                        if (svuri.LoadByPrimaryKey(regTo.ServiceUnitID))
                        {
                            textBox128.Value = (regTo.SRRegistrationType == "IPR") ? (svuri.ServiceUnitName + " / " + regTo.BedID) : svuri.ServiceUnitName;
                        }
                    }
                    else
                    {
                        //// hhilangkan gap rj terlalu jauh
                        //pageHeaderSection1.Height = srRegType == "IPR" ? pageHeaderSection1.Height : (pageHeaderSection1.Height - uLiftUp * 2);
                    }
                }
            }

            var healthcare = Healthcare.GetHealthcare();
            
            textBox110.Value = healthcare.City;
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
        }
    }
}