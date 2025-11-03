using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Leave
{
    public partial class EmployeeLeaveRequestDetail : BasePageDetail
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        private string Role
        {
            get
            {
                return Request.QueryString["role"];
            }
        }

        #region Page Event & Initialize
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 300;
            // Url Search & List
            if (FormType == "request")
            {
                UrlPageSearch = "EmployeeLeaveRequestSearch.aspx?type=request";
                UrlPageList = "EmployeeLeaveRequestList.aspx?type=request";
                ProgramID = AppConstant.Program.EmployeeLeaveRequest;
            }
            else if (FormType == "request2")
            {
                UrlPageSearch = "EmployeeLeaveRequestSearch.aspx?type=request2";
                UrlPageList = "EmployeeLeaveRequestList.aspx?type=request2";
                ProgramID = AppConstant.Program.EmployeeLeaveRequest2;
            }
            else if (FormType == "appr")
            {
                UrlPageSearch = "#";
                UrlPageList = "EmployeeLeaveRequestApprovalList.aspx?type=appr";
                ProgramID = AppConstant.Program.EmployeeLeaveApproval;

                if (!string.IsNullOrWhiteSpace(Role)) UrlPageList += $"&role={Role}";
            }
            else if (FormType == "appr1")
            {
                UrlPageSearch = "#";
                UrlPageList = "EmployeeLeaveRequestApprovalList.aspx?type=appr1";
                ProgramID = AppConstant.Program.EmployeeLeaveApproval1;

                if (!string.IsNullOrWhiteSpace(Role)) UrlPageList += $"&role={Role}";
            }
            else if (FormType == "appr2")
            {
                UrlPageSearch = "#";
                UrlPageList = "EmployeeLeaveRequestApprovalList.aspx?type=appr2";
                ProgramID = AppConstant.Program.EmployeeLeaveApproval2;
            }
            else
            {
                UrlPageSearch = "#";
                UrlPageList = "EmployeeLeaveRequestApprovalList.aspx?type=verif";
                ProgramID = AppConstant.Program.EmployeeLeaveVerified;
            }

            if (!IsPostBack)
            {
                if (FormType == "request" || FormType == "request2")
                {
                    rfvLeaveRequestStatus.Visible = false;
                }
                if (FormType != "verif")
                {
                    rfvVerifiedDateTime.Visible = false;
                    rfvApprovedLeaveDateFrom.Visible = false;
                    rfvApprovedLeaveDateTo.Visible = false;
                    rfvApprovedDays.Visible = false;
                    rfvApprovedWorkingDate.Visible = false;
                }
                if (FormType == "verif")
                {
                    tabStrip.SelectedIndex = 1;
                    multiPage.SelectedIndex = 1;
                }
                StandardReference.InitializeIncludeSpace(cboSRWorkingDay, AppEnum.StandardReference.WorkingDay);
                pnlValidated1.Visible = AppSession.Parameter.EmployeeLeaveApprovalLevel == "3";
                pnlValidated2.Visible = AppSession.Parameter.EmployeeLeaveApprovalLevel != "1";
                pnlPayCut.Visible = AppSession.Parameter.IsEmployeeLeavePayCutVisible;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboPersonID, cboPersonID);
            ajax.AddAjaxSetting(cboPersonID, cboEmployeeLeaveID);

            ajax.AddAjaxSetting(cboEmployeeLeaveID, cboEmployeeLeaveID);
            ajax.AddAjaxSetting(cboEmployeeLeaveID, txtStartDate);
            ajax.AddAjaxSetting(cboEmployeeLeaveID, txtEndDate);
            ajax.AddAjaxSetting(cboEmployeeLeaveID, txtLeaveEntitlementsQty);
            ajax.AddAjaxSetting(cboEmployeeLeaveID, txtAlreadyTakenQty);
            ajax.AddAjaxSetting(cboEmployeeLeaveID, txtPendingQty);
            ajax.AddAjaxSetting(cboEmployeeLeaveID, txtBalace);

            if (AppSession.Parameter.IsEmployeeLeavePayCutVisible)
            {
                ajax.AddAjaxSetting(cboEmployeeLeaveID, chkIsPayCut);
                ajax.AddAjaxSetting(chkIsPayCut, chkIsPayCut);
                ajax.AddAjaxSetting(chkIsPayCut, txtPayCutDays);
                ajax.AddAjaxSetting(chkIsPayCut, cboSRWorkingDay);
                ajax.AddAjaxSetting(chkIsPayCut, cboPayrollPeriodID);
            }
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            //ViewState["LeaveRequestID"] = 0;

            OnPopulateEntryControl(new EmployeeLeaveRequest());
            txtLeaveRequestID.Text = "0";
            txtRequestDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            if (FormType == "request")
            {
                var usr = new AppUser();
                usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(usr.PersonID));
                var dtb = personal.LoadDataTable();
                cboPersonID.DataSource = dtb;
                cboPersonID.DataBind();
                if (dtb.Rows.Count > 0)
                {
                    cboPersonID.SelectedValue = usr.PersonID.ToString();
                    cboPersonID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
                }
            }
        }

        protected override void OnMenuEditClick()
        {
            if (FormType != "request" && FormType != "request2")
            {
                var entity = new EmployeeLeaveRequest();
                if (entity.LoadByPrimaryKey(Convert.ToInt32(txtLeaveRequestID.Text)))
                {
                    txtRequestDate.Enabled = false;
                    cboPersonID.Enabled = false;
                    cboEmployeeLeaveID.Enabled = false;

                    if (FormType == "verif")
                    {
                        txtRequestLeaveDateFrom.Enabled = false;
                        txtRequestLeaveDateTo.Enabled = false;
                        txtRequestDays.ReadOnly = true;
                        txtRequestWorkingDate.Enabled = false;
                        txtNotes.ReadOnly = true;
                        cboReplacementPersonID.Enabled = false;
                        chkIsPayCut.Enabled = true;

                        txtPayCutDays.ReadOnly = !chkIsPayCut.Checked;
                        cboSRWorkingDay.Enabled = chkIsPayCut.Checked;
                        cboPayrollPeriodID.Enabled = chkIsPayCut.Checked;

                        if (entity.VerifiedDateTime == null)
                        {
                            txtVerifiedDateTime.SelectedDate = (new DateTime()).NowAtSqlServer();
                            txtApprovedLeaveDateFrom.SelectedDate = txtRequestLeaveDateFrom.SelectedDate;
                            txtApprovedLeaveDateTo.SelectedDate = txtRequestLeaveDateTo.SelectedDate;
                            txtApprovedDays.Value = txtRequestDays.Value;
                            txtApprovedWorkingDate.SelectedDate = txtRequestWorkingDate.SelectedDate;
                        }
                    }
                }
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeeLeaveRequest();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtLeaveRequestID.Text)))
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
            if (txtStartDate.SelectedDate > txtEndDate.SelectedDate)
            {
                args.MessageText = "Invalid Request Leave Date. Request Leave Date To must be greater than Request Leave From.";
                args.IsCancel = true;
                return;
            }

            if (txtRequestDays.Value <= 0)
            {
                args.MessageText = "Request Days can't be less than 1.";
                args.IsCancel = true;
                return;
            }

            if (FormType == "request" || FormType == "request2")
            {
                if (txtRequestDays.Value > txtBalace.Value)
                {
                    args.MessageText = "Request Days should not be more than the balance.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (FormType == "verif" && txtApprovedDays.Value > txtBalace.Value + txtPendingQty.Value)
            {
                args.MessageText = "Approved Leave Days should not be more than the balance.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeeLeaveRequest();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (txtStartDate.SelectedDate > txtEndDate.SelectedDate)
            {
                args.MessageText = "Invalid Request Leave Date. Request Leave Date To must be greater than Request Leave From.";
                args.IsCancel = true;
                return;
            }

            if (txtRequestDays.Value <= 0)
            {
                args.MessageText = "Request Days can't be less than 1.";
                args.IsCancel = true;
                return;
            }

            if (FormType == "request" || FormType == "request2")
            {
                if (txtRequestDays.Value > txtBalace.Value)
                {
                    args.MessageText = "Request Days should not be more than the balance.";
                    args.IsCancel = true;
                    return;
                }

            }

            if (FormType == "verif" && txtApprovedDays.Value > txtBalace.Value + txtPendingQty.Value)
            {
                args.MessageText = "Approved Leave Days should not be more than the balance.";
                args.IsCancel = true;
                return;
            }

            if (FormType != "request" && FormType != "request2" && rblLeaveRequestStatus.SelectedIndex == 1 && string.IsNullOrEmpty(txtRejectedReason.Text))
            {
                args.MessageText = "Rejected Reason required.";
                args.IsCancel = true;
                return;
            }

            if (FormType == "verif")
            {
                if (txtApprovedLeaveDateFrom.IsEmpty || txtApprovedLeaveDateTo.IsEmpty)
                {
                    args.MessageText = "Verification Leave Date required.";
                    args.IsCancel = true;
                    return;
                }
                if (chkIsPayCut.Checked)
                {
                    if (txtPayCutDays.Value <= 0)
                    {
                        args.MessageText = "Pay Cut Days required.";
                        args.IsCancel = true;
                        return;
                    }
                    if (txtPayCutDays.Value > txtApprovedDays.Value)
                    {
                        args.MessageText = "Invalid Pay Cut Days. Pay Cut Days can't be greater than Leave Days";
                        args.IsCancel = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(cboSRWorkingDay.SelectedValue))
                    {
                        args.MessageText = "Working Day required.";
                        args.IsCancel = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                    {
                        args.MessageText = "Payroll Period required.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }

            var entity = new EmployeeLeaveRequest();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtLeaveRequestID.Text)))
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
            auditLogFilter.PrimaryKeyData = string.Format("LeaveRequestID='{0}'", txtLeaveRequestID.Text.ToString());
            auditLogFilter.TableName = "EmployeeLeaveRequest";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_LeaveRequestID", txtLeaveRequestID.Text);
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            if (chkIsPayCut.Checked && AppSession.Parameter.IsValidateEmployeeLeaveWithPayCutCantCrossMonth)
            {
                if (!(txtRequestLeaveDateFrom.SelectedDate.Value.Month == txtRequestLeaveDateTo.SelectedDate.Value.Month && txtRequestLeaveDateFrom.SelectedDate.Value.Year == txtRequestLeaveDateTo.SelectedDate.Value.Year))
                {
                    args.MessageText = "Leave Request period cannot cross the month.";
                    args.IsCancel = true;
                    return;
                }
            }

            var entity = new EmployeeLeaveRequest();
            entity.LoadByPrimaryKey(Convert.ToInt32(txtLeaveRequestID.Text));

            if (FormType == "appr" || FormType == "appr1" || FormType == "appr2")
            {
                if (entity.IsRequestApproved == null)
                {
                    args.MessageText = "Leave Request Status required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (FormType != "request" && FormType != "request2")
            {
                if (entity.IsRequestApproved == false && string.IsNullOrEmpty(txtRejectedReason.Text))
                {
                    args.MessageText = "Rejected Reason required.";
                    args.IsCancel = true;
                    return;
                }
            }

            if (FormType == "verif")
            {
                if (txtApprovedLeaveDateFrom.IsEmpty || txtApprovedLeaveDateTo.IsEmpty)
                {
                    args.MessageText = "Verification Leave Date required.";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                if (FormType == "request" || FormType == "request2")
                {
                    entity.IsApproved = true;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;

                    var emps = new VwEmployeeTable();
                    emps.Query.Where(emps.Query.PersonID == entity.PersonID);
                    if (emps.Query.Load() && emps.PersonID == emps.SupervisorId)
                    {
                        entity.IsRequestApproved = rblLeaveRequestStatus.SelectedIndex != 1;

                        if (AppSession.Parameter.HealthcareInitialAppsVersion != "YBRSGKP")
                        {
                            entity.IsValidated = true;
                            entity.ValidatedDateTime = (new DateTime()).NowAtSqlServer();
                            entity.ValidatedByUserID = AppSession.UserLogin.UserID;
                        }

                        if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "1")
                        {
                            entity.IsValidated1 = true;
                            entity.Validated1DateTime = (new DateTime()).NowAtSqlServer();
                            entity.Validated1ByUserID = AppSession.UserLogin.UserID;

                            entity.IsValidated2 = true;
                            entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                            entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                        }
                        else if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "2")
                        {
                            if (AppSession.Parameter.IsUsingPreceptorAsProfessionalIndirectSupervisor)
                            {
                                if (emps.PersonID == emps.PreceptorId)
                                {
                                    entity.IsValidated1 = true;
                                    entity.Validated1DateTime = (new DateTime()).NowAtSqlServer();
                                    entity.Validated1ByUserID = AppSession.UserLogin.UserID;

                                    entity.IsValidated2 = true;
                                    entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                                    entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                                }
                            }
                            else
                            {
                                var ou = new OrganizationUnit();
                                if (ou.LoadByPrimaryKey(emps.OrganizationUnitID.ToInt()) && ou.PersonID == emps.PersonID)
                                {
                                    entity.IsValidated1 = true;
                                    entity.Validated1DateTime = (new DateTime()).NowAtSqlServer();
                                    entity.Validated1ByUserID = AppSession.UserLogin.UserID;

                                    entity.IsValidated2 = true;
                                    entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                                    entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                                }
                            }
                        }
                        else if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "3")
                        {
                            if (entity.IsValidated == true && emps.PersonID == emps.ManagerID)
                            {
                                entity.IsValidated1 = true;
                                entity.Validated1DateTime = (new DateTime()).NowAtSqlServer();
                                entity.Validated1ByUserID = AppSession.UserLogin.UserID;

                                if (AppSession.Parameter.IsUsingPreceptorAsProfessionalIndirectSupervisor)
                                {
                                    if (emps.PersonID == emps.PreceptorId)
                                    {
                                        entity.IsValidated2 = true;
                                        entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                                        entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                                    }
                                }
                                else
                                {
                                    var ou = new OrganizationUnit();
                                    if (ou.LoadByPrimaryKey(emps.OrganizationUnitID.ToInt()) && ou.PersonID == emps.PersonID)
                                    {
                                        entity.IsValidated2 = true;
                                        entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                                        entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (FormType == "appr")
                {
                    entity.IsValidated = true;
                    entity.ValidatedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ValidatedByUserID = AppSession.UserLogin.UserID;

                    if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "1")
                    {
                        entity.IsValidated1 = true;
                        entity.Validated1DateTime = (new DateTime()).NowAtSqlServer();
                        entity.Validated1ByUserID = AppSession.UserLogin.UserID;

                        entity.IsValidated2 = true;
                        entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                        entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                    }
                    else if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "2")
                    {
                        var emps = new VwEmployeeTable();
                        emps.Query.Where(emps.Query.PersonID == entity.PersonID);
                        if (emps.Query.Load())
                        {
                            if (AppSession.Parameter.IsUsingPreceptorAsProfessionalIndirectSupervisor)
                            {
                                if (emps.SupervisorId == emps.PreceptorId || emps.ManagerID == emps.PreceptorId || emps.PreceptorId == -1)
                                {
                                    entity.IsValidated1 = true;
                                    entity.Validated1DateTime = (new DateTime()).NowAtSqlServer();
                                    entity.Validated1ByUserID = AppSession.UserLogin.UserID;

                                    entity.IsValidated2 = true;
                                    entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                                    entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                                }
                            }
                            else
                            {
                                var ou = new OrganizationUnit();
                                if (ou.LoadByPrimaryKey(emps.OrganizationUnitID.ToInt()) && (ou.PersonID == emps.SupervisorId || ou.PersonID == emps.ManagerID))
                                {
                                    entity.IsValidated1 = true;
                                    entity.Validated1DateTime = (new DateTime()).NowAtSqlServer();
                                    entity.Validated1ByUserID = AppSession.UserLogin.UserID;

                                    entity.IsValidated2 = true;
                                    entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                                    entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                                }
                            }
                        }
                    }
                    else if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "3")
                    {
                        var emps = new VwEmployeeTable();
                        emps.Query.Where(emps.Query.PersonID == entity.PersonID);
                        if (emps.Query.Load())
                        {
                            if (emps.SupervisorId == emps.ManagerID)
                            {
                                entity.IsValidated1 = true;
                                entity.Validated1DateTime = (new DateTime()).NowAtSqlServer();
                                entity.Validated1ByUserID = AppSession.UserLogin.UserID;

                                if (AppSession.Parameter.IsUsingPreceptorAsProfessionalIndirectSupervisor)
                                {
                                    if (emps.SupervisorId == emps.PreceptorId || emps.PreceptorId == -1)
                                    {
                                        entity.IsValidated2 = true;
                                        entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                                        entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                                    }
                                }
                                else
                                {
                                    var ou = new OrganizationUnit();
                                    if (ou.LoadByPrimaryKey(emps.OrganizationUnitID.ToInt()) && ou.PersonID == emps.SupervisorId)
                                    {
                                        entity.IsValidated2 = true;
                                        entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                                        entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (FormType == "appr1")
                {
                    entity.IsValidated1 = true;
                    entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                    entity.Validated2ByUserID = AppSession.UserLogin.UserID;

                    var emps = new VwEmployeeTable();
                    emps.Query.Where(emps.Query.PersonID == entity.PersonID);
                    if (emps.Query.Load())
                    {
                        if (AppSession.Parameter.IsUsingPreceptorAsProfessionalIndirectSupervisor)
                        {
                            if (emps.ManagerID == emps.PreceptorId || emps.PreceptorId == -1)
                            {
                                entity.IsValidated2 = true;
                                entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                                entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                            }
                        }
                        else
                        {
                            var ou = new OrganizationUnit();
                            if (ou.LoadByPrimaryKey(emps.OrganizationUnitID.ToInt()) && ou.PersonID == emps.ManagerID)
                            {
                                entity.IsValidated2 = true;
                                entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                                entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                            }
                        }
                    }
                }
                else if (FormType == "appr2")
                {
                    entity.IsValidated2 = true;
                    entity.Validated2DateTime = (new DateTime()).NowAtSqlServer();
                    entity.Validated2ByUserID = AppSession.UserLogin.UserID;
                }
                else if (FormType == "verif")
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
            var entity = new EmployeeLeaveRequest();
            entity.LoadByPrimaryKey(Convert.ToInt32(txtLeaveRequestID.Text));

            if (FormType == "request" || FormType == "request2")
            {
                if (entity.IsValidated == true)
                {
                    args.MessageText = "Leave Request already approved.";
                    args.IsCancel = true;
                    return;
                }
                if (entity.IsVerified == true)
                {
                    args.MessageText = "Leave Request already verified.";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "appr")
            {
                if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "1")
                {
                    if (entity.IsVerified == true)
                    {
                        args.MessageText = "Leave Request already verified.";
                        args.IsCancel = true;
                        return;
                    }
                }
                else if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "2")
                {
                    if (entity.IsValidated2 == true)
                    {
                        args.MessageText = "Leave Request already have #2 validated.";
                        args.IsCancel = true;
                        return;
                    }
                }
                else if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "3")
                {
                    if (entity.IsValidated1 == true)
                    {
                        args.MessageText = "Leave Request already have #1 validated.";
                        args.IsCancel = true;
                        return;
                    }
                }
            }
            else if (FormType == "appr1")
            {
                if (entity.IsValidated2 == true)
                {
                    args.MessageText = "Leave Request already have #2 validated.";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "appr2")
            {
                if (entity.IsVerified == true)
                {
                    args.MessageText = "Leave Request already verified.";
                    args.IsCancel = true;
                    return;
                }
            }
            else if (FormType == "verif" & entity.IsPayCut == true)
            {
                var cwt = new ClosingWageTransaction();
                if (cwt.LoadByPrimaryKey(entity.PayrollPeriodID.ToInt()) & cwt.IsClosed == true)
                {
                    var period = string.Empty;
                    var pp = new PayrollPeriod();
                    if (pp.LoadByPrimaryKey(entity.PayrollPeriodID.ToInt()))
                        period = pp.PayrollPeriodName;

                    args.MessageText = "Payroll for period: " + period + " have been closed.";
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
                else if (FormType == "appr2")
                {
                    entity.IsValidated2 = false;
                    entity.Validated2DateTime = null;
                    entity.Validated2ByUserID = null;
                }
                else if (FormType == "appr1")
                {
                    entity.IsValidated1 = false;
                    entity.Validated1DateTime = null;
                    entity.Validated1ByUserID = null;
                }
                else if (FormType == "appr")
                {
                    entity.IsValidated = false;
                    entity.ValidatedDateTime = null;
                    entity.ValidatedByUserID = null;
                    if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "1")
                    {
                        entity.IsValidated1 = false;
                        entity.Validated1DateTime = null;
                        entity.Validated1ByUserID = null;

                        entity.IsValidated2 = false;
                        entity.Validated2DateTime = null;
                        entity.Validated2ByUserID = null;
                    }
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
            var entity = new EmployeeLeaveRequest();
            if (!entity.LoadByPrimaryKey(Convert.ToInt32(txtLeaveRequestID.Text)))
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

            if (FormType != "request" && FormType != "request2" && FormType != "verif")
            {
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = "This data can not be void because it has been approved by the requesting user.";
                    args.IsCancel = true;
                    return;
                }
            }

            SetVoid(entity, true);
        }

        private void SetVoid(EmployeeLeaveRequest entity, bool isVoid)
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
            var entity = new EmployeeLeaveRequest();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtLeaveRequestID.Text)))
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

        private bool IsApprovedOrVoid(EmployeeLeaveRequest entity, ValidateArgs args)
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
            else if (FormType == "appr2")
            {
                if (entity.IsValidated2 ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                    return false;
                }
            }
            else if (FormType == "appr1")
            {
                if (entity.IsValidated1 ?? false)
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

        private bool IsApproved(EmployeeLeaveRequest entity, ValidateArgs args)
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

            if (FormType == "request" || FormType == "request2")
            {
                rblLeaveRequestStatus.Enabled = false;
                txtRejectedReason.ReadOnly = true;
            }
            if (FormType != "verif")
            {
                if (FormType != "request2")
                    cboPersonID.Enabled = false;

                txtVerifiedDateTime.Enabled = false;
                txtApprovedLeaveDateFrom.Enabled = false;
                txtApprovedLeaveDateTo.Enabled = false;
                txtApprovedDays.ReadOnly = true;
                txtApprovedWorkingDate.Enabled = false;

                chkIsPayCut.Enabled = false;
                txtPayCutDays.ReadOnly = true;
                cboSRWorkingDay.Enabled = false;
                cboPayrollPeriodID.Enabled = false;
            }

            if (FormType != "request" && FormType != "request2")
                ToolBarMenuSearch.Enabled = false;
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
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeLeaveRequest();
            if (parameters.Length > 0)
            {
                string leaveRequestId = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(leaveRequestId));

                txtLeaveRequestID.Text = entity.EmployeeLeaveID.ToString();
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtLeaveRequestID.Text));
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var employeeLeaveRequest = (EmployeeLeaveRequest)entity;

            txtLeaveRequestID.Text = employeeLeaveRequest.LeaveRequestID.ToString();
            txtRequestDate.SelectedDate = employeeLeaveRequest.RequestDate;

            if (!string.IsNullOrEmpty(employeeLeaveRequest.PersonID.ToString()))
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(employeeLeaveRequest.PersonID));
                var dtb = personal.LoadDataTable();
                cboPersonID.DataSource = dtb;
                cboPersonID.DataBind();
                cboPersonID.SelectedValue = employeeLeaveRequest.PersonID.ToString();
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    cboPersonID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(employeeLeaveRequest.EmployeeLeaveID.ToString()))
            {
                var empLeave = new EmployeeLeaveQuery("a");
                var leaveType = new AppStandardReferenceItemQuery("b");
                empLeave.InnerJoin(leaveType).On(empLeave.SREmployeeLeaveType == leaveType.ItemID &&
                                         leaveType.StandardReferenceID == AppEnum.StandardReference.EmployeeLeaveType);
                empLeave.Select(
                    empLeave.EmployeeLeaveID,
                    leaveType.ItemName.As("EmployeeLeaveTypeName"),
                    @"<'Period: ' + CONVERT(VARCHAR, a.StartDate, 106) + ' - ' + CONVERT(VARCHAR, a.EndDate, 106) AS Period>",
                    empLeave.Notes);

                empLeave.Where(empLeave.EmployeeLeaveID == Convert.ToInt32(employeeLeaveRequest.EmployeeLeaveID));
                var dtb = empLeave.LoadDataTable();
                cboEmployeeLeaveID.DataSource = dtb;
                cboEmployeeLeaveID.DataBind();
                cboEmployeeLeaveID.SelectedValue = employeeLeaveRequest.EmployeeLeaveID.ToString();
                if (!string.IsNullOrEmpty(cboEmployeeLeaveID.SelectedValue))
                {
                    string txt;
                    if (!string.IsNullOrEmpty(dtb.Rows[0]["Notes"].ToString()))
                        txt = " [" + dtb.Rows[0]["Notes"] + "]";
                    else
                        txt = " [" + dtb.Rows[0]["Period"] + "]";

                    cboEmployeeLeaveID.Text = dtb.Rows[0]["EmployeeLeaveTypeName"] + txt;
                }

                PopulateEmployeeLeaveInformation(employeeLeaveRequest.EmployeeLeaveID.ToString());
            }
            else
            {
                cboEmployeeLeaveID.Items.Clear();
                cboEmployeeLeaveID.Text = string.Empty;

                txtStartDate.Clear();
                txtEndDate.Clear();
                txtLeaveEntitlementsQty.Value = 0;
                txtAlreadyTakenQty.Value = 0;
                txtPendingQty.Value = 0;
                txtBalace.Value = 0;
            }

            txtRequestLeaveDateFrom.SelectedDate = employeeLeaveRequest.RequestLeaveDateFrom;
            txtRequestLeaveDateTo.SelectedDate = employeeLeaveRequest.RequestLeaveDateTo;
            txtRequestDays.Value = Convert.ToDouble(employeeLeaveRequest.RequestDays);
            txtRequestWorkingDate.SelectedDate = employeeLeaveRequest.RequestWorkingDate;
            txtNotes.Text = employeeLeaveRequest.Notes;

            if (!string.IsNullOrEmpty(employeeLeaveRequest.ReplacementPersonID.ToString()) && employeeLeaveRequest.ReplacementPersonID.ToString() != "0")
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(employeeLeaveRequest.ReplacementPersonID));
                var dtb = personal.LoadDataTable();
                cboReplacementPersonID.DataSource = dtb;
                cboReplacementPersonID.DataBind();
                cboReplacementPersonID.SelectedValue = employeeLeaveRequest.ReplacementPersonID.ToString();
                if (!string.IsNullOrEmpty(cboReplacementPersonID.SelectedValue))
                    cboReplacementPersonID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboReplacementPersonID.Items.Clear();
                cboReplacementPersonID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(employeeLeaveRequest.ValidatedByUserID))
            {
                var usr = new AppUser();
                if (usr.LoadByPrimaryKey(employeeLeaveRequest.ValidatedByUserID))
                {
                    txtValidatedDate.SelectedDate = employeeLeaveRequest.ValidatedDateTime;
                    txtValidatedBy.Text = usr.UserName;
                }
                else
                {
                    txtValidatedDate.SelectedDate = null;
                    txtValidatedBy.Text = string.Empty;
                }
            }
            else
            {
                txtValidatedDate.SelectedDate = null;
                txtValidatedBy.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(employeeLeaveRequest.Validated1ByUserID))
            {
                var usr = new AppUser();
                if (usr.LoadByPrimaryKey(employeeLeaveRequest.Validated1ByUserID))
                {
                    txtValidated1Date.SelectedDate = employeeLeaveRequest.Validated1DateTime;
                    txtValidated1By.Text = usr.UserName;
                }
                else
                {
                    txtValidated1Date.SelectedDate = null;
                    txtValidated1By.Text = string.Empty;
                }
            }
            else
            {
                txtValidated1Date.SelectedDate = null;
                txtValidated1By.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(employeeLeaveRequest.Validated2ByUserID))
            {
                var usr = new AppUser();
                if (usr.LoadByPrimaryKey(employeeLeaveRequest.Validated2ByUserID))
                {
                    txtValidated2Date.SelectedDate = employeeLeaveRequest.Validated2DateTime;
                    txtValidated2By.Text = usr.UserName;
                }
                else
                {
                    txtValidated2Date.SelectedDate = null;
                    txtValidated2By.Text = string.Empty;
                }
            }
            else
            {
                txtValidated2Date.SelectedDate = null;
                txtValidated2By.Text = string.Empty;
            }

            txtVerifiedDateTime.SelectedDate = employeeLeaveRequest.VerifiedDateTime;
            if (employeeLeaveRequest.IsRequestApproved != null)
                rblLeaveRequestStatus.SelectedIndex = employeeLeaveRequest.IsRequestApproved == true ? 0 : 1;
            txtRejectedReason.Text = employeeLeaveRequest.RejectedReason;

            txtApprovedLeaveDateFrom.SelectedDate = employeeLeaveRequest.ApprovedLeaveDateFrom;
            txtApprovedLeaveDateTo.SelectedDate = employeeLeaveRequest.ApprovedLeaveDateTo;
            txtApprovedDays.Value = Convert.ToDouble(employeeLeaveRequest.ApprovedDays);
            txtApprovedWorkingDate.SelectedDate = employeeLeaveRequest.ApprovedWorkingDate;

            chkIsPayCut.Checked = employeeLeaveRequest.IsPayCut ?? false;
            txtPayCutDays.Value = Convert.ToDouble(employeeLeaveRequest.PayCutDays);
            cboSRWorkingDay.SelectedValue = employeeLeaveRequest.SRWorkingDay;
            if (!string.IsNullOrEmpty(employeeLeaveRequest.PayrollPeriodID.ToString()) && employeeLeaveRequest.PayrollPeriodID != -1)
            {
                var pp = new PayrollPeriodQuery("a");
                pp.Select(pp.PayrollPeriodID, pp.PayrollPeriodCode, pp.PayrollPeriodName);
                pp.Where(pp.PayrollPeriodID == Convert.ToInt32(employeeLeaveRequest.PayrollPeriodID));
                var dtb = pp.LoadDataTable();
                cboPayrollPeriodID.DataSource = dtb;
                cboPayrollPeriodID.DataBind();
                cboPayrollPeriodID.SelectedValue = employeeLeaveRequest.PayrollPeriodID.ToString();
                if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                    cboPayrollPeriodID.Text = dtb.Rows[0]["PayrollPeriodCode"].ToString() + " - " + dtb.Rows[0]["PayrollPeriodName"].ToString();
            }
            else
            {
                cboPayrollPeriodID.Items.Clear();
                cboPayrollPeriodID.Text = string.Empty;
            }

            if (FormType == "request" || FormType == "request2")
                chkIsApproved.Checked = employeeLeaveRequest.IsApproved ?? false;
            else if (FormType == "appr")
                chkIsApproved.Checked = employeeLeaveRequest.IsValidated ?? false;
            else if (FormType == "appr1")
                chkIsApproved.Checked = employeeLeaveRequest.IsValidated1 ?? false;
            else if (FormType == "appr2")
                chkIsApproved.Checked = employeeLeaveRequest.IsValidated2 ?? false;
            else
                chkIsApproved.Checked = employeeLeaveRequest.IsVerified ?? false;

            chkIsVoid.Checked = employeeLeaveRequest.IsVoid ?? false;
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(EmployeeLeaveRequest entity)
        {
            if (entity.es.IsModified)
                entity.EmployeeLeaveID = Convert.ToInt32(txtLeaveRequestID.Text);
            entity.RequestDate = txtRequestDate.SelectedDate;
            entity.PersonID = Convert.ToInt32(cboPersonID.SelectedValue);
            entity.EmployeeLeaveID = Convert.ToInt32(cboEmployeeLeaveID.SelectedValue);
            entity.RequestLeaveDateFrom = txtRequestLeaveDateFrom.SelectedDate;
            entity.RequestLeaveDateTo = txtRequestLeaveDateTo.SelectedDate;
            entity.RequestDays = Convert.ToInt32(txtRequestDays.Value);
            entity.RequestWorkingDate = txtRequestWorkingDate.SelectedDate;
            entity.Notes = txtNotes.Text;
            if (string.IsNullOrEmpty(cboReplacementPersonID.SelectedValue))
                entity.ReplacementPersonID = 0;
            else
                entity.ReplacementPersonID = Convert.ToInt32(cboReplacementPersonID.SelectedValue);
            entity.RejectedReason = txtRejectedReason.Text;
            entity.VerifiedDateTime = txtVerifiedDateTime.SelectedDate;

            if (FormType != "request" && FormType != "request2")
            {
                entity.IsRequestApproved = rblLeaveRequestStatus.SelectedIndex != 1;
            }

            entity.ApprovedLeaveDateFrom = txtApprovedLeaveDateFrom.SelectedDate;
            entity.ApprovedLeaveDateTo = txtApprovedLeaveDateTo.SelectedDate;
            entity.ApprovedDays = Convert.ToInt32(txtApprovedDays.Value);
            entity.ApprovedWorkingDate = txtApprovedWorkingDate.SelectedDate;

            entity.IsPayCut = chkIsPayCut.Checked;
            entity.PayCutDays = Convert.ToInt32(txtPayCutDays.Value);
            entity.SRWorkingDay = cboSRWorkingDay.SelectedValue;
            if (FormType == "verif" && chkIsPayCut.Checked)
                entity.PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue);
            else
                entity.PayrollPeriodID = -1;

            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            }

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        }

        private void SaveEntity(EmployeeLeaveRequest entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                try
                {
                    txtLeaveRequestID.Text = entity.LeaveRequestID.ToString();
                }
                catch
                { }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeLeaveRequestQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.LeaveRequestID > Convert.ToInt32(txtLeaveRequestID.Text));
                que.OrderBy(que.LeaveRequestID.Ascending);
            }
            else
            {
                que.Where(que.LeaveRequestID < Convert.ToInt32(txtLeaveRequestID.Text));
                que.OrderBy(que.LeaveRequestID.Descending);
            }
            if (FormType == "request" || FormType == "request2")
                que.Where(que.CreatedByUserID == AppSession.UserLogin.UserID);
            else if (FormType == "appr")
            {
                var personal = new VwEmployeeTableQuery("b");
                var usrs = new AppUserQuery("usrs");
                que.InnerJoin(usrs).On(usrs.UserID == AppSession.UserLogin.UserID && usrs.PersonID == personal.SupervisorId);
                que.Where(que.IsApproved == true);
            }
            else
                que.Where(que.IsValidated == true);

            var entity = new EmployeeLeaveRequest();
            entity.Load(que);

            txtLeaveRequestID.Text = entity.LeaveRequestID.ToString();

            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Override Method & Function
        private void PopulateEmployeeLeaveInformation(string employeeLeaveId)
        {
            if (!string.IsNullOrEmpty(employeeLeaveId))
            {
                var el = new EmployeeLeave();
                if (el.LoadByPrimaryKey(employeeLeaveId.ToInt()))
                {
                    txtStartDate.SelectedDate = el.StartDate;
                    txtEndDate.SelectedDate = el.EndDate;
                    txtLeaveEntitlementsQty.Value = Convert.ToDouble(el.LeaveEntitlementsQty);

                    var elrColl = new EmployeeLeaveRequestCollection();
                    elrColl.Query.Where(elrColl.Query.EmployeeLeaveID == el.EmployeeLeaveID,
                                        elrColl.Query.IsVerified == true, elrColl.Query.IsRequestApproved == true);
                    elrColl.LoadAll();

                    int taken = elrColl.Sum(elr => elr.ApprovedDays ?? 0);

                    elrColl = new EmployeeLeaveRequestCollection();
                    elrColl.Query.Where(elrColl.Query.EmployeeLeaveID == el.EmployeeLeaveID,
                                        elrColl.Query.IsApproved == true,
                                        elrColl.Query.Or(elrColl.Query.IsRequestApproved.IsNull(), elrColl.Query.IsRequestApproved == true),
                                        elrColl.Query.IsVerified.IsNull()
                                        );
                    elrColl.LoadAll();

                    int pending = elrColl.Sum(elr => elr.RequestDays ?? 0);

                    txtAlreadyTakenQty.Value = Convert.ToDouble(taken);
                    txtPendingQty.Value = Convert.ToDouble(pending);
                    txtBalace.Value = txtLeaveEntitlementsQty.Value - txtAlreadyTakenQty.Value - txtPendingQty.Value;
                    chkIsPayCut.Checked = el.IsPayCut ?? false;
                }
                else
                {
                    txtStartDate.Clear();
                    txtEndDate.Clear();
                    txtLeaveEntitlementsQty.Value = 0;
                    txtAlreadyTakenQty.Value = 0;
                    txtPendingQty.Value = 0;
                    txtBalace.Value = 0;
                    chkIsPayCut.Checked = false;
                }
            }
            else
            {
                txtStartDate.Clear();
                txtEndDate.Clear();
                txtLeaveEntitlementsQty.Value = 0;
                txtAlreadyTakenQty.Value = 0;
                txtPendingQty.Value = 0;
                txtBalace.Value = 0;
                chkIsPayCut.Checked = false;
            }
        }
        #endregion

        #region Combobox
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
                (
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
            cboEmployeeLeaveID.Items.Clear();
            cboEmployeeLeaveID.Text = string.Empty;
        }

        protected void cboEmployeeLeaveID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var personId = "-1";
            if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                personId = cboPersonID.SelectedValue;

            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new EmployeeLeaveQuery("a");
            var leaveType = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(leaveType).On(query.SREmployeeLeaveType == leaveType.ItemID &&
                                     leaveType.StandardReferenceID == AppEnum.StandardReference.EmployeeLeaveType);
            query.es.Top = 20;
            query.Select
                (
                    query.EmployeeLeaveID,
                    query.SREmployeeLeaveType,
                    query.StartDate,
                    query.EndDate,
                    leaveType.ItemName.As("EmployeeLeaveTypeName"),
                    query.LeaveEntitlementsQty,
                    @"<'Period: ' + CONVERT(VARCHAR, a.StartDate, 106) + ' - ' + CONVERT(VARCHAR, a.EndDate, 106) AS Period>",
                    query.Notes,
                    @"<0 AS Balance>"
                );

            query.Where
                (
                    query.PersonID == personId,
                    query.Or
                        (
                            leaveType.ItemName.Like(searchTextContain),
                            leaveType.ItemName.Like(searchTextContain)
                        )
                );

            query.OrderBy(query.StartDate.Ascending);

            DataTable dtb = query.LoadDataTable();
            foreach (DataRow row in dtb.Rows)
            {
                if (row["SREmployeeLeaveType"].ToString() == AppSession.Parameter.EmployeeLeaveAnnualLeave && Convert.ToDateTime(row["EndDate"]) < DateTime.Now.Date)
                {
                    row.Delete();
                }
                else
                {
                    var request = new EmployeeLeaveRequestCollection();
                    request.Query.Where(request.Query.EmployeeLeaveID == Convert.ToInt32(row["EmployeeLeaveID"]),
                                  request.Query.IsVerified == true, request.Query.IsRequestApproved == true);
                    request.LoadAll();
                    double approvedDays = request.Aggregate<EmployeeLeaveRequest, double>(0, (current, x) => current + (x.ApprovedDays ?? 0));

                    if (approvedDays >= Convert.ToDouble(row["LeaveEntitlementsQty"]))
                        row.Delete();
                    else
                        row["Balance"] = Convert.ToDouble(row["LeaveEntitlementsQty"]) - approvedDays;
                }
            }

            dtb.AcceptChanges();

            cboEmployeeLeaveID.DataSource = dtb;
            cboEmployeeLeaveID.DataBind();
        }

        protected void cboEmployeeLeaveID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            string txt;
            if (!string.IsNullOrEmpty(((DataRowView)e.Item.DataItem)["Notes"].ToString()))
                txt = " [" + ((DataRowView)e.Item.DataItem)["Notes"] + "]";
            else
                txt = " [" + ((DataRowView)e.Item.DataItem)["Period"] + "]";

            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeLeaveTypeName"] + txt;
            e.Item.Value = ((DataRowView)e.Item.DataItem)["EmployeeLeaveID"].ToString();
        }

        protected void cboEmployeeLeaveID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulateEmployeeLeaveInformation(e.Value);
        }

        protected void rblLeaveRequestStatus_OnTextChanged(object sender, EventArgs e)
        {
            if (rblLeaveRequestStatus.SelectedIndex == 1)
            {
                txtApprovedLeaveDateFrom.SelectedDate = txtRequestLeaveDateFrom.SelectedDate;
                txtApprovedLeaveDateTo.SelectedDate = txtRequestLeaveDateTo.SelectedDate;
                txtApprovedDays.Value = txtRequestDays.Value;
                txtApprovedWorkingDate.SelectedDate = txtRequestWorkingDate.SelectedDate;
            }
            else
            {
                txtApprovedLeaveDateFrom.Clear();
                txtApprovedLeaveDateTo.Clear();
                txtApprovedDays.Value = 0;
                txtApprovedWorkingDate.Clear();
            }
        }

        protected void cboReplacementPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
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
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboReplacementPersonID.DataSource = query.LoadDataTable();
            cboReplacementPersonID.DataBind();
        }

        protected void cboReplacementPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
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

        protected void chkIsPayCut_CheckedChanged(object sender, EventArgs e)
        {
            txtPayCutDays.ReadOnly = !chkIsPayCut.Checked;
            cboSRWorkingDay.Enabled = chkIsPayCut.Checked;
            cboPayrollPeriodID.Enabled = chkIsPayCut.Checked;
            txtPayCutDays.Value = 0;
            cboSRWorkingDay.SelectedValue = string.Empty;
            cboSRWorkingDay.Text = string.Empty;
            cboPayrollPeriodID.Items.Clear();
            cboPayrollPeriodID.Text = string.Empty;
        }
        #endregion

    }
}
