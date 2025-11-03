using System;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.KEHRS.Document
{
    public partial class DocumentDownload : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var pd = new EmployeeSafetyCultureIncidentReportsDocument();
            if (pd.LoadByPrimaryKey(Request.Form["id"].ToInt()))
            {
                var filePath = (pd.IsUpload ?? false) ?
                System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "KehrsReportsDocument", pd.TransactionNo.ToString().Trim(), pd.FileAttachName)
                : pd.OriPath;
                Helper.DownloadFile(Response, filePath);
            }
        }
    }
}