using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class OpeningBalanceList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "OpeningBalanceSearch.aspx";
            UrlPageDetail = "OpeningBalanceDetail.aspx";

            ProgramID = AppConstant.Program.CashierOpeningBalance;

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
            string id = dataItem.GetDataKeyValue(CashManagementMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("OpeningBalanceDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = CashManagements;
        }

        private DataTable CashManagements
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CashManagementQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CashManagementQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CashManagementQuery("a");
                    var shift = new AppStandardReferenceItemQuery("b");
                    var counter = new AppStandardReferenceItemQuery("c");
                    var usr = new AppUserQuery("d");
                    var usrc = new AppUserQuery("e");
                    query.InnerJoin(shift).On(query.SRShift == shift.ItemID && shift.StandardReferenceID == AppEnum.StandardReference.Shift);
                    query.InnerJoin(counter).On(query.SRCashierCounter == counter.ItemID && counter.StandardReferenceID == AppEnum.StandardReference.CashierCounter);
                    query.InnerJoin(usr).On(query.CreatedByUserID == usr.UserID);
                    query.LeftJoin(usrc).On(query.ClosingByUserID == usrc.UserID);
                    query.Select
                        (
                            query.TransactionNo,
                            query.OpeningDate,
                            usr.UserName.As("OpenedBy"),
                            query.OpeningBalance,
                            query.SRShift,
                            shift.ItemName.As("ShiftName"),
                            query.SRCashierCounter,
                            counter.ItemName.As("CashierCounterName"),
                            query.OpeningBalance,
                            @"<ISNULL(a.ClosingBalance, 0) AS ClosingBalance>",
                            query.ClosingDate,
                            usrc.UserName.As("ClosedBy"),
                            query.IsApproved,
                            query.IsVoid
                        );
                    query.OrderBy(query.TransactionNo.Descending);

                    //Quick Search
                    ApplyQuickSearch(query, "TransactionNo", "TransactionNo");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var transno = e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString();

            var query = new CashManagementCashierQuery("a");
            var usr = new AppUserQuery("b");
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select
                (
                    query.CashierUserID,
                    usr.UserName.As("CashierUserName"),
                    query.IsCashierCheckin,
                    query.CashierCheckinDateTime
                );
            query.LeftJoin(usr).On(query.CashierUserID == usr.UserID);
            query.Where(query.TransactionNo == transno);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
