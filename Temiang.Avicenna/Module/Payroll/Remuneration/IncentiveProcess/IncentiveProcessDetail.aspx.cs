using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Remuneration
{
    public partial class IncentiveProcessDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "IncentiveProcessSearch.aspx";
            UrlPageList = "IncentiveProcessList.aspx";

            this.WindowSearch.Height = 400;
            ProgramID = AppConstant.Program.EmployeeIncentiveProcess;

            if (IsPostBack) return;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, grdItem);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }

        protected override void OnMenuEditClick()
        {
            cboPayrollPeriodID.Enabled = false;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeIncentiveProcess());
            txtEmployeeIncentiveProcessID.Value = -1;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new EmployeeIncentiveProcess();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeIncentiveProcessID.Text)))
            {
                if (!IsApproved(entity, args))
                    return;

                var items = new EmployeeIncentiveProcessItemCollection();
                items.Query.Where(items.Query.EmployeeIncentiveProcessID == Convert.ToInt32(txtEmployeeIncentiveProcessID.Text));
                items.LoadAll();
                items.MarkAllAsDeleted();

                var itemDets = new EmployeeIncentiveProcessItemDetailCollection();
                itemDets.Query.Where(itemDets.Query.EmployeeIncentiveProcessID == Convert.ToInt32(txtEmployeeIncentiveProcessID.Text));
                itemDets.LoadAll();
                itemDets.MarkAllAsDeleted();

                entity.MarkAsDeleted();
                using (var trans = new esTransactionScope())
                {
                    items.Save();
                    itemDets.Save();
                    entity.Save();
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var coll = new EmployeeIncentiveProcessCollection();
            coll.Query.Where(coll.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt(),
                coll.Query.Or(coll.Query.IsVoid.IsNull(), coll.Query.IsVoid == false));
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "Record with this Payroll Period has registered.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeeIncentiveProcess();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var coll = new EmployeeIncentiveProcessCollection();
            coll.Query.Where(coll.Query.EmployeeIncentiveProcessID != txtEmployeeIncentiveProcessID.Text.ToInt(),
                coll.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt(),
                coll.Query.Or(coll.Query.IsVoid.IsNull(), coll.Query.IsVoid == false));
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "Record with this Payroll Period has registered.";
                args.IsCancel = true;
                return;
            }

            var entity = new EmployeeIncentiveProcess();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeIncentiveProcessID.Text)))
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

        private bool IsApprovedOrVoid(EmployeeIncentiveProcess entity, ValidateArgs args)
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

        private bool IsApproved(EmployeeIncentiveProcess entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("EmployeeIncentiveProcessID='{0}'", txtEmployeeIncentiveProcessID.Text.Trim());
            auditLogFilter.TableName = "EmployeeIncentiveProcess";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new EmployeeIncentiveProcess();
            if (entity.LoadByPrimaryKey(txtEmployeeIncentiveProcessID.Text.ToInt()))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;

                var itm = new EmployeeIncentiveProcessItemCollection();
                itm.Query.Where(itm.Query.EmployeeIncentiveProcessID == entity.EmployeeIncentiveProcessID.ToInt(), itm.Query.TotalPoint > 0);
                itm.LoadAll();
                if (itm.Count == 0)
                {
                    args.MessageText = "Incentives have not been calculated.";
                    args.IsCancel = true;
                    return;
                }

                var closing = new ClosingWageTransaction();
                if (closing.LoadByPrimaryKey(entity.PayrollPeriodID.ToInt()) && closing.IsClosed == true)
                {
                    args.MessageText = "This payroll periode has been closed.";
                    args.IsCancel = true;
                    return;
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

                int result = EmployeeIncentiveProcess.Approved(entity.EmployeeIncentiveProcessID.ToInt(), entity.PayrollPeriodID.ToInt(), AppSession.UserLogin.UserID);
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new EmployeeIncentiveProcess();
            if (entity.LoadByPrimaryKey(txtEmployeeIncentiveProcessID.Text.ToInt()))
            {
                var closing = new ClosingWageTransaction();
                if (closing.LoadByPrimaryKey(entity.PayrollPeriodID.ToInt()) && closing.IsClosed == true)
                {
                    args.MessageText = "This payroll periode has been closed.";
                    args.IsCancel = true;
                    return;
                }

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

                int result = EmployeeIncentiveProcess.UnApproved(entity.EmployeeIncentiveProcessID.ToInt(), entity.PayrollPeriodID.ToInt(), AppSession.UserLogin.UserID);
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new EmployeeIncentiveProcess();
            if (!entity.LoadByPrimaryKey(txtEmployeeIncentiveProcessID.Text.ToInt()))
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

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new EmployeeIncentiveProcess();
            if (entity.LoadByPrimaryKey(txtEmployeeIncentiveProcessID.Text.ToInt()))
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
            bool isVisible = (newVal == AppEnum.DataMode.Read);
            grdItem.MasterTableView.CommandItemDisplay = isVisible && !chkIsApproved.Checked && !chkIsVoid.Checked ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdItem.Columns[grdItem.Columns.Count - 2].Visible = isVisible;
            
            PopulateGrid();
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new EmployeeIncentiveProcess();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(Convert.ToInt32(id));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeIncentiveProcessID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var incentive = (EmployeeIncentiveProcess)entity;
            txtEmployeeIncentiveProcessID.Value = incentive.EmployeeIncentiveProcessID;
            if (!string.IsNullOrEmpty(incentive.PayrollPeriodID.ToString()))
            {
                var pp = new PayrollPeriodQuery();
                pp.Where(pp.PayrollPeriodID == Convert.ToInt32(incentive.PayrollPeriodID));
                var dtb = pp.LoadDataTable();
                cboPayrollPeriodID.DataSource = dtb;
                cboPayrollPeriodID.DataBind();
                cboPayrollPeriodID.SelectedValue = incentive.PayrollPeriodID.ToString();
                if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                    cboPayrollPeriodID.Text = dtb.Rows[0]["PayrollPeriodCode"].ToString() + " - " + dtb.Rows[0]["PayrollPeriodName"].ToString();
            }
            else
            {
                cboPayrollPeriodID.Items.Clear();
                cboPayrollPeriodID.Text = string.Empty;
            }
            
            chkIsApproved.Checked = incentive.IsApproved ?? false;
            chkIsVoid.Checked = incentive.IsVoid ?? false;

            PopulateGrid();
        }

        private void SetEntityValue(EmployeeIncentiveProcess incentive)
        {
            incentive.EmployeeIncentiveProcessID = Convert.ToInt32(txtEmployeeIncentiveProcessID.Value);
            incentive.PayrollPeriodID = cboPayrollPeriodID.SelectedValue.ToInt();
            
            //Last Update Status
            if (incentive.es.IsAdded || incentive.es.IsModified)
            {
                incentive.LastUpdateByUserID = AppSession.UserLogin.UserID;
                incentive.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(EmployeeIncentiveProcess entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                var items = new EmployeeIncentiveProcessItemCollection();
                items.Query.Where(items.Query.EmployeeIncentiveProcessID == entity.EmployeeIncentiveProcessID);
                items.LoadAll();

                foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
                {
                    string id = dataItem.GetDataKeyValue("SRIncentiveServiceUnitGroup").ToString();
                    decimal nominal = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtNominal")).Value);

                    bool isExist = false;
                    foreach (EmployeeIncentiveProcessItem row in items)
                    {
                        if (row.SRIncentiveServiceUnitGroup.Equals(id))
                        {
                            isExist = true;
                            row.Nominal = nominal;
                            row.TotalPoint = 0;
                            row.NominalPerPoint = 0;
                            break;
                        }
                    }
                    //Add
                    if (!isExist)
                    {
                        EmployeeIncentiveProcessItem row = items.AddNew();
                        row.EmployeeIncentiveProcessID = entity.EmployeeIncentiveProcessID;
                        row.SRIncentiveServiceUnitGroup = id;
                        row.Nominal = nominal;
                        row.TotalPoint = 0;
                        row.NominalPerPoint = 0;
                        row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        row.LastUpdateDateTime = DateTime.Now;
                    }
                }

                items.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                txtEmployeeIncentiveProcessID.Value = entity.EmployeeIncentiveProcessID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeIncentiveProcessQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.EmployeeIncentiveProcessID > txtEmployeeIncentiveProcessID.Text.ToInt());
                que.OrderBy(que.EmployeeIncentiveProcessID.Ascending);
            }
            else
            {
                que.Where(que.EmployeeIncentiveProcessID < txtEmployeeIncentiveProcessID.Text.ToInt());
                que.OrderBy(que.EmployeeIncentiveProcessID.Descending);
            }

            var entity = new EmployeeIncentiveProcess();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Record Detail Method Function EmployeeIncentiveProcessItem

        private void PopulateGrid()
        {
            //Display Data Detail
            grdItem.DataSource = GetIncentives();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = GetIncentives();
        }

        private DataTable GetIncentives()
        {
            var query = new EmployeeIncentiveProcessItemQuery("a");
            var qrRef = new AppStandardReferenceItemQuery("b");
            if (this.DataModeCurrent == AppEnum.DataMode.Read)
            {
                query.InnerJoin(qrRef).On(query.SRIncentiveServiceUnitGroup == qrRef.ItemID);
                query.Where(query.EmployeeIncentiveProcessID == txtEmployeeIncentiveProcessID.Text.ToInt());
            }
            else
            {
                query.RightJoin(qrRef).On(query.SRIncentiveServiceUnitGroup == qrRef.ItemID & query.EmployeeIncentiveProcessID == txtEmployeeIncentiveProcessID.Text.ToInt());
            }
            query.Where(qrRef.StandardReferenceID == AppEnum.StandardReference.IncentiveServiceUnitGroup.ToString(), qrRef.IsActive == true);
            query.OrderBy(qrRef.ItemID.Ascending);
            query.Select
                (
                    "<ISNULL(a.EmployeeIncentiveProcessID, -1) AS EmployeeIncentiveProcessID>",
                    qrRef.ItemID.As("SRIncentiveServiceUnitGroup"),
                    qrRef.ItemName.As("IncentiveServiceUnitGroupName"),
                    "<ISNULL(a.Nominal, 0) AS Nominal>",
                    "<ISNULL(a.TotalPoint, 0) AS TotalPoint>",
                    "<ISNULL(a.NominalPerPoint, 0) AS NominalPerPoint>"
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }
        #endregion

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
                return;

            switch (eventArgument)
            {
                case "calculation":
                    Validate();
                    if (!IsValid)
                        return;
                    if (chkIsApproved.Checked || chkIsVoid.Checked)
                        return;

                    IncentiveCalculation();
                    PopulateGrid();

                    break;
            }
        }
        private void IncentiveCalculation()
        {
            int id = Convert.ToInt32(txtEmployeeIncentiveProcessID.Text);
            int payrollPeriodId = Convert.ToInt32(cboPayrollPeriodID.SelectedValue);

            int result = EmployeeIncentiveProcess.ProcessItem(id, payrollPeriodId, AppSession.UserLogin.UserID);
        }
    }
}