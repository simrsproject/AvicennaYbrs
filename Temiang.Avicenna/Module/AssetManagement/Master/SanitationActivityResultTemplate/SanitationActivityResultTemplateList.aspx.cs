using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class SanitationActivityResultTemplateList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SanitationActivityResultTemplateSearch.aspx";
            UrlPageDetail = "SanitationActivityResultTemplateDetail.aspx";

            ProgramID = AppConstant.Program.SanitationActivityResultTemplate;

            this.WindowSearch.Height = 400;

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
            string id = dataItem.GetDataKeyValue(SanitationActivityResultTemplateMetadata.ColumnNames.SanitationActivityResultID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = SanitationActivityResultTemplates;
        }

        private DataTable SanitationActivityResultTemplates
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                SanitationActivityResultTemplateQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (SanitationActivityResultTemplateQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new SanitationActivityResultTemplateQuery("a");
                    var wti = new AppStandardReferenceItemQuery("b");
                    query.LeftJoin(wti).On(wti.StandardReferenceID == "WorkTradeItem" && wti.ItemID == query.SRWorkTradeItem && wti.ReferenceID == AppSession.Parameter.WorkTradeSanitation);
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.SanitationActivityResultID,
                                    query.SRWorkTradeItem,
                                    wti.ItemName.As("WorkTradeItemName"),
                                    query.ResultTemplateName,
                                    query.Result.Substring(100).As("TestResult")
                                );
                    //Quick Search
                    ApplyQuickSearch(query, "ResultTemplateName", "SanitationActivityResultID");
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}