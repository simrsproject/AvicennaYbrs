using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CurrencyList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "CurrencySearch.aspx";
            UrlPageDetail = "CurrencyDetail.aspx";

            WindowSearch.Height = 170;

            ProgramID = AppConstant.Program.CURRENCY;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(CurrencyRateMetadata.ColumnNames.CurrencyID).ToString();
            Page.Response.Redirect("CurrencyDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Currency;
        }

        private DataTable Currency
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CurrencyRateQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CurrencyRateQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CurrencyRateQuery();
                    query.OrderBy(query.CurrencyID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
