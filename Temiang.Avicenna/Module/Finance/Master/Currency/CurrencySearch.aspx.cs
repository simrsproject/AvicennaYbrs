using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CurrencySearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.CURRENCY;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CurrencyRateQuery();
            if (!string.IsNullOrEmpty(txtCurrencyID.Text))
            {
                if (cboFilterCurrencyID.SelectedIndex == 1)
                    query.Where(query.CurrencyID == txtCurrencyID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCurrencyID.Text);
                    query.Where(query.CurrencyID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtCurrencyName.Text))
            {
                if (cboFilterCurrencyName.SelectedIndex == 1)
                    query.Where(query.CurrencyName == txtCurrencyName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCurrencyName.Text);
                    query.Where(query.CurrencyName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.CurrencyID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
