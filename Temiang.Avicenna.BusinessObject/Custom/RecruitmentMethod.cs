using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class RecruitmentMethod
    {
        public string RecruitmentMethodName
        {
            get { return GetColumn("refToRecruitmentMethod_RequestStatusName").ToString(); }
            set { SetColumn("refToRecruitmentMethod_RequestStatusName", value); }
        }
    }
}
