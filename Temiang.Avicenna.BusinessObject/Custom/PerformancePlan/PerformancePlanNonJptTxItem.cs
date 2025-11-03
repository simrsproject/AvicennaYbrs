using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PerformancePlanNonJptTxItem
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

        public string TimePeriod
        {
            get { return GetColumn("refTo_TimePeriod").ToString(); }
            set { SetColumn("refTo_TimePeriod", value); }
        }
    }
}
