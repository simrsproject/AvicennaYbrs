using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.CPA
{
    public partial class QuestionnaireSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ClinicalPerformanceAppraisalQuestionnaire;

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ClinicalPerformanceAppraisalQuestionnaireQuery("a");
            query.Select(query);

            query.OrderBy(query.QuestionnaireCode.Ascending);

            if (!string.IsNullOrEmpty(txtQuestionnaireCode.Text))
            {
                if (cboFilterQuestionnaireCode.SelectedIndex == 1)
                    query.Where(query.QuestionnaireCode == txtQuestionnaireCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtQuestionnaireCode.Text);
                    query.Where(query.QuestionnaireCode.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtQuestionnaireName.Text))
            {
                if (cboFilterQuestionnaireName.SelectedIndex == 1)
                    query.Where(query.QuestionnaireName == txtQuestionnaireName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtQuestionnaireName.Text);
                    query.Where(query.QuestionnaireName.Like(searchTextContain));
                }
            }
            
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}