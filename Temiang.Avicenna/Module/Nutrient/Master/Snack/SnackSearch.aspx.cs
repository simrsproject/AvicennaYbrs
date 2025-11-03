using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class SnackSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.Snack;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new SnackQuery();
            query.Select
                (
                    query.SnackID,
                    query.SnackName,
                    query.IsActive
                );
            if (!string.IsNullOrEmpty(txtSnackID.Text))
            {
                if (cboFilterSnackID.SelectedIndex == 1)
                    query.Where(query.SnackID == txtSnackID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSnackID.Text);
                    query.Where(query.SnackID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtSnackName.Text))
            {
                if (cboFilterSnackName.SelectedIndex == 1)
                    query.Where(query.SnackName == txtSnackName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSnackName.Text);
                    query.Where(query.SnackName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.SnackID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
