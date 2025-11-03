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

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class PhrPraRegDetail : BasePageDialogEntry
    {
        private AppAutoNumberLast _autoNumber;
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
        private string QuestionFormID
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
        private string ReferenceNo
        {
            get
            {
                return Request.QueryString["refno"];
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


        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.HealthRecordPraReg;

            // QuestionFormID dikirm dari pemanggil
            // Hanya untuk Entry Single Record, Edit and Save
             IsSingleRecordMode = true;

            //// Hanya bisa mode add dari form pemanggil
            //// Tidak menggunakan SingleRecordMode krn utk bisa approve dan delete
            //ToolBar.AddVisible = false;
            //ToolBar.NavigationVisible = false;
            //ToolBar.VoidUnVoidVisible = false;
            //ToolBar.PrintVisible = false;
            //ToolBar.ApprovalUnApprovalVisible = (QuestionFormCurrent.IsUsingApproval ?? false);

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

                this.Title = string.Format("{0} [{1}]", QuestionFormCurrent.QuestionFormName, QuestionFormCurrent.RmNO);
            }
            phrCtl.InitializedQuestion(QuestionFormID);

        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event


        protected override void OnMenuNewClick()
        {
            var phr = new PatientHealthRecord { QuestionFormID = QuestionFormID };
            OnPopulateEntryControl(phr);

            txtTransactionNo.Text = GetNewTransactionNo();
            txtRecordDate.SelectedDate = _serverDate.Date;
            txtRecordTime.Text = _serverDate.ToString("HH:mm:");
            btnCopyLast.Enabled = true;

            PopulateRegistrationInformation();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            args.IsCancel = true;

            var entity = new PatientHealthRecord();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text, PatientID, QuestionFormID))
            {
                var collValue = new PatientHealthRecordLineCollection();
                collValue.Query.Where
                    (
                        collValue.Query.RegistrationNo == PatientID,
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

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            // Cek single entry
            if (QuestionFormCurrent.IsSingleEntry ?? false)
            {
                var phr = new PatientHealthRecordCollection();
                phr.Query.Where(phr.Query.RegistrationNo == RegistrationNo,
                    phr.Query.QuestionFormID == QuestionFormID);
                phr.LoadAll();
                if (phr.Count > 0)
                {
                    args.IsCancel = true;
                    args.MessageText =
                        string.Format("{0}[{1}] has exist, this form is single entry for current registration", QuestionFormCurrent.QuestionFormName, QuestionFormCurrent.RmNO);

                    return;
                }
            }
            var collValue = new PatientHealthRecordLineCollection();
            var entity = new PatientHealthRecord();

            SaveEntity(entity, collValue);

            if (!args.IsCancel)
                IsRecordHasChanged = true;
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
            entity.LoadByPrimaryKey(txtTransactionNo.Text, PatientID, QuestionFormID);

            var collValue = new PatientHealthRecordLineCollection();

            collValue.Query.Where
                (
                    collValue.Query.TransactionNo == txtTransactionNo.Text,
                    collValue.Query.RegistrationNo == PatientID,
                    collValue.Query.QuestionFormID == QuestionFormID
                );
            collValue.LoadAll();
            SaveEntity(entity, collValue);

            if (!args.IsCancel)
                IsRecordHasChanged = true;

        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new PatientHealthRecord();
            entity.LoadByPrimaryKey(txtTransactionNo.Text, PatientID, QuestionFormID);
            entity.IsApproved = true;
            entity.ApprovedByUserID = AppSession.UserLogin.UserID;
            entity.ApprovedDatetime = DateTime.Now;
            entity.Save();

            if (!args.IsCancel)
                IsRecordHasChanged = true;
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new PatientHealthRecord();
            entity.LoadByPrimaryKey(txtTransactionNo.Text, PatientID, QuestionFormID);

            //Reset
            entity.str.IsApproved = string.Empty;
            entity.str.ApprovedByUserID = string.Empty;
            entity.str.ApprovedDatetime = string.Empty;
            entity.Save();
            if (!args.IsCancel)
                IsRecordHasChanged = true;
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("RegistrationNo='{0}' AND QuestionFormID='{1}'", PatientID.Trim(), QuestionFormID);
            auditLogFilter.TableName = "PatientHealthRecord";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            var form = new QuestionForm();
            form.LoadByPrimaryKey(Request.QueryString["fid"]);

            AppSession.PrintJobReportID = form.ReportProgramID;
            programID = form.ReportProgramID;

            printJobParameters.AddNew("p_RegistrationNo", PatientID);
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
            var entity = new PatientHealthRecord();
            entity.LoadByPrimaryKey(txtTransactionNo.Text, PatientID, QuestionFormID);

            OnPopulateEntryControl(entity);

            PopulateRegistrationInformation();
        }

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var transactionNo = string.IsNullOrEmpty(txtTransactionNo.Text)
                ? TransactionNo
                : txtTransactionNo.Text;

            var entity = new PatientHealthRecord();

            entity.LoadByPrimaryKey(
                transactionNo,
                PatientID,
                QuestionFormID);

            OnPopulateEntryControl(entity);
        }

        protected void OnPopulateEntryControl(PatientHealthRecord phr)
        {
            hdnCreateByUserID.Value = phr.CreateByUserID;
            hdnQuestionFormID.Value = phr.QuestionFormID;

            txtTransactionNo.Text = phr.TransactionNo;
            txtRecordDate.SelectedDate = phr.RecordDate;
            txtRecordTime.Text = phr.RecordTime;


            if (!string.IsNullOrEmpty(phr.QuestionFormID))
            {
                var form = new QuestionForm();
                form.LoadByPrimaryKey(phr.QuestionFormID);
                txtFormName.Text = string.Format("{0} [{1}]", QuestionFormCurrent.QuestionFormName, QuestionFormCurrent.RmNO);
            }
            PopulateRegistrationInformation();

            if (phr.TransactionNo == null)
            {
                phr.TransactionNo = string.Empty;
                phr.RegistrationNo = PatientID;
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
            return (QuestionFormCurrent.IsSharingEdit ?? false) 
                   || (AppSession.UserLogin.UserID.Equals(hdnCreateByUserID.Value));
        }

        public override bool OnGetStatusMenuDelete()
        {
            return AppSession.UserLogin.UserID.Equals(hdnCreateByUserID.Value);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return (QuestionFormCurrent.IsUsingApproval ?? false) ;
        }

        #endregion

        #region Private Method Standard

        private void SaveEntity(PatientHealthRecord entity, PatientHealthRecordLineCollection collValue)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                _autoNumber.Save();
            }

            using (var trans = new esTransactionScope())
            {

                if (DataModeCurrent == AppEnum.DataMode.New)
                {
                    entity.TransactionNo = txtTransactionNo.Text;
                    entity.RegistrationNo = PatientID;
                    entity.QuestionFormID = QuestionFormID;
                    entity.str.ServiceUnitID = string.Empty;
                    entity.CreateByUserID = AppSession.UserLogin.UserID;
                    entity.CreateDateTime = _serverDate;
                }


                entity.RecordDate = txtRecordDate.SelectedDate;
                entity.RecordTime = txtRecordTime.TextWithLiterals;
                entity.EmployeeID = AppSession.UserLogin.UserID;
                entity.ReferenceNo = PatientID;
                entity.IsComplete = false;

                entity.ExaminerID = AppSession.UserLogin.UserID;

                var reg = new Registration {RegistrationNo = PatientID};
                // Isi RegistrationNo dg Patient ID utk Form Pra Reg
                phrCtl.SetEntityValue(PatientCurrent, reg, _othRelatedEntities, entity, collValue, LastRegistrationNo);
                entity.Save();
                collValue.Save();

                if (PatientCurrent.es.IsModified)
                    PatientCurrent.Save();


                //Commit if success, Rollback if failed
                trans.Complete();
            }
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
                    _patientCurrent.LoadByPrimaryKey(PatientID);
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

        private void PopulateRegistrationInformation()
        {

            var patient = new Patient();
            patient.LoadByPrimaryKey(PatientID);

            hdnPatientID.Value = patient.PatientID;
            
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = patient.PatientName;
            txtGender.Text = patient.Sex;

            optSexFemale.Checked = (patient.Sex == "F");
            optSexFemale.Enabled = (patient.Sex == "F");
            optSexMale.Checked = (patient.Sex == "M");
            optSexMale.Enabled = (patient.Sex == "M");

            txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));
            txtAgeDay.Value = Helper.GetAgeInDay(patient.DateOfBirth.Value);
            txtAgeMonth.Value = Helper.GetAgeInMonth(patient.DateOfBirth.Value);
            txtAgeYear.Value = Helper.GetAgeInYear(patient.DateOfBirth.Value);

        }


        protected void btnCopyLast_OnClick(object sender, EventArgs e)
        {
            // Hanya untuk kondisi record baru, tambahkan kondisinya kalau mau ditambah bisa utk saat edit 
            var phrQr = new PatientHealthRecordQuery("phr");
            var regQr = new RegistrationQuery("r");
            phrQr.InnerJoin(regQr).On(phrQr.RegistrationNo == regQr.RegistrationNo);
            phrQr.Select(phrQr);

            phrQr.Where(regQr.PatientID == PatientID, phrQr.QuestionFormID==QuestionFormID);
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

}

