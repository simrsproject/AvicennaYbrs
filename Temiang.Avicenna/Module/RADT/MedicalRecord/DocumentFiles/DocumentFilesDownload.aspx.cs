using System;
using System.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class DocumentFilesDownload : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string filePath = string.Format("{0}\\{1}", Server.MapPath("~/App_Document/DocumentFiles"), Request.Form["fileName"]);
            var filePath =System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "DocumentFiles", Request.Form["fileName"]);
            Helper.DownloadFile(Response, filePath);
        }
    }
}