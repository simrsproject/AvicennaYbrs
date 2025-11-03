using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.TrainingHR.Master.AssessmentAspect
{
    public partial class AssessmentAspectSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.EmployeeTrainingAssessmentAspect;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new EmployeeTrainingAssessmentAspectQuery("a");
            query.Select(query);

            if (!string.IsNullOrEmpty(txtAssessmentAspectID.Text))
            {
                if (cboFilterAssessmentAspectID.SelectedIndex == 1)
                    query.Where(query.AssessmentAspectID == txtAssessmentAspectID.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtAssessmentAspectID.Text);
                    query.Where(query.AssessmentAspectID.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtAssessmentAspectName.Text))
            {
                if (cboFilterAssessmentAspectName.SelectedIndex == 1)
                    query.Where(query.AssessmentAspectName == txtAssessmentAspectName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtAssessmentAspectName.Text);
                    query.Where(query.AssessmentAspectName.Like(searchText));
                }
            }
            query.OrderBy(query.AssessmentAspectID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}