using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class WorkScheduleList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "WorkScheduleSearch.aspx";
            UrlPageDetail = "WorkScheduleDetail.aspx";
            UrlPageDetailNew = string.Format("{0}?md={1}", "WorkScheduleDetail.aspx", "new");

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.EmployeeWorkSchedule; //TODO: Isi ProgramID

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;

            UrlPageDetailImport = "openWinImport('" + AppConstant.Program.EmployeeWorkSchedule + "');";

            tblExportParameter.Visible = this.IsExportAble;

            if (!IsPostBack)
                lblWarning.Visible = false;
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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(EmployeeWorkingInfoMetadata.ColumnNames.PersonID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            grdList.DataSource = EmployeeWorkingInfos;
        }

        private DataTable EmployeeWorkingInfos
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                VwEmployeeTableQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (VwEmployeeTableQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new VwEmployeeTableQuery("a");
                    var emplGrade = new PositionGradeQuery("i");
                    var position = new PositionQuery("h");
                    var unit = new OrganizationUnitQuery("g");
                    var info = new EmployeeWorkingInfoQuery("f");
                    var remuneration = new AppStandardReferenceItemQuery("e");
                    var type = new AppStandardReferenceItemQuery("d");
                    var status = new AppStandardReferenceItemQuery("c");
                    var supervisor = new VwEmployeeTableQuery("b");

                    query.LeftJoin(supervisor).On(query.SupervisorId == supervisor.PersonID);
                    query.LeftJoin(status).On
                            (
                                query.SREmployeeStatus == status.ItemID &
                                status.StandardReferenceID == AppEnum.StandardReference.EmployeeStatus
                            );
                    query.LeftJoin(type).On
                            (
                                query.SREmployeeType == type.ItemID &
                                type.StandardReferenceID == AppEnum.StandardReference.EmployeeType
                            );
                    query.LeftJoin(remuneration).On
                            (
                                query.SRRemunerationType == remuneration.ItemID &
                                remuneration.StandardReferenceID == AppEnum.StandardReference.RemunerationType
                            );
                    query.LeftJoin(info).On(query.PersonID == info.PersonID);
                    query.LeftJoin(unit).On(query.ServiceUnitID == unit.OrganizationUnitID);
                    query.LeftJoin(position).On(query.PositionID == position.PositionID);
                    query.LeftJoin(emplGrade).On(query.PositionGradeID == emplGrade.PositionGradeID);

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                   query.PersonID,
                                   query.EmployeeNumber,
                                   query.EmployeeName,
                                   supervisor.EmployeeName.As("SupervisorName"),
                                   status.ItemName.As("EmployeeStatusName"),
                                   type.ItemName.As("EmployeeTypeName"),
                                   remuneration.ItemName.As("RemunerationTypeName"),
                                   info.AbsenceCardNo,
                                   query.JoinDate,
                                   position.PositionName,
                                   emplGrade.PositionGradeName,
                                   info.LastUpdateDateTime,
                                   info.LastUpdateByUserID
                                );

                    query.Select(unit.OrganizationUnitName);
                    query.Where(query.SREmploymentType != "0", query.SREmployeeStatus == "1");

                    query.OrderBy(query.PersonID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "EmployeeName", "EmployeeNumber");
                }
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

        protected void cboPayrollPeriodID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)sender, e.Text);
        }
        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }

        public override void OnMenuExportToExcelClick(ValidateArgs args)
        {
            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
            {
                var madColl = new MonthlyAttendanceDetailCollection();
                madColl.Query.Where(madColl.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt(), madColl.Query.ScheduleInTime != "00:00");
                madColl.LoadAll();
                if (madColl.Count == 0)
                {
                    try
                    {
                        var pp = new PayrollPeriod();
                        pp.LoadByPrimaryKey(cboPayrollPeriodID.SelectedValue.ToInt());

                        var table = GetDataGridDataTable(pp.PayrollPeriodID.ToInt());
                        if (table.Rows.Count > 0)
                        {
                            var fileName = "MonthlyAttendance_" + pp.PayrollPeriodCode.ToString().Trim();

                            Common.CreateExcelFile.CreateExcelDocument(table, fileName.Replace('/', '_').Replace(".", "_").Replace(" ", "_") + ".xls", this.Response);
                        }
                    }
                    catch (Exception e)
                    {
                        var error = e.Message;
                        throw new Exception(error);
                    }
                }
            }
            args.IsCancel = true;
        }

        private DataTable GetDataGridDataTable(Int32 payrollPeriodId)
        {
            //int x = MonthlyAttendanceDetail.ProcessInsertWorkSchedule(payrollPeriodId, AppSession.UserLogin.UserID);

            var query = new VwEmployeeTableQuery("a");
            var ewsq = new MonthlyAttendanceDetailQuery("b");
            var ouq = new OrganizationUnitQuery("c");
            var suq = new OrganizationUnitQuery("d");
            var posq = new PositionQuery("e");
            query.InnerJoin(ewsq).On(ewsq.PersonID == query.PersonID);
            query.LeftJoin(ouq).On(ouq.OrganizationUnitID == query.OrganizationUnitID);
            query.LeftJoin(suq).On(suq.OrganizationUnitID == query.ServiceUnitID);
            query.LeftJoin(posq).On(posq.PositionID == query.PositionID);

            query.Select(
                ewsq.PayrollPeriodID.As("PID"),
                query.PersonID.As("PersonID"),
                query.EmployeeName.As("EmployeeName"),
                query.EmployeeNumber.As("EmployeeNo"),
                @"<ISNULL(d.OrganizationUnitName, '-') AS OrganizationUnit>",
                @"<ISNULL(e.PositionName, '-') AS Position>",
                ewsq.ScheduleInDate.As("ScheduleDate"), 
                ewsq.ScheduleInTime, 
                ewsq.ScheduleOutTime
                );
            query.Where(query.SREmployeeStatus == "1", ewsq.PayrollPeriodID == payrollPeriodId);
            query.OrderBy(query.PersonID.Ascending, ewsq.ScheduleInDate.Ascending);

            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        protected void cboPayrollPeriodID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var madColl = new MonthlyAttendanceDetailCollection();
            madColl.Query.Where(madColl.Query.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt(), madColl.Query.ScheduleInTime != "00:00");
            madColl.LoadAll();

            lblWarning.Visible = madColl.Count > 0;
        }
    }
}