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
    public partial class WorkingHourList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "WorkingHourSearch.aspx";
            UrlPageDetail = "WorkingHourDetail.aspx";

            WindowSearch.Height = 300;

            ProgramID = AppConstant.Program.WorkingHour;
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
            string id = dataItem.GetDataKeyValue(WorkingHourMetadata.ColumnNames.WorkingHourID).ToString();
            Page.Response.Redirect("WorkingHourDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = WorkingHours;
        }

        private DataTable WorkingHours
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                WorkingHourQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (WorkingHourQuery)Session[SessionNameForQuery];
                else
                {
                    query = new WorkingHourQuery("a");
                    var shift = new AppStandardReferenceItemQuery("b");
                    var wd = new AppStandardReferenceItemQuery("c");
                    query.LeftJoin(shift).On(query.SRShift == shift.ItemID && shift.StandardReferenceID == AppEnum.StandardReference.Shift);
                    query.LeftJoin(wd).On(query.SRWorkingDay == wd.ItemID && wd.StandardReferenceID == AppEnum.StandardReference.WorkingDay);
                    query.Select(query, @"<CASE WHEN a.SRShift = 'ShiftID-013' THEN 'Morning & Night' ELSE b.ItemName END AS ItemName>", wd.ItemName.As("WorkingDayName"));
                    query.OrderBy(query.WorkingHourID.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}