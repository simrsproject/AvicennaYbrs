using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class EmployeePositionSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryEmployeePosition;//TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var level = new PositionLevelQuery("e");
            var grade = new PositionGradeQuery("d");
            var position = new PositionQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new EmployeePositionQuery("a");

            query.Select
                (
                   query.EmployeePositionID,
                   query.PersonID,
                   personal.EmployeeNumber,
                   personal.EmployeeName,
                   query.PositionID,
                   position.PositionCode,
                   position.PositionName,
                   grade.PositionGradeName,
                   level.PositionLevelName,
                   query.ValidFrom,
                   query.ValidTo,
                   query.IsPrimaryPosition
                );

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.InnerJoin(position).On(query.PositionID == position.PositionID);
            query.LeftJoin(grade).On(position.PositionGradeID == grade.PositionGradeID);
            query.LeftJoin(level).On(position.PositionLevelID == level.PositionLevelID);
            query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya

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
            if (!string.IsNullOrEmpty(txtPositionName.Text))
            {
                if (cboFilterPositionName.SelectedIndex == 1)
                    query.Where(position.PositionName == txtPositionName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionName.Text);
                    query.Where(position.PositionName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPositionGradeName.Text))
            {
                if (cboFilterPositionGradeName.SelectedIndex == 1)
                    query.Where(grade.PositionGradeName == txtPositionGradeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionGradeName.Text);
                    query.Where(grade.PositionGradeName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(PositionLevelName.Text))
            {
                if (cboFilterPositionLevelName.SelectedIndex == 1)
                    query.Where(level.PositionLevelName == PositionLevelName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", PositionLevelName.Text);
                    query.Where(level.PositionLevelName.Like(searchTextContain));
                }
            }
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
