using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ContributoryFactorsClassificationFrameworkSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ContributoryFactorsClassificationFramework;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ContributoryFactorsClassificationFrameworkQuery();
            if (!string.IsNullOrEmpty(txtFactorID.Text))
            {
                if (cboFilterFactorID.SelectedIndex == 1)
                    query.Where(query.FactorID == txtFactorID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFactorID.Text);
                    query.Where(query.FactorID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtFactorName.Text))
            {
                if (cboFilterFactorName.SelectedIndex == 1)
                    query.Where(query.FactorName == txtFactorName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFactorName.Text);
                    query.Where(query.FactorName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
