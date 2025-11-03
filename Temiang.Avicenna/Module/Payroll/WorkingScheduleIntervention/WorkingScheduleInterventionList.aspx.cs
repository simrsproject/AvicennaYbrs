using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll
{
    public partial class WorkingScheduleInterventionList : BasePageList
    {
        private string RoleType
        {
            get
            {
                return !string.IsNullOrEmpty(Request.QueryString["role"]) ? Request.QueryString["role"] : string.Empty;
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", RoleType);
            return script;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "WorkingScheduleInterventionSearch.aspx?role=" + RoleType;
            UrlPageDetail = "WorkingScheduleInterventionDetail.aspx?role=" + RoleType;

            WindowSearch.Height = 300;

            ProgramID = RoleType == "admin" ? AppConstant.Program.WorkingScheduleInterventionAdmin : AppConstant.Program.WorkingScheduleIntervention;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(WorkingSchduleInterventionMetadata.ColumnNames.WorkingSchduleInterventionID).ToString();
            Page.Response.Redirect("WorkingScheduleInterventionDetail.aspx?md=" + mode + "&id=" + id + "&role=" + RoleType, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = WorkingSchedules;
        }

        private DataTable WorkingSchedules
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null) return ((DataTable)(obj));

                WorkingSchduleInterventionQuery query;
                if (Session[SessionNameForQuery] != null) query = (WorkingSchduleInterventionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new WorkingSchduleInterventionQuery("a");
                    var ws = new WorkingScheduleQuery("d");
                    var org = new OrganizationUnitQuery("b");
                    var pp = new PayrollPeriodQuery("c");
                    var user = new AppUserQuery("e");

                    query.Select(query, org.OrganizationUnitCode, org.OrganizationUnitName, pp.PayrollPeriodName, user.UserName);
                    query.InnerJoin(ws).On(query.WorkingScheduleID == ws.WorkingScheduleID);
                    query.InnerJoin(org).On(ws.OrganizationUnitID == org.OrganizationUnitID);
                    query.InnerJoin(pp).On(ws.PayrollPeriodID == pp.PayrollPeriodID);
                    query.InnerJoin(user).On(query.LastUpdateUserID == user.UserID);
                    if (RoleType != "admin")
                    {
                        var usr = new AppUser();
                        usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);

                        var emp = new VwEmployeeTable();
                        emp.Query.Where(emp.Query.PersonID == usr.PersonID);
                        emp.Query.Load();

                        var empOrgs = new EmployeeOrganizationCollection();
                        empOrgs.Query.Where(empOrgs.Query.PersonID == emp.PersonID && empOrgs.Query.ValidTo.Date() > DateTime.Now.Date);
                        if (empOrgs.Query.Load() && empOrgs.Count > 0)
                        {
                            query.Where(ws.OrganizationUnitID.In(empOrgs.Select(e => e.ServiceUnitID)));
                        }
                        //else query.Where(query.LastUpdateUserID == AppSession.UserLogin.UserID);
                    }
                    query.OrderBy(org.OrganizationUnitName.Ascending, ws.PayrollPeriodID.Descending, ws.LastUpdateDateTime.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            int id = dataItem.GetDataKeyValue("WorkingSchduleInterventionID").ToInt();

            var query = new WorkingSchduleInterventionDetailQuery("a");
            var emp = new VwEmployeeTableQuery("b");

            query.Select
                (
                    query.WorkingSchduleInterventionDetailID,
                    query.PersonID,
                    emp.EmployeeNumber,
                    emp.EmployeeName
                );
            query.InnerJoin(emp).On(emp.PersonID == query.PersonID);
            query.Where(query.WorkingSchduleInterventionID == id);
            query.OrderBy(query.WorkingSchduleInterventionDetailID.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}