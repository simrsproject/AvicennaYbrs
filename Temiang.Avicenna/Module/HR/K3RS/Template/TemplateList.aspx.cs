using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class TemplateList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "TemplateSearch.aspx";
            UrlPageDetail = "TemplateDetail.aspx";

            ProgramID = AppConstant.Program.K3RS_FormTemplate;

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
            string id = dataItem.GetDataKeyValue(K3rsFormTemplateMetadata.ColumnNames.TemplateID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = K3rsFormTemplates;
        }

        private DataTable K3rsFormTemplates
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                K3rsFormTemplateQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (K3rsFormTemplateQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new K3rsFormTemplateQuery("a");
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.TemplateID,
                                    query.TemplateName,
                                    query.Result.Substring(100).As("Result")
                                );
                    //Quick Search
                    ApplyQuickSearch(query, "TemplateName", "TemplateID");
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}