using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class AcctSubGroupSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ACCOUNT_SUB_GROUP;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new SubLedgerGroupsQuery("a");
            query.SelectAll();

            if (!string.IsNullOrEmpty(txtAcctSubGroupID.Text))
            {
                if (cboFilterAcctSubGroupID.SelectedIndex == 1)
                    query.Where(query.GroupCode == txtAcctSubGroupID.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtAcctSubGroupID.Text);
                    query.Where(query.GroupCode.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtAcctSubGroupName.Text))
            {
                if (cboFilterAcctSubGroupName.SelectedIndex == 1)
                    query.Where(query.GroupName == txtAcctSubGroupName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtAcctSubGroupName.Text);
                    query.Where(query.GroupName.Like(searchText));
                }
            }
            query.OrderBy(query.GroupCode.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
