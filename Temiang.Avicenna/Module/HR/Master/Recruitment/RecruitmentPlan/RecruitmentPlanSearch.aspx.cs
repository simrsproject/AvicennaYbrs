using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Recruitment.Master
{
    public partial class RecruitmentPlanSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RecruitmentPlan;//TODO: Isi ProgramID
        }

        public override bool OnButtonOkClicked()
        {
            var query = new RecruitmentPlanQuery("a");
            var depart = new OrganizationUnitQuery("b");
            var divi = new OrganizationUnitQuery("c");
            var subdiv = new OrganizationUnitQuery("d");
            var unit = new OrganizationUnitQuery("e");
            var posisi = new PositionQuery("f");
            query.LeftJoin(depart).On(query.OrganizationUnitID == depart.OrganizationUnitID);
            query.LeftJoin(divi).On(query.DivisionID == divi.OrganizationUnitID);
            query.LeftJoin(subdiv).On(query.SubDivisionID == subdiv.OrganizationUnitID);
            query.LeftJoin(unit).On(query.SectionID == unit.OrganizationUnitID);
            query.LeftJoin(posisi).On(query.PositionID == posisi.PositionID);
            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                            query.RecruitmentPlanID,
                            query.RecruitmentPlanName,
                            query.OrganizationUnitID,
                            depart.OrganizationUnitName.As("DepartmentName"),
                            query.DivisionID,
                            divi.OrganizationUnitName.As("DivisionName"),
                            query.SubDivisionID,
                            subdiv.OrganizationUnitName.As("SubDivisionName"),
                            query.SectionID,
                            unit.OrganizationUnitName.As("SectionName"),
                            posisi.PositionName,
                            query.PositionID,

                            query.ValidFrom,
                            query.ValidTo,
                            //query.NumberOfRequestedEmployees,
                            "<CAST(a.NumberOfRequestedEmployees AS VARCHAR(MAX)) + '/' + CAST(ISNULL((SELECT COUNT(*) FROM EmployeeEmploymentPeriod AS eep WHERE eep.RecruitmentPlanID = a.RecruitmentPlanID), 0) AS VARCHAR(MAX)) AS NumberOfRequestedEmployees>",
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID
                        );

            if (!string.IsNullOrEmpty(txtRecruitmentPlanName.Text))
            {
                if (cboFilterRecruitmentPlanName.SelectedIndex == 1)
                    query.Where(query.RecruitmentPlanName == txtRecruitmentPlanName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtRecruitmentPlanName.Text);
                    query.Where(query.RecruitmentPlanName.Like(searchTextContain));
                }
            }

            if (!string.IsNullOrEmpty(txtPositionName.Text))
            {
                if (cboFilterPositionName.SelectedIndex == 1)
                    query.Where(posisi.PositionName == txtPositionName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtPositionName.Text);
                    query.Where(posisi.PositionName.Like(searchTextContain));
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
