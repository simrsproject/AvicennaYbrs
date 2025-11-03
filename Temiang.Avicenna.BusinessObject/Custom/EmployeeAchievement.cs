using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeAchievement
    {
        public string AwardName
        {
            get { return GetColumn("refToAward_AwardName").ToString(); }
            set { SetColumn("refToAward_AwardName", value); }
        }

        public string AwardPrize
        {
            get { return GetColumn("refToAward_AwardPrize").ToString(); }
            set { SetColumn("refToAward_AwardPrize", value); }
        }
    }
}
