using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class LocationList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "LocationSearch.aspx";
            UrlPageDetail = "LocationDetail.aspx";

            ProgramID = AppConstant.Program.Location;

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
            string id = dataItem.GetDataKeyValue(LocationMetadata.ColumnNames.LocationID).ToString();
            Page.Response.Redirect("LocationDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Locations;
        }

        private DataTable Locations
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                LocationQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (LocationQuery)Session[SessionNameForQuery];
                else
                {
                    query = new LocationQuery("a");
                    var sg = new AppStandardReferenceItemQuery("b");
                    query.LeftJoin(sg).On(sg.StandardReferenceID == "StockGroup" && sg.ItemID == query.SRStockGroup);

                    query.Select(
                        query.LocationID,
                        query.LocationName,
                        query.ShortName,
                        query.IsHoldForTransaction,
                        query.IsActive,
                        query.IsConsignment,
                        query.SRStockGroup,
                        sg.ItemName.As("StockGroupName")
                        );
                    query.OrderBy(query.LocationID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "LocationName", "LocationID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
