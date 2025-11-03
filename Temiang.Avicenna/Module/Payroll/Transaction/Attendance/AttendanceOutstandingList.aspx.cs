using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class AttendanceOutstandingList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AttendanceOutstandingList;

            if (!IsPostBack)
            {
                var user = new AppUser();
                user.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                var login = new VwEmployeeTable();
                login.Query.Where(login.Query.PersonID == user.PersonID);
                login.Query.Load();

                var orgs = new OrganizationUnitCollection();
                orgs.Query.es.Distinct = true;
                orgs.Query.Where(orgs.Query.IsActive == true);
                orgs.Query.OrderBy(orgs.Query.OrganizationUnitName.Ascending);
                orgs.Query.Load();

                cboOrganizationUnitID.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var org in orgs)
                {
                    cboOrganizationUnitID.Items.Add(new Telerik.Web.UI.RadComboBoxItem(org.OrganizationUnitName, org.OrganizationUnitID.ToString()));
                }
                cboOrganizationUnitID.SelectedValue = login.ServiceUnitID;

                var periods = new PayrollPeriodCollection();
                periods.Query.OrderBy(periods.Query.SPTYear.Descending, periods.Query.SPTMonth.Ascending);
                periods.Query.Load();

                cboPayrollPeriodID.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var period in periods)
                {
                    cboPayrollPeriodID.Items.Add(new Telerik.Web.UI.RadComboBoxItem(period.PayrollPeriodName, period.PayrollPeriodID.ToString()));
                }

                var payroll = new PayrollPeriod();
                payroll.Query.Where(payroll.Query.SPTMonth == DateTime.Now.Month, payroll.Query.SPTYear == DateTime.Now.Year);
                if (payroll.Query.Load())
                {
                    cboPayrollPeriodID.SelectedValue = payroll.PayrollPeriodID.ToString();
                }

                txtScheduleDate.SelectedDate = DateTime.Now.Date;

                var hours = new WorkingHourCollection();
                hours.Query.Where(hours.Query.IsActive == true);
                hours.Query.OrderBy(hours.Query.WorkingHourName.Ascending);
                hours.Query.Load();

                cboWorkingHour.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var hour in hours)
                {
                    if (hour.SRShift == "ShiftID-013")
                        cboWorkingHour.Items.Add(new Telerik.Web.UI.RadComboBoxItem($"{hour.WorkingHourName} ({hour.StartTime}-{hour.EndTime},{hour.StartTime2}-{hour.EndTime2})", hour.WorkingHourID.ToString()));
                    else
                        cboWorkingHour.Items.Add(new Telerik.Web.UI.RadComboBoxItem($"{hour.WorkingHourName} ({hour.StartTime}-{hour.EndTime})", hour.WorkingHourID.ToString()));
                }

                var emps = new VwEmployeeTableCollection();
                emps.Query.Where(emps.Query.SREmployeeStatus == "1");
                emps.Query.Load();

                cboEmployeeName.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var emp in emps)
                {
                    cboEmployeeName.Items.Add(new Telerik.Web.UI.RadComboBoxItem($"{emp.EmployeeNumber} - {emp.EmployeeName}", emp.PersonID.ToString()));
                }
            }
        }

        private BusinessObject.EmployeeOvertimeItem GetOvertime(DateTime now, int personID)
        {
            var oiq = new EmployeeOvertimeItemQuery("b");
            var oq = new EmployeeOvertimeQuery("a");

            oiq.es.Top = 1;
            oiq.InnerJoin(oq).On(oq.TransactionNo == oiq.TransactionNo && oq.TransactionDate.Date() == now.Date && oq.IsApproved == true && oq.IsVerified == true);
            oiq.Where(oiq.PersonID == personID);
            oiq.OrderBy(oiq.LastUpdateDateTime.Descending);

            var overtimeItem = new EmployeeOvertimeItem();
            return overtimeItem.Load(oiq) ? overtimeItem : null;
        }

        private EmployeeLeaveRequest ValidateLeave(DateTime date, int personID)
        {
            var leave = new EmployeeLeaveRequest();
            leave.Query.es.Top = 1;
            leave.Query.Where(leave.Query.PersonID == personID, leave.Query.IsRequestApproved == true, leave.Query.IsVerified == true);
            leave.Query.Where($"<{date.ToString("yyyyMMdd")} BETWEEN CONVERT(varchar(max), ApprovedLeaveDateFrom, 112) AND CONVERT(varchar(max), ApprovedLeaveDateTo, 112)>");
            leave.Query.OrderBy(leave.Query.LastUpdateDateTime.Descending);
            return leave.Query.Load() ? leave : null;
        }

        private IEnumerable<DataSource> OutstandingList(int payrollPeriodID, int organizationUnitID, string personID, DateTime? now, string type)
        {
            var ws = new WorkingScheduleQuery("a");
            var wsd = new WorkingScheduleDetailQuery("b");
            var vet = new PersonalInfoQuery("c");
            var org = new OrganizationUnitQuery("e");
            var pp = new PayrollPeriodQuery("f");
            var mad = new MonthlyAttendanceDetailQuery("g");

            ws.es.Distinct = true;
            ws.Select(
                wsd.PersonID,
                vet.EmployeeNumber,
                vet.EmployeeName,
                $"<b.WorkingHourIDDay{now?.Day} AS WorkingHourID>",
                "<'' AS WorkingHourName>",
                ws.WorkingScheduleID,
                ws.PayrollPeriodID,
                org.OrganizationUnitName,
                pp.PayrollPeriodName,
                //mad.ScheduleInDate,
                $"<ISNULL(g.ScheduleInDate, CAST('{txtScheduleDate.SelectedDate?.ToString("yyyy-MM-dd")}' AS DATETIME)) AS ScheduleInDate>",
                mad.ScheduleInTime,
                mad.CheckInDate,
                mad.CheckInTime,
                mad.ScheduleOutDate,
                mad.ScheduleOutTime,
                mad.CheckOutDate,
                mad.CheckOutTime,
                $"<{now?.Day} AS Day>", 
                wsd.LastUpdateDateTime
                );
            ws.InnerJoin(wsd).On(ws.WorkingScheduleID == wsd.WorkingScheduleID);
            ws.InnerJoin(org).On(ws.OrganizationUnitID == org.OrganizationUnitID);
            ws.InnerJoin(pp).On(ws.PayrollPeriodID == pp.PayrollPeriodID);
            ws.InnerJoin(vet).On(wsd.PersonID == vet.PersonID);

            if (new string[] { string.Empty, "1" }.Contains(cboFilterType.SelectedValue))
                ws.LeftJoin(mad).On(mad.PayrollPeriodID == ws.PayrollPeriodID && mad.PersonID == wsd.PersonID && mad.ScheduleInDate.Date() == now?.Date);
            else if (new string[] { "2", "3" }.Contains(cboFilterType.SelectedValue))
                ws.InnerJoin(mad).On(mad.PayrollPeriodID == ws.PayrollPeriodID && mad.PersonID == wsd.PersonID && mad.ScheduleInDate.Date() == now?.Date);

            ws.Where(ws.PayrollPeriodID == payrollPeriodID, ws.IsApproved == true);
            //ws.Where($"<b.WorkingHourIDDay{now?.Day} IS NOT NULL>");

            if (organizationUnitID >= 0)
                ws.Where(ws.OrganizationUnitID == organizationUnitID);
            if (!string.IsNullOrWhiteSpace(personID))
                ws.Where(wsd.PersonID == personID.ToInt());
            if (cboFilterType.SelectedValue == "1")
                ws.Where(mad.ScheduleInDate.IsNull());
            else if (cboFilterType.SelectedValue == "2")
                ws.Where(mad.CheckInDate.IsNotNull(), mad.CheckInTime.Coalesce("''") != string.Empty, mad.Or(mad.CheckOutDate.IsNull(), mad.CheckOutTime.Coalesce("''") == string.Empty));
            else if (cboFilterType.SelectedValue == "3")
                ws.Where(mad.CheckInDate.IsNotNull(), mad.CheckInTime.Coalesce("''") != string.Empty, mad.CheckOutDate.IsNotNull(), mad.CheckOutTime.Coalesce("''") != string.Empty);

            ws.OrderBy(org.OrganizationUnitName.Ascending, vet.EmployeeNumber.Ascending);

            var table = ws.LoadDataTable();

            var temp = table.Clone();
            temp.Rows.Clear();

            var listDataRow = new List<DataRow>();
            var reload = false;

            foreach (DataRow row in table.Rows)
            {
                var wsi = new WorkingSchduleInterventionQuery("a");
                var wsid = new WorkingSchduleInterventionDetailQuery("b");

                wsi.es.Top = 1;
                wsi.Select($"<b.WorkingHourIDDay{now?.Day} AS WorkingHourID>");
                wsi.InnerJoin(wsid).On(wsi.WorkingSchduleInterventionID == wsid.WorkingSchduleInterventionID);
                wsi.Where(wsi.IsApproved == true, wsid.PersonID == row["PersonID"].ToInt(), wsi.WorkingScheduleID == row["WorkingScheduleID"].ToInt()); 
                wsi.Where($"<b.WorkingHourIDDay{now?.Day} IS NOT NULL>");
                wsi.OrderBy(wsid.LastUpdateDateTime.Descending);

                var intervention = wsi.LoadDataTable();

                var wh = new WorkingHour();
                if (intervention.Rows.Count > 0)
                {
                    wh.LoadByPrimaryKey(intervention.AsEnumerable().First()["WorkingHourID"].ToInt());
                    row["WorkingHourID"] = intervention.AsEnumerable().First()["WorkingHourID"].ToInt();
                    if (wh.SRShift == "ShiftID-013") row["WorkingHourName"] = $"{wh.WorkingHourName}** ({wh.StartTime}-{wh.EndTime},{wh.StartTime2}-{wh.EndTime2})";
                    else row["WorkingHourName"] = $"{wh.WorkingHourName}** ({wh.StartTime}-{wh.EndTime})";

                    if (row["ScheduleInTime"] == DBNull.Value) row["ScheduleInTime"] = wh.StartTime;

                    if (row["ScheduleOutDate"] == DBNull.Value) row["ScheduleOutDate"] = row["ScheduleInDate"];
                    if (row["ScheduleOutTime"] == DBNull.Value) row["ScheduleOutTime"] = wh.EndTime;
                }
                else
                {
                    wsi = new WorkingSchduleInterventionQuery("a");
                    wsid = new WorkingSchduleInterventionDetailQuery("b");
                    ws = new WorkingScheduleQuery("c");

                    wsi.es.Top = 1;
                    wsi.Select($"<b.WorkingHourIDDay{now?.Day} AS WorkingHourID>");
                    wsi.InnerJoin(wsid).On(wsi.WorkingSchduleInterventionID == wsid.WorkingSchduleInterventionID);
                    wsi.InnerJoin(ws).On(wsi.WorkingScheduleID == ws.WorkingScheduleID && ws.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt());
                    wsi.Where(wsi.IsApproved == true, wsid.PersonID == row["PersonID"].ToInt(), wsi.WorkingScheduleID == row["WorkingScheduleID"].ToInt());
                    wsi.Where($"<b.WorkingHourIDDay{now?.Day} IS NOT NULL>");
                    wsi.OrderBy(wsid.LastUpdateDateTime.Descending);

                    intervention = wsi.LoadDataTable();

                    wh = new WorkingHour();
                    if (intervention.Rows.Count > 0)
                    {
                        wh.LoadByPrimaryKey(intervention.AsEnumerable().First()["WorkingHourID"].ToInt());
                        row["WorkingHourID"] = intervention.AsEnumerable().First()["WorkingHourID"].ToInt();
                        if (wh.SRShift == "ShiftID-013") row["WorkingHourName"] = $"{wh.WorkingHourName}** ({wh.StartTime}-{wh.EndTime},{wh.StartTime2}-{wh.EndTime2})";
                        else row["WorkingHourName"] = $"{wh.WorkingHourName}** ({wh.StartTime}-{wh.EndTime})";

                        row["ScheduleInTime"] = wh.StartTime;
                        row["ScheduleOutTime"] = wh.EndTime;
                    }
                    else
                    {
                        wh.LoadByPrimaryKey(row["WorkingHourID"].ToInt());
                        if (wh.SRShift == "ShiftID-013") row["WorkingHourName"] = $"{wh.WorkingHourName} ({wh.StartTime}-{wh.EndTime},{wh.StartTime2}-{wh.EndTime2})";
                        else row["WorkingHourName"] = $"{wh.WorkingHourName} ({wh.StartTime}-{wh.EndTime})";
                    }
                }

                // lembur
                if (wh.IsOffDay ?? false)
                {
                    // cek lembur di hari libur
                    var overtime = GetOvertime(txtScheduleDate.SelectedDate.Value.Date, row["PersonID"].ToInt());
                    if (overtime != null)
                    {
                        wh = new WorkingHour();
                        if (wh.LoadByPrimaryKey(overtime.WorkingHourID ?? -1))
                        {
                            //if (wh.SRShift == "ShiftID-013")
                            //{
                            //    row["WorkingHourName"] = $"{wh.WorkingHourName} ({wh.StartTime}-{wh.EndTime},{wh.StartTime2}-{wh.EndTime2})";
                            //}
                            //else
                            //{
                            row["WorkingHourName"] = $"{wh.WorkingHourName} ({wh.StartTime}-{wh.EndTime})";
                            //}
                            row["ScheduleInTime"] = wh.StartTime;
                            row["ScheduleOutTime"] = wh.EndTime;
                        }
                    }
                }
                else
                // lembur di hari kerja
                {
                    var overtime = GetOvertime(txtScheduleDate.SelectedDate.Value.Date, row["PersonID"].ToInt());
                    if (overtime != null)
                    {
                        var schCheckOut = txtScheduleDate.SelectedDate?.Date.Add(TimeSpan.ParseExact(wh.EndTime, "hh\\:mm", null)).AddHours(Convert.ToDouble(overtime.Amount ?? 0));

                        wh = new WorkingHour();
                        if (wh.LoadByPrimaryKey(overtime.WorkingHourID ?? -1))
                        {
                            //if (wh.SRShift == "ShiftID-013") row["WorkingHourName"] = $"{wh.WorkingHourName}** ({wh.StartTime}-{wh.EndTime},{wh.StartTime2}-{wh.EndTime2})";
                            //else 
                            row["WorkingHourName"] = $"{wh.WorkingHourName}** ({wh.StartTime}-{schCheckOut?.ToString("HH:mm")})";
                            row["ScheduleOutTime"] = schCheckOut;
                        }
                    }
                }

                // ijin


                // cuti
                var leaveRequest = ValidateLeave(txtScheduleDate.SelectedDate.Value.Date, row["PersonID"].ToInt());
                if (leaveRequest != null)
                {
                    var leave = new EmployeeLeave();
                    leave.LoadByPrimaryKey(leaveRequest.EmployeeLeaveID ?? -1);

                    var std = new AppStandardReferenceItem();
                    std.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeLeaveType.ToString(), leave.SREmployeeLeaveType);

                    row["WorkingHourName"] = std.ItemName;
                }

                row["WorkingScheduleID"] = 0; //kosongkan biar bs di distinct

                row.AcceptChanges();

                if (new string[] { /*"ShiftID-023", */"ShiftID-013", "ShiftID-003" }.Contains(wh.SRShift) && (wh.IsCrossDay ?? false))
                {

                    DataRow nr = temp.NewRow();
                    nr["PersonID"] = row["PersonID"];
                    nr["EmployeeNumber"] = row["EmployeeNumber"];
                    nr["EmployeeName"] = row["EmployeeName"];
                    nr["WorkingHourID"] = row["WorkingHourID"];
                    nr["WorkingHourName"] = row["WorkingHourName"];
                    nr["WorkingScheduleID"] = row["WorkingScheduleID"];
                    nr["PayrollPeriodID"] = row["PayrollPeriodID"];
                    nr["OrganizationUnitName"] = row["OrganizationUnitName"];
                    nr["PayrollPeriodName"] = row["PayrollPeriodName"];
                    nr["ScheduleInDate"] = row["ScheduleInDate"];
                    nr["ScheduleInTime"] = wh.StartTime2;
                    nr["CheckInDate"] = DBNull.Value;
                    nr["CheckInTime"] = string.Empty;
                    nr["ScheduleOutDate"] = row["ScheduleInDate"] == DBNull.Value || string.IsNullOrEmpty(row["ScheduleInDate"].ToString()) ? txtScheduleDate.SelectedDate?.AddDays(1) : Convert.ToDateTime(row["ScheduleInDate"]).AddDays(1);
                    nr["ScheduleOutTime"] = wh.EndTime2;
                    nr["CheckOutDate"] = DBNull.Value;
                    nr["CheckOutTime"] = string.Empty;
                    nr["Day"] = row["Day"];
                    nr["LastUpdateDateTime"] = row["LastUpdateDateTime"];
                    temp.Rows.Add(nr);

                    //var madc = new MonthlyAttendanceDetail();
                    //madc.Query.Where(madc.Query.PersonID == row["PersonID"].ToInt(), madc.Query.ScheduleInDate.Date() == Convert.ToDateTime(nr["ScheduleInDate"]).Date, madc.Query.ScheduleInTime == wh.StartTime2);
                    //if (!madc.Query.Load())
                    //{
                    //    reload = true;

                    //    madc = new MonthlyAttendanceDetail();
                    //    madc.PersonID = row["PersonID"].ToInt();
                    //    madc.PayrollPeriodID = payrollPeriodID;
                    //    madc.ScheduleInDate = Convert.ToDateTime(nr["ScheduleInDate"]).Date;
                    //    madc.ScheduleInTime = wh.StartTime2;
                    //    madc.CheckInDate = null;
                    //    madc.CheckInTime = string.Empty;
                    //    madc.LateMinutes = null;
                    //    madc.ScheduleOutDate = Convert.ToDateTime(nr["ScheduleOutDate"]).Date;
                    //    madc.ScheduleOutTime = wh.EndTime2;
                    //    madc.IsOvertime = wh.IsOvertimeWorkingHour ?? false;
                    //    //attendanceDetail.OvertimeHours = Convert.ToInt16(hour.OvertimeValueInMinutes ?? 0);
                    //    madc.WorkingHourID = wh.WorkingHourID;
                    //    madc.Save();

                    //}
                }
            }
            table.AcceptChanges();

            if (reload) return OutstandingList(payrollPeriodID, organizationUnitID, personID, now, type);

            if (temp.Rows.Count > 0)
            {
                foreach (DataRow row in temp.Rows)
                {
                    table.ImportRow(row);
                }
                table.AcceptChanges();
            }

            var logs = new WebServiceAPILogCollection();
            logs.Query.Where(logs.Query.DateRequest.Date() == txtScheduleDate.SelectedDate?.Date, logs.Query.IPAddress == string.Empty, logs.Query.UrlAddress == string.Empty, logs.Query.Response == string.Empty);
            logs.Query.es.WithNoLock = true;
            logs.Query.Load();

            var list = new List<DataSource>();

            if (cboWorkingHour.CheckedItems.Any() && table.Rows.Count > 0) table = table.AsEnumerable().Where(t => cboWorkingHour.CheckedItems.Select(c => c.Value.ToInt()).Contains(t.Field<int>("WorkingHourID"))).CopyToDataTable();

            foreach (DataRow row in table.Rows)
            {
                if (row["ScheduleInDate"] == DBNull.Value)
                {
                    if (list.Any(l => l.PersonID == Convert.ToInt32(row["PersonID"]))) continue;
                }
                else
                    if (list.Any(l => l.PersonID == Convert.ToInt32(row["PersonID"]) && l.ScheduleInDate == Convert.ToDateTime(row["ScheduleInDate"]) && l.ScheduleInTime == row["ScheduleInTime"].ToString())) continue;
                list.Add(new DataSource
                {
                    PersonID = Convert.ToInt32(row["PersonID"]),
                    PayrollPeriodName = row["PayrollPeriodName"].ToString(),
                    EmployeeNumber = row["EmployeeNumber"].ToString(),
                    EmployeeName = row["EmployeeName"].ToString(),
                    OrganizationUnitName = row["OrganizationUnitName"].ToString(),
                    WorkingHourName = row["WorkingHourName"].ToString(),
                    ScheduleInDate = row["ScheduleInDate"] as DateTime?,
                    ScheduleInTime = row["ScheduleInTime"].ToString(),
                    CheckInDate = row["CheckInDate"] as DateTime?,
                    CheckInTime = row["CheckInTime"].ToString(),
                    ScheduleOutDate = row["ScheduleOutDate"] as DateTime?,
                    ScheduleOutTime = row["ScheduleOutTime"].ToString(),
                    CheckOutDate = row["CheckOutDate"] as DateTime?,
                    CheckOutTime = row["CheckOutTime"].ToString(),
                    TapCount = logs.Count(l => JsonConvert.DeserializeObject<TapHistory>(l.Params).employeeNo == row["EmployeeNumber"].ToString()),
                    PayrollPeriodID = Convert.ToInt32(row["PayrollPeriodID"]),
                    Day = Convert.ToInt32(row["Day"]),
                });
            }

            if (list.Any()) return list.OrderBy(l => l.EmployeeName);
            return list;
        }

        protected void grdList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                var pp = string.IsNullOrWhiteSpace(cboPayrollPeriodID.SelectedValue) ? -1 : cboPayrollPeriodID.SelectedValue.ToInt();
                var org = string.IsNullOrWhiteSpace(cboOrganizationUnitID.SelectedValue) ? -1 : cboOrganizationUnitID.SelectedValue.ToInt();

                grdList.DataSource = OutstandingList(pp, org, cboEmployeeName.SelectedValue, txtScheduleDate.SelectedDate, cboFilterType.SelectedValue);
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            grdList.MasterTableView.ExportToExcel();
        }

        protected void grdList_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            var log = new WebServiceAPILogCollection();
            log.Query.Where(log.Query.DateRequest.Date() == txtScheduleDate.SelectedDate?.Date, log.Query.IPAddress == string.Empty, log.Query.UrlAddress == string.Empty, log.Query.Response == e.DetailTableView.ParentItem.GetDataKeyValue("EmployeeNumber"));
            //log.Query.Where($"<Params LIKE '%{e.DetailTableView.ParentItem.GetDataKeyValue("EmployeeNumber")}%'>");
            //log.Query.Where(log.Query.Response == e.DetailTableView.ParentItem.GetDataKeyValue("EmployeeNumber"));
            log.Query.OrderBy(log.Query.DateRequest.Ascending);
            if (!log.Query.Load()) e.DetailTableView.DataSource = new List<TapHistory>();

            e.DetailTableView.DataSource = log.Select(l => new DataSourceHistory()
            {
                employeeNo = JsonConvert.DeserializeObject<TapHistory>(l.Params).employeeNo,
                dateTimeKey = JsonConvert.DeserializeObject<TapHistory>(l.Params).dateTime,
                dateTime = DateTime.ParseExact(JsonConvert.DeserializeObject<TapHistory>(l.Params).dateTime, "yyyyMMdd-HHmm", null, System.Globalization.DateTimeStyles.None),
                response = l.Response
            });
        }

        protected void grdList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var item = e.Item;
            if (e.CommandName == "Reload")
            {
                var employeeNo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["employeeNo"].ToString();
                var dateTime = item.OwnerTableView.DataKeyValues[item.ItemIndex]["dateTimeKey"].ToString();

                var ws = new WebService.Attendance();
                var response = JsonConvert.DeserializeObject<ReloadResponse>(ws.ValidateAttendance(employeeNo, dateTime));

                ScriptManager.RegisterStartupScript(this, GetType(), "message", string.Format("alert('{0}');", response.message), true);

                if (response.status) grdList.Rebind();
            }
            else if (e.CommandName == "Reset")
            {
                var period = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PayrollPeriodID"]);
                var person = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PersonID"]);

                using (var trans = new esTransactionScope())
                {
                    var history = new MonthlyAttendanceDetailHistoryCollection();
                    history.Query.Where(history.Query.PersonID == person, history.Query.CheckInDateTime.Date() == txtScheduleDate.SelectedDate?.Date);
                    if (history.Query.Load())
                    {
                        history.MarkAllAsDeleted();
                        history.Save();
                    }

                    var attendance = new MonthlyAttendanceDetailCollection();
                    attendance.Query.Where(attendance.Query.PayrollPeriodID == period, attendance.Query.PersonID == person, attendance.Query.ScheduleInDate.Date() == txtScheduleDate.SelectedDate?.Date);
                    if (attendance.Query.Load())
                    {
                        attendance.MarkAllAsDeleted();
                        attendance.Save();
                    }

                    trans.Complete();
                }

                grdList.Rebind();
            }
        }

        protected void btnExportOutstandingSchdedule_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboPayrollPeriodID.SelectedValue)) return;

            var wsd = new WorkingScheduleDetailQuery("a");
            var ws = new WorkingScheduleQuery("b");

            wsd.Select(wsd.PersonID);
            wsd.InnerJoin(ws).On(wsd.WorkingScheduleID == ws.WorkingScheduleID && ws.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt());

            var vw = new VwEmployeeTableQuery();
            vw.Select(vw.EmployeeNumber, vw.EmployeeName);
            vw.Where(vw.SREmployeeStatus == "1");
            vw.Where(vw.PersonID.NotIn(wsd));
            vw.OrderBy(vw.EmployeeNumber.Ascending);
            var table = vw.LoadDataTable();

            CreateExcelFile.CreateExcelDocument(table, $"OutsandingScheduleReport-{cboPayrollPeriodID.Text}.xlsx", this.Response);
        }

        protected void btnExportAttendanceCard_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboPayrollPeriodID.SelectedValue)) return;

            var period = new PayrollPeriod();
            period.LoadByPrimaryKey(cboPayrollPeriodID.SelectedValue.ToInt());

            var maxDay = DateTime.DaysInMonth(period.SPTYear ?? -1, period.SPTMonth ?? -1);

            bool filter = false;

            var emps = new VwEmployeeTableCollection();
            if (!string.IsNullOrWhiteSpace(cboOrganizationUnitID.SelectedValue))
            {
                emps.Query.Where(emps.Query.ServiceUnitID == cboOrganizationUnitID.SelectedValue.ToInt());
                filter = true;
            }
            if (!string.IsNullOrWhiteSpace(cboEmployeeName.SelectedValue))
            {
                emps.Query.Where(emps.Query.PersonID == cboEmployeeName.SelectedValue.ToInt());
                filter = true;
            }
            emps.Query.Where(emps.Query.SREmployeeStatus == "1");

            if (!filter) return;

            emps.Query.Load();

            var ds = new DataSet();

            foreach (var emp in emps)
            {
                var table = new DataTable(emp.EmployeeName);
                table.Columns.Add(new DataColumn("ScheduleType", typeof(bool)));
                table.Columns.Add(new DataColumn("WorkingHourID", typeof(int)));
                table.Columns.Add(new DataColumn("Date", typeof(DateTime)));
                table.Columns.Add(new DataColumn("WorkingHourName", typeof(string)));
                table.Columns.Add(new DataColumn("CheckInDate", typeof(DateTime)));
                table.Columns.Add(new DataColumn("CheckInTime", typeof(string)));
                table.Columns.Add(new DataColumn("CheckOutDate", typeof(DateTime)));
                table.Columns.Add(new DataColumn("CheckOutTime", typeof(string)));

                for (int i = 1; i <= maxDay; i++)
                {
                    DataRow row = table.NewRow();
                    row["Date"] = new DateTime(period.SPTYear ?? -1, period.SPTMonth ?? -1, i);

                    var schd = new WorkingScheduleDetailQuery("a");
                    var sch = new WorkingScheduleQuery("b");

                    schd.es.Top = 1;
                    schd.Select(schd.WorkingScheduleID);
                    schd.Select($"<ISNULL(a.WorkingHourIDDay{i}, -1) AS WorkingHourID>");
                    schd.InnerJoin(sch).On(schd.WorkingScheduleID == sch.WorkingScheduleID && sch.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt() && sch.IsApproved == true);
                    schd.Where(schd.PersonID == emp.PersonID);
                    schd.OrderBy(schd.LastUpdateDateTime.Descending);
                    var dtb = schd.LoadDataTable();
                    if (dtb.Rows.Count > 0 && dtb.Rows[0][1].ToInt() > -1)
                    {
                        var wh = new WorkingHour();

                        var wsi = new WorkingSchduleInterventionQuery("a");
                        var wsid = new WorkingSchduleInterventionDetailQuery("b");

                        wsi.es.Top = 1;
                        wsi.Select($"<b.WorkingHourIDDay{i} AS WorkingHourID>");
                        wsi.InnerJoin(wsid).On(wsi.WorkingSchduleInterventionID == wsid.WorkingSchduleInterventionID);
                        wsi.Where(wsi.WorkingScheduleID == dtb.Rows[0][0].ToInt(), wsi.IsApproved == true, wsid.PersonID == emp.PersonID); //wsi.WorkingScheduleID == row["WorkingScheduleID"].ToInt(), 
                        wsi.Where($"<b.WorkingHourIDDay{i} IS NOT NULL>");
                        wsi.OrderBy(wsid.LastUpdateDateTime.Descending);
                        var intervention = wsi.LoadDataTable();

                        if (intervention.Rows.Count > 0) wh.LoadByPrimaryKey(intervention.Rows[0][0].ToInt());
                        else wh.LoadByPrimaryKey(dtb.Rows[0][1].ToInt());
                        row["WorkingHourID"] = wh.WorkingHourID;
                        row["WorkingHourName"] = wh.WorkingHourName;

                        var leaveRequest = ValidateLeave(Convert.ToDateTime(row["Date"]).Date, emp.PersonID ?? -1);
                        if (leaveRequest != null)
                        {
                            var leave = new EmployeeLeave();
                            leave.LoadByPrimaryKey(leaveRequest.EmployeeLeaveID ?? -1);

                            var std = new AppStandardReferenceItem();
                            std.LoadByPrimaryKey(AppEnum.StandardReference.EmployeeLeaveType.ToString(), leave.SREmployeeLeaveType);

                            row["ScheduleType"] = false;
                            row["WorkingHourName"] = std.ItemName;
                        }
                        else row["ScheduleType"] = true;
                    }
                    else row["ScheduleType"] = false;

                    //row["CheckIn"] = new DateTime(period.SPTYear ?? -1, period.SPTMonth ?? -1, i, 0, 0, 0);
                    //row["CheckOut"] = new DateTime(period.SPTYear ?? -1, period.SPTMonth ?? -1, i, 0, 0, 0);

                    table.Rows.Add(row);
                }

                table.AcceptChanges();

                var list = new List<AttendanceHistory>();

                var madc = new MonthlyAttendanceDetailCollection();
                madc.Query.Where(madc.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt(), madc.Query.PersonID == emp.PersonID);
                madc.Query.OrderBy(madc.Query.LastUpdateDateTime.Ascending);
                if (madc.Query.Load())
                {
                    foreach (DataRow row in table.Rows)
                    {
                        if (!Convert.ToBoolean(row["ScheduleType"])) continue;

                        var wh = new WorkingHour();
                        wh.LoadByPrimaryKey(row["WorkingHourID"].ToInt());

                        if (wh.SRShift != "ShiftID-013")
                        {
                            var mad = madc.LastOrDefault(m => m.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt() && m.PersonID == emp.PersonID && m.ScheduleInDate == Convert.ToDateTime(row["Date"]) && m.WorkingHourID == row["WorkingHourID"].ToInt());
                            if (mad != null)
                            {
                                row["CheckInDate"] = mad.CheckInDate;
                                row["CheckInTime"] = mad.CheckInTime;
                                if (mad.CheckOutDate != null && !string.IsNullOrWhiteSpace(mad.CheckOutTime))
                                {
                                    row["CheckOutDate"] = mad.CheckOutDate;
                                    row["CheckOutTime"] = mad.CheckOutTime;
                                }
                            }
                        }
                        else
                        {
                            var mad = madc.Where(m => m.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt() && m.PersonID == emp.PersonID && m.ScheduleInDate == Convert.ToDateTime(row["Date"]) && m.WorkingHourID == row["WorkingHourID"].ToInt());
                            if (mad.Any())
                            {
                                var index = 0;
                                foreach (var attendance in mad)
                                {
                                    if (index == 0)
                                    {
                                        row["CheckInDate"] = attendance.CheckInDate;
                                        row["CheckInTime"] = attendance.CheckInTime;
                                        if (attendance.CheckOutDate != null && !string.IsNullOrWhiteSpace(attendance.CheckOutTime))
                                        {
                                            row["CheckOutDate"] = attendance.CheckOutDate;
                                            row["CheckOutTime"] = attendance.CheckOutTime;
                                        }
                                    }
                                    else
                                    {
                                        list.Add(new AttendanceHistory()
                                        {
                                            ScheduleType = Convert.ToBoolean(row["ScheduleType"]),
                                            WorkingHourID = row["WorkingHourID"].ToInt(),
                                            Date = Convert.ToDateTime(row["Date"]),
                                            WorkingHourName = row["WorkingHourName"].ToString(),
                                            CheckInDate = Convert.ToDateTime(attendance.CheckInDate),
                                            CheckInTime = attendance.CheckInTime,
                                            CheckOutDate = (attendance.CheckOutDate != null) ? Convert.ToDateTime(attendance.CheckOutDate) : new DateTime(),
                                            CheckOutTime = !string.IsNullOrWhiteSpace(attendance.CheckOutTime) ? attendance.CheckOutTime : string.Empty
                                        });
                                    }
                                    index++;
                                }
                            }

                        }
                    }
                }

                foreach (var item in list)
                {
                    var row = table.NewRow();
                    row["ScheduleType"] = item.ScheduleType;
                    row["WorkingHourID"] = item.WorkingHourID;
                    row["Date"] = item.Date;
                    row["WorkingHourName"] = item.WorkingHourName;
                    row["CheckInDate"] = item.CheckInDate;
                    row["CheckInTime"] = item.CheckInTime;
                    row["CheckOutDate"] = item.CheckOutDate;
                    row["CheckOutTime"] = item.CheckOutTime;
                    table.Rows.Add(row);
                }

                table.AcceptChanges();

                table.Columns.Remove("ScheduleType");
                table.Columns.Remove("WorkingHourID");
                table.AcceptChanges();

                table = table.AsEnumerable().OrderBy(t => t.Field<DateTime>("Date")).CopyToDataTable();
                table.TableName = emp.EmployeeName;

                ds.Tables.Add(table);
            }

            if (ds.Tables.Count > 1)
                CreateExcelFile.CreateExcelDocument(ds, $"AttendanceReport-{cboOrganizationUnitID.Text}-{cboPayrollPeriodID.Text}.xlsx", this.Response);
            else CreateExcelFile.CreateExcelDocument(ds.Tables[0], $"AttendanceReport-{cboOrganizationUnitID.Text}-{ds.Tables[0].TableName}-{cboPayrollPeriodID.Text}.xlsx", this.Response);
        }

        protected void btnExportLateness_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboPayrollPeriodID.SelectedValue)) return;
            if (string.IsNullOrWhiteSpace(cboOrganizationUnitID.SelectedValue)) return;

            var madc = new MonthlyAttendanceDetailQuery("a");
            var vet = new VwEmployeeTableQuery("b");

            madc.Select(madc.CheckInDate, vet.EmployeeNumber, vet.EmployeeName, madc.LateMinutes);
            madc.InnerJoin(vet).On(madc.PersonID == vet.PersonID);
            madc.Where(madc.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt(), madc.LateMinutes > 0, vet.ServiceUnitID == cboOrganizationUnitID.SelectedValue.ToInt());
            madc.OrderBy(madc.CheckInDate.Ascending);

            var table = madc.LoadDataTable();

            CreateExcelFile.CreateExcelDocument(table, $"LatenessAttendanceReport-{cboOrganizationUnitID.Text}-{cboPayrollPeriodID.Text}.xlsx", this.Response);
        }

        protected void btnExportNightShift_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboPayrollPeriodID.SelectedValue)) return;

            var ws = new WorkingScheduleQuery("a");
            var wsd = new WorkingScheduleDetailQuery("b");
            var org = new OrganizationUnitQuery("c");

            ws.Select(ws.OrganizationUnitID, org.OrganizationUnitName, wsd);
            ws.InnerJoin(wsd).On(ws.WorkingScheduleID == wsd.WorkingScheduleID);
            ws.InnerJoin(org).On(ws.OrganizationUnitID == org.OrganizationUnitID);
            ws.Where(ws.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt(), ws.IsApproved == true);
            ws.OrderBy(ws.OrganizationUnitID.Ascending, wsd.PersonID.Ascending, wsd.LastUpdateDateTime.Descending);

            var table = ws.LoadDataTable();

            foreach (var row in table.AsEnumerable().GroupBy(t => new
            {
                OrganizationUnitID = t.Field<int>("OrganizationUnitID"),
                //OrganizationUnitName = t.Field<string>("OrganizationUnitName"),
                PersonID = t.Field<int>("PersonID")
            }))
            {

            }

        }
    }

    public class AttendanceHistory
    {
        public bool ScheduleType { get; set; }
        public int WorkingHourID { get; set; }
        public DateTime Date { get; set; }
        public string WorkingHourName { get; set; }
        public DateTime CheckInDate { get; set; }
        public string CheckInTime { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string CheckOutTime { get; set; }
    }

    public class DataSourceHistory
    {
        public string employeeNo { get; set; }
        public string dateTimeKey { get; set; }
        public DateTime dateTime { get; set; }
        public string response { get; set; }
    }

    public class DataSourceTapHistory
    {
        public string employeeNo { get; set; }
        public string dateTime { get; set; }
    }

    public class TapHistory
    {
        public string employeeNo { get; set; }
        public string dateTime { get; set; }
        public string response { get; set; }
    }

    public class ReloadResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string detail { get; set; }
    }

    public class DataSource
    {
        public int Day { get; set; }
        public int PayrollPeriodID { get; set; }
        public int PersonID { get; set; }
        public string PayrollPeriodName { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string OrganizationUnitName { get; set; }
        public string WorkingHourName { get; set; }
        public DateTime? ScheduleInDate { get; set; }
        public string ScheduleInTime { get; set; }
        public DateTime? CheckInDate { get; set; }
        public string CheckInTime { get; set; }
        public DateTime? ScheduleOutDate { get; set; }
        public string ScheduleOutTime { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string CheckOutTime { get; set; }
        public int TapCount { get; set; }
    }
}