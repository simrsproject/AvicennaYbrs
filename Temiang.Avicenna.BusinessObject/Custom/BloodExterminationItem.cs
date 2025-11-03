using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class BloodExterminationItem
    {
        public string BloodType
        {
            get { return GetColumn("refToAppStandardReferenceItem_BloodType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_BloodType", value); }
        }

        public string BloodRhesus
        {
            get { return GetColumn("refToBloodBagNo_BloodRhesus").ToString(); }
            set { SetColumn("refToBloodBagNo_BloodRhesus", value); }
        }

        public string BloodGroup
        {
            get { return GetColumn("refToAppStandardReferenceItem_BloodGroup").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_BloodGroup", value); }
        }

        public decimal? VolumeBag
        {
            get { return Convert.ToDecimal(GetColumn("refBloodBagNo_VolumeBag")); }
            set { SetColumn("refBloodBagNo_VolumeBag", value); }
        }

        public string ExpiredDateTime
        {
            get { return GetColumn("refToBloodBagNo_ExpiredDateTime").ToString(); }
            set { SetColumn("refToBloodBagNo_ExpiredDateTime", value); }
        }
    }
}
