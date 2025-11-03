using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class TestResultTemplateList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "TestResultTemplateSearch.aspx";
            UrlPageDetail = "TestResultTemplateDetail.aspx";

            ProgramID = AppConstant.Program.TestResultTemplate;

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
            string id = dataItem.GetDataKeyValue(TestResultTemplateMetadata.ColumnNames.TestResultTemplateID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = TestResultTemplates;
        }

        private DataTable TestResultTemplates
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				TestResultTemplateQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (TestResultTemplateQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new TestResultTemplateQuery("a");
                    ParamedicQuery paramedicQuery = new ParamedicQuery("b");
                    query.LeftJoin(paramedicQuery).On(query.ParamedicID == paramedicQuery.ParamedicID);
                    ItemQuery itemQuery = new ItemQuery("c");
                    query.LeftJoin(itemQuery).On(query.ItemID == itemQuery.ItemID);
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.TestResultTemplateID,
                                    query.ParamedicID,
                                    paramedicQuery.ParamedicName,
                                    query.ItemID,
                                    itemQuery.ItemName,
                                    query.TestResultTemplateName,
                                    query.TestResult.Substring(100).As("TestResult")
                                );
                    //Quick Search
                    ApplyQuickSearch(query, "TestResultTemplateName", "TestResultTemplateID");
                }

				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}
