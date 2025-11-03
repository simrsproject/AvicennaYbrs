using System;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalDocumentDownload : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var pd = new PersonalDocument();
            if (pd.LoadByPrimaryKey(Request.Form["id"].ToInt()))
            {
                var filePath = (pd.IsUpload ?? false) ?
                System.IO.Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "PersonalDocument", pd.PersonID.ToString().Trim(), pd.FileAttachName)
                : pd.OriPath;
                Helper.DownloadFile(Response, filePath);
            }
        }
    }
}