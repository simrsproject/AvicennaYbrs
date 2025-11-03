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
    public partial class WorkingScheduleList : BasePageList
    {
        private string RoleType
        {
            get
            {
                return !string.IsNullOrEmpty(Request.QueryString["role"]) ? Request.QueryString["role"] : string.Empty;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "WorkingScheduleSearch.aspx?role=" + RoleType;
            UrlPageDetail = "WorkingScheduleDetail.aspx?role=" + RoleType;
            //UrlPageDetailNew = "WorkingScheduleDetail.aspx?md=new&role=" + RoleType;

            WindowSearch.Height = 300;

            ProgramID = RoleType == "admin" ? AppConstant.Program.WorkingScheduleAdmin : AppConstant.Program.WorkingSchedule;
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", RoleType);
            return script;
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
            string id = dataItem.GetDataKeyValue(WorkingScheduleMetadata.ColumnNames.WorkingScheduleID).ToString();
            Page.Response.Redirect("WorkingScheduleDetail.aspx?md=" + mode + "&id=" + id + "&role=" + RoleType, true);
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

                WorkingScheduleQuery query;
                if (Session[SessionNameForQuery] != null) 
                    query = (WorkingScheduleQuery)Session[SessionNameForQuery];
                else
                {
                    query = new WorkingScheduleQuery("a");
                    var org = new OrganizationUnitQuery("b");
                    var pp = new PayrollPeriodQuery("c");
                    var user = new AppUserQuery("d");

                    query.Select(query, org.OrganizationUnitCode, org.OrganizationUnitName, pp.PayrollPeriodName, user.UserName);
                    query.InnerJoin(org).On(query.OrganizationUnitID == org.OrganizationUnitID);
                    query.InnerJoin(pp).On(query.PayrollPeriodID == pp.PayrollPeriodID);
                    query.InnerJoin(user).On(query.LastUpdateUserID == user.UserID);
                    if (RoleType != "admin") 
                        query.Where(query.LastUpdateUserID == AppSession.UserLogin.UserID);
                    query.OrderBy(org.OrganizationUnitName.Ascending, query.PayrollPeriodID.Descending, query.LastUpdateDateTime.Descending);
                }
                
                //query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            int id = dataItem.GetDataKeyValue("WorkingScheduleID").ToInt();

            var query = new WorkingScheduleDetailQuery("a");
            var emp = new VwEmployeeTableQuery("b");

            query.Select
                (
                    query.WorkingScheduleDetailID,
                    query.PersonID,
                    emp.EmployeeNumber,
                    emp.EmployeeName
                );
            query.InnerJoin(emp).On(emp.PersonID == query.PersonID);
            query.Where(query.WorkingScheduleID == id);
            query.OrderBy(query.WorkingScheduleDetailID.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}