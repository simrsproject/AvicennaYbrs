using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CashTransactionTemplateList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "CashTransactionTemplateSearch.aspx";
            UrlPageDetail = "CashTransactionTemplateDetail.aspx";

            ProgramID = AppConstant.Program.CASH_TRANSACTION_LIST;

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
            string id = dataItem.GetDataKeyValue(CashTransactionTemplateMetadata.ColumnNames.TemplateId).ToString();
            //Page.Response.Redirect("CashTransactionListDetail.aspx?md=" + mode + "&id=" + id, true);

            string url = string.Format("{0}?md={1}&TemplateId={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CashTransactionTemplates;
        }

        private DataTable CashTransactionTemplates
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                CashTransactionTemplateQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (CashTransactionTemplateQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new CashTransactionTemplateQuery("a");
                    query.Select
                        (
                            query
                        );

                    //Quick Search
                    ApplyQuickSearch(query, "TemplateName");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }


    }
}
