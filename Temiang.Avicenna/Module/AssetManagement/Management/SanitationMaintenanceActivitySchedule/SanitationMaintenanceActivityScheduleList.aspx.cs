using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationMaintenanceActivityScheduleList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "SanitationMaintenanceActivityScheduleSearch.aspx";
            UrlPageDetail = "SanitationMaintenanceActivityScheduleDetail.aspx";

            ProgramID = AppConstant.Program.SanitationMaintenanceActivitySchedule;

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
            string srWorkTradeItem = dataItem.GetDataKeyValue(SanitationMaintenanceActivitySchedulePeriodMetadata.ColumnNames.SRWorkTradeItem).ToString();
            string serviceUnitId = dataItem.GetDataKeyValue(SanitationMaintenanceActivitySchedulePeriodMetadata.ColumnNames.ServiceUnitID).ToString();
            string periodYear = dataItem.GetDataKeyValue(SanitationMaintenanceActivitySchedulePeriodMetadata.ColumnNames.PeriodYear).ToString();
            Page.Response.Redirect("SanitationMaintenanceActivityScheduleDetail.aspx?md=" + mode + "&aid=" + srWorkTradeItem + "&uid=" + serviceUnitId + "&per=" + periodYear, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = SanitationMaintenanceActivitySchedulePeriods;
        }

        private DataTable SanitationMaintenanceActivitySchedulePeriods
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                SanitationMaintenanceActivitySchedulePeriodQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (SanitationMaintenanceActivitySchedulePeriodQuery)Session[SessionNameForQuery];
                else
                {
                    query = new SanitationMaintenanceActivitySchedulePeriodQuery("a");
                    var wti = new AppStandardReferenceItemQuery("b");
                    var unit = new ServiceUnitQuery("c");
                    
                    query.Select
                        (
                            query.SRWorkTradeItem,
                            query.ServiceUnitID,
                            query.PeriodYear,
                            wti.ItemName.As("WorkTradeItemName"),
                            unit.ServiceUnitName
                        );
                    query.InnerJoin(wti).On(wti.StandardReferenceID == "WorkTradeItem" && wti.ItemID == query.SRWorkTradeItem);
                    query.InnerJoin(unit).On(unit.ServiceUnitID == query.ServiceUnitID);

                    query.OrderBy(query.PeriodYear.Descending, query.ServiceUnitID.Ascending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}