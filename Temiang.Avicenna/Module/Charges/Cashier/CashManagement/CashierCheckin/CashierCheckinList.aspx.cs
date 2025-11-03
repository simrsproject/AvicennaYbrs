using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class CashierCheckinList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CashierCheckin;

            if (!IsPostBack)
            {
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        private DataTable CashManagements()
        {
            var query = new CashManagementQuery("a");
            var cashier = new CashManagementCashierQuery("b");
            var shift = new AppStandardReferenceItemQuery("c");
            var counter = new AppStandardReferenceItemQuery("d");
            var usr = new AppUserQuery("e");
            query.InnerJoin(cashier).On(query.TransactionNo == cashier.TransactionNo);
            query.InnerJoin(shift).On(query.SRShift == shift.ItemID && shift.StandardReferenceID == AppEnum.StandardReference.Shift);
            query.InnerJoin(counter).On(query.SRCashierCounter == counter.ItemID && counter.StandardReferenceID == AppEnum.StandardReference.CashierCounter);
            query.InnerJoin(usr).On(cashier.CashierUserID == usr.UserID);
            query.Select
                (
                    query.TransactionNo,
                    query.OpeningDate,
                    query.OpeningBalance,
                    query.SRShift,
                    shift.ItemName.As("ShiftName"),
                    query.SRCashierCounter,
                    counter.ItemName.As("CashierCounterName"),
                    cashier.CashierUserID,
                    usr.UserName.As("CashierUserName"),
                    query.OpeningBalance,
                    @"<ISNULL(a.ClosingBalance, 0) AS ClosingBalance>",
                    query.ClosingDate,
                    query.IsApproved,
                    query.IsVoid,
                    cashier.IsCashierCheckin,
                    @"<CASE WHEN a.ClosingDate IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsClosing>"
                );
            query.OrderBy(query.TransactionNo.Descending);

            if (!txtOpeningDate.IsEmpty)
                query.Where(query.OpeningDate.Date() == txtOpeningDate.SelectedDate);
            if (!txtClosingDate.IsEmpty)
                query.Where(query.ClosingDate.Date() == txtClosingDate.SelectedDate);

            query.Where(query.IsApproved == true, cashier.CashierUserID == AppSession.UserLogin.UserID);

            return query.LoadDataTable();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = CashManagements();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');
                string msg = string.Empty;

                var cm = new CashManagement();
                cm.LoadByPrimaryKey(param[1]);
                if (cm.OpeningDate.Value.Date != (new DateTime()).NowAtSqlServer().Date)
                {
                    msg = "Selected cash management invalid.";
                }

                if (msg != string.Empty)
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = msg;
                }
                else
                {
                    using (var trans = new esTransactionScope())
                    {
                        var cmc = new CashManagementCashier();
                        cmc.LoadByPrimaryKey(param[1], AppSession.UserLogin.UserLogID.ToString());

                        cmc.IsCashierCheckin = true;
                        cmc.CashierCheckinDateTime = (new DateTime()).NowAtSqlServer();
                        cmc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        cmc.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        cmc.Save();

                        var usr = new AppUser();
                        usr.LoadByPrimaryKey(AppSession.UserLogin.UserID);
                        usr.CashManagementNo = param[1];
                        usr.Save();

                        trans.Complete();
                    }
                }

                grdList.Rebind();
            }
        }
    }
}
