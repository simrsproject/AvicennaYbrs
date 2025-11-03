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
    public partial class ClosingBalanceList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CashierClosingBalance;

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
            var usr = new AppUserQuery("d");
            var counter = new AppStandardReferenceItemQuery("e");
            query.InnerJoin(cashier).On(query.TransactionNo == cashier.TransactionNo);
            query.InnerJoin(shift).On(query.SRShift == shift.ItemID && shift.StandardReferenceID == AppEnum.StandardReference.Shift);
            query.InnerJoin(usr).On(cashier.CashierUserID == usr.UserID);
            query.InnerJoin(counter).On(query.SRCashierCounter == counter.ItemID && counter.StandardReferenceID == AppEnum.StandardReference.CashierCounter);
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
                    @"<CASE WHEN a.TransactionNo = d.CashManagementNo AND b.IsCashierCheckin = 1 AND a.ClosingDate IS NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'IsVisible'>"
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

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();
        }

        private decimal CalculateCashPayment(string cmno)
        {
            var dt = new TransPaymentItemQuery("a");
            var hd = new TransPaymentQuery("b");
            dt.InnerJoin(hd).On(dt.PaymentNo == hd.PaymentNo);
            dt.Where(hd.TransactionCode != "019", hd.IsApproved == true, hd.CashManagementNo == cmno,
                     dt.SRPaymentType == AppSession.Parameter.PaymentTypePayment,
                     dt.SRPaymentMethod == AppSession.Parameter.PaymentMethodCash);

            var coll = new TransPaymentItemCollection();
            coll.Load(dt);

            decimal? totalPayment = coll.Aggregate<TransPaymentItem, decimal?>(0, (current, c) => current + c.Amount);

            dt = new TransPaymentItemQuery("a");
            hd = new TransPaymentQuery("b");
            dt.InnerJoin(hd).On(dt.PaymentNo == hd.PaymentNo);
            dt.Where(hd.TransactionCode == "019", hd.IsApproved == true, hd.CashManagementNo == cmno,
                     dt.SRPaymentType == AppSession.Parameter.PaymentTypePayment,
                     dt.SRPaymentMethod == AppSession.Parameter.PaymentMethodCash);

            coll = new TransPaymentItemCollection();
            coll.Load(dt);

            decimal? totalReturDp = coll.Aggregate<TransPaymentItem, decimal?>(0, (current, c) => current + c.Amount);

            var dtp = new TransPaymentPatientItemQuery("a");
            var hdp = new TransPaymentPatientQuery("b");
            dtp.InnerJoin(hdp).On(dtp.PaymentNo == hdp.PaymentNo);
            dtp.Where(hdp.TransactionCode == "018", hdp.IsApproved == true, hdp.CashManagementNo == cmno,
                     dtp.SRPaymentType == AppSession.Parameter.PaymentTypePayment,
                     dtp.SRPaymentMethod == AppSession.Parameter.PaymentMethodCash);

            var collp = new TransPaymentPatientItemCollection();
            collp.Load(dtp);

            decimal? totalPatientPayment = collp.Aggregate<TransPaymentPatientItem, decimal?>(0, (current, c) => current + c.Amount);

            dtp = new TransPaymentPatientItemQuery("a");
            hdp = new TransPaymentPatientQuery("b");
            dtp.InnerJoin(hdp).On(dtp.PaymentNo == hdp.PaymentNo);
            dtp.Where(hdp.TransactionCode == "019", hdp.IsApproved == true, hdp.CashManagementNo == cmno,
                     dtp.SRPaymentType == AppSession.Parameter.PaymentTypePayment,
                     dtp.SRPaymentMethod == AppSession.Parameter.PaymentMethodCash);

            collp = new TransPaymentPatientItemCollection();
            collp.Load(dtp);

            decimal? totalPatientRetur = collp.Aggregate<TransPaymentPatientItem, decimal?>(0, (current, c) => current + c.Amount);

            return (totalPayment ?? 0) - (totalReturDp ?? 0) + (totalPatientPayment ?? 0) - (totalPatientRetur ?? 0);
        }
    }
}
