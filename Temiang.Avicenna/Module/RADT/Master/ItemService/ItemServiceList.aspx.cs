using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemServiceList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "ItemServiceSearch.aspx";
            UrlPageDetail = "ItemServiceDetail.aspx";

            ProgramID = AppConstant.Program.ServiceItem;

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
            string id = dataItem.GetDataKeyValue(ItemMetadata.ColumnNames.ItemID).ToString();
            Page.Response.Redirect("ItemServiceDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemServices;
        }

        private DataTable ItemServices
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ItemQuery query;
                
                if (Session[SessionNameForQuery] != null)
                    query = (ItemQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ItemQuery("a");
                    var serv = new ItemServiceQuery("b");
                    var grp = new ItemGroupQuery("c");
                    query.InnerJoin(serv).On(query.ItemID == serv.ItemID);
                    query.InnerJoin(grp).On(query.ItemGroupID == grp.ItemGroupID);

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(query.ItemID,
                                    grp.ItemGroupName,
                                    query.ItemName,
                                    query.IsActive,
                                    query.Notes,
                                    serv.SRItemUnit);
                    query.Where(query.SRItemType == BusinessObject.Reference.ItemType.Service);
                    query.OrderBy(query.ItemGroupID.Ascending, query.ItemID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ItemName", "ItemID");
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}