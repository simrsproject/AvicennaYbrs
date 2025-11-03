using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ApplicantWorkExperience
    {
        public string LineBisnisName
        {
            get { return GetColumn("refTo_ApplicantWorkExperiences").ToString(); }
            set { SetColumn("refTo_ApplicantWorkExperiences", value); }
        }
    }
}
