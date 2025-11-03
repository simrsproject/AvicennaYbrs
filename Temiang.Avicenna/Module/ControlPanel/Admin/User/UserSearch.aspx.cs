using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.Admin
{
    public partial class UserSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.User;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AppUserQuery("u");
            var par = new ParamedicQuery("p");

            query.LeftJoin(par).On(query.ParamedicID == par.ParamedicID);
            query.Select(query, par.ParamedicName);

            if (!string.IsNullOrEmpty(txtUserID.Text))
            {
                if (cboFilterUserID.SelectedIndex == 1)
                    query.Where(query.UserID == txtUserID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtUserID.Text);
                    query.Where(query.UserID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtUserName.Text))
            {
                if (cboFilterUserName.SelectedIndex == 1)
                    query.Where(query.UserName == txtUserName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtUserName.Text);
                    query.Where(query.UserName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
            return true;
        }
    }
}
