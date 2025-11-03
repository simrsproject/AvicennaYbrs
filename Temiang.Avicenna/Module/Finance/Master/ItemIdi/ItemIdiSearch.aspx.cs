using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemIdiSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ItemIDI;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ItemIdiQuery();
            if (!string.IsNullOrEmpty(txtIdiCode.Text))
            {
                if (cboFilterIdiCode.SelectedIndex == 1)
                    query.Where(query.IdiCode == txtIdiCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtIdiCode.Text);
                    query.Where(query.IdiCode.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtIdiName.Text))
            {
                if (cboFilterIdiName.SelectedIndex == 1)
                    query.Where(query.IdiName == txtIdiName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtIdiName.Text);
                    query.Where(query.IdiName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}