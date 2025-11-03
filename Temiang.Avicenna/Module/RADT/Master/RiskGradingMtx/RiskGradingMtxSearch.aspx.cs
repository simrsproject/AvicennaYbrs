using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class RiskGradingMtxSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.RiskGradingMtx;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new AppStandardReferenceItemQuery();
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.ClinicalImpact);
            query.OrderBy(query.ItemID.Ascending);

            if (!string.IsNullOrEmpty(txtSRClinicalImpact.Text))
            {
                if (cboFilterSRClinicalImpact.SelectedIndex == 1)
                    query.Where(query.ItemID == txtSRClinicalImpact.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSRClinicalImpact.Text);
                    query.Where(query.ItemID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtClinicalImpact.Text))
            {
                if (cboFilterClinicalImpact.SelectedIndex == 1)
                    query.Where(query.ItemName == txtClinicalImpact.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtClinicalImpact.Text);
                    query.Where(query.ItemName.Like(searchTextContain));
                }
            }
            
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
