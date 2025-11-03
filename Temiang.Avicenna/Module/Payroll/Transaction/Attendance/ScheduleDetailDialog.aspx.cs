using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class ScheduleDetailDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AttendanceOutstandingList;
        }

        protected void grdList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var schd = new WorkingScheduleDetailQuery("a");
            var schh = new WorkingScheduleQuery("b");

            schd.es.Top = 1;
            schd.Select(
                schh.WorkingScheduleID,
                schd.WorkingScheduleDetailID,
                "<'Schedule' AS [Type]>",
                $"<a.WorkingHourIDDay{Request.QueryString["day"]} AS WorkingHourID>",
                "<'' AS WorkingHourName>",
                schd.LastUpdateDateTime,
                schd.LastUpdateUserID
            );
            schd.InnerJoin(schh).On(schd.WorkingScheduleID == schh.WorkingScheduleID);
            schd.Where(schh.PayrollPeriodID == Request.QueryString["period"].ToInt(), schh.IsApproved == true, schd.PersonID == Request.QueryString["person"].ToInt());
            schd.OrderBy(schd.LastUpdateDateTime.Descending);
            var table = schd.LoadDataTable();

            var period = new PayrollPeriod();
            period.LoadByPrimaryKey(Request.QueryString["period"].ToInt());
            var date = new DateTime(period.SPTYear ?? 0, period.SPTMonth ?? 0, Request.QueryString["day"].ToInt());
            var maxDate = new DateTime(period.SPTYear ?? 0, period.SPTMonth ?? 0, Request.QueryString["day"].ToInt()).AddMonths(1);

            if (table.Rows.Count > 0)
            {
                var wsid = new WorkingSchduleInterventionDetailQuery("a");
                var wsih = new WorkingSchduleInterventionQuery("b");

                wsid.Select(
                    wsih.WorkingSchduleInterventionID.As("WorkingScheduleID"),
                    wsid.WorkingSchduleInterventionDetailID.As("WorkingScheduleDetailID"),
                    "<'Intervention' AS [Type]>",
                    $"<a.WorkingHourIDDay{Request.QueryString["day"]} AS WorkingHourID>",
                    "<'' AS WorkingHourName>",
                    wsid.LastUpdateDateTime,
                    wsid.LastUpdateUserID
                );
                wsid.InnerJoin(wsih).On(wsid.WorkingSchduleInterventionID == wsih.WorkingSchduleInterventionID);
                wsid.Where(wsih.IsApproved == true, wsid.PersonID == Request.QueryString["person"].ToInt(), wsih.WorkingScheduleID == table.Rows[0]["WorkingScheduleID"].ToInt());
                wsid.Where($"<a.WorkingHourIDDay{Request.QueryString["day"]} IS NOT NULL>");
                wsid.OrderBy(wsid.LastUpdateDateTime.Descending);

                table.Merge(wsid.LoadDataTable());

                foreach (DataRow row in table.Rows)
                {
                    var wh = new WorkingHour();
                    wh.LoadByPrimaryKey(Convert.ToInt32(row["WorkingHourID"]));
                    row["WorkingHourName"] = wh.WorkingHourName;
                }
                table.AcceptChanges();
            }

            grdList.DataSource = table;
        }
    }
}