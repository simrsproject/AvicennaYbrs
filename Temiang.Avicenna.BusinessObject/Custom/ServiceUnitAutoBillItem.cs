namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitAutoBillItem
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string ItemUnit
        {
            get { return GetColumn("refToAppStandardReferenceItem_SRItemUnit").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_SRItemUnit", value); }
        }
    }
}