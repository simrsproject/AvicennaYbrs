using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using DevExpress.XtraRichEdit.Import.Doc;

namespace Temiang.Avicenna.Module.Ambulance.Master
{
    public partial class VehicleList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "VehicleSearch.aspx";
            UrlPageDetail = "VehicleDetail.aspx";

            ProgramID = AppConstant.Program.Vehicle;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(VehiclesMetadata.ColumnNames.VehicleID).ToString();
            Page.Response.Redirect("VehicleDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Vehicles;
        }

        private DataTable Vehicles
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                VehiclesQuery query;
                
                if (Session[SessionNameForQuery] != null)
                    query = (VehiclesQuery)Session[SessionNameForQuery];
                else
                {
                    query = new VehiclesQuery("a");
                    var stVT = new AppStandardReferenceItemQuery("stVT");
                    var stVS = new AppStandardReferenceItemQuery("stVS");
                    var ast = new AssetQuery("ast");
                    query.LeftJoin(stVT).On(stVT.StandardReferenceID == "VehicleType" && stVT.ItemID == query.SRVehicleType)
                        .LeftJoin(stVS).On(stVS.StandardReferenceID == "VehicleStatus" && stVS.ItemID == query.SRVehicleStatus)
                        .LeftJoin(ast).On(query.AssetID == ast.AssetID);
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(query.VehicleID, query.PlateNo, query.SRVehicleType, query.SRVehicleStatus,
                        stVT.ItemName.As("SRVehicleTypeName"),
                        stVS.ItemName.As("SRVehicleStatusName"),
                        query.IsActive
                        );
                    query.OrderBy(query.PlateNo.Ascending);

                    //Quick Search
                    //ApplyQuickSearch(query, "PlatNo");
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}