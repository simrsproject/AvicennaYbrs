using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PerformancePlanJptTx
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

        public bool IsOpenRealizationQuarter1
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenRealizationQuarter1")); }
            set { SetColumn("refToSchedule_IsOpenRealizationQuarter1", value); }
        }

        public bool IsOpenRealizationQuarter2
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenRealizationQuarter2")); }
            set { SetColumn("refToSchedule_IsOpenRealizationQuarter2", value); }
        }

        public bool IsOpenRealizationQuarter3
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenRealizationQuarter3")); }
            set { SetColumn("refToSchedule_IsOpenRealizationQuarter3", value); }
        }

        public bool IsOpenRealizationQuarter4
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenRealizationQuarter4")); }
            set { SetColumn("refToSchedule_IsOpenRealizationQuarter4", value); }
        }

        public bool IsOpenVerificationQuarter1
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenVerificationQuarter1")); }
            set { SetColumn("refToSchedule_IsOpenVerificationQuarter1", value); }
        }

        public bool IsOpenVerificationQuarter2
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenVerificationQuarter2")); }
            set { SetColumn("refToSchedule_IsOpenVerificationQuarter2", value); }
        }

        public bool IsOpenVerificationQuarter3
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenVerificationQuarter3")); }
            set { SetColumn("refToSchedule_IsOpenVerificationQuarter3", value); }
        }

        public bool IsOpenVerificationQuarter4
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenVerificationQuarter4")); }
            set { SetColumn("refToSchedule_IsOpenVerificationQuarter4", value); }
        }

        public bool IsOpenValidationQuarter1
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenValidationQuarter1")); }
            set { SetColumn("refToSchedule_IsOpenValidationQuarter1", value); }
        }

        public bool IsOpenValidationQuarter2
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenValidationQuarter2")); }
            set { SetColumn("refToSchedule_IsOpenValidationQuarter2", value); }
        }

        public bool IsOpenValidationQuarter3
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenValidationQuarter3")); }
            set { SetColumn("refToSchedule_IsOpenValidationQuarter3", value); }
        }

        public bool IsOpenValidationQuarter4
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenValidationQuarter4")); }
            set { SetColumn("refToSchedule_IsOpenValidationQuarter4", value); }
        }

        public bool IsOpenVerification
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenVerification")); }
            set { SetColumn("refToSchedule_IsOpenVerification", value); }
        }

        public bool IsOpenValidation
        {
            get { return Convert.ToBoolean(GetColumn("refToSchedule_IsOpenValidation")); }
            set { SetColumn("refToSchedule_IsOpenValidation", value); }
        }

        public string RealizationNotes
        {
            get { return GetColumn("refTo_RealizationNotes").ToString(); }
            set { SetColumn("refTo_RealizationNotes", value); }
        }

        public string ValidationNotes
        {
            get { return GetColumn("refTo_ValidationNotes").ToString(); }
            set { SetColumn("refTo_ValidationNotes", value); }
        }
    }
}
