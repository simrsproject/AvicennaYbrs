using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class FabricList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "FabricSearch.aspx";
            UrlPageDetail = "FabricDetail.aspx";

            ProgramID = AppConstant.Program.Fabric;

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
            string id = dataItem.GetDataKeyValue(FabricMetadata.ColumnNames.FabricID).ToString();
            Page.Response.Redirect("FabricDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Fabrics;
        }

        private DataTable Fabrics
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                FabricQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (FabricQuery)Session[SessionNameForQuery];
                else
                {
                    query = new FabricQuery();
                    query.OrderBy(query.FabricID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }
                
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
