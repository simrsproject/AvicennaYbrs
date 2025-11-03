using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalBloodTypeSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryPersonalBloodType;//TODO: Isi ProgramID

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRBloodType, AppEnum.StandardReference.BloodType);
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PersonalInfoQuery("a");
            var std = new AppStandardReferenceItemQuery("b");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                        query.PersonID,
                        query.EmployeeNumber,
                        query.EmployeeName,
                        query.SRReligion,
                        std.ItemName.As("BloodTypeName")
                    );
            query.LeftJoin(std).On
            (
                query.SRBloodType == std.ItemID &
                std.StandardReferenceID == "BloodType"
            );

            query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya

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
            if (!string.IsNullOrEmpty(cboSRBloodType.SelectedValue))
                query.Where(query.SRBloodType == cboSRBloodType.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}