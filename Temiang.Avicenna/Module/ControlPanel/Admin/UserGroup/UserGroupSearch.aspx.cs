using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.Admin
{
    public partial class UserGroupSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.UserGroup;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AppUserGroupQuery();
            if (!string.IsNullOrEmpty(txtUserGroupID.Text))
            {
                if (cboFilterUserGroupID.SelectedIndex == 1)
                    query.Where(query.UserGroupID == txtUserGroupID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtUserGroupID.Text);
                    query.Where(query.UserGroupID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtUserGroupName.Text))
            {
                if (cboFilterUserGroupName.SelectedIndex == 1)
                    query.Where(query.UserGroupName == txtUserGroupName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtUserGroupName.Text);
                    query.Where(query.UserGroupName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
            return true;
        }
    }
}