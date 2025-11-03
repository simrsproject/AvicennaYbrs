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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Linq;
using DevExpress.DataProcessing;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord.MedicalRecordFileCompletenessAnalysis
{
    public partial class CompletenessAnalysisDetail : BasePageDetail
    {
        private Registration _regCurr;
        private Registration RegistrationCurrent
        {
            get
            {
                if (_regCurr == null)
                {
                    _regCurr = new Registration();
                    _regCurr.LoadByPrimaryKey(RegistrationNo);
                }

                return _regCurr;
            }
        }

        public string ReferFromRegistrationNo
        {
            get
            {
                // Baca ulangdari Registration krn ada fitur bisa merubah FromRgistrationNo
                return RegistrationCurrent.FromRegistrationNo;
            }
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "#";
            UrlPageList = "CompletenessAnalysisList.aspx";

            ProgramID = AppConstant.Program.MedicalRecordFileCompletenessAnalysis;

            if (!IsPostBack)
            {
                txtRegistrationNo.Text = Request.QueryString["regno"].ToString();
                PopulatePatientInformation(txtRegistrationNo.Text);
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

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuMoveNext.Enabled = false;
            ToolBarMenuMovePrev.Enabled = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboSRFilesAnalysis, cboSRFilesAnalysis);
            ajax.AddAjaxSetting(cboSRFilesAnalysis, grdItem);

            ajax.AddAjaxSetting(grdItem, grdItem);
            ajax.AddAjaxSetting(grdItem, grdHistory);
            ajax.AddAjaxSetting(grdItem, txtLastSubmitDate);
            if (pnlSubmitToUnit.Visible)
                ajax.AddAjaxSetting(grdItem, lblSubmitSuccess);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new MedicalRecordFileCompleteness());

            txtRegistrationNo.Text = Request.QueryString["regno"].ToString();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            grdItem.DataSource = MedicalRecordFileCompletenessItems; //Requery
            grdItem.DataBind();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new MedicalRecordFileCompleteness();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new MedicalRecordFileCompleteness();
            if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtRegistrationNo.Text.Trim());
            auditLogFilter.TableName = "MedicalRecordFileCompleteness";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_RegistrationNo", txtRegistrationNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new MedicalRecordFileCompleteness();
            entity.LoadByPrimaryKey(txtRegistrationNo.Text);

            if (entity.LastSubmitDate.HasValue && !entity.LastReturnDate.HasValue)
            {
                args.MessageText = "This data has been submitted to the unit for completion but has not been returned.";
                args.IsCancel = true;
                return;
            }

            if (entity.LastSubmitDate.HasValue && entity.LastReturnDate.HasValue)
            {
                var coll = MedicalRecordFileCompletenessHistorys.Where(b => b.ReturnDateTime == null);
                if (coll.Count() > 0)
                {
                    args.MessageText = "This data has been submitted to the unit for completion but has not been returned.";
                    args.IsCancel = true;
                    return;
                }
            }

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
            var entity = new MedicalRecordFileCompleteness();
            entity.LoadByPrimaryKey(txtRegistrationNo.Text);

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

        #endregion

        #region ToolBar Menu Support

        private bool IsApproved(MedicalRecordFileCompleteness entity, ValidateArgs args)
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

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemHistory(oldVal, newVal);
            
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns.FindByUniqueName("IsComplete").Visible = !isVisible;
            grdItem.Columns.FindByUniqueName("IsNotApplicable").Visible = !isVisible;
            grdItem.Columns.FindByUniqueName("Notes").Visible = !isVisible;

            grdItem.Columns.FindByUniqueName("chkIsComplete").Visible = isVisible;
            grdItem.Columns.FindByUniqueName("chkIsNotApplicable").Visible = isVisible;
            grdItem.Columns.FindByUniqueName("txtNotes").Visible = isVisible;

            RefreshGridItems();
            pnlSubmitToUnit.Visible = !isVisible && !chkIsApproved.Checked;
            txtNotesToUnit.ReadOnly = false;
            lblSubmitSuccess.Visible = false;
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new MedicalRecordFileCompleteness();
            if (entity.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                if (!IsApproved(entity, args))
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
            var entity = new MedicalRecordFileCompleteness();
            entity.LoadByPrimaryKey(txtRegistrationNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new MedicalRecordFileCompleteness();

            entity.LoadByPrimaryKey(string.IsNullOrEmpty(txtRegistrationNo.Text) ? Request.QueryString["regno"] : txtRegistrationNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var cp = (MedicalRecordFileCompleteness)entity;

            txtRegistrationNo.Text = string.IsNullOrEmpty(cp.RegistrationNo) ? Request.QueryString["regno"].ToString() : cp.RegistrationNo;
            PopulatePatientInformation(txtRegistrationNo.Text);

            if (cp.TransactionDate.HasValue)
                txtTransactionDate.SelectedDate = cp.TransactionDate;
            else
                txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            var srFilesAnalysis = string.IsNullOrEmpty(cp.SRFilesAnalysis) ? GetFilesAnalysisId(txtDepartmentID.Text) : cp.SRFilesAnalysis;

            if (!string.IsNullOrEmpty(srFilesAnalysis))
            {
                var filesQ = new AppStandardReferenceItemQuery();
                filesQ.Select(filesQ.ItemID.As("SRFilesAnalysis"), filesQ.ItemName.As("FilesAnalysisName"));
                filesQ.Where(filesQ.StandardReferenceID == AppEnum.StandardReference.FilesAnalysis.ToString(), filesQ.ItemID == srFilesAnalysis);
                cboSRFilesAnalysis.DataSource = filesQ.LoadDataTable();
                cboSRFilesAnalysis.DataBind();
                cboSRFilesAnalysis.SelectedValue = srFilesAnalysis;
            }
            else
            {
                cboSRFilesAnalysis.Items.Clear();
                cboSRFilesAnalysis.SelectedValue = string.Empty;
                cboSRFilesAnalysis.Text = string.Empty;
            }

            if (cp.LastSubmitDate.HasValue)
                txtLastSubmitDate.SelectedDate = cp.LastSubmitDate;

            if (cp.LastReturnDate.HasValue)
                txtLastReturnDate.SelectedDate = cp.LastReturnDate;

            chkIsApproved.Checked = cp.IsApproved ?? false;

            PopulateHistoryGrid();

            if (IsPostBack)
            {
                RefreshGridItems();
            }
        }

        private void PopulatePatientInformation(string regNo)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(regNo))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                txtMedicalNo.Text = pat.MedicalNo;
                txtPatientName.Text = pat.PatientName;

                txtAddress.Text = pat.StreetName + " " + pat.City.Trim() + " " + pat.County.Trim();

                string ageYear = Helper.GetAgeInYear(pat.DateOfBirth.Value).ToString();
                string ageMonth = Helper.GetAgeInMonth(pat.DateOfBirth.Value).ToString();
                string ageDay = Helper.GetAgeInDay(pat.DateOfBirth.Value).ToString();

                if (ageYear == "0")
                {
                    if (ageMonth == "0")
                        txtAge.Text = ageDay + " d";
                    else
                        txtAge.Text = ageMonth + " m";
                }
                else
                    txtAge.Text = ageYear + " y";

                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);

                var q = new ParamedicQuery();
                q.Where(q.ParamedicID == reg.ParamedicID);
                cboParamedicID.DataSource = q.LoadDataTable();
                cboParamedicID.DataBind();

                cboParamedicID.SelectedValue = reg.ParamedicID;
                txtDepartmentID.Text = reg.DepartmentID;
                var depart = new Department();
                if (depart.LoadByPrimaryKey(txtDepartmentID.Text))
                    txtDepartmentName.Text = depart.DepartmentName;
                else txtDepartmentName.Text = string.Empty;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtAge.Text = string.Empty;
                txtGender.Text = string.Empty;
                txtPlaceDOB.Text = string.Empty;

                cboParamedicID.Items.Clear();
                cboParamedicID.Text = string.Empty;

                txtDepartmentID.Text = string.Empty;
                txtDepartmentName.Text = string.Empty;
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(MedicalRecordFileCompleteness entity)
        {
            entity.RegistrationNo = txtRegistrationNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.SRFilesAnalysis = cboSRFilesAnalysis.SelectedValue;
            entity.IsApproved = chkIsApproved.Checked;

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

            foreach (var item in MedicalRecordFileCompletenessHistorys)
            {
                item.RegistrationNo = txtRegistrationNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(MedicalRecordFileCompleteness entity)
        {
            using (var trans = new esTransactionScope())
            {
                if (MedicalRecordFileCompletenessHistorys.Count == 0)
                    entity.LastSubmitDate = null;
                else
                {
                    var historys = MedicalRecordFileCompletenessHistorys.OrderByDescending(h => h.SubmitDate).ThenByDescending(h => h.SubmitDateTime).Take(1).Single();
                    entity.LastSubmitDate = historys.SubmitDate;
                }

                entity.Save();
                MedicalRecordFileCompletenessHistorys.Save();

                foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
                {
                    string documentFilesId = dataItem.GetDataKeyValue("DocumentFilesID").ToString();

                    var item = new MedicalRecordFileCompletenessItem();
                    item.Query.Where(item.Query.RegistrationNo == entity.RegistrationNo, item.Query.DocumentFilesID == documentFilesId);
                    if (!item.Query.Load())
                    {
                        item = new MedicalRecordFileCompletenessItem();
                        item.AddNew();
                    }

                    item.RegistrationNo = entity.RegistrationNo;
                    item.DocumentFilesID = documentFilesId.ToInt();

                    bool isComplete = ((CheckBox)dataItem.FindControl("chkIsComplete")).Checked;
                    bool isNotApplicable = ((CheckBox)dataItem.FindControl("chkIsNotApplicable")).Checked;
                    string notes = ((RadTextBox)dataItem.FindControl("txtNotes")).Text;

                    item.IsComplete = isComplete;
                    item.IsNotApplicable = isNotApplicable;
                    item.Notes = notes;

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
            var que = new MedicalRecordFileCompletenessQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.RegistrationNo > txtRegistrationNo.Text);
                que.OrderBy(que.RegistrationNo.Ascending);
            }
            else
            {
                que.Where(que.RegistrationNo < this.txtRegistrationNo.Text);
                que.OrderBy(que.RegistrationNo.Descending);
            }

            var entity = new MedicalRecordFileCompleteness();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region ComboBox
        protected void cboSRFilesAnalysis_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FilesAnalysisName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SRFilesAnalysis"].ToString();
        }

        protected void cboSRFilesAnalysis_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new DocumentDefinitionQuery("a");
            var std = new AppStandardReferenceItemQuery("b");
            query.Select
                (
                    query.SRFilesAnalysis,
                    std.ItemName.As("FilesAnalysisName")
                );
            query.InnerJoin(std).On(query.SRFilesAnalysis == std.ItemID &&
                                    std.StandardReferenceID == AppEnum.StandardReference.FilesAnalysis.ToString());
            query.Where(
                query.DepartmentID == txtDepartmentID.Text,
                std.ItemName.Like(searchTextContain),
                query.IsActive == true
                );

            cboSRFilesAnalysis.DataSource = query.LoadDataTable();
            cboSRFilesAnalysis.DataBind();
        }

        protected void cboSRFilesAnalysis_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RefreshGridItems();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            query.Select
                (
                    query.ParamedicID, query.ParamedicName
                );
            
            query.Where(
                query.ParamedicName.Like(searchTextContain),
                query.IsActive == true
                );

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        private string GetFilesAnalysisId(string departmentId)
        {
            var filesAnalysisQ = new DocumentDefinitionQuery("a");
            var filesQ = new AppStandardReferenceItemQuery("b");

            filesAnalysisQ.Select(filesAnalysisQ.SRFilesAnalysis, filesQ.ItemName.As("FilesAnalysisName"));

            filesAnalysisQ.InnerJoin(filesQ).On(filesQ.StandardReferenceID == AppEnum.StandardReference.FilesAnalysis.ToString() && filesQ.ItemID == filesAnalysisQ.SRFilesAnalysis);
            filesAnalysisQ.Where(filesAnalysisQ.DepartmentID == departmentId,
                filesAnalysisQ.IsActive == true);
            filesAnalysisQ.es.Top = 1;
            DataTable dtb = filesAnalysisQ.LoadDataTable();
            if (dtb.Rows.Count > 0)
                return dtb.Rows[0]["SRFilesAnalysis"].ToString();
            
            return string.Empty;
        }

        #endregion

        #region Record Detail Method MedicalRecordFileCompletenessItem
        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = MedicalRecordFileCompletenessItems;
        }

        private DataTable MedicalRecordFileCompletenessItems
        {
            get
            {
                object obj = this.Session["collMedicalRecordFileCompletenessItem" + Request.UserHostName];
                if (obj != null)
                    return ((DataTable)(obj));

                var registrationNo = string.IsNullOrEmpty(txtRegistrationNo.Text) ? Request.QueryString["regno"].ToString() : txtRegistrationNo.Text;
                var departmentId = string.IsNullOrEmpty(txtDepartmentID.Text) ? AppSession.Parameter.InPatientDepartmentID : txtDepartmentID.Text;
                var srFilesAnalysis = string.IsNullOrEmpty(cboSRFilesAnalysis.SelectedValue) ? GetFilesAnalysisId(departmentId) : cboSRFilesAnalysis.SelectedValue;


                var coll = new MedicalRecordFileCompletenessItemCollection();
                DataTable dtb = coll.GetInnerJoinWDocument(registrationNo);
                if (dtb.Rows.Count == 0)
                    dtb = coll.GetFullJoinWDocument(departmentId, srFilesAnalysis);

                Session["collMedicalRecordFileCompletenessItem" + Request.UserHostName] = dtb;
                return dtb;
            }
        }

        private void RefreshGridItems()
        {
            Session["collMedicalRecordFileCompletenessItem" + Request.UserHostName] = null;
            grdItem.Rebind();
        }
        #endregion

        #region Record Detail Method Function MedicalRecordFileCompletenessHistory

        private MedicalRecordFileCompletenessHistoryCollection MedicalRecordFileCompletenessHistorys
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collMedicalRecordFileCompletenessHistory" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((MedicalRecordFileCompletenessHistoryCollection)(obj));
                    }
                }

                var coll = new MedicalRecordFileCompletenessHistoryCollection();
                var query = new MedicalRecordFileCompletenessHistoryQuery("a");
                var usrSubmit = new AppUserQuery("b");
                var usrReturn = new AppUserQuery("c");

                query.Select
                    (
                    query,
                    usrSubmit.UserName.As("refToUser_SubmitBy"),
                    usrReturn.UserName.As("refToUser_ReturnBy")
                    );
                query.InnerJoin(usrSubmit).On(usrSubmit.UserID == query.SubmitByUserID);
                query.LeftJoin(usrReturn).On(usrReturn.UserID == query.ReturnByUserID);

                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                query.OrderBy(query.SubmitDate.Descending, query.SubmitDateTime.Descending);

                coll.Load(query);

                Session["collMedicalRecordFileCompletenessHistory" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collMedicalRecordFileCompletenessHistory" + Request.UserHostName] = value; }
        }

        private void RefreshCommandItemHistory(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = false; //(newVal != AppEnum.DataMode.Read);
            grdHistory.Columns[0].Visible = isVisible;
            grdHistory.Columns[grdHistory.Columns.Count - 1].Visible = isVisible;

            grdHistory.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            if (IsPostBack)
                grdHistory.Rebind();
        }

        private void PopulateHistoryGrid()
        {
            //Display Data Detail
            MedicalRecordFileCompletenessHistorys = null; //Reset Record Detail
            grdHistory.DataSource = MedicalRecordFileCompletenessHistorys; //Requery
            grdHistory.MasterTableView.IsItemInserted = false;
            grdHistory.MasterTableView.ClearEditItems();
            grdHistory.DataBind();
        }

        protected void grdHistory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdHistory.DataSource = MedicalRecordFileCompletenessHistorys;
        }

        protected void grdHistory_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            Int64 id = Convert.ToInt64(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.TxId]);
            MedicalRecordFileCompletenessHistory entity = FindHistoryItem(id);

            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdHistory_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null)
                return;

            Int64 id = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex][MedicalRecordFileCompletenessHistoryMetadata.ColumnNames.TxId]);
            MedicalRecordFileCompletenessHistory entity = FindHistoryItem(id);

            if (entity != null && entity.ReturnDate == null)
                entity.MarkAsDeleted();
        }

        protected void grdHistory_InsertCommand(object source, GridCommandEventArgs e)
        {
            MedicalRecordFileCompletenessHistory entity = MedicalRecordFileCompletenessHistorys.AddNew();

            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdHistory.Rebind();
        }

        private MedicalRecordFileCompletenessHistory FindHistoryItem(Int64 id)
        {
            MedicalRecordFileCompletenessHistoryCollection coll = MedicalRecordFileCompletenessHistorys;
            MedicalRecordFileCompletenessHistory retEntity = null;
            foreach (MedicalRecordFileCompletenessHistory rec in coll)
            {
                if (rec.TxId.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(MedicalRecordFileCompletenessHistory entity, GridCommandEventArgs e)
        {
            var userControl = (CompletenessAnalysisHistoryItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.SubmitDate = userControl.SubmitDate;
                entity.SubmitNotes = userControl.SubmitNotes;
                entity.SubmitByUserID = userControl.SubmitByUserID;
                entity.SubmitBy = userControl.SubmitBy;
                entity.SubmitDateTime = (new DateTime()).NowAtSqlServer();

                //if (userControl.ReturnDate == null)
                //    entity.str.ReturnDate = string.Empty;
                //else
                //    entity.ReturnDate = userControl.ReturnDate;
                
            }
        }

        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;
            if (!(source is RadGrid))
                return;

            if (eventArgument == "submit")
            {
                var mrfc = new MedicalRecordFileCompleteness();
                mrfc.LoadByPrimaryKey(txtRegistrationNo.Text);

                if (mrfc.IsApproved ?? false)
                {
                    lblSubmitSuccess.Text = "** Data has been approved.";
                    lblSubmitSuccess.Visible = true;
                    grdItem.Rebind();

                    return;
                }

                if (mrfc.LastSubmitDate.HasValue && !mrfc.LastReturnDate.HasValue)
                {
                    lblSubmitSuccess.Text = "** Data has been submitted.";
                    lblSubmitSuccess.Visible = true;
                    grdItem.Rebind();
                    
                    return;
                }

                if (mrfc.LastSubmitDate.HasValue && mrfc.LastReturnDate.HasValue)
                {
                    var coll = MedicalRecordFileCompletenessHistorys.Where(b => b.ReturnDateTime == null);
                    if (coll.Count() > 0)
                    {
                        lblSubmitSuccess.Text = "** Data has been submitted.";
                        lblSubmitSuccess.Visible = true;
                        grdItem.Rebind();

                        return;
                    }
                }

                using (var trans = new esTransactionScope())
                {
                    mrfc.LastSubmitDate = (new DateTime()).NowAtSqlServer().Date;
                    mrfc.Save();

                    var mrfch = new MedicalRecordFileCompletenessHistory();
                    mrfch.AddNew();
                    mrfch.RegistrationNo = txtRegistrationNo.Text;
                    mrfch.SubmitDate = mrfc.LastSubmitDate;
                    mrfch.SubmitNotes = txtNotesToUnit.Text;
                    mrfch.SubmitDateTime = (new DateTime()).NowAtSqlServer();
                    mrfch.SubmitByUserID = AppSession.UserLogin.UserID;
                    mrfch.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    mrfch.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    mrfch.Save();

                    var coll = new MedicalRecordFileCompletenessHistoryItemCollection();
                    foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
                    {
                        string documentFilesId = dataItem.GetDataKeyValue("DocumentFilesID").ToString();
                        bool isComplete = ((CheckBox)dataItem.FindControl("chkIsComplete")).Checked;
                        bool isNotApplicable = ((CheckBox)dataItem.FindControl("chkIsNotApplicable")).Checked;
                        string notes = ((RadTextBox)dataItem.FindControl("txtNotes")).Text;

                        if (!isNotApplicable && !isComplete)
                        {
                            var mrfchi = coll.AddNew();

                            mrfchi.TxId = mrfch.TxId;
                            mrfchi.DocumentFilesID = documentFilesId.ToInt();
                            mrfchi.Notes = notes;
                            mrfchi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            mrfchi.LastUpdateDateTime = DateTime.Now;
                        }
                    }

                    coll.Save();

                    var mrfci = new MedicalRecordFileCompletenessItemCollection();
                    mrfci.Query.Where(mrfci.Query.RegistrationNo == txtRegistrationNo.Text, mrfci.Query.Or(mrfci.Query.IsComplete == true, mrfci.Query.IsNotApplicable == true));
                    mrfci.LoadAll();
                    foreach (var i in mrfci)
                    {
                        i.Notes = string.Empty;
                    }
                    mrfci.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();

                    txtLastSubmitDate.SelectedDate = mrfc.LastSubmitDate;
                    lblSubmitSuccess.Visible = true;
                }

                grdItem.Rebind();
                grdHistory.Rebind();
            }
        }

        protected static string GetRegistrationInfoMedic(string regNo, string astp)
        {
            var coll = new PatientAssessmentCollection();
            coll.Query.Where(coll.Query.RegistrationNo == regNo, coll.Query.SRAssessmentType == astp);
            coll.Query.OrderBy(coll.Query.AssessmentDateTime.Descending);
            coll.LoadAll();
            var registrationInfoMedicID = coll.FirstOrDefault()?.RegistrationInfoMedicID ?? string.Empty;
            return registrationInfoMedicID;
        }

        protected static string GetNursingCareNo(string regNo)
        {
            var coll = new NursingTransHDCollection();
            coll.Query.Where(coll.Query.RegistrationNo == regNo);
            coll.LoadAll();
            var nursingCareID = coll.FirstOrDefault()?.TransactionNo ?? string.Empty;
            return nursingCareID;
        }
    }
}