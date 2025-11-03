using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalEducationHistory
    {
        public string EducationLevelName
        {
            get { return GetColumn("refToEducationLevelName_PersonalEducationHistory").ToString(); }
            set { SetColumn("refToEducationLevelName_PersonalEducationHistory", value); }
        }
    }
}
