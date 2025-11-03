using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Budgeting
{
    public partial class ImportFromExcel : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'uploaded'";
        }

        public override bool OnButtonOkClicked()
        {
            return Upload();
        }


        #region ImportExcel
        private bool Upload()
        {
            if (fUpload.HasFile)
            {
                if (ConfigurationManager.AppSettings["DocumentFolder"] == null) return false;
                string targetFolder = ConfigurationManager.AppSettings["DocumentFolder"];
                if (!System.IO.Directory.Exists(targetFolder))
                    System.IO.Directory.CreateDirectory(targetFolder);

                string path = string.Format("{0}{1}", ConfigurationManager.AppSettings["DocumentFolder"], fUpload.PostedFile.FileName);

                fUpload.SaveAs(path);


                Session["budgetImport"] = path;
                return true;
            }
            
            return false;
        }
        #endregion

    }
}
