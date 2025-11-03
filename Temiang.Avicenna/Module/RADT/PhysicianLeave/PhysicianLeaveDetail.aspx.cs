using System;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PhysicianLeaveDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PhysicianLeave);
            txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
            cboParamedicID.Text = string.Empty;
        }

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "PhysicianLeaveSearch.aspx";
            UrlPageList = "PhysicianLeaveList.aspx";

            ProgramID = AppConstant.Program.PhysicianLeave;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRPhysicianLeaveReason, AppEnum.StandardReference.PhysicianLeaveReason);
                ComboBox.PopulateWithParamedic(cboSubsParamedicEMR);
                ComboBox.PopulateWithParamedic(cboSubsParamedicIP);
                ComboBox.PopulateWithParamedic(cboSubsParamedicOP);
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(cboParamedicID, cboParamedicID);
            ajax.AddAjaxSetting(cboParamedicID, grdExeptionUnit);
        }
        #endregion

        #region ComboBox ParamedicID

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery();
            query.es.Top = 20;
            query.Select(
                query.ParamedicID,
                query.ParamedicName
                );
            query.Where(
                query.ParamedicName.Like(searchTextContain),
                query.IsActive == true
                );
            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void cboParamedicID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdExeptionUnit.Rebind();
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {
            PopulateExeptionUnitGrid();
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboSRPhysicianLeaveReason.SelectedValue))
            {
                args.MessageText = "Leave Reason required.";
                args.IsCancel = true;
                return;
            }
            var entity = new ParamedicLeave();
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

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                var entity = new ParamedicLeave();
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
                entity.IsApproved = true;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.Save();

                var startDate = txtStartDate.SelectedDate;
                var endDate = txtEndDate.SelectedDate;
                var leaveDateColl = new ParamedicLeaveDateCollection();
                
                for(DateTime lDate = startDate ?? (new DateTime()).NowAtSqlServer(); lDate <= endDate; lDate=lDate.AddDays(1))
                {
                    ParamedicLeaveDate leaveDate = leaveDateColl.AddNew();
                    leaveDate.TransactionNo = txtTransactionNo.Text;
                    leaveDate.LeaveDate = lDate;
                    leaveDate.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    leaveDate.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                leaveDateColl.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                var entity = new ParamedicLeave();
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
                entity.IsApproved = false;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.Save();

                var leaveDateColl = new ParamedicLeaveDateCollection();
                leaveDateColl.Query.Where(leaveDateColl.Query.TransactionNo == txtTransactionNo.Text);
                leaveDateColl.LoadAll();
                leaveDateColl.MarkAllAsDeleted();
                leaveDateColl.Save();

                trans.Complete();
            }
        }

        private bool IsApprovedOrVoid(ParamedicLeave entity, ValidateArgs args)
        {
            if (entity.IsApproved != null && entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ParamedicLeave());
            PopulateNewTransactionNo();

            cboParamedicID.Text = string.Empty;
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new ParamedicLeave();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                entity.MarkAsDeleted();

                var excUnit = new ParamedicLeaveExeptionUnitCollection();
                excUnit.Query.Where(excUnit.Query.TransactionNo == txtTransactionNo.Text);
                excUnit.LoadAll();
                entity.MarkAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    excUnit.Save();

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
            var appQuery = new AppointmentQuery();
            appQuery.Where(appQuery.ParamedicID == cboParamedicID.SelectedValue &&
                           appQuery.AppointmentDate >= txtStartDate.SelectedDate &&
                           appQuery.AppointmentDate <= txtEndDate.SelectedDate &&
                           appQuery.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
            DataTable dtb = appQuery.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                args.IsCancel = false;
                args.MessageText = "Existing patient appointments in the selected period.";
                return;
            }

            if (txtEndDate.SelectedDate.Value.Date < txtStartDate.SelectedDate.Value.Date)
            {
                args.IsCancel = false;
                args.MessageText = "End Period must be equal or greater than Start Periode.";
                return;
            }

            if (string.IsNullOrEmpty(cboSRPhysicianLeaveReason.SelectedValue))
            {
                args.MessageText = "Leave Reason required.";
                args.IsCancel = true;
                return;
            }

            PopulateNewTransactionNo();
            var entity = new ParamedicLeave();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new ParamedicLeave();
            entity.AddNew();
            SetEntityValue(entity);

            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var appQuery = new AppointmentQuery();
            appQuery.Where(appQuery.ParamedicID == cboParamedicID.SelectedValue &&
                           appQuery.AppointmentDate >= txtStartDate.SelectedDate &&
                           appQuery.AppointmentDate <= txtEndDate.SelectedDate &&
                           appQuery.SRAppointmentStatus != AppSession.Parameter.AppointmentStatusCancel);
            DataTable dtb = appQuery.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                args.IsCancel = false;
                args.MessageText = "Existing patient appointments in the selected period.";
                return;
            }

            var entity = new ParamedicLeave();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);

                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            auditLogFilter.TableName = "ParamedicLeave";
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemExeptionUnit(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ParamedicLeave();
            if (parameters.Length > 0)
            {
                String transactionNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transactionNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }

            var par = new ParamedicQuery("a");
            par.Select(par.ParamedicID, par.ParamedicName);

            DataTable tbl = par.LoadDataTable();
            cboParamedicID.DataSource = tbl;
            cboParamedicID.DataBind();

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var pl = (ParamedicLeave)entity;
            txtTransactionNo.Text = pl.TransactionNo;
            txtTransactionDate.SelectedDate = pl.TransactionDate;

            var par = new ParamedicQuery("a");
            par.Select(
                par.ParamedicID,
                par.ParamedicName
                );
            par.Where(par.ParamedicID == pl.str.ParamedicID);
            cboParamedicID.DataSource = par.LoadDataTable();
            cboParamedicID.DataBind();
            cboParamedicID.SelectedValue = pl.ParamedicID;
            txtStartDate.SelectedDate = pl.StartDate;
            txtEndDate.SelectedDate = pl.EndDate;
            cboSRPhysicianLeaveReason.SelectedValue = pl.SRPhysicianLeaveReason;
            txtNotes.Text = pl.Notes;
            chkIsApproved.Checked = pl.IsApproved ?? false;
            cboSubsParamedicIP.SelectedValue = pl.SubsParamedicIP;
            cboSubsParamedicOP.SelectedValue = pl.SubsParamedicOP;
            cboSubsParamedicEMR.SelectedValue = pl.SubsParamedicEMR;

            //Display Data Detail
            PopulateExeptionUnitGrid();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuDelete.Enabled = !chkIsApproved.Checked;
            ToolBarMenuEdit.Enabled = !chkIsApproved.Checked;
        }


        #endregion

        #region Private Method Standard

        private void SetEntityValue(ParamedicLeave entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.ParamedicID = cboParamedicID.SelectedValue;
            entity.StartDate = txtStartDate.SelectedDate;
            entity.EndDate = txtEndDate.SelectedDate;
            entity.SRPhysicianLeaveReason = cboSRPhysicianLeaveReason.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.SubsParamedicIP = cboSubsParamedicIP.SelectedValue;
            entity.SubsParamedicOP = cboSubsParamedicOP.SelectedValue;
            entity.SubsParamedicEMR = cboSubsParamedicEMR.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(ParamedicLeave entity)
        {
            //TransactionCode
            var excUnit = new ParamedicLeaveExeptionUnitCollection();
            excUnit.Query.Where(excUnit.Query.TransactionNo == entity.TransactionNo);
            excUnit.LoadAll();

            foreach (GridDataItem dataItem in grdExeptionUnit.MasterTableView.Items)
            {
                string unitId = dataItem.GetDataKeyValue("ServiceUnitID").ToString();
                bool isSelect = ((System.Web.UI.WebControls.CheckBox)dataItem.FindControl("chkIsSelect")).Checked;
                string startTime = ((Telerik.Web.UI.RadMaskedTextBox)dataItem.FindControl("txtStartTime")).TextWithLiterals;
                string endTime = ((Telerik.Web.UI.RadMaskedTextBox)dataItem.FindControl("txtEndTime")).TextWithLiterals;
                
                bool isExist = false;
                foreach (ParamedicLeaveExeptionUnit row in excUnit)
                {
                    if (row.ServiceUnitID.Equals(unitId))
                    {
                        isExist = true;
                        if (!isSelect)
                            row.MarkAsDeleted();
                        break;
                    }
                }
                //Add
                if (!isExist && isSelect)
                {
                    ParamedicLeaveExeptionUnit row = excUnit.AddNew();
                    row.TransactionNo = entity.TransactionNo;
                    row.ServiceUnitID = unitId;
                    row.StartTime = startTime;
                    row.EndTime = endTime;
                    row.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    row.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (DataModeCurrent == AppEnum.DataMode.New)
                    _autoNumber.Save();

                entity.Save();
                excUnit.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ParamedicLeaveQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
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
            var entity = new ParamedicLeave();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function ParamedicLeaveExeptionUnit

        private void PopulateExeptionUnitGrid()
        {
            //Display Data Detail
            grdExeptionUnit.DataSource = GetParamedicLeaveExeptionUnits();
            grdExeptionUnit.DataBind();
        }

        protected void grdExeptionUnit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdExeptionUnit.DataSource = GetParamedicLeaveExeptionUnits();
        }

        private DataTable GetParamedicLeaveExeptionUnits()
        {
            var query = new ParamedicLeaveExeptionUnitQuery("a");
            var unit = new ServiceUnitQuery("b");

            if (this.DataModeCurrent == AppEnum.DataMode.Read)
            {
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
            }
            else
            {
                query.RightJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID & query.TransactionNo == txtTransactionNo.Text);
            }
            
            query.Where(unit.SRRegistrationType != string.Empty, unit.IsActive == true);
            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
            {
                var unitpar = new ServiceUnitParamedicQuery("c");
                query.LeftJoin(unitpar).On(unit.ServiceUnitID == unitpar.ServiceUnitID);
                query.Where(query.Or(unitpar.ParamedicID == cboParamedicID.SelectedValue,
                                     unit.SRRegistrationType == "IPR"));
                query.es.Distinct = true;
            }

            query.OrderBy(unit.ServiceUnitID.Ascending);
            
            query.Select
                (
                    "<CONVERT(BIT,CASE WHEN COALESCE(a.ServiceUnitID,'')='' THEN 0 ELSE 1 END) AS IsSelect>",
                    unit.ServiceUnitID,
                    unit.ServiceUnitName,
                    "<ISNULL(a.StartTime, '00:00') AS StartTime>",
                    "<ISNULL(a.EndTime, '23:59') AS EndTime>",
                    "<CASE WHEN b.SRRegistrationType = 'IPR' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsVisible>"                    
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private void RefreshCommandItemExeptionUnit(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdExeptionUnit.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdExeptionUnit.Rebind();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdExeptionUnit.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("chkIsSelect")).Checked = selected;
            }
        }

        #endregion
    }
}
