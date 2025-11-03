using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PositionWorkExperience
    {
        public string HRRequirementName
        {
            get { return GetColumn("refToHRRequirementName_PositionWorkExperience").ToString(); }
            set { SetColumn("refToHRRequirementName_PositionWorkExperience", value); }
        }

        public string LineBusinessName
        {
            get { return GetColumn("refToLineBusinessName_PositionWorkExperience").ToString(); }
            set { SetColumn("refToLineBusinessName_PositionWorkExperience", value); }
        }
    }
}
