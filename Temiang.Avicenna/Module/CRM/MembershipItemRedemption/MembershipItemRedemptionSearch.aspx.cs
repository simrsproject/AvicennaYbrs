using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.CRM
{
    public partial class MembershipItemRedemptionSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.MembershipItemRedemption;
        }

        public override bool OnButtonOkClicked()
        {
            var query = new MembershipItemRedemptionQuery("a");
            var mem = new MembershipQuery("b");
            var pat = new PatientQuery("c");
            var pat2 = new PatientQuery("d");
            var sal = new AppStandardReferenceItemQuery("e");
            query.InnerJoin(mem).On(mem.MembershipNo == query.MembershipNo);
            query.InnerJoin(pat).On(pat.PatientID == query.PatientID);
            query.InnerJoin(pat2).On(pat2.PatientID == mem.PatientID);
            query.LeftJoin(sal).On(sal.StandardReferenceID == AppEnum.StandardReference.Salutation && sal.ItemID == pat.SRSalutation);
            query.Select(
                            query.TransactionNo,
                            query.TransactionDate,
                            query.MembershipNo,
                            mem.JoinDate,
                            pat2.PatientName,
                            pat2.Address,
                            pat2.PhoneNo,
                            pat.PatientName.As("RedeemedBy"),
                            query.IsApproved, 
                            query.IsVoid
                        );
            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
            }
            if (!txtFromDate.IsEmpty && !txtToDate.IsEmpty)
            {
                query.Where(query.TransactionDate >= txtFromDate.SelectedDate, query.TransactionDate < txtToDate.SelectedDate.Value.AddDays(1));
            }
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
            if (!string.IsNullOrEmpty(txtPatientName.Text))
            {
                if (cboFilterPatientName.SelectedIndex == 1)
                    query.Where(pat2.PatientName == txtPatientName.Text);
                else
                {
                    string searchTextContain = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(d.FirstName + ' ' + d.MiddleName)) + ' ' + d.LastName) LIKE '{0}'>", searchTextContain)
                        );
                    //string searchTextContain = string.Format("%{0}%", txtPatientName.Text);
                    //query.Where(pat2.PatientName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtRedeemedBy.Text))
            {
                if (cboFilterRedeemedBy.SelectedIndex == 1)
                    query.Where(pat.PatientName == txtRedeemedBy.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRedeemedBy.Text);
                    query.Where(pat.PatientName.Like(searchTextContain));
                }
            }
            query.OrderBy(query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}