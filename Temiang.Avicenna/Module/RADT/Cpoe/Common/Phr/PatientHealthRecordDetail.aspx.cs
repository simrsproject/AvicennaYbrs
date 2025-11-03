using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.Module.Emr.Phr
{
    public partial class PatientHealthRecordDetail : BasePageDialogEntry
    {
        private AppAutoNumberLast _autoNumber, _autoNumber2;
        private Dictionary<string, esEntityWAuditLog> _othRelatedEntities = new Dictionary<string, esEntityWAuditLog>();
        private DateTime _serverDate = (new DateTime()).NowAtSqlServer();


        private QuestionForm _qform = null;
        private QuestionForm QuestionFormCurrent
        {
            get
            {
                if (_qform == null)
                {
                    _qform = new QuestionForm();
                    _qform.LoadByPrimaryKey(QuestionFormID);
                }

                return _qform;
            }
        }
        protected string QuestionFormID
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["fid"]))
                {
                    if (string.IsNullOrWhiteSpace(hdnQuestionFormID.Value))
                    {
                        // Dipanggil dari Integrated Notes list yg tidak kirim QuestionFormID yg SOAP nya hasil dari import isian PHR
                        // Dg key RegistrationNo & TransactionNo sudah unique

                        // SELECT  phr.RegistrationNo,phr.TransactionNo FROM PatientHealthRecord AS phr GROUP BY phr.RegistrationNo,phr.TransactionNo HAVING COUNT(1)>1
                        var phr = new PatientHealthRecord();
                        var qr = new PatientHealthRecordQuery("a");
                        qr.es.Top = 1;
                        qr.Where(qr.RegistrationNo == RegistrationNo, qr.TransactionNo == TransactionNo);
                        phr.Load(qr);
                        hdnQuestionFormID.Value = phr.QuestionFormID;
                    }

                    return hdnQuestionFormID.Value;
                }

                return Request.QueryString["fid"];
            }
        }
        private string ServiceUnitID
        {
            get { return Request.QueryString["unit"]; }
        }
        private string ReferenceNo
        {
            get
            {
                return Request.QueryString["refno"];
            }
        }
        private bool IsLetterForm
        {
            get
            {
                return Request.QueryString["isLetter"] == "1";
            }
        }

        private string TransactionNo
        {
            get
            {
                return Request.QueryString["trno"] ?? Request.QueryString["id"];
            }
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(_serverDate, AppEnum.AutoNumber.PatientHealthRecord);
            return _autoNumber.LastCompleteNumber;
        }

        private string GetNewLetterNo(string formId)
        {
            var qf = new QuestionForm();
            qf.LoadByPrimaryKey(formId);
            if (string.IsNullOrEmpty(qf.SRAutoNumber))
                return string.Empty;

            _autoNumber2 = Helper.GetNewAutoNumber(DateTime.Now.Date, qf.SRAutoNumber);
            return _autoNumber2.LastCompleteNumber;
        }


        #region Page Event & Initialize

        protected override void OnPreInit(EventArgs e)
        {
            // Check tipe QuestionForm
            if (Request.QueryString["md"].ToLower() == "new")
            {
                if (QuestionFormCurrent.SRQuestionFormType == QuestionForm.QuestionFormType.PatientTransfer &&
                    string.IsNullOrEmpty(ReferenceNo))
                {
                    var qs = string.Empty;
                    foreach (var key in Request.QueryString.AllKeys)
                    {
                        qs = string.Concat(qs, "&", key, "=", Request.QueryString[key]);
                    }

                    // Pilih dahulu TransferNo nya
                    Response.Redirect(string.Format("PatientTransferReferenceDialog.aspx?{0}", qs.Substring(1)));
                    return;
                }

                // Pra Registration
                if (QuestionFormCurrent.SRQuestionFormType == QuestionForm.QuestionFormType.PraRegistration)
                {
                    var phr = new PatientHealthRecordQuery("b");
                    phr.Where(phr.RegistrationNo == PatientID);  //RegistrationNo diisi PatientID utk QuestionFormType.PraRegistration           
                    phr.es.Top = 1;
                    var dtbCheck = phr.LoadDataTable();
                    if (dtbCheck.Rows.Count > 0)
                    {

                        var qs = string.Empty;
                        foreach (var key in Request.QueryString.AllKeys)
                        {
                            qs = string.Concat(qs, "&", key, "=", Request.QueryString[key]);
                        }

                        // Berikan pilihan Pra Reg form yg sudah dibuat
                        Response.Redirect(string.Format("PhrPraRegReferenceDialog.aspx?{0}", qs.Substring(1)));
                    }

                    return;
                }
            }

            base.OnPreInit(e);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var qrPrgId = Request.QueryString["prgid"];
            if (qrPrgId != null && !string.IsNullOrWhiteSpace(qrPrgId))
                ProgramID = qrPrgId;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                if (Request.QueryString["iusig"] == "1")
                {
                    IsProgramUseSignature = true;
                }
            }

            // QuestionFormID dikirm dari pemanggil
            // Hanya untuk Entry Single Record, Edit and Save
            // IsSingleRecordMode = true;

            // Hanya bisa mode add dari form pemanggil
            // Tidak menggunakan SingleRecordMode krn utk bisa approve dan delete
            ToolBar.AddVisible = false;
            ToolBar.NavigationVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.ApprovalUnApprovalVisible = (QuestionFormCurrent.IsUsingApproval ?? false);
            ToolBar.SaveAndEditVisible = true;

            tblOtherInfo.Visible = !IsLetterForm;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Enable saat mode New saja
            btnCopyLast.Enabled = false;

            if (!IsPostBack)
            {
                // Ganti ReferenceNo jika dipanggil dari edit
                var referenceNo = ReferenceNo;
                var phr = new PatientHealthRecord();
                if (phr.LoadByPrimaryKey(
                    TransactionNo,
                    RegistrationNo,
                    QuestionFormID))
                {
                    referenceNo = phr.ReferenceNo;
                }

                if (!string.IsNullOrWhiteSpace(QuestionFormCurrent.RmNO))
                    this.Title = string.Format("{0} [{1}]", QuestionFormCurrent.QuestionFormName, QuestionFormCurrent.RmNO);
                else
                    this.Title = string.Format("{0}", QuestionFormCurrent.QuestionFormName);

                ctlReference.Visible = QuestionFormCurrent.SRQuestionFormType ==
                                       QuestionForm.QuestionFormType.ServiceUnitBooking
                                       || QuestionFormCurrent.SRQuestionFormType ==
                                       QuestionForm.QuestionFormType.Physiotherapy
                                       || QuestionFormCurrent.SRQuestionFormType ==
                                       QuestionForm.QuestionFormType.PatientTransfer;


                switch (QuestionFormCurrent.SRQuestionFormType)
                {
                    case QuestionForm.QuestionFormType.ServiceUnitBooking:
                        {
                            lblReferenceType.Text = "Operating Theater";
                            lblReferenceNo.Text = "Booking No";
                            lblReferenceNote.Text = "Operating Theater";

                            var sbook = new ServiceUnitBooking();
                            sbook.LoadByPrimaryKey(referenceNo);
                            txtReferenceNote.Text = string.Format("Diagnose: {0}{1}Notes: {2}", sbook.Diagnose, Environment.NewLine, sbook.Notes);
                            break;
                        }
                    case QuestionForm.QuestionFormType.Physiotherapy:
                        {
                            lblReferenceType.Text = "Operating Theater";
                            lblReferenceNo.Text = "Consult / Refer No";
                            lblReferenceNote.Text = "";

                            var consult = new ParamedicConsultRefer();
                            consult.LoadByPrimaryKey(referenceNo);
                            txtReferenceNote.Text = string.Concat("Exam:", Environment.NewLine, consult.ActionExamTreatment, Environment.NewLine, Environment.NewLine, "Note:", Environment.NewLine, consult.Notes);
                            break;
                        }
                    case QuestionForm.QuestionFormType.PatientTransfer:
                        {
                            lblReferenceType.Text = "Patient Transfer";
                            lblReferenceNo.Text = "Transfer No";
                            lblReferenceNote.Text = "Transfer To";

                            var trf = new PatientTransferHistory();
                            var refNo = ReferenceNo == "-" ? string.Empty : referenceNo; // "Tanda - untuk mengakali TranferNo yg diset kosong 
                            trf.LoadByPrimaryKey(RegistrationNo, refNo);

                            var su = new ServiceUnit();
                            su.LoadByPrimaryKey(trf.ServiceUnitID);

                            var room = new ServiceRoom();
                            room.LoadByPrimaryKey(trf.RoomID);

                            txtReferenceNote.Text = string.Format("Service Unit : {0}{1}Room : {2}{1}Bed : {3}", su.ServiceUnitName, Environment.NewLine, room.RoomName, trf.BedID);
                            break;
                        }
                }
            }
            phrCtl.InitializedQuestion(QuestionFormID);

        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            var fw_btnAutoSave = (Button)Helper.FindControlRecursive(Master, "fw_btnAutoSave");
            ajax.AddAjaxSetting(fw_btnAutoSave, hdnTransactionNo);
        }

        #endregion

        #region Toolbar Menu Event


        protected override void OnMenuNewClick()
        {
            var phr = new PatientHealthRecord { QuestionFormID = QuestionFormID };
            OnPopulateEntryControl(phr);

            txtTransactionNo.Text = GetNewTransactionNo();
            txtReferenceNo.Text = ReferenceNo;
            txtRecordDate.SelectedDate = _serverDate.Date;
            txtRecordTime.Text = _serverDate.ToString("HH:mm:");
            txtRegistrationNo.Text = RegistrationNo;

            btnCopyLast.Enabled = true;

            PopulateRegistrationInformation();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            args.IsCancel = true;

            var entity = new PatientHealthRecord();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text, txtRegistrationNo.Text, QuestionFormID))
            {
                var collValue = new PatientHealthRecordLineCollection();
                collValue.Query.Where
                    (
                        collValue.Query.TransactionNo == txtTransactionNo.Text,
                        collValue.Query.RegistrationNo == txtRegistrationNo.Text,
                        collValue.Query.QuestionFormID == QuestionFormID
                    );
                collValue.LoadAll();

                using (var trans = new esTransactionScope())
                {
                    collValue.MarkAllAsDeleted();
                    collValue.Save();
                    entity.MarkAsDeleted();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
                args.IsCancel = false;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }

            if (!args.IsCancel)
            {
                IsRecordHasChanged = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeAndApply", "CloseAndApply();", true);
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return base.OnGetAdditionalJavaScriptCloseAndApply();
        }

        //public override void OnMenuSaveAndEditClick(ValidateArgs args)
        //{
        //    if (string.IsNullOrWhiteSpace(hdnTransactionNo.Value))
        //    {
        //        OnMenuSaveNewClick(args);
        //        if (!args.IsCancel && string.IsNullOrWhiteSpace(args.MessageText))
        //            args.MessageText = "New Health Record has saved";
        //    }
        //    else
        //    {
        //        OnMenuSaveEditClick(args);
        //        if (!args.IsCancel && string.IsNullOrWhiteSpace(args.MessageText))
        //            args.MessageText = "Current Health Record has saved";
        //    }
        //}

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {
            IsSingleEntryRestrictedAdd(args);
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (!string.IsNullOrWhiteSpace(hdnTransactionNo.Value)) // Tanda sudah berubah ke mode Edit
            {
                // Cek jika mode new sudah di Save oleh tombol Save And Continue maka jalankan OnMenuSaveEditClick
                OnMenuSaveEditClick(args);
                return;
            }

            // Cek single entry
            if (IsSingleEntryRestrictedAdd(args))
                return;

            var collValue = new PatientHealthRecordLineCollection();
            var entity = new PatientHealthRecord();

            txtTransactionNo.Text = GetNewTransactionNo();
            _autoNumber.Save();

            var letterNo = GetNewLetterNo(QuestionFormID);
            if (letterNo != string.Empty)
            {
                _autoNumber2.Save();
                entity.LetterNo = letterNo;
            }

            SaveEntity(entity, collValue);

            if (!args.IsCancel)
                IsRecordHasChanged = true;
        }

        private bool IsSingleEntryRestrictedAdd(ValidateArgs args)
        {
            // Cek single entry
            if (QuestionFormCurrent.IsSingleEntry ?? false)
            {
                var phr = new PatientHealthRecordCollection();
                phr.Query.Where(phr.Query.RegistrationNo == RegistrationNo,
                    phr.Query.QuestionFormID == QuestionFormID);

                // Chek apakah menempel ke transaksi serviceunit booking
                if (!string.IsNullOrWhiteSpace(txtReferenceNo.Text))
                    phr.Query.Where(phr.Query.ReferenceNo == txtReferenceNo.Text);

                phr.LoadAll();
                if (phr.Count > 0)
                {
                    args.IsCancel = true;
                    if (string.IsNullOrWhiteSpace(txtReferenceNo.Text))
                        args.MessageText =
                            string.Format("{0}[{1}] has exist, this form is single entry for current registration", QuestionFormCurrent.QuestionFormName, QuestionFormCurrent.RmNO);
                    else
                    {
                        args.MessageText =
    string.Format("{0}[{1}] has exist, this form is single entry for current {2} {3}", QuestionFormCurrent.QuestionFormName, QuestionFormCurrent.RmNO, lblReferenceNo.Text, txtReferenceNo.Text);
                    }
                    return true;
                }
            }
            return false;
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            // Cek Sharing Edit
            if (!QuestionFormCurrent.IsSharingEdit ?? false)
            {
                var phr = new PatientHealthRecord();
                if (phr.LoadByPrimaryKey(txtTransactionNo.Text, RegistrationNo, QuestionFormID)) ;
                {
                    if (!AppSession.UserLogin.UserID.Equals(phr.CreateByUserID))
                    {
                        args.IsCancel = true;
                        args.MessageText =
                            string.Format("{0}[{1}] has created by other user, can't edit",
                                QuestionFormCurrent.QuestionFormName, QuestionFormCurrent.RmNO);
                        return;
                    }
                }
            }

            var entity = new PatientHealthRecord();
            entity.LoadByPrimaryKey(txtTransactionNo.Text, txtRegistrationNo.Text, QuestionFormID);

            var collValue = new PatientHealthRecordLineCollection();

            collValue.Query.Where
                (
                    collValue.Query.TransactionNo == txtTransactionNo.Text,
                    collValue.Query.RegistrationNo == txtRegistrationNo.Text,
                    collValue.Query.QuestionFormID == QuestionFormID
                );
            collValue.LoadAll();
            SaveEntity(entity, collValue);

            if (!args.IsCancel)
                IsRecordHasChanged = true;

        }

        private void ApprUnApprPhr(bool isAppr)
        {
            // Update line  ABY & ADT 
            //1. Load PatientHealthRecordLine yg SRAnswerType = ABY & ADT
            var hrLineColl = new PatientHealthRecordLineCollection();
            var query = new PatientHealthRecordLineQuery("a");
            var qQuest = new QuestionQuery("b");

            query.InnerJoin(qQuest).On(query.QuestionID == qQuest.QuestionID);

            query.Select
            (
                query,
                qQuest.SRAnswerType.As("refToQuestion_SRAnswerType")
            );
            query.Where
            (
                query.TransactionNo == txtTransactionNo.Text,
                query.RegistrationNo == txtRegistrationNo.Text,
                query.QuestionFormID == QuestionFormID,
                qQuest.SRAnswerType.In("ABY", "ADT")
            );
            query.OrderBy(qQuest.IndexNo.Ascending);

            hrLineColl.Load(query);

            //2. Update ABY-> UserName, ADT-> Now
            foreach (var line in hrLineColl)
            {
                if (line.SRAnswerType == "ABY")
                    line.QuestionAnswerText = isAppr ? AppSession.UserLogin.UserName : string.Empty;
                else if (line.SRAnswerType == "ADT")
                    line.QuestionAnswerText = isAppr ? DateTime.Now.ToString("MM/dd/yyyy HH:mm") : string.Empty;
            }
            hrLineColl.Save();
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new PatientHealthRecord();
            entity.LoadByPrimaryKey(txtTransactionNo.Text, txtRegistrationNo.Text, QuestionFormID);
            entity.IsApproved = true;
            entity.ApprovedByUserID = AppSession.UserLogin.UserID;
            entity.ApprovedDatetime = DateTime.Now;
            entity.Save();

            ApprUnApprPhr(true);

            if (!args.IsCancel)
                IsRecordHasChanged = true;
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new PatientHealthRecord();
            entity.LoadByPrimaryKey(txtTransactionNo.Text, txtRegistrationNo.Text, QuestionFormID);

            //Reset
            entity.str.IsApproved = string.Empty;
            entity.str.ApprovedByUserID = string.Empty;
            entity.str.ApprovedDatetime = string.Empty;
            entity.Save();

            ApprUnApprPhr(false);

            if (!args.IsCancel)
                IsRecordHasChanged = true;
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("RegistrationNo='{0}' AND QuestionFormID='{1}'", txtRegistrationNo.Text.Trim(), QuestionFormID);
            auditLogFilter.TableName = "PatientHealthRecord";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {

            var form = new QuestionForm();
            form.LoadByPrimaryKey(Request.QueryString["fid"]);

            AppSession.PrintJobReportID = form.ReportProgramID;

            printJobParameters.AddNew("p_RegistrationNo", txtRegistrationNo.Text);
            printJobParameters.AddNew("p_QuestionFormID", Request.QueryString["fid"]);
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }
        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            phrCtl.SetReadOnlyPatientHealthRecordLine(newVal == AppEnum.DataMode.Read, PatientCurrent, RegistrationCurrent);
        }

        protected override void OnMenuEditClick()
        {
            txtRegistrationNo.Text = Request.QueryString["regno"];

            var entity = new PatientHealthRecord();
            entity.LoadByPrimaryKey(txtTransactionNo.Text, txtRegistrationNo.Text, QuestionFormID);

            OnPopulateEntryControl(entity);

            PopulateRegistrationInformation();
        }

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var transactionNo = string.IsNullOrEmpty(txtTransactionNo.Text)
                ? TransactionNo
                : txtTransactionNo.Text;
            var regNo = string.IsNullOrEmpty(txtRegistrationNo.Text)
                ? RegistrationNo
                : txtRegistrationNo.Text;

            var entity = new PatientHealthRecord();

            entity.LoadByPrimaryKey(
                transactionNo,
                regNo,
                QuestionFormID);

            OnPopulateEntryControl(entity);
        }

        protected void OnPopulateEntryControl(PatientHealthRecord phr)
        {
            if (phr.RecordDate != null)
            {
                var date = phr.RecordDate.Value;
                var times = phr.RecordTime.Split(':');
                vitalSignInfoCtl.VitalSignDateTime = new DateTime(date.Year, date.Month, date.Day, times[0].ToInt(),
                    times[1].ToInt(), 0);
            }
            else
                vitalSignInfoCtl.VitalSignDateTime = DateTime.Now;

            hdnCreateByUserID.Value = phr.CreateByUserID;
            hdnQuestionFormID.Value = phr.QuestionFormID;

            hdnTransactionNo.Value = phr.TransactionNo; // Untuk pengecekan pada proses save and stay
            txtTransactionNo.Text = phr.TransactionNo;
            txtRegistrationNo.Text = phr.RegistrationNo;
            txtRecordDate.SelectedDate = phr.RecordDate;
            txtRecordTime.Text = phr.RecordTime;
            chkIsComplete.Checked = phr.IsComplete ?? false;
            chkIsApproved.Checked = phr.IsApproved ?? false;
            txtReferenceNo.Text = phr.ReferenceNo;

            if (!string.IsNullOrEmpty(phr.ExaminerID))
            {
                var query = new ParamedicQuery("a");
                query.Select
                    (
                        query.ParamedicID,
                        query.ParamedicName
                    );
                query.Where(query.ParamedicID == phr.ExaminerID);

                cboExaminerID.DataSource = query.LoadDataTable();
                cboExaminerID.DataBind();
                ComboBox.SelectedValue(cboExaminerID, phr.ExaminerID);
            }

            if (!string.IsNullOrWhiteSpace(QuestionFormCurrent.RmNO))
                txtFormName.Text = string.Format("{0} [{1}]", QuestionFormCurrent.QuestionFormName, QuestionFormCurrent.RmNO);
            else
                txtFormName.Text = string.Format("{0}", QuestionFormCurrent.QuestionFormName);

            PopulateRegistrationInformation();

            if (phr.TransactionNo == null)
            {
                phr.TransactionNo = string.Empty;
                phr.RegistrationNo = txtRegistrationNo.Text;
                phr.QuestionFormID = QuestionFormID;
            }

            phrCtl.PopulateValue(PatientCurrent, RegistrationCurrent, _othRelatedEntities, phr, LastRegistrationNo);
        }

        public override bool OnGetStatusMenuAdd()
        {
            return false; // Tidak boleh add dari toolbar krn tipe form, ref no harus dikirim

            //return string.IsNullOrEmpty(QuestionFormCurrent.RestrictionUserType) ||
            //       QuestionFormCurrent.RestrictionUserType.Contains(AppSession.UserLogin.SRUserType);
        }

        public override bool OnGetStatusMenuEdit()
        {
            return ((QuestionFormCurrent.IsSharingEdit ?? false) && chkIsApproved.Checked == false)
                   || (chkIsApproved.Checked == false && AppSession.UserLogin.UserID.Equals(hdnCreateByUserID.Value));
        }

        public override bool OnGetStatusMenuDelete()
        {
            return chkIsApproved.Checked == false && AppSession.UserLogin.UserID.Equals(hdnCreateByUserID.Value);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return (QuestionFormCurrent.IsUsingApproval ?? false) && chkIsApproved.Checked == false;
        }

        #endregion

        #region Private Method Standard

        private void SaveEntity(PatientHealthRecord entity, PatientHealthRecordLineCollection collValue)
        {
            using (var trans = new esTransactionScope())
            {
                var reg = RegistrationCurrent;

                if (string.IsNullOrWhiteSpace(hdnTransactionNo.Value)) // New PHR
                {
                    entity.TransactionNo = txtTransactionNo.Text;
                    entity.RegistrationNo = txtRegistrationNo.Text;
                    entity.QuestionFormID = QuestionFormID;
                    entity.ServiceUnitID = string.IsNullOrEmpty(ServiceUnitID) ? reg.ServiceUnitID : ServiceUnitID;
                    entity.CreateByUserID = AppSession.UserLogin.UserID;
                    entity.CreateDateTime = _serverDate;
                }

                //reg.Complaint = txtComplaint.Text;
                //reg.Save();

                entity.RecordDate = txtRecordDate.SelectedDate;
                entity.RecordTime = txtRecordTime.TextWithLiterals;
                entity.EmployeeID = AppSession.UserLogin.UserID;
                entity.ReferenceNo = txtReferenceNo.Text == "-" ? string.Empty : txtReferenceNo.Text;

                entity.ExaminerID = cboExaminerID.SelectedValue;
                entity.IsComplete = chkIsComplete.Checked;

                phrCtl.SetEntityValue(PatientCurrent, reg, _othRelatedEntities, entity, collValue, LastRegistrationNo);
                entity.Save();
                collValue.Save();

                if (PatientCurrent.es.IsModified)
                    PatientCurrent.Save();

                if (reg.es.IsModified)
                    reg.Save();

                // othRelatedEntities
                foreach (var othRelatedEntity in _othRelatedEntities.Values)
                {
                    if (othRelatedEntity.es.IsModified || othRelatedEntity.es.IsAdded)
                        othRelatedEntity.Save();
                }

                // Save ReferenceNo
                switch (QuestionFormCurrent.SRQuestionFormType)
                {
                    case QuestionForm.QuestionFormType.ServiceUnitBooking:
                        {
                            // Tidak usah direkam ke ServiceUnitBookingForm krn bermasalah jika dihapus di PHR nya dan tidak support Multi Form
                            //var bform = new ServiceUnitBookingForm();
                            //if (!bform.LoadByPrimaryKey(txtReferenceNo.Text, QuestionFormID))
                            //{
                            //    bform = new ServiceUnitBookingForm();
                            //    bform.QuestionFormID = QuestionFormID;
                            //    bform.BookingNo = txtReferenceNo.Text;
                            //    bform.TransactionNo = entity.TransactionNo;
                            //    bform.CreatedByUserID = AppSession.UserLogin.UserID;
                            //    bform.CreatedDateTime = _serverDate;
                            //}

                            //bform.LastUpdateDateTime = _serverDate;
                            //bform.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            //bform.Save();
                            break;
                        }
                    case QuestionForm.QuestionFormType.Physiotherapy:
                        {
                            // Save to ParamedicConsultForm
                            // Yakinkan form type Physiotherapy tidak muncul di Health Record
                            var cform = new ParamedicConsultForm();
                            if (!cform.LoadByPrimaryKey(txtReferenceNo.Text, entity.TransactionNo))
                            {
                                cform = new ParamedicConsultForm();
                                cform.QuestionFormID = QuestionFormID;
                                cform.ConsultReferNo = txtReferenceNo.Text;
                                cform.TransactionNo = entity.TransactionNo;
                                cform.RegistrationNo = entity.RegistrationNo;
                                cform.CreatedByUserID = AppSession.UserLogin.UserID;
                                cform.CreatedDateTime = _serverDate;
                            }

                            cform.LastUpdateDateTime = _serverDate;
                            cform.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            cform.Save();
                            break;
                        }
                }

                // Save to ReistrationInfoMedic for InPatient and Emergency
                if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient || reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient)
                    SaveSoap(entity);

                //Commit if success, Rollback if failed
                trans.Complete();
                hdnTransactionNo.Value = txtTransactionNo.Text; // Untuk pengecekan pada proses save and stay
            }
        }

        private void SaveSoap(PatientHealthRecord phr)
        {
            var qf = new QuestionForm();
            qf.LoadByPrimaryKey(phr.QuestionFormID);
            if (!(qf.IsSoapForm ?? false)) return;

            var ent = new RegistrationInfoMedic();
            var qr = new RegistrationInfoMedicQuery();
            qr.Where(qr.RegistrationNo == phr.RegistrationNo, qr.ReferenceNo == phr.TransactionNo);
            qr.es.Top = 1;

            if (!ent.Load(qr))
            {
                ent = new RegistrationInfoMedic();
                var autoNumber = Helper.GetNewAutoNumber(_serverDate, AppEnum.AutoNumber.RegInfoMedicNo);
                ent.RegistrationInfoMedicID = autoNumber.LastCompleteNumber;
                autoNumber.Save();

                ent.RegistrationNo = RegistrationNo;

                ent.SRMedicalNotesInputType = "SOAP";
                ent.ServiceUnitID = phr.ServiceUnitID;
                ent.SRUserType = AppSession.UserLogin.SRUserType;
                ent.str.ParamedicID = AppSession.UserLogin.ParamedicID;
                ent.ReferenceNo = phr.TransactionNo;
                ent.ReferenceType = "PHR";
            }

            string subjective = string.Empty;
            string objective = string.Empty;
            string assessment = string.Empty;
            string planning = string.Empty;
            string instruction = string.Empty;
            phrCtl.PopulateSoapValue(phr, ref subjective, ref objective, ref assessment, ref planning, ref instruction);

            ent.Info1 = subjective;
            ent.Info2 = objective;
            ent.Info3 = assessment;
            ent.Info4 = planning;
            ent.PpaInstruction = instruction;
            ent.IsPRMRJ = false;
            var date = phr.RecordDate.Value;
            var times = phr.RecordTime.Split(':');
            ent.DateTimeInfo = new DateTime(date.Year, date.Month, date.Day, times[0].ToInt(), times[1].ToInt(), 0);
            ent.Save();
        }
        #endregion

        private Patient _patientCurrent;
        private Patient PatientCurrent
        {
            get
            {
                if (_patientCurrent == null)
                {
                    _patientCurrent = new Patient();
                    _patientCurrent.LoadByPrimaryKey(RegistrationCurrent.PatientID);
                }

                return _patientCurrent;
            }
        }

        private string _lastRegistrationNo = null;
        private string LastRegistrationNo
        {
            get
            {
                if (_lastRegistrationNo == null)
                {
                    var lastReg = PatientCurrent.LastRegistration();
                    if (lastReg != null)
                        _lastRegistrationNo = lastReg.RegistrationNo;
                    else
                        _lastRegistrationNo = string.Empty;
                }

                return _lastRegistrationNo;
            }

        }
        private Registration _registrationCurrent;
        private Registration RegistrationCurrent
        {
            get
            {
                if (_registrationCurrent == null)
                {
                    _registrationCurrent = new Registration();
                    _registrationCurrent.LoadByPrimaryKey(RegistrationNo);
                }

                return _registrationCurrent;
            }
        }
        private void PopulateRegistrationInformation()
        {
            var registration = RegistrationCurrent;

            var patient = new Patient();
            patient.LoadByPrimaryKey(registration.PatientID);

            hdnPatientID.Value = patient.PatientID;
            txtMedicalNo.Text = patient.MedicalNo;

            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = patient.PatientName;
            txtGender.Text = patient.Sex;

            optSexFemale.Checked = (patient.Sex == "F");
            optSexFemale.Enabled = (patient.Sex == "F");
            optSexMale.Checked = (patient.Sex == "M");
            optSexMale.Enabled = (patient.Sex == "M");

            txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
            txtAgeDay.Text = patient.IsAlive == true ? Helper.GetAgeInDay(patient.DateOfBirth.Value).ToString() : Helper.GetAgeInDay(patient.DateOfBirth.Value, patient.DeceasedDateTime ?? DateTime.Now.Date).ToString();
            txtAgeMonth.Text = patient.IsAlive == true ? Helper.GetAgeInMonth(patient.DateOfBirth.Value).ToString() : Helper.GetAgeInMonth(patient.DateOfBirth.Value, patient.DeceasedDateTime ?? DateTime.Now.Date).ToString();
            txtAgeYear.Text = patient.IsAlive == true ? Helper.GetAgeInYear(patient.DateOfBirth.Value).ToString() : Helper.GetAgeInYear(patient.DateOfBirth.Value, patient.DeceasedDateTime ?? DateTime.Now.Date).ToString();

            txtParamedicID.Text = registration.ParamedicID;

            txtSsn.Text = patient.Ssn;

            var paramedic = new Paramedic();
            paramedic.LoadByPrimaryKey(txtParamedicID.Text);
            lblParamedicName.Text = paramedic.ParamedicName;

            txtGuarantorID.Text = registration.GuarantorID;

            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(txtGuarantorID.Text);
            lblGuarantorName.Text = guarantor.GuarantorName;

            var responsible = new RegistrationResponsiblePerson();
            responsible.LoadByPrimaryKey(registration.RegistrationNo);

            txtResponsible.Text = responsible.NameOfTheResponsible;
            txtPhoneNo.Text = patient.PhoneNo ?? patient.MobilePhoneNo;

            //txtComplaint.Text = registration.Complaint; // Diganti menggunakan record Question (Handono 221204)

        }

        protected void cboExaminerID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");

            query.es.Top = 10;
            query.Select
                (
                    query.ParamedicID,
                    query.ParamedicName
                );
            query.Where(
                query.Or(
                    query.ParamedicID.Like(searchTextContain),
                    query.ParamedicName.Like(searchTextContain)
                    ),
                query.IsActive == true
                );

            cboExaminerID.DataSource = query.LoadDataTable();
            cboExaminerID.DataBind();
        }

        protected void cboExaminerID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void btnCopyLast_OnClick(object sender, EventArgs e)
        {
            // Hanya untuk kondisi record baru, tambahkan kondisinya kalau mau ditambah bisa utk saat edit 
            var phrQr = new PatientHealthRecordQuery("phr");
            var regQr = new RegistrationQuery("r");
            phrQr.InnerJoin(regQr).On(phrQr.RegistrationNo == regQr.RegistrationNo);
            phrQr.Select(phrQr);

            phrQr.Where(regQr.PatientID == PatientID, phrQr.QuestionFormID == QuestionFormID);
            phrQr.es.Top = 1;
            phrQr.OrderBy(phrQr.TransactionNo.Descending);

            var lineValues = new PatientHealthRecordLineCollection();
            var ent = new PatientHealthRecord();
            if (ent.Load(phrQr))
            {
                lineValues = phrCtl.LoadHistoryValue(ent.TransactionNo, ent.RegistrationNo, ent.QuestionFormID);
            }

            var phr = new PatientHealthRecord { QuestionFormID = QuestionFormID };
            phrCtl.PopulateValue(PatientCurrent, RegistrationCurrent, phr, LastRegistrationNo, lineValues);
        }
    }

    public class DentalHelper
    {
        private string ResultValue(int number, string value)
        {
            return string.Format("<span style='color: {0}'>{1}</span>{2}",
                string.IsNullOrEmpty(value) ? "#ffffff" : "#000000",
                string.IsNullOrEmpty(value) ? number.ToString() : value,
                value != "M" ? "&nbsp;" : string.Empty);
        }

        public string MarkupResult
        {
            get
            {
                var markup = string.Format(@"{0}&nbsp;&nbsp;&nbsp;
{1}&nbsp;&nbsp;&nbsp;
{2}&nbsp;&nbsp;&nbsp;
{3}&nbsp;&nbsp;&nbsp;
{4}&nbsp;&nbsp;&nbsp;
{5}&nbsp;&nbsp;&nbsp;
{6}&nbsp;&nbsp;&nbsp;
{7}&nbsp;&nbsp;&nbsp;
<span style='color: #ffffff'>¦</span>&nbsp;&nbsp;&nbsp;&nbsp;
{8}&nbsp;&nbsp;&nbsp;
{9}&nbsp;&nbsp;&nbsp;
{10}&nbsp;&nbsp;&nbsp;
{11}&nbsp;&nbsp;&nbsp;
{12}&nbsp;&nbsp;&nbsp;
{13}&nbsp;&nbsp;&nbsp;
{14}&nbsp;&nbsp;&nbsp;
{15}<br />
<span style='color: #000000'>8</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>7</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>6</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>5</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>4</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>3</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>2</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>1</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>¦</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>1</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>2</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>3</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>4</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>5</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>6</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>7</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>8</span><br />
--------------------------------------------------------------------------------------------------<br />
<span style='color: #000000'>8</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>7</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>6</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>5</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>4</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>3</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>2</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>1</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>¦</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>1</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>2</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>3</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>4</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>5</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>6</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>7</span>&nbsp;&nbsp;&nbsp;&nbsp;
<span style='color: #000000'>8</span><br />
{16}&nbsp;&nbsp;&nbsp;
{17}&nbsp;&nbsp;&nbsp;
{18}&nbsp;&nbsp;&nbsp;
{19}&nbsp;&nbsp;&nbsp;
{20}&nbsp;&nbsp;&nbsp;
{21}&nbsp;&nbsp;&nbsp;
{22}&nbsp;&nbsp;&nbsp;
{23}&nbsp;&nbsp;&nbsp;
<span style='color: #ffffff'>¦</span>&nbsp;&nbsp;&nbsp;&nbsp;
{24}&nbsp;&nbsp;&nbsp;
{25}&nbsp;&nbsp;&nbsp;
{26}&nbsp;&nbsp;&nbsp;
{27}&nbsp;&nbsp;&nbsp;
{28}&nbsp;&nbsp;&nbsp;
{29}&nbsp;&nbsp;&nbsp;
{30}&nbsp;&nbsp;&nbsp;
{31}<br /><br />
G = Gangrene (Terinfeksi)&nbsp;R = Radix (Akar)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;F = Filling (Tumpatan)<br />
C = Caries (Berlubang)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;M = Missing (Hilang)&nbsp;P = Prothese (Gigi Palsu)",
                    ResultValue(8, LU8), ResultValue(7, LU7), ResultValue(6, LU6), ResultValue(5, LU5), ResultValue(4, LU4), ResultValue(3, LU3), ResultValue(2, LU2), ResultValue(1, LU1),
                    ResultValue(1, RU1), ResultValue(2, RU2), ResultValue(3, RU3), ResultValue(4, RU4), ResultValue(5, RU5), ResultValue(6, RU6), ResultValue(7, RU7), ResultValue(8, RU8),
                    ResultValue(8, LD8), ResultValue(7, LD7), ResultValue(6, LD6), ResultValue(5, LD5), ResultValue(4, LD4), ResultValue(3, LD3), ResultValue(2, LD2), ResultValue(1, LD1),
                    ResultValue(1, RD1), ResultValue(2, RD2), ResultValue(3, RD3), ResultValue(4, RD4), ResultValue(5, RD5), ResultValue(6, RD6), ResultValue(7, RD7), ResultValue(8, RD8));

                return markup;
            }
        }

        public string LU1
        {
            get;
            set;
        }

        public string LD1
        {
            get;
            set;
        }

        public string RU1
        {
            get;
            set;
        }

        public string RD1
        {
            get;
            set;
        }


        public string LU2
        {
            get;
            set;
        }

        public string LD2
        {
            get;
            set;
        }

        public string RU2
        {
            get;
            set;
        }

        public string RD2
        {
            get;
            set;
        }


        public string LU3
        {
            get;
            set;
        }

        public string LD3
        {
            get;
            set;
        }

        public string RU3
        {
            get;
            set;
        }

        public string RD3
        {
            get;
            set;
        }


        public string LU4
        {
            get;
            set;
        }

        public string LD4
        {
            get;
            set;
        }

        public string RU4
        {
            get;
            set;
        }

        public string RD4
        {
            get;
            set;
        }


        public string LU5
        {
            get;
            set;
        }

        public string LD5
        {
            get;
            set;
        }

        public string RU5
        {
            get;
            set;
        }

        public string RD5
        {
            get;
            set;
        }


        public string LU6
        {
            get;
            set;
        }

        public string LD6
        {
            get;
            set;
        }

        public string RU6
        {
            get;
            set;
        }

        public string RD6
        {
            get;
            set;
        }


        public string LU7
        {
            get;
            set;
        }

        public string LD7
        {
            get;
            set;
        }

        public string RU7
        {
            get;
            set;
        }

        public string RD7
        {
            get;
            set;
        }


        public string LU8
        {
            get;
            set;
        }

        public string LD8
        {
            get;
            set;
        }

        public string RU8
        {
            get;
            set;
        }

        public string RD8
        {
            get;
            set;
        }
    }
}

