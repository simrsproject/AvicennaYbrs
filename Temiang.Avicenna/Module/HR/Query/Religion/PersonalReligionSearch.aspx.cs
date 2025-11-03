using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalReligionSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryPersonalReligion; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var religion = new AppStandardReferenceItemQuery("b");
            var query = new PersonalInfoQuery("a");

            query.Select(
                        query.PersonID,
                        query.EmployeeNumber,
                        query.EmployeeName,
                        query.SRReligion,
                        religion.ItemName.As("ReligionName")
                    );
            query.LeftJoin(religion).On
            (
                query.SRReligion == religion.ItemID &
                religion.StandardReferenceID == "Religion"
            );


            if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
            {
                if (cboFilterEmployeeNumber.SelectedIndex == 1)
                    query.Where(query.EmployeeNumber == txtEmployeeNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeNo.Text);
                    query.Where(query.EmployeeNumber.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtFirstName.Text))
            {
                if (cboFirstName.SelectedIndex == 1)
                    query.Where(query.FirstName == txtFirstName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFirstName.Text);
                    query.Where(query.FirstName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtReligion.Text))
            {
                if (cboFilterReligion.SelectedIndex == 1)
                    query.Where(religion.ItemName == txtReligion.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtReligion.Text);
                    query.Where(religion.ItemName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.PersonID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
