using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class EmployeeLoanSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EmployeeLoan ;//TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var loan = new AppStandardReferenceItemQuery("c");
            var personal = new PersonalInfoQuery("b");
            var query = new EmployeeLoanQuery("a");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                            query.EmployeeLoanID,
                            query.PersonID,
                            personal.EmployeeNumber,
                            personal.EmployeeName,
                            query.LoanDate,
                            query.Amount,
                            query.SRPurposeOfLoan,
                            loan.ItemName.As("PurposeOfLoanName"),
                            query.NumberOfInstallment,
                            query.AmountOfInstallment,
                            query.StartPaymentPeriode,
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID,
                            query.CoverageAmount,
                            query.IsApproved
                        );
            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.InnerJoin(loan).On
                   (
                       query.SRPurposeOfLoan == loan.ItemID &
                       loan.StandardReferenceID == "PurposeOfLoan"
                   );
            query.OrderBy(query.LoanDate.Descending, query.PersonID.Ascending);

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
            if (!string.IsNullOrEmpty(txtLastName.Text))
            {
                if (cboLastName.SelectedIndex == 1)
                    query.Where(personal.LastName == txtLastName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtLastName.Text);
                    query.Where(personal.LastName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPurposeOfLoan.Text))
            {
                if (cboFilterSRPurposeOfLoan.SelectedIndex == 1)
                    query.Where(loan.ItemName == txtPurposeOfLoan.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPurposeOfLoan.Text);
                    query.Where(loan.ItemName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
