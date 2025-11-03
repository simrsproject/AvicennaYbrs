using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalUploadFoto : BasePageDialog
    {
        private string PersonID
        {
            get
            {
                return Request.QueryString["pid"];
            }
        }
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
        public override bool OnButtonOkClicked()
        {
            return Upload();
        }

        #region FotoUpload
        private bool Upload()
        {
            if (ruFoto.UploadedFiles.Count > 0)
            {
                var f1 = ruFoto.UploadedFiles[0];
                string filename = Path.GetFileName(f1.FileName);
                string contentType = f1.ContentType;
                using (Stream fs = f1.InputStream)
                {
                    var personalImage = new PersonalImage();
                    if (personalImage.LoadByPrimaryKey(Convert.ToInt32(PersonID)))
                    {
                        personalImage.Photo = ResizeImg(fs);
                        personalImage.LastUpdateDateTime = DateTime.Now;
                        personalImage.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        personalImage.Save();
                    }
                    else
                    {
                        personalImage.AddNew();
                        personalImage.PersonID = Convert.ToInt32(PersonID);
                        personalImage.Photo = ResizeImg(fs);
                        personalImage.LastUpdateDateTime = DateTime.Now;
                        personalImage.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        personalImage.Save();
                    }

                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private static Byte[] ResizeImg(Stream img)
        {
            byte[] resizedImage;
            using (Image orginalImage = Image.FromStream(img))
            {
                ImageFormat orginalImageFormat = orginalImage.RawFormat;
                int orginalImageWidth = orginalImage.Width;
                int orginalImageHeight = orginalImage.Height;
                int resizedImageHeight = 120;
                int resizedImageWidth = Convert.ToInt32(resizedImageHeight * orginalImageWidth / orginalImageHeight);
                using (Bitmap bitmapResized = new Bitmap(orginalImage, resizedImageWidth, resizedImageHeight))
                {
                    using (MemoryStream streamResized = new MemoryStream())
                    {
                        bitmapResized.Save(streamResized, orginalImageFormat);
                        resizedImage = streamResized.ToArray();
                    }
                }
            }
            return resizedImage;
        }
        #endregion
    }
}