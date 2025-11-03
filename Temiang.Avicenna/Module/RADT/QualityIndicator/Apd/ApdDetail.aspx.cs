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
    public partial class ApdDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ApdSearch.aspx";
            UrlPageList = "ApdList.aspx";

            ProgramID = AppConstant.Program.ApdSurvey;

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
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ApdSurvey());

            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            txtTransactionNo.Text = PopulateNewNo();

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (ApdSurveyItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new ApdSurvey();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (ApdSurveyItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new ApdSurvey();
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
            auditLogFilter.TableName = "ApdSurvey";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ApdSurvey();
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

        private bool IsApprovedOrVoid(ApdSurvey entity, ValidateArgs args)
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
            var entity = new ApdSurvey();
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
            var hh = (ApdSurvey)entity;

            txtTransactionNo.Text = hh.TransactionNo;
            txtTransactionDate.SelectedDate = hh.TransactionDate;

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
            var entity = new ApdSurvey();
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
            var entity = new ApdSurvey();
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

        private void SetApproval(ApdSurvey entity, bool isApproval, ValidateArgs args)
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
            var entity = new ApdSurvey();
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
            var entity = new ApdSurvey();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(ApdSurvey entity, bool isVoid)
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

        private void SetEntityValue(ApdSurvey entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = PopulateNewNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ObserverID = cboObserverID.SelectedValue.ToInt();
            entity.EmployeeID = cboEmployeeID.SelectedValue.ToInt();
            entity.DepartmentID = string.IsNullOrEmpty(cboDepartmentID.SelectedValue) ? 0 : cboDepartmentID.SelectedValue.ToInt();
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

            foreach (var item in ApdSurveyItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(ApdSurvey entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                ApdSurveyItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ApdSurveyQuery("a");

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

            var entity = new ApdSurvey();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of ApdSurveyItem

        private ApdSurveyItemCollection ApdSurveyItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collApdSurveyItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((ApdSurveyItemCollection)(obj));
                    }
                }

                var coll = new ApdSurveyItemCollection();
                var query = new ApdSurveyItemQuery("a");
                var apd = new AppStandardReferenceItemQuery("b");

                query.Select
                    (
                        query, 
                        apd.ItemName.As("refToStdReff_ApdType"),
                        @"<CASE WHEN a.IsCorrectIndication = 1 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'refTo_IsIncorrectIndication'>",
                        @"<CASE WHEN a.IsCorrectUsageTime = 1 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'refTo_IsIncorrectUsageTime'>",
                        @"<CASE WHEN a.IsCorrectUsageTechnique = 1 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'refTo_IsIncorrectUsageTechnique'>",
                        @"<CASE WHEN a.IsCorrectReleaseTechnique = 1 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'refTo_IsIncorrectReleaseTechnique'>"
                    );

                query.InnerJoin(apd).On(apd.StandardReferenceID == AppEnum.StandardReference.ApdType && apd.ItemID == query.SRApdType);

                query.Where(query.TransactionNo == txtTransactionNo.Text);

                query.OrderBy(query.SRApdType.Ascending);
                coll.Load(query);

                Session["collApdSurveyItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collApdSurveyItem" + Request.UserHostName] = value;
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
            ApdSurveyItems = null; //Reset Record Detail
            grdItem.DataSource = ApdSurveyItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private ApdSurveyItem FindItem(String id)
        {
            ApdSurveyItemCollection coll = ApdSurveyItems;
            ApdSurveyItem retEntity = null;
            foreach (ApdSurveyItem rec in coll)
            {
                if (rec.SRApdType.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ApdSurveyItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String id =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ApdSurveyItemMetadata.ColumnNames.SRApdType]);
            ApdSurveyItem entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String id =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ApdSurveyItemMetadata.ColumnNames.SRApdType]);
            ApdSurveyItem entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            ApdSurveyItem entity = ApdSurveyItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(ApdSurveyItem entity, GridCommandEventArgs e)
        {
            var userControl = (ApdItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRApdType = userControl.SRApdType;
                entity.ApdTypeName = userControl.ApdTypeName;
                entity.IsCorrectIndication = userControl.IsCorrectIndication;
                entity.IsCorrectUsageTime = userControl.IsCorrectUsageTime;
                entity.IsCorrectUsageTechnique = userControl.IsCorrectUsageTechnique;
                entity.IsCorrectReleaseTechnique = userControl.IsCorrectReleaseTechnique;

                entity.IsIncorrectIndication = !userControl.IsCorrectIndication;
                entity.IsIncorrectUsageTime = !userControl.IsCorrectUsageTime;
                entity.IsIncorrectUsageTechnique = !userControl.IsCorrectUsageTechnique;
                entity.IsIncorrectReleaseTechnique = !userControl.IsCorrectReleaseTechnique;
            }
        }

        #endregion

        #region Combobox
        protected void cboObserverID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
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

            query.Where
                (query.Or
                        (
                            query.EmployeeNumber.Like(searchText),
                            query.EmployeeName.Like(searchText)
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
            string searchText = string.Format("%{0}%", e.Text);
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
                            query.EmployeeNumber.Like(searchText),
                            query.EmployeeName.Like(searchText)
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
            string searchText = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();
            query.Where(
                query.OrganizationUnitName.Like(searchText));

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
            string searchText = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();
            query.Where(query.OrganizationUnitName.Like(searchText));
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
            string searchText = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();
            query.Where(query.OrganizationUnitName.Like(searchText));
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
            string searchText = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery("a");
            var sub = new OrganizationUnitQuery("b");
            query.LeftJoin(sub).On(sub.OrganizationUnitID == query.ParentOrganizationUnitID);
            query.Select(query.OrganizationUnitID, query.OrganizationUnitName);
            query.Where(query.OrganizationUnitName.Like(searchText), query.SROrganizationLevel == "0",
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
        }


        private string PopulateNewNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value, AppEnum.AutoNumber.ApdSurveyNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}