using System;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MonthlyAttendanceDetailSummary
    {
        public string WorkingHourName
        {
            get { return GetColumn("refToWorkingHour_WorkingHourName").ToString(); }
            set { SetColumn("refToWorkingHour_WorkingHourName", value); }
        }

        public DateTime ScheduleInDate
        {
            get { return Convert.ToDateTime(GetColumn("refToMonthlyAttendanceDetail_ScheduleInDate")); }
            set { SetColumn("refToMonthlyAttendanceDetail_ScheduleInDate", value); }
        }

        public string ScheduleInTime
        {
            get { return GetColumn("refToMonthlyAttendanceDetail_ScheduleInTime").ToString(); }
            set { SetColumn("refToMonthlyAttendanceDetail_ScheduleInTime", value); }
        }

        public DateTime ScheduleOutDate
        {
            get { return Convert.ToDateTime(GetColumn("refToMonthlyAttendanceDetail_ScheduleOutDate")); }
            set { SetColumn("refToMonthlyAttendanceDetail_ScheduleOutDate", value); }
        }

        public string ScheduleOutTime
        {
            get { return GetColumn("refToMonthlyAttendanceDetail_ScheduleOutTime").ToString(); }
            set { SetColumn("refToMonthlyAttendanceDetail_ScheduleOutTime", value); }
        }

        public static int RecalculateSummaryValue(Int64 id)
        {
            esParameters prms = new esParameters();

            prms.Add("p_MonthlyAttendanceDetailID", id, esParameterDirection.Input, DbType.Int64, 0);
            prms.Add("Return_Value", esParameterDirection.ReturnValue);

            var entity = new MonthlyAttendanceDetailSummary();
            entity.es.Connection.CommandTimeout = 60 * 5; //5 mins 
            entity.ExecuteNonQuery(esQueryType.StoredProcedure, "sp_MonthlyAttendanceDetailSummary_RecalculateValue", prms);
            return (int)prms["Return_Value"].Value;
        }
    }
}
