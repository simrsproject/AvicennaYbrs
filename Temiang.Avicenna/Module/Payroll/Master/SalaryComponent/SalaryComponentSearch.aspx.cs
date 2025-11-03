using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryComponentSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SalaryComponent;//TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new SalaryComponentQuery();
            if (!string.IsNullOrEmpty(txtSalaryComponentCode.Text))
            {
                if (cboFilterSalaryComponentCode.SelectedIndex == 1)
                    query.Where(query.SalaryComponentCode == txtSalaryComponentCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSalaryComponentCode.Text);
                    query.Where(query.SalaryComponentCode.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtSalaryComponentName.Text))
            {
                if (cboFilterSalaryComponentName.SelectedIndex == 1)
                    query.Where(query.SalaryComponentName == txtSalaryComponentName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSalaryComponentName.Text);
                    query.Where(query.SalaryComponentName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
