using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports
{
    /// <summary>
    /// Summary description for PdfUrlViewer1
    /// </summary>
    public class PdfUrlViewerHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string sourceFileName = null;
            var programCategory = string.Empty;
            byte[] fileBuffer = null;
            if (context.Request.QueryString["mode"] == "esign")
                fileBuffer = LoadESignDocHist(context.Request.QueryString["programId"], context.Request.QueryString["regNo"], ref sourceFileName);

            else
                fileBuffer = LoadToPdf(context.Request.QueryString["mode"], context.Request.QueryString["id"].ToInt(), context.Request.QueryString["trno"], string.IsNullOrWhiteSpace(context.Request.QueryString["trno"]) ? string.Empty : context.Request.QueryString["seqno"], context.Request.QueryString["accno"], ref sourceFileName);

            if (fileBuffer != null)
            {
                context.Response.ContentType = "application/pdf";
                context.Response.AddHeader("content-length", fileBuffer.Length.ToString());
                context.Response.BinaryWrite(fileBuffer);
            }
        }
        private static byte[] LoadESignDocHist(string programId, string regNo, ref string sourceFileName)
        {
            var esign = new AppProgramEsign();
            esign.LoadByPrimaryKey(programId);
            var urlFile = string.Format("{0}/{1}", esign.UrlRootHist, regNo);
            try
            {
                using (WebClient client = new WebClient())
                {
                    using (Stream ms = new MemoryStream(client.DownloadData(urlFile)))
                    {
                        MemoryStream mStream = new MemoryStream();
                        mStream.SetLength(ms.Length);
                        ms.Read(mStream.GetBuffer(), 0, (int)ms.Length);

                        //Return sourceFileName
                        sourceFileName = string.Format("RM{0}.pdf", regNo.Replace("/", ""));
                        return mStream.ToArray();
                    }
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        internal static byte[] LoadToPdf(string mode, int id, string trno, string seqno, string accno, ref string sourceFileName)
        {
            switch (mode)
            {
                case "patdoc":
                    {
                        var ent = new PatientDocument();
                        if (ent.LoadByPrimaryKey(id))
                        {
                            // Berubah cara ambil filenya krn file bisa diset berada diluar aplikasi
                            //string urlFile = string.Format("{2}/App_Document/PatientDocument/{0}/{1}", ent.PatientID.Trim(), ent.FileAttachName, Helper.BaseSiteUrl);
                            //pdfViewer.PdfjsProcessingSettings.File = urlFile;

                            // Baca file pdf
                            //var filePath = (ent.IsUpload ?? false) ?
                            //Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim(), ent.FileAttachName)
                            //: ent.OriPath;

                            var filePath = "";
                            if (ent.IsUpload ?? false)
                            {
                                //filePath = System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim(), ent.FileAttachName);

                                var fileFolderOld = System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim());
                                var fileFolderYearly = "";
                                if (!string.IsNullOrEmpty(ent.DocumentFolderYearly))
                                    fileFolderYearly = System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocumentYearly", ent.DocumentFolderYearly, ent.PatientID.Trim());

                                var fileFolder = fileFolderOld;
                                if (!System.IO.Directory.Exists(fileFolder))
                                {
                                    // jika old blm ada brarti pakai yearly
                                    fileFolder = string.IsNullOrEmpty(fileFolderYearly) ? fileFolderOld : fileFolderYearly;
                                }

                                filePath = System.IO.Path.Combine(fileFolder, ent.FileAttachName);
                            }
                            else
                            {
                                filePath = ent.OriPath;
                            }

                            //Return sourceFileName
                            sourceFileName = Path.GetFileName(filePath);

                            byte[] array = File.ReadAllBytes(filePath);
                            return array;
                        }
                        break;
                    }
                case "lab":
                case "labpa":
                    {
                        if (!string.IsNullOrWhiteSpace(AppSession.Parameter.PrefixOnoSysmexInterop))
                        {
                            // Download w prefix
                            try
                            {
                                var trnoWprefix = AppSession.Parameter.PrefixOnoSysmexInterop + trno;

                                var urlFile = string.Format("{0}/{1}.pdf",
                                    System.Configuration.ConfigurationManager.AppSettings.Get("LabFileResultUrlRoot"),
                                    trnoWprefix);

                                if (mode == "labpa")
                                    urlFile = string.Format("{0}/{1}.pdf",
                                        System.Configuration.ConfigurationManager.AppSettings.Get(
                                            "LabPaFileResultUrlRoot"), trnoWprefix);

                                sourceFileName = string.Format("{0}.pdf", trnoWprefix);
                                return DownloadFileToArrayByte(urlFile);
                            }
                            catch (Exception ex)
                            {
                                // Nothing
                            }
                        }

                        // Try Download w no prefix
                        try
                        {
                            var urlFile = string.Format("{0}/{1}.pdf", System.Configuration.ConfigurationManager.AppSettings.Get("LabFileResultUrlRoot"), trno);

                            if (mode == "labpa")
                                urlFile = string.Format("{0}/{1}.pdf", System.Configuration.ConfigurationManager.AppSettings.Get("LabPaFileResultUrlRoot"), trno);

                            sourceFileName = string.Format("{0}.pdf", trno);
                            return DownloadFileToArrayByte(urlFile);
                        }
                        catch (Exception ex)
                        {
                            // Nothing
                        }
                        break;
                    }
                case "rad":
                    {
                        string urlFile;

                        if (AppSession.Parameter.HealthcareInitial.Equals("RSTJ"))
                        {
                            urlFile = string.Format("{0}/{1}",
                               System.Configuration.ConfigurationManager.AppSettings.Get("RadFileResultUrlRoot"),
                               accno);
                            sourceFileName = string.Format("{0}.pdf", trno + '-' + seqno);
                        }
                        else
                        {
                            urlFile = string.Format("{0}/{1}.pdf",
                                System.Configuration.ConfigurationManager.AppSettings.Get("RadFileResultUrlRoot"),
                                trno);
                            sourceFileName = string.Format("{0}.pdf", trno);
                        }

                        try
                        {
                            return DownloadFileToArrayByte(urlFile);
                        }
                        catch (Exception)
                        {
                            //pdfViewer.PdfjsProcessingSettings.File = urlFile;
                        }
                        break;
                    }
                case "eklaim":
                    {
                        var svc = new Common.Inacbg.v51.Service();
                        var print = svc.Print(new Temiang.Avicenna.Common.Inacbg.v51.Claim.Create.Data() { nomor_sep = trno });
                        if (print.Metadata.IsValid)
                        {
                            sourceFileName = string.Format("{0}.pdf", trno);
                            return Convert.FromBase64String(print.Data);
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, GetType(), "print", string.Format("alert('{0} - {1}');", print.Metadata.Code, print.Metadata.Message), true);
                        }

                        break;
                    }
                case "empdoc":
                    {
                        var ent = new PersonalDocument();
                        if (ent.LoadByPrimaryKey(id))
                        {
                            // Baca file pdf
                            var filePath = (ent.IsUpload ?? false) ?
                            Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "PersonalDocument", ent.PersonID.ToString(), ent.FileAttachName)
                            : ent.OriPath;

                            //Return sourceFileName
                            sourceFileName = Path.GetFileName(filePath);

                            byte[] array = File.ReadAllBytes(filePath);
                            return array;
                        }
                        break;
                    }
                case "crddoc":
                    {
                        var ent = new CredentialProcessDocumentUpload();
                        if (ent.LoadByPrimaryKey(id))
                        {
                            // Baca file pdf
                            var filePath = (ent.IsUpload ?? false) ?
                            Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "CredentialingDocument", ent.TransactionNo.ToString(), ent.FileAttachName)
                            : ent.OriPath;

                            //Return sourceFileName
                            sourceFileName = Path.GetFileName(filePath);

                            byte[] array = File.ReadAllBytes(filePath);
                            return array;
                        }
                        break;
                    }
                case "renkindoc":
                    {
                        var ent = new PerformancePlanDocument();
                        if (ent.LoadByPrimaryKey(id))
                        {
                            // Baca file pdf

                            var pageId = ent.PerformancePlanType == "1" ? "jpt" : (ent.PerformancePlanType == "2" ? "njpt" : "pppk");

                            var filePath = (ent.IsUpload ?? false) ?
                            Path.Combine(AppSession.Parameter.PerformancePlanDocumentFolder, "PerformancePlanDocument_" + pageId, ent.PerformancePlanID.ToString(), ent.FileAttachName)
                            : ent.OriPath;

                            //Return sourceFileName
                            sourceFileName = Path.GetFileName(filePath);

                            byte[] array = File.ReadAllBytes(filePath);
                            return array;
                        }
                        break;
                    }
            }
            return null;
        }

        private static byte[] DownloadFileToArrayByte(string urlFile)
        {
            using (WebClient client = new WebClient())
            {
                using (Stream ms = new MemoryStream(client.DownloadData(urlFile)))
                {
                    MemoryStream mStream = new MemoryStream();
                    mStream.SetLength(ms.Length);
                    ms.Read(mStream.GetBuffer(), 0, (int)ms.Length);
                    return mStream.ToArray();
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}