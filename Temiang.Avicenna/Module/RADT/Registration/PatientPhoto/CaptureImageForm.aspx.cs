using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Web.Services;
using System.IO;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class CaptureImageForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.InputStream.Length > 0) // Call from POST Method
                {
                    //called page form json for creating imgBase64 in image
                    var reader = new StreamReader(Request.InputStream);
                    var data = Server.UrlDecode(reader.ReadToEnd());
                    reader.Close();

                    // Save to File
                    SaveImageToFile(data.Replace("imgBase64=data:image/png;base64,", String.Empty));

                }
                else
                {
                    Session["capturedImageFileIsCompress"] = false;
                    Session["capturedImageFile"] = string.Empty;

                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static string GetCapturedImage()
        {
            return HttpContext.Current.Session["capturedImageFile"].ToString();
        }

        public void SaveImageToFile(string base64) // Drawing image from Base64 string.
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/temp")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/temp"));
            }
            if (!System.IO.Directory.Exists(Server.MapPath("~/temp/Captures")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/temp/Captures"));
            }

            var filename = Server.MapPath("~/temp/Captures/PatientPhoto_") + AppSession.UserLogin.UserID + ".jpeg";

            var data = Convert.FromBase64String(base64);
            var isCompress = false;
            if (Session["capturedImageFileIsCompress"] != null && true.Equals(Session["capturedImageFileIsCompress"]))
            {
                // Compress
                var imgHelper = new ImageHelper();
                imgHelper.Compress(imgHelper.ConvertByteArrayToImage(data), filename, 200);
                isCompress = true;
            }
            else
            {
                // Save as is
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (var bw = new BinaryWriter(fs))
                    {
                        bw.Write(data);
                        bw.Close();
                    }
                }
            }
            //Session dipakai pd event tombol OK CaptureImage
            Session["capturedImageFile"] = string.Format("{0}|{1}|{2}", filename, Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/temp/Captures/PatientPhoto_" + AppSession.UserLogin.UserID + ".jpeg"), isCompress);
        }

    }
}
