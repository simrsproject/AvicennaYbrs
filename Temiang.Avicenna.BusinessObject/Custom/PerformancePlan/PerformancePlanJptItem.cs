using System;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PerformancePlanJptItem
    {
        public string PerformancePlanDataSourceName
        {
            get { return GetColumn("refToAppStdRefItem_PerformancePlanDataSource").ToString(); }
            set { SetColumn("refToAppStdRefItem_PerformancePlanDataSource", value); }
        }

        public string SectionPerspectiveName
        {
            get { return GetColumn("refToAppStdRefItem_SectionPerspective").ToString(); }
            set { SetColumn("refToAppStdRefItem_SectionPerspective", value); }
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

        public static decimal GetYearTarget(decimal q1, decimal q2, decimal q3, decimal q4, string formula)
        {
            decimal retVal;
            var f = new AppStandardReferenceItem();
            if (f.LoadByPrimaryKey("RealizationFormula", formula))
            {
                switch (f.ItemID)
                {
                    case "01": //Rata-Rata
                        {
                            var i = 0;
                            if (q1 > 0)
                                i++;
                            if (q2 > 0)
                                i++;
                            if (q3 > 0)
                                i++;
                            if (q4 > 0)
                                i++;

                            if (i == 0)
                                retVal = 0;
                            else
                                retVal = (q1 + q2 + q3 + q4) / i;
                        }
                        break;

                    case "02": //Akumulasi
                        {
                            retVal = (q1 + q2 + q3 + q4);
                        }
                        break;

                    case "03": //Absolut
                        {
                            retVal = q4;
                        }
                        break;

                    default:
                        retVal = 0;
                        break;
                }
            }
            else
                retVal = 0;

            return retVal;
        }

        public static decimal GetAchievementsValue(decimal realization, decimal target, string formula)
        {
            decimal retVal;
            var f = new AppStandardReferenceItem();
            if (f.LoadByPrimaryKey("AchievementFormula", formula))
            {
                switch (f.ItemID)
                {
                    case "01": //Normal
                        {
                            retVal = realization * target / 100;
                        }
                        break;

                    case "02": //Invert
                        {
                            if (target == 0)
                                retVal = 0;
                            else
                                retVal = (((2 * target) - realization) / target * 100);
                        }
                        break;

                    case "03": //Zero
                        {
                            retVal = Convert.ToDecimal(Math.Exp(-50.00 * Convert.ToDouble(realization)) * 100);
                        }
                        break;

                    default:
                        retVal = 0;
                        break;
                }
            }
            else
                retVal = 0;

            return retVal;
        }
    }
}
