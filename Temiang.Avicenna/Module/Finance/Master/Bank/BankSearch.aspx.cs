using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class BankSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.BANK;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new BankQuery("a");
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

            if (!string.IsNullOrEmpty(txtBankID.Text))
            {
                if (cboFilterBankID.SelectedIndex == 1)
                    query.Where(query.BankID == txtBankID.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtBankID.Text);
                    query.Where(query.BankID.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtBankName.Text))
            {
                if (cboFilterBankName.SelectedIndex == 1)
                    query.Where(query.BankName == txtBankName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtBankName.Text);
                    query.Where(query.BankName.Like(searchText));
                }
            }
            query.OrderBy(query.BankID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
