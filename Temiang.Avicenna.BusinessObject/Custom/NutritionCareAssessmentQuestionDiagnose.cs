namespace Temiang.Avicenna.BusinessObject
{
    public partial class NutritionCareAssessmentQuestionDiagnose
    {
        public string TerminologyName
        {
            get { return GetColumn("refNutritionCareTerminology_TerminologyName").ToString(); }
            set { SetColumn("refNutritionCareTerminology_TerminologyName", value); }
        }
    }
}
