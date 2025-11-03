using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalEducationHistorySearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryPersonalEducationHistory;//TODO: Isi ProgramID

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSREducationLevel, AppEnum.StandardReference.EducationLevel);
        }

        public override bool OnButtonOkClicked()
        {
            var education = new AppStandardReferenceItemQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new PersonalEducationHistoryQuery("a");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select
                (
                   query.PersonalEducationHistoryID,
                   query.PersonID,
                   personal.EmployeeNumber,
                   personal.EmployeeName,
                   query.SREducationLevel,
                   education.ItemName.As("EducationLevelName"),
                   query.InstitutionName,
                   query.Location,
                   query.StartYear,
                   query.EndYear,
                   query.Gpa,
                   query.Achievement,
                   query.Note,
                   query.LastUpdateByUserID,
                   query.LastUpdateDateTime
                );

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.LeftJoin(education).On
                    (
                        query.SREducationLevel == education.ItemID &
                        education.StandardReferenceID == "EducationLevel"
                    );

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
            if (!string.IsNullOrEmpty(cboSREducationLevel.SelectedValue))
                query.Where(query.SREducationLevel == cboSREducationLevel.SelectedValue);
            if (!string.IsNullOrEmpty(txtInstitutionName.Text))
            {
                if (cboFilterInstitutionName.SelectedIndex == 1)
                    query.Where(query.InstitutionName == txtInstitutionName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtInstitutionName.Text);
                    query.Where(query.InstitutionName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
