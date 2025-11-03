using System;
using System.Web.UI;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class CropImage : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["capturedImageFileIsCompress"] = true;

            // Ovveride button Ok ClientClick
            ButtonOk.OnClientClick = "javascript:CropAndUploadPic();return false;";
        }

        protected string CapturedImageFileUrl
        {
            get {
                var url = string.Empty;
                var capturedImageFile = Session["capturedImageFile"].ToString();
                if (!string.IsNullOrEmpty(capturedImageFile))
                {
                    return string.Format("{0}?rnd={1}", capturedImageFile.Split('|')[1], (new Random()).Next() );
                }
                return string.Empty;
            }
        }
    }
}