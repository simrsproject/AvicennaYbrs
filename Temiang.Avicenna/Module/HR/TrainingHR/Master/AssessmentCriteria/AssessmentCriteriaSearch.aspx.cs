using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.TrainingHR.Master.AssessmentCriteria
{
    public partial class AssessmentCriteriaSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.EmployeeTrainingAssessmentCriteria;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new EmployeeTrainingAssessmentCriteriaQuery("a");
            query.Select(query);

            if (!string.IsNullOrEmpty(txtAssessmentCriteriaName.Text))
            {
                if (cboFilterAssessmentCriteriaName.SelectedIndex == 1)
                    query.Where(query.AssessmentCriteriaName == txtAssessmentCriteriaName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtAssessmentCriteriaName.Text);
                    query.Where(query.AssessmentCriteriaName.Like(searchText));
                }
            }
            query.OrderBy(query.MinValue.Descending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}