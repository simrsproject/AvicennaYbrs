using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class WorkScheduleDetail : BasePageDetail
    {
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EmployeeWorkSchedule;

            // Url Search & List
            UrlPageSearch = "WorkScheduleSearch.aspx";
            UrlPageList = "WorkScheduleList.aspx";

            if (!IsPostBack)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuAdd.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {

        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new EmployeeWorkingInfo();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Text)))
            {
                SetEntityValue();
                SaveEntity();
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
            //auditLogFilter.PrimaryKeyData = string.Format("PersonID='{0}'", txtPersonID.Text.Trim());
            //auditLogFilter.TableName = "EmployeeWorkingInfo";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
            cboPayrollPeriodID.Enabled = false;
            txtStartDate.Enabled = false;
            txtEndDate.Enabled = false;
            cboPayrollPeriodID2.Enabled = true;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            EmployeeWorkingInfo entity = new EmployeeWorkingInfo();
            if (parameters.Length > 0)
            {
                string personID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(personID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var employeeWorkingInfo = (EmployeeWorkingInfo)entity;
            txtPersonID.Text = employeeWorkingInfo.PersonID.ToString();
            txtJoinDate.SelectedDate = employeeWorkingInfo.JoinDate;

            var personal = new PersonalInfo();
            personal.LoadByPrimaryKey(Convert.ToInt32(txtPersonID.Text));
            txtEmployeeNumber.Text = personal.EmployeeNumber;
            txtEmployeeName.Text = personal.EmployeeName;

            PopulateEmployeeImage(Convert.ToInt32(txtPersonID.Text), personal.SRGenderType);

            var supervisor = new PersonalInfo();
            if (supervisor.LoadByPrimaryKey(Convert.ToInt32(employeeWorkingInfo.SupervisorId)))
                txtSupervisorName.Text = supervisor.EmployeeName;
            else txtSupervisorName.Text = string.Empty;

            DataTable Employee = (new EmployeeWorkingInfoCollection()).EmployeeWorkingInfoView(Convert.ToInt32(txtPersonID.Text));
            string organizationID = "-1";
            try { organizationID = Employee.Rows[0]["OrganizationUnitID"].ToString(); }
            catch { }
            string positionID = "-1";
            try { positionID = Employee.Rows[0]["PositionID"].ToString(); }
            catch { }

            var organizationUnit = new OrganizationUnit();
            if (organizationUnit.LoadByPrimaryKey(Convert.ToInt32(organizationID)))
                txtOrganizationName.Text = organizationUnit.OrganizationUnitName;

            var position = new Position();
            position.LoadByPrimaryKey(Convert.ToInt32(positionID));
            txtPositionTitle.Text = position.PositionName;

            var vqr = new VwEmployeeTableQuery();
            vqr.Where(vqr.PersonID == (employeeWorkingInfo.PersonID ?? 0));
            var v = new VwEmployeeTable();
            v.Load(vqr);

            txtServiceYear.Value = Convert.ToDouble(v.ServiceYear);
            txtServiceYearText.Text = v.ServiceYearText;
            txtBirthDate.SelectedDate = v.BirthDate;

            var asri = new AppStandardReferenceItem();
            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeStatus.ToString(), employeeWorkingInfo.SREmployeeStatus))
                txtEmployeeStatusName.Text = asri.ItemName;
            else txtEmployeeStatusName.Text = string.Empty;

            txtResignDate.SelectedDate = v.ResignDate;

            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeType.ToString(), employeeWorkingInfo.SREmployeeType))
                txtEmployeeTypeName.Text = asri.ItemName;
            else txtEmployeeTypeName.Text = string.Empty;

            if (v.PositionGradeID != null && v.PositionGradeID != -1)
            {
                var pg = new PositionGrade();
                if (pg.LoadByPrimaryKey(Convert.ToInt32(v.PositionGradeID)))
                    txtPositionGradeID.Text = pg.PositionGradeName;
                else txtPositionGradeID.Text = string.Empty;
            }
            else txtPositionGradeID.Text = string.Empty;

            if (v.GradeYear != null)
                txtGradeYear.Value = Convert.ToDouble(v.GradeYear);

            txtAbsenceCardNo.Text = employeeWorkingInfo.AbsenceCardNo;
            txtEmployeeRegistrationNo.Text = employeeWorkingInfo.EmployeeRegistrationNo;
            
            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.EmploymentType.ToString(), v.SREmploymentType))
                txtSREmploymentType.Text = asri.ItemName;
            else txtSREmploymentType.Text = string.Empty;

            asri = new AppStandardReferenceItem();
            if (asri.LoadByPrimaryKey(AppEnum.StandardReference.EducationLevel.ToString(), v.SREducationLevel))
                txtSREducationLevel.Text = asri.ItemName;
            else txtSREducationLevel.Text = string.Empty;

            var period = new PayrollPeriodQuery();
            period.Where(period.StartDate <= DateTime.Now, period.PayDate >= DateTime.Now.Date);
            period.OrderBy(period.StartDate.Ascending);
            period.es.Top = 1;
            var dtb = period.LoadDataTable();
            cboPayrollPeriodID.DataSource = dtb;
            cboPayrollPeriodID.DataBind();
            cboPayrollPeriodID.SelectedValue = dtb.Rows[0]["PayrollPeriodID"].ToString();
            cboPayrollPeriodID.Text = dtb.Rows[0]["PayrollPeriodName"].ToString();
            txtStartDate.SelectedDate = Convert.ToDateTime(dtb.Rows[0]["StartDate"]);
            txtEndDate.SelectedDate = Convert.ToDateTime(dtb.Rows[0]["EndDate"]);

            PopulateItemGrid();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue()
        {
            foreach (var item in EmployeeWorkSchedules)
            {
                item.PersonID = Convert.ToInt32(txtPersonID.Text);
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity()
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                EmployeeWorkSchedules.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new EmployeeWorkingInfoQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PersonID > txtPersonID.Text.ToInt());
                que.OrderBy(que.PersonID.Ascending);
            }
            else
            {
                que.Where(que.PersonID < txtPersonID.Text.ToInt());
                que.OrderBy(que.PersonID.Descending);
            }
            var entity = new EmployeeWorkingInfo();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of EmployeeWorkSchedule
        private MonthlyAttendanceDetailCollection EmployeeWorkSchedules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeWorkSchedule"];
                    if (obj != null)
                    {
                        return ((MonthlyAttendanceDetailCollection)(obj));
                    }
                }

                var coll = new MonthlyAttendanceDetailCollection();
                var query = new MonthlyAttendanceDetailQuery("a");
                query.Select
                    (
                        query
                    );
                query.Where(query.PersonID == Convert.ToInt32(txtPersonID.Text));

                var payrollPeriod = new PayrollPeriod();
                if (string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                {
                    var ppq = new PayrollPeriodQuery();
                    ppq.Where(ppq.StartDate <= DateTime.Now, ppq.PayDate >= DateTime.Now.Date);
                    ppq.es.Top = 1;
                    payrollPeriod.Load(ppq);
                }
                else
                {
                    payrollPeriod.LoadByPrimaryKey(Convert.ToInt32(cboPayrollPeriodID.SelectedValue));
                }
                query.Where(query.PayrollPeriodID == payrollPeriod.PayrollPeriodID);

                query.OrderBy(query.ScheduleInDate.Ascending);

                coll.Load(query);

                Session["collEmployeeWorkSchedule"] = coll;
                return coll;
            }
            set
            {
                Session["collEmployeeWorkSchedule"] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdWorkSchedule.Columns[0].Visible = isVisible;
            grdWorkSchedule.Columns[grdWorkSchedule.Columns.Count - 1].Visible = isVisible;

            grdWorkSchedule.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdWorkSchedule.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            EmployeeWorkSchedules = null; //Reset Record Detail
            grdWorkSchedule.DataSource = EmployeeWorkSchedules; //Requery
            grdWorkSchedule.MasterTableView.IsItemInserted = false;
            grdWorkSchedule.MasterTableView.ClearEditItems();
            grdWorkSchedule.DataBind();
        }

        private MonthlyAttendanceDetail FindItem(Int64 id)
        {
            MonthlyAttendanceDetailCollection coll = EmployeeWorkSchedules;
            MonthlyAttendanceDetail retEntity = null;
            foreach (MonthlyAttendanceDetail rec in coll)
            {
                if (rec.MonthlyAttendanceDetailID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdWorkSchedule_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdWorkSchedule.DataSource = EmployeeWorkSchedules;
        }

        protected void grdWorkSchedule_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int64 id = Convert.ToInt64(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][MonthlyAttendanceDetailMetadata.ColumnNames.MonthlyAttendanceDetailID]);
            MonthlyAttendanceDetail entity = FindItem(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdWorkSchedule_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            Int64 id =
                Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex][MonthlyAttendanceDetailMetadata.ColumnNames.MonthlyAttendanceDetailID]);
            MonthlyAttendanceDetail entity = FindItem(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdWorkSchedule_InsertCommand(object source, GridCommandEventArgs e)
        {
            MonthlyAttendanceDetail entity = EmployeeWorkSchedules.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdWorkSchedule.Rebind();
        }

        private void SetEntityValue(MonthlyAttendanceDetail entity, GridCommandEventArgs e)
        {
            var userControl = (WorkScheduleDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.MonthlyAttendanceDetailID = userControl.MonthlyAttendanceDetailID;
                entity.PersonID = Convert.ToInt32(txtPersonID.Text);
                entity.PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue);
                entity.ScheduleInDate = userControl.ScheduleInDate;
                entity.ScheduleOutDate = userControl.ScheduleOutDate;
                entity.ScheduleInTime = userControl.ScheduleInTime;
                entity.ScheduleOutTime = userControl.ScheduleOutTime;
            }
        }
        #endregion

        #region history

        protected void grdWorkSchedule2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdWorkSchedule2.DataSource = EmployeeWorkScheduleHists;
        }

        private DataTable EmployeeWorkScheduleHists
        {
            get
            {
                var query = new MonthlyAttendanceDetailQuery("a");
                query.Select
                    (
                        query
                    );
                query.Where(query.PersonID == Convert.ToInt32(txtPersonID.Text));

                if (string.IsNullOrEmpty(cboPayrollPeriodID2.SelectedValue))
                    query.Where(query.PayrollPeriodID == -1);
                else
                {
                    var payrollPeriod = new PayrollPeriod();
                    if (payrollPeriod.LoadByPrimaryKey(Convert.ToInt32(cboPayrollPeriodID2.SelectedValue)))
                        query.Where(query.PayrollPeriodID == payrollPeriod.PayrollPeriodID);
                    else 
                        query.Where(query.PayrollPeriodID == -1);
                }

                query.OrderBy(query.ScheduleInDate.Ascending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdWorkSchedule2.Rebind();
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

        private void PopulateEmployeeImage(int personId, string gender)
        {
            // Load from database
            var personalImg = new PersonalImage();
            if (personalImg.LoadByPrimaryKey(personId))
            {
                // Show Image
                if (personalImg.Photo != null)
                {
                    imgPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(personalImg.Photo));
                }
                else
                {
                    imgPhoto.ImageUrl = gender == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPhoto.ImageUrl = gender == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
        }
    }
}