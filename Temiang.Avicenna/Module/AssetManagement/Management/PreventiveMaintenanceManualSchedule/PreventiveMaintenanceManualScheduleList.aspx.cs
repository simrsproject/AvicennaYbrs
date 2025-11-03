using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.AssetManagement
{
    public partial class PreventiveMaintenanceManualScheduleList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "PreventiveMaintenanceManualScheduleSearch.aspx";
            UrlPageDetail = "PreventiveMaintenanceManualScheduleDetail.aspx";

            ProgramID = AppConstant.Program.AssetPreventiveMaintenanceManualSchedule;

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
            string assetId = dataItem.GetDataKeyValue(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.AssetID).ToString();
            string periodYear = dataItem.GetDataKeyValue(AssetPreventiveMaintenanceSchedulePeriodMetadata.ColumnNames.PeriodYear).ToString();
            Page.Response.Redirect("PreventiveMaintenanceManualScheduleDetail.aspx?md=" + mode + "&aid=" + assetId + "&per=" + periodYear, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AssetPreventiveMaintenanceSchedulePeriods;
        }

        private DataTable AssetPreventiveMaintenanceSchedulePeriods
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AssetPreventiveMaintenanceSchedulePeriodQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AssetPreventiveMaintenanceSchedulePeriodQuery)Session[SessionNameForQuery];
                else
                {
                    query = new AssetPreventiveMaintenanceSchedulePeriodQuery("a");
                    var asset = new AssetQuery("b");
                    var unit = new ServiceUnitQuery("c");
                    var usr = new AppUserServiceUnitQuery("d");
                    var tounit = new ServiceUnitQuery("e");

                    query.Select
                        (
                            query.AssetID,
                            query.PeriodYear,
                            asset.AssetName,
                            asset.BrandName,
                            asset.SerialNumber,
                            unit.ServiceUnitName,
                            tounit.ServiceUnitName.As("ToServiceUnitName")
                        );
                    query.InnerJoin(asset).On(query.AssetID == asset.AssetID);
                    query.InnerJoin(unit).On(asset.ServiceUnitID == unit.ServiceUnitID);
                    query.InnerJoin(tounit).On(asset.MaintenanceServiceUnitID == tounit.ServiceUnitID);
                    query.InnerJoin(usr).On(asset.MaintenanceServiceUnitID == usr.ServiceUnitID &&
                                    usr.UserID == AppSession.UserLogin.UserID);

                    query.OrderBy(query.PeriodYear.Ascending, asset.AssetName.Ascending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}
