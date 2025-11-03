using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class QuestionFormSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.QuestionForm;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new QuestionFormQuery("a");
            if (!string.IsNullOrEmpty(txtQuestionFormID.Text))
            {
                if (cboFilterQuestionFormID.SelectedIndex == 1)
                    query.Where(query.QuestionFormID == txtQuestionFormID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtQuestionFormID.Text);
                    query.Where(query.QuestionFormID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtQuestionFormName.Text))
            {
                if (cboFilterQuestionFormName.SelectedIndex == 1)
                    query.Where(query.QuestionFormName == txtQuestionFormName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtQuestionFormName.Text);
                    query.Where(query.QuestionFormName.Like(searchTextContain));
                }
            }

            query.Where(query.IsActive == true);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
