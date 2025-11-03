using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemPackageList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "ItemPackageSearch.aspx";
            UrlPageDetail = "ItemPackageDetail.aspx";

            ProgramID = AppConstant.Program.PackageItem;

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
            Page.Response.Redirect("ItemPackageDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemPackages;
        }

        private DataTable ItemPackages
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
                    ItemGroupQuery grp = new ItemGroupQuery("c");
                    query = new ItemQuery("a");
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Where(query.SRItemType == BusinessObject.Reference.ItemType.Package);
                    query.LeftJoin(grp).On(query.ItemGroupID == grp.ItemGroupID);
                    query.Select(query.ItemID,
                                    grp.ItemGroupName,
                                    query.ItemName,
                                    query.IsActive,
                                    query.Notes);
                    query.OrderBy(query.ItemID.Ascending);

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