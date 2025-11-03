using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.KEHRS
{
    public partial class SafetyCultureIncidentReportsSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.KEHRS_SafetyCultureIncidentReports;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new EmployeeSafetyCultureIncidentReportsQuery("a");
            var qemp = new VwEmployeeTableQuery("b");
            var qusrVic = new AppUserQuery("c");
            var qusrSupervisor = new AppUserQuery("d");
            var qusr = new AppUserQuery("e");
            var qsubject = new EmployeeSafetyCultureIncidentReportsSubjectQuery("f");
            var qic = new AppStandardReferenceItemQuery("g");

            query.InnerJoin(qemp).On(qemp.PersonID == query.VictimPersonID);
            query.LeftJoin(qusrVic).On(qusrVic.PersonID == query.VictimPersonID && qusrVic.PersonID != -1);
            query.LeftJoin(qusrSupervisor).On(qusrSupervisor.PersonID == qemp.SupervisorId && qusrSupervisor.PersonID != -1);
            query.InnerJoin(qusr).On(qusr.UserID == AppSession.UserLogin.UserID);
            query.LeftJoin(qsubject).On(qsubject.TransactionNo == query.TransactionNo && qsubject.SubjectPersonID == qusr.PersonID && qusr.PersonID != -1);
            query.LeftJoin(qic).On(qic.StandardReferenceID == AppEnum.StandardReference.EmployeeAccidentReportStatus && qic.ItemID == query.SRIncidentReportStatus);

            query.es.Distinct = true;
            query.Select(
                query.TransactionNo,
                query.ReportDate,
                query.ReportDescription,
                query.VictimPersonID,
                qemp.EmployeeNumber,
                qemp.EmployeeName,
                query.IsApproved,
                query.IsVoid,
                query.IsVerified,
                query.VerifiedDateTime,
                qic.ItemName.As("IncidentReportStatus"),
                query.IsClosed,
                query.ClosedDateTime
                );

            query.Where
                (
                query.Or(
                    query.CreatedByUserID == AppSession.UserLogin.UserID,
                    qusrVic.UserID == AppSession.UserLogin.UserID,
                    qusrSupervisor.UserID == AppSession.UserLogin.UserID
                    ),
                qsubject.TransactionNo.IsNull()
                );

            query.OrderBy(query.TransactionNo.Descending);

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
            if (!txtReportDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.ReportDate >= txtReportDate.SelectedDate, query.ReportDate < txtReportDate.SelectedDate.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(txtReportDescription.Text))
            {
                if (cboFilterReportDescription.SelectedIndex == 1)
                    query.Where(query.ReportDescription == txtReportDescription.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtReportDescription.Text);
                    query.Where(query.ReportDescription.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(qemp.EmployeeName == txtEmployeeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(qemp.EmployeeName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(qemp.EmployeeName == txtEmployeeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(qemp.EmployeeName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}