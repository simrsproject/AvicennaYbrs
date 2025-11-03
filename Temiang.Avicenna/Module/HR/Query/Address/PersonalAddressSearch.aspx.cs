using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalAddressSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryPersonalAddress; //TODO: Isi ProgramID

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRAddressType, AppEnum.StandardReference.AddressType);
        }

        public override bool OnButtonOkClicked()
        {

            var state = new AppStandardReferenceItemQuery("e");
            var addressType = new AppStandardReferenceItemQuery("d");
            var gender = new AppStandardReferenceItemQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new PersonalAddressQuery("a");

            query.Select(query.PersonalAddressID,
                          query.PersonID,
                          personal.EmployeeNumber,
                          personal.FirstName,
                          personal.MiddleName,
                          personal.LastName,
                          personal.EmployeeName,
                          gender.ItemName.As("GenderTypeName"),
                          addressType.ItemName.As("AddressTypeName"),
                          query.District,
                          query.County,
                          query.City,
                          state.ItemName.As("StateName"),
                          query.ZipCode,
                          query.LastUpdateByUserID,
                          query.LastUpdateDateTime
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
            query.InnerJoin(addressType).On
                (
                    query.SRAddressType == addressType.ItemID &
                    addressType.StandardReferenceID == "AddressType"
                );
            query.LeftJoin(state).On
                (
                    query.SRState == state.ItemID &
                    state.StandardReferenceID == "Province"
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
            if (!string.IsNullOrEmpty(cboSRAddressType.SelectedValue))
                query.Where(query.SRAddressType == cboSRAddressType.SelectedValue);
            if (!string.IsNullOrEmpty(txtAddress.Text))
            {
                if (cboFilterAddress.SelectedIndex == 1)
                    query.Where(query.Address == txtAddress.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtAddress.Text);
                    query.Where(query.Address.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtSRState.Text))
            {
                if (cboFilterSRState.SelectedIndex == 1)
                    query.Where(state.ItemName == txtSRState.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtSRState.Text);
                    query.Where(state.ItemName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtCity.Text))
            {
                if (cboFilterSRCity.SelectedIndex == 1)
                    query.Where(query.City == txtCity.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtCity.Text);
                    query.Where(query.City.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtZipCode.Text))
            {
                if (cboFilterZipCode.SelectedIndex == 1)
                    query.Where(query.ZipCode == txtZipCode.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtZipCode.Text);
                    query.Where(query.ZipCode.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
