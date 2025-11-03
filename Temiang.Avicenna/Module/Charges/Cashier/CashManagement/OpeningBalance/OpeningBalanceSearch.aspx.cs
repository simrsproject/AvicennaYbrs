using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class OpeningBalanceSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CashierOpeningBalance;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRShift, AppEnum.StandardReference.Shift);
                StandardReference.InitializeIncludeSpace(cboSRCashierCounter, AppEnum.StandardReference.CashierCounter);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CashManagementQuery("a");
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

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
            }
            if (!txtOpeningDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.OpeningBalance >= txtOpeningDate.SelectedDate, query.OpeningBalance < txtOpeningDate.SelectedDate.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(txtOpenedBy.Text))
            {
                if (cboFilterOpenedBy.SelectedIndex == 1)
                    query.Where(query.Or(query.CreatedByUserID == txtOpenedBy.Text, usr.UserName == txtOpenedBy.Text));
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtOpenedBy.Text);
                    query.Where(query.Or(query.CreatedByUserID.Like(searchTextContain),
                                         usr.UserName.Like(searchTextContain)));
                }
            }
            if (!string.IsNullOrEmpty(cboSRShift.SelectedValue))
            {
                query.Where(query.SRShift == cboSRShift.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboSRCashierCounter.SelectedValue))
            {
                query.Where(query.SRCashierCounter == cboSRCashierCounter.SelectedValue);
            }
            if (!txtClosingDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.ClosingBalance.Date() == txtClosingDate.SelectedDate);
            }
            if (chkIsApproved.Checked)
                query.Where(query.IsApproved == true);
            
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
