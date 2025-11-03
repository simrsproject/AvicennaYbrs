using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PositionEducation
    {
        public string HRRequirementName
        {
            get { return GetColumn("refToHRRequirementName_PositionEducation").ToString(); }
            set { SetColumn("refToHRRequirementName_PositionEducation", value); }
        }

        public string EducationLevelName
        {
            get { return GetColumn("refToEducationLevelName_PositionEducation").ToString(); }
            set { SetColumn("refToEducationLevelName_PositionEducation", value); }
        }

        public string EducationFieldName
        {
            get { return GetColumn("refToEducationFieldName_PositionEducation").ToString(); }
            set { SetColumn("refToEducationFieldName_PositionEducation", value); }
        }
    }
}
