namespace Temiang.Avicenna.BusinessObject
{
    public partial class PerformancePlanNonJptItem
    {
        public string PerformancePlanActivityTypeName
        {
            get { return GetColumn("refToAppStdRefItem_PerformancePlanActivityType").ToString(); }
            set { SetColumn("refToAppStdRefItem_PerformancePlanActivityType", value); }
        }

        public string AchievementFormulaName
        {
            get { return GetColumn("refToAppStdRefItem_AchievementFormula").ToString(); }
            set { SetColumn("refToAppStdRefItem_AchievementFormula", value); }
        }

        public string RealizationFormulaName
        {
            get { return GetColumn("refToAppStdRefItem_RealizationFormula").ToString(); }
            set { SetColumn("refToAppStdRefItem_RealizationFormula", value); }
        }
    }
}
