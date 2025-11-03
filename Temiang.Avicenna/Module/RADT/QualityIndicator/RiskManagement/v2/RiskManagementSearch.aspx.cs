using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator.v2
{
    public partial class RiskManagementSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RiskManagement;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, true);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new RiskManagementQuery("a");
            var qsu = new ServiceUnitQuery("b");

            query.InnerJoin(qsu).On(query.ServiceUnitID == qsu.ServiceUnitID);


            query.OrderBy
                (
                    query.RiskManagementNo.Descending
                );

            query.Select(

                query.RiskManagementNo,
                query.PeriodYear,
                query.ServiceUnitID,
                qsu.ServiceUnitName,
                query.IsApproved
                );

            if (!string.IsNullOrEmpty(txtRiskManagementNo.Text))
            {
                if (cboFilterRiskManagementNo.SelectedIndex == 1)
                    query.Where(query.RiskManagementNo == txtRiskManagementNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRiskManagementNo.Text);
                    query.Where(query.RiskManagementNo.Like(searchTextContain));
                }
            }
            if (!txtPeriodYear.Text.Equals(string.Empty))
            {
                query.Where(query.PeriodYear == txtPeriodYear.Value.ToShort());
            }
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}