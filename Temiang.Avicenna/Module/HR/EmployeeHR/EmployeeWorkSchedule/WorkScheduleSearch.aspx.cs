using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class WorkScheduleSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.EmployeeWorkSchedule;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmployeeStatus, AppEnum.StandardReference.EmployeeStatus);
                StandardReference.InitializeIncludeSpace(cboSREmployeeType, AppEnum.StandardReference.EmployeeType);
                StandardReference.InitializeIncludeSpace(cboSRRemunerationType, AppEnum.StandardReference.RemunerationType);

                var unit = new OrganizationUnitCollection();
                unit.Query.Where(unit.Query.IsActive == true);
                if (unit.Query.Load())
                {
                    cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                    foreach (OrganizationUnit u in unit)
                    {
                        cboOrganizationUnit.Items.Add(new Telerik.Web.UI.RadComboBoxItem(u.OrganizationUnitName, u.OrganizationUnitID.ToString()));
                    }
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            OnButtonOkClick();

            return true;
        }

        private void OnButtonOkClick()
        {
            var query = new VwEmployeeTableQuery("a");
            var emplGrade = new PositionGradeQuery("i");
            var position = new PositionQuery("h");
            var unit = new OrganizationUnitQuery("g");
            var info = new EmployeeWorkingInfoQuery("f");
            var remuneration = new AppStandardReferenceItemQuery("e");
            var type = new AppStandardReferenceItemQuery("d");
            var status = new AppStandardReferenceItemQuery("c");
            var supervisor = new VwEmployeeTableQuery("b");
            var unit2 = new ServiceUnitQuery("x");

            query.LeftJoin(supervisor).On(query.SupervisorId == supervisor.PersonID);
            query.LeftJoin(status).On
                    (
                        query.SREmployeeStatus == status.ItemID &
                        status.StandardReferenceID == AppEnum.StandardReference.EmployeeStatus
                    );
            query.LeftJoin(type).On
                    (
                        query.SREmployeeType == type.ItemID &
                        type.StandardReferenceID == AppEnum.StandardReference.EmployeeType
                    );
            query.LeftJoin(remuneration).On
                    (
                        query.SRRemunerationType == remuneration.ItemID &
                        remuneration.StandardReferenceID == AppEnum.StandardReference.RemunerationType
                    );
            query.LeftJoin(info).On(query.PersonID == info.PersonID);
            query.LeftJoin(unit).On(query.OrganizationUnitID == unit.OrganizationUnitID);
            query.LeftJoin(position).On(query.PositionID == position.PositionID);
            query.LeftJoin(emplGrade).On(query.PositionGradeID == emplGrade.PositionGradeID);
            query.LeftJoin(unit2).On(query.ServiceUnitID == unit2.ServiceUnitID);

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                           query.PersonID,
                           query.EmployeeNumber,
                           query.EmployeeName,
                           supervisor.EmployeeName.As("SupervisorName"),
                           status.ItemName.As("EmployeeStatusName"),
                           type.ItemName.As("EmployeeTypeName"),
                           remuneration.ItemName.As("RemunerationTypeName"),
                           info.AbsenceCardNo,
                           query.JoinDate,
                           position.PositionName,
                           emplGrade.PositionGradeName,
                           info.LastUpdateDateTime,
                           info.LastUpdateByUserID
                        );

            query.Select(unit.OrganizationUnitName);
            query.Where(query.SREmploymentType != "0", query.SREmployeeStatus == "1");

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
            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(query.EmployeeName == txtEmployeeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(query.EmployeeName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSREmployeeStatus.SelectedValue))
                query.Where(query.SREmployeeStatus == cboSREmployeeStatus.SelectedValue);
            if (!string.IsNullOrEmpty(cboSREmployeeType.SelectedValue))
                query.Where(query.SREmployeeType == cboSREmployeeType.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRRemunerationType.SelectedValue))
                query.Where(query.SRRemunerationType == cboSRRemunerationType.SelectedValue);
            if (!string.IsNullOrEmpty(cboOrganizationUnit.SelectedValue))
                query.Where(query.ServiceUnitID == cboOrganizationUnit.SelectedValue);

            query.OrderBy(query.PersonID.Ascending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }
    }
}