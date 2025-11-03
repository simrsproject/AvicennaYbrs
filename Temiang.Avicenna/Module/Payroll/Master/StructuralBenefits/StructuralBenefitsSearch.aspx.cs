using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class StructuralBenefitsSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.StructuralBenefits;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new OrganizationUnitQuery("a");
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select
                (
                    query.OrganizationUnitID,
                    query.OrganizationUnitCode,
                    query.OrganizationUnitName
                );
            query.Where(query.IsActive == true, query.SROrganizationLevel == "1");
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            if (!string.IsNullOrEmpty(txtOrganizationUnitCode.Text))
            {
                if (cboFilterOrganizationUnitCode.SelectedIndex == 1)
                    query.Where(query.OrganizationUnitCode == txtOrganizationUnitCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtOrganizationUnitCode.Text);
                    query.Where(query.OrganizationUnitCode.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtOrganizationUnitName.Text))
            {
                if (cboFilterOrganizationUnitName.SelectedIndex == 1)
                    query.Where(query.OrganizationUnitName == txtOrganizationUnitName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtOrganizationUnitName.Text);
                    query.Where(query.OrganizationUnitName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
