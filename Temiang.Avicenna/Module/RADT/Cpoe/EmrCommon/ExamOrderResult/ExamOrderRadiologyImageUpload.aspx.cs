using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ExamOrderRadiologyImageUpload : BasePageDialog
    {
        protected string TransactionNo
        {
            get
            {
                return Request.QueryString["trno"];
            }
        }
        protected string SequenceNo
        {
            get
            {
                return Request.QueryString["seqno"];
            }
        }
        protected string ImageNo
        {
            get
            {
                return Request.QueryString["imgno"];
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(AjaxManager, uplDocumentImage);
            ajax.AddAjaxSetting(AjaxManager, imgDocumentImage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(ImageNo))
                    PopulateImageNo(NewImageNo());
                else
                    PopulateImage(ImageNo.ToInt());
            }
        }
        private void PopulateImage(int imageNo)
        {
            var ent = new TransChargesItemImage();
            if (ent.LoadByPrimaryKey(TransactionNo, SequenceNo, imageNo))
            {
                PopulateImageNo(imageNo);
                txtDocumentName.Text = ent.DocumentName;

                imgDocumentImage.DataValue = ent.DocumentImage;
                imgDocumentImage.Width = 500;
                imgDocumentImage.Height = 500;
                Context.Cache.Remove(Session.SessionID + "_DocumentImage");
                if (ent.DocumentImage != null)
                    Context.Cache.Insert(Session.SessionID + "_DocumentImage", ent.DocumentImage, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
                else
                    imgDocumentImage.ImageUrl = ""; // Untuk menghilangkan image
            }
            else
            {
                PopulateImageNo(NewImageNo());
            }
        }
        #region Entry

        private int NewImageNo()
        {
            var qr = new TransChargesItemImageQuery("a");
            var fb = new TransChargesItemImage();
            qr.es.Top = 1;
            qr.Where(qr.TransactionNo == TransactionNo, qr.SequenceNo == SequenceNo);
            qr.OrderBy(qr.ImageNo.Descending);

            if (fb.Load(qr))
            {
                return fb.ImageNo.ToInt() + 1;
            }
            return 1;
        }

        protected void uplDocumentImage_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            using (Stream stream = e.File.InputStream)
            {
                byte[] imgData = new byte[stream.Length];
                stream.Read(imgData, 0, imgData.Length);

                Context.Cache.Remove(Session.SessionID + "_DocumentImage");
                Context.Cache.Insert(Session.SessionID + "_DocumentImage", imgData, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);

                imgDocumentImage.DataValue = imgData;
            }

        }

        private void PopulateImageNo(int no)
        {
            txtImageNo.Text = string.Format("{0:00000}", no);
        }
        #endregion

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            if (Context.Cache[Session.SessionID + "_DocumentImage"] != null)
            {
                var ent = new TransChargesItemImage();
                if (!ent.LoadByPrimaryKey(TransactionNo, SequenceNo, txtImageNo.Text.ToInt()))
                {
                    ent.TransactionNo = TransactionNo;
                    ent.SequenceNo = SequenceNo;
                    ent.ImageNo = NewImageNo();
                }
                ent.DocumentName = txtDocumentName.Text;
                ent.DocumentImage = imgDocumentImage.DataValue;
                ent.DocumentImage = (byte[])Context.Cache[Session.SessionID + "_DocumentImage"];

                ent.Save();
            }
            else
            {
                args.IsCancel = true;
                args.MessageText = "Image can't empty, please upload image first";
            }
        }
    }
}