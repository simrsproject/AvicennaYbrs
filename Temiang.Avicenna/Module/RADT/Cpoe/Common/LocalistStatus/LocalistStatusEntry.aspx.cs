using System;
using System.Web;
using Temiang.Avicenna.Common;
using System.Data;
using System.IO;
using Temiang.Avicenna.BusinessObject;
using System.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class LocalistStatusEntry : BasePageDialog
    {
        private string BodyID
        {
            get
            {
                return Request.QueryString["bodyId"];
            }
        }
        private string RegistrationInfoMedicID
        {
            get
            {
                return Request.QueryString["rimid"];
            }
        }

        /// <summary>
        /// Custom Session Name BodyImages Datatable
        /// </summary>
        private string SessionNameDtb
        {
            get
            {
                return Request.QueryString["sndtb"];
            }
        }

        internal static DataTable BodyImages
        {
            get
            {
                return (DataTable)HttpContext.Current.Session["rimBodyImage"];
            }
            set { HttpContext.Current.Session["rimBodyImage"] = value; }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonOk.OnClientClick = "javascript:onSaveImage();return false;";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private MemoryStream _resetedImg = null;
        protected void RadImgEdt_ImageLoading(object sender, Telerik.Web.UI.ImageEditorLoadingEventArgs args)
        {
            if (_resetedImg != null)
            {
                Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(_resetedImg);
                args.Image = img;
                _resetedImg = null;
            }
            else
            {
                var mstream = InitializedBlankImage();

                DataTable dtbSession;
                if (string.IsNullOrEmpty(SessionNameDtb))
                    dtbSession = BodyImages;
                else
                    dtbSession = (DataTable)Session[SessionNameDtb];

                if (dtbSession != null)
                {
                    foreach (DataRow row in dtbSession.Rows)
                    {
                        if (BodyID.Equals(row["BodyID"]))
                        {
                            mstream = new MemoryStream((Byte[])row["BodyImage"]);
                            break;
                        }
                    }
                }

                Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                args.Image = img;
            }
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
            // Store to Session

            DataTable dtbSession;
            if (string.IsNullOrEmpty(SessionNameDtb))
                dtbSession = BodyImages;
            else
                dtbSession = (DataTable)Session[SessionNameDtb];

            foreach (DataRow row in dtbSession.Rows)
            {
                if (BodyID.Equals(row["BodyID"]))
                {
                    row["BodyImage"] = ImageToByteArray(img.Image);
                    row["IsModified"] = true;
                    break;
                }
            }

            args.Cancel = true;

        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            switch (eventArgument)
            {
                case "ResetToLastSaved":
                    if (!string.IsNullOrWhiteSpace(BodyID))
                    {
                        var bd = new RegistrationInfoMedicBodyDiagram();
                        if (bd.LoadByPrimaryKey(RegistrationInfoMedicID, BodyID))
                        {
                            _resetedImg = new MemoryStream(bd.BodyImage);
                            imeBodyImage.ResetChanges();
                        }
                    }
                    break;
                case "ResetToTemplate":
                    if (!string.IsNullOrWhiteSpace(BodyID))
                    {
                        var bd = new BodyDiagram();
                        if (bd.LoadByPrimaryKey(BodyID))
                        {
                            _resetedImg = new MemoryStream(bd.BodyImage);
                            imeBodyImage.ResetChanges();
                        }
                    }
                    break;
                default:
                    break;
            }

        }


        private MemoryStream InitializedBlankImage()
        {
            MemoryStream mstream = new MemoryStream();
            // Load blank Image Template
            using (FileStream file = new FileStream(Server.MapPath("~/Images/BodyImageCanvas.png"), FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                mstream.Write(bytes, 0, (int)file.Length);
            }
            return mstream;
        }
        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

    }
}
