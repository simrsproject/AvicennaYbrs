using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class RiskGradingSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.RiskGrading;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new RiskGradingQuery();
            query.OrderBy(query.RiskGradingID.Ascending);

            if (!string.IsNullOrEmpty(txtRiskGradingID.Text))
            {
                if (cboFilterRiskGradingID.SelectedIndex == 1)
                    query.Where(query.RiskGradingID == txtRiskGradingID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRiskGradingID.Text);
                    query.Where(query.RiskGradingID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtRiskGradingName.Text))
            {
                if (cboFilterRiskGradingName.SelectedIndex == 1)
                    query.Where(query.RiskGradingName == txtRiskGradingName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRiskGradingName.Text);
                    query.Where(query.RiskGradingName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
