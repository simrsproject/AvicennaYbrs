using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Appraisal
{
    public partial class QuestionerSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AppraisalQuestioner;

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new AppraisalQuestionQuery("a");
            var type = new AppStandardReferenceItemQuery("b");
            query.Select(query, type.ItemName.As("AppraisalTypeName"));
            query.LeftJoin(type).On(type.StandardReferenceID == AppEnum.StandardReference.AppraisalType && type.ItemID == query.SRAppraisalType);

            if (!string.IsNullOrEmpty(txtQuestionerName.Text))
            {
                if (cboFilterQuestionerName.SelectedIndex == 1)
                    query.Where(query.QuestionerName == txtQuestionerName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtQuestionerName.Text);
                    query.Where(query.QuestionerName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPeriodYear.Text))
            {
                query.Where(query.PeriodYear == txtPeriodYear.Text);
            }

            query.OrderBy(query.QuestionerID.Ascending, query.PeriodYear.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}