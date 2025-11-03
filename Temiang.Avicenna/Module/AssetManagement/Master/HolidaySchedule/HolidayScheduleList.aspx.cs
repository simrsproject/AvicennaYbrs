using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class HolidayScheduleList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "HolidayScheduleSearch.aspx";
            UrlPageDetail = "HolidayScheduleDetail.aspx";

            ProgramID = AppConstant.Program.AssetHolidaySchedule;

            this.WindowSearch.Height = 400;
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
            string periodYear = dataItem.GetDataKeyValue(HolidayScheduleMetadata.ColumnNames.PeriodYear).ToString();
            Page.Response.Redirect("HolidayScheduleDetail.aspx?md=" + mode + "&per=" + periodYear, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = HolidaySchedules;
        }

        private DataTable HolidaySchedules
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                HolidayScheduleQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (HolidayScheduleQuery)Session[SessionNameForQuery];
                else
                {
                    query = new HolidayScheduleQuery("a");

                    query.Select
                        (
                            query.PeriodYear
                        );

                    query.OrderBy(query.PeriodYear.Descending);
                    query.es.Distinct = true;
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}
