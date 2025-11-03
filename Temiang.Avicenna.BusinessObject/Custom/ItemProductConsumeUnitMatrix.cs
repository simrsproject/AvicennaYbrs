namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemProductConsumeUnitMatrix
    {
        public string ItemUnitName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemUnitName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemUnitName", value); }
        }
        public string ConsumeUnitName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ConsumeUnitName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ConsumeUnitName", value); }
        }
    }
}
