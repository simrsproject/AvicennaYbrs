using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class WorkScheduleDetailItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox cboPayrollPeriodID
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboPayrollPeriodID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtMonthlyAttendanceDetailID.Value = 1;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtScheduleInDate.Enabled = false;
            txtScheduleInDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInDate);
            txtScheduleOutDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutDate);
            txtScheduleInTime.SelectedDate = Helper.GetForDisplayTime((String)DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleInTime));
            txtScheduleOutTime.SelectedDate = Helper.GetForDisplayTime((String)DataBinder.Eval(DataItem, MonthlyAttendanceDetailMetadata.ColumnNames.ScheduleOutTime));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (MonthlyAttendanceDetailCollection)Session["collEmployeeWorkSchedule"];

                DateTime id = txtScheduleInDate.SelectedDate.Value.Date;
                bool isExist = false;
                foreach (MonthlyAttendanceDetail item in coll)
                {
                    if (item.ScheduleInDate.Value.Date.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Schedule In Date: {0} has exist", id.ToString("dd/MM/yyyy"));
                    return;
                }

                var payrollPeriod = new PayrollPeriod();
                if (payrollPeriod.LoadByPrimaryKey(Convert.ToInt32(cboPayrollPeriodID.SelectedValue)))
                {
                    if (id < payrollPeriod.StartDate || id > payrollPeriod.EndDate)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = "Schedule date does not match with payroll period";
                    }
                }
            }
        }

        #region Properties for return entry value
        public Int64 MonthlyAttendanceDetailID
        {
            get { return Convert.ToInt64(txtMonthlyAttendanceDetailID.Text); }
        }

        public DateTime ScheduleInDate
        {
            get { return txtScheduleInDate.SelectedDate.Value.Date; }
        }

        public DateTime ScheduleOutDate
        {
            get { return txtScheduleOutDate.SelectedDate.Value.Date; }
        }

        public String ScheduleInTime
        {
            get { return Helper.GetHourMinute(txtScheduleInTime.SelectedDate); }
        }

        public String ScheduleOutTime
        {
            get { return Helper.GetHourMinute(txtScheduleOutTime.SelectedDate); }
        }

        #endregion
    }
}