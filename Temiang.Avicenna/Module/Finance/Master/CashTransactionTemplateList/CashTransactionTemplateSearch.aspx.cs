using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CashTransactionTemplateSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.CASH_TRANSACTION_LIST;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CashTransactionTemplateQuery("a");
            query.Select
                (
                    query
                );

            if (!string.IsNullOrEmpty(txtTemplateID.Text))
            {
                if (cboFilterTemplateID.SelectedIndex == 1)
                    query.Where(query.TemplateId == txtTemplateID.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtTemplateID.Text);
                    query.Where(query.TemplateId.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtTemplateName.Text))
            {
                if (cboFilterTemplateName.SelectedIndex == 1)
                    query.Where(query.TemplateName == txtTemplateName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtTemplateName.Text);
                    query.Where(query.TemplateName.Like(searchText));
                }
            }
            query.OrderBy(query.TemplateName.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
