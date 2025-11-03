using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class EmployeeEmploymentPeriodSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryEmploymentPeriod;//TODO: Isi ProgramID

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);
        }

        public override bool OnButtonOkClicked()
        {
            var employment = new AppStandardReferenceItemQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new EmployeeEmploymentPeriodQuery("a");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select
                (
                   query.EmployeeEmploymentPeriodID,
                   query.PersonID,
                   personal.EmployeeNumber,
                   personal.EmployeeName,
                   query.SREmploymentType,
                   employment.ItemName.As("EmploymentTypeName"),
                   query.ValidFrom,
                   query.ValidTo,
                   query.Note
                );

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.LeftJoin(employment).On
                    (
                        query.SREmploymentType == employment.ItemID &
                        employment.StandardReferenceID == "EmploymentType"
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
            if (!string.IsNullOrEmpty(cboSREmploymentType.SelectedValue))
                query.Where(query.SREmploymentType == cboSREmploymentType.SelectedValue);
            
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
