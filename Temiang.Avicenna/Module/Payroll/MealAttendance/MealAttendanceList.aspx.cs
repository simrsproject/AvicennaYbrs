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
    public partial class MealAttendanceList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "MealAttendanceSearch.aspx";
            UrlPageDetail = "MealAttendanceDetail.aspx";

            WindowSearch.Height = 170;

            ProgramID = AppConstant.Program.WorkingSchedule;
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
            string id = dataItem.GetDataKeyValue(MealAttendanceMetadata.ColumnNames.MealAttendanceID).ToString();
            Page.Response.Redirect("MealAttendanceDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = MealAttendances;
        }

        private DataTable MealAttendances
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null) return ((DataTable)(obj));

                MealAttendanceQuery query;
                if (Session[SessionNameForQuery] != null) query = (MealAttendanceQuery)Session[SessionNameForQuery];
                else
                {
                    query = new MealAttendanceQuery("a");
                    query.Select(
                        query,
                        "<CASE WHEN a.Status = 1 THEN 'Open' ELSE 'Close' END AS StatusName>"
                        );
                    query.OrderBy(query.OpenDatetime.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}