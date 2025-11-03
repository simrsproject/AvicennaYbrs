using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class SubLedgerBalanceSearch : BasePageDialog
    {
        public class SearchValue
        {
            public string CoaCode;
            public string CoaName;
            public string SlName;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_SUBLEDGER_BALANCE;
            if (Session[SessionNameForQuery] != null)
            {
                SubLedgerBalanceSearch.SearchValue sv = (SubLedgerBalanceSearch.SearchValue)Session[SessionNameForQuery];
                txtCoaCode.Text = sv.CoaCode;
                txtCoaName.Text = sv.CoaName;
                txtSlName.Text = sv.SlName;
            }
        }

        public override bool OnButtonOkClicked()
        {
            SearchValue sv = new SearchValue();

            sv.CoaCode = txtCoaCode.Text;
            sv.CoaName = txtCoaName.Text;
            sv.SlName = txtSlName.Text;

            Session[SessionNameForQuery] = sv;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}