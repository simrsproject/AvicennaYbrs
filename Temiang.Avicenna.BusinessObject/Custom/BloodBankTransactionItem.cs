using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class BloodBankTransactionItem
    {
        public decimal? VolumeBag
        {
            get { return Convert.ToDecimal(GetColumn("refBloodBagNo_VolumeBag")); }
            set { SetColumn("refBloodBagNo_VolumeBag", value); }
        }

        public DateTime? ExpiredDateTime
        {
            get { return Convert.ToDateTime(GetColumn("refBloodBagNo_ExpiredDateTime")); }
            set { SetColumn("refBloodBagNo_ExpiredDateTime", value); }
        }

        public string BloodGroupReceived
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }

        public string CrossMatchingByUserName
        {
            get { return GetColumn("refToAppUser_CrossMatchingByUserName").ToString(); }
            set { SetColumn("refToAppUser_CrossMatchingByUserName", value); }
        }

        public string ExaminerByUserName
        {
            get { return GetColumn("refToAppUser_ExaminerByUserName").ToString(); }
            set { SetColumn("refToAppUser_ExaminerByUserName", value); }
        }

        public string BloodBagStatusName
        {
            get { return GetColumn("refToAppStandardReferenceItem_BloodBagStatus").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_BloodBagStatus", value); }
        }
    }
}
