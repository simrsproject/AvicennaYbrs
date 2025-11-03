namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaundryReturnedItem
    {
        public string ReceivedNo
        {
            get { return GetColumn("refToLaunderedProcessItem_ReceivedNo").ToString(); }
            set { SetColumn("refToLaunderedProcessItem_ReceivedNo", value); }
        }

        public string ReceivedSeqNo
        {
            get { return GetColumn("refToLaunderedProcessItem_ReceivedSeqNo").ToString(); }
            set { SetColumn("refToLaunderedProcessItem_ReceivedSeqNo", value); }
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
