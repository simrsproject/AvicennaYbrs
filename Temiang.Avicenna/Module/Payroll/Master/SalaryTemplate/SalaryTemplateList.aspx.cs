using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryTemplateList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SalaryTemplateSearch.aspx";
            UrlPageDetail = "SalaryTemplateDetail.aspx";

            ProgramID = AppConstant.Program.SalaryTemplate; //TODO: Isi ProgramID

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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(SalaryTemplateMetadata.ColumnNames.SalaryTemplateID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = SalaryTemplates;
        }

        private DataTable SalaryTemplates
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				SalaryTemplateQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (SalaryTemplateQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new SalaryTemplateQuery();
                    //Quick Search
                    ApplyQuickSearch(query);
                }
				query.es.Top = AppSession.Parameter.MaxResultRecord;
				query.Select(
                				query.SalaryTemplateID,
                				query.SalaryTemplateName,
                				query.IsActive,
                				query.LastUpdateDateTime,
                				query.LastUpdateByUserID
							);
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

