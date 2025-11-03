namespace Temiang.Avicenna.BusinessObject
{
    public partial class BloodReceivedItem
    {
        public string BloodType
        {
            get { return GetColumn("refToAppStandardReferenceItem_BloodType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_BloodType", value); }
        }

        public string BloodGroup
        {
            get { return GetColumn("refToAppStandardReferenceItem_BloodGroup").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_BloodGroup", value); }
        }
    }
}
