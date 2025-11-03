using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class DepartmentList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "DepartmentSearch.aspx";
            UrlPageDetail = "DepartmentDetail.aspx";

            ProgramID = AppConstant.Program.Department;
            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(DepartmentMetadata.ColumnNames.DepartmentID).ToString();
            Page.Response.Redirect("DepartmentDetail.aspx?md=" + mode + "&id=" + id, true);
        }
        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Departments;
        }

        private DataTable Departments
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                DepartmentQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (DepartmentQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new DepartmentQuery();
                    query.OrderBy(query.DepartmentID.Ascending);
                    //Quick Search
                    ApplyQuickSearch(query);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

