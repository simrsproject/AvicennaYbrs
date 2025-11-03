using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Net;
using System.IO;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.Reports
{
    [System.Web.Script.Services.ScriptService]
    public partial class PdfUrlViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //// pdf.js tidak jalan di firefox versi terakhir
                //string sourceFileName = null;
                //var array = LoadToPdf(Request.QueryString["mode"], Request.QueryString["id"].ToInt(), Request.QueryString["trno"], string.IsNullOrWhiteSpace(Request.QueryString["trno"]) ? string.Empty : Request.QueryString["seqno"], ref sourceFileName);
                //if (array != null)
                //{
                //    pdfViewer.PdfjsProcessingSettings.FileSettings.Data = Convert.ToBase64String(array);
                //}

                // diganti dengan viewer bawaan browser (adobe
                //string sourceFileName = null;
                //var fileBuffer = LoadToPdf(Request.QueryString["mode"], Request.QueryString["id"].ToInt(), Request.QueryString["trno"], string.IsNullOrWhiteSpace(Request.QueryString["trno"]) ? string.Empty : Request.QueryString["seqno"], ref sourceFileName);

                //if (fileBuffer != null)
                //{
                //    Response.ContentType = "application/pdf";
                //    Response.AddHeader("content-length", fileBuffer.Length.ToString());
                //    Response.BinaryWrite(fileBuffer);
                //}

                if (Request.QueryString["mode"].ToString() == "empdoc" || Request.QueryString["mode"].ToString() == "renkindoc")
                {
                    btnSaveToGuarantor.Visible = false;
                }
            }
        }

        //private static byte[] LoadToPdf(string mode, int id, string trno, string seqno, ref string sourceFileName)
        //{
        //    switch (mode)
        //    {
        //        case "patdoc":
        //            {
        //                var ent = new PatientDocument();
        //                if (ent.LoadByPrimaryKey(id))
        //                {
        //                    // Berubah cara ambil filenya krn file bisa diset berada diluar aplikasi
        //                    //string urlFile = string.Format("{2}/App_Document/PatientDocument/{0}/{1}", ent.PatientID.Trim(), ent.FileAttachName, Helper.BaseSiteUrl);
        //                    //pdfViewer.PdfjsProcessingSettings.File = urlFile;

        //                    // Baca file pdf
        //                    var filePath = (ent.IsUpload ?? false) ?
        //                    Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim(), ent.FileAttachName)
        //                    : ent.OriPath;

        //                    //Return sourceFileName
        //                    sourceFileName = Path.GetFileName(filePath);

        //                    byte[] array = File.ReadAllBytes(filePath);
        //                    return array;
        //                }
        //                break;
        //            }
        //        case "lab":
        //            {
        //                var urlFile = string.Format("{0}/{1}.pdf", System.Configuration.ConfigurationManager.AppSettings.Get("LabFileResultUrlRoot"), trno);
        //                try
        //                {
        //                    using (WebClient client = new WebClient())
        //                    {
        //                        using (Stream ms = new MemoryStream(client.DownloadData(urlFile)))
        //                        {
        //                            MemoryStream mStream = new MemoryStream();
        //                            mStream.SetLength(ms.Length);
        //                            ms.Read(mStream.GetBuffer(), 0, (int)ms.Length);

        //                            //Return sourceFileName
        //                            sourceFileName = string.Format("{0}.pdf", trno);
        //                            return mStream.ToArray();
        //                        }
        //                    }
        //                }
        //                catch (Exception)
        //                {
        //                    //pdfViewer.PdfjsProcessingSettings.File = urlFile;
        //                }
        //                break;
        //            }
        //        case "rad":
        //            {
        //                switch (AppSession.Parameter.HealthcareInitial)
        //                {
        //                    case "RSTJ":
        //                        var urlFile = string.Format("{0}/{1}", System.Configuration.ConfigurationManager.AppSettings.Get("RadFileResultUrlRoot"), trno);
        //                        try
        //                        {
        //                            using (WebClient client = new WebClient())
        //                            {
        //                                using (Stream ms = new MemoryStream(client.DownloadData(urlFile)))
        //                                {
        //                                    MemoryStream mStream = new MemoryStream();
        //                                    mStream.SetLength(ms.Length);
        //                                    ms.Read(mStream.GetBuffer(), 0, (int)ms.Length);

        //                                    //Return sourceFileName
        //                                    sourceFileName = string.Format("{0}.pdf", seqno);
        //                                    return mStream.ToArray();
        //                                }
        //                            }
        //                        }
        //                        catch (Exception)
        //                        {
        //                            //pdfViewer.PdfjsProcessingSettings.File = urlFile;
        //                        }
        //                        break;
        //                }
        //                break;
        //            }
        //        case "eklaim":
        //            {
        //                var svc = new Common.Inacbg.v51.Service();
        //                var print = svc.Print(new Temiang.Avicenna.Common.Inacbg.v51.Claim.Create.Data() { nomor_sep = trno });
        //                if (print.Metadata.IsValid)
        //                {
        //                    sourceFileName = string.Format("{0}.pdf", trno);
        //                    return Convert.FromBase64String(print.Data);
        //                }
        //                else
        //                {
        //                    //ScriptManager.RegisterStartupScript(this, GetType(), "print", string.Format("alert('{0} - {1}');", print.Metadata.Code, print.Metadata.Message), true);
        //                }

        //                break;
        //            }
        //    }
        //    return null;
        //}

        [WebMethod()]
        public static string SaveToGuarantorDoc(string mode, string id)
        {
            var regNo = string.Empty;
            var sepNo = string.Empty;
            if (mode == "eklaim")
            {
                var regColl = new RegistrationCollection();
                regColl.Query.Where(regColl.Query.BpjsSepNo == id);
                if (regColl.LoadAll())
                {
                    // kalau reg lebih dari satu karena salah entry SEP gmn?
                    if (regColl.Count > 1)
                    {
                        regNo = regColl.First().RegistrationNo;
                        sepNo = regColl.First().BpjsSepNo;
                    }
                    else
                    {
                        regNo = regColl.First().RegistrationNo;
                        sepNo = regColl.First().BpjsSepNo;
                    }
                }
            }
            else
            {
                var tc = new TransCharges();
                tc.LoadByPrimaryKey(id);
                regNo = tc.RegistrationNo;
            }

            string filePath = string.Empty;
            string path = string.Empty;

            string fileName = null;
            var programCategory = string.Empty;

            var isInteger = int.TryParse(id, out var idd);

            var datas = PdfUrlViewerHandler.LoadToPdf(mode, isInteger ? id.ToInt() : 0, id, string.Empty, string.Empty, ref fileName);

            //var df = new DocumentFiles();
            //df.LoadByPrimaryKey(dfId.ToInt());

            string regType = null;
            string guarantorID = null;

            if (mode == "lab")
                fileName = "LAB" + fileName;
            if (mode == "eklaim")
                fileName = "EKLAIM_" + fileName;

            filePath = Reports.ReportViewer.GuarantorDocumentFilePath(regNo, string.Empty, fileName, string.Empty, ref regType, ref guarantorID, sepNo);

            var docExistCount = 0;
            try
            {

                path = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // Save File
                File.WriteAllBytes(filePath, datas);

                //// Save Checklist
                //var rdcl = new BusinessObject.RegistrationDocumentCheckList();
                //if (!rdcl.LoadByPrimaryKey(tc.RegistrationNo, dfId.ToInt())) rdcl = new BusinessObject.RegistrationDocumentCheckList();

                //rdcl.RegistrationNo = tc.RegistrationNo;
                //rdcl.DocumentFilesID = dfId.ToInt();
                //rdcl.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //rdcl.LastUpdateDateTime = DateTime.Now;
                //rdcl.FileName = filePath;
                //rdcl.Save();

                //// Jumlah document yg dianggap sudah ada
                //var rdclColl = new RegistrationDocumentCheckListCollection();
                //rdclColl.Query.Where(rdclColl.Query.RegistrationNo == tc.RegistrationNo);
                //rdclColl.LoadAll();
                //docExistCount = rdclColl.Count;

                //var regInfoCount = new RegistrationInfoSumary();
                //if (!regInfoCount.LoadByPrimaryKey(tc.RegistrationNo))
                //{
                //    regInfoCount.AddNew();
                //    regInfoCount.RegistrationNo = tc.RegistrationNo;
                //    regInfoCount.NoteCount = 0;
                //    regInfoCount.NoteMedicalCount = 0;
                //}
                //regInfoCount.DocumentCheckListCount = docExistCount;
                //regInfoCount.Save();

                return string.Format("File has save to {0}", filePath);
            }
            catch (Exception ex)
            {
                var log = new WebServiceAPILog();
                log.DateRequest = DateTime.Now;
                log.IPAddress = string.Empty;
                log.UrlAddress = "PdfUrlViewer";
                log.Params = JsonConvert.SerializeObject(new
                {
                    filePath,
                    path
                });
                log.Response = ex.Message;
                log.Save();

                return ex.Message;
            }

            return "Save file failed";
        }

    }
}