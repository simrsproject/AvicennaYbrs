using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CashTransactionListSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.CASH_TRANSACTION_LIST;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CashTransactionListQuery("a");
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
            query.InnerJoin(b).On(query.ChartOfAccountId == b.ChartOfAccountId);
            query.LeftJoin(c).On(c.SubLedgerId == query.SubledgerId);
            query.InnerJoin(d).On(query.CashType == d.ItemID && d.StandardReferenceID == AppEnum.StandardReference.CashManagementType);

            if (!string.IsNullOrEmpty(txtListID.Text))
            {
                if (cboFilterListID.SelectedIndex == 1)
                    query.Where(query.ListId == txtListID.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtListID.Text);
                    query.Where(query.ListId.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                if (cboFilterDescription.SelectedIndex == 1)
                    query.Where(query.Description == txtDescription.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtDescription.Text);
                    query.Where(query.Description.Like(searchText));
                }
            }
            query.OrderBy(query.Description.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
