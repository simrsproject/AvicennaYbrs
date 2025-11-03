using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using System.Web;

namespace Temiang.Avicenna.Module.HR.Credential.Document
{
    public partial class DocumentImageZoomView2 : BasePageDialog
    {
        public int DocumentId
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
                this.Title = "Image File";
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";
            }
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            // Default CurrentItemIndex
            if (!IsPostBack)
            {

            }
        }

        protected void RadImageEditor1_ImageLoading(object sender, Telerik.Web.UI.ImageEditorLoadingEventArgs args)
        {
            MemoryStream mstream;
            var imgHelper = new ImageHelper();

            byte[] img = new byte[1];
            var ent = new CredentialProcessDocumentUpload();
            if (ent.LoadByPrimaryKey(DocumentId))
            {
                var filePath = (ent.IsUpload ?? false) ?
                System.IO.Path.Combine(AppSession.Parameter.EmployeeDocumentFolder, "CredentialingDocument", ent.TransactionNo.ToString(), ent.FileAttachName)
                : ent.OriPath;
                img = System.IO.File.ReadAllBytes(filePath);
            }
            else
            {
                // Blank Imgae
                var bitmap = new Bitmap(1, 1);
                img = (new ImageHelper()).ToByteArray(bitmap, ImageFormat.Png);
            }

            try
            {
                mstream = new MemoryStream(img);
            }
            catch (Exception e)
            {
                mstream = imgHelper.LoadImageToMemoryStream(Server.MapPath("~/Images/BodyImageCanvas.png"));
            }

            Telerik.Web.UI.ImageEditor.EditableImage eImg = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
            args.Image = eImg;
            args.Cancel = true;
        }
    }
}