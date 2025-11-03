using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class HandHygieneDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "HandHygieneSearch.aspx";
            UrlPageList = "HandHygieneList.aspx";

            ProgramID = AppConstant.Program.HandHygiene;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            //ToolBarMenuSearch.Visible = false;
            //ToolBarMenuAdd.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            //ajax.AddAjaxSetting(cboEmployeeID, txtProfessionType);
            //ajax.AddAjaxSetting(cboEmployeeID, cboDepartmentID);
            //ajax.AddAjaxSetting(cboEmployeeID, cboDivisionID);
            //ajax.AddAjaxSetting(cboEmployeeID, cboSubDivisionID);
            //ajax.AddAjaxSetting(cboEmployeeID, cboUnit);

            //ajax.AddAjaxSetting(cboDepartmentID, cboDivisionID);
            //ajax.AddAjaxSetting(cboDepartmentID, cboSubDivisionID);
            //ajax.AddAjaxSetting(cboDepartmentID, cboUnit);

            //ajax.AddAjaxSetting(cboDivisionID, cboSubDivisionID);
            //ajax.AddAjaxSetting(cboDivisionID, cboUnit);

            //ajax.AddAjaxSetting(cboSubDivisionID, cboUnit);

            //ajax.AddAjaxSetting(txtTransactionDate, txtTransactionDate);
            //ajax.AddAjaxSetting(txtTransactionDate, txtTransactionNo);
            //ajax.AddAjaxSetting(txtTransactionDate, txtSessionLength);

            //ajax.AddAjaxSetting(txtStartTime, txtSessionLength);
            //ajax.AddAjaxSetting(txtEndTime, txtSessionLength);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new HandHygiene());

            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtStartTime.SelectedDate= (new DateTime()).NowAtSqlServer();
            txtEndTime.SelectedDate = (new DateTime()).NowAtSqlServer();

            txtTransactionNo.Text = PopulateNewNo();
            SessionLengthCalculated();

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (HandHygieneItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }
            
            var entity = new HandHygiene();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (HandHygieneItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new HandHygiene();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "HandHygiene";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new HandHygiene();
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

        private bool IsApprovedOrVoid(HandHygiene entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
            printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return ViewState["IsApproved"] == null ? false : !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new HandHygiene();
            if (parameters.Length > 0)
            {
                var tno = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(tno);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var hh = (HandHygiene)entity;

            txtTransactionNo.Text = hh.TransactionNo;
            txtTransactionDate.SelectedDate = hh.TransactionDate;
            txtStartTime.SelectedDate = Helper.GetForDisplayTime(hh.StartTime);
            txtEndTime.SelectedDate = Helper.GetForDisplayTime(hh.EndTime);
            txtSessionLength.Value = Convert.ToInt16(hh.SessionLength);

            if (!string.IsNullOrEmpty(hh.ObserverID.ToString()))
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(hh.ObserverID));
                var dtb = personal.LoadDataTable();
                cboObserverID.DataSource = dtb;
                cboObserverID.DataBind();
                cboObserverID.SelectedValue = hh.ObserverID.ToString();
                if (!string.IsNullOrEmpty(cboObserverID.SelectedValue))
                    cboObserverID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboObserverID.Items.Clear();
                cboObserverID.SelectedValue = string.Empty;
                cboObserverID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(hh.EmployeeID.ToString()))
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(hh.EmployeeID));
                var dtb = personal.LoadDataTable();
                cboEmployeeID.DataSource = dtb;
                cboEmployeeID.DataBind();
                cboEmployeeID.SelectedValue = hh.EmployeeID.ToString();
                if (!string.IsNullOrEmpty(cboEmployeeID.SelectedValue))
                {
                    cboEmployeeID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();

                    var pt = new AppStandardReferenceItem();
                    if (pt.LoadByPrimaryKey(AppEnum.StandardReference.ProfessionType.ToString(), dtb.Rows[0]["SRProfessionType"].ToString()))
                        txtProfessionType.Text = pt.ItemName;
                    else
                        txtProfessionType.Text = string.Empty;
                }
            }
            else
            {
                cboEmployeeID.Items.Clear();
                cboEmployeeID.SelectedValue = string.Empty;
                cboEmployeeID.Text = string.Empty;
                txtProfessionType.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(hh.DepartmentID.ToString()) && hh.DepartmentID > 0)
            {
                var query = new OrganizationUnitQuery();
                query.Where(query.OrganizationUnitID == Convert.ToInt32(hh.DepartmentID));
                var dtb = query.LoadDataTable();
                cboDepartmentID.DataSource = dtb;
                cboDepartmentID.DataBind();
                cboDepartmentID.SelectedValue = hh.DepartmentID.ToString();
            }
            else
            {
                cboDepartmentID.Items.Clear();
                cboDepartmentID.SelectedValue = string.Empty;
                cboDepartmentID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(hh.DivisionID.ToString()) && hh.DivisionID > 0)
            {
                var query = new OrganizationUnitQuery();
                query.Where(query.OrganizationUnitID == Convert.ToInt32(hh.DivisionID));
                var dtb = query.LoadDataTable();
                cboDivisionID.DataSource = dtb;
                cboDivisionID.DataBind();
                cboDivisionID.SelectedValue = hh.DivisionID.ToString();
            }
            else
            {
                cboDivisionID.Items.Clear();
                cboDivisionID.SelectedValue = string.Empty;
                cboDivisionID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(hh.SubDivisionID.ToString()) && hh.SubDivisionID > 0)
            {
                var query = new OrganizationUnitQuery();
                query.Where(query.OrganizationUnitID == Convert.ToInt32(hh.SubDivisionID));
                var dtb = query.LoadDataTable();
                cboSubDivisionID.DataSource = dtb;
                cboSubDivisionID.DataBind();
                cboSubDivisionID.SelectedValue = hh.SubDivisionID.ToString();
            }
            else
            {
                cboSubDivisionID.Items.Clear();
                cboSubDivisionID.SelectedValue = string.Empty;
                cboSubDivisionID.Text = string.Empty;
            }

            if (hh.ServiceUnitID != null && hh.ServiceUnitID.ToInt() > 0)
            {
                var query = new OrganizationUnitQuery();
                query.Where(query.OrganizationUnitID == Convert.ToInt32(hh.ServiceUnitID));
                var dtb = query.LoadDataTable();
                cboUnit.DataSource = dtb;
                cboUnit.DataBind();
                cboUnit.SelectedValue = hh.ServiceUnitID.ToString();
            }
            else
            {
                cboUnit.Items.Clear();
                cboUnit.SelectedValue = string.Empty;
                cboUnit.Text = string.Empty;
            }

            PopulateItemGrid();

            ViewState["IsApproved"] = hh.IsApproved ?? false;
            ViewState["IsVoid"] = hh.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new HandHygiene();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new HandHygiene();
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

            if (entity.IsApproved == false)
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(HandHygiene entity, bool isApproval, ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = isApproval;

                if (isApproval)
                {
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                }
                else
                {
                    entity.ApprovedByUserID = null;
                    entity.ApprovedDateTime = null;
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new HandHygiene();
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

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new HandHygiene();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(HandHygiene entity, bool isVoid)
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
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(HandHygiene entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = PopulateNewNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.StartTime = Helper.GetHourMinute(txtStartTime.SelectedDate);
            entity.EndTime = Helper.GetHourMinute(txtEndTime.SelectedDate);
            entity.SessionLength = Convert.ToInt16(txtSessionLength.Value);
            entity.ObserverID = cboObserverID.SelectedValue.ToInt();
            entity.EmployeeID = cboEmployeeID.SelectedValue.ToInt();
            entity.DepartmentID = string.IsNullOrEmpty(cboDepartmentID.SelectedValue) ? 0: cboDepartmentID.SelectedValue.ToInt();
            entity.DivisionID = string.IsNullOrEmpty(cboDivisionID.SelectedValue) ? 0 : cboDivisionID.SelectedValue.ToInt();
            entity.SubDivisionID = string.IsNullOrEmpty(cboSubDivisionID.SelectedValue) ? 0 : cboSubDivisionID.SelectedValue.ToInt();
            entity.ServiceUnitID = cboUnit.SelectedValue;
            entity.IsApproved = false;
            entity.IsVoid = false;

            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            }

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in HandHygieneItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(HandHygiene entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                HandHygieneItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new HandHygieneQuery("a");

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }

            var entity = new HandHygiene();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of HandHygieneItem

        private HandHygieneItemCollection HandHygieneItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collHandHygieneItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((HandHygieneItemCollection)(obj));
                    }
                }

                var coll = new HandHygieneItemCollection();
                var query = new HandHygieneItemQuery("a");
                var opp = new AppStandardReferenceItemQuery("b");
                var hw = new AppStandardReferenceItemQuery("c");
                var hh = new AppStandardReferenceItemQuery("d");
                var rs = new AppStandardReferenceItemQuery("e");

                query.Select
                    (
                        query,
                        opp.ItemName.As("refToStdRef_Opportunity"),
                        hw.ItemName.As("refToStdRef_HandWashType"),
                        hh.ItemName.As("refToStdRef_HandHygieneNote"),
                        rs.ItemName.As("refToStdRef_Apply6StepsResult")
                    );
                
                query.InnerJoin(opp).On(opp.StandardReferenceID == AppEnum.StandardReference.Opportunity && opp.ItemID == query.SROpportunity);
                query.LeftJoin(hw).On(hw.StandardReferenceID == AppEnum.StandardReference.HandWashType && hw.ItemID == query.SRHandWashType);
                query.LeftJoin(hh).On(hh.StandardReferenceID == AppEnum.StandardReference.HandHygieneNote && hh.ItemID == query.SRHandHygieneNote);
                query.LeftJoin(rs).On(rs.StandardReferenceID == AppEnum.StandardReference.Apply6StepsResult && rs.ItemID == query.SRApply6StepsResult);

                query.Where(query.TransactionNo == txtTransactionNo.Text);
                
                query.OrderBy(query.SROpportunity.Ascending);
                coll.Load(query);

                Session["collHandHygieneItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collHandHygieneItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            HandHygieneItems = null; //Reset Record Detail
            grdItem.DataSource = HandHygieneItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private HandHygieneItem FindItem(String id)
        {
            HandHygieneItemCollection coll = HandHygieneItems;
            HandHygieneItem retEntity = null;
            foreach (HandHygieneItem rec in coll)
            {
                if (rec.SROpportunity.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = HandHygieneItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][HandHygieneItemMetadata.ColumnNames.SROpportunity]);
            HandHygieneItem entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String id =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][HandHygieneItemMetadata.ColumnNames.SROpportunity]);
            HandHygieneItem entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            HandHygieneItem entity = HandHygieneItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(HandHygieneItem entity, GridCommandEventArgs e)
        {
            var userControl = (HandHygieneItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SROpportunity = userControl.SROpportunity;
                entity.OpportunityName = userControl.OpportunityName;
                entity.SRHandWashType = userControl.SRHandWashType;
                entity.HandWashTypeName = userControl.HandWashTypeName;
                entity.IsWearGloves = userControl.IsWearGloves;
                entity.SRHandHygieneNote = userControl.SRHandHygieneNote;
                entity.HandHygieneNoteName = userControl.HandHygieneNoteName;
                entity.IsApply6Steps = userControl.IsApply6Steps;
                entity.SRApply6StepsResult = userControl.SRApply6StepsResult;
                entity.Apply6StepsResultName = userControl.Apply6StepsResultName;
            }
        }

        #endregion

        #region Combobox
        protected void cboObserverID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
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

            query.Where
                (query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboObserverID.DataSource = query.LoadDataTable();
            cboObserverID.DataBind();
        }

        protected void cboObserverID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboEmployeeID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
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

            query.Where
                (query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboEmployeeID.DataSource = query.LoadDataTable();
            cboEmployeeID.DataBind();
        }

        protected void cboEmployeeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }
        protected void cboEmployeeID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                txtProfessionType.Text = string.Empty;
                cboDepartmentID.Items.Clear();
                cboDepartmentID.SelectedValue = string.Empty;
                cboDepartmentID.Text = string.Empty;
                cboDivisionID.Items.Clear();
                cboDivisionID.SelectedValue = string.Empty;
                cboDivisionID.Text = string.Empty;
                cboSubDivisionID.Items.Clear();
                cboSubDivisionID.SelectedValue = string.Empty;
                cboSubDivisionID.Text = string.Empty;
                cboUnit.Items.Clear();
                cboUnit.SelectedValue = string.Empty;
                cboUnit.Text = string.Empty;

                return;
            }

            var empq = new VwEmployeeTableQuery();
            empq.Where(empq.PersonID == e.Value.ToInt());
            var emp = new VwEmployeeTable();
            emp.Load(empq);

            var pt = new AppStandardReferenceItem();
            if (pt.LoadByPrimaryKey(AppEnum.StandardReference.ProfessionType.ToString(), emp.SRProfessionType))
                txtProfessionType.Text = pt.ItemName;
            else
                txtProfessionType.Text = string.Empty;

            if (emp.OrganizationUnitID > 0)
            {
                var query = new OrganizationUnitQuery();
                query.Where(query.OrganizationUnitID == Convert.ToInt32(emp.OrganizationUnitID));
                var dtb = query.LoadDataTable();
                cboDepartmentID.DataSource = dtb;
                cboDepartmentID.DataBind();
                cboDepartmentID.SelectedValue = emp.OrganizationUnitID.ToString();
            }
            else
            {
                cboDepartmentID.Items.Clear();
                cboDepartmentID.SelectedValue = string.Empty;
                cboDepartmentID.Text = string.Empty;
            }

            if (emp.SubOrganizationUnitID > 0)
            {
                var query = new OrganizationUnitQuery();
                query.Where(query.OrganizationUnitID == Convert.ToInt32(emp.SubOrganizationUnitID));
                var dtb = query.LoadDataTable();
                cboDivisionID.DataSource = dtb;
                cboDivisionID.DataBind();
                cboDivisionID.SelectedValue = emp.SubOrganizationUnitID.ToString();
            }
            else
            {
                cboDivisionID.Items.Clear();
                cboDivisionID.SelectedValue = string.Empty;
                cboDivisionID.Text = string.Empty;
            }

            if (emp.SubDivisonID > 0)
            {
                var query = new OrganizationUnitQuery();
                query.Where(query.OrganizationUnitID == Convert.ToInt32(emp.SubDivisonID));
                var dtb = query.LoadDataTable();
                cboSubDivisionID.DataSource = dtb;
                cboSubDivisionID.DataBind();
                cboSubDivisionID.SelectedValue = emp.SubDivisonID.ToString();
            }
            else
            {
                cboSubDivisionID.Items.Clear();
                cboSubDivisionID.SelectedValue = string.Empty;
                cboSubDivisionID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(emp.ServiceUnitID.ToString()) && emp.ServiceUnitID.ToInt() > 0)
            {
                var query = new OrganizationUnitQuery();
                query.Where(query.OrganizationUnitID == Convert.ToInt32(emp.ServiceUnitID));
                var dtb = query.LoadDataTable();
                cboUnit.DataSource = dtb;
                cboUnit.DataBind();
                cboUnit.SelectedValue = emp.ServiceUnitID.ToString();
            }
            else
            {
                cboUnit.Items.Clear();
                cboUnit.SelectedValue = string.Empty;
                cboUnit.Text = string.Empty;
            }
        }

        protected void cboDepartmentID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();

            query.Where(
                query.OrganizationUnitName.Like(searchTextContain));

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.Where(query.SROrganizationLevel == "3");
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboDepartmentID.DataSource = dtb;
            cboDepartmentID.DataBind();
        }
        
        protected void cboDepartmentID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }
        protected void cboDepartmentID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboDivisionID.Items.Clear();
            cboDivisionID.Text = string.Empty;
            cboSubDivisionID.Items.Clear();
            cboSubDivisionID.Items.Clear();
            cboUnit.Items.Clear();
            cboUnit.Text = string.Empty;
        }

        protected void cboDivisionID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitName.Like(searchTextContain));
            query.Select(query.OrganizationUnitID, query.OrganizationUnitName);
            query.Where(query.SROrganizationLevel == "2", query.ParentOrganizationUnitID == cboDepartmentID.SelectedValue.ToInt());
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboDivisionID.DataSource = dtb;
            cboDivisionID.DataBind();
        }
        protected void cboDivisionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }
        protected void cboDivisionID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboUnit.Items.Clear();
            cboUnit.Text = string.Empty;
            cboSubDivisionID.Items.Clear();
            cboSubDivisionID.Text = string.Empty;
        }

        protected void cboSubDivisionID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitName.Like(searchTextContain));
            query.Select(query.OrganizationUnitID, query.OrganizationUnitName);
            query.Where(query.SROrganizationLevel == "1",
                query.Or(query.ParentOrganizationUnitID == cboDivisionID.SelectedValue.ToInt(), query.ParentOrganizationUnitID == cboDepartmentID.SelectedValue.ToInt())
                );
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubDivisionID.DataSource = dtb;
            cboSubDivisionID.DataBind();
        }
        protected void cboSubDivisionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }
        protected void cboSubDivisionID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboUnit.Items.Clear();
            cboUnit.Text = string.Empty;
        }

        protected void cboUnit_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery("a");
            var sub = new OrganizationUnitQuery("b");
            query.LeftJoin(sub).On(sub.OrganizationUnitID == query.ParentOrganizationUnitID);
            query.Select(query.OrganizationUnitID, query.OrganizationUnitName);
            query.Where(query.OrganizationUnitName.Like(searchTextContain), query.SROrganizationLevel == "0",
                query.Or(
                    query.ParentOrganizationUnitID == cboSubDivisionID.SelectedValue.ToInt(),
                    query.ParentOrganizationUnitID == cboDivisionID.SelectedValue.ToInt(),
                    query.ParentOrganizationUnitID == cboDepartmentID.SelectedValue.ToInt()));
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();

            cboUnit.DataSource = dtb;
            cboUnit.DataBind();
        }
        
        protected void cboUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }


        #endregion

        protected void txtTransactionDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            txtTransactionNo.Text = PopulateNewNo();
            SessionLengthCalculated();
        }

        protected void txtTime_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            SessionLengthCalculated();
        }

        private string PopulateNewNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value, AppEnum.AutoNumber.HandHygieneNo);

            return _autoNumber.LastCompleteNumber;
        }

        private void SessionLengthCalculated()
        {
            DateTime d1 = DateTime.Parse(txtTransactionDate.SelectedDate.Value.ToShortDateString() + " " +
                               txtStartTime.SelectedDate.Value.ToShortTimeString());
            DateTime d2 = DateTime.Parse(txtTransactionDate.SelectedDate.Value.ToShortDateString() + " " +
                               txtEndTime.SelectedDate.Value.ToShortTimeString());
            TimeSpan diff = d2 - d1;
            int hour = diff.Minutes;

            txtSessionLength.Value = Convert.ToDouble(hour);
        }
    }
}