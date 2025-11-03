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
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class EpisodeSoapImageDialog : BasePageDialogEntry
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
            var timeNow = (new DateTime()).NowAtSqlServer();
            txtDateSOAP.SelectedDate = timeNow;
            txtTimeSOAP.SelectedDate = timeNow; 
            txtSubjective.Text = string.Empty;
            txtObjective.Text = string.Empty;
            txtAssesment.Text = string.Empty;
            txtPlanning.Text = string.Empty;
            txtAttendingNotes.Text = string.Empty;
            chkIsInformConcern.Checked = false;
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
            switch (CpoeType)
            {
                case CpoeTypeEnum.InPatient:
                    ProgramID = AppConstant.Program.CpoeInPatient;
                    break;
                case CpoeTypeEnum.Emergency:
                    ProgramID = AppConstant.Program.CpoeEmergency;
                    break;
                case CpoeTypeEnum.Outpatient:
                    ProgramID = AppConstant.Program.EpisodeAndHistory;
                    break;
            }
            // Program Fiture
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
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

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
        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);
            if (eventArgument.Contains("saveSoap"))
            {
                var seqNo = eventArgument.Split(':')[1];
                var es = new EpisodeSOAPE();
                es.LoadByPrimaryKey(RegistrationNo, seqNo);
                es.ParamedicID = ParamedicID;
                es.SOAPEDate = txtDateSOAP.SelectedDate;
                es.SOAPETime = txtTimeSOAP.SelectedDate.Value.ToString("HH:mm");
                es.Subjective = txtSubjective.Text;
                es.Objective = txtObjective.Text;
                es.Assesment = txtAssesment.Text;
                es.Planning = txtPlanning.Text;
                es.Evaluation = string.Empty;
                es.AttendingNotes = txtAttendingNotes.Text;
                es.IsInformConcern = chkIsInformConcern.Checked;
                es.Save();
                Helper.RegisterStartupScript(this, "closeMe", "CloseAndApply();");
            }
        }

        protected void RadImgEdt_ImageLoading(object sender, Telerik.Web.UI.ImageEditorLoadingEventArgs args)
        {
            MemoryStream mstream = new MemoryStream();
            if (Session["Soap:BodyID"] != null && !string.IsNullOrEmpty(Session["Soap:BodyID"].ToString()))
            {
                // Load Image Template from database
                var ent = new BodyDiagram();
                ent.LoadByPrimaryKey(Session["Soap:BodyID"].ToString());
                mstream = new MemoryStream(ent.BodyImage);
            }
            else
            {
                if (Request.QueryString["md"] == "new")
                {
                    // Load blank Image Template
                    using (FileStream file = new FileStream(Server.MapPath("~/Images/BodyImageCanvas.png"), FileMode.Open, FileAccess.Read))
                    {
                        byte[] bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);
                        mstream.Write(bytes, 0, (int)file.Length);
                    }
                }
                else
                {
                    var regNo = Request.QueryString["regno"];
                    var seqNo = Request.QueryString["seqno"];

                    var es = new EpisodeSOAPE();
                    if (es.LoadByPrimaryKey(regNo, seqNo))
                    {
                        // Load Image from database
                        mstream = new MemoryStream(es.BodyImage ?? new Byte[0]);
                        PopulateSoap(regNo, seqNo);
                    }
                }
            }

            Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
            args.Image = img;
            args.Cancel = true;
        }

        protected void RadImgEdt_ImageSaving(object sender, Telerik.Web.UI.ImageEditorSavingEventArgs args)
        {
            Telerik.Web.UI.ImageEditor.EditableImage img = args.Image;
            var seqNoSaved = Save(ImageToByteArray(img.Image));
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

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public string Save(byte[] imgBytes)
        {
            var seqNo = Request.QueryString["seqno"];
            var es = new EpisodeSOAPE();
            if (Request.QueryString["md"] == "new" || !es.LoadByPrimaryKey(RegistrationNo, seqNo))
            {
                var soape = new EpisodeSOAPE();
                soape.Query.es.Top = 1;
                soape.Query.Where(
                    soape.Query.RegistrationNo == RegistrationNo,
                    soape.Query.ServiceUnitID == Request.QueryString["unit"]
                    );
                soape.Query.OrderBy(soape.Query.SequenceNo.Descending);

                es = new EpisodeSOAPE();

                es.RegistrationNo = RegistrationNo;
                es.SequenceNo = soape.Query.Load() ? Convert.ToString(Convert.ToInt32(soape.SequenceNo) + 1) : "1";
                es.AttendingNotes = string.Empty;
                es.IsSummary = false;
                es.IsVoid = false;
                es.ServiceUnitID = Request.QueryString["unit"];
            }

            es.BodyImage = imgBytes;
            es.Save();

            Session.Remove("Soap:BodyID");
            return es.SequenceNo;
        }


        protected void cboBodyImage_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["Soap:BodyID"] = e.Value;
            imeBodyImage.ResetChanges();
        }

        private void PopulateSoap(string regNo, string seqNo)
        {
            var entity = new EpisodeSOAPE();
            entity.Query.Where(
                entity.Query.RegistrationNo == regNo &&
                entity.Query.SequenceNo == seqNo
                );
            if (entity.Query.Load())
            {

                //TODO: Tambahkan kondisi IsVoid dan Paramedic sebelum load entry SOAP
                //if (entity.IsVoid ?? false) return;

                //var usr = new AppUser();
                //if (usr.LoadByPrimaryKey(AppSession.UserLogin.UserID))
                //{
                //    if (entity.ParamedicID != usr.ParamedicID)
                //        return;
                //}

                txtDateSOAP.SelectedDate = entity.SOAPEDate;
                txtTimeSOAP.SelectedDate = Convert.ToDateTime(entity.SOAPETime);
                txtSubjective.Text = entity.Subjective;
                txtObjective.Text = entity.Objective;
                txtAssesment.Text = entity.Assesment;
                txtPlanning.Text = entity.Planning;
                txtAttendingNotes.Text = entity.AttendingNotes;
                chkIsInformConcern.Checked = entity.IsInformConcern ?? false;
            }
        }

    }
}
