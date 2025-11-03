using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CashTransactionListList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "CashTransactionListSearch.aspx";
            UrlPageDetail = "CashTransactionListDetail.aspx";

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
            string id = dataItem.GetDataKeyValue(CashTransactionListMetadata.ColumnNames.ListId).ToString();
            //Page.Response.Redirect("CashTransactionListDetail.aspx?md=" + mode + "&id=" + id, true);

            string url = string.Format("{0}?md={1}&vt={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CashTransactionLists;
        }

        private DataTable CashTransactionLists
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                CashTransactionListQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (CashTransactionListQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new CashTransactionListQuery("a");
                    var b = new ChartOfAccountsQuery("b");
                    var c = new SubLedgersQuery("c");
                    var d = new AppStandardReferenceItemQuery("d");
                    query.Select
                        (
                            query.ListId,
                            query.Description,
                            query.CashType,
                            query.ChartOfAccountId,
                            query.SubledgerId,
                            b.ChartOfAccountCode,
                            b.ChartOfAccountName,
                            "<ISNULL(c.SubLedgerName,'-') AS SubLedgerName>",
                            d.ItemName.As("CashManagementType")
                        );
                    query.LeftJoin(b).On(query.ChartOfAccountId == b.ChartOfAccountId);
                    query.LeftJoin(c).On(c.SubLedgerId == query.SubledgerId );
                    query.InnerJoin(d).On(query.CashType == d.ItemID && d.StandardReferenceID == AppEnum.StandardReference.CashManagementType);

                    //Quick Search
                    ApplyQuickSearch(query, "Description", "ListId");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }


    }
}
