using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryComponentRoundingSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SalaryComponentRounding; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new SalaryComponentRoundingQuery();
            if (!string.IsNullOrEmpty(txtSalaryComponentRoundingName.Text))
            {
                if (cboFilterSalaryComponentRoundingName.SelectedIndex == 1)
                    query.Where(query.SalaryComponentRoundingName == txtSalaryComponentRoundingName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSalaryComponentRoundingName.Text);
                    query.Where(query.SalaryComponentRoundingName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
