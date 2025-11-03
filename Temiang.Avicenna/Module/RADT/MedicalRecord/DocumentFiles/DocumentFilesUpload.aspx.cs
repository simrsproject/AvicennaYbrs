using System;
using System.IO;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord

{
    public partial class DocumentFilesUpload : BasePageDialog
    {
        private string _fileUploadName = string.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DocumentFiles;
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oWnd.argument='{0}'", _fileUploadName);
        }
        public override bool OnButtonOkClicked()
        {
            if (uplFileTemplate.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile validFile in uplFileTemplate.UploadedFiles)
                {
                    //string targetFolder = Server.MapPath("~/App_Document/DocumentFiles");
                    string targetFolder =Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "DocumentFiles");

                    if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);
                    validFile.SaveAs(Path.Combine(targetFolder, validFile.GetName()), true);

                    var entity = new DocumentFiles();
                    if (entity.LoadByPrimaryKey(Convert.ToInt32(Request.QueryString["dfid"])))
                    {
                        _fileUploadName = validFile.GetName();
                        entity.FileTemplateName = _fileUploadName;
                        entity.Save();
                    }

                    break;
                }
            }

            return true;
        }
    }
}
