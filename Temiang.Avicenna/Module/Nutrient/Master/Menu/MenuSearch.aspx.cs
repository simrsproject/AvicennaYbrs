using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class MenuSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Menu;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new MenuQuery();
            query.Select
                (
                    query.MenuID,
                    query.MenuName,
                    query.IsExtra,
                    query.IsActive
                );
            if (!string.IsNullOrEmpty(txtMenuID.Text))
            {
                if (cboFilterMenuID.SelectedIndex == 1)
                    query.Where(query.MenuID == txtMenuID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMenuID.Text);
                    query.Where(query.MenuID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtMenuName.Text))
            {
                if (cboFilterMenuName.SelectedIndex == 1)
                    query.Where(query.MenuName == txtMenuName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMenuName.Text);
                    query.Where(query.MenuName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.MenuID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
