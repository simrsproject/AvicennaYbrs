using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WorkingHourDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "WorkingHourSearch.aspx";
            UrlPageList = "WorkingHourList.aspx";

            ProgramID = AppConstant.Program.WorkingHour;

            WindowSearch.Height = 300;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRShift, AppEnum.StandardReference.Shift);
                cboSRShift.Items.AddRange(new List<Telerik.Web.UI.RadComboBoxItem>
                {
                    //new Telerik.Web.UI.RadComboBoxItem("Morning & Afternoon", "ShiftID-012"),
                    new Telerik.Web.UI.RadComboBoxItem("Morning & Night", "ShiftID-013"),
                    //new Telerik.Web.UI.RadComboBoxItem("Afternoon & Night", "ShiftID-023")
                });
                StandardReference.InitializeIncludeSpace(cboSRWorkingDay, AppEnum.StandardReference.WorkingDay);
            }
        }

        protected override void OnMenuNewClick()
        {
            ViewState["id"] = 0;
            txtOvertimeValueInMinutes.Value = 0;
            chkIsActive.Checked = true;
            txtMealQty.Value = 0;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new WorkingHour();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new WorkingHour();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString())))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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
            auditLogFilter.PrimaryKeyData = string.Format("WorkingHourID='{0}'", ViewState["id"].ToString());
            auditLogFilter.TableName = "WorkingHour";
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new WorkingHour();
            if (parameters.Length > 0)
            {
                if (!parameters[0].Equals(string.Empty)) entity.LoadByPrimaryKey(int.Parse(parameters[0]));
            }
            else entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"].ToString()));

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var wh = (WorkingHour)entity;
            if (wh == null)
            {
                ViewState["id"] = 0;
                return;
            }
            ViewState["id"] = wh.WorkingHourID;

            txtWorkingHourName.Text = wh.WorkingHourName;
            cboSRShift.SelectedValue = wh.SRShift;

            txtStartTime.Text = wh.StartTime;
            txtMinimumStartTime.Text = wh.MinimumStartTime;
            txtMaxStartTime.Text = wh.MaximumStartTime;
            txtEndTime.Text = wh.EndTime;
            txtMinEndTime.Text = wh.MinimumEndTime;
            txtMaxEndTime.Text = wh.MaximumEndTime;

            chkIsOverTime.Checked = wh.IsOvertimeWorkingHour ?? false;
            txtOvertimeValueInMinutes.Value = Convert.ToDouble(wh.OvertimeValueInMinutes ?? 0); // jam

            chkIsShiftLeader.Checked = wh.IsShiftLeader ?? false;
            chkIsShiftLeader2.Checked = wh.IsShiftLeader2 ?? false;
            chkIsNotValidated.Checked = wh.IsNotValidated ?? false;
            chkIsOffDay.Checked = wh.IsOffDay ?? false;
            chkIsLongShift.Checked = wh.IsLongShift ?? false;
            chkIsCrossDay.Checked = wh.IsCrossDay ?? false;
            chkIsHoliday.Checked = wh.IsHoliday ?? false;
            chkIsActive.Checked = wh.IsActive ?? false;

            txtStartTime2.Text = wh.StartTime2;
            txtMinimumStartTime2.Text = wh.MinimumStartTime2;
            txtMaxStartTime2.Text = wh.MaximumStartTime2;
            txtEndTime2.Text = wh.EndTime2;
            txtMinEndTime2.Text = wh.MinimumEndTime2;
            txtMaxEndTime2.Text = wh.MaximumEndTime2;

            txtMealQty.Value = Convert.ToDouble(wh.MealQty ?? 0);

            cboSRWorkingDay.SelectedValue = wh.SRWorkingDay;

            PopulateWorkingHourOrganizationUnitGrid();
        }

        private void SetEntityValue(WorkingHour entity)
        {
            entity.WorkingHourName = txtWorkingHourName.Text;
            entity.StartTime = txtStartTime.TextWithLiterals;
            entity.MinimumStartTime = txtMinimumStartTime.TextWithLiterals;
            entity.MaximumStartTime = txtMaxStartTime.TextWithLiterals;
            entity.EndTime = txtEndTime.TextWithLiterals;
            entity.MinimumEndTime = txtMinEndTime.TextWithLiterals;
            entity.MaximumEndTime = txtMaxEndTime.TextWithLiterals;
            entity.IsOvertimeWorkingHour = chkIsOverTime.Checked;
            entity.OvertimeValueInMinutes = Convert.ToInt32(txtOvertimeValueInMinutes.Value);
            entity.IsActive = chkIsActive.Checked;
            entity.MealQty = Convert.ToInt32(txtMealQty.Value);
            entity.SRShift = cboSRShift.SelectedValue;
            entity.IsShiftLeader = chkIsShiftLeader.Checked;
            entity.IsShiftLeader2 = chkIsShiftLeader2.Checked;
            entity.IsNotValidated = chkIsNotValidated.Checked;
            entity.IsOffDay = chkIsOffDay.Checked;
            entity.IsLongShift = chkIsLongShift.Checked;
            entity.IsCrossDay = chkIsCrossDay.Checked;
            entity.IsHoliday = chkIsHoliday.Checked;

            entity.StartTime2 = txtStartTime2.TextWithLiterals;
            entity.MinimumStartTime2 = txtMinimumStartTime2.TextWithLiterals;
            entity.MaximumStartTime2 = txtMaxStartTime2.TextWithLiterals;
            entity.EndTime2 = txtEndTime2.TextWithLiterals;
            entity.MinimumEndTime2 = txtMinEndTime2.TextWithLiterals;
            entity.MaximumEndTime2 = txtMaxEndTime2.TextWithLiterals;

            entity.SRWorkingDay = cboSRWorkingDay.SelectedValue;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(WorkingHour entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                foreach (var itwm in WorkingHourOrganizationUnits)
                {
                    itwm.WorkingHourID = entity.WorkingHourID;
                    itwm.LastUpdateUserID = AppSession.UserLogin.UserID;
                    itwm.LastUpdateDateTime = DateTime.Now;
                }

                WorkingHourOrganizationUnits.Save();

                //Commit if success, Rollback if failed
                trans.Complete();

                ViewState["id"] = entity.WorkingHourID;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new WorkingHourQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.WorkingHourID > (int)ViewState["id"]);
                que.OrderBy(que.WorkingHourID.Ascending);
            }
            else
            {
                que.Where(que.WorkingHourID < (int)ViewState["id"]);
                que.OrderBy(que.WorkingHourID.Descending);
            }

            var entity = new WorkingHour();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        private WorkingHourOrganizationUnitCollection WorkingHourOrganizationUnits
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collWorkingHourOrganizationUnit"];
                    if (obj != null) return ((WorkingHourOrganizationUnitCollection)(obj));
                }

                WorkingHourOrganizationUnitCollection coll = new WorkingHourOrganizationUnitCollection();

                WorkingHourOrganizationUnitQuery query = new WorkingHourOrganizationUnitQuery("a");
                OrganizationUnitQuery asri = new OrganizationUnitQuery("b");

                query.Select(query, asri.OrganizationUnitName.As("refToOrganizationUnit_OrganizationUnitName"));
                query.InnerJoin(asri).On(query.OrganizationUnitID == asri.OrganizationUnitID);
                query.Where(query.WorkingHourID == (ViewState["id"] == null ? "0" : ViewState["id"].ToString()));
                coll.Load(query);

                Session["collWorkingHourOrganizationUnit"] = coll;
                return coll;
            }
            set
            {
                Session["collWorkingHourOrganizationUnit"] = value;
            }
        }

        private void RefreshCommandItemParamedicBridging(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdAliasName.Columns[0].Visible = isVisible;
            grdAliasName.Columns[grdAliasName.Columns.Count - 1].Visible = isVisible;

            grdAliasName.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdAliasName.Rebind();
        }

        private void PopulateWorkingHourOrganizationUnitGrid()
        {
            //Display Data Detail
            WorkingHourOrganizationUnits = null; //Reset Record Detail
            grdAliasName.DataSource = WorkingHourOrganizationUnits; //Requery
            grdAliasName.MasterTableView.IsItemInserted = false;
            grdAliasName.MasterTableView.ClearEditItems();
            grdAliasName.DataBind();
        }

        protected void grdAliasName_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdAliasName.DataSource = WorkingHourOrganizationUnits;
        }

        protected void grdAliasName_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            int wh = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][WorkingHourOrganizationUnitMetadata.ColumnNames.WorkingHourID]);
            int ou = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][WorkingHourOrganizationUnitMetadata.ColumnNames.OrganizationUnitID]);

            var entity = FindWorkingHourOrganizationUnit(wh, ou);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdAliasName_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            int wh = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][WorkingHourOrganizationUnitMetadata.ColumnNames.WorkingHourID]);
            int ou = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][WorkingHourOrganizationUnitMetadata.ColumnNames.OrganizationUnitID]);

            var entity = FindWorkingHourOrganizationUnit(wh, ou);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdAliasName_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = WorkingHourOrganizationUnits.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdAliasName.Rebind();
        }

        private WorkingHourOrganizationUnit FindWorkingHourOrganizationUnit(int wh, int ou)
        {
            var coll = WorkingHourOrganizationUnits;
            return coll.FirstOrDefault(rec => rec.WorkingHourID.Equals(wh) && rec.OrganizationUnitID.Equals(ou));
        }

        private void SetEntityValue(WorkingHourOrganizationUnit entity, GridCommandEventArgs e)
        {
            WorkingHourItem userControl = (WorkingHourItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.OrganizationUnitID = userControl.OrganizationUnitID;
                entity.OrganizationUnitName = userControl.OrganizationUnitName;
            }
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemParamedicBridging(newVal);
        }
    }
}