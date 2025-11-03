using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class AccidentsAndIncidentsReportsSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.K3RS_EmployeeIncident;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmployeeIncidentType, AppEnum.StandardReference.EmployeeIncidentType);
                StandardReference.InitializeIncludeSpace(cboSREmployeeInjuryCategory, AppEnum.StandardReference.EmployeeInjuryCategory);
                StandardReference.InitializeIncludeSpace(cboSREmployeeIncidentStatus, AppEnum.StandardReference.EmployeeIncidentStatus);
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
            var query = new EmployeeAccidentReportsQuery("a");
            var qemp = new VwEmployeeTableQuery("b");
            var qpos = new PositionQuery("c");
            var qic = new AppStandardReferenceItemQuery("d");
            var qis = new AppStandardReferenceItemQuery("e");
            var qit = new AppStandardReferenceItemQuery("f");
            var qsuv = new VwEmployeeTableQuery("g");

            query.InnerJoin(qemp).On(qemp.PersonID == query.PersonID);
            query.LeftJoin(qpos).On(qpos.PositionID == qemp.PositionID);
            query.LeftJoin(qic).On(qic.StandardReferenceID == AppEnum.StandardReference.EmployeeInjuryCategory && qic.ItemID == query.SREmployeeInjuryCategory);
            query.LeftJoin(qis).On(qis.StandardReferenceID == AppEnum.StandardReference.EmployeeIncidentStatus && qis.ItemID == query.SREmployeeIncidentStatus);
            query.LeftJoin(qit).On(qit.StandardReferenceID == AppEnum.StandardReference.EmployeeIncidentType && qit.ItemID == query.SREmployeeIncidentType);
            query.InnerJoin(qsuv).On(qsuv.PersonID == qemp.SupervisorId && qsuv.PersonID == AppSession.UserLogin.PersonID);

            query.OrderBy
                (
                    query.IncidentDateTime.Descending
                );

            query.Select(
                query.TransactionNo,
                query.ReportingDateTime,
                query.IncidentDateTime,
                query.PersonID,
                qemp.EmployeeNumber,
                qemp.EmployeeName,
                qpos.PositionName,
                qic.ItemName.As("EmployeeInjuryCategory"),
                qis.ItemName.As("EmployeeIncidentStatus"),
                qit.ItemName.As("EmployeeIncidentType"),
                query.IsApproved,
                query.IsVoid
                );

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchText));
                }
            }
            if (!txtIncidentDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.IncidentDateTime >= txtIncidentDate.SelectedDate, query.IncidentDateTime < txtIncidentDate.SelectedDate.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(txtEmployeeNumber.Text))
            {
                if (cboFilterEmployeeNumber.SelectedIndex == 1)
                    query.Where(qemp.EmployeeNumber == txtEmployeeNumber.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtEmployeeNumber.Text);
                    query.Where(qemp.EmployeeNumber.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(qemp.EmployeeName == txtEmployeeName.Text);
                else
                {
                    string searchText = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(qemp.EmployeeName.Like(searchText));
                }
            }
            if (!string.IsNullOrEmpty(cboSREmployeeIncidentType.SelectedValue))
            {
                query.Where(query.SREmployeeIncidentType == cboSREmployeeIncidentType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboSREmployeeInjuryCategory.SelectedValue))
            {
                query.Where(query.SREmployeeInjuryCategory == cboSREmployeeInjuryCategory.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboSREmployeeIncidentStatus.SelectedValue))
            {
                query.Where(query.SREmployeeIncidentStatus == cboSREmployeeIncidentStatus.SelectedValue);
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}