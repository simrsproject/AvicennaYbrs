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
    public partial class EmployeeLeaveDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "EmployeeLeaveSearch.aspx";
            UrlPageList = "EmployeeLeaveList.aspx";

            ProgramID = AppConstant.Program.EmployeeLeave;

            WindowSearch.Height = 300;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmployeeLeaveType, AppEnum.StandardReference.EmployeeLeaveType);
                trIsPayCut.Visible = AppSession.Parameter.IsEmployeeLeavePayCutVisible;
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeLeave());
            txtEmployeeLeaveID.Text = "0";

            cboPersonID.Text = string.Empty;
            cboSREmployeeLeaveType.SelectedValue = string.Empty;
            cboSREmployeeLeaveType.Text = string.Empty;
        }

        protected override void OnMenuEditClick()
        {
            cboPersonID.Enabled = false;
            cboSREmployeeLeaveType.Enabled = false;

            var entity = new EmployeeLeave();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeLeaveID.Text)) && entity.SREmployeeLeaveType == AppSession.Parameter.EmployeeLeaveAnnualLeave)
            {
                var elrColl = new EmployeeLeaveRequestCollection();
                elrColl.Query.Where(elrColl.Query.EmployeeLeaveID == txtEmployeeLeaveID.Text.ToInt());
                elrColl.LoadAll();
                if (elrColl.Count > 0)
                {
                    txtNotes.ReadOnly = true;
                    chkIsPayCut.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtLeaveEntitlementsQty.ReadOnly = true;
                }
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeeLeave();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeLeaveID.Text)))
            {
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
            if (txtStartDate.SelectedDate > txtEnddate.SelectedDate)
            {
                args.MessageText = "Invalid Period. The start date cannot be less than the end date.";
                args.IsCancel = true;
            }

            var entity = new EmployeeLeave();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (txtStartDate.SelectedDate > txtEnddate.SelectedDate)
            {
                args.MessageText = "Invalid Period. The start date cannot be less than the end date.";
                args.IsCancel = true;
            }

            var entity = new EmployeeLeave();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeLeaveID.Text)))
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

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var elrColl = new EmployeeLeaveRequestCollection();
            elrColl.Query.Where(elrColl.Query.EmployeeLeaveID == txtEmployeeLeaveID.Text.ToInt());
            elrColl.LoadAll();
            if (elrColl.Count > 0)
            {
                if (!AppSession.Parameter.IsAllowEditEmployeeAnnualLeaveEndPeriod || cboSREmployeeLeaveType.SelectedValue != AppSession.Parameter.EmployeeLeaveAnnualLeave)
                {
                    args.MessageText = "Data is used to emlpoyee leave request.";
                    args.IsCancel = true;
                    return;
                }

                //elrColl = new EmployeeLeaveRequestCollection();
                //elrColl.Query.Where(elrColl.Query.EmployeeLeaveID == txtEmployeeLeaveID.Text.ToInt(),
                //                    elrColl.Query.IsVerified == true, elrColl.Query.IsRequestApproved == true);
                //elrColl.LoadAll();

                //int taken = elrColl.Sum(elr => elr.ApprovedDays ?? 0);

                //elrColl = new EmployeeLeaveRequestCollection();
                //elrColl.Query.Where(elrColl.Query.EmployeeLeaveID == txtEmployeeLeaveID.Text.ToInt(),
                //                    elrColl.Query.IsApproved == true,
                //                    elrColl.Query.Or(elrColl.Query.IsRequestApproved.IsNull(), elrColl.Query.IsRequestApproved == true),
                //                    elrColl.Query.IsVerified.IsNull()
                //                    );
                //elrColl.LoadAll();

                //int pending = elrColl.Sum(elr => elr.RequestDays ?? 0);

                //if (txtLeaveEntitlementsQty.Value <= Convert.ToDouble(taken + pending))
                //{
                //    args.MessageText = "Annual leave entitlements has no balance.";
                //    args.IsCancel = true;
                //    return;
                //}
            }
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("EmployeeLeaveID='{0}'", txtEmployeeLeaveID.Text);
            auditLogFilter.TableName = "EmployeeLeave";
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeLeave();
            if (parameters.Length > 0)
            {
                string employeeLeaveId = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(employeeLeaveId));

                txtEmployeeLeaveID.Text = entity.EmployeeLeaveID.ToString();
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeLeaveID.Text));
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var employeeLeave = (EmployeeLeave)entity;

            txtEmployeeLeaveID.Text = employeeLeave.EmployeeLeaveID.ToString();
            if (!string.IsNullOrEmpty(employeeLeave.PersonID.ToString()))
            {
                var personal = new VwEmployeeTableQuery();
                personal.Where(personal.PersonID == Convert.ToInt32(employeeLeave.PersonID));
                var dtb = personal.LoadDataTable();
                cboPersonID.DataSource = dtb;
                cboPersonID.DataBind();
                cboPersonID.SelectedValue = employeeLeave.PersonID.ToString();
                if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                    cboPersonID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.Text = string.Empty;
            }

            cboSREmployeeLeaveType.SelectedValue = employeeLeave.SREmployeeLeaveType;
            txtStartDate.SelectedDate = employeeLeave.StartDate;
            txtEnddate.SelectedDate = employeeLeave.EndDate;
            txtLeaveEntitlementsQty.Value = Convert.ToDouble(employeeLeave.LeaveEntitlementsQty);
            txtNotes.Text = employeeLeave.Notes;
            chkIsPayCut.Checked = employeeLeave.IsPayCut ?? false;

            if (!string.IsNullOrEmpty(employeeLeave.EmployeeLeaveID.ToString()))
            {
                var elrColl = new EmployeeLeaveRequestCollection();
                elrColl.Query.Where(elrColl.Query.EmployeeLeaveID == employeeLeave.EmployeeLeaveID, elrColl.Query.IsVerified == true, elrColl.Query.IsRequestApproved == true);
                elrColl.LoadAll();
                double taken = elrColl.Aggregate<EmployeeLeaveRequest, double>(0, (current, x) => current + (x.ApprovedDays ?? 0));

                elrColl = new EmployeeLeaveRequestCollection();
                elrColl.Query.Where(elrColl.Query.EmployeeLeaveID == employeeLeave.EmployeeLeaveID,
                                    elrColl.Query.IsApproved == true,
                                    elrColl.Query.Or(elrColl.Query.IsRequestApproved.IsNull(), elrColl.Query.IsRequestApproved == true),
                                    elrColl.Query.IsVerified.IsNull()
                                    );
                elrColl.LoadAll();

                double pending = elrColl.Sum(elr => elr.RequestDays ?? 0);

                txtAlreadyTakenQty.Value = taken;
                txtPendingQty.Value = pending;
                txtBalance.Value = txtLeaveEntitlementsQty.Value - taken - pending;
            }
            else
            {
                txtAlreadyTakenQty.Value = 0;
                txtPendingQty.Value = 0;
                txtBalance.Value = 0;
            }
                

            grdList.Rebind();
        }

        private void SetEntityValue(EmployeeLeave entity)
        {
            if (entity.es.IsModified)
                entity.EmployeeLeaveID = Convert.ToInt32(txtEmployeeLeaveID.Text);
            entity.PersonID = Convert.ToInt32(cboPersonID.SelectedValue);
            entity.SREmployeeLeaveType = cboSREmployeeLeaveType.SelectedValue;
            entity.StartDate = txtStartDate.SelectedDate;
            entity.EndDate = txtEnddate.SelectedDate;
            entity.LeaveEntitlementsQty = Convert.ToInt32(txtLeaveEntitlementsQty.Value);
            entity.Notes = txtNotes.Text;
            entity.IsPayCut = chkIsPayCut.Checked;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
        }

        private void SaveEntity(EmployeeLeave entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                try
                {
                    txtEmployeeLeaveID.Text = entity.EmployeeLeaveID.ToString();
                }
                catch
                { }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeLeaveQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.EmployeeLeaveID > Convert.ToInt32(txtEmployeeLeaveID.Text));
                que.OrderBy(que.EmployeeLeaveID.Ascending);
            }
            else
            {
                que.Where(que.EmployeeLeaveID < Convert.ToInt32(txtEmployeeLeaveID.Text));
                que.OrderBy(que.EmployeeLeaveID.Descending);
            }
            var entity = new EmployeeLeave();
            entity.Load(que);

            txtEmployeeLeaveID.Text = entity.EmployeeLeaveID.ToString();

            OnPopulateEntryControl(entity);
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

        #region Leave Item

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = GetEmployeeLeaveHistory();
        }

        private DataTable GetEmployeeLeaveHistory()
        {
            var query = new EmployeeLeaveRequestQuery("a");

            query.Select(
                query.LeaveRequestID,
                query.RequestDate,
                query.PersonID,
                query.EmployeeLeaveID,
                query.RequestLeaveDateFrom,
                query.RequestLeaveDateTo,
                query.RequestDays,
                query.RequestWorkingDate,
                query.Notes,
                query.CreatedDateTime,
                query.CreatedByUserID,
                query.IsApproved,
                query.ApprovedDateTime,
                query.ApprovedByUserID,
                query.IsVoid,
                query.VoidDateTime,
                query.VoidByUserID,
                query.IsRequestApproved,
                query.ApprovedLeaveDateFrom,
                query.ApprovedLeaveDateTo,
                query.ApprovedDays,
                query.ApprovedWorkingDate,
                query.IsVerified,
                query.VerifiedDateTime,
                query.VerifiedByUserID,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                );

            query.Where(query.EmployeeLeaveID == txtEmployeeLeaveID.Text.ToInt(),
                        query.IsApproved == true,
                        query.Or(query.IsVoid.IsNull(), query.IsVoid == false),
                        query.Or(query.IsRequestApproved.IsNull(), query.IsRequestApproved == true));

            query.OrderBy
                (
                    query.LeaveRequestID.Ascending
                );

            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        #endregion
    }
}
