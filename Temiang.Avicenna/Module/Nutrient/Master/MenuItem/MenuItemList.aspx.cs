using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuItemList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "MenuItemSearch.aspx?ext=" + Request.QueryString["ext"];
            UrlPageDetail = "MenuItemDetail.aspx?ext=" + Request.QueryString["ext"];

            ProgramID = Request.QueryString["ext"] == "0" ? AppConstant.Program.MenuItem : AppConstant.Program.MenuExtraItem;
            
            this.WindowSearch.Height = 400;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoNewUrl('{0}'); args.set_cancel(true);", Request.QueryString["ext"]);
            return script;
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
            string id = dataItem.GetDataKeyValue(MenuItemMetadata.ColumnNames.MenuItemID).ToString();
            Page.Response.Redirect("MenuItemDetail.aspx?md=" + mode + "&id=" + id + "&ext=" + Request.QueryString["ext"], true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = MenuItems;
        }

        private DataTable MenuItems
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                MenuItemQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (MenuItemQuery)Session[SessionNameForQuery];
                else
                {
                    query = new MenuItemQuery("a");
                    var menu = new MenuQuery("b");
                    var version = new MenuVersionQuery("c");
                    var cls = new ClassQuery("d");
                    
                    query.InnerJoin(menu).On(query.MenuID == menu.MenuID);
                    query.InnerJoin(version).On(query.VersionID == version.VersionID);
                    query.InnerJoin(cls).On(query.ClassID == cls.ClassID);
                    
                    query.Select
                        (
                            query.MenuItemID,
                            query.MenuItemName,
                            menu.MenuName,
                            version.VersionName,
                            query.SeqNo,
                            query.ClassID,
                            cls.ClassName,
                            query.IsActive
                        );
                    if (Request.QueryString["ext"] == "0")
                        query.Where(menu.IsExtra == false);
                    else
                        query.Where(menu.IsExtra == true);

                    query.OrderBy(query.MenuID.Ascending, query.MenuItemID.Ascending);

                    ApplyQuickSearch(query, "MenuItemName", "MenuItemID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
