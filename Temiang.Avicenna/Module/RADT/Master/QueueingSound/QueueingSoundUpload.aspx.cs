using System;
using System.IO;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master

{
    public partial class QueueingSoundUpload : BasePageDialog
    {
        
        private string _fileUploadName = string.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueueingSound;
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oWnd.argument='{0}'", _fileUploadName.Replace("\\","\\\\")); //buat nampilin slash di File Path  
        }
        public override bool OnButtonOkClicked()
        {
            if (uplFilePath.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile validFile in uplFilePath.UploadedFiles)
                {
                    //string targetFolder = Server.MapPath("~/App_Document/QueueingSound/");
                    //string path = Path.GetFullPath(targetFolder);
                    string targetFolder = Path.Combine(AppSession.Parameter.SoundFolder, "QueueingSound");

                    if (!System.IO.Directory.Exists(targetFolder)) System.IO.Directory.CreateDirectory(targetFolder);
                    validFile.SaveAs(Path.Combine(targetFolder, validFile.GetName()), true);

                    _fileUploadName = validFile.GetName();
                    break;
                    //Path.GetFullPath(path) + 
                }
            }
            return true;
        }
    }

}
