using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PositionEmploymentCompany
    {
        public string HRRequirementName
        {
            get { return GetColumn("refToHRRequirementName_PositionWorkExperience").ToString(); }
            set { SetColumn("refToHRRequirementName_PositionWorkExperience", value); }
        }

        public string PositionGradeName
        {
            get { return GetColumn("refToPositionGradeName_PositionEmploymentCompany").ToString(); }
            set { SetColumn("refToPositionGradeName_PositionEmploymentCompany", value); }
        }
    }
}
