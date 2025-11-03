namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaundryReceivedItemInfectious
    {
        public string ItemName
        {
            get { return GetColumn("refToItemLinen_ItemName").ToString(); }
            set { SetColumn("refToItemLinen_ItemName", value); }
        }

        public string ItemUnit
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemUnit").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemUnit", value); }
        }
    }
}
