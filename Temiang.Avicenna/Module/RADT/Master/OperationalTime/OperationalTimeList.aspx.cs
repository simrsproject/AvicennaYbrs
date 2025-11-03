using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class OperationalTimeList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "OperationalTimeSearch.aspx";
            UrlPageDetail = "OperationalTimeDetail.aspx";

            ProgramID = AppConstant.Program.OperationalTime;

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
            string id = dataItem.GetDataKeyValue(OperationalTimeMetadata.ColumnNames.OperationalTimeID).ToString();
            Page.Response.Redirect("OperationalTimeDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = OperationalTimes;
        }

        private DataTable OperationalTimes
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                OperationalTimeQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (OperationalTimeQuery)Session[SessionNameForQuery];
                else
                {
                    query = new OperationalTimeQuery();
                    query.Select
                        (
                            query.OperationalTimeID, 
                            query.OperationalTimeName, 
                            query.OperationalTimeBackcolor,
                            (query.StartTime1 + " - " + query.EndTime1).As("Time1"),
                            (query.StartTime2 + " - " + query.EndTime2).As("Time2"),
                            (query.StartTime3 + " - " + query.EndTime3).As("Time3"),
                            (query.StartTime4 + " - " + query.EndTime4).As("Time4"),
                            (query.StartTime5 + " - " + query.EndTime5).As("Time5")
                        );
                    query.OrderBy(query.OperationalTimeID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "OperationalTimeName", "OperationalTimeID");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

