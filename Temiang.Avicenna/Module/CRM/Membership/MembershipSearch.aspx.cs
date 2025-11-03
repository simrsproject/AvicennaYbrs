using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.CRM
{
    public partial class MembershipSearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["t"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = FormType == "g" ? AppConstant.Program.Membership : AppConstant.Program.MembershipEmployee;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new MembershipQuery("a");
            var pat = new PatientQuery("b");
            var mt = new AppStandardReferenceItemQuery("c");
            var sal = new AppStandardReferenceItemQuery("d");
            var emp = new PersonalInfoQuery("e");
            query.LeftJoin(pat).On(pat.PatientID == query.PatientID);
            query.LeftJoin(emp).On(emp.PersonID == query.PersonID);
            query.InnerJoin(mt).On(mt.StandardReferenceID == AppEnum.StandardReference.MembershipType && mt.ItemID == query.SRMembershipType);
            query.LeftJoin(sal).On(sal.StandardReferenceID == AppEnum.StandardReference.Salutation && sal.ItemID == pat.SRSalutation);
            query.Select(
                            query.MembershipNo,
                            query.JoinDate,
                            mt.ItemName.As("MembershipTypeName"),
                            query.PatientID,
                            pat.MedicalNo,
                            emp.EmployeeNumber,
                            sal.ItemName.As("SalutationName"),
                            query.MemberName,
                            query.Sex,
                            query.DateOfBirth,
                            query.Address,
                            query.PhoneNo,
                            query.MobilePhoneNo,
                            query.Email,
                            query.IsActive
                        );

            if (FormType == "g")
                query.Where(query.SRMembershipType == "01");
            else
                query.Where(query.SRMembershipType == "02");

            if (!string.IsNullOrEmpty(txtMembershipNo.Text))
            {
                if (cboFilterMembershipNo.SelectedIndex == 1)
                    query.Where(query.MembershipNo == txtMembershipNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMembershipNo.Text);
                    query.Where(query.MembershipNo.Like(searchTextContain));
                }
            }
            if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
            {
                query.Where(query.JoinDate >= txtFromDate.SelectedDate, query.JoinDate < txtToDate.SelectedDate.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(txtMemberName.Text))
            {
                if (cboFilterMemberName.SelectedIndex == 1)
                    query.Where(query.MemberName == txtMemberName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMemberName.Text);
                    query.Where(query.MemberName.Like(searchTextContain));
                }
            }
            if (!txtDoB.IsEmpty)
            {
                query.Where(query.Or(pat.DateOfBirth == txtDoB.SelectedDate, query.DateOfBirth == txtDoB.SelectedDate));
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

            query.OrderBy(query.MembershipNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}