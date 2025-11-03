using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.HR.Master
{
    public partial class FormsTemplateSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EmployeeFormsTemplate;

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new EmployeeFormTemplateQuery("a");
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                            query.TemplateID,
                            query.TemplateName,
                            query.Result.Substring(100).As("Result")
                        );

            if (!string.IsNullOrEmpty(txtTemplateName.Text))
            {
                if (cboFilterTemplateName.SelectedIndex == 1)
                    query.Where(query.TemplateName == txtTemplateName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTemplateName.Text);
                    query.Where(query.TemplateName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}