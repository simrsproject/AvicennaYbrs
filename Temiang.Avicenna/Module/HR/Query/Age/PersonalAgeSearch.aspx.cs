using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class PersonalAgeSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryPersonalAge; //TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new PersonalInfoQuery("a");
            query.Select(
                            query.PersonID,
                            query.EmployeeNumber,
                            query.EmployeeName,
                            query.BirthDate,
                            @"<DATEDIFF(YEAR,a.BirthDate, GETDATE()) AS AgeInYears>"
                        );
            query.OrderBy(query.PersonID.Ascending);

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
            if (!txtFromBirthDate.IsEmpty && !txtToBirthDate.IsEmpty)
            {
                query.Where(query.BirthDate >= txtFromBirthDate.SelectedDate, query.BirthDate <= txtToBirthDate.SelectedDate);
            }
            if ((txtAgeFrom.Value ?? 0) > 0 && (txtAgeTo.Value ?? 0) > 0)
            {
                query.Where($"<DATEDIFF(YEAR,a.BirthDate, GETDATE()) BETWEEN {txtAgeFrom.Value ?? 0} AND {txtAgeTo.Value ?? 0}>");
            }
            if (!txtBirthdayPeriodFrom.IsEmpty && !txtBirthdayPeriodTo.IsEmpty)
            {
                string from = string.Format("{0:00}", txtBirthdayPeriodFrom.SelectedDate.Value.Month) + string.Format("{0:00}", txtBirthdayPeriodFrom.SelectedDate.Value.Day);
                string to = string.Format("{0:00}", txtBirthdayPeriodTo.SelectedDate.Value.Month) + string.Format("{0:00}", txtBirthdayPeriodTo.SelectedDate.Value.Day);
                query.Where(string.Format(@"<RIGHT(CONVERT(varchar(8),a.BirthDate,112), 4) BETWEEN '{0}' AND '{1}' >", from, to));
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
