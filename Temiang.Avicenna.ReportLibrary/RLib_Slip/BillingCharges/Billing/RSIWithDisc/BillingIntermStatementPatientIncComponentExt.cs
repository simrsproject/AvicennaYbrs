using System.Linq;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing.RSIWithDisc
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
    public partial class BillingIntermStatementPatientIncComponentExt : Telerik.Reporting.Report
    {
        public BillingIntermStatementPatientIncComponentExt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoOnly(this.pageHeaderSection1);

            string registrationNo = printJobParameters.FindByParameterName("RegistrationNoList").ValueString;
            string[] registrationNoList = new string[1];
            if (registrationNo.Contains(","))
                registrationNoList = registrationNo.Split(',');
            else
                registrationNoList.SetValue(registrationNo, 0);

            var regs = new RegistrationCollection();
            regs.Query.Where(regs.Query.RegistrationNo.In(registrationNoList));
            regs.LoadAll();

            textBox131.Value = printJobParameters.FindByParameterName("UserName").ValueString;

            var regis = new Registration();
            regis.LoadByPrimaryKey(printJobParameters.FindByParameterName("RegNo").ValueString);

            string srRegType = regis.SRRegistrationType;

            var p = new Patient();
            p.LoadByPrimaryKey(regis.PatientID);
            var sal = new AppStandardReferenceItem();
            if (sal.LoadByPrimaryKey("Salutation", p.SRSalutation))
                textBox10.Value = sal.ItemName + " " + p.PatientName;
            else
                textBox10.Value = p.PatientName;
            textBox12.Value = printJobParameters.FindByParameterName("RegNo").ValueString + " / " + p.MedicalNo;
            textBox116.Value = string.Format("{0:dd-MMM-yyyy}", p.DateOfBirth);

            var dr = new Paramedic();
            if (regis.ParamedicID != null)
            {
                if (dr.LoadByPrimaryKey(regis.ParamedicID))
                {
                    textBox117.Value = dr.ParamedicName;
                }
            }
            var svr = new ServiceRoom();
            if (svr.LoadByPrimaryKey(regis.RoomID))
            {
                textBox11.Value = svr.RoomName;
            }
            var svu = new ServiceUnit();
            if (svu.LoadByPrimaryKey(regis.ServiceUnitID))
            {
                if (srRegType == "IPR")
                {
                    textBox11.Value = svu.ServiceUnitName + ' ' + textBox11.Value;
                }
            }
            // kelas, no tempat tidur
            var cls = new Temiang.Avicenna.BusinessObject.Class();
            if (cls.LoadByPrimaryKey(regis.ClassID))
            {
                if (srRegType == "IPR")
                {
                    textBox9.Value = cls.ClassName + " / " + regis.BedID;
                }
                else
                {
                    textBox9.Value = cls.ClassName + " / " + textBox9.Value;
                }
            }
            // hak kelas
            var clsCvr = new Temiang.Avicenna.BusinessObject.Class();
            if (clsCvr.LoadByPrimaryKey(regis.CoverageClassID))
            {
                textBox128.Value = clsCvr.ClassName;
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
            textBox48.Value = string.Format("{0:dd-MMM-yyyy hh:mm}",
                regis.RegistrationDate.Value
                    .AddHours(System.Convert.ToDouble(regis.RegistrationTime.Substring(0, 2)))
                    .AddMinutes(System.Convert.ToDouble(regis.RegistrationTime.Substring(3, 2)))
            );
            textBox123.Value = string.Format("{0:dd-MMM-yyyy hh:mm}", regis.DischargeDate.HasValue ?
                regis.DischargeDate.Value
                    .AddHours(System.Convert.ToDouble(regis.DischargeTime.Substring(0, 2)))
                    .AddMinutes(System.Convert.ToDouble(regis.DischargeTime.Substring(3, 2))) :
                DateTime.Now);

            // header tgl mulai sampai
            textBox129.Value = "Periode " + (srRegType == "IPR" ? "Perawatan" : "Pemeriksaan") + " : " +
                string.Format("{0:dd-MMM-yyyy}", regis.RegistrationDate) + " s/d " +
                string.Format("{0:dd-MMM-yyyy}", regis.DischargeDate.HasValue ? regis.DischargeDate.Value : DateTime.Now);
            textBox129.Visible = (srRegType == "IPR");
            textBox131.Visible = (srRegType == "IPR");

            textBox13.Value += srRegType == "IPR" ? " PERAWATAN" : " PEMERIKSAAN";
            textBox50.Value = srRegType == "IPR" ? "Tanggal Masuk" : "Tanggal Periksa";
            textBox2.Value = srRegType == "IPR" ? "Ruang Perawatan" : "Ruang Pemeriksaan";

            // kelas ruang
            textBox4.Value = srRegType == "IPR" ? "Kelas / No.Tempat Tidur" : "Kelas / Poliklinik";

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

                        textBox121.Value = "No.Reg Rawat Inap";
                        textBox123.Value = regTo.RegistrationNo;

                        textBox126.Value = "Ruang Rawat / Bed";
                        var svuri = new ServiceUnit();
                        if (svuri.LoadByPrimaryKey(regTo.ServiceUnitID))
                        {
                            textBox128.Value = svuri.ServiceUnitName + " / " + regTo.BedID;
                        }
                        // hhilangkan gap rj terlalu jauh
                        pageHeaderSection1.Height = srRegType == "IPR" ? pageHeaderSection1.Height : (pageHeaderSection1.Height - new Unit(0.2, UnitType.Inch));
                    }
                    else
                    {
                        // hhilangkan gap rj terlalu jauh
                        pageHeaderSection1.Height = srRegType == "IPR" ? pageHeaderSection1.Height : (pageHeaderSection1.Height - new Unit(0.4, UnitType.Inch));
                    }
                }
            }

            var healthcare = Healthcare.GetHealthcare();
            
            textBox24.Value = healthcare.HealthcareName;
            textBox110.Value = healthcare.City;
            textBox28.Value = healthcare.AddressLine1 + (srRegType == "IPR" ? " " + healthcare.City : "");
            textBox29.Value = srRegType == "IPR" ? "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo : healthcare.City;
            var Email = string.Empty;
            var EmParam = new AppParameter();
            if (EmParam.LoadByPrimaryKey("HealthcareFinanceEmailAddr"))
                Email = EmParam.ParameterValue;
            textBox30.Value = srRegType == "IPR" ? "Email: " + healthcare.EmailAddr : "Telp. " + healthcare.PhoneNo + " Fax " + healthcare.FaxNo;


            DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            this.DataSource = DataSource;
            //this.DataSource = table;
        }
    }
}