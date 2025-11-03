using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class MenuInitializationList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "MenuInitializationSearch.aspx?ext=" + Request.QueryString["ext"];
            UrlPageDetail = "MenuInitializationDetail.aspx?ext=" + Request.QueryString["ext"];

            ProgramID = Request.QueryString["ext"] == "0" ? AppConstant.Program.MenuInitialization : AppConstant.Program.MenuExtraInitialization;
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
            string id = dataItem.GetDataKeyValue(MenuSettingMetadata.ColumnNames.StartingDate).ToString();
            Page.Response.Redirect("MenuInitializationDetail.aspx?md=" + mode + "&id=" + id + "&ext=" + Request.QueryString["ext"], true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = MenuSettingss;
        }

        private DataTable MenuSettingss
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                MenuSettingQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (MenuSettingQuery)Session[SessionNameForQuery];
                else
                {
                    query = new MenuSettingQuery("a");
                    var version = new MenuVersionQuery("b");
                    query.InnerJoin(version).On(query.VersionID == version.VersionID);
                    query.Select
                        (
                            query.StartingDate,
                            query.VersionID,
                            version.VersionName,
                            query.SeqNo
                        );
                    if (Request.QueryString["ext"] == "0")
                        query.Where(query.IsExtra == false);
                    else
                        query.Where(query.IsExtra == true);
                    
                    query.OrderBy(query.StartingDate.Descending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
