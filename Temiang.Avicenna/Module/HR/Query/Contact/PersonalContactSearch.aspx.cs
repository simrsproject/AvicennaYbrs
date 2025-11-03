using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalContactSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryPersonalContact; //TODO: Isi ProgramID

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRContactType, AppEnum.StandardReference.ContactType);
        }

        public override bool OnButtonOkClicked()
        {

            var contactType = new AppStandardReferenceItemQuery("d");
            var gender = new AppStandardReferenceItemQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new PersonalContactQuery("a");

            query.Select(
                        query.PersonalContactID,
                        query.PersonID,
                        personal.EmployeeNumber,
                        personal.EmployeeName,
                        gender.ItemName.As("GenderTypeName"),
                        contactType.ItemName.As("ContactTypeName"),
                        query.SRContactType,
                        query.ContactValue,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                    );
            query.InnerJoin(personal).On
                (
                    query.PersonID == personal.PersonID
                );
            query.InnerJoin(gender).On
                (
                    personal.SRGenderType == gender.ItemID &
                    gender.StandardReferenceID == "GenderType"
                );
            query.InnerJoin(contactType).On
                (
                    query.SRContactType == contactType.ItemID &
                    contactType.StandardReferenceID == "ContactType"
                );

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
            if (!string.IsNullOrEmpty(cboSRContactType.SelectedValue))
                query.Where(query.SRContactType == cboSRContactType.SelectedValue);
            if (!string.IsNullOrEmpty(txtContactValue.Text))
            {
                if (cboFilterContactValue.SelectedIndex == 1)
                    query.Where(query.ContactValue == txtContactValue.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtContactValue.Text);
                    query.Where(query.ContactValue.Like(searchTextContain));
                }
            }

            query.OrderBy(query.PersonID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
