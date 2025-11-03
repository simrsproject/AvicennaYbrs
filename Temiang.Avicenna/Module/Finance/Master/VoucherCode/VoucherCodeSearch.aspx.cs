using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class VoucherCodeSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_CODE;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new JournalCodesQuery("a");
            query.SelectAll();
            query.Where(query.IsVisible == true);

            if (!string.IsNullOrEmpty(txtVoucherCode.Text))
            {
                if (cboFilterVoucherCode.SelectedIndex == 1)
                    query.Where(query.JournalCode == txtVoucherCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtVoucherCode.Text);
                    query.Where(query.JournalCode.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtVoucherNote.Text))
            {
                if (cboFilterVoucherNote.SelectedIndex == 1)
                    query.Where(query.Description == txtVoucherNote.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtVoucherNote.Text);
                    query.Where(query.Description.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
