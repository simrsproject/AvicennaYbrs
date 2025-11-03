using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Transaction.Attendance
{
    public partial class MonthlyAttendanceItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            var hours = new WorkingHourCollection();
            hours.Query.Where(hours.Query.IsActive == true);
            hours.Query.Load();

            cboWorkingHour.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var hour in hours)
            {
                cboWorkingHour.Items.Add(new RadComboBoxItem(hour.WorkingHourName, (hour.WorkingHourID ?? -1).ToString()));
            }

            txtSchedule.SelectedDate = DateTime.Now.Date;
            txtSchedule_SelectedDateChanged(null, new Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs(null, txtSchedule.SelectedDate));

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row

                txtMonthlyAttendanceDetailID.Value = 1;

                //if (AppSession.Parameter.IsSeparateScheduleAndAttendanceSheet)
                //{
                //    txtMonthlyAttendanceDetailID.Enabled = false;
                //    txtScheduleInDate.Enabled = false;
                //    txtScheduleInTime.Enabled = false;
                //}

                return;
            }
            ViewState["IsNewRecord"] = false;

            //txtMonthlyAttendanceDetailID.Enabled = false;
            //txtScheduleInDate.Enabled = false;
            //txtScheduleInTime.Enabled = false;

            txtMonthlyAttendanceDetailID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.MonthlyAttendanceDetailID));

            txtSchedule.SelectedDate = (DateTime?)(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInDate));
            txtSchedule_SelectedDateChanged(null, new Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs(null, txtSchedule.SelectedDate));

            txtScheduleInDate.SelectedDate = (DateTime?)(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInDate));
            txtScheduleOutDate.SelectedDate = (DateTime?)(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutDate));
            txtScheduleInTime.SelectedDate = Helper.GetForDisplayTime((String)DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInTime));
            txtScheduleOutTime.SelectedDate = Helper.GetForDisplayTime((String)DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutTime));
            txtCheckInDate.SelectedDate = (DateTime?)(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.CheckInDate));
            txtCheckInTime.SelectedDate = Helper.GetForDisplayTime((String)DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.CheckInTime));
            txtCheckOutDate.SelectedDate = (DateTime?)(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.CheckOutDate));
            txtCheckOutTime.SelectedDate = Helper.GetForDisplayTime((String)DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.CheckOutTime));
            chkIsOvertime.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.IsOvertime));
            txtOvertimeHours.Value = Convert.ToDouble(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.OvertimeHours));
            txtLateMinutes.Value = Convert.ToDouble(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.LateMinutes));
            txtLateCutPercentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.LateCutPercentage));
            txtEarlyLeaveMinutes.Value = Convert.ToDouble(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.EarlyLeaveMinutes));
            txtEarlyLeaveCutPercentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.EarlyLeaveCutPercentage));
            chkIsHasPermission.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.IsHasPermission));
            chkIsInvalid.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.IsInvalid));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (MonthlyAttendanceDetailCollection)Session["collMonthlyAttendanceDetail"];

                DateTime? scheduleDateIn = txtScheduleInDate.SelectedDate;
                string scheduleTimeIn = Helper.GetHourMinute(txtScheduleInTime.SelectedDate);
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.ScheduleInDate.Equals(scheduleDateIn) && item.ScheduleInTime.Equals(scheduleTimeIn))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Schedule In DateTime : {0} {1} already exist", scheduleDateIn?.ToString("MM/dd/yyyy"), scheduleTimeIn);
                }
            }
        }

        private RadComboBox cboPayrollPeriodID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboPayrollPeriodID");
            }
        }

        private RadComboBox cboPersonID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboPersonID");
            }
        }

        #region Properties for return entry value
        public Int64 MonthlyAttendanceDetailID
        {
            get { return Convert.ToInt64(txtMonthlyAttendanceDetailID.Text); }
        }
        public DateTime ScheduleInDate
        {
            get { return Convert.ToDateTime(txtScheduleInDate.SelectedDate); }
        }
        public DateTime ScheduleOutDate
        {
            get { return Convert.ToDateTime(txtScheduleOutDate.SelectedDate); }
        }
        public String ScheduleInTime
        {
            get { return Helper.GetHourMinute(txtScheduleInTime.SelectedDate); }
        }
        public String ScheduleOutTime
        {
            get { return Helper.GetHourMinute(txtScheduleOutTime.SelectedDate); }
        }
        public DateTime CheckInDate
        {
            get { return Convert.ToDateTime(txtCheckInDate.SelectedDate); }
        }
        public String CheckInTime
        {
            get { return Helper.GetHourMinute(txtCheckInTime.SelectedDate); }
        }
        public DateTime? CheckOutDate
        {
            get { return txtCheckOutDate.SelectedDate; }
        }
        public String CheckOutTime
        {
            get { return Helper.GetHourMinute(txtCheckOutTime.SelectedDate); }
        }
        public Boolean IsOvertime
        {
            get { return chkIsOvertime.Checked; }
        }
        public decimal OvertimeHours
        {
            get { return Convert.ToDecimal(txtOvertimeHours.Value); }
        }

        public Int16 LateMinutes
        {
            get { return Convert.ToInt16(txtLateMinutes.Value); }
        }
        public Decimal LateCutPercentage
        {
            get { return Convert.ToDecimal(txtLateCutPercentage.Value); }
        }
        public Int16 EarlyLeaveMinutes
        {
            get { return Convert.ToInt16(txtEarlyLeaveMinutes.Value); }
        }
        public Decimal EarlyLeaveCutPercentage
        {
            get { return Convert.ToDecimal(txtEarlyLeaveCutPercentage.Value); }
        }
        public Boolean IsHasPermission
        {
            get { return chkIsHasPermission.Checked; }
        }
        public Boolean IsInvalid
        {
            get { return chkIsInvalid.Checked; }
        }
        public int WorkingHourID
        {
            get { return string.IsNullOrWhiteSpace(cboWorkingHour.SelectedValue) ? -1 : cboWorkingHour.SelectedValue.ToInt(); }
        }

        #endregion

        protected void txtSchedule_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            var hour = GetWorkingHour(!string.IsNullOrWhiteSpace(cboPersonID.SelectedValue) ? cboPersonID.SelectedValue.ToInt() : -1,
                !string.IsNullOrWhiteSpace(cboPayrollPeriodID.SelectedValue) ? cboPayrollPeriodID.SelectedValue.ToInt() : -1, e.NewDate.Value);
            if (hour != null)
            {
                cboWorkingHour.SelectedValue = hour.WorkingHourID.ToString();
                cboWorkingHour_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, (hour.WorkingHourID ?? -1).ToString(), string.Empty));
            }
        }

        protected void cboWorkingHour_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var hour = new WorkingHour();
            hour.LoadByPrimaryKey(e.Value.ToInt());

            var date = txtSchedule.SelectedDate.Value.Date;

            //if (hour.SRShift != "ShiftID-013")
            //{
            txtScheduleInDate.SelectedDate = date;
            txtScheduleInTime.SelectedDate = Helper.GetForDisplayTime(hour.StartTime);
            txtCheckInDate.SelectedDate = date;

            if (hour.SRShift != "ShiftID-003")
            {
                txtScheduleOutDate.SelectedDate = (hour.IsCrossDay ?? false) ? date.AddDays(1) : date;
                txtScheduleOutTime.SelectedDate = Helper.GetForDisplayTime(hour.EndTime);
                txtCheckOutDate.SelectedDate = (hour.IsCrossDay ?? false) ? date.AddDays(1) : date;
            }
            else
            {
                txtScheduleOutDate.SelectedDate = date;
                txtScheduleOutTime.SelectedDate = Helper.GetForDisplayTime(hour.EndTime);
                txtCheckOutDate.SelectedDate = date;
            }
        }

        private int GetWorkingHourID(int day, BusinessObject.WorkingScheduleDetail workingScheduleDetail)
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

        private int GetWorkingHourID(int day, BusinessObject.WorkingSchduleInterventionDetail workingScheduleDetail)
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

        private WorkingHour GetWorkingHour(int personID, int payrollPeriodID, DateTime now)
        {
            var wh = new WorkingHour();

            var wsdq = new BusinessObject.WorkingScheduleDetailQuery("a");
            var wsq = new WorkingScheduleQuery("b");

            wsdq.es.Top = 1;
            wsdq.InnerJoin(wsq).On(wsdq.WorkingScheduleID == wsq.WorkingScheduleID && wsq.PayrollPeriodID == payrollPeriodID && wsq.IsApproved == true);
            wsdq.Where(wsdq.PersonID == personID);
            wsdq.Where($"<a.WorkingHourIDDay{now.Day} IS NOT NULL>");
            wsdq.OrderBy(wsdq.LastUpdateDateTime.Descending);

            var wsd = new BusinessObject.WorkingScheduleDetail();
            if (!wsd.Load(wsdq)) return null; // tidak punya jadwal

            var wsidq = new WorkingSchduleInterventionDetailQuery("a");
            var wsiq = new WorkingSchduleInterventionQuery("b");

            wsidq.es.Top = 1;
            wsidq.InnerJoin(wsiq).On(wsidq.WorkingSchduleInterventionID == wsiq.WorkingSchduleInterventionID && wsiq.IsApproved == true && wsiq.WorkingScheduleID == wsd.WorkingScheduleID);
            wsidq.Where(wsidq.PersonID == personID);
            wsidq.Where($"<a.WorkingHourIDDay{now.Day} IS NOT NULL>");
            wsidq.OrderBy(wsidq.LastUpdateDateTime.Descending);

            var wsidc = new WorkingSchduleInterventionDetailCollection();
            if (!wsidc.Load(wsidq))
            {
                wh = new WorkingHour();
                return wh.LoadByPrimaryKey(GetWorkingHourID(now.Day, wsd)) ? wh : null;
            }
            else
            {
                foreach (var wsid in wsidc)
                {
                    if (GetWorkingHourID(now.Day, wsid) == -1)
                    {
                        return wh.LoadByPrimaryKey(GetWorkingHourID(now.Day, wsd)) ? wh : null;
                    }
                    else
                    {
                        wh = new WorkingHour();
                        return wh.LoadByPrimaryKey(GetWorkingHourID(now.Day, wsid)) ? wh : null;
                    }
                }
            }

            return null;
        }
    }
}