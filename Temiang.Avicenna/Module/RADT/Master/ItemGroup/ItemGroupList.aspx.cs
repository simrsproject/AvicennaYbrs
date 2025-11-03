using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemGroupList : BasePageList
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ItemGroupSearch.aspx?type=" + FormType;
            UrlPageDetail = "ItemGroupDetail.aspx?type=" + FormType;

            switch (FormType)
            {
                case "":
                case "service":
                    ProgramID = AppConstant.Program.GroupItem;
                    break;

                case "product":
                    ProgramID = AppConstant.Program.GroupItemProduct;
                    break;
            }
            
            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", FormType);
            return script;
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
            string id = dataItem.GetDataKeyValue(ItemGroupMetadata.ColumnNames.ItemGroupID).ToString();
            Page.Response.Redirect("ItemGroupDetail.aspx?md=" + mode + "&id=" + id + "&type=" + FormType, true);
        }	

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemGroups;
        }

        private DataTable ItemGroups
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ItemGroupQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ItemGroupQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ItemGroupQuery("a");
                    var it = new AppStandardReferenceItemQuery("b");
                    query.InnerJoin(it).On(query.SRItemType == it.ItemID && it.StandardReferenceID == "ItemType");
                    query.Select(query.ItemGroupID, query.ItemGroupName, query.Initial, 
                        @"<a.SRItemType + ' - ' + b.ItemName AS 'ItemType'>",
                        it.ItemName, query.IsActive, query.CssClass);
                    if (FormType == "service")
                        query.Where(query.SRItemType.In("01", "31", "41", "61"));
                    else if (FormType == "product")
                        query.Where(query.SRItemType.In("11", "21", "81"));
                    query.OrderBy(query.ItemGroupID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ItemGroupName", "ItemGroupID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

