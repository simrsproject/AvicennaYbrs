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

namespace Temiang.Avicenna.Module.HR.KEHRS
{
    public partial class SafetyCultureIncidentReportsDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.IncidentReportNo);
            return _autoNumber.LastCompleteNumber;
        }

        private string QuestionFormID
        {
            get { return AppSession.Parameter.QuestionFormEmployeeSafetyCultureIncidentReports; }
        }

        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = FormType == "ver" ? AppConstant.Program.KEHRS_SafetyCultureIncidentReportsVerification : (FormType == "con" ? AppConstant.Program.KEHRS_SafetyCultureIncidentReportsConclusion : AppConstant.Program.KEHRS_SafetyCultureIncidentReports);
            UrlPageList = FormType == "ver" ? "../SafetyCultureIncidentReportsVerification/SafetyCultureIncidentReportsVerificationList.aspx" : (FormType == "con" ? "../SafetyCultureIncidentReportsConclusion/SafetyCultureIncidentReportsConclusionList.aspx" : "SafetyCultureIncidentReportsList.aspx");
            UrlPageSearch = (FormType == "ver" || FormType == "con") ? "##" : "SafetyCultureIncidentReportsSearch.aspx?type=";

            WindowSearch.Height = 400;
            
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRIncidentReportStatus, AppEnum.StandardReference.IncidentReportStatus);

                rfvSRIncidentReportStatus.Visible = FormType == "ver";
                
                tabStrip.Tabs[2].Visible = FormType == "ver" || FormType == "con";
                tabStrip.Tabs[3].Visible = FormType == "con";

                trDocumentUpload.Visible = FormType == "ver" || FormType == "con";
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

            ToolBarMenuSearch.Enabled = FormType != "ver" && FormType != "con";
            ToolBarMenuAdd.Enabled = FormType != "ver" && FormType != "con";
            ToolBarMenuMoveNext.Enabled = false; //FormType != "ver" && FormType != "con";
            ToolBarMenuMovePrev.Enabled = false; //FormType != "ver" && FormType != "con";

            if (FormType == "ver" || FormType == "con")
            {
                txtReportDate.Enabled = false;
                txtReportTime.Enabled = false;
                txtReportDescription.ReadOnly = true;
                cboPersonID.Enabled = false;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeSafetyCultureIncidentReports());

            txtTransactionNo.Text = GetNewTransactionNo();

            txtReportDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtReportTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            var form = new QuestionForm();
            form.LoadByPrimaryKey(QuestionFormID);
            txtFormName.Text = form.QuestionFormName;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeeSafetyCultureIncidentReports();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;

                var collValue = new EmployeeSafetyCultureIncidentReportsQuestionCollection();
                collValue.Query.Where
                    (
                        collValue.Query.TransactionNo == txtTransactionNo.Text
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
            if (EmployeeSafetyCultureIncidentReportsSubjects.Count == 0)
            {
                args.MessageText = "Subject has not been defined.";
                args.IsCancel = true;
                return;
            }
            bool isMain = false;
            foreach (var subject in EmployeeSafetyCultureIncidentReportsSubjects)
            {
                if (subject.IsMainSubject == true)
                    isMain = true;
            }

            if (isMain == false)
            {
                args.MessageText = "Main subject has not been defined.";
                args.IsCancel = true;
                return;
            }

            var collValue = new EmployeeSafetyCultureIncidentReportsQuestionCollection();
            var entity = new EmployeeSafetyCultureIncidentReports();
            entity.AddNew();

            SetEntityValue(entity, collValue);
            SaveEntity(entity, collValue);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (EmployeeSafetyCultureIncidentReportsSubjects.Count == 0)
            {
                args.MessageText = "Subject has not been defined.";
                args.IsCancel = true;
                return;
            }

            bool isMain = false;
            foreach (var subject in EmployeeSafetyCultureIncidentReportsSubjects)
            {
                if (subject.IsMainSubject == true)
                    isMain = true;
            }

            if (isMain == false)
            {
                args.MessageText = "Main subject has not been defined.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeeSafetyCultureIncidentReports();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            var collValue = new EmployeeSafetyCultureIncidentReportsQuestionCollection();

            collValue.Query.Where
                (
                    collValue.Query.TransactionNo == txtTransactionNo.Text
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "EmployeeSafetyCultureIncidentReports";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            var form = new QuestionForm();
            form.LoadByPrimaryKey(QuestionFormID);

            AppSession.PrintJobReportID = form.ReportProgramID;
            programID = form.ReportProgramID;
            
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new EmployeeSafetyCultureIncidentReports();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            if (FormType == "con")
            {
                if (entity.IsVerified == null || entity.IsVerified == false)
                {
                    args.MessageText = "Verified Status required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (FormType ==  "ver")
            {
                if (entity.IsApproved == null || entity.IsApproved == false)
                {
                    args.MessageText = "Spproved Status required.";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                if (FormType != "ver" && FormType != "con")
                {
                    entity.IsApproved = true;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                }
                else if (FormType == "ver")
                {
                    entity.IsVerified = true;
                    entity.VerifiedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.VerifiedByUserID = AppSession.UserLogin.UserID;
                }
                else if (FormType == "con")
                {
                    entity.IsClosed = true;
                    entity.ClosedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ClosedByUserID = AppSession.UserLogin.UserID;
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new EmployeeSafetyCultureIncidentReports();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            if (FormType != "ver" && FormType != "con")
            {
                if (entity.IsVerified == true)
                {
                    args.MessageText = "Incident Reports already verified.";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "ver")
            {
                if (entity.IsClosed == true)
                {
                    args.MessageText = "Incident Reports already closed.";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                if (FormType != "ver" && FormType != "con")
                {
                    entity.IsApproved = false;
                    entity.ApprovedDateTime = null;
                    entity.ApprovedByUserID = null;
                }
                else if (FormType == "ver")
                {
                    entity.IsVerified = false;
                    entity.VerifiedDateTime = null;
                    entity.VerifiedByUserID = null;
                }
                else if (FormType == "con")
                {
                    entity.IsClosed = false;
                    entity.ClosedDateTime = null;
                    entity.ClosedByUserID = null;
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new EmployeeSafetyCultureIncidentReports();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
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

        private void SetVoid(EmployeeSafetyCultureIncidentReports entity, bool isVoid)
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

        private bool IsApprovedOrVoid(EmployeeSafetyCultureIncidentReports entity, ValidateArgs args)
        {
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }
            if (FormType == "ver")
            {
                if (entity.IsVerified ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else if (FormType == "con")
            {
                if (entity.IsClosed ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else
            {
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }

            return true;
        }

        private bool IsApproved(EmployeeSafetyCultureIncidentReports entity, ValidateArgs args)
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
            RefreshCommandItemSubject(oldVal, newVal);
            RefreshCommandItemWitness(oldVal, newVal);
            RefreshCommandItemChronology(oldVal, newVal);
            RefreshCommandItemParticipant(oldVal, newVal);
            RefreshCommandItemMeeting(oldVal, newVal);
            RefreshCommandItemConslusion(oldVal, newVal);
            RefreshCommandItemRecommendation(oldVal, newVal);

            var dtbQuestion = QuestionDataTable(QuestionFormID);
            
            foreach (DataRow rowQuestion in dtbQuestion.Rows)
            {
                // Tips: Don't use entity.es.IsModified, krn belum tentu record sudah diedit waktu save
                if (FormType != "ver" && FormType != "con")
                    SetReadOnlySafetyCultureIncidentReportsQuestion((newVal == AppEnum.DataMode.Read), rowQuestion, rowQuestion["QuestionGroupID"].ToString());
                else
                    SetReadOnlySafetyCultureIncidentReportsQuestion(true, rowQuestion, rowQuestion["QuestionGroupID"].ToString());
            }

            cboSRIncidentReportStatus.Enabled = (newVal != AppEnum.DataMode.Read) && FormType == "ver";
            txtResume.ReadOnly = !((newVal != AppEnum.DataMode.Read) && FormType == "ver");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new EmployeeSafetyCultureIncidentReports();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            var entity = new EmployeeSafetyCultureIncidentReports();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);

            var form = new QuestionForm();
            form.LoadByPrimaryKey(QuestionFormID);
            txtFormName.Text = form.QuestionFormName;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeSafetyCultureIncidentReports();

            entity.LoadByPrimaryKey(string.IsNullOrEmpty(txtTransactionNo.Text) ? Request.QueryString["id"] : txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var rpt = (EmployeeSafetyCultureIncidentReports)entity;

            txtTransactionNo.Text = rpt.TransactionNo;
            if (rpt.ReportDate.HasValue)
            {
                txtReportDate.SelectedDate = rpt.ReportDate;
                txtReportTime.Text = rpt.ReportDate.Value.ToString("HH:mm");
            }
            
            txtReportDescription.Text = rpt.ReportDescription;
            if (!string.IsNullOrEmpty(rpt.QuestionFormID))
            {
                var form = new QuestionForm();
                form.LoadByPrimaryKey(rpt.QuestionFormID);
                txtFormName.Text = form.QuestionFormName;
            }

            if (rpt.VictimPersonID.HasValue && rpt.VictimPersonID.ToInt() > -1)
            {
                var query = new VwEmployeeTableQuery();
                query.Select
                    (
                        query.PersonID,
                        query.EmployeeNumber,
                        query.EmployeeName
                    );

                query.Where(query.PersonID == rpt.VictimPersonID.ToInt());

                cboPersonID.DataSource = query.LoadDataTable();
                cboPersonID.DataBind();

                cboPersonID.SelectedValue = rpt.VictimPersonID.ToString();
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.SelectedValue = string.Empty;
                cboPersonID.Text = string.Empty;
            }
            cboSRProfessionType.SelectedValue = rpt.VictimSRProfessionType;
            if (rpt.VictimOrganizationID.HasValue && rpt.VictimOrganizationID.ToInt() > 0)
            {
                PopulateCboOrganizationUnitID(cboOrganizationUnitID, rpt.VictimOrganizationID.ToInt());
                cboOrganizationUnitID.SelectedValue = rpt.VictimOrganizationID.ToString();
            }
            else
            {
                cboOrganizationUnitID.Items.Clear();
                cboOrganizationUnitID.SelectedValue = string.Empty;
                cboOrganizationUnitID.Text = string.Empty;
            }
            if (rpt.VictimSubOrganizationID.HasValue && rpt.VictimSubOrganizationID.ToInt() > 0)
            {
                PopulateCboOrganizationUnitID(cboSubOrganizationUnitID, rpt.VictimSubOrganizationID.ToInt());
                cboSubOrganizationUnitID.SelectedValue = rpt.VictimSubOrganizationID.ToString();
            }
            else
            {
                cboSubOrganizationUnitID.Items.Clear();
                cboSubOrganizationUnitID.SelectedValue = string.Empty;
                cboSubOrganizationUnitID.Text = string.Empty;
            }
            if (rpt.VictimSubDivisonID.HasValue && rpt.VictimSubDivisonID.ToInt() > 0)
            {
                PopulateCboOrganizationUnitID(cboSubDivisonID, rpt.VictimSubDivisonID.ToInt());
                cboSubDivisonID.SelectedValue = rpt.VictimSubDivisonID.ToString();
            }
            else 
            {
                cboSubDivisonID.Items.Clear();
                cboSubDivisonID.SelectedValue = string.Empty;
                cboSubDivisonID.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(rpt.VictimServiceUnitID))
            {
                PopulateCboOrganizationUnitID(cboServiceUnit, rpt.VictimServiceUnitID.ToInt());
                cboServiceUnit.SelectedValue = rpt.VictimServiceUnitID;
            }
            else 
            {
                cboServiceUnit.Items.Clear();
                cboServiceUnit.SelectedValue = string.Empty;
                cboServiceUnit.Text = string.Empty;
            }

            cboSRIncidentReportStatus.SelectedValue = rpt.SRIncidentReportStatus;
            txtResume.Text = rpt.Resume;

            if (FormType == "con")
                chkIsApproved.Checked = rpt.IsClosed ?? false;
            else if (FormType == "ver")
                chkIsApproved.Checked = rpt.IsVerified ?? false;
            else chkIsApproved.Checked = rpt.IsApproved ?? false;

            chkIsVoid.Checked = rpt.IsVoid ?? false;

            PopulateSubjectGrid();
            PopulateWitnessGrid();
            PopulateQuestionValue();
            PopulateChronologyGrid();
            PopulateParticipantGrid();
            PopulateMeetingGrid();
            PopulateConslusionGrid();
            PopulateRecommendationGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(EmployeeSafetyCultureIncidentReports entity, EmployeeSafetyCultureIncidentReportsQuestionCollection collValue)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                _autoNumber.Save();
            }

            entity.TransactionNo = txtTransactionNo.Text;
            entity.QuestionFormID = QuestionFormID;
            entity.ReportDescription = txtReportDescription.Text;
            entity.ReportDate = DateTime.Parse(txtReportDate.SelectedDate.Value.ToShortDateString() + " " + txtReportTime.TextWithLiterals);
            entity.VictimPersonID = cboPersonID.SelectedValue.ToInt();
            entity.VictimSRProfessionType = cboSRProfessionType.SelectedValue;

            int victimOrganizationId = 0;
            int.TryParse(cboOrganizationUnitID.SelectedValue, out victimOrganizationId);
            int victimSubOrganizationId = 0;
            int.TryParse(cboSubOrganizationUnitID.SelectedValue, out victimSubOrganizationId);
            int victimSubDivisonId = 0;
            int.TryParse(cboSubDivisonID.SelectedValue, out victimSubDivisonId);
            
            entity.VictimOrganizationID = victimOrganizationId;
            entity.VictimSubOrganizationID = victimSubOrganizationId;
            entity.VictimSubDivisonID = victimSubDivisonId;
            entity.VictimServiceUnitID = cboServiceUnit.SelectedValue;

            entity.SRIncidentReportStatus = cboSRIncidentReportStatus.SelectedValue;
            entity.Resume = txtResume.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = DateTime.Now;
            }

            if (FormType != "ver" && FormType != "con")
            {
                foreach (var subject in EmployeeSafetyCultureIncidentReportsSubjects)
                {
                    subject.TransactionNo = txtTransactionNo.Text;
                    subject.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    subject.LastUpdateDateTime = DateTime.Now;
                }

                foreach (var witness in EmployeeSafetyCultureIncidentReportsWitnesses)
                {
                    witness.TransactionNo = txtTransactionNo.Text;
                    witness.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    witness.LastUpdateDateTime = DateTime.Now;
                }
                entity.IsVoid = chkIsVoid.Checked;
                entity.IsApproved = chkIsApproved.Checked;
            }

            if (FormType == "ver")
            {
                foreach (var chronology in EmployeeSafetyCultureIncidentReportsChronologys)
                {
                    chronology.TransactionNo = txtTransactionNo.Text;
                    chronology.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    chronology.LastUpdateDateTime = DateTime.Now;
                }

                foreach (var participant in EmployeeSafetyCultureIncidentReportsParticipants)
                {
                    participant.TransactionNo = txtTransactionNo.Text;
                    participant.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    participant.LastUpdateDateTime = DateTime.Now;
                }
                entity.IsVerified = chkIsApproved.Checked;
            }

            if (FormType == "con")
            {
                foreach (var meeting in EmployeeSafetyCultureIncidentReportsMeetings)
                {
                    meeting.TransactionNo = txtTransactionNo.Text;
                    meeting.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    meeting.LastUpdateDateTime = DateTime.Now;
                }

                foreach (var con in EmployeeSafetyCultureIncidentReportsConslusions)
                {
                    con.TransactionNo = txtTransactionNo.Text;
                    con.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    con.LastUpdateDateTime = DateTime.Now;
                }

                foreach (var rec in EmployeeSafetyCultureIncidentReportsRecommendations)
                {
                    rec.TransactionNo = txtTransactionNo.Text;
                    rec.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    rec.LastUpdateDateTime = DateTime.Now;
                }
                entity.IsClosed = chkIsApproved.Checked;
            }

            //PatientHealthRecordLine
            var dtbQuestion = QuestionDataTable(QuestionFormID);

            foreach (DataRow rowQuestion in dtbQuestion.Rows)
            {
                // Tips: Don't use entity.es.IsModified, krn belum tentu record sudah diedit waktu save
                SetSafetyCultureIncidentReportsQuestion(entity.es.IsAdded, entity.TransactionNo, collValue, rowQuestion, rowQuestion["QuestionGroupID"].ToString());

                var quest = new QuestionQuery();
                quest.Where(quest.ParentQuestionID == rowQuestion["QuestionID"] && quest.SRAnswerType != string.Empty);
                var dtbSubQuestion = quest.LoadDataTable();

                foreach (DataRow rowSubQuestion in dtbSubQuestion.Rows)
                {
                    SetSafetyCultureIncidentReportsQuestion(entity.es.IsAdded, entity.TransactionNo, collValue, rowSubQuestion, rowQuestion["QuestionGroupID"].ToString());
                }
            }
        }

        private void SetSafetyCultureIncidentReportsQuestion(bool isNewRecord, string transactionNo, EmployeeSafetyCultureIncidentReportsQuestionCollection collValue, DataRow rowQuestion, string questionGroupID)
        {

            EmployeeSafetyCultureIncidentReportsQuestion hrLine;
            string questionID = rowQuestion[EmployeeSafetyCultureIncidentReportsQuestionMetadata.ColumnNames.QuestionID].ToString();
            //if (isNewRecord)
            //    hrLine = collValue.AddNew();
            //else
            hrLine = collValue.FindByPrimaryKey(txtTransactionNo.Text, QuestionFormID, questionGroupID, questionID) ?? collValue.AddNew();

            hrLine.TransactionNo = transactionNo;
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
                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID);
                else
                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, QuestionControlID(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()));

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

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    var ctxTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(ctxTxt.Text));
                    break;
                case "CDO":
                    var cdoChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cdoChk != null && cdoChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    var cdoCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cdoCbo.Text));
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cdoCbo.SelectedValue);
                    break;
                case "CTM":
                    var ctmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctmChk != null && ctmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    var ctmTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(ctmTxt.Text));
                    break;
                case "CNM":
                    var cnmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cnmChk != null && cnmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    var cnmNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cnmNum.Text));
                    break;
                case "CDT":
                    var cdtChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cdtChk != null && cdtChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    var cdtDattim = (obj as RadDateTimePicker);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate((cdtDattim.SelectedDate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm")));
                    break;
                case "CBT":
                    var cbtCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbo.Text);

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    var cbtTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbtTxt.Text));
                    break;
                case "CBN":
                    var cbnCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbnCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbnCbo.Text);

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    var cbnNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbnNum.Text));
                    break;
                case "CBM":
                    var cbtCbm = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbm.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbm.Text);

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    var cbtTxm = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbtTxm.Text));
                    break;
                case "CB2":
                    var cbo1 = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbo1.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbo1.Text);

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    var cbo2 = (obj as RadComboBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbo2.Text));

                    hrLine.QuestionAnswerSelectionLineID = string.Format("{0}|{1}", HtmlTagHelper.Validate(cbo1.SelectedValue), HtmlTagHelper.Validate(cbo2.SelectedValue));
                    break;
                case "TTX":
                    var txt1 = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(txt1.Text);

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                    pnlSafetyCultureIncidentReportsQuestion,
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

        private void SaveEntity(EmployeeSafetyCultureIncidentReports entity, EmployeeSafetyCultureIncidentReportsQuestionCollection collValue)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                collValue.Save();
                if (FormType != "ver" && FormType != "con")
                {
                    EmployeeSafetyCultureIncidentReportsSubjects.Save();
                    EmployeeSafetyCultureIncidentReportsWitnesses.Save();

                    EmployeeSafetyCultureIncidentReportsChronologys.Save();
                    foreach (var c in EmployeeSafetyCultureIncidentReportsChronologys)
                    {
                        string seqNo = c.SequenceNo;

                        var css = EmployeeSafetyCultureIncidentReportsChronologySubjects.Where(cs => cs.SequenceNo == seqNo);
                        foreach (var s in css)
                        {
                            var cs = new EmployeeSafetyCultureIncidentReportsChronologySubject();
                            cs.Query.Where(cs.Query.SequenceNo == seqNo, cs.Query.SubjectPersonID == s.SubjectPersonID);
                            if (!cs.Query.Load())
                                cs = new EmployeeSafetyCultureIncidentReportsChronologySubject();
                            cs.TransactionNo = entity.TransactionNo;
                            cs.SequenceNo = seqNo;
                            cs.SubjectPersonID = s.SubjectPersonID;
                            cs.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            cs.LastUpdateDateTime = DateTime.Now;
                            cs.Save();
                        }
                    }
                }

                if (FormType == "ver")
                {
                    EmployeeSafetyCultureIncidentReportsParticipants.Save();
                }

                if (FormType == "con")
                {
                    EmployeeSafetyCultureIncidentReportsMeetings.Save();
                    foreach (var c in EmployeeSafetyCultureIncidentReportsMeetings)
                    {
                        string seqNo = c.SequenceNo;

                        var css = EmployeeSafetyCultureIncidentReportsMeetingParticipants.Where(cs => cs.SequenceNo == seqNo);
                        foreach (var s in css)
                        {
                            var cs = new EmployeeSafetyCultureIncidentReportsMeetingParticipant();
                            cs.Query.Where(cs.Query.SequenceNo == seqNo, cs.Query.ParticipantPersonID == s.ParticipantPersonID);
                            if (!cs.Query.Load())
                                cs = new EmployeeSafetyCultureIncidentReportsMeetingParticipant();
                            cs.TransactionNo = entity.TransactionNo;
                            cs.SequenceNo = seqNo;
                            cs.ParticipantPersonID = s.ParticipantPersonID;
                            cs.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            cs.LastUpdateDateTime = DateTime.Now;
                            cs.Save();
                        }
                    }

                    EmployeeSafetyCultureIncidentReportsConslusions.Save();
                    EmployeeSafetyCultureIncidentReportsRecommendations.Save();
                }
                
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeSafetyCultureIncidentReportsQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < this.txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new EmployeeSafetyCultureIncidentReports();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        private void SetReadOnlySafetyCultureIncidentReportsQuestion(bool isReadOnly, DataRow rowQuestion, string questionGroupID)
        {
            string questionID = rowQuestion[EmployeeSafetyCultureIncidentReportsQuestionMetadata.ColumnNames.QuestionID].ToString();
            string controlID = QuestionControlID(rowQuestion["QuestionID"].ToString());
            string answerType = rowQuestion[QuestionMetadata.ColumnNames.SRAnswerType].ToString();
            object obj;

            if (string.IsNullOrEmpty(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()))
                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID);
            else
                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, QuestionControlID(rowQuestion[QuestionMetadata.ColumnNames.ReferenceQuestionID].ToString()));

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

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CDO":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    (obj as RadComboBox).Enabled = !isReadOnly;
                    break;
                case "CTM":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CNM":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    (obj as RadNumericTextBox).ReadOnly = isReadOnly;
                    break;
                case "CDT":
                    (obj as CheckBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    (obj as RadDateTimePicker).DatePopupButton.Enabled = !isReadOnly;
                    (obj as RadDateTimePicker).TimePopupButton.Enabled = !isReadOnly;
                    (obj as RadDateTimePicker).DateInput.ReadOnly = isReadOnly;
                    break;
                case "CBT":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CBN":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    (obj as RadNumericTextBox).ReadOnly = isReadOnly;
                    break;
                case "CBM":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    (obj as RadTextBox).ReadOnly = isReadOnly;
                    break;
                case "CB2":
                    (obj as RadComboBox).Enabled = !isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
                    (obj as RadComboBox).Enabled = !isReadOnly;
                    break;
                case "TTX":
                    (obj as RadTextBox).ReadOnly = isReadOnly;

                    obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                    pnlSafetyCultureIncidentReportsQuestion,
                                    controlID + "_" + r.ToString() + "_" + c.ToString());
                                (objCell as RadTextBox).ReadOnly = isReadOnly;
                            }
                        }
                    }
                    break;
            }
        }

        #endregion

        #region ComboBox
        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );

            query.Where(
                query.SREmployeeStatus == AppSession.Parameter.EmployeeStatusActive,
                query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPersonID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboOrganizationUnitID.Items.Clear();
            cboOrganizationUnitID.SelectedValue = string.Empty;
            cboOrganizationUnitID.Text = string.Empty;

            cboSubOrganizationUnitID.Items.Clear();
            cboSubOrganizationUnitID.SelectedValue = string.Empty;
            cboSubOrganizationUnitID.Text = string.Empty;

            cboSubDivisonID.Items.Clear();
            cboSubDivisonID.SelectedValue = string.Empty;
            cboSubDivisonID.Text = string.Empty;

            cboServiceUnit.Items.Clear();
            cboServiceUnit.SelectedValue = string.Empty;
            cboServiceUnit.Text = string.Empty;

            var emp = new VwEmployeeTable();
            var empq = new VwEmployeeTableQuery();
            empq.Where(empq.PersonID == e.Value.ToInt());
            emp.Load(empq);
            if (emp != null)
            {
                cboSRProfessionType.SelectedValue = emp.SRProfessionType;

                if (emp.OrganizationUnitID > 0)
                {
                    PopulateCboOrganizationUnitID(cboOrganizationUnitID, emp.OrganizationUnitID.ToInt());
                    cboOrganizationUnitID.SelectedValue = emp.OrganizationUnitID.ToString();
                }
                if (emp.SubOrganizationUnitID > 0)
                {
                    PopulateCboOrganizationUnitID(cboSubOrganizationUnitID, emp.SubOrganizationUnitID.ToInt());
                    cboSubOrganizationUnitID.SelectedValue = emp.SubOrganizationUnitID.ToString();
                }
                if (emp.SubDivisonID > 0)
                {
                    PopulateCboOrganizationUnitID(cboSubDivisonID, emp.SubDivisonID.ToInt());
                    cboSubDivisonID.SelectedValue = emp.SubDivisonID.ToString();
                }
                var unitId = emp.ServiceUnitID;
                if (!string.IsNullOrEmpty(unitId))
                {
                    PopulateCboOrganizationUnitID(cboServiceUnit, unitId.ToInt());
                    cboServiceUnit.SelectedValue = unitId;
                }
            }
        }

        private void PopulateCboOrganizationUnitID(RadComboBox comboBox, int textSearch)
        {
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitID == textSearch);

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();

        }

        protected void cboOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }
        #endregion

        #region Record Detail Method Function EmployeeSafetyCultureIncidentReportsSubject

        private EmployeeSafetyCultureIncidentReportsSubjectCollection EmployeeSafetyCultureIncidentReportsSubjects
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeSafetyCultureIncidentReportsSubject" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((EmployeeSafetyCultureIncidentReportsSubjectCollection)(obj));
                    }
                }

                var coll = new EmployeeSafetyCultureIncidentReportsSubjectCollection();
                var query = new EmployeeSafetyCultureIncidentReportsSubjectQuery("a");
                var pinfo = new VwEmployeeTableQuery("b");
                var pt = new AppStandardReferenceItemQuery("c");
                var orga = new OrganizationUnitQuery("d");
                var orgb = new OrganizationUnitQuery("e");
                var orgc = new OrganizationUnitQuery("f");
                var orgd = new OrganizationUnitQuery("g");

                query.Select
                    (
                    query,
                    pinfo.EmployeeName.As("refToPersonalInfo_EmployeeName"),
                    pt.ItemName.As("refToAppStdField_ProfessionType"),
                    orga.OrganizationUnitName.As("refToOrganization_Organization"),
                    orgb.OrganizationUnitName.As("refToOrganization_SubOrganization"),
                    orgc.OrganizationUnitName.As("refToOrganization_SubDivison"),
                    orgd.OrganizationUnitName.As("refToOrganization_ServiceUnit")
                    );
                query.InnerJoin(pinfo).On(pinfo.PersonID == query.SubjectPersonID);
                query.LeftJoin(pt).On
                   (
                       pt.StandardReferenceID == AppEnum.StandardReference.ProfessionType.ToString() &
                       pt.ItemID == query.SubjectSRProfessionType
                   );
                query.LeftJoin(orga).On(orga.OrganizationUnitID == query.SubjectOrganizationID);
                query.LeftJoin(orgb).On(orgb.OrganizationUnitID == query.SubjectSubOrganizationID);
                query.LeftJoin(orgc).On(orgc.OrganizationUnitID == query.SubjectSubDivisonID);
                query.LeftJoin(orgd).On(orgd.OrganizationUnitID == query.SubjectServiceUnitID);

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.IsMainSubject.Descending);

                coll.Load(query);

                Session["collEmployeeSafetyCultureIncidentReportsSubject" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeSafetyCultureIncidentReportsSubject" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemSubject(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && FormType != "ver" && FormType != "con";
            grdSubject.Columns[0].Visible = isVisible;
            grdSubject.Columns[grdSubject.Columns.Count - 1].Visible = isVisible;

            grdSubject.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdSubject.Rebind();
        }

        private void PopulateSubjectGrid()
        {
            //Display Data Detail
            EmployeeSafetyCultureIncidentReportsSubjects = null; //Reset Record Detail
            grdSubject.DataSource = EmployeeSafetyCultureIncidentReportsSubjects; //Requery
            grdSubject.MasterTableView.IsItemInserted = false;
            grdSubject.MasterTableView.ClearEditItems();
            grdSubject.DataBind();
        }

        protected void grdSubject_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSubject.DataSource = EmployeeSafetyCultureIncidentReportsSubjects;
        }

        protected void grdSubject_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            EmployeeSafetyCultureIncidentReportsSubject entity = FindSubjectItem((int)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectPersonID]);

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdSubject_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            int subjectId = item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeSafetyCultureIncidentReportsSubjectMetadata.ColumnNames.SubjectPersonID].ToInt();
            EmployeeSafetyCultureIncidentReportsSubject entity = FindSubjectItem(subjectId);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdSubject_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeSafetyCultureIncidentReportsSubject entity = EmployeeSafetyCultureIncidentReportsSubjects.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdSubject.Rebind();
        }

        private EmployeeSafetyCultureIncidentReportsSubject FindSubjectItem(int subjectId)
        {
            EmployeeSafetyCultureIncidentReportsSubjectCollection coll = EmployeeSafetyCultureIncidentReportsSubjects;
            EmployeeSafetyCultureIncidentReportsSubject retEntity = null;
            foreach (EmployeeSafetyCultureIncidentReportsSubject rec in coll)
            {
                if (rec.SubjectPersonID.Equals(subjectId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(EmployeeSafetyCultureIncidentReportsSubject entity, GridCommandEventArgs e)
        {
            var userControl = (SafetyCultureIncidentReportsSubjectItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.SubjectPersonID = userControl.SubjectPersonID;
                entity.SubjectName = userControl.SubjectName;
                entity.SubjectSRProfessionType = userControl.SubjectSRProfessionType;
                entity.SubjectProfessionTypeName = userControl.SubjectProfessionTypeName;
                entity.SubjectOrganizationID = userControl.SubjectOrganizationID;
                entity.SubjectOrganizationName = userControl.SubjectOrganizationName;
                entity.SubjectSubOrganizationID = userControl.SubjectSubOrganizationID;
                entity.SubjectSubOrganizationName = userControl.SubjectSubOrganizationName;
                entity.SubjectSubDivisonID = userControl.SubjectSubDivisonID;
                entity.SubjectSubDivisonName = userControl.SubjectSubDivisonName;
                entity.SubjectServiceUnitID = userControl.SubjectServiceUnitID;
                entity.SubjectServiceUnitName = userControl.SubjectServiceUnitName;
                entity.IsMainSubject = userControl.IsMainSubject;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeSafetyCultureIncidentReportsWitness

        private EmployeeSafetyCultureIncidentReportsWitnessCollection EmployeeSafetyCultureIncidentReportsWitnesses
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeSafetyCultureIncidentReportsWitness" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((EmployeeSafetyCultureIncidentReportsWitnessCollection)(obj));
                    }
                }

                var coll = new EmployeeSafetyCultureIncidentReportsWitnessCollection();
                var query = new EmployeeSafetyCultureIncidentReportsWitnessQuery("a");
                var pinfo = new VwEmployeeTableQuery("b");
                var pt = new AppStandardReferenceItemQuery("c");
                var orga = new OrganizationUnitQuery("d");
                var orgb = new OrganizationUnitQuery("e");
                var orgc = new OrganizationUnitQuery("f");
                var orgd = new OrganizationUnitQuery("g");

                query.Select
                    (
                    query,
                    pinfo.EmployeeName.As("refToPersonalInfo_EmployeeName"),
                    pt.ItemName.As("refToAppStdField_ProfessionType"),
                    orga.OrganizationUnitName.As("refToOrganization_Organization"),
                    orgb.OrganizationUnitName.As("refToOrganization_SubOrganization"),
                    orgc.OrganizationUnitName.As("refToOrganization_SubDivison"),
                    orgd.OrganizationUnitName.As("refToOrganization_ServiceUnit")
                    );
                query.InnerJoin(pinfo).On(pinfo.PersonID == query.WitnessPersonID);
                query.LeftJoin(pt).On
                   (
                       pt.StandardReferenceID == AppEnum.StandardReference.ProfessionType.ToString() &
                       pt.ItemID == query.WitnessSRProfessionType
                   );
                query.LeftJoin(orga).On(orga.OrganizationUnitID == query.WitnessOrganizationID);
                query.LeftJoin(orgb).On(orgb.OrganizationUnitID == query.WitnessSubOrganizationID);
                query.LeftJoin(orgc).On(orgc.OrganizationUnitID == query.WitnessSubDivisonID);
                query.LeftJoin(orgd).On(orgd.OrganizationUnitID == query.WitnessServiceUnitID);

                query.Where(query.TransactionNo == txtTransactionNo.Text);

                coll.Load(query);

                Session["collEmployeeSafetyCultureIncidentReportsWitness" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeSafetyCultureIncidentReportsWitness" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemWitness(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && FormType != "ver" && FormType != "con";
            grdWitness.Columns[0].Visible = isVisible;
            grdWitness.Columns[grdWitness.Columns.Count - 1].Visible = isVisible;

            grdWitness.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdWitness.Rebind();
        }

        private void PopulateWitnessGrid()
        {
            //Display Data Detail
            EmployeeSafetyCultureIncidentReportsWitnesses = null; //Reset Record Detail
            grdWitness.DataSource = EmployeeSafetyCultureIncidentReportsWitnesses; //Requery
            grdWitness.MasterTableView.IsItemInserted = false;
            grdWitness.MasterTableView.ClearEditItems();
            grdWitness.DataBind();
        }

        protected void grdWitness_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdWitness.DataSource = EmployeeSafetyCultureIncidentReportsWitnesses;
        }

        protected void grdWitness_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            EmployeeSafetyCultureIncidentReportsWitness entity = FindWitnessItem((int)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessPersonID]);

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdWitness_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            int witnessId = item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessPersonID].ToInt();
            EmployeeSafetyCultureIncidentReportsWitness entity = FindWitnessItem(witnessId);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdWitness_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeSafetyCultureIncidentReportsWitness entity = EmployeeSafetyCultureIncidentReportsWitnesses.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdWitness.Rebind();
        }

        private EmployeeSafetyCultureIncidentReportsWitness FindWitnessItem(int witnessId)
        {
            EmployeeSafetyCultureIncidentReportsWitnessCollection coll = EmployeeSafetyCultureIncidentReportsWitnesses;
            EmployeeSafetyCultureIncidentReportsWitness retEntity = null;
            foreach (EmployeeSafetyCultureIncidentReportsWitness rec in coll)
            {
                if (rec.WitnessPersonID.Equals(witnessId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(EmployeeSafetyCultureIncidentReportsWitness entity, GridCommandEventArgs e)
        {
            var userControl = (SafetyCultureIncidentReportsWitnessItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.WitnessPersonID = userControl.WitnessPersonID;
                entity.WitnessName = userControl.WitnessName;
                entity.WitnessSRProfessionType = userControl.WitnessSRProfessionType;
                entity.WitnessProfessionTypeName = userControl.WitnessProfessionTypeName;
                entity.WitnessOrganizationID = userControl.WitnessOrganizationID;
                entity.WitnessOrganizationName = userControl.WitnessOrganizationName;
                entity.WitnessSubOrganizationID = userControl.WitnessSubOrganizationID;
                entity.WitnessSubOrganizationName = userControl.WitnessSubOrganizationName;
                entity.WitnessSubDivisonID = userControl.WitnessSubDivisonID;
                entity.WitnessSubDivisonName = userControl.WitnessSubDivisonName;
                entity.WitnessServiceUnitID = userControl.WitnessServiceUnitID;
                entity.WitnessServiceUnitName = userControl.WitnessServiceUnitName;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeSafetyCultureIncidentReportsChronology

        private EmployeeSafetyCultureIncidentReportsChronologyCollection EmployeeSafetyCultureIncidentReportsChronologys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeSafetyCultureIncidentReportsChronology" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((EmployeeSafetyCultureIncidentReportsChronologyCollection)(obj));
                    }
                }

                var coll = new EmployeeSafetyCultureIncidentReportsChronologyCollection();
                var query = new EmployeeSafetyCultureIncidentReportsChronologyQuery("a");

                query.Select
                    (
                    query,
                    "<[dbo].[fn_EmployeeSafetyCultureIncidentReportsChronologySubjects](a.TransactionNo,a.SequenceNo) AS refToEmployeeSafetyCultureIncidentReportsChronologySubject_Subjects>"
                    );

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.ChronologyDateTime.Ascending);

                coll.Load(query);

                Session["collEmployeeSafetyCultureIncidentReportsChronology" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeSafetyCultureIncidentReportsChronology" + Request.UserHostName] = value; }
        }

        private EmployeeSafetyCultureIncidentReportsChronologySubjectCollection EmployeeSafetyCultureIncidentReportsChronologySubjects
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collEmployeeSafetyCultureIncidentReportsChronologySubject" + Request.UserHostName];
                    if (obj != null) return ((EmployeeSafetyCultureIncidentReportsChronologySubjectCollection)(obj));
                }

                var coll = new EmployeeSafetyCultureIncidentReportsChronologySubjectCollection();

                var query = new EmployeeSafetyCultureIncidentReportsChronologySubjectQuery("a");
                var emp = new VwEmployeeTableQuery("b");

                query.Select(query,
                    emp.EmployeeName.As("refToPersonalInfo_EmployeeName")
                    );
                query.LeftJoin(emp).On(query.SubjectPersonID == emp.PersonID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (EmployeeSafetyCultureIncidentReportsChronologys.Any())
                    query.Where(query.SequenceNo.In(EmployeeSafetyCultureIncidentReportsChronologys.Select(p => p.SequenceNo)));
                else
                    query.Where(query.SequenceNo == "x");
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["collEmployeeSafetyCultureIncidentReportsChronologySubject" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeSafetyCultureIncidentReportsChronologySubject" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemChronology(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && FormType != "ver" && FormType != "con";
            grdChronology.Columns[0].Visible = isVisible;
            grdChronology.Columns[grdChronology.Columns.Count - 1].Visible = isVisible;

            grdChronology.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdChronology.Rebind();
        }

        private void PopulateChronologyGrid()
        {
            //Display Data Detail
            EmployeeSafetyCultureIncidentReportsChronologys = null; //Reset Record Detail
            grdChronology.DataSource = EmployeeSafetyCultureIncidentReportsChronologys; //Requery
            grdChronology.MasterTableView.IsItemInserted = false;
            grdChronology.MasterTableView.ClearEditItems();
            grdChronology.DataBind();

            EmployeeSafetyCultureIncidentReportsChronologySubjects = null;
            var pq = EmployeeSafetyCultureIncidentReportsChronologySubjects;
        }

        protected void grdChronology_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdChronology.DataSource = EmployeeSafetyCultureIncidentReportsChronologys;
        }

        protected void grdChronology_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeSafetyCultureIncidentReportsChronologyMetadata.ColumnNames.SequenceNo]);
            var entity = FindChronologyItem(id);
            if (entity != null)
            {
                SetEntityValue(entity, e);
            }
        }

        protected void grdChronology_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeSafetyCultureIncidentReportsChronologyMetadata.ColumnNames.SequenceNo]);

            EmployeeSafetyCultureIncidentReportsChronology entity = FindChronologyItem(id);
            if (entity != null)
            {
                var cs = EmployeeSafetyCultureIncidentReportsChronologySubjects.Where(pq => pq.SequenceNo == entity.SequenceNo);
                foreach (var s in EmployeeSafetyCultureIncidentReportsChronologySubjects)
                {
                    s.MarkAsDeleted();
                }
                EmployeeSafetyCultureIncidentReportsChronologySubjects.Save();

                entity.MarkAsDeleted();
                EmployeeSafetyCultureIncidentReportsChronologys.Save();
            }
        }

        protected void grdChronology_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeSafetyCultureIncidentReportsChronology entity = EmployeeSafetyCultureIncidentReportsChronologys.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdChronology.Rebind();
        }

        private EmployeeSafetyCultureIncidentReportsChronology FindChronologyItem(string id)
        {
            return EmployeeSafetyCultureIncidentReportsChronologys.FirstOrDefault(rec => rec.SequenceNo.Equals(id));
        }

        private void SetEntityValue(EmployeeSafetyCultureIncidentReportsChronology entity, GridCommandEventArgs e)
        {
            var userControl = (SafetyCultureIncidentReportsChronologyItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.ChronologyDateTime = userControl.ChronologyDateTime;
                entity.ChronologyDescription = userControl.ChronologyDescription;
                entity.Subjects = userControl.Subjects;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeSafetyCultureIncidentReportsParticipant

        private EmployeeSafetyCultureIncidentReportsParticipantCollection EmployeeSafetyCultureIncidentReportsParticipants
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeSafetyCultureIncidentReportsParticipant" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((EmployeeSafetyCultureIncidentReportsParticipantCollection)(obj));
                    }
                }

                var coll = new EmployeeSafetyCultureIncidentReportsParticipantCollection();
                var query = new EmployeeSafetyCultureIncidentReportsParticipantQuery("a");
                var pinfo = new VwEmployeeTableQuery("b");
                var pt = new AppStandardReferenceItemQuery("c");
                
                query.Select
                    (
                    query,
                    pinfo.EmployeeName.As("refToPersonalInfo_EmployeeName"),
                    pt.ItemName.As("refToAppStdRef_ParticipantStatus")
                    );
                query.InnerJoin(pinfo).On(pinfo.PersonID == query.ParticipantPersonID);
                query.LeftJoin(pt).On
                   (
                       pt.StandardReferenceID == AppEnum.StandardReference.ParticipantStatus.ToString() &
                       pt.ItemID == query.SRParticipantStatus
                   );

                query.Where(query.TransactionNo == txtTransactionNo.Text);

                coll.Load(query);

                Session["collEmployeeSafetyCultureIncidentReportsParticipant" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeSafetyCultureIncidentReportsParticipant" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemParticipant(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read) && FormType == "ver";
            grdParticipant.Columns[0].Visible = isVisible;
            grdParticipant.Columns[grdParticipant.Columns.Count - 1].Visible = isVisible;

            grdParticipant.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdParticipant.Rebind();
        }

        private void PopulateParticipantGrid()
        {
            //Display Data Detail
            EmployeeSafetyCultureIncidentReportsParticipants = null; //Reset Record Detail
            grdParticipant.DataSource = EmployeeSafetyCultureIncidentReportsParticipants; //Requery
            grdParticipant.MasterTableView.IsItemInserted = false;
            grdParticipant.MasterTableView.ClearEditItems();
            grdParticipant.DataBind();
        }

        protected void grdParticipant_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdParticipant.DataSource = EmployeeSafetyCultureIncidentReportsParticipants;
        }

        protected void grdParticipant_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            EmployeeSafetyCultureIncidentReportsParticipant entity = FindParticipantItem((int)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [EmployeeSafetyCultureIncidentReportsParticipantMetadata.ColumnNames.ParticipantPersonID]);

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdParticipant_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            int id = item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeSafetyCultureIncidentReportsParticipantMetadata.ColumnNames.ParticipantPersonID].ToInt();
            EmployeeSafetyCultureIncidentReportsParticipant entity = FindParticipantItem(id);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdParticipant_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeSafetyCultureIncidentReportsParticipant entity = EmployeeSafetyCultureIncidentReportsParticipants.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdParticipant.Rebind();
        }

        private EmployeeSafetyCultureIncidentReportsParticipant FindParticipantItem(int id)
        {
            EmployeeSafetyCultureIncidentReportsParticipantCollection coll = EmployeeSafetyCultureIncidentReportsParticipants;
            EmployeeSafetyCultureIncidentReportsParticipant retEntity = null;
            foreach (EmployeeSafetyCultureIncidentReportsParticipant rec in coll)
            {
                if (rec.ParticipantPersonID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(EmployeeSafetyCultureIncidentReportsParticipant entity, GridCommandEventArgs e)
        {
            var userControl = (SafetyCultureIncidentReportsParticipantItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.ParticipantPersonID = userControl.ParticipantPersonID;
                entity.ParticipantName = userControl.ParticipantName;
                entity.SRParticipantStatus = userControl.SRParticipantStatus;
                entity.ParticipantStatusName = userControl.ParticipantStatusName;
                entity.Notes = userControl.Notes;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeSafetyCultureIncidentReportsChronology

        private EmployeeSafetyCultureIncidentReportsMeetingCollection EmployeeSafetyCultureIncidentReportsMeetings
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeSafetyCultureIncidentReportsMeeting" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((EmployeeSafetyCultureIncidentReportsMeetingCollection)(obj));
                    }
                }

                var coll = new EmployeeSafetyCultureIncidentReportsMeetingCollection();
                var query = new EmployeeSafetyCultureIncidentReportsMeetingQuery("a");

                query.Select
                    (
                    query,
                    "<[dbo].[fn_EmployeeSafetyCultureIncidentReportsMeetingParticipants](a.TransactionNo,a.SequenceNo) AS refToEmployeeSafetyCultureIncidentReportsMeetingParticipant_Participants>"
                    );

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.MeetingDateTime.Ascending);

                coll.Load(query);

                Session["collEmployeeSafetyCultureIncidentReportsMeeting" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeSafetyCultureIncidentReportsMeeting" + Request.UserHostName] = value; }
        }

        private EmployeeSafetyCultureIncidentReportsMeetingParticipantCollection EmployeeSafetyCultureIncidentReportsMeetingParticipants
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collEmployeeSafetyCultureIncidentReportsMeetingParticipant" + Request.UserHostName];
                    if (obj != null) return ((EmployeeSafetyCultureIncidentReportsMeetingParticipantCollection)(obj));
                }

                var coll = new EmployeeSafetyCultureIncidentReportsMeetingParticipantCollection();

                var query = new EmployeeSafetyCultureIncidentReportsMeetingParticipantQuery("a");
                var emp = new VwEmployeeTableQuery("b");

                query.Select(query,
                    emp.EmployeeName.As("refToPersonalInfo_EmployeeName")
                    );
                query.LeftJoin(emp).On(query.ParticipantPersonID == emp.PersonID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                if (EmployeeSafetyCultureIncidentReportsMeetings.Any())
                    query.Where(query.SequenceNo.In(EmployeeSafetyCultureIncidentReportsMeetings.Select(p => p.SequenceNo)));
                else
                    query.Where(query.SequenceNo == "x");
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["collEmployeeSafetyCultureIncidentReportsMeetingParticipant" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeSafetyCultureIncidentReportsMeetingParticipant" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemMeeting(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdMeeting.Columns[0].Visible = isVisible;
            grdMeeting.Columns[grdMeeting.Columns.Count - 1].Visible = isVisible;

            grdMeeting.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdMeeting.Rebind();
        }

        private void PopulateMeetingGrid()
        {
            //Display Data Detail
            EmployeeSafetyCultureIncidentReportsMeetings = null; //Reset Record Detail
            grdMeeting.DataSource = EmployeeSafetyCultureIncidentReportsMeetings; //Requery
            grdMeeting.MasterTableView.IsItemInserted = false;
            grdMeeting.MasterTableView.ClearEditItems();
            grdMeeting.DataBind();

            EmployeeSafetyCultureIncidentReportsMeetingParticipants = null;
            var pq = EmployeeSafetyCultureIncidentReportsMeetingParticipants;
        }

        protected void grdMeeting_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdMeeting.DataSource = EmployeeSafetyCultureIncidentReportsMeetings;
        }

        protected void grdMeeting_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            var id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeSafetyCultureIncidentReportsMeetingMetadata.ColumnNames.SequenceNo]);
            var entity = FindMeetingItem(id);
            if (entity != null)
            {
                SetEntityValue(entity, e);
            }
        }

        protected void grdMeeting_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeSafetyCultureIncidentReportsMeetingMetadata.ColumnNames.SequenceNo]);

            EmployeeSafetyCultureIncidentReportsMeeting entity = FindMeetingItem(id);
            if (entity != null)
            {
                var cs = EmployeeSafetyCultureIncidentReportsMeetingParticipants.Where(pq => pq.SequenceNo == entity.SequenceNo);
                foreach (var s in EmployeeSafetyCultureIncidentReportsMeetingParticipants)
                {
                    s.MarkAsDeleted();
                }
                EmployeeSafetyCultureIncidentReportsMeetingParticipants.Save();

                entity.MarkAsDeleted();
                EmployeeSafetyCultureIncidentReportsMeetings.Save();
            }
        }

        protected void grdMeeting_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeSafetyCultureIncidentReportsMeeting entity = EmployeeSafetyCultureIncidentReportsMeetings.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdMeeting.Rebind();
        }

        private EmployeeSafetyCultureIncidentReportsMeeting FindMeetingItem(string id)
        {
            return EmployeeSafetyCultureIncidentReportsMeetings.FirstOrDefault(rec => rec.SequenceNo.Equals(id));
        }

        private void SetEntityValue(EmployeeSafetyCultureIncidentReportsMeeting entity, GridCommandEventArgs e)
        {
            var userControl = (SafetyCultureIncidentReportsMeetingItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.MeetingDateTime = userControl.MeetingDateTime;
                entity.MeetingSummary = userControl.MeetingSummary;
                entity.Participants = userControl.Participants;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeSafetyCultureIncidentReportsConslusion

        private EmployeeSafetyCultureIncidentReportsConslusionCollection EmployeeSafetyCultureIncidentReportsConslusions
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeSafetyCultureIncidentReportsConslusion" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((EmployeeSafetyCultureIncidentReportsConslusionCollection)(obj));
                    }
                }

                var coll = new EmployeeSafetyCultureIncidentReportsConslusionCollection();
                var query = new EmployeeSafetyCultureIncidentReportsConslusionQuery("a");

                query.Select
                    (
                    query
                    );

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["collEmployeeSafetyCultureIncidentReportsConslusion" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeSafetyCultureIncidentReportsConslusion" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemConslusion(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdConslusion.Columns[0].Visible = isVisible;
            grdConslusion.Columns[grdConslusion.Columns.Count - 1].Visible = isVisible;

            grdConslusion.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdConslusion.Rebind();
        }

        private void PopulateConslusionGrid()
        {
            //Display Data Detail
            EmployeeSafetyCultureIncidentReportsConslusions = null; //Reset Record Detail
            grdConslusion.DataSource = EmployeeSafetyCultureIncidentReportsConslusions; //Requery
            grdConslusion.MasterTableView.IsItemInserted = false;
            grdConslusion.MasterTableView.ClearEditItems();
            grdConslusion.DataBind();
        }

        protected void grdConslusion_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdConslusion.DataSource = EmployeeSafetyCultureIncidentReportsConslusions;
        }

        protected void grdConslusion_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            EmployeeSafetyCultureIncidentReportsConslusion entity = FindConslusionItem(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [EmployeeSafetyCultureIncidentReportsConslusionMetadata.ColumnNames.SequenceNo].ToString());

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdConslusion_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            string seqNo = item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeSafetyCultureIncidentReportsConslusionMetadata.ColumnNames.SequenceNo].ToString();
            EmployeeSafetyCultureIncidentReportsConslusion entity = FindConslusionItem(seqNo);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdConslusion_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeSafetyCultureIncidentReportsConslusion entity = EmployeeSafetyCultureIncidentReportsConslusions.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdConslusion.Rebind();
        }

        private EmployeeSafetyCultureIncidentReportsConslusion FindConslusionItem(string seqNo)
        {
            EmployeeSafetyCultureIncidentReportsConslusionCollection coll = EmployeeSafetyCultureIncidentReportsConslusions;
            EmployeeSafetyCultureIncidentReportsConslusion retEntity = null;
            foreach (EmployeeSafetyCultureIncidentReportsConslusion rec in coll)
            {
                if (rec.SequenceNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(EmployeeSafetyCultureIncidentReportsConslusion entity, GridCommandEventArgs e)
        {
            var userControl = (SafetyCultureIncidentReportsConslusionItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.Conclusion = userControl.Conclusion;
            }
        }

        #endregion

        #region Record Detail Method Function EmployeeSafetyCultureIncidentReportsRecommendation

        private EmployeeSafetyCultureIncidentReportsRecommendationCollection EmployeeSafetyCultureIncidentReportsRecommendations
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeSafetyCultureIncidentReportsRecommendation" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((EmployeeSafetyCultureIncidentReportsRecommendationCollection)(obj));
                    }
                }

                var coll = new EmployeeSafetyCultureIncidentReportsRecommendationCollection();
                var query = new EmployeeSafetyCultureIncidentReportsRecommendationQuery("a");

                query.Select
                    (
                    query
                    );

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["collEmployeeSafetyCultureIncidentReportsRecommendation" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collEmployeeSafetyCultureIncidentReportsRecommendation" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemRecommendation(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdRecommendation.Columns[0].Visible = isVisible;
            grdRecommendation.Columns[grdRecommendation.Columns.Count - 1].Visible = isVisible;

            grdRecommendation.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdRecommendation.Rebind();
        }

        private void PopulateRecommendationGrid()
        {
            //Display Data Detail
            EmployeeSafetyCultureIncidentReportsRecommendations = null; //Reset Record Detail
            grdRecommendation.DataSource = EmployeeSafetyCultureIncidentReportsRecommendations; //Requery
            grdRecommendation.MasterTableView.IsItemInserted = false;
            grdRecommendation.MasterTableView.ClearEditItems();
            grdRecommendation.DataBind();
        }

        protected void grdRecommendation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRecommendation.DataSource = EmployeeSafetyCultureIncidentReportsRecommendations;
        }

        protected void grdRecommendation_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            EmployeeSafetyCultureIncidentReportsRecommendation entity = FindRecommendationItem(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]
                                                  [EmployeeSafetyCultureIncidentReportsRecommendationMetadata.ColumnNames.SequenceNo].ToString());

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRecommendation_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            string seqNo = item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeSafetyCultureIncidentReportsRecommendationMetadata.ColumnNames.SequenceNo].ToString();
            EmployeeSafetyCultureIncidentReportsRecommendation entity = FindRecommendationItem(seqNo);

            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRecommendation_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeSafetyCultureIncidentReportsRecommendation entity = EmployeeSafetyCultureIncidentReportsRecommendations.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdRecommendation.Rebind();
        }

        private EmployeeSafetyCultureIncidentReportsRecommendation FindRecommendationItem(string seqNo)
        {
            EmployeeSafetyCultureIncidentReportsRecommendationCollection coll = EmployeeSafetyCultureIncidentReportsRecommendations;
            EmployeeSafetyCultureIncidentReportsRecommendation retEntity = null;
            foreach (EmployeeSafetyCultureIncidentReportsRecommendation rec in coll)
            {
                if (rec.SequenceNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(EmployeeSafetyCultureIncidentReportsRecommendation entity, GridCommandEventArgs e)
        {
            var userControl = (SafetyCultureIncidentReportsRecommendationItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = txtTransactionNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.Recommendation = userControl.Recommendation;
            }
        }

        #endregion

        #region PatientHealthRecordLine

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
            var query = new EmployeeSafetyCultureIncidentReportsQuestionQuery("a");
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
                    query.TransactionNo == txtTransactionNo.Text,
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


                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID);
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
                                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                obj = Helper.FindControlRecursive(pnlSafetyCultureIncidentReportsQuestion, controlID + "_2");
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
                                            pnlSafetyCultureIncidentReportsQuestion,
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
                                            pnlSafetyCultureIncidentReportsQuestion,
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
            pnlSafetyCultureIncidentReportsQuestion.Controls.Clear();
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
                pnlSafetyCultureIncidentReportsQuestion.Controls.Add(groupTable);
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
            pnlSafetyCultureIncidentReportsQuestion.Controls.Clear();
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
                pnlSafetyCultureIncidentReportsQuestion.Controls.Add(groupTable);
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
    }
}