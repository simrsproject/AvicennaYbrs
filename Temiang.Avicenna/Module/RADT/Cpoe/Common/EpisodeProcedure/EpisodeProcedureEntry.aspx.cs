using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class EpisodeProcedureEntry : BasePageDialogEntry
    {
        private string SeqNo
        {
            get
            {
                if (!IsPostBack)
                {
                    hdnSeqNo.Value = Request.QueryString["seqno"];
                }
                return hdnSeqNo.Value;
            }
            set
            {
                hdnSeqNo.Value = value;
            }
        }

        private string BookingNo
        {
            get
            {
                if (!IsPostBack)
                {
                    txtBookingNo.Text = Request.QueryString["bno"];
                }
                return txtBookingNo.Text;
            }
            set
            {
                txtBookingNo.Text = value;
            }
        }

        private bool IsAnesthetist
        {
            get
            {
                var bookingId = Request.QueryString["bno"];
                var parId = Request.QueryString["parid"];
                var sub = new ServiceUnitBooking();
                return sub.LoadByPrimaryKey(bookingId) && parId == sub.ParamedicIDAnestesi;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;
            ToolBar.SaveAndEditVisible = true;
            ToolBar.AutoSaveVisible = true;
            // -------------------

            if (!IsPostBack)
            {
                cboPhysicianID.Visible = false; //string.IsNullOrEmpty(BookingNo); //Direct Procedure
                txtParamedicID.Visible = false; // !string.IsNullOrEmpty(BookingNo);
                txtParamedicName.Visible = true; //!string.IsNullOrEmpty(BookingNo);

                if (IsAnesthetist)
                {
                    tabHeader.Tabs[1].Visible = false;
                    tabHeader.Tabs[3].Visible = false;

                    txtRegio.ReadOnly = true;
                }
                else
                    tabHeader.Tabs[2].Visible = false;

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Episode Procedure of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                BookingNo = Request.QueryString["bno"];
                SeqNo = Request.QueryString["seqno"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            PopulateEntryControl();
        }

        private void PopulateEntryControl()
        {
            PopulateWithServiceUnitBooking(BookingNo);
            PopulateWithServiceUnitBookingOperatingNotes(BookingNo, SeqNo);
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //SIGN
            var isVisible = newVal != AppEnum.DataMode.Read;
            btnSurgSign.Enabled = isVisible;
            btnAnestSign.Enabled = isVisible;
        }
        protected override void OnMenuNewClick()
        {
            EmptyingControl();
            txtBookingNo.ReadOnly = true;
            if (string.IsNullOrEmpty(BookingNo))
            {
                var timeNow = (new DateTime()).NowAtSqlServer();
                txtProcedureDate.SelectedDate = timeNow;
                txtProcedureTime.SelectedDate = timeNow;
                txtProcedureDate2.SelectedDate = timeNow;
                txtProcedureTime2.SelectedDate = timeNow.AddMinutes(30);
                txtIncisionDate.SelectedDate = timeNow;
                txtIncisionTime.SelectedDate = timeNow;
                txtParamedicID.Text = ParamedicID;
                var p = new Paramedic();
                if (p.LoadByPrimaryKey(ParamedicID))
                    txtParamedicName.Text = p.ParamedicName;

                SetEnabledControlServiceUnitBooking(true);
            }
            else
            {
                txtBookingNo.Text = BookingNo;
                SetEnabledControlServiceUnitBooking(false);
                PopulateWithServiceUnitBooking(BookingNo);
            }

            PopulateGridDetail();
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args);
        }
        private void Save(ValidateArgs args)
        {
            /// <summary>
            /// Sign mandatory
            /// </summary>
            /// Create By: Fajri
            /// Create Date: 2023 April 05
            /// Client Reg: RSYS
            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsSignMandatoryOnOperatingNotes).ToLower() == "yes")
            {
                if (!IsAnesthetist)
                {
                    if (string.IsNullOrEmpty(hdnImageSurgSign.Value))
                    {
                        args.MessageText = "Sign is required.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsMandatoryEpisodeProcedureOnOperatingNotes).ToLower() == "yes")
            {
                if (!IsAnesthetist)
                {
                    var eps = EpisodeProcedures.Where(b => b.BookingNo == BookingNo && b.OpNotesSeqNo == SeqNo && b.IsVoid == false);
                    if (eps.Count() == 0)
                    {
                        args.MessageText = "Procedures is required.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

                // Cek ServiceUnitBooking
            var sub = new ServiceUnitBooking();
            if (string.IsNullOrWhiteSpace(BookingNo)) // Direct procedure
            {
                // Create new ServiceUnitBooking
                sub = new ServiceUnitBooking();

                var autoNum = Helper.GetNewAutoNumber(txtProcedureDate.SelectedDate.Value.Date, AppEnum.AutoNumber.ServiceUnitBookingNo);

                txtBookingNo.Text = autoNum.LastCompleteNumber;
                BookingNo = txtBookingNo.Text;
                sub.BookingNo = txtBookingNo.Text;
                sub.BookingDateTimeFrom = DateTime.Parse(txtProcedureDate.SelectedDate.Value.ToShortDateString() + " " +
                    txtProcedureTime.SelectedDate.Value.ToShortTimeString());
                sub.BookingDateTimeTo = DateTime.Parse(txtProcedureDate2.SelectedDate.Value.ToShortDateString() + " " +
                    txtProcedureTime2.SelectedDate.Value.ToShortTimeString());

                sub.RealizationDateTimeFrom = DateTime.Parse(txtProcedureDate.SelectedDate.Value.ToShortDateString() + " " +
                     txtProcedureTime.SelectedDate.Value.ToShortTimeString());
                sub.RealizationDateTimeTo = DateTime.Parse(txtProcedureDate2.SelectedDate.Value.ToShortDateString() + " " +
                    txtProcedureTime2.SelectedDate.Value.ToShortTimeString());

                sub.RoomID = RegistrationCurrent.RoomID;
                sub.ServiceUnitID = RegistrationCurrent.ServiceUnitID;

                sub.RegistrationNo = RegistrationNo;
                sub.PatientID = RegistrationCurrent.PatientID;

                sub.ParamedicID = txtParamedicID.Text;//cboPhysicianID.SelectedValue;
                //sub.ParamedicID2 = cboPhysicianID2.SelectedValue;
                //sub.ParamedicID3 = cboPhysicianID3.SelectedValue;
                //sub.ParamedicID4 = cboPhysicianID4.SelectedValue;
                //sub.ParamedicIDAnestesi = cboPhysicianIDAnestesi.SelectedValue;
                sub.Diagnose = txtPreDiagnosis.Text;
                sub.PostDiagnosis = txtPostDiagnosis.Text;
                sub.AmountOfBleeding = Convert.ToDecimal(txtAmountOfBleeding.Value);
                sub.AmountOfTransfusions = Convert.ToDecimal(txtAmountOfTransfusions.Value);
                sub.IsApproved = true;
                sub.IsVoid = false;
                //sub.SRAnestesiPlan = cboSRAnestesi.SelectedValue;
                //sub.Diagnose = txtDiagnose.Text;
                //sub.Notes = txtNotes.Text;
                //sub.IsExtendedSurgery = chkIsExtendedSurgery.Checked;
                //sub.IsCito = rbtActionType.SelectedValue == "1";

                sub.FromServiceUnitID = RegistrationCurrent.ServiceUnitID;
                
                sub.Save();
                autoNum.Save();
            }
            else
            {
                if (!sub.LoadByPrimaryKey(txtBookingNo.Text))
                {
                    args.IsCancel = true;
                    args.MessageText = "Booking No not registered.";
                    return;
                }

                sub.Diagnose = txtPreDiagnosis.Text;
                sub.PostDiagnosis = txtPostDiagnosis.Text;
                sub.Save();
            }

            if (!IsAnesthetist)
            {
                var entity = new ServiceUnitBookingOperatingNotes();
                if (string.IsNullOrEmpty(SeqNo) || !entity.LoadByPrimaryKey(txtBookingNo.Text, SeqNo))
                {
                    // New record
                    var newSqNo = "001";
                    var query = new ServiceUnitBookingOperatingNotesQuery();
                    query.Where(query.BookingNo == txtBookingNo.Text);
                    query.es.Top = 1;
                    query.OrderBy(query.OpNotesSeqNo.Descending);

                    var entLastSeq = new ServiceUnitBookingOperatingNotes();
                    if (entLastSeq.Load(query))
                    {
                        var seqNo = int.Parse(entLastSeq.OpNotesSeqNo);
                        newSqNo = string.Format("{0:000}", seqNo + 1);
                    }

                    entity.BookingNo = txtBookingNo.Text;
                    entity.OpNotesSeqNo = newSqNo;
                    imageCtl.OpNotesSeqNo = newSqNo;

                    // Untuk status Save new / edit
                    SeqNo = entity.OpNotesSeqNo;
                }

                SetEntityServiceUnitBookingOperatingNotes(entity);
                entity.Save();

                // Booking
                sub.IncisionDateTime = DateTime.Parse(txtIncisionDate.SelectedDate.Value.ToShortDateString() + " " + txtIncisionTime.SelectedDate.Value.ToShortTimeString());
                sub.AmountOfBleeding = Convert.ToDecimal(txtAmountOfBleeding.Value);
                sub.AmountOfTransfusions = Convert.ToDecimal(txtAmountOfTransfusions.Value);
                sub.Save();

                foreach (var c in EpisodeProcedures)
                {
                    if (string.IsNullOrEmpty(c.BookingNo))
                        c.BookingNo = txtBookingNo.Text;
                    if (c.BookingNo == BookingNo && string.IsNullOrEmpty(c.OpNotesSeqNo) && c.CreateByUserID == AppSession.UserLogin.UserID)
                        c.OpNotesSeqNo = entity.OpNotesSeqNo;
                }
                EpisodeProcedures.Save();

                ImplantInstallations.Save();
                imageCtl.Save(args);
            }
            else
            {
                if (!string.IsNullOrEmpty(txtBookingNo.Text))
                {
                    // Booking
                    sub.AnestesyNotes = txtAnestesyNotes.Text;
                    sub.AnestPostSurgeryInstructions = txtAnestPostSurgeryInstructions.Text;
                    sub.IsInsertionImplant = ImplantInstallations.Count > 0;

                    // SIGN
                    var imgHelper = new ImageHelper();
                    if (!string.IsNullOrWhiteSpace(hdnImageAnestSign.Value))
                    {
                        var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnImageAnestSign.Value), new Size(332, 185));
                        sub.AnesthesiologistSign = imgHelper.ToByteArray(resized, ImageFormat.Png);
                    }
                    else
                        sub.AnesthesiologistSign = null;

                    sub.Save();

                    //foreach (var c in EpisodeProcedures)
                    //{
                    //    if (string.IsNullOrEmpty(c.OpNotesSeqNo) && c.CreateByUserID == AppSession.UserLogin.UserID)
                    //        c.OpNotesSeqNo = "000";
                    //}

                    EpisodeProcedures.Save();
                }
            }
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
            txtBookingNo.ReadOnly = true;
            SetEnabledControlServiceUnitBooking(string.IsNullOrEmpty(BookingNo));
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
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
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            // Untuk autosave dan SaveAndEdit update status
            var fw_btnAutoSave = (Button)Helper.FindControlRecursive(Master, "fw_btnAutoSave");
            ajax.AddAjaxSetting(fw_btnAutoSave, txtBookingNo);
            ajax.AddAjaxSetting(fw_btnAutoSave, hdnSeqNo);

        }
        #endregion

        #region Properties for return entry value, pakai properties krn codingnya hasil copy dari entrian detil digrid pada program yg ada

        private String ParamedicName
        {
            get { return txtParamedicName.Text; }
        }

        private String ParamedicID2a
        {
            get { return txtParamedicID2a.Text; }
        }

        private String ParamedicID3a
        {
            get { return txtParamedicID3a.Text; }
        }

        private String ParamedicID4a
        {
            get { return txtParamedicID4a.Text; }
        }

        private String ParamedicID2
        {
            get { return txtParamedicID2.Text; }
        }

        private String AssistantIDAnestesi
        {
            get { return txtAssistantIDAnestesi.Text; }
        }

        private String AssistantID1
        {
            get { return txtAssistantID1.Text; }
        }

        private String AssistantID2
        {
            get { return txtAssistantID2.Text; }
        }
        private String InstrumentatorID1
        {
            get { return txtInstrumentatorID1.Text; }
        }

        private String InstrumentatorID2
        {
            get { return txtInstrumentatorID2.Text; }
        }

        private String AssistantInstrumentatorId
        {
            get { return txtAssistantInstrumentatorId.Text; }
        }


        private String SRProcedureCategory
        {
            get { return txtSRProcedureCategory.Text; }
        }

        private String SRAnestesi
        {
            get { return txtSRAnestesi.Text; }
        }

        private String RoomID
        {
            get { return txtRoomID.Text; }
        }

        private Boolean IsCito
        {
            get { return chkIsCito.Checked; }
        }

        private Boolean IsVoid
        {
            get { return chkIsVoid.Checked; }
        }

        #endregion

        private void SetEntityServiceUnitBookingOperatingNotes(ServiceUnitBookingOperatingNotes entity)
        {
            entity.ParamedicID = ParamedicID;
            entity.Regio = !IsAnesthetist ? txtRegio.Text : string.Empty;
            entity.OperatingNotes = !IsAnesthetist ? txtOperatingNotes.Text : txtAnestesyNotes.Text;
            entity.PostSurgeryInstructions = !IsAnesthetist ? txtPostSurgeryInstructions.Text : txtAnestPostSurgeryInstructions.Text;
            entity.ComplicationsNotes = !IsAnesthetist ? txtComplicationsNotes.Text : string.Empty;
            entity.PreDiagnosis = !IsAnesthetist ? txtPreDiagnosisNotes.Text : string.Empty;
            entity.PostDiagnosis = !IsAnesthetist ? txtPostDiagnosisNotes.Text : string.Empty;
            entity.IsVoid = false;

            //SIGN
            var imgHelper = new ImageHelper();
            if (!string.IsNullOrWhiteSpace(hdnImageSurgSign.Value))
            {
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnImageSurgSign.Value), new Size(332, 185));
                entity.SurgeonSign = imgHelper.ToByteArray(resized, ImageFormat.Png);
            }
            else
                entity.SurgeonSign = null;
        }

        private void EmptyingControl()
        {
            txtParamedicID.Text = string.Empty;
            txtParamedicName.Text = string.Empty;
            txtParamedicID2a.Text = string.Empty;
            txtParamedicName2a.Text = string.Empty;
            txtParamedicID3a.Text = string.Empty;
            txtParamedicName3a.Text = string.Empty;
            txtParamedicID4a.Text = string.Empty;
            txtParamedicName4a.Text = string.Empty;
            txtParamedicID2.Text = string.Empty;
            txtParamedicName2.Text = string.Empty;
            txtAssistantIDAnestesi.Text = string.Empty;
            txtAssistantNameAnestesi.Text = string.Empty;
            txtAssistantIDAnestesi2.Text = string.Empty;
            txtAssistantNameAnestesi2.Text = string.Empty;
            txtAssistantID1.Text = string.Empty;
            txtAssistantName1.Text = string.Empty;
            txtAssistantID2.Text = string.Empty;
            txtAssistantName2.Text = string.Empty;
            txtInstrumentatorID1.Text = string.Empty;
            txtInstrumentatorName1.Text = string.Empty;
            txtInstrumentatorID2.Text = string.Empty;
            txtInstrumentatorName2.Text = string.Empty;
            txtAssistantInstrumentatorId.Text = string.Empty;
            txtAssistantInstrumentatorName.Text = string.Empty;

            txtSRAnestesi.Text = string.Empty;
            txtSRAnestesiName.Text = string.Empty;
            txtSRProcedureCategory.Text = string.Empty;
            txtSRProcedureCategoryName.Text = string.Empty;
            chkIsCito.Checked = false;
            txtRoomID.Text = string.Empty;
            txtRoomName.Text = string.Empty;
            txtPreDiagnosis.Text = string.Empty;
            txtPostDiagnosis.Text = string.Empty;
            txtAmountOfBleeding.Value = 0;
            txtAmountOfTransfusions.Value = 0;

            txtOperatingNotes.Text = string.Empty;
            txtAnestesyNotes.Text = string.Empty;
            txtPostSurgeryInstructions.Text = string.Empty;
            txtAnestPostSurgeryInstructions.Text = string.Empty;
            txtComplicationsNotes.Text = string.Empty;
            txtPreDiagnosisNotes.Text = txtPreDiagnosis.Text;
            txtPostDiagnosisNotes.Text = txtPostDiagnosis.Text;
        }

        private void SetEnabledControlServiceUnitBooking(bool b)
        {
            txtProcedureDate.Enabled = b;
            txtProcedureDate2.Enabled = b;
            txtProcedureTime.Enabled = b;
            txtProcedureTime2.Enabled = b;
            if (!IsAnesthetist)
            {
                txtIncisionDate.Enabled = true;
                txtIncisionTime.Enabled = true;
                txtAmountOfBleeding.ReadOnly = false;
                txtAmountOfTransfusions.ReadOnly = false;
            }
            else
            {
                txtIncisionDate.Enabled = b;
                txtIncisionTime.Enabled = b;
                txtAmountOfBleeding.ReadOnly = !b;
                txtAmountOfTransfusions.ReadOnly = !b;
            }

            txtParamedicID.Visible = false;
            txtParamedicName.ReadOnly = true;
            txtParamedicID2a.Visible = false;
            txtParamedicName2a.ReadOnly = true;
            txtParamedicID3a.Visible = false;
            txtParamedicName3a.ReadOnly = true;
            txtParamedicID4a.Visible = false;
            txtParamedicName4a.ReadOnly = true;
            txtParamedicID2.Visible = false;
            txtParamedicName2.ReadOnly = true;
            txtAssistantIDAnestesi.Visible = false;
            txtAssistantNameAnestesi.ReadOnly = true;
            txtAssistantIDAnestesi2.Visible = false;
            txtAssistantNameAnestesi2.ReadOnly = true;
            txtAssistantID1.Visible = false;
            txtAssistantName1.ReadOnly = true;
            txtAssistantID2.Visible = false;
            txtAssistantName2.ReadOnly = true;
            txtInstrumentatorID1.Visible = false;
            txtInstrumentatorName1.ReadOnly = true;
            txtInstrumentatorID2.Visible = false;
            txtInstrumentatorName2.ReadOnly = true;
            txtAssistantInstrumentatorId.Visible = false;
            txtAssistantInstrumentatorName.ReadOnly = true;
            txtSRAnestesi.Visible = false;
            txtSRAnestesiName.ReadOnly = true;
            txtSRProcedureCategory.Visible = false;
            txtSRProcedureCategoryName.ReadOnly = true;
            txtRoomID.Visible = false;
            txtRoomName.ReadOnly = true;
            //txtPreDiagnosis.ReadOnly = !b;
            //txtPostDiagnosis.ReadOnly = !b;
            txtPreDiagnosis.ReadOnly = !AppParameter.IsYes(AppParameter.ParameterItem.IsAllowEditDiagnosisOnEpisodeProcedureEMR) ? !b : b;
            txtPostDiagnosis.ReadOnly = !AppParameter.IsYes(AppParameter.ParameterItem.IsAllowEditDiagnosisOnEpisodeProcedureEMR) ? !b : b;

            chkIsCito.Enabled = b;
            chkIsVoid.Enabled = b;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(txtBookingNo.Text))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Booking no is requered";
            }
        }

        private void PopulateWithServiceUnitBookingOperatingNotes(string bookingNo, string seqNo)
        {
            var opNotes = new ServiceUnitBookingOperatingNotes();
            opNotes.LoadByPrimaryKey(bookingNo, seqNo);
            txtOperatingNotes.Text = opNotes.OperatingNotes;
            txtPostSurgeryInstructions.Text = opNotes.PostSurgeryInstructions;
            txtComplicationsNotes.Text = opNotes.ComplicationsNotes;
            txtPreDiagnosisNotes.Text = opNotes.PreDiagnosis;
            txtPostDiagnosisNotes.Text = opNotes.PostDiagnosis;
            txtRegio.Text = opNotes.Regio;

            //SIGN
            var imgHelper = new ImageHelper();
            if (opNotes.SurgeonSign != null)
            {
                var val = (byte[])opNotes.SurgeonSign;
                surgImage.DataValue = val;
                var mstream = new MemoryStream(val);
                Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                hdnImageSurgSign.Value = imgHelper.ToBase64String(img.Image, ImageFormat.Png);
            }
            else
            {
                surgImage.DataValue = null;
                hdnImageSurgSign.Value = String.Empty;
            }

            if (IsAnesthetist)
            {
                var coll = new ServiceUnitBookingOperatingNotesCollection();
                coll.Query.Where(coll.Query.BookingNo == bookingNo, coll.Query.IsVoid == false,
                    coll.Query.Regio != string.Empty);
                coll.LoadAll();
                var regio = string.Empty;
                foreach (var c in coll)
                {
                    if (regio == string.Empty)
                        regio = c.Regio;
                    else regio = regio + "; " + c.Regio;
                }
                txtRegio.Text = regio;
            }
        }

        private void PopulateWithServiceUnitBooking(string bookingNo)
        {
            var b = new ServiceUnitBooking();
            if (b.LoadByPrimaryKey(bookingNo))
            {
                imageCtl.BookingNo = BookingNo;
                imageCtl.OpNotesSeqNo = SeqNo;
                imageCtl.ServiceUnitID = b.ServiceUnitID;

                txtBookingNo.Text = b.BookingNo;

                var par = new Paramedic();
                txtParamedicID.Text = b.ParamedicID;
                if (par.LoadByPrimaryKey(b.ParamedicID))
                    txtParamedicName.Text = par.ParamedicName;

                if (!string.IsNullOrEmpty(b.ParamedicID2))
                {
                    par = new Paramedic();
                    txtParamedicID2a.Text = b.ParamedicID2;
                    if (par.LoadByPrimaryKey(b.ParamedicID2))
                        txtParamedicName2a.Text = par.ParamedicName;
                }

                if (!string.IsNullOrEmpty(b.ParamedicID3))
                {
                    par = new Paramedic();
                    txtParamedicID3a.Text = b.ParamedicID3;
                    if (par.LoadByPrimaryKey(b.ParamedicID3))
                        txtParamedicName3a.Text = par.ParamedicName;
                }

                if (!string.IsNullOrEmpty(b.ParamedicID4))
                {
                    par = new Paramedic();
                    txtParamedicID4a.Text = b.ParamedicID4;
                    if (par.LoadByPrimaryKey(b.ParamedicID4))
                        txtParamedicName4a.Text = par.ParamedicName;
                }

                if (!string.IsNullOrEmpty(b.ParamedicIDAnestesi))
                {
                    par = new Paramedic();
                    txtParamedicID2.Text = b.ParamedicIDAnestesi;
                    if (par.LoadByPrimaryKey(b.ParamedicIDAnestesi))
                        txtParamedicName2.Text = par.ParamedicName;
                }

                if (!string.IsNullOrEmpty(b.AssistantIDAnestesi))
                {
                    par = new Paramedic();
                    txtAssistantIDAnestesi.Text = b.AssistantIDAnestesi;
                    if (par.LoadByPrimaryKey(b.AssistantIDAnestesi))
                        txtAssistantNameAnestesi.Text = par.ParamedicName;
                }

                if (!string.IsNullOrEmpty(b.AssistantIDAnestesi2))
                {
                    par = new Paramedic();
                    txtAssistantIDAnestesi2.Text = b.AssistantIDAnestesi2;
                    if (par.LoadByPrimaryKey(b.AssistantIDAnestesi2))
                        txtAssistantNameAnestesi2.Text = par.ParamedicName;
                }

                if (!string.IsNullOrEmpty(b.AssistantID1))
                {
                    par = new Paramedic();
                    txtAssistantID1.Text = b.AssistantID1;
                    if (par.LoadByPrimaryKey(b.AssistantID1))
                        txtAssistantName1.Text = par.ParamedicName;
                }

                if (!string.IsNullOrEmpty(b.AssistantID2))
                {
                    par = new Paramedic();
                    txtAssistantID2.Text = b.AssistantID2;
                    if (par.LoadByPrimaryKey(b.AssistantID2))
                        txtAssistantName2.Text = par.ParamedicName;
                }

                if (!string.IsNullOrEmpty(b.Instrumentator1))
                {
                    par = new Paramedic();
                    txtInstrumentatorID1.Text = b.Instrumentator1;
                    if (par.LoadByPrimaryKey(b.Instrumentator1))
                        txtInstrumentatorName1.Text = par.ParamedicName;
                }

                if (!string.IsNullOrEmpty(b.Instrumentator2))
                {
                    par = new Paramedic();
                    txtInstrumentatorID2.Text = b.Instrumentator2;
                    if (par.LoadByPrimaryKey(b.Instrumentator2))
                        txtInstrumentatorName2.Text = par.ParamedicName;
                }

                if (!string.IsNullOrEmpty(b.AssistantIDInstrumentator))
                {
                    par = new Paramedic();
                    txtAssistantInstrumentatorId.Text = b.AssistantIDInstrumentator;
                    if (par.LoadByPrimaryKey(b.AssistantIDInstrumentator))
                        txtAssistantInstrumentatorName.Text = par.ParamedicName;
                }

                if (b.RealizationDateTimeFrom.HasValue)
                {
                    txtProcedureDate.SelectedDate = b.RealizationDateTimeFrom.Value;
                    txtProcedureTime.SelectedDate = b.RealizationDateTimeFrom.Value;
                }
                else
                {
                    txtProcedureDate.SelectedDate = null;
                    txtProcedureTime.SelectedDate = null;
                }
                if (b.RealizationDateTimeTo.HasValue)
                {
                    txtProcedureDate2.SelectedDate = b.RealizationDateTimeTo.Value;
                    txtProcedureTime2.SelectedDate = b.RealizationDateTimeTo.Value;
                }
                else
                {
                    txtProcedureDate2.SelectedDate = null;
                    txtProcedureTime2.SelectedDate = null;
                }
                if (b.IncisionDateTime.HasValue)
                {
                    txtIncisionDate.SelectedDate = b.IncisionDateTime.Value;
                    txtIncisionTime.SelectedDate = b.IncisionDateTime.Value;
                }
                else
                {
                    txtIncisionDate.SelectedDate = null;
                    txtIncisionTime.SelectedDate = null;
                }

                var std = new AppStandardReferenceItem();
                txtSRAnestesi.Text = b.SRAnestesiPlan;
                if (!string.IsNullOrEmpty(b.SRAnestesiPlan))
                {
                    if (std.LoadByPrimaryKey(AppEnum.StandardReference.Anestesi.ToString(), b.SRAnestesiPlan))
                        txtSRAnestesiName.Text = std.ItemName;
                }

                std = new AppStandardReferenceItem();
                txtSRProcedureCategory.Text = b.SRProcedureCategory;
                if (!string.IsNullOrEmpty(b.SRProcedureCategory))
                {
                    if (std.LoadByPrimaryKey(AppEnum.StandardReference.ProcedureCategory.ToString(), b.SRProcedureCategory))
                        txtSRProcedureCategoryName.Text = std.ItemName;
                }

                var room = new ServiceRoom();
                txtRoomID.Text = b.RoomID;
                if (!string.IsNullOrEmpty(b.RoomID))
                {
                    if (room.LoadByPrimaryKey(b.RoomID))
                        txtRoomName.Text = room.RoomName;
                }
                txtPreDiagnosis.Text = b.Diagnose;
                txtPostDiagnosis.Text = b.PostDiagnosis;
                txtAmountOfBleeding.Value = Convert.ToDouble(b.AmountOfBleeding);
                txtAmountOfTransfusions.Value = Convert.ToDouble(b.AmountOfTransfusions);

                chkIsCito.Checked = b.IsCito.HasValue ? b.IsCito.Value : false;
                txtAnestesyNotes.Text = b.AnestesyNotes;
                txtAnestPostSurgeryInstructions.Text = b.AnestPostSurgeryInstructions;

                //SIGN
                var imgHelper = new ImageHelper();
                if (b.AnesthesiologistSign != null)
                {
                    var val = (byte[])b.AnesthesiologistSign;
                    anestImage.DataValue = val;
                    var mstream = new MemoryStream(val);
                    Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                    hdnImageAnestSign.Value = imgHelper.ToBase64String(img.Image, ImageFormat.Png);
                }
                else
                {
                    anestImage.DataValue = null;
                    hdnImageAnestSign.Value = String.Empty;
                }
            }
        }

        protected object ScriptCopyTemplate(string noteType)
        {
            var retval = string.Empty;
            if (noteType == "OPR")
                retval =
                    "<a href='#' onclick=\"javascript:openOperatingNotesTemplate(); return false;\"><img src=\"../../../../../Images/Toolbar/ordering16.png\" alt=\"a\" />&nbsp; Copy from template</a>";
            else if (noteType == "PSI")
                retval =
                    "<a href='#' onclick=\"javascript:openPostOpNotesTemplate(); return false;\"><img src=\"../../../../../Images/Toolbar/ordering16.png\" alt=\"a\" />&nbsp; Copy from template</a>";
            else if (noteType == "ANS")
                retval =
                    "<a href='#' onclick=\"javascript:openAnestesyNotesTemplate(); return false;\"><img src=\"../../../../../Images/Toolbar/ordering16.png\" alt=\"a\" />&nbsp; Copy from template</a>";
            else 
                retval =
                    "<a href='#' onclick=\"javascript:openAnestPostOpNotesTemplate(); return false;\"><img src=\"../../../../../Images/Toolbar/ordering16.png\" alt=\"a\" />&nbsp; Copy from template</a>";

            return retval;
        }
        protected object ScriptNewTemplate(string noteType)
        {
            var retval = string.Empty;
            if (noteType == "OPR")
                retval =
                    "<a href='#' onclick=\"javascript:openOperatingNotesTemplateNew(); return false;\"><img src=\"../../../../../Images/Toolbar/copy16.png\" alt=\"b\" />&nbsp;Save to Template</a>";
            else if (noteType == "PSI")
                retval =
                    "<a href='#' onclick=\"javascript:openPostOpNotesTemplateNew(); return false;\"><img src=\"../../../../../Images/Toolbar/copy16.png\" alt=\"b\" />&nbsp;Save to Template</a>";
            else if (noteType == "ANS")
                retval =
                    "<a href='#' onclick=\"javascript:openAnestesyNotesTemplateNew(); return false;\"><img src=\"../../../../../Images/Toolbar/copy16.png\" alt=\"b\" />&nbsp;Save to Template</a>";
            else
                retval =
                    "<a href='#' onclick=\"javascript:openAnestPostOpNotesTemplateNew(); return false;\"><img src=\"../../../../../Images/Toolbar/copy16.png\" alt=\"b\" />&nbsp;Save to Template</a>";

            return retval;
        }

        #region Record Detail Method Function

        private void PopulateGridDetail()
        {
            //Display Data Detail
            EpisodeProcedures = null; //Reset Record Detail
            if (!IsAnesthetist)
            {
                var ds = from d in EpisodeProcedures
                         where d.BookingNo == BookingNo && d.OpNotesSeqNo == SeqNo && d.IsVoid == false
                         select d;
                grdEpisodeProcedure.DataSource = ds;
            }
            else
            {
                var ds = from d in EpisodeProcedures
                         where d.BookingNo == BookingNo && d.OpNotesSeqNo == "000" && d.CreatedByParamedicId == ParamedicID && d.IsVoid == false
                         select d;
                grdEpisodeProcedure.DataSource = ds;
            }
            grdEpisodeProcedure.MasterTableView.IsItemInserted = false;
            grdEpisodeProcedure.MasterTableView.ClearEditItems();
            grdEpisodeProcedure.DataBind();

            ImplantInstallations = null; //Reset Record Detail
            grdImplantInstallation.DataSource = ImplantInstallations;
            grdImplantInstallation.MasterTableView.IsItemInserted = false;
            grdImplantInstallation.MasterTableView.ClearEditItems();
            grdImplantInstallation.DataBind();
        }

        #region EpisodeProcedure
        private EpisodeProcedureCollection EpisodeProcedures
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEpisodeProcedure" + Request.UserHostName];
                    if (obj != null)
                        return ((EpisodeProcedureCollection)(obj));
                }

                var coll = new EpisodeProcedureCollection();
                var query = new EpisodeProcedureQuery("a");
                var booking = new ServiceUnitBookingQuery("b");
                var usr = new AppUserQuery("c");
                var ctg = new AppStandardReferenceItemQuery("d");
                query.LeftJoin(booking).On(booking.BookingNo == query.BookingNo);
                query.InnerJoin(usr).On(usr.UserID == query.CreateByUserID);
                query.LeftJoin(ctg).On(ctg.StandardReferenceID == "ProcedureCategory" && ctg.ItemID == query.SRProcedureCategory);
                query.Select
                (
                    query.SelectAll(),
                    ctg.ItemName.As("refToStdRef_ProcedureCategory"),
                    usr.ParamedicID.As("refToAppUser_ParamedicID")
                );

                query.Where(query.RegistrationNo == RegistrationNo);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["collEpisodeProcedure" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEpisodeProcedure" + Request.UserHostName] = value; }
        }

        protected void grdEpisodeProcedure_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsAnesthetist)
            {
                var ds = from d in EpisodeProcedures
                         where d.BookingNo == BookingNo && d.OpNotesSeqNo == SeqNo && d.IsVoid == false
                         select d;
                grdEpisodeProcedure.DataSource = ds;
            }
            else
            {
                var ds = from d in EpisodeProcedures
                         where d.BookingNo == BookingNo && d.OpNotesSeqNo == "000" && d.CreatedByParamedicId == ParamedicID && d.IsVoid == false
                         select d;
                grdEpisodeProcedure.DataSource = ds;
            }
        }

        protected void grdEpisodeProcedure_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EpisodeProcedureMetadata.ColumnNames.SequenceNo]);
            EpisodeProcedure entity = FindEpisodeProcedure(sequenceNo);
            if (entity != null)
                SetEntityValueEpisodeProcedure(entity, e);
        }

        protected void grdEpisodeProcedure_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EpisodeProcedureMetadata.ColumnNames.SequenceNo]);
            EpisodeProcedure entity = FindEpisodeProcedure(sequenceNo);
            if (entity != null)
                entity.IsVoid = true;
        }

        protected void grdEpisodeProcedure_InsertCommand(object source, GridCommandEventArgs e)
        {
            EpisodeProcedure entity = EpisodeProcedures.AddNew();
            SetEntityValueEpisodeProcedure(entity, e);

            e.Canceled = true;
            grdEpisodeProcedure.Rebind();
        }

        private EpisodeProcedure FindEpisodeProcedure(String sequenceNo)
        {
            EpisodeProcedureCollection coll = EpisodeProcedures;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValueEpisodeProcedure(EpisodeProcedure entity, GridCommandEventArgs e)
        {
            var userControl = (EpisodeProcedureEntryDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = RegistrationNo;
                entity.SequenceNo = userControl.SequenceNo;
                entity.ProcedureDate = txtProcedureDate.SelectedDate;

                var procTime = txtProcedureTime.SelectedDate.Value;
                entity.ProcedureTime = string.Format("{0:00}:{1:00}", procTime.Hour, procTime.Minute);
                entity.ProcedureDate2 = txtProcedureDate2.SelectedDate;

                procTime = txtProcedureTime2.SelectedDate.Value;
                entity.ProcedureTime2 = string.Format("{0:00}:{1:00}", procTime.Hour, procTime.Minute);

                entity.IncisionDateTime = DateTime.Parse(txtIncisionDate.SelectedDate.Value.ToShortDateString() + " " + txtIncisionTime.SelectedDate.Value.ToShortTimeString());
                entity.ParamedicID = string.IsNullOrWhiteSpace(txtParamedicID.Text)? cboPhysicianID.SelectedValue: txtParamedicID.Text;
                entity.ParamedicName = string.IsNullOrWhiteSpace(ParamedicName)? cboPhysicianID.Text: ParamedicName;
                entity.ParamedicID2 = ParamedicID2;
                entity.ProcedureID = userControl.ProcedureID;
                entity.ProcedureName = userControl.ProcedureName;
                entity.SRProcedureCategory = userControl.SRProcedureCategory;
                entity.ProcedureCategoryName = userControl.ProcedureCategoryName;
                entity.ProcedureSynonym = userControl.ProcedureSynonym;
                entity.SRAnestesi = SRAnestesi;
                entity.RoomID = RoomID;
                entity.IsCito = IsCito;
                entity.AssistantID1 = AssistantID1;
                entity.AssistantID2 = AssistantID2;
                entity.BookingNo = BookingNo;
                if (!IsAnesthetist)
                    entity.OpNotesSeqNo = SeqNo;
                else
                    entity.OpNotesSeqNo = "000";
                entity.ParamedicID2a = ParamedicID2a;
                entity.ParamedicID3a = ParamedicID3a;
                entity.ParamedicID4a = ParamedicID4a;
                entity.IsFromOperatingRoom = true;
                entity.AssistantIDAnestesi = AssistantIDAnestesi;
                entity.InstrumentatorID1 = InstrumentatorID1;
                entity.InstrumentatorID2 = InstrumentatorID2;
                entity.IsVoid = false;

                if (entity.es.IsAdded)
                {
                    entity.CreateByUserID = AppSession.UserLogin.UserID;
                    entity.CreateDateTime = (new DateTime()).NowAtSqlServer();
                }
                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

                var usr = new AppUser();
                entity.CreatedByParamedicId = usr.LoadByPrimaryKey(entity.CreateByUserID) ? usr.ParamedicID : string.Empty;
            }
        }

        protected void grdEpisodeProcedure_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    EpisodeProcedure item = EpisodeProcedures[e.Item.DataSetIndex];
            //    if (item != null)
            //    {
            //        if (item.IsVoid ?? false)
            //        {
            //            for (int i = 0; i < e.Item.Cells.Count; i++)
            //            {
            //                if (i > 0 && i < e.Item.Cells.Count)
            //                    e.Item.Cells[i].Font.Strikeout = true;
            //            }
            //        }
            //    }
            //}
        }
        #endregion
        #region ImplantInstallation
        private ImplantInstallationCollection ImplantInstallations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collImplantInstallation" + Request.UserHostName];
                    if (obj != null)
                        return ((ImplantInstallationCollection)(obj));
                }

                var coll = new ImplantInstallationCollection();
                var query = new ImplantInstallationQuery("a");

                query.Select
                    (
                        query
                    );

                query.Where(query.BookingNo == txtBookingNo.Text);

                query.OrderBy(query.SeqNo.Ascending);

                coll.Load(query);

                Session["collImplantInstallation" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collImplantInstallation" + Request.UserHostName] = value; }
        }

        protected void grdImplantInstallation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdImplantInstallation.DataSource = ImplantInstallations;
        }

        protected void grdImplantInstallation_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String id =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        ImplantInstallationMetadata.ColumnNames.SeqNo]);
            ImplantInstallation entity = FindItemGrid(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdImplantInstallation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String id =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][ImplantInstallationMetadata.ColumnNames.SeqNo]);
            ImplantInstallation entity = FindItemGrid(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdImplantInstallation_InsertCommand(object source, GridCommandEventArgs e)
        {
            ImplantInstallation entity = ImplantInstallations.AddNew();
            SetEntityValue(entity, e);

            e.Canceled = true;
            grdImplantInstallation.Rebind();
        }

        private void SetEntityValue(ImplantInstallation entity, GridCommandEventArgs e)
        {
            var userControl = (ImplantInstallationDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.BookingNo = txtBookingNo.Text;
                entity.SeqNo = userControl.SeqNo;
                entity.ImplantType = userControl.ImplantType;
                entity.SerialNo = userControl.SerialNo;
                entity.Qty = userControl.Qty;
                entity.PlacementSite = userControl.PlacementSite;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }

        private ImplantInstallation FindItemGrid(string id)
        {
            ImplantInstallationCollection coll = ImplantInstallations;
            ImplantInstallation retval = null;
            foreach (ImplantInstallation rec in coll)
            {
                if (rec.SeqNo.Equals(id))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }
        #endregion

        #endregion

        protected void txtProcedureDate_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            txtIncisionDate.SelectedDate = txtProcedureDate.SelectedDate;
        }

        protected void txtProcedureTime_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (!txtIncisionTime.IsEmpty)
                txtIncisionTime.SelectedDate = txtProcedureTime.SelectedDate;

            if (!txtProcedureTime2.IsEmpty)
            {
                txtProcedureTime2.SelectedDate = txtProcedureTime.SelectedDate.Value.Add(new TimeSpan(0, int.Parse(AppSession.Parameter.DefaultSurgeryTime), 0));
                txtProcedureTime.SelectedDate = txtProcedureTime.SelectedDate.Value.Add(new TimeSpan(0, 0, 1, 0));
            }
        }
    }
}
