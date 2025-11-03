using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using System.IO;
using DevExpress.XtraBars;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class EpisodeBodyDiagramEntry : BasePageDialogEntry
    {
        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
        }

        protected void OnPopulateEntryControl(esEntity entity)
        {
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
        }
        protected override void OnMenuNewClick()
        {

        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }

        public override string OnGetScriptToolBarSaveClicking()
        {
            return "onSaveImage();args.set_cancel(true);";
        }

        #endregion

        private CpoeTypeEnum CpoeType
        {
            get
            {
                switch (this.Request.QueryString["rt"])
                {
                    //case "EMR":
                    //    return CpoeTypeEnum.Emergency;
                    case "IPR":
                        return CpoeTypeEnum.InPatient;
                    default:
                        return CpoeTypeEnum.Outpatient;
                }

            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            switch (CpoeType)
            {
                case CpoeTypeEnum.InPatient:
                    ProgramID = AppConstant.Program.CpoeInPatient;
                    break;
                case CpoeTypeEnum.Emergency:
                    ProgramID = AppConstant.Program.CpoeEmergency;
                    break;
                case CpoeTypeEnum.Outpatient:
                    ProgramID = AppConstant.Program.CpoeOutPatient;
                    break;
            }

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                Session.Remove("Soap:BodyID");

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(Request.QueryString["medno"]))
                {
                    this.Title = pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }

            // Load matrix body Image Diagram
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            var qr = new BodyDiagramQuery("a");
            var qrMatrix = new BodyDiagramServiceUnitQuery("m");
            qr.InnerJoin(qrMatrix).On(qr.BodyID == qrMatrix.BodyID);
            qr.Where(qrMatrix.ServiceUnitID == reg.ServiceUnitID);

            qr.Select(qr.BodyID, qr.BodyName, qr.BodyImage);

            var dtb = qr.LoadDataTable();
            cboBodyImage.DataSource = dtb;
            cboBodyImage.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RadImgEdt_ImageLoading(object sender, Telerik.Web.UI.ImageEditorLoadingEventArgs args)
        {
            MemoryStream mstream = new MemoryStream();
            if (Session["Soap:BodyID"] != null && !string.IsNullOrEmpty(Session["Soap:BodyID"].ToString()))
            {
                // Load Image Template from database
                mstream = LoadBodyDiagramTemplate(Session["Soap:BodyID"].ToString());
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["bodyId"]) && Request.QueryString["bodyId"]!="undefined")
            {
                // Load Image Template from database
                mstream = LoadBodyDiagramTemplate(Request.QueryString["bodyId"]);
            }
            else
            {
                var es = new EpisodeBodyDiagram();
                if (es.LoadByPrimaryKey(RegistrationNo, SequenceNo))
                {
                    // Load Image from database
                    mstream = new MemoryStream(es.BodyImage ?? new Byte[0]);
                }
                else
                {
                    mstream = InitializedBlankImage();
                }
            }

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
            var seqNoSaved = Save(ImageToByteArray(img.Image));
            if (seqNoSaved > 0)
            {
                // Return sequenceNo to client Save event
                args.Argument = seqNoSaved.ToString();
            }
            else
            {
                var hdfMessage = (HiddenField)Helper.FindControlRecursive(Master, "hdfMessage");
                if (hdfMessage.Value != string.Empty)
                {
                    //Create Startup Javascript for message
                    Helper.ShowMessageAfterPostback(this, hdfMessage.Value);
                }
            }
            args.Cancel = true;

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

        private int SequenceNo
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["seqno"]);
            }
        }
        private int GetNewSequenceNo()
        {
            var qr = new EpisodeBodyDiagramQuery();
            qr.es.Top = 1;
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.Select(qr.SequenceNo);
            qr.OrderBy(qr.SequenceNo.Descending);
            var dtb = qr.LoadDataTable();

            int newSqNo = 1;
            if (dtb.Rows.Count > 0)
                newSqNo = Convert.ToInt32(dtb.Rows[0][0]) + 1;

            return newSqNo;
        }
        public int Save(byte[] imgBytes)
        {
            var es = new EpisodeBodyDiagram();
            if (Request.QueryString["mod"] == "new" || Request.QueryString["md"] == "new" || !es.LoadByPrimaryKey(RegistrationNo, SequenceNo))
            {
                es = new EpisodeBodyDiagram();

                es.RegistrationNo = RegistrationNo;
                es.SequenceNo = GetNewSequenceNo();
                es.IsDeleted = false;
                es.ServiceUnitID = Request.QueryString["unit"];
                es.ParamedicID = ParamedicID;

            }

            es.BodyImage = imgBytes;
            es.Save();

            Session.Remove("Soap:BodyID");
            return es.SequenceNo ?? 0;
        }


        protected void cboBodyImage_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["Soap:BodyID"] = e.Value;
            imeBodyImage.ResetChanges();
        }

    }
}
