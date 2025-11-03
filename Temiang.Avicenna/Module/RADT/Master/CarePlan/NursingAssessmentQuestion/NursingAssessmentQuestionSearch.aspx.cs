using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class NursingAssessmentQuestionSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.NursingAssessmentQuestion;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new QuestionQuery("a");
            var qEquiv = new QuestionQuery("qEquiv");

            query.LeftJoin(qEquiv).On(query.EquivalentQuestionID == qEquiv.QuestionID)
                .Where(query.NursingDisplayAs.Coalesce("''") != string.Empty)
                .Select
                (
                    query.QuestionID,
                    query.QuestionText,
                    query.NursingDisplayAs,
                    query.SRAnswerType,
                    "<ISNULL((select top 1 SRAnswerType from NursingAssessmentDiagnosa nad where nad.QuestionID = a.QuestionID and nad.SRAnswerType <> a.SRAnswerType),'') SRNadAnswerType>",
                    query.IsActive,
                    qEquiv.QuestionID.As("equivQuestionID"),
                    qEquiv.QuestionText.As("equivQuestionText"),
                    qEquiv.SRAnswerType.As("equivSRAnswerType")
                );

            query.es.Distinct = true;

            if (!string.IsNullOrEmpty(txtQuestionID.Text))
            {
                if (cboFilterQuestionID.SelectedIndex == 1)
                    query.Where(query.QuestionID == txtQuestionID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtQuestionID.Text);
                    query.Where(query.QuestionID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtQuestionText.Text))
            {
                if (cboFilterQuestionText.SelectedIndex == 1)
                    query.Where(query.QuestionText == txtQuestionText.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtQuestionText.Text);
                    query.Where(query.QuestionText.Like(searchTextContain));
                }
            }
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
