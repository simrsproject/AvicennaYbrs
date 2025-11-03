using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ConsumeMethodList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ConsumeMethodSearch.aspx";
            UrlPageDetail = "ConsumeMethodDetail.aspx";

            ProgramID = AppConstant.Program.ConsumeMethod;

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
            string id = dataItem.GetDataKeyValue(ConsumeMethodMetadata.ColumnNames.SRConsumeMethod).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ConsumeMethods;
        }

        private DataTable ConsumeMethods
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ConsumeMethodQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ConsumeMethodQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ConsumeMethodQuery();
                    //Quick Search
                    ApplyQuickSearch(query);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select(query.SRConsumeMethod, query.SRConsumeMethodName, query.TimeSequence, query.SygnaText, query.LineNumber, query.IsActive);
                query.OrderBy(query.SRConsumeMethod.Ascending);
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
