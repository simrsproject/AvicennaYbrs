using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ZatActiveList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ZatActiveSearch.aspx";
            UrlPageDetail = "ZatActiveDetail.aspx";

            ProgramID = AppConstant.Program.ZatActive;

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
            string id = dataItem.GetDataKeyValue(ZatActiveMetadata.ColumnNames.ZatActiveID).ToString();
            Page.Response.Redirect("ZatActiveDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ZatActives;
        }

        private DataTable ZatActives
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ZatActiveQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ZatActiveQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ZatActiveQuery("a");
                    var stdi = new AppStandardReferenceItemQuery("stdi");
                    query.LeftJoin(stdi).On(query.SRZatActiveGroup==stdi.ItemID & stdi.StandardReferenceID=="ZatActiveGroup");
                    query.Select(query, stdi.ItemName.As("ZatActiveGroupName"));
                    query.OrderBy(query.ZatActiveID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }
                
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
