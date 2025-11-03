using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalLicenceSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryPersonalLicence ;//TODO: Isi ProgramID

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRLicenceType, AppEnum.StandardReference.LicenseType);
        }

        public override bool OnButtonOkClicked()
        {
            var licence = new AppStandardReferenceItemQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new PersonalLicenceQuery("a");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select
                (
                   query.PersonalLicenceID,
                   query.PersonID,
                   personal.EmployeeNumber,
                   personal.EmployeeName,
                   licence.ItemName.As("LicenceTypeName"),
                   licence.Note.As("LicenceTypeNote"),
                   query.ValidFrom,
                   query.ValidTo,
                   query.Note
                );

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.LeftJoin(licence).On
                    (
                        query.SRLicenceType == licence.ItemID &
                        licence.StandardReferenceID == "LicenseType"
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
            if (!string.IsNullOrEmpty(cboSRLicenceType.SelectedValue))
                query.Where(query.SRLicenceType == cboSRLicenceType.SelectedValue);
            
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
