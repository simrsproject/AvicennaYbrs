using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalWorkExperience
    {
        public string LineBisnisName
        {
            get { return GetColumn("refToLineBusinessName_PersonalWorkExperiences").ToString(); }
            set { SetColumn("refToLineBusinessName_PersonalWorkExperiences", value); }
        }
    }
}
