using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class MedicalRecordFileBorrowedSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MedicalRecordFileBorrowed;

            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new MedicalRecordFileBorrowedQuery("a");
            var pat = new PatientQuery("c");
            var reg = new RegistrationQuery("b");
            var su = new ServiceUnitQuery("d");
            var usr = new AppUserQuery("e");
            var usrg = new AppUserQuery("f");
            var sal = new AppStandardReferenceItemQuery("sal");

            query.InnerJoin(pat).On(query.PatientID == pat.PatientID);
            query.LeftJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
            query.LeftJoin(usr).On(query.NameOfRecipientID == usr.UserID);
            query.LeftJoin(usrg).On(query.NameOfGivenID == usrg.UserID);
            query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == pat.SRSalutation);
            query.Select
                (
                    query.TransactionNo,
                    query.PatientID,
                    query.RegistrationNo,
                    pat.MedicalNo,
                    pat.PatientName,
                    query.DateOfBorrowing,
                    query.DateOfReturn,
                    query.ServiceUnitID,
                    su.ServiceUnitName,
                    query.NameOfTheBorrower,
                    query.SRForThePurposesOf,
                    query.Notes,
                    query.NameOfRecipientID,
                    usr.UserName.As("ReceivedBy"),
                    usrg.UserName.As("GivenBy"),
                    query.Duration,
                    sal.ItemName.As("SalutationName"),
                    @"<CASE WHEN a.DateOfReturn IS NULL THEN (DATEDIFF(DAY, a.DateOfBorrowing, GETDATE())) - a.Duration ELSE 0 END AS 'OrderBy'>",
                    @"<CASE WHEN a.DateOfReturn IS NULL THEN CASE WHEN ((DATEDIFF(DAY, a.DateOfBorrowing, GETDATE())) - a.Duration) > 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END AS 'IsWarnedVisible'>",
                    @"<DATEADD(DAY, a.Duration, a.DateOfBorrowing) AS 'ShouldBeReturnDate'>",
                    @"<CASE WHEN a.DateOfReturn IS NULL THEN CASE WHEN ((DATEDIFF(DAY, a.DateOfBorrowing, GETDATE())) - a.Duration) > 0 THEN ((DATEDIFF(DAY, a.DateOfBorrowing, GETDATE())) - a.Duration) ELSE 0 END ELSE 0 END AS 'LoB'>"
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
            if (!string.IsNullOrEmpty(txtMedicalNo.Text))
            {
                if (cboFilterMedicalNo.SelectedIndex == 1)
                    query.Where(pat.MedicalNo == txtMedicalNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtMedicalNo.Text);
                    query.Where(pat.MedicalNo.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                if (cboFilterRegistrationNo.SelectedIndex == 1)
                    query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRegistrationNo.Text);
                    query.Where(query.RegistrationNo.Like(searchTextContain));
                }
            }
            if (!txtDateOfBorrowing.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.DateOfBorrowing == txtDateOfBorrowing.SelectedDate);
            }
            if (!txtDateOfReturn.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.DateOfReturn == txtDateOfReturn.SelectedDate);
            }
            if (!txtDueDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where("<DATEADD(day, a.Duration, a.DateOfBorrowing) = '"+ txtDueDate.SelectedDate.Value.ToString("yyyy-MM-dd") + "'>");
            }
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }
            
            query.Where(chkIsReturned.Checked ? query.DateOfReturn.IsNotNull() : query.DateOfReturn.IsNull());

            //query.OrderBy(query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
