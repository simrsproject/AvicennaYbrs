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
using DevExpress.XtraBars;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class GenogramEntry : BasePageDialogEntry
    {
        protected string RegistrationInfoMedicID
        {
            get
            {
                return Request.QueryString["rimid"];
            }
        }

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
            //switch (CpoeType)
            //{
            //    case CpoeTypeEnum.InPatient:
            //        ProgramID = AppConstant.Program.CpoeInPatient;
            //        break;
            //    case CpoeTypeEnum.Emergency:
            //        ProgramID = AppConstant.Program.CpoeEmergency;
            //        break;
            //    case CpoeTypeEnum.Outpatient:
            //        ProgramID = AppConstant.Program.CpoeOutPatient;
            //        break;
            //}

            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            // -------------------

            ToolBarMenuSave.Text = "Ok";
            if (!IsPostBack)
            {
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RadImgEdt_ImageLoading(object sender, Telerik.Web.UI.ImageEditorLoadingEventArgs args)
        {
            if (Session["genogram"] == null)
            {
                var assess = new PatientAssessment();
                if (assess.LoadByPrimaryKey(RegistrationInfoMedicID) && assess.Genogram != null)
                {
                    Session["genogram"] = assess.Genogram;
                }
                else
                {
                    // ambil dari Genogram terakhir ada di PatientGenogram 
                    var pg = new PatientGenogram();
                    if (pg.LoadByPrimaryKey(PatientID) && pg.Genogram != null)
                    {
                        Session["genogram"] = assess.Genogram;
                    }
                }
            }
            if (Session["genogram"] == null)
                Session["genogram"] = InitializedBlankImage();
            var mstream = new MemoryStream((Byte[])Session["genogram"]);

            Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
            args.Image = img;
            args.Cancel = true;
        }


        protected void RadImgEdt_ImageSaving(object sender, Telerik.Web.UI.ImageEditorSavingEventArgs args)
        {
            Telerik.Web.UI.ImageEditor.EditableImage img = args.Image;
            // Store to Session
            Session["genogram"] = ImageToByteArray(img.Image);
            args.Cancel = true;

        }
        private byte[] InitializedBlankImage()
        {
            var x = 1200;
            var y = 600;
            Bitmap bmp = new Bitmap(x, y);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, x, y);
                graph.FillRectangle(Brushes.White, ImageSize);
            }
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(bmp, typeof(byte[]));



            //MemoryStream mstream = new MemoryStream();
            //// Load blank Image Template
            //using (FileStream file = new FileStream(Server.MapPath("~/Images/BodyImageCanvas.png"), FileMode.Open, FileAccess.Read))
            //{
            //    byte[] bytes = new byte[file.Length];
            //    file.Read(bytes, 0, (int)file.Length);
            //    mstream.Write(bytes, 0, (int)file.Length);
            //}
            //return mstream;
        }
        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }


    }
}
