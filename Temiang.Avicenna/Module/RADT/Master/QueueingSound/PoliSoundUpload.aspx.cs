using System;
using System.IO;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master

{
    public partial class PoliSoundUpload : BasePageDialog
    {
        private string _fileUploadName = string.Empty;
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueueingSound;
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oWnd.argument='{0}'", _fileUploadName.Replace("\\","\\\\"));
        }
        private string ServiceUnitID
        {
            get
            {
                return Request.QueryString["suid"];
            }
        }
        public override bool OnButtonOkClicked()
        {
            if (uplSoundFilePath.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile validFile in uplSoundFilePath.UploadedFiles)
                {
                    //string targetFolder = Server.MapPath("~/App_Document/ServiceUnit/");
                    //string pat = Path.GetFullPath(targetFolder);
                    string targetFolder = Path.Combine(AppSession.Parameter.SoundFolder, "ServiceUnit");

                    if (!System.IO.Directory.Exists(targetFolder))
                        System.IO.Directory.CreateDirectory(targetFolder);
                    validFile.SaveAs(Path.Combine(targetFolder, validFile.GetName()), true);
                   
                        _fileUploadName = validFile.GetName();
                        
                    break;
                    // Path.GetFullPath(pat) +
                }
            }

            return true;
        }       
    }
}
