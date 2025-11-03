namespace Temiang.Avicenna.BusinessObject
{
    public partial class BloodTransformationItem
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

        public string BloodGroupFrom
        {
            get { return GetColumn("refToAppStandardReferenceItem_BloodGroupFrom").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_BloodGroupFrom", value); }
        }

        public string BloodGroupTo
        {
            get { return GetColumn("refToAppStandardReferenceItem_BloodGroupTo").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_BloodGroupTo", value); }
        }
    }
}
