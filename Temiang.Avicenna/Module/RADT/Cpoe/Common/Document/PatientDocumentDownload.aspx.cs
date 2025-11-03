using System;
using System.Web;
using System.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class PatientDocumentDownload : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var filePath = HttpUtility.UrlDecode(Request.Form["filepath"]);
            Helper.DownloadFile(Response, filePath);
        }
    }
}