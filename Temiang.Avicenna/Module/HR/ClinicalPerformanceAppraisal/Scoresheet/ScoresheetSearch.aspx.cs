using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.CPA
{
    public partial class ScoresheetSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ClinicalPerformanceAppraisalScoresheet;

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new ClinicalPerformanceAppraisalQuestionnaireScoresheetQuery("a");
            var personal = new PersonalInfoQuery("b");
            var evaluator = new PersonalInfoQuery("c");
            var profession = new AppStandardReferenceItemQuery("d");
            var area = new AppStandardReferenceItemQuery("e");
            var level = new AppStandardReferenceItemQuery("f");

            query.InnerJoin(personal).On(personal.PersonID == query.PersonID);
            query.InnerJoin(evaluator).On(evaluator.PersonID == query.EvaluatorID);
            query.LeftJoin(profession).On(profession.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() & profession.ItemID == query.SRProfessionGroup);
            query.LeftJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString() & area.ItemID == query.SRClinicalWorkArea);
            query.LeftJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel.ToString() & level.ItemID == query.SRClinicalAuthorityLevel);
            query.Where(query.EvaluatorID == AppSession.UserLogin.PersonID.ToInt());
            query.OrderBy
                (
                    query.ScoringDate.Descending
                );

            query.Select(
                query.ScoresheetNo,
                query.ScoringDate,
                evaluator.EmployeeName.As("EvaluatorName"),
                query.PersonID,
                personal.EmployeeNumber,
                personal.EmployeeName,
                profession.ItemName.As("ProfessionGroupName"),
                area.ItemName.As("ClinicalWorkAreaName"),
                level.ItemName.As("ClinicalAuthorityLevelName"),
                query.IsApproved,
                query.IsVoid
                );

            if (!txtScoringDate.IsEmpty)
                query.Where(query.ScoringDate == txtScoringDate.SelectedDate);
            if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
            {
                string searchText = string.Format("%{0}%", txtEmployeeNo.Text);
                if (cboFilterEmployeeNo.SelectedIndex == 1)
                    query.Where(personal.EmployeeNumber == txtEmployeeNo.Text);
                else
                    query.Where(personal.EmployeeNumber.Like(searchText));
            }
            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                string searchText = string.Format("%{0}%", txtEmployeeName.Text);
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(personal.EmployeeName == txtEmployeeName.Text);
                else
                    query.Where(personal.EmployeeName.Like(searchText));
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}