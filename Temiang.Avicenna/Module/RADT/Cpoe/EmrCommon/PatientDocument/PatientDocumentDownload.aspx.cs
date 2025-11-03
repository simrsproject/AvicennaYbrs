using System;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.Services;
using System.IO;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    [System.Web.Script.Services.ScriptService]

    public partial class PatientDocumentDownload : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ent = new PatientDocument();
            if (ent.LoadByPrimaryKey(Request.Form["id"].ToInt()))
            {
                //var filePath = (ent.IsUpload ?? false) ?
                //System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", ent.PatientID.Trim(), ent.FileAttachName)
                //: ent.OriPath;
                //Helper.DownloadFile(Response, filePath);

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
                Helper.DownloadFile(Response, filePath);
            }
        }

        [WebMethod()]
        public static string SaveToGuarantorDoc(string mode, string id)
        {
            try
            {
                var pd = new PatientDocument();
                if (!pd.LoadByPrimaryKey(id.ToInt()))
                    return "File attachment not found";

                var regNo = pd.RegistrationNo;

                string regType = null;
                string guarantorID = null;
                string programCategory = "005"; // Dokumen Penunjang
                var fInfo = new FileInfo(pd.FileAttachName);
                var destFilePath = Reports.ReportViewer.GuarantorDocumentFilePath(regNo, string.Empty, Path.GetFileNameWithoutExtension(fInfo.Name), fInfo.Extension.Substring(1), ref regType, ref guarantorID, string.Empty);
                var path = Path.GetDirectoryName(destFilePath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //var sourceFilePath = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", pd.PatientID.Trim(), pd.FileAttachName);
                var fileFolderOld = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", pd.PatientID.Trim());
                var fileFolderYearly = "";
                if (!string.IsNullOrEmpty(pd.DocumentFolderYearly))
                    fileFolderYearly = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocumentYearly", pd.DocumentFolderYearly, pd.PatientID.Trim());

                var fileFolder = fileFolderOld;
                if (!System.IO.Directory.Exists(fileFolder))
                {
                    // jika old blm ada brarti pakai yearly
                    fileFolder = string.IsNullOrEmpty(fileFolderYearly) ? fileFolderOld : fileFolderYearly;
                }

                var sourceFilePath = Path.Combine(fileFolder, pd.FileAttachName);
                File.Copy(sourceFilePath, destFilePath);

                // Save Checklist
                Reports.ReportViewer.SaveRegistrationDocumentCheckList(regNo, regType, guarantorID, programCategory);

                return string.Format("File has save to {0}", destFilePath);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Save file failed";
        }

    }
}