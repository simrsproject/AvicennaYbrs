using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Permission
{
    public partial class EmployeePermissionDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;
            // Url Search & List
            if (FormType == "")
            {
                UrlPageSearch = "EmployeePermissionSearch.aspx?type=";
                UrlPageList = "EmployeePermissionList.aspx?type=";
                ProgramID = AppConstant.Program.EmployeePermission;
            }
            else
            {
                UrlPageSearch = "#";
                UrlPageList = "EmployeePermissionVerifiedList.aspx?type=verif";
                ProgramID = AppConstant.Program.EmployeePermissionVerified;
            }

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRPermissionType, AppEnum.StandardReference.PermissionType);
                cboSupervisorID.Enabled = false;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            //ViewState["LeaveRequestID"] = 0;

            OnPopulateEntryControl(new EmployeePermission());
            txtPermissionID.Text = "0";
            txtPermissionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtPermissionDateFrom.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtPermissionDateTo.SelectedDate = (new DateTime()).NowAtSqlServer();


            var usr = new AppUser();
            usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);

            var supervisor = new VwEmployeeTableQuery();
            supervisor.Where(supervisor.PersonID == Convert.ToInt32(usr.PersonID));
            var dtb = supervisor.LoadDataTable();
            cboSupervisorID.DataSource = dtb;
            cboSupervisorID.DataBind();
            if (dtb.Rows.Count > 0)
            {
                cboSupervisorID.SelectedValue = usr.PersonID.ToString();
                cboSupervisorID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboSupervisorID.Items.Clear();
                cboSupervisorID.Text = string.Empty;
            }

            cboPersonID.Items.Clear();
            cboPersonID.Text = string.Empty;
            cboSRPermissionType.SelectedValue = string.Empty;
            cboSRPermissionType.Text = string.Empty;
            txtNotes.Text = string.Empty;
        }

        protected override void OnMenuEditClick()
        {
            if (FormType == "verif")
            {
                var entity = new EmployeePermission();
                if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPermissionID.Text)))
                {
                    txtPermissionDate.Enabled = false;
                    cboPersonID.Enabled = false;
                    cboSRPermissionType.Enabled = false;
                    txtPermissionDateFrom.Enabled = false;
                    txtPermissionDateTo.Enabled = false;
                    txtPermissionTimeFrom.Enabled = false;
                    txtPermissionTimeTo.Enabled = false;
                    txtNotes.ReadOnly = true;

                    if (entity.VerifiedDateTime == null)
                    {
                        txtVerifiedDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
                    }
                }
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeePermission();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPermissionID.Text)))
            {
                if (!IsApproved(entity, args))
                    return;

                entity.MarkAsDeleted();
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
            if (chkIsPayCut.Checked)
            {
                if (txtPayCutDays.Value == 0)
                {
                    args.MessageText = "Pay Cut Days is required.";
                    args.IsCancel = true;
                    return;
                }

                var x = txtPermissionDateFrom.SelectedDate.Value.Date;
                var y = txtPermissionDateTo.SelectedDate.Value.Date;
                var totalDays = (y - x).TotalDays + 1;
                if (txtPayCutDays.Value.ToInt() > totalDays)
                {
                    args.MessageText = "Pay Cut Days cannot be greater than " + totalDays.ToString() + ".";
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new EmployeePermission();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (chkIsPayCut.Checked)
            {
                if (txtPayCutDays.Value == 0)
                {
                    args.MessageText = "Pay Cut Days is required.";
                    args.IsCancel = true;
                    return;
                }

                var x = txtPermissionDateFrom.SelectedDate.Value.Date;
                var y = txtPermissionDateTo.SelectedDate.Value.Date;
                var totalDays = (y - x).TotalDays + 1;
                if (txtPayCutDays.Value.ToInt() > totalDays)
                {
                    args.MessageText = "Pay Cut Days cannot be greater than " + totalDays.ToString() + ".";
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new EmployeePermission();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPermissionID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("LeaveRequestID='{0}'", txtPermissionID.Text.ToString());
            auditLogFilter.TableName = "EmployeePermission";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_PermissionID", txtPermissionID.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (chkIsPayCut.Checked)
            {
                if (txtPayCutDays.Value == 0)
                {
                    args.MessageText = "Pay Cut Days is required.";
                    args.IsCancel = true;
                    return;
                }

                var x = txtPermissionDateFrom.SelectedDate.Value.Date;
                var y = txtPermissionDateTo.SelectedDate.Value.Date;
                var totalDays = (y - x).TotalDays + 1;
                if (txtPayCutDays.Value.ToInt() > totalDays)
                {
                    args.MessageText = "Pay Cut Days cannot be greater than " + totalDays.ToString() + ".";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                var entity = new EmployeePermission();
                entity.LoadByPrimaryKey(Convert.ToInt32(txtPermissionID.Text));

                entity.IsApproved = true;
                entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;

                if (FormType == "verif")
                {
                    entity.IsVerified = true;
                    entity.VerifiedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.VerifiedByUserID = AppSession.UserLogin.UserID;
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new EmployeePermission();
            entity.LoadByPrimaryKey(Convert.ToInt32(txtPermissionID.Text));

            if (FormType == "")
            {
                if (entity.IsVerified == true)
                {
                    args.MessageText = "Permission already verified.";
                    args.IsCancel = true;
                    return;
                }
            }

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
                }

                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new EmployeePermission();
            if (!entity.LoadByPrimaryKey(Convert.ToInt32(txtPermissionID.Text)))
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

            if (FormType == "verif")
            {
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = "This data can not be void because it has been approved by the user input.";
                    args.IsCancel = true;
                    return;
                }
            }

            SetVoid(entity, true);
        }

        private void SetVoid(EmployeePermission entity, bool isVoid)
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
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new EmployeePermission();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPermissionID.Text)))
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

        private bool IsApprovedOrVoid(EmployeePermission entity, ValidateArgs args)
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

        private bool IsApproved(EmployeePermission entity, ValidateArgs args)
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

            if (FormType == "verif")
            {
                ToolBarMenuSearch.Enabled = false;
            }
            else
            {
                chkIsPayCut.Enabled = false;
                txtPayCutDays.Enabled = false;
            }
        }

        //public override bool OnGetStatusMenuEdit()
        //{
        //    return txtPatientIncidentNo.Text != string.Empty;
        //}

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
            cboPersonID.Enabled = (newVal == AppEnum.DataMode.New);
            cboSupervisorID.Enabled = false;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeePermission();
            if (parameters.Length > 0)
            {
                string leaveRequestId = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(leaveRequestId));

                txtPermissionID.Text = entity.PermissionID.ToString();
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtPermissionID.Text));
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var employeePermission = (EmployeePermission)entity;

            txtPermissionID.Text = employeePermission.PermissionID.ToString();
            txtPermissionDate.SelectedDate = employeePermission.PermissionDate;

            if (!string.IsNullOrEmpty(employeePermission.SupervisorID.ToString()))
            {
                var supervisor = new VwEmployeeTableQuery();
                supervisor.Where(supervisor.PersonID == Convert.ToInt32(employeePermission.SupervisorID));
                var dtb = supervisor.LoadDataTable();
                cboSupervisorID.DataSource = dtb;
                cboSupervisorID.DataBind();
                cboSupervisorID.SelectedValue = employeePermission.SupervisorID.ToString();
                if (!string.IsNullOrEmpty(cboSupervisorID.SelectedValue))
                    cboSupervisorID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboSupervisorID.Items.Clear();
                cboSupervisorID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(employeePermission.PersonID.ToString()))
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(employeePermission.PersonID));
                var dtb = personal.LoadDataTable();
                cboPersonID.DataSource = dtb;
                cboPersonID.DataBind();
                cboPersonID.SelectedValue = employeePermission.PersonID.ToString();
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    cboPersonID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.Text = string.Empty;
            }

            cboSRPermissionType.SelectedValue = employeePermission.SRPermissionType;
            txtPermissionDateFrom.SelectedDate = employeePermission.PermissionDateFrom;
            txtPermissionDateTo.SelectedDate = employeePermission.PermissionDateTo;
            txtPermissionTimeFrom.SelectedDate = Helper.GetForDisplayTime(employeePermission.PermissionTimeFrom);
            txtPermissionTimeTo.SelectedDate = Helper.GetForDisplayTime(employeePermission.PermissionTimeTo);
            txtNotes.Text = employeePermission.Notes;
            chkIsPayCut.Checked = employeePermission.IsPayCut ?? false;
            txtPayCutDays.Value = Convert.ToDouble(employeePermission.PayCutDays);

            if (!string.IsNullOrEmpty(employeePermission.VerifiedByUserID))
            {
                var usr = new AppUser();
                if (usr.LoadByPrimaryKey(employeePermission.VerifiedByUserID))
                {
                    txtVerifiedDateTime.SelectedDate = employeePermission.VerifiedDateTime;
                    txtVerifiedBy.Text = usr.UserName;
                }
                else
                {
                    txtVerifiedDateTime.SelectedDate = null;
                    txtVerifiedBy.Text = string.Empty;
                }
            }
            else
            {
                txtVerifiedDateTime.SelectedDate = null;
                txtVerifiedBy.Text = string.Empty;
            }

            if (FormType == "")
                chkIsApproved.Checked = employeePermission.IsApproved ?? false;
            else
                chkIsApproved.Checked = employeePermission.IsVerified ?? false;

            chkIsVoid.Checked = employeePermission.IsVoid ?? false;
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(EmployeePermission entity)
        {
            if (entity.es.IsModified)
            {
                entity.PermissionID = Convert.ToInt32(txtPermissionID.Text);
            }

            entity.PermissionDate = txtPermissionDate.SelectedDate;
            entity.SupervisorID = Convert.ToInt32(cboSupervisorID.SelectedValue);
            entity.PersonID = Convert.ToInt32(cboPersonID.SelectedValue);
            entity.SRPermissionType = cboSRPermissionType.SelectedValue;
            entity.PermissionDateFrom = txtPermissionDateFrom.SelectedDate;
            entity.PermissionDateTo = txtPermissionDateTo.SelectedDate;
            entity.PermissionTimeFrom = Helper.GetHourMinute(txtPermissionTimeFrom.SelectedDate);
            entity.PermissionTimeTo = Helper.GetHourMinute(txtPermissionTimeTo.SelectedDate);
            entity.Notes = txtNotes.Text;
            entity.IsPayCut = chkIsPayCut.Checked;
            entity.PayCutDays = Convert.ToInt32(txtPayCutDays.Value);

            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
            else if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(EmployeePermission entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                try
                {
                    txtPermissionID.Text = entity.PermissionID.ToString();
                }
                catch
                { }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeePermissionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PermissionID > Convert.ToInt32(txtPermissionID.Text));
                que.OrderBy(que.PermissionID.Ascending);
            }
            else
            {
                que.Where(que.PermissionID < Convert.ToInt32(txtPermissionID.Text));
                que.OrderBy(que.PermissionID.Descending);
            }
            if (FormType == "")
                que.Where(que.CreatedByUserID == AppSession.UserLogin.UserID);
            else
                que.Where(que.IsApproved == true);

            var entity = new EmployeePermission();
            entity.Load(que);

            txtPermissionID.Text = entity.PermissionID.ToString();

            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function

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

            query.Where
                (query.Or(query.SupervisorId == cboSupervisorID.SelectedValue.ToInt(), query.ManagerID == cboSupervisorID.SelectedValue.ToInt()),
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

        protected void cboSRPermissionType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var ptype = new AppStandardReferenceItem();
            if (ptype.LoadByPrimaryKey(AppEnum.StandardReference.PermissionType.ToString(), e.Value))
                chkIsPayCut.Checked = ptype.ReferenceID == "1";
            else
                chkIsPayCut.Checked = false;
        }

        #endregion
    }
}