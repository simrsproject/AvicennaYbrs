using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "MenuSearch.aspx";
            UrlPageDetail = "MenuDetail.aspx";

            ProgramID = AppConstant.Program.Menu;

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
            string id = dataItem.GetDataKeyValue(MenuMetadata.ColumnNames.MenuID).ToString();
            Page.Response.Redirect("MenuDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Menus;
        }

        private DataTable Menus
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                MenuQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (MenuQuery)Session[SessionNameForQuery];
                else
                {
                    query = new MenuQuery();
                    query.Select
                        (
                            query.MenuID,
                            query.MenuName,
                            query.IsExtra,
                            query.IsActive
                        );
                    query.OrderBy(query.MenuID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "MenuName", "MenuID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
