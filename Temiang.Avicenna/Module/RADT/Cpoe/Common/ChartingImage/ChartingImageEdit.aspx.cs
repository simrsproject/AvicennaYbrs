using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using DevExpress.XtraBars;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    /// <summary>
    /// Form ini hanya untuk edit PatientDocumentImage
    /// </summary>
    public partial class ChartingImageEdit : BasePageDialogEntry
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
                switch(this.Request.QueryString["rt"])
                {
                    case "IPR":
                        return CpoeTypeEnum.InPatient;
                    //case "EMR":
                    //    return CpoeTypeEnum.Emergency;
                    default:
                        return CpoeTypeEnum.Outpatient;
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            switch(CpoeType)
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
                Session.Remove("ImgTemplateID");

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(Request.QueryString["medno"]))
                {
                    this.Title = pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }


            // Load matrix Image Template
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            var qr = new ImageTemplateQuery("a");
            var qrMatrix = new ServiceUnitImageTemplateQuery("m");
            qr.InnerJoin(qrMatrix).On(qr.ImageTemplateID == qrMatrix.ImageTemplateID);
            qr.Where(qrMatrix.ServiceUnitID == reg.ServiceUnitID);

            qr.Select(qr.ImageTemplateID, qr.ImageTemplateName, qr.Image);

            var dtb = qr.LoadDataTable();
            cboImageTemplateID.DataSource = dtb;
            cboImageTemplateID.DataBind();


        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RadImgEdt_ImageLoading(object sender, Telerik.Web.UI.ImageEditorLoadingEventArgs args)
        {
            MemoryStream mstream = new MemoryStream();
            if (Session["ImgTemplateID"] != null && !string.IsNullOrEmpty(Session["ImgTemplateID"].ToString()))
            {
                // Load Image Template from database
                var ent = new ImageTemplate();
                ent.LoadByPrimaryKey(Session["ImgTemplateID"].ToString());
                mstream = new MemoryStream(ent.Image);
            }
            else
            {
                var es = new PatientDocumentImage();
                if (es.LoadByPrimaryKey(PatientID, SequenceNo))
                {
                    // Load Image from database
                    mstream = new MemoryStream(es.Image ?? new Byte[0]);

                    ComboBox.SelectedValue(cboImageTemplateID, es.ImageTemplateID);
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
        protected void RadImgEdt_ImageSaving(object sender, Telerik.Web.UI.ImageEditorSavingEventArgs args)
        {
            Telerik.Web.UI.ImageEditor.EditableImage img = args.Image;
            var seqNoSaved = Save((new ImageHelper()).ToByteArray(img.Image,ImageFormat.Png));
            if (!string.IsNullOrEmpty(seqNoSaved))
            {
                // Return sequenceNo to client Save event
                args.Argument = seqNoSaved;
            }
            else
            {
                var hdfMessage = (HiddenField)Helper.FindControlRecursive(Master, "hdfMessage");
                if (hdfMessage.Value != string.Empty)
                {
                    //Create Startup Javascript for message
                    Helper.ShowMessageAfterPostback(this,hdfMessage.Value);
                }
            }
            args.Cancel = true;

        }



        private string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        private int SequenceNo
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["seqno"]);
            }
        }
        public string Save(byte[] imgBytes)
        {
            var es = new PatientDocumentImage();
            if (es.LoadByPrimaryKey(PatientID, SequenceNo))
            {
                es.Image = imgBytes;
                es.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); 
                es.LastUpdateByUserID = AppSession.UserLogin.UserID;

                es.Save();
            }
            Session.Remove("ImgTemplateID");
            return string.Format("{0}_{1}",es.PatientID,es.SequenceNo);
        }

        protected void cboImageTemplateID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["ImgTemplateID"] = e.Value;
            imeImage.ResetChanges();
        }

    }
}
