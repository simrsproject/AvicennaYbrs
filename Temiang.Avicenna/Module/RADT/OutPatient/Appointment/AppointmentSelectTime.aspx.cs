using System;
using System.Data;
using System.Text;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.OutPatient
{
    public partial class AppointmentSelectTime : BasePageDialog
    {
        private DataTable Appointments
        {
            get
            {
                object obj = Session["AppointmentPerDate"];
                if (obj != null)
                {
                    return (DataTable)obj;
                }

                AppointmentQuery query = new AppointmentQuery("a");
                query.Select
                    (
                        query.AppointmentNo,
                        (query.FirstName + " : " + query.Notes).As("SubjectField"),
                        query.AppointmentDate, query.AppointmentTime, query.VisitDuration
                    );
                query.Where
                    (
                        query.ParamedicID == cboParamedicID.SelectedValue,
                        query.AppointmentDate == schedule.SelectedDate.Date,
                        query.SRAppointmentStatus.NotIn(AppSession.Parameter.AppointmentStatusCancel)
                    //query.SRAppointmentStatus.NotIn(AppSession.Parameter.AppointmentStatusCancel, AppSession.Parameter.AppointmentStatusClosed)
                    );

                DataTable dtbTemp = query.LoadDataTable();

                //Convert
                DataTable dtb = new DataTable();
                dtb.Columns.Add("AppointmentNo", Type.GetType("System.String"));
                dtb.Columns.Add("SubjectField", Type.GetType("System.String"));
                dtb.Columns.Add("StartDateTime", Type.GetType("System.DateTime"));
                dtb.Columns.Add("EndDateTime", Type.GetType("System.DateTime"));

                foreach (DataRow row in dtbTemp.Rows)
                {
                    DateTime appointmentDate = Convert.ToDateTime(row["AppointmentDate"]);
                    string appointmentTime = row["AppointmentTime"].ToString();
                    int visitDuration = Convert.ToInt16(row["VisitDuration"]);
                    int hour = 0;
                    int minute = 0;
                    if (appointmentTime.Contains(":"))
                    {
                        hour = Convert.ToInt16(appointmentTime.Split(':')[0]);
                        minute = Convert.ToInt16(appointmentTime.Split(':')[1]);
                    }

                    DataRow newRow = dtb.NewRow();
                    newRow["AppointmentNo"] = row["AppointmentNo"];
                    newRow["SubjectField"] = row["SubjectField"];
                    newRow["StartDateTime"] = new DateTime(appointmentDate.Year, appointmentDate.Month, appointmentDate.Day, hour, minute, 1);

                    if (minute + visitDuration > 59)
                    {
                        hour++;
                        minute += visitDuration - 60;
                    }
                    else
                        minute += visitDuration;

                    newRow["EndDateTime"] = new DateTime(appointmentDate.Year, appointmentDate.Month, appointmentDate.Day, hour, minute, 0);

                    //Add
                    dtb.Rows.Add(newRow);
                }

                Session["AppointmentPerDate"] = dtb;
                return dtb;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string serviceUnitID = Page.Request.QueryString["suID"];
                PopulateParamedicList(serviceUnitID);
                PopulateVisitTypeList(serviceUnitID);
                string paramedicID = Page.Request.QueryString["pmID"];
                cboParamedicID.SelectedValue = paramedicID;
                DateTime date = Convert.ToDateTime(Page.Request.QueryString["pDate"].ToString());

                schedule.SelectedDate = date;

                PopulateWorkTime(paramedicID, date);

                SetRangeTimeSchedule(paramedicID, date, cboWorkTime.SelectedValue);
                PopulateSchedule(date, true);

                lblInfo.Text = string.Empty;
                pnlInfo.Visible = false;
            }
        }

        private void PopulateSchedule(DateTime date, bool isReload)
        {
            if (isReload)
                Session["AppointmentPerDate"] = null;

            schedule.DataKeyField = "AppointmentNo";
            schedule.DataSubjectField = "SubjectField";
            schedule.DataStartField = "StartDateTime";
            schedule.DataEndField = "EndDateTime";
            schedule.SelectedDate = date;
            schedule.DataSource = Appointments;
            schedule.Rebind();
        }

        private void SetRangeTimeSchedule(string paramedicID, DateTime scheduleDate, string timeNo)
        {
            schedule.DayStartTime = new TimeSpan(8, 0, 0);
            schedule.DayEndTime = new TimeSpan(8, 0, 0);

            ParamedicScheduleDate rec = new ParamedicScheduleDate();

            if (!rec.LoadByPrimaryKey(Request.QueryString["suID"], paramedicID, scheduleDate.Year.ToString(), scheduleDate))
                return;

            OperationalTime opTime = new OperationalTime();

            if (!opTime.LoadByPrimaryKey(rec.OperationalTimeID))
                return;

            string timeStart = "08:00";
            string timeEnd = "08:00";
            switch (timeNo)
            {
                case "1":
                    timeStart = opTime.StartTime1;
                    timeEnd = opTime.EndTime1;
                    break;
                case "2":
                    timeStart = opTime.StartTime2;
                    timeEnd = opTime.EndTime2;
                    break;
                case "3":
                    timeStart = opTime.StartTime3;
                    timeEnd = opTime.EndTime3;
                    break;
                case "4":
                    timeStart = opTime.StartTime4;
                    timeEnd = opTime.EndTime4;
                    break;
                case "5":
                    timeStart = opTime.StartTime5;
                    timeEnd = opTime.EndTime5;
                    break;
            }

            if (!timeStart.Contains(":"))
                return;

            schedule.ShowFullTime = false;
            string[] arr = timeStart.Split(':');
            int hour = Convert.ToInt16(arr[0]);
            int minute = Convert.ToInt16(arr[1]);

            schedule.DayStartTime = new TimeSpan(hour, minute, 0);

            arr = timeEnd.Split(':');
            hour = Convert.ToInt16(arr[0]);
            minute = Convert.ToInt16(arr[1]);
            schedule.DayEndTime = new TimeSpan(hour, minute, 0);
        }

        protected void cboParamedicID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string paramedicID = cboParamedicID.SelectedValue;
            PopulateWorkTime(paramedicID, schedule.SelectedDate.Date);
            SetRangeTimeSchedule(paramedicID, schedule.SelectedDate.Date, cboWorkTime.SelectedValue);
            PopulateSchedule(schedule.SelectedDate.Date, true);
        }

        private void PopulateParamedicList(string serviceUnitID)
        {
            cboParamedicID.Items.Clear();
            if (serviceUnitID != string.Empty)
            {
                ServiceUnitParamedicQuery query = new ServiceUnitParamedicQuery("a");
                ParamedicQuery qPar = new ParamedicQuery("b");
                query.InnerJoin(qPar).On(query.ParamedicID == qPar.ParamedicID);
                query.Where(query.ServiceUnitID == serviceUnitID);
                query.Select(qPar.ParamedicID, qPar.ParamedicName);
                query.OrderBy(qPar.ParamedicName.Ascending);
                DataTable dtb = query.LoadDataTable();

                cboParamedicID.Items.Add(new RadComboBoxItem("", ""));
                foreach (DataRow row in dtb.Rows)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(row["ParamedicName"].ToString(), row["ParamedicID"].ToString()));
                }
            }
        }

        private void PopulateVisitTypeList(string serviceUnitID)
        {
            cboVisitTypeID.Items.Clear();
            if (serviceUnitID != string.Empty)
            {
                ServiceUnitVisitTypeQuery query = new ServiceUnitVisitTypeQuery("a");
                VisitTypeQuery qVisit = new VisitTypeQuery("b");
                query.InnerJoin(qVisit).On(query.VisitTypeID == qVisit.VisitTypeID);
                query.Where(query.ServiceUnitID == serviceUnitID);
                query.Select(qVisit.VisitTypeID, qVisit.VisitTypeName);
                query.OrderBy(qVisit.VisitTypeName.Ascending);
                DataTable dtb = query.LoadDataTable();

                cboVisitTypeID.Items.Add(new RadComboBoxItem("", ""));
                foreach (DataRow row in dtb.Rows)
                {
                    cboVisitTypeID.Items.Add(new RadComboBoxItem(row["VisitTypeName"].ToString(), row["VisitTypeID"].ToString()));
                }
            }
        }

        private void PopulateWorkTime(string paramedicID, DateTime scheduleDate)
        {
            cboWorkTime.Items.Clear();
            ParamedicScheduleDate rec = new ParamedicScheduleDate();
            if (rec.LoadByPrimaryKey(Request.QueryString["suID"], paramedicID, scheduleDate.Year.ToString(), scheduleDate))
            {
                OperationalTime opTime = new OperationalTime();
                if (opTime.LoadByPrimaryKey(rec.OperationalTimeID))
                {
                    string itemFormat = "Time {0} ({1}-{2})";
                    if (!string.IsNullOrEmpty(opTime.StartTime1) && opTime.StartTime1.Contains(":"))
                        cboWorkTime.Items.Add(
                            new RadComboBoxItem(string.Format(itemFormat, 1, opTime.StartTime1, opTime.EndTime1), "1"));
                    if (!string.IsNullOrEmpty(opTime.StartTime2) && opTime.StartTime2.Contains(":"))
                        cboWorkTime.Items.Add(
                            new RadComboBoxItem(string.Format(itemFormat, 2, opTime.StartTime2, opTime.EndTime2), "2"));
                    if (!string.IsNullOrEmpty(opTime.StartTime3) && opTime.StartTime3.Contains(":"))
                        cboWorkTime.Items.Add(
                            new RadComboBoxItem(string.Format(itemFormat, 3, opTime.StartTime3, opTime.EndTime3), "3"));
                    if (!string.IsNullOrEmpty(opTime.StartTime4) && opTime.StartTime4.Contains(":"))
                        cboWorkTime.Items.Add(
                            new RadComboBoxItem(string.Format(itemFormat, 4, opTime.StartTime4, opTime.EndTime4), "4"));
                    if (!string.IsNullOrEmpty(opTime.StartTime5) && opTime.StartTime5.Contains(":"))
                        cboWorkTime.Items.Add(
                            new RadComboBoxItem(string.Format(itemFormat, 5, opTime.StartTime5, opTime.EndTime5), "5"));
                }
            }
        }

        protected void cboVisitTypeID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string suID = Page.Request.QueryString["suID"];
            ServiceUnitVisitTypeQuery query = new ServiceUnitVisitTypeQuery("a");
            query.Select(query.VisitDuration);
            query.Where(query.ServiceUnitID == suID, query.VisitTypeID == cboVisitTypeID.SelectedValue);
            query.es.Top = 1;
            DataTable dtb = query.LoadDataTable();
            txtVisitDuration.Value = dtb.Rows.Count > 0 ? Convert.ToDouble(dtb.Rows[0]["VisitDuration"]) : 0;
        }

        protected void cboWorkTime_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRangeTimeSchedule(cboParamedicID.SelectedValue, schedule.SelectedDate.Date, cboWorkTime.SelectedValue);
        }

        public override bool OnButtonOkClicked()
        {
            Validate();
            if (!IsValid)
                return false;

            if (txtVisitDuration.Value < 1)
            {
                lblInfo.Text = "Visit Duration (Minute) must greater than 0";
                pnlInfo.Visible = true;
                return false;
            }
            else
            {
                lblInfo.Text = "";
                pnlInfo.Visible = false;
            }

            string[] times = txtAppointmentTime.TextWithLiterals.Split(':');
            int year = Convert.ToInt32(times[0]);
            int month = Convert.ToInt32(times[1]);
            Session["AppointmentDateTime"] = new DateTime(schedule.SelectedDate.Year, schedule.SelectedDate.Month, schedule.SelectedDate.Day, year, month, 0);
            Session["ParamedicID"] = cboParamedicID.SelectedValue;
            Session["VisitTypeID"] = cboVisitTypeID.SelectedValue;
            Session["VisitDuration"] = txtVisitDuration.Value;
            Session["Notes"] = txtNotes.Text;
            return true;
        }

        protected void schedule_NavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
        {
            if (e.Command == SchedulerNavigationCommand.NavigateToSelectedDate ||
                e.Command == SchedulerNavigationCommand.NavigateToNextPeriod ||
                e.Command == SchedulerNavigationCommand.NavigateToPreviousPeriod ||
                e.Command == SchedulerNavigationCommand.SwitchToSelectedDay)
            {
                PopulateWorkTime(cboParamedicID.SelectedValue, schedule.SelectedDate.Date);
                PopulateSchedule(schedule.SelectedDate.Date, true);
                SetRangeTimeSchedule(cboParamedicID.SelectedValue, schedule.SelectedDate.Date, cboWorkTime.SelectedValue);
            }
        }

        protected void calSchedule_SelectionChanged(object sender, SelectedDatesEventArgs e)
        {
            schedule.SelectedDate = calSchedule.SelectedDate;
            SetRangeTimeSchedule(cboParamedicID.SelectedValue, schedule.SelectedDate.Date, cboWorkTime.SelectedValue);
            PopulateSchedule(schedule.SelectedDate.Date, true);
        }
    }
}