using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.TrainingHR
{
    public partial class EmployeeTrainingSearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = FormType == string.Empty ? AppConstant.Program.EmployeeTraining : (FormType == "point" ? AppConstant.Program.EmployeeTrainingPoint : (FormType == "pps" ? AppConstant.Program.EmployeeTrainingProposal : AppConstant.Program.EmployeeTrainingProposal2));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            OnButtonOkClick();

            return true;
        }

        private void OnButtonOkClick()
        {
            var query = new EmployeeTrainingQuery("a");
            query.Select(
                query,
                "<CAST(a.TargetAttendance AS VARCHAR) + '/' + CAST((SELECT COUNT(*) FROM EmployeeTrainingHistory eth WHERE eth.EmployeeTrainingID = a.EmployeeTrainingID AND eth.IsAttending = 1) AS VARCHAR) AS Attendance>"
            );
            query.Where(query.IsActive == true);
            if (FormType == string.Empty || FormType == "point")
                query.Where(query.IsProposal == false);
            else
            {
                query.Where(query.IsProposal == true);
                if (FormType == "pps")
                    query.Where(query.LastUpdateByUserID == AppSession.UserLogin.UserID);
            }
                
            if (!string.IsNullOrEmpty(txtTrainingName.Text))
            {
                if (cboFilterTrainingName.SelectedIndex == 1)
                    query.Where(query.EmployeeTrainingName == txtTrainingName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTrainingName.Text);
                    query.Where(query.EmployeeTrainingName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtTrainingLocation.Text))
            {
                if (cboFilterTrainingLocation.SelectedIndex == 1)
                    query.Where(query.TrainingLocation == txtTrainingLocation.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTrainingLocation.Text);
                    query.Where(query.TrainingLocation.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtTrainingOrganizer.Text))
            {
                if (cboFilterTrainingOrganizer.SelectedIndex == 1)
                    query.Where(query.TrainingOrganizer == txtTrainingOrganizer.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTrainingOrganizer.Text);
                    query.Where(query.TrainingOrganizer.Like(searchTextContain));
                }
            }

            query.OrderBy(query.EmployeeTrainingID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }
    }
}
