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

namespace Temiang.Avicenna.Module.HR.CPA
{
    public partial class ScoresheetDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.ClinicalAppraisalNo);
            return _autoNumber.LastCompleteNumber;
        }

        private string personId
        {
            get { return string.IsNullOrEmpty(Request.QueryString["pid"]) ? string.Empty : Request.QueryString["pid"]; }
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ClinicalPerformanceAppraisalScoresheet;

            UrlPageList = "ScoresheetList.aspx";
            UrlPageSearch = "ScoresheetSearch.aspx";

            WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);
                StandardReference.InitializeIncludeSpace(cboSRProfessionGroup, AppEnum.StandardReference.ProfessionGroup);
                StandardReference.InitializeIncludeSpace(cboSRClinicalWorkArea, AppEnum.StandardReference.ClinicalWorkArea);
                StandardReference.InitializeIncludeSpace(cboSRClinicalAuthorityLevel, AppEnum.StandardReference.ClinicalAuthorityLevel);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            cboEvaluatorID.Enabled = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboPersonID, cboPersonID);
            ajax.AddAjaxSetting(cboPersonID, txtEmployeeNo);
            ajax.AddAjaxSetting(cboPersonID, txtPlaceDOB);
            ajax.AddAjaxSetting(cboPersonID, cboSREmploymentType);
            ajax.AddAjaxSetting(cboPersonID, txtREmploymentPermanentDate);
            ajax.AddAjaxSetting(cboPersonID, cboServiceUnitID);
            ajax.AddAjaxSetting(cboPersonID, cboPositionID);
            ajax.AddAjaxSetting(cboPersonID, cboSRProfessionGroup);
            ajax.AddAjaxSetting(cboPersonID, cboSRClinicalWorkArea);
            ajax.AddAjaxSetting(cboPersonID, cboSRClinicalAuthorityLevel);
            ajax.AddAjaxSetting(cboPersonID, cboQuestionnaireID);
            ajax.AddAjaxSetting(cboPersonID, grdSheet);

            ajax.AddAjaxSetting(cboQuestionnaireID, cboQuestionnaireID);
            ajax.AddAjaxSetting(cboQuestionnaireID, grdSheet);

            ajax.AddAjaxSetting(grdSheet, txtTotal);
            ajax.AddAjaxSetting(grdSheet, lblConclusionGrade);
            ajax.AddAjaxSetting(grdSheet, lblConclusionGradeName);
            ajax.AddAjaxSetting(grdSheet, lblConclusionSeparate);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ClinicalPerformanceAppraisalQuestionnaireScoresheet());

            txtScoresheetNo.Text = GetNewTransactionNo();

            txtScoringDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            var personal = new VwEmployeeTableQuery();
            personal.Where(personal.PersonID == Convert.ToInt32(AppSession.UserLogin.PersonID));
            var dtb = personal.LoadDataTable();
            cboEvaluatorID.DataSource = dtb;
            cboEvaluatorID.DataBind();
            if (dtb.Rows.Count > 0)
            {
                cboEvaluatorID.SelectedValue = AppSession.UserLogin.PersonID.ToString();
                cboEvaluatorID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }

            cboSRProfessionGroup.SelectedValue = string.Empty;
            cboSRProfessionGroup.Text = string.Empty;
            cboSRClinicalWorkArea.SelectedValue = string.Empty;
            cboSRClinicalWorkArea.Text = string.Empty;
            cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
            cboSRClinicalAuthorityLevel.Text = string.Empty;

            grdSheet.DataSource = ClinicalPerformanceAppraisalQuestionnaireScoresheetItems; //Requery
            grdSheet.DataBind();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ClinicalPerformanceAppraisalQuestionnaireScoresheet();
            if (entity.LoadByPrimaryKey(txtScoresheetNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;

                var collValue = new ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection();
                collValue.Query.Where(collValue.Query.ScoresheetNo == txtScoresheetNo.Text);
                collValue.LoadAll();
                collValue.MarkAllAsDeleted();

                entity.MarkAsDeleted();

                using (var trans = new esTransactionScope())
                {
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
            var entity = new ClinicalPerformanceAppraisalQuestionnaireScoresheet();
            if (entity.LoadByPrimaryKey(txtScoresheetNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new ClinicalPerformanceAppraisalQuestionnaireScoresheet();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new ClinicalPerformanceAppraisalQuestionnaireScoresheet();
            if (entity.LoadByPrimaryKey(txtScoresheetNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
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
            auditLogFilter.PrimaryKeyData = string.Format("ScoresheetNo='{0}'", txtScoresheetNo.Text.Trim());
            auditLogFilter.TableName = "ClinicalPerformanceAppraisalQuestionnaireScoresheet";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_ScoresheetNo", txtScoresheetNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new ClinicalPerformanceAppraisalQuestionnaireScoresheet();
            entity.LoadByPrimaryKey(txtScoresheetNo.Text);

            using (var trans = new esTransactionScope())
            {
                entity.TotalScore = Convert.ToInt16(txtTotal.Value);
                entity.ConclusionGrade = lblConclusionGrade.Text;
                entity.ConclusionGradeName = lblConclusionGradeName.Text;

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
            var entity = new ClinicalPerformanceAppraisalQuestionnaireScoresheet();
            entity.LoadByPrimaryKey(txtScoresheetNo.Text);

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
            var entity = new ClinicalPerformanceAppraisalQuestionnaireScoresheet();
            if (!entity.LoadByPrimaryKey(txtScoresheetNo.Text))
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

        private void SetVoid(ClinicalPerformanceAppraisalQuestionnaireScoresheet entity, bool isVoid)
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

        private bool IsApprovedOrVoid(ClinicalPerformanceAppraisalQuestionnaireScoresheet entity, ValidateArgs args)
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

        private bool IsApproved(ClinicalPerformanceAppraisalQuestionnaireScoresheet entity, ValidateArgs args)
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
            bool isVisible = (newVal != AppEnum.DataMode.Read);

            cboEvaluatorID.Enabled = false;

            grdSheet.Columns.FindByUniqueName("txtScore").Visible = isVisible;
            grdSheet.Columns.FindByUniqueName("Score").Visible = !isVisible;

            RefreshGridSheetItems();
            CalcultedScore();
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ClinicalPerformanceAppraisalQuestionnaireScoresheet();
            if (entity.LoadByPrimaryKey(txtScoresheetNo.Text))
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
            var entity = new ClinicalPerformanceAppraisalQuestionnaireScoresheet();
            entity.LoadByPrimaryKey(txtScoresheetNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ClinicalPerformanceAppraisalQuestionnaireScoresheet();

            entity.LoadByPrimaryKey(string.IsNullOrEmpty(txtScoresheetNo.Text) ? Request.QueryString["id"] : txtScoresheetNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var cp = (ClinicalPerformanceAppraisalQuestionnaireScoresheet)entity;

            txtScoresheetNo.Text = cp.ScoresheetNo;
            if (cp.ScoringDate.HasValue)
                txtScoringDate.SelectedDate = cp.ScoringDate;

            if (cp.EvaluatorID.HasValue && cp.EvaluatorID.ToInt() > -1)
            {
                var query = new VwEmployeeTableQuery();
                query.Select
                    (
                        query.PersonID,
                        query.EmployeeNumber,
                        query.EmployeeName
                    );

                query.Where(query.PersonID == cp.EvaluatorID.ToInt());

                cboEvaluatorID.DataSource = query.LoadDataTable();
                cboEvaluatorID.DataBind();

                cboEvaluatorID.SelectedValue = cp.EvaluatorID.ToString();
            }
            else
            {
                cboEvaluatorID.Items.Clear();
                cboEvaluatorID.SelectedValue = string.Empty;
                cboEvaluatorID.Text = string.Empty;
            }

            if (cp.PersonID.HasValue && cp.PersonID.ToInt() > -1)
            {
                var query = new VwEmployeeTableQuery();
                query.Select
                    (
                        query.PersonID,
                        query.EmployeeNumber,
                        query.EmployeeName
                    );

                query.Where(query.PersonID == cp.PersonID.ToInt());

                cboPersonID.DataSource = query.LoadDataTable();
                cboPersonID.DataBind();

                cboPersonID.SelectedValue = cp.PersonID.ToString();

                var employeeInfo = new VwEmployeeTable();
                var employeeInfoQ = new VwEmployeeTableQuery();
                employeeInfoQ.es.Top = 1;
                employeeInfoQ.Where(employeeInfoQ.PersonID == cp.PersonID.ToInt());
                employeeInfo.Load(employeeInfoQ);
                if (employeeInfo != null)
                {
                    txtEmployeeNo.Text = employeeInfo.EmployeeNumber;
                    txtPlaceDOB.Text = string.Format("{0}, {1}", employeeInfo.PlaceBirth, Convert.ToDateTime(employeeInfo.BirthDate).ToString("dd-MMM-yyyy"));

                    cboSREmploymentType.SelectedValue = employeeInfo.SREmploymentType;

                    if (cboSREmploymentType.SelectedValue == AppSession.Parameter.EmploymentTypePermanent)
                        txtREmploymentPermanentDate.SelectedDate = employeeInfo.TglDiangkat;
                    else
                        txtREmploymentPermanentDate.Clear();


                    var org = new OrganizationUnitQuery();
                    org.Where(org.OrganizationUnitID == employeeInfo.ServiceUnitID.ToInt());
                    var orgDtb = org.LoadDataTable();
                    if (orgDtb.Rows.Count > 0)
                    {
                        cboServiceUnitID.DataSource = orgDtb;
                        cboServiceUnitID.DataBind();
                        cboServiceUnitID.SelectedValue = employeeInfo.ServiceUnitID;
                    }
                    else
                    {
                        cboServiceUnitID.Items.Clear();
                        cboServiceUnitID.SelectedValue = string.Empty;
                        cboServiceUnitID.Text = string.Empty;
                    }

                    var pos = new PositionQuery();
                    pos.Where(pos.PositionID == employeeInfo.PositionID.ToInt());
                    var posDtb = pos.LoadDataTable();
                    if (posDtb.Rows.Count > 0)
                    {
                        cboPositionID.DataSource = posDtb;
                        cboPositionID.DataBind();
                        cboPositionID.SelectedValue = employeeInfo.PositionID.ToString();
                    }
                    else
                    {
                        cboPositionID.Items.Clear();
                        cboPositionID.SelectedValue = string.Empty;
                        cboPositionID.Text = string.Empty;
                    }
                }
                else
                {
                    txtEmployeeNo.Text = string.Empty;
                    txtPlaceDOB.Text = string.Empty;
                    cboSREmploymentType.SelectedValue = string.Empty;
                    cboSREmploymentType.Text = string.Empty;
                    txtREmploymentPermanentDate.Clear();
                }
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.SelectedValue = string.Empty;
                cboPersonID.Text = string.Empty;

                txtEmployeeNo.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;

                cboSREmploymentType.SelectedValue = string.Empty;
                cboSREmploymentType.Text = string.Empty;
                txtREmploymentPermanentDate.Clear();

                cboServiceUnitID.Items.Clear();
                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;

                cboPositionID.Items.Clear();
                cboPositionID.SelectedValue = string.Empty;
                cboPositionID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(cp.SRProfessionGroup))
                cboSRProfessionGroup.SelectedValue = cp.SRProfessionGroup;
            else
            {
                cboSRProfessionGroup.SelectedValue = string.Empty;
                cboSRProfessionGroup.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(cp.SRClinicalWorkArea))
            {
                cboSRClinicalWorkArea.SelectedValue = cp.SRClinicalWorkArea;
            }
            else
            {
                cboSRClinicalWorkArea.SelectedValue = string.Empty;
                cboSRClinicalWorkArea.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(cp.SRClinicalAuthorityLevel))
            {
                cboSRClinicalAuthorityLevel.SelectedValue = cp.SRClinicalAuthorityLevel;
            }
            else
            {
                cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
                cboSRClinicalAuthorityLevel.Text = string.Empty;
            }

            if (cp.QuestionnaireID.HasValue)
            {
                var cqq = new ClinicalPerformanceAppraisalQuestionnaireQuery();
                cqq.Where(cqq.QuestionnaireID == cp.QuestionnaireID);
                cboQuestionnaireID.DataSource = cqq.LoadDataTable();
                cboQuestionnaireID.DataBind();
                cboQuestionnaireID.SelectedValue = cp.QuestionnaireID.ToString();
            }
            else
            {
                cboQuestionnaireID.Items.Clear();
                cboQuestionnaireID.SelectedValue = string.Empty;
                cboQuestionnaireID.Text = string.Empty;
            }

            txtTotal.Value = Convert.ToDouble(cp.TotalScore);
            lblConclusionGrade.Text = cp.ConclusionGrade;
            lblConclusionGradeName.Text = cp.ConclusionGradeName;
            if (!string.IsNullOrEmpty(cp.ConclusionGrade))
                lblConclusionSeparate.Text = "-";
            else lblConclusionSeparate.Text = string.Empty;

            txtConclusionNotes.Text = cp.ConclusionNotes;

            chkIsApproved.Checked = cp.IsApproved ?? false;
            chkIsVoid.Checked = cp.IsVoid ?? false;

            if (IsPostBack)
            {
                RefreshGridSheetItems();
                CalcultedScore();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(ClinicalPerformanceAppraisalQuestionnaireScoresheet entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtScoresheetNo.Text = GetNewTransactionNo();
                _autoNumber.Save();
            }

            entity.ScoresheetNo = txtScoresheetNo.Text;
            entity.ScoringDate = txtScoringDate.SelectedDate;
            entity.EvaluatorID = cboEvaluatorID.SelectedValue.ToInt();
            entity.PersonID = cboPersonID.SelectedValue.ToInt();
            entity.SRProfessionGroup = cboSRProfessionGroup.SelectedValue;
            entity.SRClinicalWorkArea = cboSRClinicalWorkArea.SelectedValue;
            entity.SRClinicalAuthorityLevel = cboSRClinicalAuthorityLevel.SelectedValue;
            entity.QuestionnaireID = cboQuestionnaireID.SelectedValue.ToInt();
            entity.TotalScore = Convert.ToInt16(txtTotal.Value);
            entity.ConclusionGrade = lblConclusionGrade.Text;
            entity.ConclusionGradeName = lblConclusionGradeName.Text;
            entity.ConclusionNotes = txtConclusionNotes.Text;

            entity.IsVoid = chkIsVoid.Checked;
            entity.IsApproved = chkIsApproved.Checked;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(ClinicalPerformanceAppraisalQuestionnaireScoresheet entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                foreach (GridDataItem dataItem in grdSheet.MasterTableView.Items)
                {
                    string questionnaireItemId = dataItem.GetDataKeyValue("QuestionnaireItemID").ToString();
                    Int16 score = Convert.ToInt16(((RadNumericTextBox)dataItem.FindControl("txtScore")).Value);

                    var item = new ClinicalPerformanceAppraisalQuestionnaireScoresheetItem();
                    item.Query.Where(item.Query.ScoresheetNo == entity.ScoresheetNo, item.Query.QuestionnaireItemID == questionnaireItemId);
                    if (!item.Query.Load())
                    {
                        item = new ClinicalPerformanceAppraisalQuestionnaireScoresheetItem();
                        item.AddNew();
                    }

                    item.ScoresheetNo = entity.ScoresheetNo;
                    item.QuestionnaireItemID = questionnaireItemId.ToInt();
                    item.Score = score;

                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;

                    item.Save();
                }
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.ScoresheetNo > txtScoresheetNo.Text, que.EvaluatorID == AppSession.UserLogin.PersonID.ToInt());
                que.OrderBy(que.ScoresheetNo.Ascending);
            }
            else
            {
                que.Where(que.ScoresheetNo < this.txtScoresheetNo.Text, que.EvaluatorID == AppSession.UserLogin.PersonID.ToInt());
                que.OrderBy(que.ScoresheetNo.Descending);
            }

            var entity = new ClinicalPerformanceAppraisalQuestionnaireScoresheet();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region ComboBox
        protected void cboEvaluatorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
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
                            query.EmployeeNumber.Like(searchText),
                            query.EmployeeName.Like(searchText)
                        )
                );

            cboEvaluatorID.DataSource = query.LoadDataTable();
            cboEvaluatorID.DataBind();
        }

        protected void cboEvaluatorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
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
                query.Or(query.SupervisorId == AppSession.UserLogin.PersonID.ToInt(), query.ManagerID == AppSession.UserLogin.PersonID.ToInt()),
                query.Or
                        (
                            query.EmployeeNumber.Like(searchText),
                            query.EmployeeName.Like(searchText)
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
            var employeeInfo = new VwEmployeeTable();
            var employeeInfoQ = new VwEmployeeTableQuery();
            employeeInfoQ.es.Top = 1;
            employeeInfoQ.Where(employeeInfoQ.PersonID == e.Value.ToInt());
            employeeInfo.Load(employeeInfoQ);
            if (employeeInfo != null)
            {
                txtEmployeeNo.Text = employeeInfo.EmployeeNumber;
                txtPlaceDOB.Text = string.Format("{0}, {1}", employeeInfo.PlaceBirth, Convert.ToDateTime(employeeInfo.BirthDate).ToString("dd-MMM-yyyy"));
                cboSREmploymentType.SelectedValue = employeeInfo.SREmploymentType;
                if (cboSREmploymentType.SelectedValue == AppSession.Parameter.EmploymentTypePermanent)
                    txtREmploymentPermanentDate.SelectedDate = employeeInfo.TglDiangkat;
                else
                    txtREmploymentPermanentDate.Clear();

                var org = new OrganizationUnitQuery();
                org.Where(org.OrganizationUnitID == employeeInfo.ServiceUnitID.ToInt());
                var orgDtb = org.LoadDataTable();
                if (orgDtb.Rows.Count > 0)
                {
                    cboServiceUnitID.DataSource = orgDtb;
                    cboServiceUnitID.DataBind();
                    cboServiceUnitID.SelectedValue = employeeInfo.ServiceUnitID;
                }
                else
                {
                    cboServiceUnitID.Items.Clear();
                    cboServiceUnitID.SelectedValue = string.Empty;
                    cboServiceUnitID.Text = string.Empty;
                }

                var pos = new PositionQuery();
                pos.Where(pos.PositionID == employeeInfo.PositionID.ToInt());
                var posDtb = pos.LoadDataTable();
                if (posDtb.Rows.Count > 0)
                {
                    cboPositionID.DataSource = posDtb;
                    cboPositionID.DataBind();
                    cboPositionID.SelectedValue = employeeInfo.PositionID.ToString();
                }
                else
                {
                    cboPositionID.Items.Clear();
                    cboPositionID.SelectedValue = string.Empty;
                    cboPositionID.Text = string.Empty;
                }
            }
            else
            {
                txtEmployeeNo.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;
                cboSREmploymentType.SelectedValue = string.Empty;
                cboSREmploymentType.Text = string.Empty;
                txtREmploymentPermanentDate.Clear();

                cboServiceUnitID.Items.Clear();
                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;

                cboPositionID.Items.Clear();
                cboPositionID.SelectedValue = string.Empty;
                cboPositionID.Text = string.Empty;
            }

            var ecpq = new EmployeeClinicalPrivilegeQuery();
            ecpq.Where(ecpq.PersonID == e.Value.ToInt(), ecpq.ValidFrom <= DateTime.Now.Date);
            ecpq.OrderBy(ecpq.ValidFrom.Descending);
            ecpq.es.Top = 1;
            DataTable ecpdt = ecpq.LoadDataTable();
            if (ecpdt.Rows.Count > 0)
            {
                cboSRProfessionGroup.SelectedValue = ecpdt.Rows[0]["SRProfessionGroup"].ToString();
                cboSRClinicalWorkArea.SelectedValue = ecpdt.Rows[0]["SRClinicalWorkArea"].ToString();
                cboSRClinicalAuthorityLevel.SelectedValue = ecpdt.Rows[0]["SRClinicalAuthorityLevel"].ToString();
            }
            else
            {
                cboSRProfessionGroup.SelectedValue = string.Empty;
                cboSRProfessionGroup.Text = string.Empty;

                cboSRClinicalWorkArea.SelectedValue = string.Empty;
                cboSRClinicalWorkArea.Text = string.Empty;

                cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
                cboSRClinicalAuthorityLevel.Text = string.Empty;

                cboQuestionnaireID.Items.Clear();
                cboQuestionnaireID.SelectedValue = string.Empty;
                cboQuestionnaireID.Text = string.Empty;
            }

            GetQuestionnaireID();

            RefreshGridSheetItems();
            CalcultedScore();
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.OrganizationUnitID,
                    query.OrganizationUnitName
                );

            query.Where(
                query.SROrganizationLevel == "0",
                query.OrganizationUnitName.Like(searchText)
                );

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }

        protected void cboPositionID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new PositionQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PositionID,
                    query.PositionName
                );

            query.Where(
                query.PositionName.Like(searchText)
                );

            cboPositionID.DataSource = query.LoadDataTable();
            cboPositionID.DataBind();
        }

        protected void cboPositionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionID"].ToString();
        }

        protected void cboQuestionnaireID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["QuestionnaireName"].ToString() + " [" + ((DataRowView)e.Item.DataItem)["QuestionnaireCode"].ToString() + "]";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["QuestionnaireID"].ToString();
        }

        protected void cboQuestionnaireID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ClinicalPerformanceAppraisalQuestionnaireQuery("a");
            query.Where
                (
                    query.QuestionnaireName.Like(searchText)
                );
            query.OrderBy(query.QuestionnaireCode.Ascending);

            cboQuestionnaireID.DataSource = query.LoadDataTable();
            cboQuestionnaireID.DataBind();
        }

        protected void cboQuestionnaireID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RefreshGridSheetItems();
        }

        private void GetQuestionnaireID()
        {
            var questionnaire = new ClinicalPerformanceAppraisalQuestionnaire();
            var questionnaireQ = new ClinicalPerformanceAppraisalQuestionnaireQuery();
            //questionnaireQ.Where(questionnaireQ.SRProfessionGroup == cboSRProfessionGroup.SelectedValue,
            //    questionnaireQ.SRClinicalWorkArea == cboSRClinicalWorkArea.SelectedValue,
            //    questionnaireQ.SRClinicalAuthorityLevel == cboSRClinicalAuthorityLevel.SelectedValue,
            //    questionnaireQ.IsActive == true);
            questionnaireQ.es.Top = 1;
            questionnaire.Load(questionnaireQ);
            if (questionnaire != null)
            {
                cboQuestionnaireID.DataSource = questionnaireQ.LoadDataTable();
                cboQuestionnaireID.DataBind();
                cboQuestionnaireID.SelectedValue = questionnaire.QuestionnaireID.ToString();
            }
            else
            {
                cboQuestionnaireID.Items.Clear();
                cboQuestionnaireID.SelectedValue = string.Empty;
                cboQuestionnaireID.Text = string.Empty;
            }
        }

        #endregion

        #region ClinicalPerformanceAppraisalQuestionnaireScoresheetItem
        protected void grdSheet_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSheet.DataSource = ClinicalPerformanceAppraisalQuestionnaireScoresheetItems;
        }

        private DataTable ClinicalPerformanceAppraisalQuestionnaireScoresheetItems
        {
            get
            {
                object obj = this.Session["ClinicalPerformanceAppraisalQuestionnaireScoresheetItems" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                var questionnaireId = string.IsNullOrEmpty(cboQuestionnaireID.SelectedValue) ? "-1" : cboQuestionnaireID.SelectedValue;
                var scoresheetNo = txtScoresheetNo.Text;

                ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection coll = new ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection();
                DataTable dtb = coll.GetJoin(scoresheetNo, questionnaireId);

                Session["ClinicalPerformanceAppraisalQuestionnaireScoresheetItems" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        protected void grdSheet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    var dataItem = e.Item as GridDataItem;
                    if (dataItem["IsDetail"].Text == "False")
                    {
                        //dataItem.ForeColor = Color.DarkBlue;
                        dataItem.Font.Bold = true;
                        dataItem.Font.Italic = true;
                    }
                }
            }
            catch
            { }
        }

        protected void grdSheet_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdSheet_ItemPreRender;
        }

        private void grdSheet_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;
        }

        private void RefreshGridSheetItems()
        {
            Session["ClinicalPerformanceAppraisalQuestionnaireScoresheetItems" + Request.UserHostName] = null;
            grdSheet.Rebind();
        }

        protected string GetQuestionName(object questionGroupName, object questionName)
        {
            if (questionGroupName.ToString().Equals(string.Empty))
                return questionName.ToString();
            return "&nbsp;&nbsp;&nbsp;" + questionName.ToString();
        }

        private void CalcultedScore()
        {
            decimal total = -1;

            ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection coll = new ClinicalPerformanceAppraisalQuestionnaireScoresheetItemCollection();
            DataTable dtb = coll.GetTotalScore(txtScoresheetNo.Text);

            if (dtb.Rows.Count > 0)
            {
                total = 0;
                foreach (DataRow row in dtb.Rows)
                {
                    total += Convert.ToDecimal(row["TotalScore"]);
                }
            }

            txtTotal.Value = Convert.ToDouble(total);

            var con = new ClinicalPerformanceAppraisalQuestionnaireConclusion();
            con.Query.Where(con.Query.MinValue <= total, con.Query.MaxValue >= total);
            if (con.Query.Load())
            {
                lblConclusionGrade.Text = con.ConclusionGrade;
                lblConclusionSeparate.Text = "-";
                lblConclusionGradeName.Text = con.ConclusionGradeName;
            }
            else
            {
                lblConclusionGrade.Text = string.Empty;
                lblConclusionSeparate.Text = string.Empty;
                lblConclusionGradeName.Text = string.Empty;
            }
        }

        #endregion
    }
}