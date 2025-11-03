using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class PaymentTypeList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "PaymentTypeSearch.aspx";
            UrlPageDetail = "PaymentTypeDetail.aspx";

            WindowSearch.Height = 170;

            ProgramID = AppConstant.Program.PAYMENTTYPE;

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
            string id = dataItem.GetDataKeyValue(PaymentTypeMetadata.ColumnNames.SRPaymentTypeID).ToString();
            Page.Response.Redirect("PaymentTypeDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PaymentTypes;
        }

        private DataTable PaymentTypes
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                PaymentTypeQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (PaymentTypeQuery)Session[SessionNameForQuery];
                else
                {
                    query = new PaymentTypeQuery("a");
                    ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery("b");
                    SubLedgersQuery slQ = new SubLedgersQuery("c");
                    query.LeftJoin(coaQ).On(query.ChartOfAccountID == coaQ.ChartOfAccountId);
                    query.LeftJoin(slQ).On(query.SubledgerID == slQ.SubLedgerId);
                    query.Select
                        (
                        query.SRPaymentTypeID, 
                        query.PaymentTypeName,
                        @"<
                            RTRIM(b.ChartOfAccountCode) + ' - ' + RTRIM(b.ChartOfAccountName) AS ChartOfAccountName
                        >",
                        @"<
                            RTRIM(c.SubLedgerName) + ' - ' + RTRIM(c.Description) AS SubLedgerName
                        >",
                          query.IsCashierFrontOffice,
                          query.IsArPayment,
                          query.IsApPayment,
                          query.IsFeePayment, 
                          query.IsAssetAuctionPayment
                        );
                    query.OrderBy(query.SRPaymentTypeID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "PaymentTypeName", "SRPaymentTypeID");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}