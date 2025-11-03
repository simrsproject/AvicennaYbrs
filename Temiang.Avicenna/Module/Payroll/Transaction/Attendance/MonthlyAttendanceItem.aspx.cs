using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

namespace Temiang.Avicenna.Module.Payroll.Transaction.Attendance
{
    public partial class MonthlyAttendanceItem : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "MonthlyAttendanceSearch.aspx";
            UrlPageList = "MonthlyAttendanceList.aspx";

            ProgramID = AppConstant.Program.MonthlyAttendance;

            WindowSearch.Height = 300;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRAttedanceInsentif, AppEnum.StandardReference.AttedanceInsentif);
                RadTabStrip1.Tabs[1].Visible = AppSession.Parameter.HealthcareInitial == "RSI";
                RadTabStrip1.Tabs[2].Visible = AppSession.Parameter.HealthcareInitial == "RSI";
            }
        }

        protected override void OnMenuNewClick()
        {
            ViewState["MonthlyAttendanceID"] = -1;

            cboPayrollPeriodID.Text = string.Empty;
            cboPersonID.Text = string.Empty;
            cboSRAttedanceInsentif.SelectedValue = string.Empty;

            OnPopulateEntryControl(new MonthlyAttendance());
        }

        protected override void OnMenuEditClick()
        {
            //ViewState["MonthlyAttendanceID"] = Request.QueryString["id"];
            if (MonthlyAttendanceDetails.Count > 0)
            {
                cboPayrollPeriodID.Enabled = false;
                cboPersonID.Enabled = false;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            MonthlyAttendance entity = new MonthlyAttendance();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["MonthlyAttendanceID"])))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            MonthlyAttendance entity = new MonthlyAttendance();
            entity = new MonthlyAttendance();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            MonthlyAttendance entity = new MonthlyAttendance();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["MonthlyAttendanceID"])))
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("MonthlyAttendanceID='{0}'", ViewState["MonthlyAttendanceID"].ToString());
            auditLogFilter.TableName = "MonthlyAttendance";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            MonthlyAttendance entity = new MonthlyAttendance();
            if (parameters.Length > 0)
            {
                string monthlyAttendanceID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(monthlyAttendanceID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(ViewState["MonthlyAttendanceID"]));
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            MonthlyAttendance montlyAttedance = (MonthlyAttendance)entity;

            ViewState["MonthlyAttendanceID"] = montlyAttedance.MonthlyAttendanceID ?? -1;

            VwEmployeeTableQuery personal = new VwEmployeeTableQuery();
            personal.Where(personal.PersonID == Convert.ToInt32(montlyAttedance.PersonID ?? 0));
            var dtb = personal.LoadDataTable();
            cboPersonID.DataSource = dtb;
            cboPersonID.DataBind();
            cboPersonID.SelectedValue = montlyAttedance.PersonID.ToString();
            if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
            {
                cboPersonID.Text = dtb.Rows[0]["EmployeeNumber"].ToString() + " - " + dtb.Rows[0]["EmployeeName"].ToString();
                GetServiceUnitName(cboPersonID.SelectedValue.ToInt());
            }
            else
                txtServiceUnitName.Text = string.Empty;

            PayrollPeriodQuery period = new PayrollPeriodQuery();
            period.Where(period.PayrollPeriodID == Convert.ToInt32(montlyAttedance.PayrollPeriodID ?? 0));
            dtb = period.LoadDataTable();
            cboPayrollPeriodID.DataSource = dtb;
            cboPayrollPeriodID.DataBind();
            cboPayrollPeriodID.SelectedValue = montlyAttedance.PayrollPeriodID.ToString();
            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                cboPayrollPeriodID.Text = dtb.Rows[0]["PayrollPeriodName"].ToString();

            txtPayDays.Value = Convert.ToDouble(montlyAttedance.PayDays ?? 0);
            txtUnPayDays.Value = Convert.ToDouble(montlyAttedance.UnPayDays ?? 0);
            txtAbsenceCount.Value = Convert.ToDouble(montlyAttedance.AbsenceCount ?? 0);
            cboSRAttedanceInsentif.SelectedValue = montlyAttedance.SRAttedanceInsentif;
            txtWorkingDays.Value = Convert.ToDouble(montlyAttedance.WorkingDays ?? 0);
            txtHolidays.Value = Convert.ToDouble(montlyAttedance.Holidays ?? 0);
            txtOvertimeDays.Value = Convert.ToDouble(montlyAttedance.OvertimeDays ?? 0);
            txtHolidayWorking.Value = Convert.ToDouble(montlyAttedance.HolidayWorking ?? 0);
            txtLateDays.Value = Convert.ToDouble(montlyAttedance.LateDays ?? 0);
            txtEarlyLeaveDays.Value = Convert.ToDouble(montlyAttedance.EarlyLeaveDays ?? 0);
            txtBasicWorkingTime.Value = Convert.ToDouble(montlyAttedance.BasicWorkingTime ?? 0);
            txtOvertimeHours.Value = Convert.ToDouble(montlyAttedance.OvertimeHours ?? 0);
            txtConvertOvertimeHours.Value = Convert.ToDouble(montlyAttedance.ConvertOvertimeHours ?? 0);
            txtTotalWorkingTime.Value = Convert.ToDouble(montlyAttedance.TotalWorkingTime ?? 0);
            txtBasicFoodExspenses.Value = Convert.ToDouble(montlyAttedance.BasicFoodExspenses ?? 0);
            txtOvertimeFoodExspenses.Value = Convert.ToDouble(montlyAttedance.OvertimeFoodExspenses ?? 0);
            txtRamadhanFoodExspenses.Value = Convert.ToDouble(montlyAttedance.RamadhanFoodExspenses ?? 0);
            txtShift1Compensation.Value = Convert.ToDouble(montlyAttedance.Shift1Compensation ?? 0);
            txtShift2Compensation.Value = Convert.ToDouble(montlyAttedance.Shift2Compensation ?? 0);
            txtShift3Compensation.Value = Convert.ToDouble(montlyAttedance.Shift3Compensation ?? 0);
            txtShift4Compensation.Value = Convert.ToDouble(montlyAttedance.Shift4Compensation ?? 0);

            //Display Data Detail
            PopulateItemGrid();
        }

        private void SetEntityValue(MonthlyAttendance entity)
        {
            entity.PersonID = Convert.ToInt32(cboPersonID.SelectedValue);
            entity.PayrollPeriodID = Convert.ToInt32(cboPayrollPeriodID.SelectedValue);
            entity.PayDays = Convert.ToInt32(txtPayDays.Value);
            entity.UnPayDays = Convert.ToInt32(txtUnPayDays.Value);
            entity.AbsenceCount = Convert.ToInt32(txtAbsenceCount.Value);
            entity.SRAttedanceInsentif = cboSRAttedanceInsentif.SelectedValue;
            entity.WorkingDays = Convert.ToInt32(txtWorkingDays.Value);
            entity.Holidays = Convert.ToInt32(txtHolidays.Value);
            entity.OvertimeDays = Convert.ToInt32(txtOvertimeDays.Value);
            entity.HolidayWorking = Convert.ToInt32(txtHolidayWorking.Value);
            entity.LateDays = Convert.ToInt32(txtLateDays.Value);
            entity.EarlyLeaveDays = Convert.ToInt32(txtEarlyLeaveDays.Value);
            entity.BasicWorkingTime = Convert.ToDecimal(txtBasicWorkingTime.Value);
            entity.OvertimeHours = Convert.ToDecimal(txtOvertimeHours.Value);
            entity.ConvertOvertimeHours = Convert.ToDecimal(txtConvertOvertimeHours.Value);
            entity.TotalWorkingTime = Convert.ToDecimal(txtTotalWorkingTime.Value);
            entity.BasicFoodExspenses = Convert.ToInt32(txtBasicFoodExspenses.Value);
            entity.OvertimeFoodExspenses = Convert.ToInt32(txtOvertimeFoodExspenses.Value);
            entity.RamadhanFoodExspenses = Convert.ToInt32(txtRamadhanFoodExspenses.Value);
            entity.Shift1Compensation = Convert.ToInt32(txtShift1Compensation.Value);
            entity.Shift2Compensation = Convert.ToInt32(txtShift2Compensation.Value);
            entity.Shift3Compensation = Convert.ToInt32(txtShift3Compensation.Value);
            entity.Shift4Compensation = Convert.ToInt32(txtShift4Compensation.Value);

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
        }

        private void SaveEntity(MonthlyAttendance entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                MonthlyAttendanceDetails.Save();
                MonthlyAttendanceDetailHistories.Save();

                try
                {
                    ViewState["MonthlyAttendanceID"] = entity.MonthlyAttendanceID;
                }
                catch
                { }

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //int x = MonthlyAttendanceDetail.ProcessCalculation(cboPayrollPeriodID.SelectedValue.ToInt(), AppSession.UserLogin.UserID, Convert.ToInt32(entity.PersonID));
        }

        private void MoveRecord(bool isNextRecord)
        {
            MonthlyAttendanceQuery que = new MonthlyAttendanceQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.MonthlyAttendanceID > Convert.ToInt32(ViewState["MontlyAttedanceID"]));
                que.OrderBy(que.MonthlyAttendanceID.Ascending);
            }
            else
            {
                que.Where(que.MonthlyAttendanceID < Convert.ToInt32(ViewState["MontlyAttedanceID"]));
                que.OrderBy(que.MonthlyAttendanceID.Descending);
            }
            MonthlyAttendance entity = new MonthlyAttendance();
            entity.Load(que);

            ViewState["MontlyAttedanceID"] = entity.MonthlyAttendanceID;

            OnPopulateEntryControl(entity);
        }

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            VwEmployeeTableQuery query = new VwEmployeeTableQuery();
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

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }

        #region Record Detail Method Function EmployeeSalaryMatrix

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            //bool isVisible = (newVal != AppEnum.DataMode.Read);
            //grdAliasName.Columns[0].Visible = isVisible;

            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdDetail.Columns[0].Visible = isVisible;

            grdDetail.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;
            grdDetail.Columns[grdDetail.Columns.Count - 1].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdDetail.Rebind();

            grdSummary.Columns[0].Visible = !isVisible;
            grdSummary.Rebind();
        }

        private MonthlyAttendanceDetailCollection MonthlyAttendanceDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collMonthlyAttendanceDetail"];
                    if (obj != null)
                    {
                        return ((MonthlyAttendanceDetailCollection)(obj));
                    }
                }

                var coll = new MonthlyAttendanceDetailCollection();
                var query = new MonthlyAttendanceDetailQuery("a");

                query.Where(query.PersonID == (string.IsNullOrWhiteSpace(cboPersonID.SelectedValue) ? -1 : cboPersonID.SelectedValue.ToInt()),
                    query.PayrollPeriodID == (string.IsNullOrWhiteSpace(cboPayrollPeriodID.SelectedValue) ? -1 : cboPayrollPeriodID.SelectedValue.ToInt()));
                query.OrderBy(query.ScheduleInDate.Ascending);
                coll.Load(query);

                Session["collMonthlyAttendanceDetail"] = coll;
                return coll;
            }
            set { Session["collMonthlyAttendanceDetail"] = value; }
        }

        private MonthlyAttendanceDetailHistoryCollection MonthlyAttendanceDetailHistories
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collMonthlyAttendanceDetailHistory"];
                    if (obj != null)
                    {
                        return ((MonthlyAttendanceDetailHistoryCollection)(obj));
                    }
                }

                var pp = new PayrollPeriod();
                pp.Query.Where(pp.Query.PayrollPeriodID == (string.IsNullOrWhiteSpace(cboPayrollPeriodID.SelectedValue) ? -1 : cboPayrollPeriodID.SelectedValue.ToInt()));
                var load = pp.Query.Load();

                var coll = new MonthlyAttendanceDetailHistoryCollection();
                var query = new MonthlyAttendanceDetailHistoryQuery("a");

                query.Where(query.PersonID == (string.IsNullOrWhiteSpace(cboPersonID.SelectedValue) ? -1 : cboPersonID.SelectedValue.ToInt()));
                if (load) query.Where($"<MONTH(a.CheckInDateTime)={pp.SPTMonth ?? -1} AND YEAR(a.CheckInDateTime)={pp.SPTYear ?? -1}>");
                else query.Where(query.WorkingHourID == -1);
                coll.Load(query);

                Session["collMonthlyAttendanceDetailHistory"] = coll;
                return coll;
            }
            set { Session["collMonthlyAttendanceDetailHistory"] = value; }
        }

        private MonthlyAttendanceDetailSummaryCollection MonthlyAttendanceDetailSummaries
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collMonthlyAttendanceDetailSummary"];
                    if (obj != null)
                    {
                        return ((MonthlyAttendanceDetailSummaryCollection)(obj));
                    }
                }

                var coll = new MonthlyAttendanceDetailSummaryCollection();
                var query = new MonthlyAttendanceDetailSummaryQuery("a");
                var mad = new MonthlyAttendanceDetailQuery("b");
                var wh = new WorkingHourQuery("c");
                query.Select(query,
                    wh.WorkingHourName.As("refToWorkingHour_WorkingHourName"),
                    mad.ScheduleInDate.As("refToMonthlyAttendanceDetail_ScheduleInDate"),
                    mad.ScheduleInTime.As("refToMonthlyAttendanceDetail_ScheduleInTime"),
                    mad.ScheduleOutDate.As("refToMonthlyAttendanceDetail_ScheduleOutDate"),
                    mad.ScheduleOutTime.As("refToMonthlyAttendanceDetail_ScheduleOutTime")
                    );
                query.InnerJoin(mad).On(mad.MonthlyAttendanceDetailID == query.MonthlyAttendanceDetailID);
                query.LeftJoin(wh).On(wh.WorkingHourID == mad.WorkingHourID);
                query.Where(mad.PersonID == (string.IsNullOrWhiteSpace(cboPersonID.SelectedValue) ? -1 : cboPersonID.SelectedValue.ToInt()),
                    mad.PayrollPeriodID == (string.IsNullOrWhiteSpace(cboPayrollPeriodID.SelectedValue) ? -1 : cboPayrollPeriodID.SelectedValue.ToInt()));
                query.OrderBy(mad.ScheduleInDate.Ascending);
                coll.Load(query);

                Session["collMonthlyAttendanceDetailSummary"] = coll;
                return coll;
            }
            set { Session["collMonthlyAttendanceDetailSummary"] = value; }
        }

        private EmployeeLeaveRequestCollection EmployeeLeaveRequests
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeLeaveRequest"];
                    if (obj != null)
                    {
                        return ((EmployeeLeaveRequestCollection)(obj));
                    }
                }

                var coll = new EmployeeLeaveRequestCollection();
                var query = new EmployeeLeaveRequestQuery("a");
                var el = new EmployeeLeaveQuery("b");
                var elt = new AppStandardReferenceItemQuery("c");
                var wd = new AppStandardReferenceItemQuery("d");
                query.Select(query,
                    elt.ItemName.As("EmployeeLeaveTypeName"),
                    wd.ItemName.As("WorkingDayName")
                    );
                query.InnerJoin(el).On(el.EmployeeLeaveID == query.EmployeeLeaveID);
                query.InnerJoin(elt).On(elt.StandardReferenceID == AppEnum.StandardReference.EmployeeLeaveType.ToString() && elt.ItemID == el.SREmployeeLeaveType);
                query.InnerJoin(wd).On(wd.StandardReferenceID == AppEnum.StandardReference.WorkingDay.ToString() && wd.ItemID == query.SRWorkingDay);
                query.Where(query.PersonID == (string.IsNullOrWhiteSpace(cboPersonID.SelectedValue) ? -1 : cboPersonID.SelectedValue.ToInt()),
                    query.IsVerified == true,
                    query.PayrollPeriodID == (string.IsNullOrWhiteSpace(cboPayrollPeriodID.SelectedValue) ? -1 : cboPayrollPeriodID.SelectedValue.ToInt()),
                    query.IsPayCut == true);
                query.OrderBy(query.RequestLeaveDateFrom.Ascending);
                coll.Load(query);

                Session["collEmployeeLeaveRequest"] = coll;
                return coll;
            }
            set { Session["collEmployeeLeaveRequest"] = value; }
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            MonthlyAttendanceDetails = null; //Reset Record Detail
            grdDetail.DataSource = MonthlyAttendanceDetails; //Requery
            grdDetail.MasterTableView.IsItemInserted = false;
            grdDetail.MasterTableView.ClearEditItems();
            grdDetail.DataBind();

            MonthlyAttendanceDetailHistories = null;
            var histories = MonthlyAttendanceDetailHistories;

            MonthlyAttendanceDetailSummaries = null;
            var summaries = MonthlyAttendanceDetailSummaries;

            EmployeeLeaveRequests = null;
            var leaves = EmployeeLeaveRequests;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = MonthlyAttendanceDetails;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int64 id = Convert.ToInt64(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][MonthlyAttendanceDetailMetadata.ColumnNames.MonthlyAttendanceDetailID]);
            MonthlyAttendanceDetail entity = FindItem(id);

            MonthlyAttendanceDetailHistory history = FindItemHistory(entity.CheckInDate?.Add(TimeSpan.ParseExact(entity.CheckInTime, "hh\\:mm", null)), entity.WorkingHourID);
            if (history == null)
            {
                history = MonthlyAttendanceDetailHistories.AddNew();
                history.PersonID = entity.PersonID;
                history.WorkingHourID = entity.WorkingHourID;
                history.CheckInDateTime = entity.CheckInDate?.Add(TimeSpan.ParseExact(entity.CheckInTime, "hh\\:mm", null));
            }

            if (entity != null && history != null) SetEntityValue(entity, history, e);
        }

        private MonthlyAttendanceDetail FindItem(Int64 id)
        {
            MonthlyAttendanceDetailCollection coll = MonthlyAttendanceDetails;
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

        private MonthlyAttendanceDetailHistory FindItemHistory(DateTime? checkIn, int? workingHourID)
        {
            MonthlyAttendanceDetailHistoryCollection coll = MonthlyAttendanceDetailHistories;
            MonthlyAttendanceDetailHistory retEntity = null;
            foreach (MonthlyAttendanceDetailHistory rec in coll)
            {
                if (rec.CheckInDateTime.Equals(checkIn) && rec.WorkingHourID.Equals(workingHourID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(MonthlyAttendanceDetail entity, MonthlyAttendanceDetailHistory history, GridCommandEventArgs e)
        {
            var userControl = (MonthlyAttendanceItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.MonthlyAttendanceDetailID = userControl.MonthlyAttendanceDetailID;
                entity.PersonID = cboPersonID.SelectedValue.ToInt();
                entity.PayrollPeriodID = cboPayrollPeriodID.SelectedValue.ToInt();
                entity.ScheduleInDate = userControl.ScheduleInDate;
                entity.ScheduleOutDate = userControl.ScheduleOutDate;
                entity.ScheduleInTime = userControl.ScheduleInTime;
                entity.ScheduleOutTime = userControl.ScheduleOutTime;
                entity.CheckInDate = userControl.CheckInDate;
                entity.CheckInTime = userControl.CheckInTime;
                entity.CheckOutDate = userControl.CheckOutDate;
                entity.CheckOutTime = userControl.CheckOutTime;
                entity.IsOvertime = userControl.IsOvertime;
                entity.OvertimeHours = userControl.OvertimeHours;
                entity.LateMinutes = userControl.LateMinutes;
                entity.LateCutPercentage = userControl.LateCutPercentage;
                entity.EarlyLeaveMinutes = userControl.EarlyLeaveMinutes;
                entity.EarlyLeaveCutPercentage = userControl.EarlyLeaveCutPercentage;
                entity.IsHasPermission = userControl.IsHasPermission;
                entity.IsInvalid = userControl.IsInvalid;

                entity.SRAttendanceFileFormat = "";
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;

                entity.WorkingHourID = userControl.WorkingHourID;

                history.PersonID = entity.PersonID;
                history.WorkingHourID = entity.WorkingHourID;
                history.CheckInDateTime = entity.CheckInDate?.Add(TimeSpan.ParseExact(entity.CheckInTime, "hh\\:mm", null));
                if (entity.CheckOutDate != null && !string.IsNullOrWhiteSpace(entity.CheckOutTime)) history.CheckOutDateTime = entity.CheckOutDate?.Add(TimeSpan.ParseExact(entity.CheckOutTime, "hh\\:mm", null));
            }
        }

        #endregion

        private int GetWorkingHourID(int day, Temiang.Avicenna.BusinessObject.WorkingScheduleDetail workingScheduleDetail)
        {
            if (day == 1)
                return workingScheduleDetail.WorkingHourIDDay1 ?? -1;
            else if (day == 2)
                return workingScheduleDetail.WorkingHourIDDay2 ?? -1;
            else if (day == 3)
                return workingScheduleDetail.WorkingHourIDDay3 ?? -1;
            else if (day == 4)
                return workingScheduleDetail.WorkingHourIDDay4 ?? -1;
            else if (day == 5)
                return workingScheduleDetail.WorkingHourIDDay5 ?? -1;
            else if (day == 6)
                return workingScheduleDetail.WorkingHourIDDay6 ?? -1;
            else if (day == 7)
                return workingScheduleDetail.WorkingHourIDDay7 ?? -1;
            else if (day == 8)
                return workingScheduleDetail.WorkingHourIDDay8 ?? -1;
            else if (day == 9)
                return workingScheduleDetail.WorkingHourIDDay9 ?? -1;
            else if (day == 10)
                return workingScheduleDetail.WorkingHourIDDay10 ?? -1;
            else if (day == 11)
                return workingScheduleDetail.WorkingHourIDDay11 ?? -1;
            else if (day == 12)
                return workingScheduleDetail.WorkingHourIDDay12 ?? -1;
            else if (day == 13)
                return workingScheduleDetail.WorkingHourIDDay13 ?? -1;
            else if (day == 14)
                return workingScheduleDetail.WorkingHourIDDay14 ?? -1;
            else if (day == 15)
                return workingScheduleDetail.WorkingHourIDDay15 ?? -1;
            else if (day == 16)
                return workingScheduleDetail.WorkingHourIDDay16 ?? -1;
            else if (day == 17)
                return workingScheduleDetail.WorkingHourIDDay17 ?? -1;
            else if (day == 18)
                return workingScheduleDetail.WorkingHourIDDay18 ?? -1;
            else if (day == 19)
                return workingScheduleDetail.WorkingHourIDDay19 ?? -1;
            else if (day == 20)
                return workingScheduleDetail.WorkingHourIDDay20 ?? -1;
            else if (day == 21)
                return workingScheduleDetail.WorkingHourIDDay21 ?? -1;
            else if (day == 22)
                return workingScheduleDetail.WorkingHourIDDay22 ?? -1;
            else if (day == 23)
                return workingScheduleDetail.WorkingHourIDDay23 ?? -1;
            else if (day == 24)
                return workingScheduleDetail.WorkingHourIDDay24 ?? -1;
            else if (day == 25)
                return workingScheduleDetail.WorkingHourIDDay25 ?? -1;
            else if (day == 26)
                return workingScheduleDetail.WorkingHourIDDay26 ?? -1;
            else if (day == 27)
                return workingScheduleDetail.WorkingHourIDDay27 ?? -1;
            else if (day == 28)
                return workingScheduleDetail.WorkingHourIDDay28 ?? -1;
            else if (day == 29)
                return workingScheduleDetail.WorkingHourIDDay29 ?? -1;
            else if (day == 30)
                return workingScheduleDetail.WorkingHourIDDay30 ?? -1;
            else if (day == 31)
                return workingScheduleDetail.WorkingHourIDDay31 ?? -1;

            return -1;
        }

        private int GetWorkingHourID(int day, Temiang.Avicenna.BusinessObject.WorkingSchduleInterventionDetail workingScheduleDetail)
        {
            if (day == 1)
                return workingScheduleDetail.WorkingHourIDDay1 ?? -1;
            else if (day == 2)
                return workingScheduleDetail.WorkingHourIDDay2 ?? -1;
            else if (day == 3)
                return workingScheduleDetail.WorkingHourIDDay3 ?? -1;
            else if (day == 4)
                return workingScheduleDetail.WorkingHourIDDay4 ?? -1;
            else if (day == 5)
                return workingScheduleDetail.WorkingHourIDDay5 ?? -1;
            else if (day == 6)
                return workingScheduleDetail.WorkingHourIDDay6 ?? -1;
            else if (day == 7)
                return workingScheduleDetail.WorkingHourIDDay7 ?? -1;
            else if (day == 8)
                return workingScheduleDetail.WorkingHourIDDay8 ?? -1;
            else if (day == 9)
                return workingScheduleDetail.WorkingHourIDDay9 ?? -1;
            else if (day == 10)
                return workingScheduleDetail.WorkingHourIDDay10 ?? -1;
            else if (day == 11)
                return workingScheduleDetail.WorkingHourIDDay11 ?? -1;
            else if (day == 12)
                return workingScheduleDetail.WorkingHourIDDay12 ?? -1;
            else if (day == 13)
                return workingScheduleDetail.WorkingHourIDDay13 ?? -1;
            else if (day == 14)
                return workingScheduleDetail.WorkingHourIDDay14 ?? -1;
            else if (day == 15)
                return workingScheduleDetail.WorkingHourIDDay15 ?? -1;
            else if (day == 16)
                return workingScheduleDetail.WorkingHourIDDay16 ?? -1;
            else if (day == 17)
                return workingScheduleDetail.WorkingHourIDDay17 ?? -1;
            else if (day == 18)
                return workingScheduleDetail.WorkingHourIDDay18 ?? -1;
            else if (day == 19)
                return workingScheduleDetail.WorkingHourIDDay19 ?? -1;
            else if (day == 20)
                return workingScheduleDetail.WorkingHourIDDay20 ?? -1;
            else if (day == 21)
                return workingScheduleDetail.WorkingHourIDDay21 ?? -1;
            else if (day == 22)
                return workingScheduleDetail.WorkingHourIDDay22 ?? -1;
            else if (day == 23)
                return workingScheduleDetail.WorkingHourIDDay23 ?? -1;
            else if (day == 24)
                return workingScheduleDetail.WorkingHourIDDay24 ?? -1;
            else if (day == 25)
                return workingScheduleDetail.WorkingHourIDDay25 ?? -1;
            else if (day == 26)
                return workingScheduleDetail.WorkingHourIDDay26 ?? -1;
            else if (day == 27)
                return workingScheduleDetail.WorkingHourIDDay27 ?? -1;
            else if (day == 28)
                return workingScheduleDetail.WorkingHourIDDay28 ?? -1;
            else if (day == 29)
                return workingScheduleDetail.WorkingHourIDDay29 ?? -1;
            else if (day == 30)
                return workingScheduleDetail.WorkingHourIDDay30 ?? -1;
            else if (day == 31)
                return workingScheduleDetail.WorkingHourIDDay31 ?? -1;

            return -1;
        }

        protected void grdDetail_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var entity = MonthlyAttendanceDetails.AddNew();
            var history = MonthlyAttendanceDetailHistories.AddNew();

            SetEntityValue(entity, history, e);

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        protected void grdDetail_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            Int64 id = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex][MonthlyAttendanceDetailMetadata.ColumnNames.MonthlyAttendanceDetailID]);
            MonthlyAttendanceDetail entity = FindItem(id);

            MonthlyAttendanceDetailHistory history = FindItemHistory(entity.CheckInDate?.Add(TimeSpan.ParseExact(entity.CheckInTime, "hh\\:mm", null)), entity.WorkingHourID);
            if (history != null) history.MarkAsDeleted();

            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdSummary_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSummary.DataSource = MonthlyAttendanceDetailSummaries;
        }

        protected void grdLeave_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLeave.DataSource = EmployeeLeaveRequests;
        }

        protected void cboPersonID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value)) return;
            GetServiceUnitName(e.Value.ToInt());
        }

        private void GetServiceUnitName(int personId)
        {
            var empq = new VwEmployeeTableQuery();
            empq.Where(empq.PersonID == personId);

            var emp = new VwEmployeeTable();
            emp.Load(empq);

            var org = new OrganizationUnit();
            if (org.LoadByPrimaryKey(emp.ServiceUnitID.ToInt()))
                txtServiceUnitName.Text = org.OrganizationUnitName;
            else
                txtServiceUnitName.Text = string.Empty;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("recalculate"))
            {
                Int64 id = Convert.ToInt64(eventArgument.Split('|')[1]);
                if (id == -1)
                {
                    foreach (var item in MonthlyAttendanceDetailSummaries)
                    {
                        int result = MonthlyAttendanceDetailSummary.RecalculateSummaryValue(item.MonthlyAttendanceDetailID.ToInt());
                    }
                }
                else
                {
                    int result = MonthlyAttendanceDetailSummary.RecalculateSummaryValue(id);
                }

                MonthlyAttendanceDetailSummaries = null;
                var summaries = MonthlyAttendanceDetailSummaries;
                grdSummary.Rebind();
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdSummary, grdSummary);
        }
    }
}