using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalEmergencyContactSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryPersonalEmergency; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var city = new AppStandardReferenceItemQuery("f");
            var state = new AppStandardReferenceItemQuery("e");
            var addressType = new AppStandardReferenceItemQuery("d");
            var gender = new AppStandardReferenceItemQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new PersonalEmergencyContactQuery("a");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(query.PersonalEmergencyContactID,
                          query.PersonID,
                          personal.EmployeeNumber,
                          personal.EmployeeName,
                          query.Address,
                          state.ItemName.As("StateName"),
                          city.ItemName.As("CityName"),
                          query.ZipCode,
                          query.Phone,
                          query.Mobile,
                          query.LastUpdateByUserID,
                          query.LastUpdateDateTime
                           );

            query.InnerJoin(personal).On
                (
                    query.PersonID == personal.PersonID
                );

            query.LeftJoin(state).On
                (
                    query.SRState == state.ItemID &
                    state.StandardReferenceID == "Province"
                );
            query.LeftJoin(city).On
                (
                    query.SRCity == city.ItemID &
                    city.StandardReferenceID == "City"
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
            if (!string.IsNullOrEmpty(txtContactName.Text))
            {
                if (cboFilterContactName.SelectedIndex == 1)
                    query.Where(query.ContactName == txtContactName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtContactName.Text);
                    query.Where(query.ContactName.Like(searchTextContain));
                }
            }
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
            if (!string.IsNullOrEmpty(txtPhone.Text))
            {
                if (cboFilterPhone.SelectedIndex == 1)
                    query.Where(query.Phone == txtPhone.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPhone.Text);
                    query.Where(query.Phone.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtMobile.Text))
            {
                if (cboFilterMobile.SelectedIndex == 1)
                    query.Where(query.Mobile == txtMobile.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMobile.Text);
                    query.Where(query.Mobile.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
