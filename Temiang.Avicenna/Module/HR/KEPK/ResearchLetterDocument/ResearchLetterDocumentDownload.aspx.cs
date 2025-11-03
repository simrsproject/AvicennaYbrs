using System;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.KEPK
{
    public partial class ResearchLetterDocumentDownload : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var pd = new ResearchLetterDocument();
            if (pd.LoadByPrimaryKey(Request.Form["id"].ToInt()))
            {
                var filePath = (pd.IsUpload ?? false) ?
                System.IO.Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "ResearchLetterDocument", pd.LetterID.ToString().Trim(), pd.FileAttachName)
                : pd.OriPath;
                Helper.DownloadFile(Response, filePath);
            }
        }
    }
}