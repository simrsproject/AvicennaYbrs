using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuItemExtraList : BasePageList
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ToolBarMenuQuickSearch.Enabled = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 240;

            UrlPageSearch = "MenuItemExtraSearch.aspx";
            UrlPageDetail = "MenuItemExtraDetail.aspx";

            ProgramID = AppConstant.Program.MenuExtraItemFood;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string modus)
        {
            string id = dataItem.GetDataKeyValue(MenuItemExtraMetadata.ColumnNames.SeqNo).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, modus, id);
            Page.Response.Redirect(url, true);
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = MenuItemExtras;
        }

        private DataTable MenuItemExtras
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                MenuItemExtraQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (MenuItemExtraQuery)Session[SessionNameForQuery];
                else
                {
                    query = new MenuItemExtraQuery("a");
                    var menuQ = new MenuQuery("b");
                    var msQ = new AppStandardReferenceItemQuery("c");
                    query.InnerJoin(menuQ).On(query.MenuID == menuQ.MenuID);
                    query.InnerJoin(msQ).On(query.SRMealSet == msQ.ItemID &&
                                            msQ.StandardReferenceID == AppEnum.StandardReference.MealSet);
                    query.Select
                        (
                            query.SeqNo,
                            query.MenuID,
                            menuQ.MenuName,
                            query.StartingDate,
                            msQ.ItemName.As("MealSet")
                        );
                    query.OrderBy(query.SeqNo.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}
