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

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class ThrScheduleList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ThrScheduleSearch.aspx";
            UrlPageDetail = "ThrScheduleDetail.aspx";

            WindowSearch.Height = 170;

            ProgramID = AppConstant.Program.ThrSchedule;
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
            string id = dataItem.GetDataKeyValue(ThrScheduleMetadata.ColumnNames.CounterID).ToString();
            Page.Response.Redirect("ThrScheduleDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ThrSchedules;
        }

        private DataTable ThrSchedules
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ThrScheduleQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ThrScheduleQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ThrScheduleQuery("a");
                    var pp = new PayrollPeriodQuery("b");
                    query.InnerJoin(pp).On(query.PayrollPeriodID == pp.PayrollPeriodID);
                    query.Select(query.CounterID, pp.PayrollPeriodCode, query.PayrollPeriodName, query.PayDate, query.SPTYear, query.LastUpdateDateTime, query.LastUpdateByUserID);
                    query.OrderBy(pp.PayrollPeriodCode.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
