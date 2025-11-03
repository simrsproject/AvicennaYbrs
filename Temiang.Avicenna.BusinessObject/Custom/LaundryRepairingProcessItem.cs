namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaundryRepairingProcessItem
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string ItemUnit
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemUnit").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemUnit", value); }
        }
    }
}
