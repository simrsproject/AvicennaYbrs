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

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationControlSheetDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtControlDate.SelectedDate.Value, AppEnum.AutoNumber.SanitationControlSheetNo);
            return _autoNumber.LastCompleteNumber;
        }

        private string QuestionFormID
        {
            get { return Request.QueryString["fid"]; }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SanitationControlSheet;
            UrlPageList = "SanitationControlSheetList.aspx";
            UrlPageSearch = "##";

            if (!IsPostBack)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }

            InitializedQuestion(QuestionFormID);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuAdd.Enabled = false;
            ToolBarMenuMoveNext.Enabled = false; 
            ToolBarMenuMovePrev.Enabled = false; 
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new SanitationControlSheet());

            txtControlDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtControlTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            txtControlSheetNo.Text = GetNewTransactionNo();

            var form = new QuestionForm();
            form.LoadByPrimaryKey(QuestionFormID);
            txtFormName.Text = form.QuestionFormName;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new SanitationControlSheet();
            if (entity.LoadByPrimaryKey(txtControlSheetNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;

                var collValue = new SanitationControlSheetItemCollection();
                collValue.Query.Where
                    (
                        collValue.Query.ControlSheetNo == txtControlSheetNo.Text
                    );

                entity.MarkAsDeleted();
                using (var trans = new esTransactionScope())
                {
                    collValue.LoadAll();
                    collValue.MarkAllAsDeleted();
                    collValue.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var sheets = new SanitationControlSheetCollection();
            sheets.Query.Where(sheets.Query.QuestionFormID == QuestionFormID, sheets.Query.ControlDate.Date() == txtControlDate.SelectedDate.Value.Date, sheets.Query.IsVoid == false);
            sheets.LoadAll();
            if (sheets.Count > 0)
            {
                args.MessageText = string.Format("{0} for {1} already exists.", txtFormName.Text, txtControlDate.SelectedDate.Value.ToString("dd-MMM-yyyy"));
                args.IsCancel = true;
                return;
            }

            var collValue = new SanitationControlSheetItemCollection();
            var entity = new SanitationControlSheet();
            entity.AddNew();

            SetEntityValue(entity, collValue);
            SaveEntity(entity, collValue);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var sheets = new SanitationControlSheetCollection();
            sheets.Query.Where(sheets.Query.ControlSheetNo != txtControlSheetNo.Text, sheets.Query.QuestionFormID == QuestionFormID, 
                sheets.Query.ControlDate.Date() == txtControlDate.SelectedDate.Value.Date, sheets.Query.IsVoid == false);
            sheets.LoadAll();
            if (sheets.Count > 0)
            {
                args.MessageText = string.Format("{0} for {1} already exists.", txtFormName.Text, txtControlDate.SelectedDate.Value.ToString("dd-MMM-yyyy"));
                args.IsCancel = true;
                return;
            }

            var entity = new SanitationControlSheet();
            entity.LoadByPrimaryKey(txtControlSheetNo.Text);

            var collValue = new SanitationControlSheetItemCollection();

            collValue.Query.Where
                (
                    collValue.Query.ControlSheetNo == txtControlSheetNo.Text
                );
            collValue.LoadAll();

            SetEntityValue(entity, collValue);
            SaveEntity(entity, collValue);
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("ControlSheetNo='{0}'", txtControlSheetNo.Text.Trim());
            auditLogFilter.TableName = "SanitationControlSheet";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            var form = new QuestionForm();
            form.LoadByPrimaryKey(QuestionFormID);

            AppSession.PrintJobReportID = form.ReportProgramID;
            programID = form.ReportProgramID;

            printJobParameters.AddNew("p_ControlSheetNo", txtControlSheetNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new SanitationControlSheet();
            entity.LoadByPrimaryKey(txtControlSheetNo.Text);

            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = true;
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new SanitationControlSheet();
            entity.LoadByPrimaryKey(txtControlSheetNo.Text);

            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = false;
                entity.ApprovedDateTime = null;
                entity.ApprovedByUserID = null;

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new SanitationControlSheet();
            if (!entity.LoadByPrimaryKey(txtControlSheetNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (!IsApproved(entity, args))
                return;

            SetVoid(entity, true);
        }

        private void SetVoid(SanitationControlSheet entity, bool isVoid)
        {
            using (var trans = new esTransactionScope())
            {
                //header
                entity.IsVoid = isVoid;
                if (isVoid)
                {
                    entity.VoidByUserID = AppSession.UserLogin.UserID;
                    entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
                }
                else
                {
                    entity.VoidByUserID = null;
                    entity.VoidDateTime = null;
                }
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                trans.Complete();
            }
        }

        #endregion

        #region ToolBar Menu Support

        private bool IsApprovedOrVoid(SanitationControlSheet entity, ValidateArgs args)
        {
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        private bool IsApproved(SanitationControlSheet entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            var dtbQuestion = QuestionDataTable(QuestionFormID);

            foreach (DataRow rowQuestion in dtbQuestion.Rows)
            {
                SetReadOnlySanitationControlSheetQuestion((newVal == AppEnum.DataMode.Read), rowQuestion, rowQuestion["QuestionGroupID"].ToString());
            }
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new SanitationControlSheet();
            if (entity.LoadByPrimaryKey(txtControlSheetNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuEditClick()
        {
            var entity = new SanitationControlSheet();
            entity.LoadByPrimaryKey(txtControlSheetNo.Text);

            OnPopulateEntryControl(entity);

            var form = new QuestionForm();
            form.LoadByPrimaryKey(QuestionFormID);
            txtFormName.Text = form.QuestionFormName;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new SanitationControlSheet();

            entity.LoadByPrimaryKey(string.IsNullOrEmpty(txtControlSheetNo.Text) ? Request.QueryString["id"] : txtControlSheetNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var rpt = (SanitationControlSheet)entity;

            txtControlSheetNo.Text = rpt.ControlSheetNo;
            if (rpt.ControlDate.HasValue)
            {
                txtControlDate.SelectedDate = rpt.ControlDate;
                txtControlTime.Text = rpt.ControlDate.Value.ToString("HH:mm");
            }

            if (!string.IsNullOrEmpty(rpt.QuestionFormID))
            {
                var form = new QuestionForm();
                form.LoadByPrimaryKey(rpt.QuestionFormID);
                txtFormName.Text = form.QuestionFormName;
            }

            chkIsApproved.Checked = rpt.IsApproved ?? false;
            chkIsVoid.Checked = rpt.IsVoid ?? false;

            PopulateQuestionValue();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(SanitationControlSheet entity, SanitationControlSheetItemCollection collValue)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtControlSheetNo.Text = GetNewTransactionNo();
                _autoNumber.Save();
            }

            entity.ControlSheetNo = txtControlSheetNo.Text;
            entity.QuestionFormID = QuestionFormID;
            entity.ControlDate = DateTime.Parse(txtControlDate.SelectedDate.Value.ToShortDateString() + " " + txtControlTime.TextWithLiterals);

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            entity.IsVoid = chkIsVoid.Checked;
            entity.IsApproved = chkIsApproved.Checked;

            //PatientHealthRecordLine
            var dtbQuestion = QuestionDataTable(QuestionFormID);

            foreach (DataRow rowQuestion in dtbQuestion.Rows)
            {
                // Tips: Don't use entity.es.IsModified, krn belum tentu record sudah diedit waktu save
                SetSanitationControlSheetItem(entity.es.IsAdded, entity.ControlSheetNo, collValue, rowQuestion, rowQuestion["QuestionGroupID"].ToString());

                var quest = new QuestionQuery();
                quest.Where(quest.ParentQuestionID == rowQuestion["QuestionID"] && quest.SRAnswerType != string.Empty);
                var dtbSubQuestion = quest.LoadDataTable();

                foreach (DataRow rowSubQuestion in dtbSubQuestion.Rows)
                {
                    SetSanitationControlSheetItem(entity.es.IsAdded, entity.ControlSheetNo, collValue, rowSubQuestion, rowQuestion["QuestionGroupID"].ToString());
                }
            }
        }

        private void SetSanitationControlSheetItem(bool isNewRecord, string controlSheetNo, SanitationControlSheetItemCollection collValue, DataRow rowQuestion, string questionGroupID)
        {

            SanitationControlSheetItem hrLine;
            string questionID = rowQuestion[SanitationControlSheetItemMetadata.ColumnNames.QuestionID].ToString();
            //if (isNewRecord)
            //    hrLine = collValue.AddNew();
            //else
            hrLine = collValue.FindByPrimaryKey(txtControlSheetNo.Text, QuestionFormID, questionGroupID, questionID) ?? collValue.AddNew();

            hrLine.ControlSheetNo = controlSheetNo;
            hrLine.QuestionFormID = QuestionFormID;
            hrLine.QuestionGroupID = questionGroupID;
            hrLine.QuestionID = questionID;
            hrLine.QuestionAnswerPrefix = rowQuestion["AnswerPrefix"].ToStringDefaultEmpty();
            hrLine.QuestionAnswerSuffix = rowQuestion["AnswerSuffix"].ToStringDefaultEmpty();

            string controlID = QuestionControlID(rowQuestion["QuestionID"].ToString());
            string answerType = rowQuestion[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
            object obj = null;

            if (answerType != "DNT") //Dental Control
            {
                if (string.IsNullOrEmpty(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()))
                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID);
                else
                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, QuestionControlID(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()));

                if (obj == null)
                {
                    hrLine.MarkAsDeleted();
                    return;
                }
            }

            switch (answerType)
            {
                case "MSK":
                    var mskAnswerValue = (obj as RadMaskedTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(mskAnswerValue.TextWithLiterals);
                    break;
                case "DAT":
                    var dat = (obj as RadDatePicker);
                    hrLine.QuestionAnswerText = (dat.SelectedDate ?? DateTime.Now).ToShortDateString();
                    break;
                case "TIM":
                    var tim = (obj as RadTimePicker);
                    hrLine.QuestionAnswerText = (tim.SelectedDate ?? DateTime.Now).ToString("HH:mm");
                    break;
                case "DTM":
                    var dattim = (obj as RadDateTimePicker);
                    hrLine.QuestionAnswerText = (dattim.SelectedDate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm");
                    break;
                case "NUM":
                    var numAnswerValue = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerNum = Convert.ToDecimal(numAnswerValue.Value);
                    break;
                case "MEM":
                    var memAnswerValue = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(memAnswerValue.Text);
                    break;
                case "TXT":
                    var txtAnswerValue = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(txtAnswerValue.Text);
                    break;
                case "CBO":
                    var cboAnswerValue = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cboAnswerValue.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cboAnswerValue.Text);
                    break;
                case "CHK":
                    var chk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = chk != null && chk.Checked ? "1" : "0";
                    break;
                case "CTX":
                    var ctxChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctxChk != null && ctxChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    var ctxTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(ctxTxt.Text));
                    break;
                case "CDO":
                    var cdoChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cdoChk != null && cdoChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    var cdoCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cdoCbo.Text));
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cdoCbo.SelectedValue);
                    break;
                case "CTM":
                    var ctmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctmChk != null && ctmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    var ctmTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(ctmTxt.Text));
                    break;
                case "CNM":
                    var cnmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cnmChk != null && cnmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    var cnmNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cnmNum.Text));
                    break;
                case "CDT":
                    var cdtChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cdtChk != null && cdtChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    var cdtDattim = (obj as RadDateTimePicker);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate((cdtDattim.SelectedDate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm")));
                    break;
                case "CBT":
                    var cbtCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbo.Text);

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    var cbtTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbtTxt.Text));
                    break;
                case "CBN":
                    var cbnCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbnCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbnCbo.Text);

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    var cbnNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbnNum.Text));
                    break;
                case "CBM":
                    var cbtCbm = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbm.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbm.Text);

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    var cbtTxm = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbtTxm.Text));
                    break;
                case "CB2":
                    var cbo1 = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbo1.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbo1.Text);

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    var cbo2 = (obj as RadComboBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbo2.Text));

                    hrLine.QuestionAnswerSelectionLineID = string.Format("{0}|{1}", HtmlTagHelper.Validate(cbo1.SelectedValue), HtmlTagHelper.Validate(cbo2.SelectedValue));
                    break;
                case "TTX":
                    var txt1 = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(txt1.Text);

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    var txt2 = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(txt2.Text));
                    break;
                case "TBL":
                    var tbl = (obj as HtmlTable);
                    // get row length and col length
                    var rCount = tbl.Rows.Count;
                    string ansText = string.Empty;
                    if (rCount > 0)
                    {
                        var cCount = tbl.Rows[0].Cells.Count;
                        for (var r = 1; r < rCount; r++)
                        {
                            for (var c = 0; c < cCount; c++)
                            {
                                var objCell = Helper.FindControlRecursive(
                                    pnlSanitationControlSheetItem,
                                    controlID + "_" + r.ToString() + "_" + c.ToString());
                                var objCellText = (objCell as RadTextBox);
                                ansText += (ansText.Equals(string.Empty) ? "" : "|") + HtmlTagHelper.Validate(objCellText.Text);
                            }
                        }
                    }
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(ansText);
                    break;
            }
            if (hrLine.es.IsAdded || hrLine.es.IsModified)
            {
                //hrLine.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //hrLine.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(SanitationControlSheet entity, SanitationControlSheetItemCollection collValue)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                collValue.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new SanitationControlSheetQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ControlSheetNo > txtControlSheetNo.Text);
                que.OrderBy(que.ControlSheetNo.Ascending);
            }
            else
            {
                que.Where(que.ControlSheetNo < this.txtControlSheetNo.Text);
                que.OrderBy(que.ControlSheetNo.Descending);
            }

            var entity = new SanitationControlSheet();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        private void SetReadOnlySanitationControlSheetQuestion(bool isReadOnly, DataRow rowQuestion, string questionGroupID)
        {
            string questionID = rowQuestion[SanitationControlSheetItemMetadata.ColumnNames.QuestionID].ToString();
            string controlID = QuestionControlID(rowQuestion["QuestionID"].ToString());
            string answerType = rowQuestion[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
            object obj;

            if (string.IsNullOrEmpty(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()))
                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID);
            else
                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, QuestionControlID(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()));

            switch (answerType)
            {
                case "MSK":
                    (obj as RadMaskedTextBox).ReadOnly = isReadOnly;
                    break;
                case "DAT":
                    (obj as RadDatePicker).DatePopupButton.Enabled = !isReadOnly;
                    (obj as RadDatePicker).DateInput.ReadOnly = isReadOnly;
                    break;
                case "TIM":
                    (obj as RadTimePicker).DatePopupButton.Enabled = !isReadOnly;
                    (obj as RadTimePicker).DateInput.ReadOnly = isReadOnly;
                    break;
                case "DTM":
                    (obj as RadDateTimePicker).DatePopupButton.Enabled = !isReadOnly;
                    (obj as RadDateTimePicker).TimePopupButton.Enabled = !isReadOnly;
                    (obj as RadDateTimePicker).DateInput.ReadOnly = isReadOnly;
                    break;
                case "NUM":
                    (obj as RadNumericTextBox).ReadOnly = isReadOnly;
                    break;
                case "MEM":
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "TXT":
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CBO":
                    (obj as RadComboBox).Enabled = !isReadOnly;
                    break;
                case "CHK":
                    (obj as CheckBox).Enabled = !isReadOnly;
                    break;
                case "CTX":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CDO":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    (obj as RadComboBox).Enabled = !isReadOnly;
                    break;
                case "CTM":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CNM":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    (obj as RadNumericTextBox).ReadOnly = isReadOnly;
                    break;
                case "CDT":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    (obj as RadDateTimePicker).DatePopupButton.Enabled = !isReadOnly;
                    (obj as RadDateTimePicker).TimePopupButton.Enabled = !isReadOnly;
                    (obj as RadDateTimePicker).DateInput.ReadOnly = isReadOnly;
                    break;
                case "CBT":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CBN":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    (obj as RadNumericTextBox).ReadOnly = isReadOnly;
                    break;
                case "CBM":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CB2":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    (obj as RadComboBox).Enabled = !isReadOnly;
                    break;
                case "TTX":
                    (obj as RadTextBox).ReadOnly = isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "TBL":
                    var tbl = (obj as HtmlTable);
                    // get row length and col length
                    var rCount = tbl.Rows.Count;
                    string ansText = string.Empty;
                    if (rCount > 0)
                    {
                        var cCount = tbl.Rows[0].Cells.Count;
                        for (var r = 1; r < rCount; r++)
                        {
                            for (var c = 0; c < cCount; c++)
                            {
                                var objCell = Helper.FindControlRecursive(
                                    pnlSanitationControlSheetItem,
                                    controlID + "_" + r.ToString() + "_" + c.ToString());
                                (objCell as RadTextBox).ReadOnly = isReadOnly;
                            }
                        }
                    }
                    break;
            }
        }

        #endregion

        #region SanitationControlSheetItem

        private string QuestionControlID(string questionID)
        {
            return "quest" + questionID.Replace('.', '_');
        }
        private string RfvControlID(string questionID)
        {
            return "rfv" + questionID.Replace('.', '_');
        }

        private void PopulateQuestionValue()
        {
            var query = new SanitationControlSheetItemQuery("a");
            var qQuest = new QuestionQuery("b");

            query.InnerJoin(qQuest).On(query.QuestionID == qQuest.QuestionID);

            query.Select
                (
                    query.QuestionID,
                    query.QuestionAnswerSelectionLineID,
                    query.QuestionAnswerNum,
                    "<CASE WHEN b.SRAnswerType = 'DNT' THEN a.QuestionAnswerText2 ELSE a.QuestionAnswerText END AS QuestionAnswerText>",
                    //query.QuestionAnswerText,
                    qQuest.SRAnswerType
                );
            query.Where
                (
                    query.ControlSheetNo == txtControlSheetNo.Text,
                    query.QuestionFormID == QuestionFormID,
                    qQuest.SRAnswerType != "LBL"
                );
            query.OrderBy(qQuest.IndexNo.Ascending);

            DataTable dtbValue = query.LoadDataTable();

            foreach (DataRow dataRow in dtbValue.Rows)
            {
                string controlID = QuestionControlID(dataRow["QuestionID"].ToString());
                string answerType = dataRow[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
                object obj;


                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID);
                if (obj == null)
                    if (answerType != "DNT") continue;

                switch (answerType)
                {
                    case "MSK":
                        var mskAnswerValue = (obj as RadMaskedTextBox);
                        if (mskAnswerValue != null)
                            mskAnswerValue.Text = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerText"].ToStringDefaultEmpty());
                        break;
                    case "DAT":
                        var dtAnswerValue = (obj as RadDatePicker);
                        if (dtAnswerValue != null)
                            dtAnswerValue.SelectedDate = Convert.ToDateTime(dataRow["QuestionAnswerText"]);
                        break;
                    case "TIM":
                        var tpAnswerValue = (obj as RadTimePicker);
                        if (tpAnswerValue != null)
                            tpAnswerValue.SelectedDate = Convert.ToDateTime(dataRow["QuestionAnswerText"]);
                        break;
                    case "DTM":
                        var dtmAnswerValue = (obj as RadDateTimePicker);
                        if (dtmAnswerValue != null)
                            dtmAnswerValue.SelectedDate = Convert.ToDateTime(dataRow["QuestionAnswerText"]);
                        break;
                    case "NUM":
                        var numAnswerValue = (obj as RadNumericTextBox);
                        if (numAnswerValue != null)
                            if (!dataRow["QuestionAnswerNum"].Equals("&nbsp;") &&
                                dataRow["QuestionAnswerNum"] != DBNull.Value)
                                numAnswerValue.Value = Convert.ToDouble(dataRow["QuestionAnswerNum"]);
                        break;
                    case "TXT":
                        var txtAnswerValue = (obj as RadTextBox);
                        if (txtAnswerValue != null)
                            txtAnswerValue.Text = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerText"].ToStringDefaultEmpty());
                        break;
                    case "MEM":
                        var memAnswerValue = (obj as RadTextBox);
                        if (memAnswerValue != null)
                            memAnswerValue.Text = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerText"].ToStringDefaultEmpty());
                        break;
                    case "CBO":
                        var cboAnswerValue = (obj as RadComboBox);
                        if (cboAnswerValue != null)
                            cboAnswerValue.SelectedValue = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty());
                        break;
                    case "CHK":
                        var chk = (obj as CheckBox);
                        if (chk != null)
                            chk.Checked = "1".Equals(dataRow["QuestionAnswerText"]);
                        break;
                    case "CTX":
                        var ctxValue = dataRow["QuestionAnswerText"];
                        if (ctxValue != DBNull.Value)
                        {
                            var ctxValues = ctxValue.ToString().Split('|');
                            if (ctxValues.Length > 0 && ctxValues[0] != null)
                            {
                                var ctxChk = (obj as CheckBox);
                                if (ctxChk != null)
                                    ctxChk.Checked = "1".Equals(ctxValues[0]);
                            }
                            if (ctxValues.Length > 1 && ctxValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                                var ctxTxt = (obj as RadTextBox);
                                if (ctxTxt != null)
                                    ctxTxt.Text = HtmlTagHelper.Devalidate(ctxValues[1]);
                            }
                        }
                        break;
                    case "CDO":
                        var cdoValue = dataRow["QuestionAnswerText"];
                        if (cdoValue != DBNull.Value)
                        {
                            var cdoValues = cdoValue.ToString().Split('|');
                            if (cdoValues.Length > 0 && cdoValues[0] != null)
                            {
                                var cdoChk = (obj as CheckBox);
                                if (cdoChk != null)
                                    cdoChk.Checked = "1".Equals(cdoValues[0]);
                            }

                            var cdo2Value = dataRow["QuestionAnswerSelectionLineID"];
                            if (cdo2Value != DBNull.Value)
                            {
                                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                                var cbo2 = (obj as RadComboBox);
                                if (cbo2 != null)
                                    cbo2.SelectedValue = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty());
                            }
                        }

                        break;
                    case "CTM":
                        var ctmValue = dataRow["QuestionAnswerText"];
                        if (ctmValue != DBNull.Value)
                        {
                            var ctmValues = ctmValue.ToString().Split('|');
                            if (ctmValues.Length > 0 && ctmValues[0] != null)
                            {
                                var ctxChk = (obj as CheckBox);
                                if (ctxChk != null)
                                    ctxChk.Checked = "1".Equals(ctmValues[0]);
                            }
                            if (ctmValues.Length > 1 && ctmValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                                var ctxTxt = (obj as RadTextBox);
                                if (ctxTxt != null)
                                    ctxTxt.Text = HtmlTagHelper.Devalidate(ctmValues[1]);
                            }
                        }
                        break;
                    case "CNM":
                        var cnmValue = dataRow["QuestionAnswerText"];
                        if (cnmValue != DBNull.Value)
                        {
                            var cnmValues = cnmValue.ToString().Split('|');
                            if (cnmValues.Length > 0 && cnmValues[0] != null)
                            {
                                var ctxChk = (obj as CheckBox);
                                if (ctxChk != null)
                                    ctxChk.Checked = "1".Equals(cnmValues[0]);
                            }
                            if (cnmValues.Length > 1 && cnmValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                                var cnmNum = (obj as RadNumericTextBox);
                                if (cnmNum != null)
                                    if (!string.IsNullOrEmpty(cnmValues[1]) && !cnmValues[1].Equals("&nbsp;"))
                                        cnmNum.Value = Convert.ToDouble(cnmValues[1]);
                            }
                        }
                        break;
                    case "CDT":
                        var cdtValue = dataRow["QuestionAnswerText"];
                        if (cdtValue != DBNull.Value)
                        {
                            var cdtValues = cdtValue.ToString().Split('|');
                            if (cdtValues.Length > 0 && cdtValues[0] != null)
                            {
                                var ctxChk = (obj as CheckBox);
                                if (ctxChk != null)
                                    ctxChk.Checked = "1".Equals(cdtValues[0]);
                            }
                            if (cdtValues.Length > 1 && cdtValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                                var cdtDattim = (obj as RadDateTimePicker);
                                if (cdtDattim != null)
                                    if (!string.IsNullOrEmpty(cdtValues[1]) && !cdtValues[1].Equals("&nbsp;"))
                                        cdtDattim.SelectedDate = Convert.ToDateTime(cdtValues[1]);
                            }
                        }
                        break;
                    case "CBT":
                        var cbtValue = dataRow["QuestionAnswerText"];
                        if (cbtValue != DBNull.Value)
                        {
                            var cbtValues = cbtValue.ToString().Split('|');
                            if (cbtValues.Length > 0 && cbtValues[0] != null)
                            {
                                var cbtCbo = (obj as RadComboBox);
                                if (cbtCbo != null)
                                    cbtCbo.SelectedValue = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty());
                            }
                            if (cbtValues.Length > 1 && cbtValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                                var cbtTxt = (obj as RadTextBox);
                                if (cbtTxt != null)
                                    cbtTxt.Text = HtmlTagHelper.Devalidate(cbtValues[1]);
                            }
                        }
                        break;
                    case "CBN":
                        var cbnValue = dataRow["QuestionAnswerText"];
                        if (cbnValue != DBNull.Value)
                        {
                            var cbnValues = cbnValue.ToString().Split('|');
                            if (cbnValues.Length != null && cbnValues[0] != null)
                            {
                                var cbnCbo = (obj as RadComboBox);
                                if (cbnCbo != null)
                                    cbnCbo.SelectedValue = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty());
                            }
                            if (cbnValues.Length > 1 && cbnValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                                var cbnNum = (obj as RadNumericTextBox);
                                if (cbnNum != null)
                                    if (!string.IsNullOrEmpty(cbnValues[1]) && !cbnValues[1].Equals("&nbsp;"))
                                        cbnNum.Value = Convert.ToDouble(cbnValues[1]);
                            }
                        }
                        break;
                    case "CBM":
                        var cbmValue = dataRow["QuestionAnswerText"];
                        if (cbmValue != DBNull.Value)
                        {
                            var cbmValues = cbmValue.ToString().Split('|');
                            if (cbmValues.Length > 0 && cbmValues[0] != null)
                            {
                                var cbtCbo = (obj as RadComboBox);
                                if (cbtCbo != null)
                                    cbtCbo.SelectedValue = HtmlTagHelper.Devalidate(dataRow["QuestionAnswerSelectionLineID"].ToStringDefaultEmpty());
                            }
                            if (cbmValues.Length > 1 && cbmValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                                var cbtTxt = (obj as RadTextBox);
                                if (cbtTxt != null)
                                    cbtTxt.Text = HtmlTagHelper.Devalidate(cbmValues[1]);
                            }
                        }
                        break;
                    case "CB2":
                        var cb2Value = dataRow["QuestionAnswerSelectionLineID"];
                        if (cb2Value != DBNull.Value)
                        {
                            var cb2Values = cb2Value.ToStringDefaultEmpty().Split('|');
                            if (cb2Values.Length > 0 && cb2Values[0] != null)
                            {
                                var cbo1 = (obj as RadComboBox);
                                if (cbo1 != null)
                                    cbo1.SelectedValue = HtmlTagHelper.Devalidate(cb2Values[0]); ;
                            }
                            if (cb2Values.Length > 1 && cb2Values[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                                var cbo2 = (obj as RadComboBox);
                                if (cbo2 != null)
                                    cbo2.SelectedValue = HtmlTagHelper.Devalidate(cb2Values[1]);
                            }
                        }
                        break;
                    case "TTX":
                        var ttxValue = dataRow["QuestionAnswerText"];
                        if (ttxValue != DBNull.Value)
                        {
                            var ttxValues = ttxValue.ToString().Split('|');
                            if (ttxValues.Length > 0 && ttxValues[0] != null)
                            {
                                var txt = (obj as RadTextBox);
                                if (txt != null)
                                    txt.Text = HtmlTagHelper.Devalidate(ttxValues[0]);
                            }
                            if (ttxValues.Length > 1 && ttxValues[1] != null)
                            {
                                obj = Helper.FindControlRecursive(pnlSanitationControlSheetItem, controlID + "_2");
                                var txt2 = (obj as RadTextBox);
                                if (txt2 != null)
                                    txt2.Text = HtmlTagHelper.Devalidate(ttxValues[1]);
                            }
                        }
                        break;
                    case "TBL":
                        var tblValue = dataRow["QuestionAnswerText"];
                        if (tblValue != DBNull.Value)
                        {
                            var tblValues = tblValue.ToString().Split('|');

                            // find table
                            var tbl = (HtmlTable)Helper.FindControlRecursive(
                                            pnlSanitationControlSheetItem,
                                            controlID);
                            // get row length and col length
                            var rCount = tbl.Rows.Count;
                            string ansText = string.Empty;
                            if (rCount > 0)
                            {
                                var cCount = tbl.Rows[0].Cells.Count;
                                for (var r = 1; r < rCount; r++)
                                {
                                    for (var c = 0; c < cCount; c++)
                                    {
                                        var objCell = Helper.FindControlRecursive(
                                            pnlSanitationControlSheetItem,
                                            controlID + "_" + r.ToString() + "_" + c.ToString());
                                        var objCellText = (objCell as RadTextBox);
                                        var cellValIndex = c % cCount;
                                        if (cellValIndex + (cCount * (r - 1)) < tblValues.Length)
                                        {
                                            objCellText.Text = HtmlTagHelper.Devalidate(tblValues[cellValIndex + (cCount * (r - 1))]);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }

            }
        }

        private IEnumerable<QuestionGroup> LoadQuestionGroup(string formID)
        {
            if (ViewState["questionGroup"] != null)
                return ViewState["questionGroup"] as QuestionGroupCollection;
            else
            {
                var query = new QuestionGroupQuery("a");
                var qrQGroupInForm = new QuestionGroupInFormQuery("d");
                query.InnerJoin(qrQGroupInForm).On(query.QuestionGroupID == qrQGroupInForm.QuestionGroupID);
                query.Where(qrQGroupInForm.QuestionFormID == formID);
                query.SelectAll();
                query.OrderBy(qrQGroupInForm.RowIndex.Ascending);

                var coll = new QuestionGroupCollection();
                coll.Load(query);

                ViewState["questionGroup"] = coll;

                return coll;
            }
        }

        private DataTable QuestionDataTable(string formID)
        {
            if (ViewState["questionDataTable"] != null)
                return ViewState["questionDataTable"] as DataTable;
            else
            {
                var questionQuery = new QuestionQuery("a");
                var qrQInGroup = new QuestionInGroupQuery("c");
                var qrQGInForm = new QuestionGroupInFormQuery("d");
                questionQuery.InnerJoin(qrQInGroup).On(questionQuery.QuestionID == qrQInGroup.QuestionID);
                questionQuery.InnerJoin(qrQGInForm).On(qrQInGroup.QuestionGroupID == qrQGInForm.QuestionGroupID);
                questionQuery.OrderBy(qrQGInForm.RowIndex.Ascending, qrQInGroup.QuestionGroupID.Ascending, qrQInGroup.RowIndex.Ascending);
                questionQuery.Where(qrQGInForm.QuestionFormID == formID, questionQuery.IsActive == true);
                questionQuery.Select
                    (
                        //questionQuery,
                        questionQuery.QuestionID, questionQuery.ParentQuestionID, questionQuery.IndexNo,
                        "<ISNULL(c.QuestionLevel,a.QuestionLevel) QuestionLevel>",
                        questionQuery.QuestionText, questionQuery.QuestionShortText,
                        questionQuery.SRAnswerType, questionQuery.AnswerDecimalDigit, questionQuery.AnswerPrefix,
                        questionQuery.AnswerSuffix, questionQuery.IsActive, questionQuery.AnswerWidth,
                        questionQuery.AnswerWidth2, questionQuery.QuestionAnswerSelectionID,
                        questionQuery.QuestionAnswerDefaultSelectionID, questionQuery.QuestionAnswerSelectionID2,
                        questionQuery.QuestionAnswerDefaultSelectionID2, questionQuery.Formula, questionQuery.IsAlwaysPrint,
                        questionQuery.LastUpdateDateTime, questionQuery.LastUpdateByUserID, questionQuery.IsMandatory,
                        "<ISNULL(a.IsEmptyDefault,0) IsEmptyDefault>",
                        questionQuery.ReferenceQuestionID,
                        qrQInGroup.QuestionGroupID,
                        qrQInGroup.RowIndex
                    );

                var dtb = questionQuery.LoadDataTable();
                ViewState["questionDataTable"] = dtb;
                return dtb;
            }
        }

        private void InitializedQuestion(string formID)
        {
            //  Get List Question Group
            IEnumerable<QuestionGroup> questionGroups = LoadQuestionGroup(formID);

            //  Get List Question
            var dtbQuestion = QuestionDataTable(formID);
            //  Generate Question Entry
            pnlSanitationControlSheetItem.Controls.Clear();
            int rowNo = 0;
            var formulas = new Hashtable(); // Untuk menampung jawaban jenis formula
            foreach (QuestionGroup questionGroup in questionGroups)
            {
                rowNo++;
                var groupTable = new Table { Width = Unit.Percentage(100) };
                // Add Group Label
                var row = new TableRow();
                groupTable.Rows.Add(row);
                var cell = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Left,
                    Text = string.Format("{0}. {1}", rowNo, questionGroup.QuestionGroupName)
                };
                cell.Font.Bold = true;
                cell.Style["color"] = "white";
                cell.Style["background-color"] = "#758DA6";
                cell.ColumnSpan = 3;
                row.Cells.Add(cell);

                groupTable.Rows.Add(row);
                DataRow[] questionRows = dtbQuestion.Select(string.Format("QuestionGroupID='{0}'", questionGroup.QuestionGroupID), "RowIndex");

                InitializedQuestion(questionRows, groupTable, row, formulas);
                pnlSanitationControlSheetItem.Controls.Add(groupTable);
            }

            //Generate Formula Script
            if (!IsPostBack)
            {
                var script = new StringBuilder();
                script.AppendLine("<script type='text/javascript' language='javascript'>");
                script.AppendLine("function fillFormulaField(){");

                foreach (DictionaryEntry dictionaryEntry in formulas)
                {

                    string id = dictionaryEntry.Key.ToString();

                    // [200.020]/(([200.030]/100)*([200.030]/100))
                    string formula = dictionaryEntry.Value.ToString();
                    formula = formula.Replace("/.", "d0t").Replace('.', '_').Replace("d0t", ".");
                    formula = formula.Replace("[", "$find('ctl00_ContentPlaceHolder1_quest");
                    formula = formula.Replace("]", "').get_value()");

                    // combobox
                    if (formula.Length > 3)
                    {
                        if (formula.Substring(0, 3) == "CBO")
                        {
                            formula = formula.Substring(3);
                            script.AppendFormat("var dd{0} = $find('ctl00_ContentPlaceHolder1_quest{0}');", id);
                            script.AppendLine();
                            script.AppendFormat("var value{0}={1};", id, formula);
                            script.AppendLine();
                            script.AppendFormat("var item{0} = dd{0}.findItemByValue(value{0});", id);
                            script.AppendLine();
                            script.AppendFormat("item{0}.select();", id);
                            continue;
                        }
                    }

                    script.AppendFormat("var value{0}={1};", id, formula);
                    script.AppendLine();
                    script.AppendFormat("$find('ctl00_ContentPlaceHolder1_quest{0}').set_value(value{0});", id);
                }
                script.AppendLine();
                script.AppendLine("}</script>");
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "formula", script.ToString());
            }
        }

        private void InitializedQuestion(DataRow[] questionRows, Table groupTable, TableRow row, Hashtable formulas)
        {
            foreach (DataRow rowChild in questionRows)
            {
                //-----------------------
                // diperlukan untuk form askep multi hirarki
                var ctlID = QuestionControlID(rowChild["QuestionID"].ToString());
                var ctlAlreadyCreated = Helper.FindControlRecursive(groupTable, ctlID);
                if (ctlAlreadyCreated != null) continue;
                //-----------------------

                if (!rowChild["Formula"].ToString().Equals(string.Empty))
                {
                    formulas.Add(rowChild["QuestionID"].ToString().Replace('.', '_'), rowChild["Formula"].ToString());
                }
                if (!string.IsNullOrEmpty(rowChild["SRAnswerType"].ToString()))
                {
                    row = InitializedRowQuestion(rowChild);
                    groupTable.Rows.Add(row);
                }
                var quest = new QuestionQuery();
                quest.Where(quest.ParentQuestionID == rowChild["QuestionID"], quest.SRAnswerType != string.Empty);
                quest.OrderBy(quest.IndexNo.Ascending);
                var dtbSubQuestion = quest.LoadDataTable();

                if (dtbSubQuestion.Rows.Count > 0)
                {
                    InitializedQuestion(dtbSubQuestion.Select(), groupTable, row, formulas);
                }
            }
        }

        private void InitializedQuestion_DEACTIVATED(string formID)
        {
            //  Get List Question Group
            IEnumerable<QuestionGroup> questionGroups = LoadQuestionGroup(formID);

            //  Get List Question
            var dtbQuestion = QuestionDataTable(formID);
            //  Generate Question Entry
            pnlSanitationControlSheetItem.Controls.Clear();
            int rowNo = 0;
            var formulas = new Hashtable(); // Untuk menampung jawaban jenis formula
            foreach (QuestionGroup questionGroup in questionGroups)
            {
                rowNo++;
                var groupTable = new Table { Width = Unit.Percentage(100) };
                // Add Group Label
                var row = new TableRow();
                groupTable.Rows.Add(row);
                var cell = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Left,
                    Text = string.Format("{0}. {1}", rowNo, questionGroup.QuestionGroupName)
                };
                cell.Font.Bold = true;
                cell.Style["color"] = "white";
                cell.Style["background-color"] = "#758DA6";
                cell.ColumnSpan = 3;
                row.Cells.Add(cell);

                groupTable.Rows.Add(row);
                DataRow[] questionRows = dtbQuestion.Select(string.Format("QuestionGroupID='{0}'", questionGroup.QuestionGroupID), "RowIndex");

                //var questionRows = dtbQuestion.AsEnumerable().Where(d => ! string.IsNullOrEmpty(d.Field<string>("ParentQuestionID")));

                foreach (DataRow rowChild in questionRows)
                {
                    //-----------------------
                    // diperlukan untuk form askep multi hirarki
                    var ctlID = QuestionControlID(rowChild["QuestionID"].ToString());
                    var ctlAlreadyCreated = Helper.FindControlRecursive(groupTable, ctlID);
                    if (ctlAlreadyCreated != null) continue;
                    //-----------------------

                    if (!rowChild["Formula"].ToString().Equals(string.Empty))
                    {
                        formulas.Add(rowChild["QuestionID"].ToString().Replace('.', '_'), rowChild["Formula"].ToString());
                    }
                    if (!string.IsNullOrEmpty(rowChild["SRAnswerType"].ToString()))
                    {
                        row = InitializedRowQuestion(rowChild);
                        groupTable.Rows.Add(row);
                    }
                    var quest = new QuestionQuery();
                    quest.Where(quest.ParentQuestionID == rowChild["QuestionID"], quest.SRAnswerType != string.Empty);
                    quest.OrderBy(quest.IndexNo.Ascending);
                    var dtbSubQuestion = quest.LoadDataTable();

                    foreach (DataRow rowSubQuestion in dtbSubQuestion.Rows)
                    {
                        //-----------------------
                        // diperlukan untuk form askep multi hirarki
                        ctlID = QuestionControlID(rowSubQuestion["QuestionID"].ToString());
                        var ctlAlreadyCreated2 = Helper.FindControlRecursive(groupTable, ctlID);
                        if (ctlAlreadyCreated2 != null) continue;
                        //-----------------------

                        if (!rowSubQuestion["Formula"].ToString().Equals(string.Empty))
                        {
                            formulas.Add(rowSubQuestion["QuestionID"].ToString().Replace('.', '_'),
                                         rowSubQuestion["Formula"].ToString());
                        }
                        row = InitializedRowQuestion(rowSubQuestion);
                        groupTable.Rows.Add(row);
                    }
                }
                pnlSanitationControlSheetItem.Controls.Add(groupTable);
            }

            //Generate Formula Script
            if (!IsPostBack)
            {
                var script = new StringBuilder();
                script.AppendLine("<script type='text/javascript' language='javascript'>");
                script.AppendLine("function fillFormulaField(){");

                foreach (DictionaryEntry dictionaryEntry in formulas)
                {

                    string id = dictionaryEntry.Key.ToString();

                    // [200.020]/(([200.030]/100)*([200.030]/100))
                    string formula = dictionaryEntry.Value.ToString();
                    formula = formula.Replace("/.", "d0t").Replace('.', '_').Replace("d0t", ".");
                    formula = formula.Replace("[", "$find('ctl00_ContentPlaceHolder1_quest");
                    formula = formula.Replace("]", "').get_value()");

                    // combobox
                    if (formula.Length > 3)
                    {
                        if (formula.Substring(0, 3) == "CBO")
                        {
                            formula = formula.Substring(3);
                            script.AppendFormat("var dd{0} = $find('ctl00_ContentPlaceHolder1_quest{0}')", id);
                            script.AppendLine();
                            script.AppendFormat("var value{0}={1};", id, formula);
                            script.AppendLine();
                            script.AppendFormat("var item{0} = dd{0}.findItemByValue(value{0});", id);
                            script.AppendLine();
                            script.AppendFormat("item{0}.select();", id);
                            continue;
                        }
                    }

                    script.AppendFormat("var value{0}={1};", id, formula);
                    script.AppendLine();
                    script.AppendFormat("$find('ctl00_ContentPlaceHolder1_quest{0}').set_value(value{0});", id);
                }
                script.AppendLine();
                script.AppendLine("}</script>");
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "formula", script.ToString());
            }
        }

        private void CreateValidationControl(string ctlToValidate, string unique, DataRow rowQuestion, TableCell tc)
        {
            //if (true)
            if ((bool)rowQuestion["IsMandatory"])
            {
                var rfv = new RequiredFieldValidator();
                rfv.ValidationGroup = "entry";
                rfv.ID = RfvControlID(rowQuestion["QuestionID"].ToString() + unique);
                rfv.ControlToValidate = ctlToValidate;
                rfv.ErrorMessage = string.Format("Field {0} Required!", rowQuestion["QuestionText"]);
                rfv.SetFocusOnError = true;
                //rfv.ForeColor = System.Drawing.Color.Red;
                //rfv.Display = ValidatorDisplay.Dynamic;
                //rfv.IsValid = false;
                //rfv.EnableViewState = true;
                System.Web.UI.WebControls.Image myImg = new System.Web.UI.WebControls.Image();
                myImg.Visible = true;
                myImg.SkinID = "rfvImage";
                rfv.Controls.Add(myImg);

                tc.Controls.Add(rfv);
                //Page.Controls.Add(rfv);
            }
        }

        private TableRow InitializedRowQuestion(DataRow rowQuestion)
        {
            var tblRow = new TableRow();
            string answerType = rowQuestion["SRAnswerType"].ToString();
            string controlID = QuestionControlID(rowQuestion["QuestionID"].ToString());

            //Create 2 Cell
            tblRow.Cells.Add(new TableCell());
            tblRow.Cells.Add(new TableCell());
            //validation
            //tblRow.Cells.Add(new TableCell());
            //tblRow.Cells.Add(new TableCell());

            tblRow.Cells[0].Attributes["class"] = "label";
            //tblRow.Cells[0].VerticalAlign = VerticalAlign.Top;

            var litSep = new Literal();
            switch (answerType)
            {
                case "MSK":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
                    var msk = MaskedTextBoxControl(controlID, rowQuestion["AnswerWidth"].ToInt(), rowQuestion["QuestionAnswerSelectionID"].ToStringDefaultEmpty());
                    tblRow.Cells[1].Controls.Add(msk);
                    CreateValidationControl(msk.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "DAT":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);

                    var dat = DatePickerControl(controlID, rowQuestion["AnswerWidth"].ToInt(), Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
                    tblRow.Cells[1].Controls.Add(dat);
                    CreateValidationControl(dat.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "TIM":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);

                    var tim = TimePickerControl(controlID, rowQuestion["AnswerWidth"].ToInt(), Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
                    tblRow.Cells[1].Controls.Add(tim);
                    CreateValidationControl(tim.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "DTM":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);

                    var dattim = DateTimePickerControl(controlID, rowQuestion["AnswerWidth"].ToInt(), Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
                    tblRow.Cells[1].Controls.Add(dattim);
                    CreateValidationControl(dattim.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "LBL":
                    //AddRowLabel(tblRow, rowQuestion);
                    AddLabel(controlID, tblRow, rowQuestion);
                    break;
                case "NUM":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var num = RadNumericTextBoxControl(controlID, int.Parse(rowQuestion["AnswerDecimalDigit"].ToString()),
                                             rowQuestion["AnswerSuffix"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                             rowQuestion["Formula"].ToString(), rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());
                    tblRow.Cells[1].Controls.Add(num);
                    CreateValidationControl(num.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "MEM":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var mem = MemoControl(controlID, rowQuestion["AnswerWidth"].ToInt(), rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());
                    tblRow.Cells[1].Controls.Add(mem);
                    CreateValidationControl(mem.ID, "", rowQuestion, tblRow.Cells[1]);

                    break;
                case "TXT":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
                    var txt = TextBoxControl(controlID, rowQuestion["AnswerWidth"].ToInt(), rowQuestion["Formula"].ToString(), rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());
                    //tblRow.Cells[1].Controls.Add(txt);

                    if (!string.IsNullOrEmpty(rowQuestion["AnswerSuffix"].ToString()))
                    {
                        litSep = new Literal();
                        litSep.Text = "&nbsp;&nbsp;" + rowQuestion["AnswerSuffix"].ToString();
                        tblRow.Cells[1].Controls.Add(litSep);

                        var tab = new HtmlTable() { ID = "tab_" + controlID, CellSpacing = 0, CellPadding = 0 };

                        var row = new HtmlTableRow();
                        row.Cells.Add(new HtmlTableCell());
                        row.Cells.Add(new HtmlTableCell());

                        row.Cells[0].Controls.Add(txt);
                        row.Cells[1].Controls.Add(litSep);

                        tab.Rows.Add(row);

                        tblRow.Cells[1].Controls.Add(tab);
                    }
                    else
                        tblRow.Cells[1].Controls.Add(txt);

                    CreateValidationControl(txt.ID, "", rowQuestion, tblRow.Cells[1]);

                    break;
                case "CBO":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbo = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    rowQuestion["Formula"].ToString());
                    tblRow.Cells[1].Controls.Add(cbo);
                    CreateValidationControl(cbo.ID, "", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CHK":
                    var chk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(chk);
                    break;
                case "TTX":
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
                    var txt1 = TextBoxControl(controlID, rowQuestion["AnswerWidth"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID"].ToString());
                    tblRow.Cells[1].Controls.Add(txt1);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var txt2 = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(txt2);
                    CreateValidationControl(txt1.ID, "1", rowQuestion, tblRow.Cells[1]);
                    CreateValidationControl(txt2.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CTX":
                    var ctxChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(ctxChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var ctxTxt = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(ctxTxt);
                    break;
                case "CDO":
                    var cdoChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(cdoChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cdoCbo = ComboBoxControl(controlID + "_2", rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cdoCbo);
                    break;
                case "CTM":
                    var ctmChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(ctmChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var ctmTxt = MemoControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(ctmTxt);
                    break;
                case "CNM":
                    var cnmChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(cnmChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cnmNum = RadNumericTextBoxControl(controlID + "_2", int.Parse(rowQuestion["AnswerDecimalDigit"].ToString()),
                                             rowQuestion["AnswerSuffix"].ToString(), rowQuestion["AnswerWidth2"].ToInt(),
                                             string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(cnmNum);
                    break;
                case "CDT":
                    var cdtChk = CheckBoxControl(controlID, rowQuestion["QuestionText"].ToString(), rowQuestion["AnswerWidth"].ToInt());
                    tblRow.Cells[1].Controls.Add(cdtChk);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cdtDattim = DateTimePickerControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), Convert.ToBoolean(rowQuestion["IsEmptyDefault"]));
                    tblRow.Cells[1].Controls.Add(cdtDattim);
                    break;
                case "CB2":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbo1 = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cbo1);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cbo2 = ComboBoxControl(controlID + "_2", rowQuestion["QuestionAnswerSelectionID2"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString(), rowQuestion["AnswerWidth2"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cbo2);
                    CreateValidationControl(cbo1.ID, "1", rowQuestion, tblRow.Cells[1]);
                    CreateValidationControl(cbo2.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CBT":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbCbo = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cbCbo);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cbTxt = TextBoxControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(cbTxt);
                    CreateValidationControl(cbCbo.ID, "1", rowQuestion, tblRow.Cells[1]);
                    CreateValidationControl(cbTxt.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
                case "CBN":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbCbn = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cbCbn);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cbnNum = RadNumericTextBoxControl(controlID + "_2", int.Parse(rowQuestion["AnswerDecimalDigit"].ToString()),
                                             rowQuestion["AnswerSuffix"].ToString(), rowQuestion["AnswerWidth2"].ToInt(),
                                             string.Empty, rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(cbnNum);
                    break;
                case "CBM":
                    AddCaptionLabel(tblRow, rowQuestion);
                    var cbCbm = ComboBoxControl(controlID, rowQuestion["QuestionAnswerSelectionID"].ToString(),
                                    rowQuestion["QuestionAnswerDefaultSelectionID"].ToString(), rowQuestion["AnswerWidth"].ToInt(),
                                    string.Empty);
                    tblRow.Cells[1].Controls.Add(cbCbm);
                    litSep = new Literal();
                    litSep.Text = string.Format("&nbsp;{0}&nbsp;", rowQuestion["AnswerPrefix"]);
                    tblRow.Cells[1].Controls.Add(litSep);
                    var cbTxm = MemoControl(controlID + "_2", rowQuestion["AnswerWidth2"].ToInt(), rowQuestion["QuestionAnswerDefaultSelectionID2"].ToString());
                    tblRow.Cells[1].Controls.Add(cbTxm);
                    CreateValidationControl(cbCbm.ID, "1", rowQuestion, tblRow.Cells[1]);
                    CreateValidationControl(cbTxm.ID, "2", rowQuestion, tblRow.Cells[1]);
                    break;
                case "TBL":
                    AddCaptionLabel(tblRow, rowQuestion);
                    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);

                    var tbl = new HtmlTable() { ID = controlID, CellSpacing = 0, CellPadding = 0 };
                    // ambil answer width sebagai jumlah kolom
                    // ambil answer width 2 sebagai jumalh row
                    var cList = rowQuestion["QuestionAnswerSelectionID"].ToString().Split('|');
                    var defaultAnsList = rowQuestion["QuestionAnswerDefaultSelectionID"].ToString().Split('|');
                    var cCount = cList.Length;
                    var rCount = rowQuestion["AnswerWidth"].ToInt();
                    //create header table
                    for (int iR = 0; iR <= rCount; iR++)
                    { // row
                        var rowTbl = new HtmlTableRow();
                        for (int iC = 0; iC < cCount; iC++)
                        {
                            var rHeader = cList[iC].Split(':');
                            rowTbl.Cells.Add(new HtmlTableCell());
                            if (iR == 0)
                            {
                                // table header
                                rowTbl.Cells[iC].InnerText = rHeader[0]; // print header
                                rowTbl.Cells[iC].Width = rHeader.Length == 1 ? "50" : rHeader[1];
                                rowTbl.Align = "Center";
                            }
                            else
                            {
                                // table content input
                                var txtCell = TextBoxControl(
                                    controlID + "_" + iR.ToString() + "_" + iC.ToString(),
                                    System.Convert.ToInt32((rHeader.Length == 1 ? "50" : rHeader[1])),
                                    string.Empty, string.Empty);
                                // set defaul value if any
                                if ((iC % cCount) + (iR - 1) * cCount < defaultAnsList.Length)
                                {
                                    txtCell.Text = defaultAnsList[(iC % cCount) + (iR - 1) * cCount];
                                }
                                rowTbl.Cells[iC].Controls.Add(txtCell);
                            }
                        }
                        tbl.Rows.Add(rowTbl);
                    }
                    tblRow.Cells[1].Controls.Add(tbl);
                    break;

            }
            return tblRow;
        }
        private HtmlTableCell AddTableCell(string controlID)
        {
            var txt = new RadTextBox() { ID = controlID, Width = Unit.Pixel(35), MaxLength = 2 };
            txt.Style["text-align"] = "center";
            txt.Style["font-weight"] = "bold";

            var cell = new HtmlTableCell();
            cell.Controls.Add(txt);
            return cell;
        }
        private HtmlTableCell AddTableCellStandard(string text)
        {
            var cell = new HtmlTableCell();
            cell.Style["border-bottom-style"] = "solid";
            cell.InnerText = text;
            return cell;
        }
        private void AddLabel(string id, TableRow tblRow, DataRow rowQuestion)
        {
            var lbl = new Label();
            lbl.ID = id;
            lbl.Text = string.Format("<b>{0}{1}</b>", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);

            tblRow.Cells[0].ColumnSpan = 3;
            tblRow.Cells[0].Controls.Add(lbl);
        }
        private void AddRowLabel(TableRow tblRow, DataRow rowQuestion)
        {
            //if (rowQuestion["QuestionLevel"].ToInt() == 0)
            //{
            tblRow.Cells[0].ColumnSpan = 3;
            tblRow.Cells[0].Text = string.Format("<b>{0}{1}</b>", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
            //}
            //else
            //    tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
        }
        private void AddCaptionLabel(TableRow tblRow, DataRow rowQuestion)
        {
            if (rowQuestion["QuestionLevel"].ToInt() == 0)
            {
                tblRow.Cells[0].ColumnSpan = 3;
                tblRow.Cells[0].Text = string.Format("<b>{0}{1}</b>", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
            }
            else
                tblRow.Cells[0].Text = string.Format("{0}{1}", Spacer(rowQuestion["QuestionLevel"].ToInt()), rowQuestion["QuestionText"]);
        }
        private string Spacer(int questionLevel)
        {
            var retval = string.Empty;
            for (int i = 0; i < questionLevel; i++)
            {
                retval = string.Concat(retval, "&nbsp;&nbsp;");
            }
            return retval;
        }

        private RadDatePicker DatePickerControl(string id, int width, bool isEmptyDefault)
        {
            var obj = new RadDatePicker();
            obj.ID = id;
            if (!isEmptyDefault)
                obj.SelectedDate = DateTime.Now;
            obj.Width = Unit.Pixel(width == 0 ? 100 : width);
            return obj;
        }
        private RadTimePicker TimePickerControl(string id, int width, bool isEmptyDefault)
        {
            var obj = new RadTimePicker();
            obj.ID = id;
            if (!isEmptyDefault)
                obj.SelectedDate = DateTime.Now;
            obj.Width = Unit.Pixel(width == 0 ? 100 : width);
            return obj;
        }
        private RadDateTimePicker DateTimePickerControl(string id, int width, bool isEmptyDefault)
        {
            var obj = new RadDateTimePicker();
            obj.ID = id;
            if (!isEmptyDefault)
                obj.SelectedDate = DateTime.Now;
            obj.Width = Unit.Pixel(width == 0 ? 170 : width);
            //obj.TimeView.TimeFormat = "HH:mm tt";
            return obj;
        }
        private CheckBox CheckBoxControl(string id, string text, int width)
        {
            var chk = new CheckBox();
            chk.ID = id;
            chk.Width = Unit.Pixel(width == 0 ? 300 : width);
            chk.Text = text;
            return chk;
        }
        private RadTextBox TextBoxControl(string id, int width, string formula, string defaultValue)
        {
            var textBox = new RadTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            if (!string.IsNullOrEmpty(formula))
            {
                textBox.ShowButton = true;
                textBox.ClientEvents.OnValueChanged = "fillFormulaField";
                textBox.ClientEvents.OnButtonClick = "fillFormulaField";
            }
            // default value
            textBox.Text = defaultValue;
            return textBox;
        }
        private RadTextBox MemoControl(string id, int width, string defaultValue)
        {
            var textBox = new RadTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            textBox.Height = Unit.Pixel(100); //sebelumnya di remark
            textBox.TextMode = InputMode.MultiLine;
            textBox.Text = defaultValue;
            return textBox;
        }
        private RadNumericTextBox RadNumericTextBoxControl(string id, int decimalDigit, string suffix, int width, string formula, string defaultVal)
        {
            var textBox = new RadNumericTextBox();
            textBox.ID = id;
            textBox.Width = Unit.Pixel(width == 0 ? 100 : width);
            textBox.NumberFormat.DecimalDigits = decimalDigit;
            textBox.NumberFormat.PositivePattern = suffix.Equals("&nbsp;")
                                                       ? string.Empty
                                                       : string.Format("n {0}", suffix);
            defaultVal = defaultVal.Trim();
            if (Helper.IsNumeric(defaultVal))
            {
                textBox.Value = System.Convert.ToDouble(defaultVal);
            }
            else
            {
                textBox.Value = 0;
            }
            if (!string.IsNullOrEmpty(formula))
            {
                textBox.ShowButton = true;
                textBox.ClientEvents.OnButtonClick = "fillFormulaField";
            }
            return textBox;
        }
        private RadMaskedTextBox MaskedTextBoxControl(string id, int width, string mask)
        {
            var textBox = new RadMaskedTextBox();
            textBox.ID = id;
            textBox.Mask = mask;
            textBox.Width = Unit.Pixel(width == 0 ? 300 : width);
            return textBox;
        }

        private RadComboBox ComboBoxControl(string id, string selectionID, string defaultSelectionID, int width, string formula)
        {
            var comboBox = new RadComboBox();
            //comboBox.AllowCustomText = true;
            //comboBox.Filter = RadComboBoxFilter.Contains;
            comboBox.ID = id;
            comboBox.Width = Unit.Pixel(width == 0 ? 304 : width);
            var query = new QuestionAnswerSelectionLineQuery();
            query.Where(query.QuestionAnswerSelectionID == selectionID);
            query.Select(query.QuestionAnswerSelectionLineID, query.QuestionAnswerSelectionLineText);
            DataTable dtb = query.LoadDataTable();
            comboBox.Items.Clear();
            comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                comboBox.Items.Add(new RadComboBoxItem(row["QuestionAnswerSelectionLineText"].ToString(),
                                                       row["QuestionAnswerSelectionLineID"].ToString()));
            }

            if (!string.IsNullOrEmpty(formula))
            {
                //comboBox.ShowButton = true;
                //comboBox.ClientEvents.OnValueChanged = "fillFormulaField";
                //comboBox.ClientEvents.OnButtonClick = "fillFormulaField";
            }

            if (!string.IsNullOrEmpty(defaultSelectionID))
                comboBox.SelectedValue = defaultSelectionID;

            return comboBox;
        }


        private void AddSpacerCell(TableCellCollection cells)
        {
            var cell = new TableCell { Text = "&nbsp;&nbsp;", Wrap = false };
            cells.Add(cell);
        }

        #endregion

        protected void txtControlDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            txtControlSheetNo.Text = GetNewTransactionNo();
        }
    }
}