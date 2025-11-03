using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Services;
using DevExpress.XtraBars;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.CustomControl.Phr.InputControl
{
    /// <summary>
    /// ImageEdit
    /// </summary>
    /// <example>
    /// See on PhrCtl.ascx
    /// </example>
    /// ----------------------------------------
    /// Created By: Handono
    /// ----------------------------------------
    public partial class ImageEdit : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonOk.OnClientClick = "javascript:onSaveImage();return false;";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadImgEdt_ImageLoading(object sender, Telerik.Web.UI.ImageEditorLoadingEventArgs args)
        {
            MemoryStream mstream;
            var imgHelper = new ImageHelper();
            if (HttpContext.Current.Session["editedImage"] !=null && !string.IsNullOrEmpty(HttpContext.Current.Session["editedImage"].ToString()))
            {
                
                try
                {
                    var imgSrc = imgHelper.ToImage(HttpContext.Current.Session["editedImage"].ToString());
                    mstream = new MemoryStream(imgHelper.ToByteArray( imgSrc,ImageFormat.Png));
                }
                catch (Exception e)
                {
                    mstream = imgHelper.LoadImageToMemoryStream(Server.MapPath("~/Images/BodyImageCanvas.png"));
                }
            }
            else
                mstream = imgHelper.LoadImageToMemoryStream(Server.MapPath("~/Images/BodyImageCanvas.png"));


            Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
            args.Image = img;
            args.Cancel = true;
        }

        private MemoryStream LoadBodyDiagramTemplate(string bodyID)
        {
            var ent = new BodyDiagram();
            return ent.LoadByPrimaryKey(bodyID) ? new MemoryStream(ent.BodyImage) : new MemoryStream();
        }

        protected void RadImgEdt_ImageSaving(object sender, Telerik.Web.UI.ImageEditorSavingEventArgs args)
        {
            Telerik.Web.UI.ImageEditor.EditableImage img = args.Image;
            // Store to argument for retrieve in javascript onClientSaved
            args.Argument = (new ImageHelper()).ToBase64String(img.Image,ImageFormat.Png);
            args.Cancel = true;
        }


    }
}
