using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Transaction.Attendance
{
    public partial class MonthlyAttendanceList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "MonthlyAttendanceSearch.aspx";
            UrlPageDetail = "MonthlyAttendanceItem.aspx";
            UrlPageDetailImport = "openWinImport('" + AppConstant.Program.MonthlyAttendance + "');";

            ProgramID = AppConstant.Program.MonthlyAttendance;

            WindowSearch.Height = 300;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(MonthlyAttendanceMetadata.ColumnNames.MonthlyAttendanceID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = MonthlyAttendances;
        }

        private DataTable MonthlyAttendances
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                MonthlyAttendanceQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (MonthlyAttendanceQuery)Session[SessionNameForQuery];
                }
                else
                {
                    PayrollPeriodQuery period = new PayrollPeriodQuery("c");
                    var personal = new VwEmployeeTableQuery("b");
                    query = new MonthlyAttendanceQuery("a");

                    var app = new AppStandardReferenceItemQuery("d");
                    var org = new OrganizationUnitQuery("e");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.MonthlyAttendanceID,
                                    query.PersonID,
                                    personal.EmployeeNumber,
                                    personal.EmployeeName,
                                    query.PayrollPeriodID,
                                    period.PayrollPeriodName,
                                    query.PayDays,
                                    query.UnPayDays,
                                    query.WorkingDays,
                                    query.OvertimeHours,
                                    query.ConvertOvertimeHours,
                                    query.LastUpdateDateTime,
                                    query.LastUpdateByUserID,
                                    app.ItemName,
                                    query.OvertimeDays,
                                    org.OrganizationUnitName
                                );

                    query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
                    query.InnerJoin(period).On(query.PayrollPeriodID == period.PayrollPeriodID);
                    query.LeftJoin(app).On(query.SRAttendanceFileFormat == app.ItemID && app.StandardReferenceID == AppEnum.StandardReference.AttendanceFileFormat);
                    query.LeftJoin(org).On(personal.ServiceUnitID == org.OrganizationUnitID);
                    query.OrderBy(query.MonthlyAttendanceID.Descending, query.PersonID.Ascending);

                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}