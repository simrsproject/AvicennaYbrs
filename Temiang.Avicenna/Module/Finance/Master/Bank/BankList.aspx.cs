using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class BankList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "BankSearch.aspx";
            UrlPageDetail = "BankDetail.aspx";

            WindowSearch.Height = 170;

            ProgramID = AppConstant.Program.BANK;

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
            string id = dataItem.GetDataKeyValue(BankMetadata.ColumnNames.BankID).ToString();
            Page.Response.Redirect("BankDetail.aspx?md=" + mode + "&id=" + id, true);
        }	

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Banks;
            grdList.Columns.FindByUniqueName("IsBKU").Visible = AppSession.Parameter.IsUsingBKUModule;
        }

        private DataTable Banks
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                BankQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (BankQuery)Session[SessionNameForQuery];
                else
                {
                    query = new BankQuery("a");
                    var coa = new ChartOfAccountsQuery("b");
                    query.LeftJoin(coa).On(query.ChartOfAccountId == coa.ChartOfAccountId);
                    query.Select(query.BankID,
                                 query.BankName,
                                 query.ChartOfAccountId,
                                 query.SubledgerId,
                                 query.LastUpdateDateTime,
                                 query.LastUpdateByUserID,
                                 query.NoRek,
                                 query.JournalCode,
                                 query.CurrencyCode,
                                 query.IsActive,
                                 query.IsToBeCleared,
                                 query.IsCrossRefference,
                                 query.IsCashierFrontOffice,
                                 query.IsCashierFrontOfficeDpReturn,
                                 query.IsArPayment,
                                 query.IsApPayment,
                                 query.IsFeePayment,
                                 query.IsAssetAuctionPayment,
                                 query.IsBKU,
                                 @"<RTRIM(ISNULL(b.ChartOfAccountCode, '') + ' - ' + ISNULL(b.ChartOfAccountName, '')) AS ChartOfAccountName>");
                    query.OrderBy(query.BankID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "BankName", "BankID");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}