using System.Linq;
using Telerik.Reporting;
using System.Data;
using System;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement
{
    using BusinessObject;
    //using Temiang.Avicenna.Common;

    public partial class PhysicianStatement : Report
    {
        public PhysicianStatement(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            Helper.InitializeLogoOnlySizeModeNormal(this.pageHeaderSection1);

            var healthcare = Healthcare.GetHealthcare();
            TxtNameRS.Value = healthcare.HealthcareName;
            TxtCityRS.Value = healthcare.AddressLine1;
            textBox113.Value = healthcare.City;
            txtEmail.Value = string.IsNullOrEmpty(healthcare.EmailAddr) ? string.Empty : "Email : " + healthcare.EmailAddr.Trim();
            txtFax.Value = string.IsNullOrEmpty(healthcare.FaxNo) ? string.Empty : "Fax. " + healthcare.FaxNo;
            txtPhone.Value = string.IsNullOrEmpty(healthcare.PhoneNo) ? string.Empty : "Telp. " + healthcare.PhoneNo;

            string registrationNo = printJobParameters.FindByParameterName("p_RegistrationNo").ValueString;
            string userName = printJobParameters.FindByParameterName("p_UserName").ValueString;

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(registrationNo);
            var pas = new BusinessObject.Patient();
            pas.LoadByPrimaryKey(reg.PatientID);
            var guar = new Guarantor();
            guar.LoadByPrimaryKey(reg.GuarantorID);
            var medsum = new MedicalDischargeSummary();
            medsum.LoadByPrimaryKey(registrationNo);
            //var au = new AppUser();
            //au.LoadByPrimaryKey(au.ParamedicID);
            textBox39.Value = guar.GuarantorName;
            textBox40.Value = pas.PatientName;
            textBox41.Value = pas.PatientName;
            textBox42.Value = reg.GuarantorCardNo;
            textBox43.Value = pas.Sex.ToUpper().Trim() == "M" ? "L" : "P";
            textBox44.Value = reg.InsuranceID;
            textBox58.Value = pas.MedicalNo;
            textBox57.Value = string.Format("{0:dd/MMM/yyyy}", pas.DateOfBirth);
            textBox65.Value = string.Format("{0:dd/MMM/yyyy}", reg.RegistrationDate.Value);
            textBox67.Value = reg.DischargeDate.HasValue ? string.Format("{0:dd/MMM/yyyy}", reg.DischargeDate.Value) : "";

            //if(medsum.PpaSign != null)
            //{
            //    Image image;
            //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(medsum.PpaSign))
            //    {
            //        image = Image.FromStream(ms);
            //    }
            //    this.pictureBox1.Value = image;
            //}


            if (reg.SRRegistrationType == "IPR")
            {
                //this.textBox82.Style.BackgroundImage.ImageData = global::Temiang.Avicenna.ReportLibrary.Properties.Resources.circle_checked16x16;
                this.textBox59.Value = "RAWAT INAP";
            }
            else
            {
                // this.textBox82.Style.BackgroundImage.ImageData = global::Temiang.Avicenna.ReportLibrary.Properties.Resources.circle_checked16x16;
                this.textBox59.Value = "RAWAT JALAN";

                //if (reg.ServiceUnitID == "D01.C01")
                //{
                //    txtChkDokterUmum.Style.BackgroundImage.ImageData = textBox31.Style.BackgroundImage.ImageData;
                //}
                //else
                //    if (reg.ServiceUnitID == "D01.C18")
                //    {
                //        txtChkDokterGigi.Style.BackgroundImage.ImageData = textBox31.Style.BackgroundImage.ImageData;
                //    }
                //    else
                //    {
                //        // khusus poli imunisasi jangan dicentang, permintaan bu nar rsch
                //        if (reg.ServiceUnitID == "D01.C32") { 
                //            // do nothing
                //        }else
                //        {
                //            txtChkDokterSpecialis.Style.BackgroundImage.ImageData = textBox31.Style.BackgroundImage.ImageData;
                //        }
                //    }

                var med = new Paramedic();
                med.LoadByPrimaryKey(reg.ParamedicID);

                var refItem = new AppStandardReferenceItem();
                if (refItem.LoadByPrimaryKey("ParamedicType", med.SRParamedicType))
                {
                    if (refItem.ReferenceID == "DENT")
                        txtChkDokterGigi.Style.BackgroundImage.ImageData = textBox31.Style.BackgroundImage.ImageData;
                    else if (refItem.ReferenceID == "SPCL")
                        txtChkDokterSpecialis.Style.BackgroundImage.ImageData =
                            textBox31.Style.BackgroundImage.ImageData;
                    else
                        txtChkDokterUmum.Style.BackgroundImage.ImageData = textBox31.Style.BackgroundImage.ImageData;
                }
            }

            var rimQr = new RegistrationInfoMedicQuery("q");
            rimQr.es.Top = 1;
            rimQr.Where(rimQr.RegistrationNo == registrationNo && rimQr.SRMedicalNotesInputType == "SOAP");
            var rim = new RegistrationInfoMedic();

            var isIntegratedNoteSoapExist = rim.Load(rimQr);

            var tglSoap = isIntegratedNoteSoapExist ? PopulateFromIntegratedNote(reg) : PopulateFromEpisodeSoap(reg);

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
            foreach (System.Data.DataRow r in dttc.Rows)
            {
                textBox1.Value = textBox1.Value + (textBox1.Value == string.Empty ? "" : ", ") + r["ServiceUnitName"].ToString().Trim();
            }
            // cari nama unit order


            // pernyataan pemberi kuasa
            textBox22.Value = "Saya menyatakan bahwa saya telah membaca, " +
                "mengerti dan menjawab pertanyaan tersebut di atas dengan lengkap dan benar. " +
                "Dengan ini saya memberi kuasa kepada setiap dokter, rumah sakit, klinik, puskesmas, " +
                "perusahaan asuransi dan badan hukum, perorangan atau organisasi lainnya " +
                "yang mempunyai catatan atau mengetahui keadaan kesehatan saya " +
                "untuk memberitahukan kepada Perusahaan / Instansi: " + guar.GuarantorName + " " +
                "atau kuasa yang diberi kuasa olehnya, segala keterangan mengenai diri dan kesehatan saya. " + System.Environment.NewLine +
                "Copy dari pernyataan ini sama kuat dan sahnya seperti aslinya.";

            var au = new AppUserQuery("a");
            var par = new ParamedicQuery("b");
            au.LeftJoin(par).On(au.ParamedicID == par.ParamedicID);
            au.Select(par.ParamedicName, au.ParamedicID, au.SignatureImage, au.SRUserType);
            au.Where(au.SRUserType == "DTR", au.SignatureImage.IsNotNull(), par.ParamedicID == reg.ParamedicID);
            au.OrderBy(au.LastUpdateDateTime.Ascending);

            var dtb = au.LoadDataTable();

            DataRow rowDpjp = dtb.Rows[0];
            byte[] imgDpjp = rowDpjp["SignatureImage"] as byte[];

            imgDpjp = imgDpjp ?? new byte[0];

            pictureBox1.Value = Convert.ToBase64String(imgDpjp);
            textBox26.Value = rowDpjp["ParamedicName"]?.ToString() ?? "No name available";

            //if (dtb.Rows.Count > 0)
            //{
            //    var row = dtb.Rows[0];

            //    // Check if SignatureImage is not null or DBNull
            //    if (row["SignatureImage"] != DBNull.Value)
            //    {
            //        byte[] imageBytes = (byte[])row["SignatureImage"];
            //        using (MemoryStream ms = new MemoryStream(imageBytes))
            //        {
            //            pictureBox1.Value = Image.FromStream(ms);
            //        }
            //    }
            //    else
            //    {
            //        pictureBox1.Value = null;  // Handle null case if needed
            //    }

            //    // Set the Paramedic Name
            //    textBox26.Value = row["ParamedicName"]?.ToString() ?? "No name available";
            ////}
            //pictureBox1.Value = au.SignatureImage;

            //textBox26.Value = par.ParamedicName;

            textBox23.Value = healthcare.City + ", " + string.Format("{0:dd/MMM/yyyy}", tglSoap);
            //textBox26.Value = par.ParamedicName;
        }

        private DateTime PopulateFromEpisodeSoap(BusinessObject.Registration reg)
        {
            var soapColl = new BusinessObject.EpisodeSOAPECollection();
            soapColl.Query.Where(soapColl.Query.RegistrationNo == reg.RegistrationNo, soapColl.Query.IsVoid == false);
            soapColl.LoadAll();

            DateTime tglSoap = DateTime.Now;

            textBox69.Value = string.Empty;
            foreach (var x in soapColl)
            {
                if (x.Subjective.Trim() != string.Empty)
                {
                    textBox69.Value = (textBox69.Value == string.Empty ? "" : ", ") + x.Subjective;
                    tglSoap = x.SOAPEDate.Value;
                }
            }
            if (reg.SRRegistrationType == "IPR")
            {
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
                    textBox78.Value = (textBox78.Value == string.Empty ? "" : ", ") + x.Planning.Replace("<br />", " ");
                }
            }
            return tglSoap;
        }

        private DateTime PopulateFromIntegratedNote(BusinessObject.Registration reg)
        {
            var soapColl = new BusinessObject.RegistrationInfoMedicCollection();
            soapColl.Query.Where(soapColl.Query.RegistrationNo == reg.RegistrationNo, soapColl.Query.SRMedicalNotesInputType=="SOAP", soapColl.Query.Or(soapColl.Query.IsDeleted.IsNull(), soapColl.Query.IsDeleted == false));
            soapColl.LoadAll();

            DateTime tglSoap = DateTime.Now;

            textBox69.Value = string.Empty;
            foreach (var x in soapColl)
            {
                if (x.Info1.Trim() != string.Empty)
                {
                    textBox69.Value = (textBox69.Value == string.Empty ? "" : ", ") + x.Info1; // Subjective;
                    tglSoap = x.DateTimeInfo.Value;
                }
            }
            if (reg.SRRegistrationType == "IPR")
            {
                textBox72.Value = textBox69.Value;
            }
            textBox74.Value = string.Empty;
            foreach (var x in soapColl)
            {
                if (x.Info2.Trim() != string.Empty) //Objective
                {
                    textBox74.Value = (textBox74.Value == string.Empty ? "" : ", ") + x.Info2; //Objective;
                }
            }
            textBox76.Value = string.Empty;
            foreach (var x in soapColl)
            {
                if (x.Info3.Trim() != string.Empty) //Assesment
                {
                    textBox76.Value = (textBox76.Value == string.Empty ? "" : ", ") + x.Info3.Replace("<br />", " ");
                }
            }
            textBox78.Value = string.Empty;
            foreach (var x in soapColl)
            {
                if (x.Info4.Trim() != string.Empty || !string.IsNullOrEmpty(x.PrescriptionCurrentDay)) //Planning
                {
                    textBox78.Value = (textBox78.Value == string.Empty ? "" : ", ") + string.Concat(x.Info4," ",x.PrescriptionCurrentDay).Replace("<br />", " ");
                }
            }
            return tglSoap;
        }

    }
}