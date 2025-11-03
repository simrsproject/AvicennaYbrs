using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class OvertimeDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            switch (FormType)
            {
                case "":
                    WindowSearch.Height = 300;
                    UrlPageSearch = "OvertimeSearch.aspx?type=" + FormType;
                    UrlPageList = "OvertimeList.aspx?type=" + FormType;
                    ProgramID = AppConstant.Program.EmployeeOvertime;
                    break;
                case "appr":
                    UrlPageSearch = "#";
                    UrlPageList = "OvertimeApprovalList.aspx?type=" + FormType;
                    ProgramID = AppConstant.Program.EmployeeOvertimeApproval;
                    break;
                case "verif":
                    UrlPageSearch = "#";
                    UrlPageList = "OvertimeApprovalList.aspx?type=" + FormType;
                    ProgramID = AppConstant.Program.EmployeeOvertimeVerified;
                    break;
            }

            if (!IsPostBack)
            {
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, grdItem);
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeOvertime());

            txtTransactionNo.Text = GetNewTransactionNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            if (FormType == "")
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                var personal = new VwEmployeeTableQuery("a");
                var view = new VwEmployeeTableQuery("b");
                personal.InnerJoin(view).On(personal.PersonID == view.SupervisorId);
                personal.es.Distinct = true;
                personal.Select(personal.PersonID, personal.EmployeeNumber, personal.EmployeeName);
                personal.Where(personal.PersonID == Convert.ToInt32(usr.PersonID));
                var dtb = personal.LoadDataTable();
                cboSupervisorID.DataSource = dtb;
                cboSupervisorID.DataBind();
                if (dtb.Rows.Count > 0)
                {
                    cboSupervisorID.SelectedValue = usr.PersonID.ToString();
                    cboSupervisorID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
                }
            }
        }
        
        protected override void OnMenuEditClick()
        {
            if (FormType == "")
            {
                cboSupervisorID.Enabled = EmployeeOvertimeItems.Count == 0;
            }
            else
            {
                var entity = new EmployeeOvertime();
                if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
                {
                    if (entity.CreatedByUserID != AppSession.UserLogin.UserID)
                    {
                        cboSupervisorID.Enabled = false;
                        cboPayrollPeriodID.Enabled = false;
                    }
                    else
                        cboSupervisorID.Enabled = EmployeeOvertimeItems.Count == 0;
                }
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeeOvertime();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApproved(entity, args))
                    return;

                entity.MarkAsDeleted();
                EmployeeOvertimeItems.MarkAllAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new EmployeeOvertime();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new EmployeeOvertime();
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text);
            auditLogFilter.TableName = "EmployeeOvertime";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new EmployeeOvertime();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            if (FormType == "verif")
            {
                var aa = new EmployeeOvertimeItemQuery("aa");
                var bb = new EmployeeOvertimeItemQuery("bb");
                var b = new EmployeeOvertimeQuery("b");
                var e = new PersonalInfoQuery("e");
                aa.InnerJoin(bb).On(bb.PersonID == aa.PersonID);
                aa.InnerJoin(b).On(b.TransactionNo == bb.TransactionNo &
                    b.TransactionNo != entity.TransactionNo &
                    b.TransactionDate == entity.TransactionDate &
                    b.IsApproved == true &
                    b.IsVoid == false &
                    b.IsVerified == true);
                aa.InnerJoin(e).On(e.PersonID == aa.PersonID);
                aa.Where(aa.TransactionNo == entity.TransactionNo);
                aa.Select(aa.PersonID, e.EmployeeNumber, e.EmployeeName, b.TransactionNo);

                DataTable dtb = aa.LoadDataTable();
                if (dtb.Rows.Count > 0)
                {
                    var msg = string.Empty;
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (msg == string.Empty)
                            msg = row["EmployeeName"].ToString() + " [" + row["TransactionNo"].ToString() + "]";
                        else
                            msg += ", " + row["EmployeeName"].ToString() + " [" + row["TransactionNo"].ToString() + "]";
                    }

                    args.MessageText = "The following employees already have overtime forms on this date : " + msg;
                    args.IsCancel = true;
                    return;
                }
            }

            

            using (var trans = new esTransactionScope())
            {
                switch (FormType)
                {
                    case "":
                        entity.IsApproved = true;
                        entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                        entity.ApprovedByUserID = AppSession.UserLogin.UserID;

                        if (!AppSession.Parameter.IsOvertimeUseApprovalLevel)
                        {
                            entity.IsValidated = true;
                            entity.ValidatedDateTime = (new DateTime()).NowAtSqlServer();
                            entity.ValidatedByUserID = AppSession.UserLogin.UserID;
                        }
                        break;

                    case "appr":
                        entity.IsValidated = true;
                        entity.ValidatedDateTime = (new DateTime()).NowAtSqlServer();
                        entity.ValidatedByUserID = AppSession.UserLogin.UserID;
                        break;

                    case "verif":
                        entity.IsVerified = true;
                        entity.VerifiedDateTime = (new DateTime()).NowAtSqlServer();
                        entity.VerifiedByUserID = AppSession.UserLogin.UserID;

                        if (AppSession.Parameter.IsAutoInsertToEmployeePeriodicSalaryOvertime)
                        {
                            var epsColl = new EmployeePeriodicSalaryCollection();
                            foreach (var item in EmployeeOvertimeItems)
                            {
                                var eps = epsColl.AddNew();
                                eps.PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue);
                                eps.PersonID = item.PersonID;
                                eps.SalaryComponentID = item.SalaryComponentID;
                                eps.SRProcessType = AppSession.Parameter.ProcessTypeOvertime;
                                eps.Amount = item.Amount;
                                eps.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                eps.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                eps.TransactionDate = txtTransactionDate.SelectedDate;
                            }
                            epsColl.Save();
                        }

                        break;
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new EmployeeOvertime();
            entity.LoadByPrimaryKey(txtTransactionNo.Text);

            if (FormType == "" || FormType == "appr")
            {
                if (entity.IsVerified == true)
                {
                    args.MessageText = "Overtime already verified.";
                    args.IsCancel = true;
                    return;
                }
            }
            //else if (FormType == "verif")
            //{
            //    var cwt = new ClosingWageTransaction();
            //    if (cwt.LoadByPrimaryKey(entity.PayrollPeriodID.ToInt()) & cwt.IsClosed == true)
            //    {
            //        var period = string.Empty;
            //        var pp = new PayrollPeriod();
            //        if (pp.LoadByPrimaryKey(entity.PayrollPeriodID.ToInt()))
            //            period = pp.PayrollPeriodName;

            //        args.MessageText = "Payroll for period: " + period + " have been closed.";
            //        args.IsCancel = true;
            //        return;
            //    }
            //}

            using (var trans = new esTransactionScope())
            {
                if (FormType == "verif")
                {
                    entity.IsVerified = false;
                    entity.VerifiedDateTime = null;
                    entity.VerifiedByUserID = null;
                }
                else 
                {
                    entity.IsApproved = false;
                    entity.ApprovedDateTime = null;
                    entity.ApprovedByUserID = null;

                    entity.IsValidated = false;
                    entity.ValidatedDateTime = null;
                    entity.ValidatedByUserID = null;
                }

                switch (FormType)
                {
                    case "":
                        entity.IsApproved = false;
                        entity.ApprovedDateTime = null;
                        entity.ApprovedByUserID = null;

                        if (!AppSession.Parameter.IsOvertimeUseApprovalLevel)
                        {
                            entity.IsValidated = false;
                            entity.ValidatedDateTime = null;
                            entity.ValidatedByUserID = null;
                        }
                        break;

                    case "appr":
                        entity.IsApproved = false;
                        entity.ApprovedDateTime = null;
                        entity.ApprovedByUserID = null;

                        entity.IsValidated = false;
                        entity.ValidatedDateTime = null;
                        entity.ValidatedByUserID = null;
                        break;

                    case "verif":
                        //entity.IsVerified = false;
                        //entity.VerifiedDateTime = null;
                        //entity.VerifiedByUserID = null;

                        break;
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new EmployeeOvertime();
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
            entity.IsVoid = true;
            entity.VoidByUserID = AppSession.UserLogin.UserID;
            entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.Save();
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new EmployeeOvertime();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;

                //if (FormType == "verif" && AppSession.Parameter.IsOvertimeUseApprovalLevel)
                //{
                //    args.MessageText = "You do not have authorization to edit data.";
                //    args.IsCancel = true;
                //}
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        private bool IsApprovedOrVoid(EmployeeOvertime entity, ValidateArgs args)
        {
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            if (FormType == "verif")
            {
                if (entity.IsVerified ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else if (FormType == "appr")
            {
                if (entity.IsValidated ?? false)
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

        private bool IsApproved(EmployeeOvertime entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            switch (FormType)
            {
                case "":
                    ProgramID = AppConstant.Program.EmployeeOvertime;
                    break;
                case "appr":
                    ProgramID = AppConstant.Program.EmployeeOvertimeApproval;
                    break;
                case "verif":
                    ProgramID = AppConstant.Program.EmployeeOvertimeVerified;
                    break;
            }

            if (FormType == "")
            {
                cboSupervisorID.Enabled = false;
            }
            else
                ToolBarMenuSearch.Enabled = false;
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
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeOvertime();
            if (parameters.Length > 0)
            {
                string transNo = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);

                txtTransactionNo.Text = entity.TransactionNo;
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var employeeOvertime = (EmployeeOvertime)entity;

            txtTransactionNo.Text = employeeOvertime.TransactionNo;
            txtTransactionDate.SelectedDate = employeeOvertime.TransactionDate;

            if (!string.IsNullOrEmpty(employeeOvertime.SupervisorID.ToString()))
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(employeeOvertime.SupervisorID));
                var dtb = personal.LoadDataTable();
                cboSupervisorID.DataSource = dtb;
                cboSupervisorID.DataBind();
                cboSupervisorID.SelectedValue = employeeOvertime.SupervisorID.ToString();
                if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                    cboSupervisorID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboSupervisorID.Items.Clear();
                cboSupervisorID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(employeeOvertime.PayrollPeriodID.ToString()))
            {
                var pp = new PayrollPeriodQuery("a");
                pp.Select(pp.PayrollPeriodID, pp.PayrollPeriodCode, pp.PayrollPeriodName);
                pp.Where(pp.PayrollPeriodID == Convert.ToInt32(employeeOvertime.PayrollPeriodID));
                var dtb = pp.LoadDataTable();
                cboPayrollPeriodID.DataSource = dtb;
                cboPayrollPeriodID.DataBind();
                cboPayrollPeriodID.SelectedValue = employeeOvertime.PayrollPeriodID.ToString();
                if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                    cboPayrollPeriodID.Text = dtb.Rows[0]["PayrollPeriodCode"].ToString() + " - " + dtb.Rows[0]["PayrollPeriodName"].ToString();
            }
            else
            {
                cboPayrollPeriodID.Items.Clear();
                cboPayrollPeriodID.Text = string.Empty;
            }

            switch (FormType)
            {
                case "":
                    chkIsApproved.Checked = employeeOvertime.IsApproved ?? false;
                    break;
                case "appr":
                    chkIsApproved.Checked = employeeOvertime.IsValidated ?? false;
                    break;
                case "verif":
                    chkIsApproved.Checked = employeeOvertime.IsVerified ?? false;
                    break;
            }
            chkIsVoid.Checked = employeeOvertime.IsVoid ?? false;

            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(EmployeeOvertime entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }

            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.SupervisorID = Convert.ToInt32(cboSupervisorID.SelectedValue);
            entity.PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue);

            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                entity.IsApproved=false;
                entity.IsVoid = false;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            else if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in EmployeeOvertimeItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(EmployeeOvertime entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                EmployeeOvertimeItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeOvertimeQuery();
            que.es.Top = 1; // SELECT TOP 1 ..

            if (FormType == "")
            {
                if (isNextRecord)
                {
                    que.Where(que.TransactionNo > txtTransactionNo.Text);
                    que.OrderBy(que.TransactionNo.Ascending);
                }
                else
                {
                    que.Where(que.TransactionNo < txtTransactionNo.Text);
                    que.OrderBy(que.TransactionNo.Descending);
                }

                que.Where(que.CreatedByUserID == AppSession.UserLogin.UserID);
            }
            else 
            {
                que.Where(que.TransactionNo == txtTransactionNo.Text);
            }

            var entity = new EmployeeOvertime();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function
        #endregion

        #region Record Detail Method Function of Employee Overtime Item

        private EmployeeOvertimeItemCollection EmployeeOvertimeItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeOvertimeItem"];
                    if (obj != null)
                    {
                        return ((EmployeeOvertimeItemCollection)(obj));
                    }
                }

                var coll = new EmployeeOvertimeItemCollection();
                var query = new EmployeeOvertimeItemQuery("a");
                var person = new VwEmployeeTableQuery("b");
                var sc = new SalaryComponentQuery("c");

                query.Select
                    (
                        query,
                        person.EmployeeNumber.As("refToPersonalInfo_EmployeeNumber"),
                        person.EmployeeName.As("refToPersonalInfo_EmployeeName"),
                        sc.SalaryComponentName.As("refToSalaryComponent_SalaryComponentName")
                    );
                query.InnerJoin(person).On(query.PersonID == person.PersonID);
                query.InnerJoin(sc).On(query.SalaryComponentID == sc.SalaryComponentID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(person.EmployeeNumber.Ascending, sc.SalaryComponentCode.Ascending);
                coll.Load(query);

                Session["collEmployeeOvertimeItem"] = coll;
                return coll;
            }
            set
            {
                Session["collEmployeeOvertimeItem"] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible
                                                             ? GridCommandItemDisplay.Top
                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            EmployeeOvertimeItems = null; //Reset Record Detail
            grdItem.DataSource = EmployeeOvertimeItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private EmployeeOvertimeItem FindItem(string personId, string salaryCompId)
        {
            EmployeeOvertimeItemCollection coll = EmployeeOvertimeItems;
            EmployeeOvertimeItem retEntity = null;
            foreach (EmployeeOvertimeItem rec in coll)
            {
                if (rec.PersonID.ToString().Equals(personId) && rec.SalaryComponentID.ToString().Equals(salaryCompId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = EmployeeOvertimeItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String personId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeOvertimeItemMetadata.ColumnNames.PersonID]);
            String salaryCompId =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeOvertimeItemMetadata.ColumnNames.SalaryComponentID]);
            EmployeeOvertimeItem entity = FindItem(personId, salaryCompId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String personId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeOvertimeItemMetadata.ColumnNames.PersonID]);
            String salaryCompId =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeOvertimeItemMetadata.ColumnNames.SalaryComponentID]);
            EmployeeOvertimeItem entity = FindItem(personId, salaryCompId);
            if (entity != null)
                entity.MarkAsDeleted();

            if (FormType == "verif" || FormType == "appr")
                cboSupervisorID.Enabled = EmployeeOvertimeItems.Count == 0;
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeOvertimeItem entity = EmployeeOvertimeItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();

            if (FormType == "verif" || FormType == "appr")
                cboSupervisorID.Enabled = EmployeeOvertimeItems.Count == 0;
        }

        private void SetEntityValue(EmployeeOvertimeItem entity, GridCommandEventArgs e)
        {
            var userControl = (OvertimeItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.PersonID = userControl.PersonID.ToInt();

                var person = new VwEmployeeTableQuery();
                person.Where(person.PersonID == entity.PersonID);
                person.Select(person.EmployeeNumber, person.EmployeeName);
                DataTable dtb = person.LoadDataTable();
                if (dtb.Rows.Count > 0)
                {
                    entity.EmployeeNumber = dtb.Rows[0]["EmployeeNumber"].ToString();
                    entity.EmployeeName = dtb.Rows[0]["EmployeeName"].ToString();
                }

                entity.SalaryComponentID = userControl.SalaryComponetID.ToInt();
                entity.SalaryComponentName = userControl.SalaryComponetName;
                entity.Amount = userControl.Amount;
                entity.Notes = userControl.Notes;
                //entity.WorkingHourID = userControl.WorkingHourID == -1 ? null : userControl.WorkingHourID;
            }
        }

        #endregion

        #region Combobox
        protected void cboSupervisorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery("a");
            var view = new VwEmployeeTableQuery("b");
            query.es.Top = 20;
            query.es.Distinct = true;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.InnerJoin(view).On(query.PersonID == view.SupervisorId);
            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboSupervisorID.DataSource = query.LoadDataTable();
            cboSupervisorID.DataBind();
        }

        protected void cboSupervisorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }
        #endregion

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.EmployeeOvertimeNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}
