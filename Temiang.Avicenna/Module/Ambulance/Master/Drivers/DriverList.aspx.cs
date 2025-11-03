using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using DevExpress.XtraRichEdit.Import.Doc;

namespace Temiang.Avicenna.Module.Ambulance.Master
{
    public partial class DriverList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "DriverSearch.aspx";
            UrlPageDetail = "DriverDetail.aspx";

            ProgramID = AppConstant.Program.Driver;

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
            string id = dataItem.GetDataKeyValue(VehicleDriversMetadata.ColumnNames.DriverID).ToString();
            Page.Response.Redirect("DriverDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Drivers;
        }

        private DataTable Drivers
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                VehicleDriversQuery query;
                
                if (Session[SessionNameForQuery] != null)
                    query = (VehicleDriversQuery)Session[SessionNameForQuery];
                else
                {
                    query = new VehicleDriversQuery("a");
                    var stRef = new AppStandardReferenceItemQuery("st");
                    query.LeftJoin(stRef).On(stRef.StandardReferenceID == "DriverStatus" && stRef.ItemID == query.SRDriverStatus);
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(query.DriverID, query.DriverName, query.SRDriverStatus,
                        stRef.ItemName.As("SRDriverStatusName"),
                        query.IsActive
                        );
                    query.OrderBy(query.DriverName.Ascending);

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