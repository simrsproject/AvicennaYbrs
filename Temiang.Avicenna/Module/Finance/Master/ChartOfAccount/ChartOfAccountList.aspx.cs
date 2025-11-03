using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ChartOfAccountList : BasePageList
    {
        private void GenerateGrid()
        {
            string chartOfAccountCode = string.Empty;
            string chartOfAccountName = string.Empty;
            string accountLevel = string.Empty;
            string generalAccount = string.Empty;
            string bkuAccountCode = string.Empty;

            // set search value here..
            if (Session[SessionNameForQuery] != null)
            {
                ChartOfAccountSearch.SearchValue sv = (ChartOfAccountSearch.SearchValue)Session[SessionNameForQuery];
                chartOfAccountCode = sv.ChartOfAccountCode;
                chartOfAccountName = sv.ChartOfAccountName;
                accountLevel = sv.AccountLevel;
                generalAccount = sv.GeneralAccount;
            }

            if (!this.IsPostBack)
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = ChartOfAccountsMetadata.ColumnNames.ChartOfAccountCode;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr);
                grdList.MasterTableView.SortExpressions.AllowNaturalSort = false;
            }
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (GridSortExpression e in grdList.MasterTableView.SortExpressions)
            {
                sb.AppendFormat("{0}^{1}", this.grdList.MasterTableView.SortExpressions[0].FieldName, this.grdList.MasterTableView.SortExpressions[0].SortOrder);
                sb.Append(",");
            }
            
            //int totalCount = ChartOfAccounts.TotalCount(chartOfAccountCode, chartOfAccountName, accountLevel, generalAccount);
            //grdList.VirtualItemCount = totalCount;

            //grdList.DataSource = ChartOfAccounts.GetAllWithPaging(this.grdList.CurrentPageIndex, this.grdList.PageSize,
            //    chartOfAccountCode, chartOfAccountName, accountLevel, generalAccount,
            //    sb.ToString());
            //grdList.DataSource = ChartOfAccounts.GetAll(chartOfAccountCode, chartOfAccountName, accountLevel, generalAccount, sb.ToString());
        }
        protected void grdList_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = e.NewSortOrder;

                grdList.MasterTableView.SortExpressions.Clear();
                grdList.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

                grdList.Rebind();
            }
        }
		protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            grdList.AllowPaging = false;
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ChartOfAccountSearch.aspx";
            UrlPageDetail = "ChartOfAccountDetail.aspx";
			
			ProgramID = AppConstant.Program.CHART_OF_ACCOUNT;
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
            string id = dataItem.GetDataKeyValue(ChartOfAccountsMetadata.ColumnNames.ChartOfAccountId).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //this.GenerateGrid();

            grdList.DataSource = ChartOfAccounts;
        }

        private DataTable ChartOfAccounts
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null) return ((DataTable)(obj));

                ChartOfAccountsQuery query;
                if (Session[SessionNameForQuery] != null) query = (ChartOfAccountsQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ChartOfAccountsQuery("a");
                    query.Where(query.IsActive == true);
                    query.OrderBy(query.ChartOfAccountCode.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}