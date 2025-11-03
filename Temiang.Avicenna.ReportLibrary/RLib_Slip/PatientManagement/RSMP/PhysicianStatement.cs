using System.Linq;
using Telerik.Reporting;
using System.Data;
using System;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.RSMP
{
    using BusinessObject;
    //using Temiang.Avicenna.Common;

    public partial class PhysicianStatement : Report
    {
        public PhysicianStatement(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogoOnlySizeModeNormal(this.reportHeaderSection1);

            var healthcare = Healthcare.GetHealthcare();
            
            TxtNameRS.Value = healthcare.HealthcareName;
            TxtCityRS.Value = healthcare.AddressLine1; 
            textBox113.Value = healthcare.City;
            textBox114.Value = "Email : " + healthcare.EmailAddr;
            textBox115.Value = "Telp. " + healthcare.PhoneNo;
            textBox33.Value = "Fax. " + healthcare.FaxNo;

            string registrationNo = printJobParameters.FindByParameterName("RegistrationNo").ValueString;
            string userName = printJobParameters.FindByParameterName("UserName").ValueString;

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(registrationNo);
            var pas = new BusinessObject.Patient();
            pas.LoadByPrimaryKey(reg.PatientID);
            var guar = new Guarantor();
            guar.LoadByPrimaryKey(reg.GuarantorID);
            textBox39.Value = guar.GuarantorName;
            textBox40.Value = pas.PatientName;
            textBox41.Value = pas.PatientName;
            textBox42.Value = reg.GuarantorCardNo;
            textBox43.Value = pas.Sex.ToUpper().Trim() == "M" ? "L":"P";
            textBox44.Value = reg.InsuranceID;
            textBox58.Value = pas.MedicalNo;
            textBox57.Value = string.Format("{0:dd/MMM/yyyy}", pas.DateOfBirth);
            textBox65.Value = string.Format("{0:dd/MMM/yyyy}", reg.RegistrationDate.Value);
            textBox67.Value = reg.DischargeDate.HasValue ? string.Format("{0:dd/MMM/yyyy}", reg.DischargeDate.Value) : "";

            if (reg.SRRegistrationType == "IPR")
            {
                //this.textBox82.Style.BackgroundImage.ImageData = global::Temiang.Avicenna.ReportLibrary.Properties.Resources.circle_checked16x16;
                this.textBox59.Value = "RAWAT INAP";
            }
            else
            {
               // this.textBox82.Style.BackgroundImage.ImageData = global::Temiang.Avicenna.ReportLibrary.Properties.Resources.circle_checked16x16;
                this.textBox59.Value = "RAWAT JALAN";

                if (reg.ServiceUnitID == "D01.C01")
                {
                    textBox84.Style.BackgroundImage.ImageData = textBox31.Style.BackgroundImage.ImageData;
                }
                else
                    if (reg.ServiceUnitID == "D01.C18")
                    {
                        textBox101.Style.BackgroundImage.ImageData = textBox31.Style.BackgroundImage.ImageData;
                    }
                    else
                    {
                        // khusus poli imunisasi jangan dicentang, permintaan bu nar rsch
                        if (reg.ServiceUnitID == "D01.C32") { 
                            // do nothing
                        }else
                        {
                            textBox100.Style.BackgroundImage.ImageData = textBox31.Style.BackgroundImage.ImageData;
                        }
                    }
            }
            
           
            
            var soapColl = new BusinessObject.EpisodeSOAPECollection();
            soapColl.Query.Where(soapColl.Query.RegistrationNo == reg.RegistrationNo, soapColl.Query.IsVoid == false,
                    soapColl.Query.Or(soapColl.Query.Imported.IsNull(), soapColl.Query.Imported == false));
            soapColl.LoadAll();
            
            DateTime tglSoap = DateTime.Now;

            textBox69.Value = string.Empty;
            foreach(var x in soapColl){
                if(x.Subjective.Trim() != string.Empty){
                    textBox69.Value = (textBox69.Value == string.Empty ? "":", ") + x.Subjective;
                    tglSoap = x.SOAPEDate.Value;
                }
            }

            if (string.IsNullOrEmpty(textBox69.Value))
            {
                //From Table RegistrationInfoMedic
                var rimColl = new RegistrationInfoMedicCollection();
                rimColl.Query.Where(
                    rimColl.Query.RegistrationNo == reg.RegistrationNo,
                    rimColl.Query.Or(rimColl.Query.IsDeleted.IsNull(), rimColl.Query.IsDeleted == false)
                    );
                rimColl.LoadAll();

                foreach (var rim in rimColl)
                {
                    if (rim.Info1.Trim() != string.Empty)
                    {
                        textBox69.Value = (textBox69.Value == string.Empty ? "" : ", ") + rim.Info1;
                        tglSoap = rim.DateTimeInfo.Value;
                    }
                }
            }

            if (reg.SRRegistrationType == "IPR") {
                textBox72.Value = textBox69.Value;
            }
            textBox74.Value = string.Empty;
            foreach (var x in soapColl)
            {
                if (x.Objective.Trim() != string.Empty)
                {
                    textBox74.Value = (textBox74.Value == string.Empty ? "" : ", ") + x.Objective;
                }
            }
            textBox76.Value = string.Empty;
            foreach (var x in soapColl)
            {
                if (x.Assesment.Trim() != string.Empty)
                {
                    textBox76.Value = (textBox76.Value == string.Empty ? "" : ", ") + x.Assesment.Replace("<br />", " ");
                }
            }
            textBox78.Value = string.Empty;
            foreach (var x in soapColl)
            {
                if (x.Planning.Trim() != string.Empty)
                {
                    textBox78.Value = (textBox78.Value == string.Empty ? "" : ", ") + x.Planning.Replace("<br />"," ");
                }
            }

            // cari pemeriksaan lab
            var tcQuery = new TransChargesQuery("a");
            var suQuery = new ServiceUnitQuery("b");
            tcQuery.InnerJoin(suQuery).On(tcQuery.ToServiceUnitID == suQuery.ServiceUnitID);
            tcQuery.Where(
                tcQuery.RegistrationNo == reg.RegistrationNo,
                tcQuery.IsOrder == true,
                tcQuery.IsVoid == false);
            tcQuery.Select(tcQuery.ToServiceUnitID, suQuery.ServiceUnitName);
            tcQuery.es.Distinct = true;

            var dttc = tcQuery.LoadDataTable();
            textBox1.Value = string.Empty;
            foreach (System.Data.DataRow r in dttc.Rows) {
                textBox74.Value = textBox74.Value + (textBox74.Value == string.Empty ? "" : ", ") + r["ServiceUnitName"].ToString().Trim();
            }
            // cari operasi
            var ap = new AppParameter();
            ap.LoadByPrimaryKey("ServiceUnitOperationRoomID");

            var tc = new TransChargesQuery("a");
            var tci = new TransChargesItemQuery("b");
            var i = new ItemQuery("c");

            tc.InnerJoin(tci).On(tc.TransactionNo == tci.TransactionNo);
            tc.InnerJoin(i).On(tci.ItemID == i.ItemID && i.SRItemType == "11");
            tc.Where(tc.RegistrationNo == reg.RegistrationNo, tc.IsVoid == false, tc.FromServiceUnitID == ap.ParameterValue);
            tc.es.Distinct = true;
            tc.Select(i.ItemName);

            var tab = tc.LoadDataTable();
            foreach (System.Data.DataRow r in tab.Rows)
            {
                textBox1.Value = textBox1.Value + (textBox1.Value == string.Empty ? "" : ", ");
            }

            // diagnosa
            var ed = new EpisodeDiagnoseQuery("a");
            var d = new DiagnoseQuery("b");

            ed.InnerJoin(d).On(ed.DiagnoseID == d.DiagnoseID && ed.IsVoid == false);
            ed.Where(ed.RegistrationNo == reg.RegistrationNo);
            ed.es.Top = 3;
            ed.Select(d.DiagnoseName);

            var tab2 = ed.LoadDataTable();
            if (tab2.Rows.Count > 0)
            {
                for (int n = 0; n < tab2.Rows.Count; n++)
                {
                    if (n == 0)
                        textBox76.Value = tab2.Rows[n]["DiagnoseName"].ToString();
                    else if (n == 1)
                        textBox29.Value = tab2.Rows[n]["DiagnoseName"].ToString();
                    else
                        textBox34.Value = tab2.Rows[n]["DiagnoseName"].ToString();
                }
            }

            // pernyataan pemberi kuasa
            textBox22.Value = "Saya menyatakan bahwa saya telah membaca, " +
                "mengerti dan menjawab pertanyaan tersebut di atas dengan lengkap dan benar. " +
                "Dengan ini saya memberi kuasa kepada setiap dokter, rumah sakit, klinik, puskesmas, " +
                "perusahaan asuransi dan badan hukum, perorangan atau organisasi lainnya " +
                "yang mempunyai catatan atau mengetahui keadaan kesehatan saya " +
                "untuk memberitahukan kepada Perusahaan / Instansi: " + guar.GuarantorName + " " +
                "atau kuasa yang diberi kuasa olehnya, segala keterangan mengenai diri dan kesehatan saya. " + System.Environment.NewLine +
                "Copy dari pernyataan ini sama kuat dan sahnya seperti aslinya.";

            var par = new Paramedic();
            par.LoadByPrimaryKey(reg.ParamedicID);
            textBox26.Value = par.ParamedicName;

            textBox23.Value = healthcare.City + ", " + string.Format("{0:dd/MMM/yyyy}", tglSoap);
            textBox26.Value = par.ParamedicName;
        }
    }
}