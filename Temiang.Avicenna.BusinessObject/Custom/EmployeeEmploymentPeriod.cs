using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeEmploymentPeriod
    {
        public string EmploymentTypeName
        {
            get { return GetColumn("refToEmploymentTypeName_EmployeeEmploymentPeriod").ToString(); }
            set { SetColumn("refToEmploymentTypeName_EmployeeEmploymentPeriod", value); }
        }

        public string EmploymentCategoryName
        {
            get { return GetColumn("refToEmploymentCategoryName_EmployeeEmploymentPeriod").ToString(); }
            set { SetColumn("refToEmploymentCategoryName_EmployeeEmploymentPeriod", value); }
        }

        public string RecruitmentPlanName
        {
            get { return GetColumn("refToRecruitmentPlan_RecruitmentPlanName").ToString(); }
            set { SetColumn("refToRecruitmentPlan_RecruitmentPlanName", value); }
        }
    }
}
