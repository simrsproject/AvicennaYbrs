using DevExpress.XtraBars;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceRoomUploadFoto : BasePageDialog
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
        public override bool  OnButtonOkClicked()
        {
            return Upload();
        }


        #region FotoUpload
        private bool Upload()
        {
            if (ruFoto.UploadedFiles.Count > 0)
            {
                int LastSeq = 0;
                if (Photos.Count > 0)
                {
                    LastSeq = Photos.OrderByDescending(x => x.SeqNo).Select(x => x.SeqNo ?? 0).FirstOrDefault();
                }
                LastSeq++;

                foreach (Telerik.Web.UI.UploadedFile uf in ruFoto.UploadedFiles)
                {
                    var f1 = ruFoto.UploadedFiles[0];
                    string filename = Path.GetFileName(f1.FileName);
                    string contentType = f1.ContentType;
                    using (Stream fs = f1.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            //byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            var p = Photos.AddNew();
                            p.AddNew();
                            p.SeqNo = LastSeq;
                            p.IndexNo = p.SeqNo;
                            p.Photo = br.ReadBytes((Int32)fs.Length); ;
                        }
                    }
                    LastSeq++;
                }
                return true;
            }
            else {
                return false;
            }
        }

        private ServiceRoomImagesCollection Photos
        {
            get
            {
                return ((ServiceRoomImagesCollection)(Session["collPhotos"]));
            }
            set { Session["collPhotos"] = value; }
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
