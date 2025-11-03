using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class EmployeeAchievementSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryEmployeeAchievement;//TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var award = new AwardQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new EmployeeAchievementQuery("a");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select
                (
                   query.EmployeeAchievementID,
                   query.PersonID,
                   personal.EmployeeNumber,
                   personal.EmployeeName,
                   query.AwardID,
                   award.AwardName,
                   award.AwardPrize,
                   query.AwardDate,
                   query.Achievement,
                   query.FinancialValue,
                   query.Note
                );

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.InnerJoin(award).On(query.AwardID == award.AwardID);

            if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
            {
                if (cboFilterEmployeeNumber.SelectedIndex == 1)
                    query.Where(personal.EmployeeNumber == txtEmployeeNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeNo.Text);
                    query.Where(personal.EmployeeNumber.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtFirstName.Text))
            {
                if (cboFirstName.SelectedIndex == 1)
                    query.Where(personal.FirstName == txtFirstName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFirstName.Text);
                    query.Where(personal.FirstName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtAwardName.Text))
            {
                if (cboFilterAwardName.SelectedIndex == 1)
                    query.Where(award.AwardName == txtAwardName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtAwardName.Text);
                    query.Where(award.AwardName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtAwardPrize.Text))
            {
                if (cboFilterAwardPrize.SelectedIndex == 1)
                    query.Where(award.AwardPrize == txtAwardPrize.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtAwardPrize.Text);
                    query.Where(award.AwardPrize.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtAchievement.Text))
            {
                if (cboFilterAchievement.SelectedIndex == 1)
                    query.Where(query.Achievement == txtAchievement.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtAchievement.Text);
                    query.Where(query.Achievement.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
