using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class FormsTemplateList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "FormsTemplateSearch.aspx";
            UrlPageDetail = "FormsTemplateDetail.aspx";

            ProgramID = AppConstant.Program.EmployeeFormsTemplate;

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
            string id = dataItem.GetDataKeyValue(EmployeeFormTemplateMetadata.ColumnNames.TemplateID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EmployeeFormTemplates;
        }

        private DataTable EmployeeFormTemplates
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                EmployeeFormTemplateQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EmployeeFormTemplateQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EmployeeFormTemplateQuery("a");
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