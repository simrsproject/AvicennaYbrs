using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryTemplateSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.SalaryTemplate;//TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new SalaryTemplateQuery();
            if (!string.IsNullOrEmpty(txtSalaryTemplateID.Text))
            {
                if (cboFilterSalaryTemplateID.SelectedIndex == 1)
                    query.Where(query.SalaryTemplateID == txtSalaryTemplateID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSalaryTemplateID.Text);
                    query.Where(query.SalaryTemplateID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtSalaryTemplateName.Text))
            {
                if (cboFilterSalaryTemplateName.SelectedIndex == 1)
                    query.Where(query.SalaryTemplateName == txtSalaryTemplateName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSalaryTemplateName.Text);
                    query.Where(query.SalaryTemplateName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
