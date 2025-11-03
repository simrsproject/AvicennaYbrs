using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalFamilySearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryPersonalFamily; //TODO: Isi ProgramID

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRFamilyRelation, AppEnum.StandardReference.FamilyRelation);
                StandardReference.InitializeIncludeSpace(cboSRMaritalStatus, AppEnum.StandardReference.MaritalStatus);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var edu = new AppStandardReferenceItemQuery("g");
            var patient = new PatientQuery("f");
            var gender = new AppStandardReferenceItemQuery("e");
            var marital = new AppStandardReferenceItemQuery("d");
            var relation = new AppStandardReferenceItemQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new PersonalFamilyQuery("a");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select
                (
                   query.PersonalFamilyID,
                   query.PersonID,
                   personal.EmployeeNumber,
                   personal.EmployeeName,
                   query.PatientID,
                   patient.MedicalNo,
                   relation.ItemName.As("FamilyRelationName"),
                   query.FamilyName,
                   query.DateBirth,
                   edu.ItemName.As("EducationLevelName"),
                   query.Address,
                   query.Phone,
                   marital.ItemName.As("MaritalStatusName"),
                   gender.ItemName.As("GenderTypeName"),
                   query.IsGuaranteed,
                   query.LastUpdateByUserID,
                   query.LastUpdateDateTime
                );

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.LeftJoin(relation).On
                    (
                        query.SRFamilyRelation == relation.ItemID &
                        relation.StandardReferenceID == "FamilyRelation"
                    );
            query.LeftJoin(marital).On
                    (
                        query.SRMaritalStatus == marital.ItemID &
                        marital.StandardReferenceID == "MaritalStatus"
                    );
            query.LeftJoin(gender).On
                    (
                        query.SRGenderType == gender.ItemID &
                        gender.StandardReferenceID == "GenderType"
                    );

            query.LeftJoin(patient).On
                (
                    query.PatientID == patient.PatientID
                );
            query.LeftJoin(edu).On
                    (
                        query.SREducationLevel == edu.ItemID &
                        edu.StandardReferenceID == "EducationLevel"
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
            if (!string.IsNullOrEmpty(cboSRFamilyRelation.SelectedValue))
                query.Where(query.SRFamilyRelation == cboSRFamilyRelation.SelectedValue);
            if (!string.IsNullOrEmpty(txtFamilyName.Text))
            {
                if (cboFilterFamilyName.SelectedIndex == 1)
                    query.Where(query.FamilyName == txtFamilyName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFamilyName.Text);
                    query.Where(query.FamilyName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRMaritalStatus.SelectedValue))
                query.Where(query.SRMaritalStatus == cboSRMaritalStatus.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
