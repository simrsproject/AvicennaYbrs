using DevExpress.XtraEditors.Filtering.Templates.DateTimeRange;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for Attendance
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class Attendance : System.Web.Services.WebService
    {
        bool _cutOffDateAvailable = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["AttendanceCutOff"]) && DateTime.TryParseExact(ConfigurationManager.AppSettings["AttendanceCutOff"], "yyyy-MM-dd", null, DateTimeStyles.None, out _);

        private int GetWorkingHourID(int day, WorkingScheduleDetail workingScheduleDetail)
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
            else return -1;
        }

        private int GetWorkingHourID(int day, WorkingSchduleInterventionDetail workingScheduleDetail)
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
            else return -1;
        }

        private WorkingHour GetWorkingHour(int personID, int payrollPeriodID, DateTime now, bool isCheckIn)
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
            if (!wsd.Load(wsdq)) //return null; // tidak punya jadwal
            {
                wsdq = new BusinessObject.WorkingScheduleDetailQuery("a");
                wsq = new WorkingScheduleQuery("b");

                wsdq.es.Top = 1;
                wsdq.InnerJoin(wsq).On(wsdq.WorkingScheduleID == wsq.WorkingScheduleID && wsq.PayrollPeriodID == payrollPeriodID && wsq.IsApproved == true);
                wsdq.Where(wsdq.PersonID == personID);
                //wsdq.Where($"<a.WorkingHourIDDay{now.Day} IS NOT NULL>");
                wsdq.OrderBy(wsdq.LastUpdateDateTime.Descending);

                wsd = new BusinessObject.WorkingScheduleDetail();
                if (!wsd.Load(wsdq)) return null; // tidak punya jadwal
            }

            var wsidq = new WorkingSchduleInterventionDetailQuery("a");
            var wsiq = new WorkingSchduleInterventionQuery("b");

            wsidq.es.Top = 1;
            wsidq.InnerJoin(wsiq).On(wsidq.WorkingSchduleInterventionID == wsiq.WorkingSchduleInterventionID && wsiq.WorkingScheduleID == wsd.WorkingScheduleID && wsiq.IsApproved == true);
            wsidq.Where(wsidq.PersonID == personID);//, wsidq.LastUpdateDateTime >= now, wsidq.LastUpdateDateTime < now.AddMonths(1));
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
                    var id = GetWorkingHourID(now.Day, wsid);
                    if (id == -1)
                    {
                        wh = new WorkingHour();
                        return wh.LoadByPrimaryKey(GetWorkingHourID(now.Day, wsd)) ? wh : null;
                    }
                    else
                    {
                        wh = new WorkingHour();
                        return wh.LoadByPrimaryKey(id) ? wh : null;
                    }
                }
            }

            return null;
        }

        private WorkingHour GetWorkingHour(int personID, DateTime now)
        {
            var period = new PayrollPeriod();
            period.Query.Where(period.Query.SPTMonth == now.Month, period.Query.SPTYear == now.Year);
            if (!period.Query.Load()) return null;

            var wh = new WorkingHour();

            var wsdq = new BusinessObject.WorkingScheduleDetailQuery("a");
            var wsq = new WorkingScheduleQuery("b");

            wsdq.es.Top = 1;
            wsdq.InnerJoin(wsq).On(wsdq.WorkingScheduleID == wsq.WorkingScheduleID && wsq.PayrollPeriodID == period.PayrollPeriodID && wsq.IsApproved == true);
            wsdq.Where(wsdq.PersonID == personID);
            wsdq.Where($"<a.WorkingHourIDDay{now.Day} IS NOT NULL>");
            wsdq.OrderBy(wsdq.LastUpdateDateTime.Descending);

            var wsd = new BusinessObject.WorkingScheduleDetail();
            if (!wsd.Load(wsdq)) //return null; // tidak punya jadwal
            {
                wsdq = new BusinessObject.WorkingScheduleDetailQuery("a");
                wsq = new WorkingScheduleQuery("b");

                wsdq.es.Top = 1;
                wsdq.InnerJoin(wsq).On(wsdq.WorkingScheduleID == wsq.WorkingScheduleID && wsq.PayrollPeriodID == period.PayrollPeriodID && wsq.IsApproved == true);
                wsdq.Where(wsdq.PersonID == personID);
                //wsdq.Where($"<a.WorkingHourIDDay{now.Day} IS NOT NULL>");
                wsdq.OrderBy(wsdq.LastUpdateDateTime.Descending);

                wsd = new BusinessObject.WorkingScheduleDetail();
                if (!wsd.Load(wsdq)) return null;
            }

            var wsidq = new WorkingSchduleInterventionDetailQuery("a");
            var wsiq = new WorkingSchduleInterventionQuery("b");

            wsidq.es.Top = 1;
            wsidq.InnerJoin(wsiq).On(wsidq.WorkingSchduleInterventionID == wsiq.WorkingSchduleInterventionID && wsiq.WorkingScheduleID == wsd.WorkingScheduleID && wsiq.IsApproved == true);
            wsidq.Where(wsidq.PersonID == personID);//, wsidq.LastUpdateDateTime >= now, wsidq.LastUpdateDateTime < now.AddMonths(1));
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
                    var id = GetWorkingHourID(now.Day, wsid);
                    if (id == -1)
                    {
                        return wh.LoadByPrimaryKey(GetWorkingHourID(now.Day, wsd)) ? wh : null;
                    }
                    else
                    {
                        wh = new WorkingHour();
                        return wh.LoadByPrimaryKey(id) ? wh : null;
                    }
                }
            }

            return null;
        }

        private BusinessObject.EmployeeOvertimeItem GetOvertime(DateTime now, int personID)
        {
            var oiq = new EmployeeOvertimeItemQuery("b");
            var oq = new EmployeeOvertimeQuery("a");

            oiq.InnerJoin(oq).On(oq.TransactionNo == oiq.TransactionNo && oq.TransactionDate.Date() == now.Date && oq.IsApproved == true && oq.IsVerified == true);
            oiq.Where(oiq.PersonID == personID);

            var overtimeItem = new EmployeeOvertimeItem();
            return overtimeItem.Load(oiq) ? overtimeItem : null;
        }

        private (bool status, DateTime date) ValidateNoTap(int personID, DateTime now)
        {
            if (_cutOffDateAvailable)
            {
                var cutOffDate = DateTime.ParseExact(ConfigurationManager.AppSettings["AttendanceCutOff"], "yyyy-MM-dd", null, DateTimeStyles.None);
                if (cutOffDate.Date <= now.Date) return (true, now.Date);
            }

            var workingHourID = -1;

            var last = GetWorkingHour(personID, now);
            //if (last.IsCrossDay ?? false) last = GetWorkingHour(personID, now.AddDays(-1));
            if (last != null)
            {
                if (!(last.IsOffDay ?? false))
                {
                    var cuti = ValidateLeave(now, personID);
                    if (cuti == null) workingHourID = last.WorkingHourID ?? -1;
                    else return ValidateNoTap(personID, now.AddDays(-1));
                }
                else
                {
                    var overtime = GetOvertime(now, personID);
                    if (overtime != null) workingHourID = overtime.WorkingHourID ?? -1;
                    else return ValidateNoTap(personID, now.AddDays(-1));
                }

                var history = new MonthlyAttendanceDetailHistory();
                history.Query.es.Top = 1;
                history.Query.Where(history.Query.PersonID == personID && history.Query.WorkingHourID == workingHourID && history.Query.CheckInDateTime.Date() == now.Date
                    && history.Query.CheckOutDateTime.Date() == ((last.IsCrossDay ?? false) ? now.AddDays(1).Date : now.Date));
                if (history.Query.Load()) return (true, now.Date);
                else
                {
                    history = new MonthlyAttendanceDetailHistory();
                    history.Query.es.Top = 1;

                    var hour = new WorkingHour();
                    hour.LoadByPrimaryKey(workingHourID);
                    if (hour.IsCrossDay ?? false)
                    {
                        history.Query.Where(history.Query.PersonID == personID && history.Query.WorkingHourID == workingHourID && history.Query.CheckInDateTime.Date() == now.Date
                            && history.Query.CheckOutDateTime.IsNull());
                        return (history.Query.Load(), now.Date);
                    }
                    else
                    {
                        history.Query.Where(history.Query.PersonID == personID && history.Query.WorkingHourID == workingHourID && history.Query.CheckInDateTime.Date() == now.Date
                            && history.Query.CheckOutDateTime.IsNotNull());
                        return (history.Query.Load(), now.Date);
                    }
                }
            }
            else return (true, now.Date);
        }

        [WebMethod]
        public string ValidateAttendance(string employeeNo, string dateTime)
        {
            string format = "yyyyMMdd-HHmm";
            DateTime.TryParseExact(dateTime, format, null, DateTimeStyles.None, out var now);

            try
            {
                var log = new WebServiceAPILog()
                {
                    DateRequest = DateTime.Now,
                    IPAddress = string.Empty,
                    UrlAddress = string.Empty,
                    Params = JsonConvert.SerializeObject(new
                    {
                        employeeNo = employeeNo,
                        dateTime = dateTime
                    }),
                    Response = employeeNo,
                    Totalms = 0
                };
                log.Save();

                var emp = new VwEmployeeTable();
                emp.Query.Where(emp.Query.EmployeeNumber == employeeNo);
                if (!emp.Query.Load())
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = false,
                        message = "Kartu tidak terbaca;silahkan lapor ke SDM"
                    });
                }

                var period = new PayrollPeriod();
                period.Query.Where(period.Query.SPTMonth == now.Month && period.Query.SPTYear == now.Year);
                if (!period.Query.Load())
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = false,
                        message = $"Halo {emp.EmployeeName};anda tidak dijadwalkan;silahkan lapor ke SDM"
                    });
                }

                if (emp.SREmployeeType == "01") // medis, dokter
                {
                    AddHeader(period.PayrollPeriodID ?? -1, emp.PersonID ?? -1);
                    return NotValidated(now, period.PayrollPeriodID ?? -1, emp.PersonID ?? -1);
                }

                var hour = new WorkingHour();

                var isCheckIn = true;
                var history = new MonthlyAttendanceDetailHistory();
                var cutOffDate = DateTime.ParseExact(ConfigurationManager.AppSettings["AttendanceCutOff"], "yyyy-MM-dd", null, DateTimeStyles.None);
                if (_cutOffDateAvailable && now.AddDays(-1).Date == cutOffDate.Date)
                {
                    history = new MonthlyAttendanceDetailHistory();
                    history.Query.es.Top = 1;
                    history.Query.Where(history.Query.PersonID == emp.PersonID && history.Query.CheckInDateTime.Date() == now.Date && history.Query.CheckInDateTime < now);
                    history.Query.OrderBy(history.Query.CheckInDateTime.Descending);
                    if (history.Query.Load())
                    {
                        hour = new WorkingHour();
                        hour.LoadByPrimaryKey(history.WorkingHourID ?? -1);

                        if (hour.SRShift != "ShiftID-013") isCheckIn = false;
                        else
                        {
                            if (history.CheckOutDateTime == null) isCheckIn = false;
                        }
                    }
                    else
                    {
                        hour = new WorkingHour();
                        hour = GetWorkingHour(emp.PersonID ?? -1, now);
                    }
                }
                else
                {
                    history = new MonthlyAttendanceDetailHistory();
                    history.Query.es.Top = 1;
                    history.Query.Where(history.Query.PersonID == emp.PersonID && history.Query.CheckInDateTime < now &&
                                        history.Query.CheckOutDateTime.IsNull());
                    history.Query.OrderBy(history.Query.CheckInDateTime.Descending);
                    if (history.Query.Load())
                    {
                        hour = new WorkingHour();
                        hour.LoadByPrimaryKey(history.WorkingHourID ?? -1);

                        if (new string[]
                            {
                                /*"ShiftID-023", */"ShiftID-013", "ShiftID-003"
                            }.Contains(hour.SRShift) && (hour.IsCrossDay ?? false))
                        {
                            if (now.Day == 1) // cek setiap awal bulan, karyawan yg masuk malam dan pulang pagi
                            {
                                period = new PayrollPeriod();
                                period.Query.Where(period.Query.SPTMonth == now.Month - 1 &&
                                                   period.Query.SPTYear == now.Year);
                                if (!period.Query.Load())
                                {
                                    period = new PayrollPeriod();
                                    period.Query.Where(
                                        period.Query.SPTMonth == (now.Month - 1 == 0 ? 12 : now.Month - 1) &&
                                        period.Query.SPTYear == now.Year - 1);
                                    period.Query.Load();
                                }

                                // cek tgl sebelumnya, jika jadwalnya 2 shift pagi malam
                                var mad = new MonthlyAttendanceDetail();
                                mad.Query.es.Top = 1;
                                mad.Query.Where(mad.Query.PayrollPeriodID == period.PayrollPeriodID,
                                    mad.Query.PersonID == emp.PersonID,
                                    mad.Query.ScheduleInDate.Date() == now.Date.AddDays(-1).Date,
                                    mad.Query.CheckOutDate.IsNull(), mad.Query.CheckOutTime.IsNull());
                                if (mad.Query.Load())
                                {
                                    hour = new WorkingHour();
                                    if (hour.LoadByPrimaryKey(mad.WorkingHourID ?? 0))
                                    {
                                        if (!new string[]
                                            {
                                                /*"ShiftID-023", */"ShiftID-013", "ShiftID-003"
                                            }.Contains(hour.SRShift) && !(hour.IsCrossDay ?? false))
                                        {
                                            period = new PayrollPeriod();
                                            period.Query.Where(period.Query.SPTMonth == now.Month &&
                                                               period.Query.SPTYear == now.Year);
                                            period.Query.Load();
                                        }
                                    }
                                }
                                else
                                {
                                    period = new PayrollPeriod();
                                    period.Query.Where(period.Query.SPTMonth == now.Month &&
                                                       period.Query.SPTYear == now.Year);
                                    period.Query.Load();
                                }
                            }
                        }

                        isCheckIn = false;

                        {
                            var mad = new MonthlyAttendanceDetail();
                            mad.Query.Where(mad.Query.PayrollPeriodID == period.PayrollPeriodID,
                                mad.Query.PersonID == emp.PersonID,
                                mad.Query.ScheduleInDate.Date() == history.CheckInDateTime?.Date,
                                mad.Query.CheckOutDate.IsNotNull(),
                                mad.Query.CheckOutTime.Coalesce("''") != string.Empty,
                                mad.Query.WorkingHourID == hour.WorkingHourID);
                            if (mad.Query.Load())
                            {
                                if (!new string[]
                                    {
                                        /*"ShiftID-023", */"ShiftID-013"
                                    }.Contains(hour.SRShift))
                                {
                                    history.CheckOutDateTime =
                                        mad.CheckOutDate?.Date.Add(TimeSpan.ParseExact(mad.CheckOutTime, "hh\\:mm",
                                            null));
                                    history.Save();

                                    return ValidateAttendance(emp.EmployeeNumber, now.ToString("yyyyMMdd-HHmm"));
                                }
                            }
                        }

                        //mad = new MonthlyAttendanceDetail();
                        //mad.Query.Where(mad.Query.PayrollPeriodID == period.PayrollPeriodID, mad.Query.PersonID == emp.PersonID, mad.Query.ScheduleInDate.Date() == history.CheckInDateTime?.Date,
                        //    mad.Query.WorkingHourID == hour.WorkingHourID);
                        //if (!mad.Query.Load())
                        //{
                        //    history.MarkAsDeleted();
                        //    history.Save();

                        //    return ValidateAttendance(emp.EmployeeNumber, now.ToString("yyyyMMdd-HHmm"));
                        //}

                        if (new string[] { "ShiftID-001", "ShiftID-002" /*, "ShiftID-012"*/ }.Contains(hour.SRShift) &&
                            !(hour.IsCrossDay ?? false))
                        {
                            //var schCheckOut = history.CheckInDateTime.Value.Date.Add(TimeSpan.ParseExact(hour.EndTime, "hh\\:mm", null));
                            var maxCheckOut =
                                history.CheckInDateTime.Value.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime,
                                    "hh\\:mm", null));
                            if (now > maxCheckOut)
                            {
                                return JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    message =
                                        $"Halo {emp.EmployeeName};anda belum absen pulang {maxCheckOut.ToString("dd/MM/yyyy")};silahkan lapor ke SDM"
                                });
                            }
                        }
                        else if (new string[]
                                 {
                                     /*"ShiftID-023", */"ShiftID-013", "ShiftID-003"
                                 }.Contains(hour.SRShift) && (hour.IsCrossDay ?? false))
                        {
                            //var schCheckOut = history.CheckInDateTime.Value.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.EndTime, "hh\\:mm", null));
                            var maxCheckOut = history.CheckInDateTime.Value.AddDays(1).Date
                                .Add(TimeSpan.ParseExact(hour.MaximumEndTime, "hh\\:mm", null));
                            if (now > maxCheckOut)
                            {
                                return JsonConvert.SerializeObject(new
                                {
                                    status = false,
                                    message =
                                        $"Halo {emp.EmployeeName};anda belum absen pulang {maxCheckOut.ToString("dd/MM/yyyy")};silahkan lapor ke SDM"
                                });
                            }
                        }
                    }
                    else
                    {
                        history = new MonthlyAttendanceDetailHistory();
                        history.Query.es.Top = 1;
                        history.Query.Where(history.Query.PersonID == emp.PersonID &&
                                            history.Query.CheckInDateTime.Date() == now.Date &&
                                            history.Query.CheckInDateTime < now);
                        history.Query.OrderBy(history.Query.CheckInDateTime.Descending);
                        if (history.Query.Load())
                        {
                            hour = new WorkingHour();
                            hour.LoadByPrimaryKey(history.WorkingHourID ?? -1);

                            if (hour.SRShift != "ShiftID-013") isCheckIn = false;
                            else
                            {
                                if (history.CheckOutDateTime == null) isCheckIn = false;
                            }
                        }
                        else
                        {
                            hour = new WorkingHour();
                            hour = GetWorkingHour(emp.PersonID ?? -1, now);
                        }
                    }
                }

                {
                    // cek apabila ada yg bolos tidak absen
                    var bolos = ValidateNoTap(emp.PersonID ?? -1, now.AddDays(-1));
                    if (!bolos.status)
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = $"Halo {emp.EmployeeName};anda belum absen {bolos.date.ToString("dd/MM/yyyy")};silahkan lapor ke SDM"
                        });
                    }

                    // cuti
                    var leave = ValidateLeave(now, emp.PersonID ?? -1);
                    if (leave != null)
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = $"Halo {emp.EmployeeName};anda tidak dijadwalkan;silahkan lapor ke SDM"
                        });
                    }

                    // jadwal
                    if (isCheckIn)
                    {
                        hour = GetWorkingHour(emp.PersonID ?? -1, period.PayrollPeriodID ?? -1, now, isCheckIn);
                        if (hour == null)
                        {
                            return JsonConvert.SerializeObject(new
                            {
                                status = false,
                                message = $"Halo {emp.EmployeeName};anda tidak dijadwalkan;silahkan lapor ke SDM"
                            });
                        }
                    }

                    // tidak divalidasi
                    if (hour.IsNotValidated ?? false)
                    {
                        AddHeader(period.PayrollPeriodID ?? -1, emp.PersonID ?? -1);
                        return NotValidated(now, period.PayrollPeriodID ?? -1, emp.PersonID ?? -1);
                    }

                    // libur
                    if (hour.IsOffDay ?? false)
                    {
                        // cek lembur di hari libur
                        var overtime = GetOvertime((hour.IsCrossDay ?? false) ? now.AddDays(-1) : now, emp.PersonID ?? -1);
                        if (overtime != null)
                        {
                            hour = new WorkingHour();
                            hour.LoadByPrimaryKey(overtime.WorkingHourID ?? -1);
                            hour.IsOvertimeWorkingHour = true;
                            hour.OvertimeValueInMinutes = Convert.ToInt32(overtime.Amount ?? 0);
                        }
                    }
                    else
                    // lembur di hari kerja
                    {
                        var overtime = GetOvertime((hour.IsCrossDay ?? false) ? now.AddDays(-1) : now, emp.PersonID ?? -1);
                        if (overtime != null)
                        {
                            var minCheckOut = now.Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime, "hh\\:mm", null));
                            var schCheckOut = now.Date.Add(TimeSpan.ParseExact(hour.EndTime, "hh\\:mm", null));
                            var maxCheckOut = now.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime, "hh\\:mm", null));

                            //hour.MinimumEndTime = minCheckOut.AddHours(Convert.ToDouble(overtime.Amount ?? 0)).ToString("HH:mm");
                            hour.EndTime = schCheckOut.AddHours(Convert.ToDouble(overtime.Amount ?? 0)).ToString("HH:mm");
                            hour.MaximumEndTime = maxCheckOut.AddHours(Convert.ToDouble(overtime.Amount ?? 0)).ToString("HH:mm");
                            hour.IsOvertimeWorkingHour = true;
                            hour.OvertimeValueInMinutes = Convert.ToInt32(overtime.Amount ?? 0);
                        }
                    }

                    // ijin plg cepat atw ijin masuk terlambat
                    {
                        var permission = new EmployeePermission();
                        permission.Query.Where(permission.Query.PersonID == (emp.PersonID ?? -1), permission.Query.PermissionDateFrom.Date() == now.Date, permission.Query.PermissionDateTo.Date() == now.Date,
                            permission.Query.SRPermissionType == "01", permission.Query.IsApproved == true, permission.Query.IsVerified == true);
                        if (permission.Query.Load())
                        {
                            var ijinEnd = permission.PermissionDateFrom.Value.Date.Add(TimeSpan.ParseExact(permission.PermissionTimeTo, "hh\\:mm", null));

                            if (new string[] { "ShiftID-001", "ShiftID-002", "ShiftID-003", "ShiftID-012", "ShiftID-023", }.Contains(hour.SRShift))
                            {
                                hour.StartTime = permission.PermissionTimeTo;
                                hour.MaximumStartTime = permission.PermissionTimeTo;
                            }
                            else if (new string[] { "ShiftID-013", }.Contains(hour.SRShift))
                            {
                                var schCheckIn = now.Date.Add(TimeSpan.ParseExact(hour.StartTime, "hh\\:mm", null));
                                var schCheckOut = now.Date.Add(TimeSpan.ParseExact(hour.EndTime, "hh\\:mm", null));

                                if (ijinEnd >= schCheckIn && ijinEnd <= schCheckOut)
                                {
                                    hour.StartTime = permission.PermissionTimeTo;
                                    hour.MaximumStartTime = permission.PermissionTimeTo;
                                }
                                else
                                {
                                    hour.StartTime2 = permission.PermissionTimeTo;
                                    hour.MaximumStartTime2 = permission.PermissionTimeTo;
                                }
                            }
                        }
                        permission = new EmployeePermission();
                        permission.Query.Where(permission.Query.PersonID == (emp.PersonID ?? -1), permission.Query.PermissionDateFrom.Date() == now.Date, permission.Query.PermissionDateTo.Date() == now.Date,
                            permission.Query.SRPermissionType == "02", permission.Query.IsApproved == true, permission.Query.IsVerified == true);
                        if (permission.Query.Load())
                        {
                            var ijinStart = permission.PermissionDateFrom.Value.Date.Add(TimeSpan.ParseExact(permission.PermissionTimeFrom, "hh\\:mm", null));

                            if (new string[] { "ShiftID-001", "ShiftID-002", "ShiftID-003", "ShiftID-012", "ShiftID-023", }.Contains(hour.SRShift))
                            {
                                hour.EndTime = permission.PermissionTimeFrom;
                                hour.MaximumEndTime = permission.PermissionTimeFrom;
                            }
                            else if (new string[] { "ShiftID-013", }.Contains(hour.SRShift))
                            {
                                var schCheckIn = now.Date.Add(TimeSpan.ParseExact(hour.StartTime, "hh\\:mm", null));
                                var schCheckOut = now.Date.Add(TimeSpan.ParseExact(hour.EndTime, "hh\\:mm", null));

                                if (ijinStart >= schCheckIn && ijinStart <= schCheckOut)
                                {
                                    hour.EndTime = permission.PermissionTimeFrom;
                                    hour.MaximumEndTime = permission.PermissionTimeFrom;
                                }
                                else
                                {
                                    hour.EndTime2 = permission.PermissionTimeFrom;
                                    hour.MaximumEndTime2 = permission.PermissionTimeFrom;
                                }
                            }
                        }
                    }

                    // cek apabila sdh tap in dan out
                    //if (hour.SRShift != "ShiftID-013") // diluar pagi malam
                    {
                        var tapHistory = new MonthlyAttendanceDetailHistory();
                        tapHistory.Query.es.Top = 1;
                        tapHistory.Query.Where(tapHistory.Query.PersonID == emp.PersonID && tapHistory.Query.WorkingHourID == hour.WorkingHourID);
                        //if (hour.SRShift == "ShiftID-003")
                        //    history.Query.Where(history.Query.CheckInDateTime.Date() == now.Date);
                        //else
                        //    history.Query.Where($"<'{now.Date.ToString("yyyyMMdd")}' BETWEEN CONVERT(VARCHAR(MAX), {history.Query.CheckInDateTime}, 112) AND CONVERT(VARCHAR(MAX), {history.Query.CheckOutDateTime}, 112)>");
                        //if (history.Query.Load())
                        //{
                        //    return JsonConvert.SerializeObject(new
                        //    {
                        //        status = false,
                        //        message = "Anda tidak dijadwalkan;silahkan lapor ke SDM"
                        //    });
                        //}

                        if (isCheckIn)
                        {
                            tapHistory.Query.OrderBy(tapHistory.Query.CheckInDateTime.Descending);

                            if (hour.SRShift != "ShiftID-013")
                            {
                                var maxCheckIn = now.Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime, "hh\\:mm", null));

                                tapHistory.Query.Where(tapHistory.Query.CheckInDateTime.Date() == now.Date && tapHistory.Query.CheckInDateTime <= now);
                                if (tapHistory.Query.Load())
                                {
                                    if (now <= maxCheckIn)
                                    {
                                        return JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            message = $"Halo {emp.EmployeeName};anda sudah absen masuk pada {tapHistory.CheckInDateTime?.ToString("HH:mm")}",
                                        });
                                    }
                                    else
                                    {
                                        return JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya",
                                        });
                                    }
                                }
                            }
                            else
                            {
                                var minCheckIn = now.Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime, "hh\\:mm", null));
                                var maxCheckIn = now.Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime, "hh\\:mm", null));
                                var minCheckIn2 = now.Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime2, "hh\\:mm", null));
                                var maxCheckIn2 = now.Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime2, "hh\\:mm", null));

                                tapHistory.Query.Where(history.Query.CheckInDateTime.Date() == now.Date && history.Query.CheckOutDateTime <= now);
                                if (tapHistory.Query.Load())
                                {
                                    tapHistory = new MonthlyAttendanceDetailHistory();
                                    tapHistory.Query.es.Top = 1;
                                    tapHistory.Query.Where(tapHistory.Query.PersonID == emp.PersonID && tapHistory.Query.WorkingHourID == hour.WorkingHourID);
                                    tapHistory.Query.OrderBy(tapHistory.Query.CheckInDateTime.Descending);

                                    if (now >= minCheckIn && now <= maxCheckIn)
                                    {
                                        tapHistory.Query.Where(tapHistory.Query.CheckInDateTime.Date() == now.Date && tapHistory.Query.CheckInDateTime.Between(minCheckIn, maxCheckIn));
                                        if (tapHistory.Query.Load())
                                        {
                                            return JsonConvert.SerializeObject(new
                                            {
                                                status = false,
                                                message = $"Halo {emp.EmployeeName};anda sudah absen masuk pada {tapHistory.CheckInDateTime?.ToString("HH:mm")}",
                                            });
                                        }
                                    }
                                    else if (now >= minCheckIn2 && now <= maxCheckIn2)
                                    {
                                        tapHistory.Query.Where(tapHistory.Query.CheckInDateTime.Date() == now.Date && tapHistory.Query.CheckInDateTime.Between(minCheckIn2, maxCheckIn2));
                                        if (tapHistory.Query.Load())
                                        {
                                            return JsonConvert.SerializeObject(new
                                            {
                                                status = false,
                                                message = $"Halo {emp.EmployeeName};anda sudah absen masuk pada {tapHistory.CheckInDateTime?.ToString("HH:mm")}",
                                            });
                                        }
                                    }
                                    else
                                    {
                                        return JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya",
                                        });
                                    }
                                }
                            }
                        }
                        else
                        {
                            tapHistory.Query.OrderBy(tapHistory.Query.CheckOutDateTime.Descending);

                            if (hour.SRShift != "ShiftID-013")
                            {
                                var maxCheckOut = (hour.IsCrossDay ?? false) ? now.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime, "hh\\:mm", null)) : now.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime, "hh\\:mm", null));

                                tapHistory.Query.Where(tapHistory.Query.CheckOutDateTime.Date() == now.Date && tapHistory.Query.CheckOutDateTime <= now);
                                if (tapHistory.Query.Load())
                                {
                                    if (now <= maxCheckOut)
                                    {
                                        return JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            message = $"Halo {emp.EmployeeName};anda sudah absen pulang pada {tapHistory.CheckOutDateTime?.ToString("HH:mm")}",
                                        });
                                    }
                                    else
                                    {
                                        return JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya",
                                        });
                                    }
                                }
                            }
                            else
                            {
                                var minCheckOut = now.Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime, "hh\\:mm", null));
                                var maxCheckOut = now.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime, "hh\\:mm", null));
                                var minCheckOut2 = (hour.IsCrossDay ?? false) ? now.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime2, "hh\\:mm", null)) : now.Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime2, "hh\\:mm", null));
                                var maxCheckOut2 = (hour.IsCrossDay ?? false) ? now.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime2, "hh\\:mm", null)) : now.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime2, "hh\\:mm", null));

                                tapHistory.Query.Where(tapHistory.Query.CheckInDateTime.Date() == now.Date && tapHistory.Query.CheckOutDateTime <= now);
                                if (tapHistory.Query.Load())
                                {
                                    tapHistory = new MonthlyAttendanceDetailHistory();
                                    tapHistory.Query.es.Top = 1;
                                    tapHistory.Query.Where(tapHistory.Query.PersonID == emp.PersonID && tapHistory.Query.WorkingHourID == hour.WorkingHourID);
                                    tapHistory.Query.OrderBy(tapHistory.Query.CheckInDateTime.Descending);

                                    if (now >= minCheckOut && now <= maxCheckOut)
                                    {
                                        tapHistory.Query.Where(tapHistory.Query.CheckInDateTime.Date() == now.Date && tapHistory.Query.CheckOutDateTime.Between(minCheckOut, maxCheckOut));
                                        if (tapHistory.Query.Load())
                                        {
                                            return JsonConvert.SerializeObject(new
                                            {
                                                status = false,
                                                message = $"Halo {emp.EmployeeName};anda sudah absen pulang pada {tapHistory.CheckOutDateTime?.ToString("HH:mm")}",
                                            });
                                        }
                                    }
                                    else if (now >= minCheckOut2 && now <= maxCheckOut2)
                                    {
                                        tapHistory.Query.Where(tapHistory.Query.CheckInDateTime.Date() == now.Date && tapHistory.Query.CheckOutDateTime.Between(minCheckOut, maxCheckOut));
                                        if (tapHistory.Query.Load())
                                        {
                                            return JsonConvert.SerializeObject(new
                                            {
                                                status = false,
                                                message = $"Halo {emp.EmployeeName};anda sudah absen pulang pada {tapHistory.CheckOutDateTime?.ToString("HH:mm")}",
                                            });
                                        }
                                    }
                                    else
                                    {
                                        return JsonConvert.SerializeObject(new
                                        {
                                            status = false,
                                            message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya",
                                        });
                                    }
                                }
                            }
                        }
                    }
                }

                AddHeader(period.PayrollPeriodID ?? -1, emp.PersonID ?? -1);

                if (new string[] { "ShiftID-001", "ShiftID-002" }.Contains(hour.SRShift)) // single shift tgl in dan tgl out sama
                {
                    var str = SingleSameday(now, period.PayrollPeriodID ?? -1, emp.PersonID ?? -1, hour, isCheckIn);
                    if (!string.IsNullOrWhiteSpace(str)) return str;
                }
                else if (new string[] { "ShiftID-003" }.Contains(hour.SRShift) && (hour.IsCrossDay ?? false)) // single shift (malam) tgl in dan tgl out beda
                {
                    var str = SingleCrossday(now, period.PayrollPeriodID ?? -1, emp.PersonID ?? -1, hour, isCheckIn);
                    if (!string.IsNullOrWhiteSpace(str)) return str;
                }
                //else if (new string[] { "ShiftID-012" }.Contains(hour.SRShift)) // double shift tgl in dan tgl out sama (pagi dan sore)
                //{
                //    var str = DoubleSameday(now, period.PayrollPeriodID ?? -1, emp.PersonID ?? -1, hour, isCheckIn);
                //    if (!string.IsNullOrWhiteSpace(str)) return str;
                //}
                else if (new string[] { "ShiftID-013" }.Contains(hour.SRShift) && (hour.IsCrossDay ?? false)) // double shift tgl in dan tgl out beda (pagi dan malam)
                {
                    var minCheckIn = isCheckIn ? now.Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime, "hh\\:mm", null)) :
                        now.Date == history.CheckInDateTime.Value.Date ? now.Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime, "hh\\:mm", null)) : now.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime, "hh\\:mm", null));
                    var maxCheckIn = isCheckIn ? now.Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime, "hh\\:mm", null)) :
                        now.Date == history.CheckInDateTime.Value.Date ? now.Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime, "hh\\:mm", null)) : now.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime, "hh\\:mm", null));

                    var minCheckOut = isCheckIn ? now.Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime, "hh\\:mm", null)) :
                        now.Date == history.CheckInDateTime.Value.Date ? now.Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime, "hh\\:mm", null)) : now.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime, "hh\\:mm", null));
                    var maxCheckOut = isCheckIn ? now.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime, "hh\\:mm", null)) :
                        now.Date == history.CheckInDateTime.Value.Date ? now.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime, "hh\\:mm", null)) : now.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime, "hh\\:mm", null));

                    var minCheckIn2 = isCheckIn ? now.Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime2, "hh\\:mm", null)) :
                        now.Date == history.CheckInDateTime.Value.Date ? now.Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime2, "hh\\:mm", null)) : now.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime2, "hh\\:mm", null));
                    var maxCheckIn2 = isCheckIn ? now.Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime2, "hh\\:mm", null)) :
                        now.Date == history.CheckInDateTime.Value.Date ? now.Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime2, "hh\\:mm", null)) : now.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime2, "hh\\:mm", null));

                    var minCheckOut2 = isCheckIn ? now.Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime2, "hh\\:mm", null)) :
                        now.Date == history.CheckInDateTime.Value.AddDays(1).Date ? now.Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime2, "hh\\:mm", null)) : now.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime2, "hh\\:mm", null));
                    var maxCheckOut2 = isCheckIn ? now.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime2, "hh\\:mm", null)) :
                        now.Date == history.CheckInDateTime.Value.AddDays(1).Date ? now.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime2, "hh\\:mm", null)) : now.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime2, "hh\\:mm", null));

                    if (isCheckIn)
                    {
                        if ((now >= minCheckIn) && (now <= maxCheckIn))
                        {
                            var str = SingleSameday(now, period.PayrollPeriodID ?? -1, emp.PersonID ?? -1, hour, isCheckIn);
                            if (!string.IsNullOrWhiteSpace(str)) return str;
                        }
                        else if ((now >= minCheckIn2) && (now <= maxCheckIn2))
                        {
                            var str = DoubleSkipSingleCrossday(now, period.PayrollPeriodID ?? -1, emp.PersonID ?? -1, hour, isCheckIn);
                            if (!string.IsNullOrWhiteSpace(str)) return str;
                        }
                        else
                        {
                            return JsonConvert.SerializeObject(new
                            {
                                status = false,
                                message = "Anda tidak bisa absen karena belum waktunya"
                            });
                        }
                    }
                    else
                    {
                        if ((now >= minCheckOut) && (now <= maxCheckOut))
                        {
                            var str = SingleSameday(now, period.PayrollPeriodID ?? -1, emp.PersonID ?? -1, hour, isCheckIn);
                            if (!string.IsNullOrWhiteSpace(str)) return str;
                        }
                        else if ((now >= minCheckOut2) && (now <= maxCheckOut2))
                        {
                            var str = DoubleSkipSingleCrossday(now, period.PayrollPeriodID ?? -1, emp.PersonID ?? -1, hour, isCheckIn);
                            if (!string.IsNullOrWhiteSpace(str)) return str;
                        }
                        else
                        {
                            return JsonConvert.SerializeObject(new
                            {
                                status = false,
                                message = "Anda tidak bisa absen karena belum waktunya"
                            });
                        }
                    }
                }
                //else if (new string[] { "ShiftID-023" }.Contains(hour.SRShift) && (hour.IsCrossDay ?? false)) // double shift tgl in dan tgl out beda (sore dan malam)
                //{
                //    var str = DoubleCrossday(now, period.PayrollPeriodID ?? -1, emp.PersonID ?? -1, hour, isCheckIn);
                //    if (!string.IsNullOrWhiteSpace(str)) return str;
                //}

                if (isCheckIn)
                {
                    if (new string[] { "ShiftID-001", "ShiftID-002", "ShiftID-003" }.Contains(hour.SRShift))
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            status = true,
                            message = $"Selamat bekerja {emp.EmployeeName};anda sudah absen masuk pada {now.ToString("HH:mm")}",
                            detail = $"Jadwal anda : {hour.StartTime} s/d {hour.EndTime}"
                        });
                    }
                    //else if (new string[] { "ShiftID-012", "ShiftID-023" }.Contains(hour.SRShift))
                    //{
                    //    return JsonConvert.SerializeObject(new
                    //    {
                    //        status = true,
                    //        message = $"Selamat Bekerja {emp.EmployeeName}, anda sudah absen masuk pada {now.ToString("HH:mm")}",
                    //        detail = $"{hour.StartTime} s/d {hour.EndTime2}"
                    //    });
                    //}
                    else if (new string[] { "ShiftID-013", }.Contains(hour.SRShift))
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            status = true,
                            message = $"Selamat bekerja {emp.EmployeeName};anda sudah absen masuk pada {now.ToString("HH:mm")}",
                            detail = $"Jadwal anda : {hour.StartTime} s/d {hour.EndTime} dan {hour.StartTime2} s/d {hour.EndTime2}"
                        });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya"
                        });
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = true,
                        message = $"Hati-hati dijalan {emp.EmployeeName};anda sudah absen pulang pada {now.ToString("HH:mm")}"
                    });
                }
            }
            catch (Exception ex)
            {
                var log = new WebServiceAPILog()
                {
                    DateRequest = DateTime.Now,
                    IPAddress = string.Empty,
                    UrlAddress = string.Empty,
                    Params = JsonConvert.SerializeObject(new
                    {
                        employeeNo = employeeNo,
                        dateTime = dateTime
                    }),
                    Response = ex.Message,
                    Totalms = 0
                };
                log.Save();

                return JsonConvert.SerializeObject(new
                {
                    status = false,
                    message = "Terjadi kesalahan;silahkan dicoba kembali atau silahkan lapor ke SDM"
                });
            }
        }

        private EmployeeLeaveRequest ValidateLeave(DateTime date, int personID)
        {
            var leave = new EmployeeLeaveRequest();
            leave.Query.es.Top = 1;
            leave.Query.Where(leave.Query.PersonID == personID, leave.Query.IsRequestApproved == true, leave.Query.IsVerified == true);
            leave.Query.Where($"<{date.ToString("yyyyMMdd")} BETWEEN CONVERT(varchar(max), ApprovedLeaveDateFrom, 112) AND CONVERT(varchar(max), ApprovedLeaveDateTo, 112)>");
            leave.Query.OrderBy(leave.Query.CreatedDateTime.Descending);
            return leave.Query.Load() ? leave : null;
        }

        private void AddHeader(int payrollPeriodID, int personID)
        {
            var attendances = new MonthlyAttendanceCollection();
            attendances.Query.Where(attendances.Query.PayrollPeriodID == payrollPeriodID && attendances.Query.PersonID == personID);
            attendances.Query.OrderBy(attendances.Query.LastUpdateDateTime.Ascending);
            if (attendances.Query.Load() && attendances.Count > 1)
            {
                for (int i = 1; i < attendances.Count; i++)
                {
                    attendances[i].MarkAsDeleted();
                }

                attendances.Save();
            }

            var attendance = new MonthlyAttendance();
            attendance.Query.Where(attendance.Query.PayrollPeriodID == payrollPeriodID && attendance.Query.PersonID == personID);
            if (!attendance.Query.Load())
            {
                attendance = new MonthlyAttendance();
                attendance.PayrollPeriodID = payrollPeriodID;
                attendance.PersonID = personID;
                attendance.Save();
            }
        }

        private string NotValidated(DateTime dateTime, int payrollPeriodID, int personID)
        {
            var emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == personID);
            if (!emp.Query.Load())
            {
                return JsonConvert.SerializeObject(new
                {
                    status = false,
                    message = "Kartu tidak terbaca;silahkan lapor ke SDM"
                });
            }

            var attendanceDetail = new MonthlyAttendanceDetail();
            attendanceDetail.Query.Where(attendanceDetail.Query.PersonID == personID,
                attendanceDetail.Query.PayrollPeriodID == payrollPeriodID,
                attendanceDetail.Query.Or(
                    attendanceDetail.Query.CheckOutTime.IsNull(),
                    attendanceDetail.Query.CheckOutTime == string.Empty
                ));
            if (attendanceDetail.Query.Load())
            {
                attendanceDetail.ScheduleOutDate = dateTime.Date;
                attendanceDetail.ScheduleOutTime = dateTime.ToString("HH:mm");
                attendanceDetail.CheckOutDate = dateTime.Date;
                attendanceDetail.CheckOutTime = dateTime.ToString("HH:mm");
                attendanceDetail.Save();

                return JsonConvert.SerializeObject(new
                {
                    status = true,
                    message = $"{emp.EmployeeName};anda sudah absen pulang pada {dateTime.ToString("HH:mm")}"
                });
            }
            else
            {
                attendanceDetail = new MonthlyAttendanceDetail();
                attendanceDetail.PersonID = personID;
                attendanceDetail.PayrollPeriodID = payrollPeriodID;
                attendanceDetail.ScheduleInDate = dateTime.Date;
                attendanceDetail.ScheduleInTime = dateTime.ToString("HH:mm");
                attendanceDetail.CheckInDate = dateTime.Date;
                attendanceDetail.CheckInTime = dateTime.ToString("HH:mm");
                attendanceDetail.Save();

                return JsonConvert.SerializeObject(new
                {
                    status = true,
                    message = $"{emp.EmployeeName};anda sudah absen masuk pada {dateTime.ToString("HH:mm")}"
                });
            }
        }

        private string SingleSameday(DateTime dateTime, int payrollPeriodID, int personID, WorkingHour hour, bool isCheckIn)
        {
            var emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == personID);
            emp.Query.Load();

            var minCheckIn = dateTime.Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime, "hh\\:mm", null));
            var schCheckIn = dateTime.Date.Add(TimeSpan.ParseExact(hour.StartTime, "hh\\:mm", null));
            var maxCheckIn = dateTime.Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime, "hh\\:mm", null));

            var minCheckOut = dateTime.Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime, "hh\\:mm", null));
            var schCheckOut = dateTime.Date.Add(TimeSpan.ParseExact(hour.EndTime, "hh\\:mm", null));
            var maxCheckOut = dateTime.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime, "hh\\:mm", null));

            if (isCheckIn)
            {
                if (dateTime < minCheckIn)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = false,
                        message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya"
                    });
                }
                else if (dateTime >= minCheckIn && dateTime <= maxCheckIn)
                {
                    var attendanceDetail = new MonthlyAttendanceDetail();
                    attendanceDetail.Query.Where(attendanceDetail.Query.PersonID == personID &&
                        attendanceDetail.Query.PayrollPeriodID == payrollPeriodID &&
                        attendanceDetail.Query.ScheduleInDate == schCheckIn.Date &&
                        attendanceDetail.Query.ScheduleInTime == schCheckIn.ToString("HH:mm") &&
                        attendanceDetail.Query.CheckInDate == schCheckIn.Date &&
                        attendanceDetail.Query.CheckInTime.IsNotNull());
                    if (!attendanceDetail.Query.Load())
                    {
                        var tapDateTime = dateTime.Date.Add(TimeSpan.ParseExact(dateTime.ToString("HH:mm"), "hh\\:mm", null));

                        attendanceDetail = new MonthlyAttendanceDetail();
                        attendanceDetail.Query.Where(attendanceDetail.Query.PersonID == personID && attendanceDetail.Query.PayrollPeriodID == payrollPeriodID &&
                            attendanceDetail.Query.ScheduleInDate == schCheckIn.Date && attendanceDetail.Query.ScheduleInTime == schCheckIn.ToString("HH:mm"));
                        if (!attendanceDetail.Query.Load())
                        {
                            attendanceDetail = new MonthlyAttendanceDetail();
                            attendanceDetail.PersonID = personID;
                            attendanceDetail.PayrollPeriodID = payrollPeriodID;
                            attendanceDetail.ScheduleInDate = schCheckIn.Date;
                            attendanceDetail.ScheduleInTime = schCheckIn.ToString("HH:mm");
                            attendanceDetail.CheckInDate = tapDateTime.Date;
                            attendanceDetail.CheckInTime = tapDateTime.ToString("HH:mm");
                            attendanceDetail.LateMinutes = dateTime > schCheckIn ? Convert.ToInt16(dateTime.Subtract(schCheckIn).TotalMinutes) : Convert.ToInt16(0);
                            attendanceDetail.ScheduleOutDate = schCheckOut.Date;
                            attendanceDetail.ScheduleOutTime = schCheckOut.ToString("HH:mm");
                            attendanceDetail.IsOvertime = hour.IsOvertimeWorkingHour ?? false;
                            //attendanceDetail.OvertimeHours = Convert.ToInt16(hour.OvertimeValueInMinutes ?? 0);
                            attendanceDetail.WorkingHourID = hour.WorkingHourID;
                            attendanceDetail.Save();
                        }

                        var history = new MonthlyAttendanceDetailHistory();
                        history.Query.Where(history.Query.PersonID == personID && history.Query.WorkingHourID == (hour.WorkingHourID ?? -1) && history.Query.CheckInDateTime == tapDateTime);
                        if (!history.Query.Load())
                        {
                            history = new MonthlyAttendanceDetailHistory();
                            //history.MonthlyAttendanceDetailID = attendanceDetail.MonthlyAttendanceDetailID;
                            history.PersonID = personID;
                            history.WorkingHourID = hour.WorkingHourID;
                            history.CheckInDateTime = tapDateTime;
                            history.Save();
                        }
                    }
                    else
                    {
                        var datetime = attendanceDetail.CheckInDate.Value.Date.Add(TimeSpan.ParseExact(attendanceDetail.CheckInTime, "hh\\:mm", null));
                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = $"Halo {emp.EmployeeName};anda sudah absen masuk pada {attendanceDetail.CheckInTime}"
                        });
                    }
                }
                else if (dateTime > maxCheckIn)// && dateTime < minCheckOut)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = false,
                        message = $"Halo {emp.EmployeeName};anda terlambat lebih dari {maxCheckIn.Subtract(schCheckIn).Hours} jam;silahkan lapor ke SDM"
                    });
                }
            }
            else
            {
                if (dateTime >= minCheckOut && dateTime <= maxCheckOut)
                {
                    var attendanceDetails = new MonthlyAttendanceDetailCollection();
                    attendanceDetails.Query.Where(attendanceDetails.Query.PersonID == personID &&
                        attendanceDetails.Query.PayrollPeriodID == payrollPeriodID &&
                        attendanceDetails.Query.ScheduleInDate == schCheckIn.Date &&
                        //attendanceDetails.Query.ScheduleInTime == schCheckIn.ToString("HH:mm") &&
                        attendanceDetails.Query.CheckInDate == schCheckIn.Date &&
                        attendanceDetails.Query.CheckInTime.IsNotNull());
                    attendanceDetails.Query.OrderBy(attendanceDetails.Query.LastUpdateDateTime.Ascending);
                    attendanceDetails.Query.Load();
                    if (attendanceDetails.Count > 1)
                    {
                        for (int i = 1; i < attendanceDetails.Count; i++)
                        {
                            attendanceDetails[i].MarkAsDeleted();
                        }

                        attendanceDetails.Save();
                    }

                    var attendanceDetail = new MonthlyAttendanceDetail();
                    attendanceDetail.Query.Where(attendanceDetail.Query.PersonID == personID &&
                        attendanceDetail.Query.PayrollPeriodID == payrollPeriodID &&
                        attendanceDetail.Query.ScheduleInDate == schCheckIn.Date &&
                        //attendanceDetail.Query.ScheduleInTime == schCheckIn.ToString("HH:mm") &&
                        attendanceDetail.Query.CheckInDate == schCheckIn.Date &&
                        attendanceDetail.Query.CheckInTime.IsNotNull());
                    if (attendanceDetail.Query.Load())
                    {
                        attendanceDetail.ScheduleOutDate = schCheckOut.Date;
                        attendanceDetail.ScheduleOutTime = schCheckOut.ToString("HH:mm");
                        attendanceDetail.CheckOutDate = dateTime.Date;
                        attendanceDetail.CheckOutTime = dateTime.ToString("HH:mm");

                        if (attendanceDetail.IsOvertime ?? false)
                        {
                            var wh = new WorkingHour();
                            wh.LoadByPrimaryKey(hour.WorkingHourID ?? -1);

                            var overtimeHour = dateTime.Subtract(dateTime.Date.Add(TimeSpan.ParseExact(wh.EndTime, "hh\\:mm", null))).TotalHours;
                            attendanceDetail.OvertimeHours = Convert.ToDecimal(Math.Round(overtimeHour, 2));
                        }

                        attendanceDetail.Save();

                        var checkIn = attendanceDetail.CheckInDate?.Date.Add(TimeSpan.ParseExact(attendanceDetail.CheckInTime, "hh\\:mm", null));

                        var histories = new MonthlyAttendanceDetailHistoryCollection();
                        histories.Query.Where(histories.Query.PersonID == personID && histories.Query.CheckInDateTime == checkIn && histories.Query.CheckOutDateTime.IsNull());
                        histories.Query.Load();
                        foreach (var history in histories)
                        {
                            history.CheckOutDateTime = dateTime;
                        }
                        histories.Save();
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = "Anda belum absen masuk;silahkan lapor ke SDM"
                        });
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = false,
                        message = "Anda tidak bisa absen karena belum waktunya"
                    });
                }
            }

            return string.Empty;
        }

        private string SingleCrossday(DateTime dateTime, int payrollPeriodID, int personID, WorkingHour hour, bool isCheckIn)
        {
            var emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == personID);
            emp.Query.Load();

            var minCheckIn = isCheckIn ? dateTime.Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime, "hh\\:mm", null)) : dateTime.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime, "hh\\:mm", null));
            var schCheckIn = isCheckIn ? dateTime.Date.Add(TimeSpan.ParseExact(hour.StartTime, "hh\\:mm", null)) : dateTime.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.StartTime, "hh\\:mm", null));
            var maxCheckIn = isCheckIn ? dateTime.Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime, "hh\\:mm", null)) : dateTime.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime, "hh\\:mm", null));

            var minCheckOut = isCheckIn ? dateTime.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime, "hh\\:mm", null)) : dateTime.Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime, "hh\\:mm", null));
            var schCheckOut = isCheckIn ? dateTime.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.EndTime, "hh\\:mm", null)) : dateTime.Date.Add(TimeSpan.ParseExact(hour.EndTime, "hh\\:mm", null));
            var maxCheckOut = isCheckIn ? dateTime.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime, "hh\\:mm", null)) : dateTime.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime, "hh\\:mm", null));

            if (isCheckIn)
            {
                if (dateTime < minCheckIn)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = false,
                        message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya"
                    });
                }
                else if (dateTime >= minCheckIn && dateTime <= maxCheckIn)
                {
                    var attendanceDetail = new MonthlyAttendanceDetail();
                    attendanceDetail.Query.Where(attendanceDetail.Query.PersonID == personID &&
                        attendanceDetail.Query.PayrollPeriodID == payrollPeriodID &&
                        attendanceDetail.Query.ScheduleInDate == schCheckIn.Date &&
                        attendanceDetail.Query.ScheduleInTime == schCheckIn.ToString("HH:mm") &&
                        attendanceDetail.Query.CheckInDate == schCheckIn.Date &&
                        attendanceDetail.Query.CheckInTime.IsNotNull());
                    if (!attendanceDetail.Query.Load())
                    {
                        var tapDateTime = dateTime.Date.Add(TimeSpan.ParseExact(dateTime.ToString("HH:mm"), "hh\\:mm", null));

                        attendanceDetail = new MonthlyAttendanceDetail();
                        attendanceDetail.Query.Where(attendanceDetail.Query.PersonID == personID && attendanceDetail.Query.PayrollPeriodID == payrollPeriodID &&
                            attendanceDetail.Query.ScheduleInDate == schCheckIn.Date && attendanceDetail.Query.ScheduleInTime == schCheckIn.ToString("HH:mm"));
                        if (!attendanceDetail.Query.Load())
                        {
                            attendanceDetail = new MonthlyAttendanceDetail();
                            attendanceDetail.PersonID = personID;
                            attendanceDetail.PayrollPeriodID = payrollPeriodID;
                            attendanceDetail.ScheduleInDate = schCheckIn.Date;
                            attendanceDetail.ScheduleInTime = schCheckIn.ToString("HH:mm");
                            attendanceDetail.CheckInDate = tapDateTime.Date;
                            attendanceDetail.CheckInTime = tapDateTime.ToString("HH:mm");
                            attendanceDetail.LateMinutes = dateTime > schCheckIn ? Convert.ToInt16(dateTime.Subtract(schCheckIn).TotalMinutes) : Convert.ToInt16(0);
                            attendanceDetail.ScheduleOutDate = schCheckOut.Date;
                            attendanceDetail.ScheduleOutTime = schCheckOut.ToString("HH:mm");
                            attendanceDetail.IsOvertime = hour.IsOvertimeWorkingHour ?? false;
                            //attendanceDetail.OvertimeHours = Convert.ToInt16(hour.OvertimeValueInMinutes ?? 0);
                            attendanceDetail.WorkingHourID = hour.WorkingHourID;
                            attendanceDetail.Save();
                        }

                        var history = new MonthlyAttendanceDetailHistory();
                        history.Query.Where(history.Query.PersonID == personID && history.Query.WorkingHourID == (hour.WorkingHourID ?? -1) && history.Query.CheckInDateTime == tapDateTime);
                        if (!history.Query.Load())
                        {
                            history = new MonthlyAttendanceDetailHistory();
                            //history.MonthlyAttendanceDetailID = attendanceDetail.MonthlyAttendanceDetailID;
                            history.PersonID = personID;
                            history.WorkingHourID = hour.WorkingHourID;
                            history.CheckInDateTime = tapDateTime;
                            history.Save();
                        }

                        //attendanceDetail = new MonthlyAttendanceDetail();
                        //attendanceDetail.PersonID = personID;
                        //attendanceDetail.PayrollPeriodID = payrollPeriodID;
                        //attendanceDetail.ScheduleInDate = schCheckIn.Date;
                        //attendanceDetail.ScheduleInTime = schCheckIn.ToString("HH:mm");
                        //attendanceDetail.CheckInDate = dateTime.Date;
                        //attendanceDetail.CheckInTime = dateTime.ToString("HH:mm");
                        //attendanceDetail.LateMinutes = dateTime > schCheckIn ? Convert.ToInt16(dateTime.Subtract(schCheckIn).TotalMinutes) : Convert.ToInt16(0);
                        //attendanceDetail.ScheduleOutDate = schCheckOut.Date;
                        //attendanceDetail.ScheduleOutTime = schCheckOut.ToString("HH:mm");
                        //attendanceDetail.IsOvertime = hour.IsOvertimeWorkingHour ?? false;
                        //attendanceDetail.OvertimeHours = Convert.ToInt16(hour.OvertimeValueInMinutes ?? 0);
                        //attendanceDetail.WorkingHourID = hour.WorkingHourID;
                        //attendanceDetail.Save();

                        //var history = new MonthlyAttendanceDetailHistory();
                        ////history.MonthlyAttendanceDetailID = attendanceDetail.MonthlyAttendanceDetailID;
                        //history.PersonID = personID;
                        //history.WorkingHourID = hour.WorkingHourID;
                        //history.CheckInDateTime = dateTime;
                        //history.Save();
                    }
                    else
                    {
                        var datetime = attendanceDetail.CheckInDate.Value.Date.Add(TimeSpan.ParseExact(attendanceDetail.CheckInTime, "hh\\:mm", null));
                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = $"Halo {emp.EmployeeName};anda sudah absen masuk pada {attendanceDetail.CheckInTime}"
                        });
                    }
                }
                else if (dateTime > maxCheckIn && dateTime < minCheckOut)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = false,
                        message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya"
                    });
                }
            }
            else
            {
                if (dateTime >= minCheckOut && dateTime <= maxCheckOut)
                {
                    var attendanceDetails = new MonthlyAttendanceDetailCollection();
                    attendanceDetails.Query.Where(attendanceDetails.Query.PersonID == personID &&
                        attendanceDetails.Query.PayrollPeriodID == payrollPeriodID &&
                        attendanceDetails.Query.ScheduleInDate == schCheckIn.Date &&
                        attendanceDetails.Query.ScheduleInTime == schCheckIn.ToString("HH:mm") &&
                        attendanceDetails.Query.CheckInDate == schCheckIn.Date &&
                        attendanceDetails.Query.CheckInTime.IsNotNull());
                    attendanceDetails.Query.OrderBy(attendanceDetails.Query.LastUpdateDateTime.Ascending);
                    attendanceDetails.Query.Load();
                    if (attendanceDetails.Count > 1)
                    {
                        for (int i = 1; i < attendanceDetails.Count; i++)
                        {
                            attendanceDetails[i].MarkAsDeleted();
                        }

                        attendanceDetails.Save();
                    }

                    var attendanceDetail = new MonthlyAttendanceDetail();
                    attendanceDetail.Query.Where(attendanceDetail.Query.PersonID == personID &&
                        attendanceDetail.Query.PayrollPeriodID == payrollPeriodID &&
                        attendanceDetail.Query.ScheduleInDate == schCheckIn.Date &&
                        attendanceDetail.Query.ScheduleInTime == schCheckIn.ToString("HH:mm") &&
                        attendanceDetail.Query.CheckInDate == schCheckIn.Date &&
                        attendanceDetail.Query.CheckInTime.IsNotNull());
                    if (attendanceDetail.Query.Load())
                    {
                        attendanceDetail.ScheduleOutDate = schCheckOut.Date;
                        attendanceDetail.ScheduleOutTime = schCheckOut.ToString("HH:mm");
                        attendanceDetail.CheckOutDate = dateTime.Date;
                        attendanceDetail.CheckOutTime = dateTime.ToString("HH:mm");

                        if (attendanceDetail.IsOvertime ?? false)
                        {
                            var wh = new WorkingHour();
                            wh.LoadByPrimaryKey(hour.WorkingHourID ?? -1);

                            var overtimeHour = dateTime.Subtract(dateTime.Date.Add(TimeSpan.ParseExact(wh.EndTime, "hh\\:mm", null))).TotalHours;
                            attendanceDetail.OvertimeHours = Convert.ToDecimal(Math.Round(overtimeHour, 2));
                        }

                        attendanceDetail.Save();

                        var checkIn = attendanceDetail.CheckInDate?.Date.Add(TimeSpan.ParseExact(attendanceDetail.CheckInTime, "hh\\:mm", null));

                        //var history = new MonthlyAttendanceDetailHistory();
                        //history.Query.Where(history.Query.PersonID == personID && history.Query.CheckInDateTime == checkIn && history.Query.CheckOutDateTime.IsNull());
                        //history.Query.Load();

                        //history.CheckOutDateTime = dateTime;
                        //history.Save();

                        var histories = new MonthlyAttendanceDetailHistoryCollection();
                        histories.Query.Where(histories.Query.PersonID == personID && histories.Query.CheckInDateTime == checkIn && histories.Query.CheckOutDateTime.IsNull());
                        histories.Query.Load();
                        foreach (var history in histories)
                        {
                            history.CheckOutDateTime = dateTime;
                        }
                        histories.Save();
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = $"Halo {emp.EmployeeName};anda belum absen masuk;silahkan lapor ke SDM"
                        });
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = false,
                        message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya"
                    });
                }
            }

            return string.Empty;
        }

        private string DoubleSkipSingleCrossday(DateTime dateTime, int payrollPeriodID, int personID, WorkingHour hour, bool isCheckIn)
        {
            var emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.PersonID == personID);
            emp.Query.Load();

            var minCheckIn = isCheckIn ? dateTime.Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime2, "hh\\:mm", null)) : dateTime.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.MinimumStartTime2, "hh\\:mm", null));
            var schCheckIn = isCheckIn ? dateTime.Date.Add(TimeSpan.ParseExact(hour.StartTime2, "hh\\:mm", null)) : dateTime.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.StartTime2, "hh\\:mm", null));
            var maxCheckIn = isCheckIn ? dateTime.Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime2, "hh\\:mm", null)) : dateTime.AddDays(-1).Date.Add(TimeSpan.ParseExact(hour.MaximumStartTime2, "hh\\:mm", null));

            var minCheckOut = isCheckIn ? dateTime.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime2, "hh\\:mm", null)) : dateTime.Date.Add(TimeSpan.ParseExact(hour.MinimumEndTime2, "hh\\:mm", null));
            var schCheckOut = isCheckIn ? dateTime.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.EndTime2, "hh\\:mm", null)) : dateTime.Date.Add(TimeSpan.ParseExact(hour.EndTime2, "hh\\:mm", null));
            var maxCheckOut = isCheckIn ? dateTime.AddDays(1).Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime2, "hh\\:mm", null)) : dateTime.Date.Add(TimeSpan.ParseExact(hour.MaximumEndTime2, "hh\\:mm", null));

            if (isCheckIn)
            {
                if (dateTime < minCheckIn)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = false,
                        message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya"
                    });
                }
                else if (dateTime >= minCheckIn && dateTime <= maxCheckIn)
                {
                    var attendanceDetail = new MonthlyAttendanceDetail();
                    attendanceDetail.Query.Where(attendanceDetail.Query.PersonID == personID &&
                        attendanceDetail.Query.PayrollPeriodID == payrollPeriodID &&
                        attendanceDetail.Query.ScheduleInDate == schCheckIn.Date &&
                        attendanceDetail.Query.ScheduleInTime == schCheckIn.ToString("HH:mm") &&
                        attendanceDetail.Query.CheckInDate == schCheckIn.Date &&
                        attendanceDetail.Query.CheckInTime.IsNotNull());
                    if (!attendanceDetail.Query.Load())
                    {
                        var tapDateTime = dateTime.Date.Add(TimeSpan.ParseExact(dateTime.ToString("HH:mm"), "hh\\:mm", null));

                        attendanceDetail = new MonthlyAttendanceDetail();
                        attendanceDetail.Query.Where(attendanceDetail.Query.PersonID == personID && attendanceDetail.Query.PayrollPeriodID == payrollPeriodID &&
                            attendanceDetail.Query.ScheduleInDate == schCheckIn.Date && attendanceDetail.Query.ScheduleInTime == schCheckIn.ToString("HH:mm"));
                        if (!attendanceDetail.Query.Load())
                        {
                            attendanceDetail = new MonthlyAttendanceDetail();
                            attendanceDetail.PersonID = personID;
                            attendanceDetail.PayrollPeriodID = payrollPeriodID;
                            attendanceDetail.ScheduleInDate = schCheckIn.Date;
                            attendanceDetail.ScheduleInTime = schCheckIn.ToString("HH:mm");
                            attendanceDetail.CheckInDate = tapDateTime.Date;
                            attendanceDetail.CheckInTime = tapDateTime.ToString("HH:mm");
                            attendanceDetail.LateMinutes = dateTime > schCheckIn ? Convert.ToInt16(dateTime.Subtract(schCheckIn).TotalMinutes) : Convert.ToInt16(0);
                            attendanceDetail.ScheduleOutDate = schCheckOut.Date;
                            attendanceDetail.ScheduleOutTime = schCheckOut.ToString("HH:mm");
                            //attendanceDetail.IsOvertime = hour.IsOvertimeWorkingHour ?? false;
                            //attendanceDetail.OvertimeHours = Convert.ToInt16(hour.OvertimeValueInMinutes ?? 0);

                            if (hour.IsOvertimeWorkingHour ?? false && tapDateTime >= dateTime.Date.Add(TimeSpan.ParseExact("18:00", "hh\\:mm", null))) attendanceDetail.IsOvertime = true;
                            else attendanceDetail.IsOvertime = false;

                            attendanceDetail.WorkingHourID = hour.WorkingHourID;
                            attendanceDetail.Save();
                        }

                        var history = new MonthlyAttendanceDetailHistory();
                        history.Query.Where(history.Query.PersonID == personID && history.Query.WorkingHourID == (hour.WorkingHourID ?? -1) && history.Query.CheckInDateTime == tapDateTime);
                        if (!history.Query.Load())
                        {
                            history = new MonthlyAttendanceDetailHistory();
                            //history.MonthlyAttendanceDetailID = attendanceDetail.MonthlyAttendanceDetailID;
                            history.PersonID = personID;
                            history.WorkingHourID = hour.WorkingHourID;
                            history.CheckInDateTime = tapDateTime;
                            history.Save();
                        }

                        //attendanceDetail = new MonthlyAttendanceDetail();
                        //attendanceDetail.PersonID = personID;
                        //attendanceDetail.PayrollPeriodID = payrollPeriodID;
                        //attendanceDetail.ScheduleInDate = schCheckIn.Date;
                        //attendanceDetail.ScheduleInTime = schCheckIn.ToString("HH:mm");
                        //attendanceDetail.CheckInDate = dateTime.Date;
                        //attendanceDetail.CheckInTime = dateTime.ToString("HH:mm");
                        //attendanceDetail.LateMinutes = dateTime > schCheckIn ? Convert.ToInt16(dateTime.Subtract(schCheckIn).TotalMinutes) : Convert.ToInt16(0);
                        //attendanceDetail.ScheduleOutDate = schCheckOut.Date;
                        //attendanceDetail.ScheduleOutTime = schCheckOut.ToString("HH:mm");
                        //attendanceDetail.IsOvertime = hour.IsOvertimeWorkingHour ?? false;
                        //attendanceDetail.OvertimeHours = Convert.ToInt16(hour.OvertimeValueInMinutes ?? 0);
                        //attendanceDetail.WorkingHourID = hour.WorkingHourID;
                        //attendanceDetail.Save();

                        //var history = new MonthlyAttendanceDetailHistory();
                        ////history.MonthlyAttendanceDetailID = attendanceDetail.MonthlyAttendanceDetailID;
                        //history.PersonID = personID;
                        //history.WorkingHourID = hour.WorkingHourID;
                        //history.CheckInDateTime = dateTime;
                        //history.Save();
                    }
                    else
                    {
                        var datetime = attendanceDetail.CheckInDate.Value.Date.Add(TimeSpan.ParseExact(attendanceDetail.CheckInTime, "hh\\:mm", null));
                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = $"Halo {emp.EmployeeName};anda sudah absen masuk pada {attendanceDetail.CheckInTime}"
                        });
                    }
                }
                else if (dateTime > maxCheckIn && dateTime < minCheckOut)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = false,
                        message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya"
                    });
                }
            }
            else
            {
                if (dateTime >= minCheckOut && dateTime <= maxCheckOut)
                {
                    var attendanceDetails = new MonthlyAttendanceDetailCollection();
                    attendanceDetails.Query.Where(attendanceDetails.Query.PersonID == personID &&
                        attendanceDetails.Query.PayrollPeriodID == payrollPeriodID &&
                        attendanceDetails.Query.ScheduleInDate == schCheckIn.Date &&
                        attendanceDetails.Query.ScheduleInTime == schCheckIn.ToString("HH:mm") &&
                        attendanceDetails.Query.CheckInDate == schCheckIn.Date &&
                        attendanceDetails.Query.CheckInTime.IsNotNull());
                    attendanceDetails.Query.OrderBy(attendanceDetails.Query.LastUpdateDateTime.Ascending);
                    attendanceDetails.Query.Load();
                    if (attendanceDetails.Count > 1)
                    {
                        for (int i = 1; i < attendanceDetails.Count; i++)
                        {
                            attendanceDetails[i].MarkAsDeleted();
                        }

                        attendanceDetails.Save();
                    }

                    var attendanceDetail = new MonthlyAttendanceDetail();
                    attendanceDetail.Query.Where(attendanceDetail.Query.PersonID == personID &&
                        attendanceDetail.Query.PayrollPeriodID == payrollPeriodID &&
                        attendanceDetail.Query.ScheduleInDate == schCheckIn.Date &&
                        attendanceDetail.Query.ScheduleInTime == schCheckIn.ToString("HH:mm") &&
                        attendanceDetail.Query.CheckInDate == schCheckIn.Date &&
                        attendanceDetail.Query.CheckInTime.IsNotNull());
                    if (attendanceDetail.Query.Load())
                    {
                        attendanceDetail.ScheduleOutDate = schCheckOut.Date;
                        attendanceDetail.ScheduleOutTime = schCheckOut.ToString("HH:mm");
                        attendanceDetail.CheckOutDate = dateTime.Date;
                        attendanceDetail.CheckOutTime = dateTime.ToString("HH:mm");

                        if (attendanceDetail.IsOvertime ?? false)
                        {
                            var wh = new WorkingHour();
                            wh.LoadByPrimaryKey(hour.WorkingHourID ?? -1);

                            var overtimeHour = dateTime.Subtract(attendanceDetail.ScheduleInDate.Value.Date.Add(TimeSpan.ParseExact(attendanceDetail.ScheduleInTime, "hh\\:mm", null))).TotalHours;
                            attendanceDetail.OvertimeHours = Convert.ToDecimal(Math.Round(overtimeHour, 2));
                        }

                        attendanceDetail.Save();

                        var checkIn = attendanceDetail.CheckInDate?.Date.Add(TimeSpan.ParseExact(attendanceDetail.CheckInTime, "hh\\:mm", null));

                        //var history = new MonthlyAttendanceDetailHistory();
                        //history.Query.Where(history.Query.PersonID == personID && history.Query.CheckInDateTime == checkIn && history.Query.CheckOutDateTime.IsNull());
                        //history.Query.Load();

                        //history.CheckOutDateTime = dateTime;
                        //history.Save();

                        var histories = new MonthlyAttendanceDetailHistoryCollection();
                        histories.Query.Where(histories.Query.PersonID == personID && histories.Query.CheckInDateTime == checkIn && histories.Query.CheckOutDateTime.IsNull());
                        histories.Query.Load();
                        foreach (var history in histories)
                        {
                            history.CheckOutDateTime = dateTime;
                        }
                        histories.Save();
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = $"Halo {emp.EmployeeName};anda belum absen masuk;silahkan lapor ke SDM"
                        });
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(new
                    {
                        status = false,
                        message = $"Halo {emp.EmployeeName};anda tidak bisa absen karena belum waktunya"
                    });
                }
            }

            return string.Empty;
        }

        [WebMethod]
        public string ValidateMeal(string employeeNo, string dateTime)
        {
            string format = "yyyyMMdd-HHmm";
            DateTime.TryParseExact(dateTime, format, null, DateTimeStyles.None, out var now);

            var emp = new VwEmployeeTable();
            emp.Query.Where(emp.Query.EmployeeNumber == employeeNo);
            emp.Query.Load();

            var tap = new MonthlyAttendanceDetail();
            tap.Query.Where(tap.Query.PersonID == emp.PersonID, tap.Query.CheckInDate.Date() == now.Date, tap.Query.CheckInTime.IsNotNull(), tap.Query.CheckInTime != string.Empty);
            if (!tap.Query.Load())
            {

            }

            var hour = new WorkingHour();
            hour.LoadByPrimaryKey(tap.WorkingHourID ?? -1);
            if (hour.MealQty == 0)
            {

            }

            var meal = new MealAttendance();
            meal.Query.Where(meal.Query.OpenDatetime.Date() == now.Date);
            meal.Query.Load();

            var detail = new MealAttendanceDetail();
            detail.Query.Where(detail.Query.MealAttendanceID == meal.MealAttendanceID && detail.Query.PersonID == emp.PersonID);
            if (detail.Query.Load())
            {

            }


            return string.Empty;
        }

        [WebMethod]
        public string BirthdayList()
        {
            var now = DateTime.Now.Date;

            var vw = new VwEmployeeTableQuery("a");
            var unit = new OrganizationUnitQuery("b");

            vw.Select(vw.EmployeeName, unit.OrganizationUnitName);
            vw.InnerJoin(unit).On(vw.ServiceUnitID == unit.OrganizationUnitID);
            vw.Where(vw.SREmployeeStatus == "1");
            vw.Where($"<DAY(BirthDate) = {now.Day} AND MONTH(BirthDate) = {now.Month}>");
            var table = vw.LoadDataTable();

            if (table.Rows.Count == 0)
            {
                return JsonConvert.SerializeObject(new
                {
                    status = false
                });
            }

            return JsonConvert.SerializeObject(new
            {
                status = true,
                message = table.AsEnumerable().Select(l => new { EmployeeName = l.Field<string>("EmployeeName"), OrganizationUnitName = l.Field<string>("OrganizationUnitName") })
            });
        }

        [WebMethod]
        public string ExceptionLog(string employeeNo, string dateTime, string exceptionMessage)
        {
            string format = "yyyyMMdd-HHmm";
            DateTime.TryParseExact(dateTime, format, null, DateTimeStyles.None, out var now);

            var log = new WebServiceAPILog()
            {
                DateRequest = DateTime.Now,
                IPAddress = string.Empty,
                UrlAddress = string.Empty,
                Params = JsonConvert.SerializeObject(new
                {
                    employeeNo = employeeNo,
                    dateTime = dateTime,
                    exceptionMessage = exceptionMessage
                }),
                Response = string.Empty,
                Totalms = 0
            };
            log.Save();

            return JsonConvert.SerializeObject(new
            {
                status = true,
                message = "Terjadi kesalahan;silahkan dicoba kembali atau silahkan lapor ke SDM"
            });
        }
    }
}