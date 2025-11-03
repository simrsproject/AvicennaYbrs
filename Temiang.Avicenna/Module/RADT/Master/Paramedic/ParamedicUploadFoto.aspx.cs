using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicUploadFoto : BasePageDialog
    {
        private string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
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
                var par = new Paramedic();
                if (par.LoadByPrimaryKey(ParamedicID))
                {
                    var f1 = ruFoto.UploadedFiles[0];
                    string filename = Path.GetFileName(f1.FileName);
                    string contentType = f1.ContentType;
                    using (Stream fs = f1.InputStream)
                    {
                        if (Request.QueryString["type"] == "1")
                        {
                            par.Foto = ResizeImg(fs);
                            par.Save();
                        }
                        else
                        {
                            var au = new Temiang.Avicenna.BusinessObject.AppUser();
                            au.Query.Where(au.Query.ParamedicID == ParamedicID);
                            if (au.Query.Load())
                            {
                                au.SignatureImage = ResizeImg(fs);
                                au.Save();
                            }
                        }

                        //using (BinaryReader br = new BinaryReader(fs))
                        //{
                        //    byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        //    par.Foto = bytes;
                        //    par.Save();
                        //}
                        return true;
                    }
                }
                else
                {
                    return false;
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
