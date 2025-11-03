namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialCompetencyAssessmentEvaluator
    {
        public string CompetencyAssessmentEvalRoleName
        {
            get { return GetColumn("refToAppStdField_CompetencyAssessmentEvalRole").ToString(); }
            set { SetColumn("refToAppStdField_CompetencyAssessmentEvalRole", value); }
        }

        public string EvaluatorName
        {
            get { return GetColumn("refToPersonalInfo_EmployeeName").ToString(); }
            set { SetColumn("refToPersonalInfo_EmployeeName", value); }
        }
    }
}
