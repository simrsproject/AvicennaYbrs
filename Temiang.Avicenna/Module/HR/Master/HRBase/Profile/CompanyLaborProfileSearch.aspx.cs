using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class CompanyLaborProfileSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Profile; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CompanyLaborProfileQuery();
            if (!string.IsNullOrEmpty(txtCompanyLaborProfileCode.Text))
            {
                if (cboFilterCompanyLaborProfileCode.SelectedIndex == 1)
                    query.Where(query.CompanyLaborProfileCode == txtCompanyLaborProfileCode.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtCompanyLaborProfileCode.Text);
                    query.Where(query.CompanyLaborProfileCode.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtCompanyLaborProfileName.Text))
            {
                if (cboFilterCompanyLaborProfileName.SelectedIndex == 1)
                    query.Where(query.CompanyLaborProfileName == txtCompanyLaborProfileName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtCompanyLaborProfileName.Text);
                    query.Where(query.CompanyLaborProfileName.Like(searchText));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
