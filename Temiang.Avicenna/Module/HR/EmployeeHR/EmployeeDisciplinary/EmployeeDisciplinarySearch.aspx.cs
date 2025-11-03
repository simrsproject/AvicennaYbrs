using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Employee
{
    public partial class EmployeeDisciplinarySearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = FormType == string.Empty ? AppConstant.Program.EmployeeDisciplinaryHistory : AppConstant.Program.EmployeeDisciplinary;
        }

        public override bool OnButtonOkClicked()
        {
            var warning = new AppStandardReferenceItemQuery("c");
            var personal = new VwEmployeeTableQuery("b");
            var query = new EmployeeDisciplinaryQuery("a");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                            query.EmployeeDisciplinaryID,
                            query.PersonID,
                            personal.EmployeeNumber,
                            personal.EmployeeName,
                            query.SRWarningLevel,
                            warning.ItemName.As("WarningLevelName"),
                            query.IncidentDate,
                            query.DateIssue,
                            query.Violation,
                            query.AdviceGiven,
                            query.SRViolationDegree,
                            query.SRViolationType,
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID
                        );

            query.LeftJoin(personal).On
                (
                    query.PersonID == personal.PersonID
                );
            query.LeftJoin(warning).On
                    (
                        query.SRWarningLevel == warning.ItemID &
                        warning.StandardReferenceID == "WarningLevel"
                    );
            
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
            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(personal.EmployeeName == txtEmployeeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(personal.EmployeeName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtWarningLevel.Text))
            {
                if (cboFilterWarningLevel.SelectedIndex == 1)
                    query.Where(warning.ItemName == txtWarningLevel.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtWarningLevel.Text);
                    query.Where(warning.ItemName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtViolation.Text))
            {
                if (cboFilterViolation.SelectedIndex == 1)
                    query.Where(query.Violation == txtViolation.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtViolation.Text);
                    query.Where(query.Violation.Like(searchTextContain));
                }
            }
            if (FormType == "sv")
            {
                query.Where(personal.SupervisorId == AppSession.UserLogin.PersonID);
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
