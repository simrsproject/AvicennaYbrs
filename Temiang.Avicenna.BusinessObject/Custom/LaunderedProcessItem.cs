namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaunderedProcessItem
    {
        public string ItemID
        {
            get { return GetColumn("refToLaundryReceivedItem_ItemID").ToString(); }
            set { SetColumn("refToLaundryReceivedItem_ItemID", value); }
        }

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

        public string Notes
        {
            get { return GetColumn("refToLaundryReceivedItem_Notes").ToString(); }
            set { SetColumn("refToLaundryReceivedItem_Notes", value); }
        }
    }
}
