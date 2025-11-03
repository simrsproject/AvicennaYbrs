using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.Services;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class WebCam : BasePageDialog
    {
        public int PatientDocumentID
        {
            get
            {
                return Request.QueryString["id"].ToInt();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Footer.Visible = false;
            }
        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            //UploadFile();
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
                var compression = AppParameter.GetParameterValue(AppParameter.ParameterItem.PatientDocumentScanCompression).ToInt();
                var imgHelper = new ImageHelper();
                imgHelper.Compress(imgHelper.ConvertByteArrayToImage(data), filename, compression);
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


        //private string UploadFile()
        //{

        //            var entity = new PatientDocument();
        //            entity.PatientID = patient.PatientID;
        //            entity.RegistrationNo = lastRegNo;
        //            entity.DocumentName = fileNames.Length > 3 ? fileNames[3] : fileName;
        //            entity.DocumentDate = docDate;
        //            entity.Notes = string.Empty;
        //            entity.FileAttachName = string.Empty;
        //            entity.OriFileName = fileName;
        //            entity.IsUpload = true; // Belum bisa diterapkan jika tidak diupload krn belum dapat cara mendapatkan path file sumbernya
        //            // Save untuk mendapatkan identity (PatientDocumentID)
        //            entity.Save();

        //            // Create Thumbnail
        //            var oriFileName = entity.OriFileName.ToLower();

        //            if (oriFileName.Contains(".jpg") || oriFileName.Contains(".jpg") || oriFileName.Contains(".png"))
        //            {

        //                // when we cast the stream, we need to dispose in order to be able to manipulate the file
        //                // otherwise, "The file is being used from another process" error will appear
        //                using (var filestream = validFile.InputStream as System.IO.FileStream)
        //                {
        //                    var imgByteArr = new byte[filestream.Length];
        //                    //Read data from the file stream and put into the byte array
        //                    filestream.Read(imgByteArr, 0, Convert.ToInt32(filestream.Length));

        //                    var smallImg = ImageHelper.ResizeImage(imgByteArr, new Size(100, 100), true, InterpolationMode.Low);

        //                    if (entity.OriFileName.Contains(".jpg") || entity.OriFileName.Contains(".jpg"))
        //                        entity.SmallImage = ImageHelper.ConvertImageToByteArray(smallImg, ImageFormat.Jpeg);
        //                    else if (entity.OriFileName.Contains(".png"))
        //                        entity.SmallImage = ImageHelper.ConvertImageToByteArray(smallImg, ImageFormat.Png);

        //                    filestream.Close();
        //                }
        //            }

        //            // Save File Name
        //            fileName = string.Format("{0:000000000000000}_{1}", entity.PatientDocumentID,
        //                validFile.GetName());
        //            entity.FileAttachName = fileName;

        //            // Save File
        //            var targetFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", entity.PatientID.Trim());

        //            if (!System.IO.Directory.Exists(targetFolder))
        //                System.IO.Directory.CreateDirectory(targetFolder);

        //            var fullPathFileName = Path.Combine(targetFolder, fileName);
        //            validFile.SaveAs(fullPathFileName, true);

        //            entity.Save();

        //    return string.Empty;
        //}

    }
}