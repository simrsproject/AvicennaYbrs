using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalWorkExperienceSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryPersonalWorkExperience; //TODO: Isi ProgramID

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRLineBisnis, AppEnum.StandardReference.LineBusiness);
        }

        public override bool OnButtonOkClicked()
        {
            var address = new AppStandardReferenceItemQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new PersonalWorkExperienceQuery("a");

            query.Select
                (
                   query.PersonalWorkExperienceID,
                   personal.EmployeeNumber,
                   personal.EmployeeName,
                   address.ItemName.As("LineBisnisName"),
                   query.StartDate,
                   query.EndDate,
                   query.Company,
                   query.Division,
                   query.Location,
                   query.JobDesc,
                   query.SupervisorName,
                   query.LastSalary,
                   query.ReasonOfLeaving
                );

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.LeftJoin(address).On
                    (
                        query.SRLineBisnis == address.ItemID &
                        address.StandardReferenceID == "LineBusiness"
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
            if (!string.IsNullOrEmpty(cboSRLineBisnis.SelectedValue))
                query.Where(query.SRLineBisnis == cboSRLineBisnis.SelectedValue);
            if (!string.IsNullOrEmpty(txtCompany.Text))
            {
                if (cboFilterCompany.SelectedIndex == 1)
                    query.Where(query.Company == txtCompany.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCompany.Text);
                    query.Where(query.Company.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
