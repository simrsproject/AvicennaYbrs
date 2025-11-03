using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuVersionList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "MenuVersionSearch.aspx?ext=" + Request.QueryString["ext"];
            UrlPageDetail = "MenuVersionDetail.aspx?ext=" + Request.QueryString["ext"];

            ProgramID = Request.QueryString["ext"] == "0" ? AppConstant.Program.MenuVersion : AppConstant.Program.MenuExtraVersion;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", Request.QueryString["ext"]);
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
            string id = dataItem.GetDataKeyValue(MenuVersionMetadata.ColumnNames.VersionID).ToString();
            Page.Response.Redirect("MenuVersionDetail.aspx?md=" + mode + "&id=" + id + "&ext=" + Request.QueryString["ext"], true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = MenuVersions;
        }

        private DataTable MenuVersions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                MenuVersionQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (MenuVersionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new MenuVersionQuery();
                    query.Select
                        (
                            query.VersionID,
                            query.VersionName,
                            query.Cycle,
                            query.IsPlusOneRule,
                            query.IsActive
                        );
                    if (Request.QueryString["ext"] == "0")
                        query.Where(query.IsExtra == false);
                    else
                        query.Where(query.IsExtra == true);
                    
                    query.OrderBy(query.VersionID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "VersionName", "VersionID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
