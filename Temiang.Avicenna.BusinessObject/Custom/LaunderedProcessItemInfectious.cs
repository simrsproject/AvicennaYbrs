namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaunderedProcessItemInfectious
    {
        public string ItemID
        {
            get { return GetColumn("refToLaundryReceivedItemInfectious_ItemID").ToString(); }
            set { SetColumn("refToLaundryReceivedItemInfectious_ItemID", value); }
        }

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

        public string Notes
        {
            get { return GetColumn("refToLaundryReceivedItemInfectious_Notes").ToString(); }
            set { SetColumn("refToLaundryReceivedItemInfectious_Notes", value); }
        }

        public string FromServiceUnitID
        {
            get { return GetColumn("refToLaundryReceivedItemInfectious_FromServiceUnitID").ToString(); }
            set { SetColumn("refToLaundryReceivedItemInfectious_FromServiceUnitID", value); }
        }

        public string FromServiceUnitName
        {
            get { return GetColumn("refToLaundryReceivedItemInfectious_FromServiceUnitName").ToString(); }
            set { SetColumn("refToLaundryReceivedItemInfectious_FromServiceUnitName", value); }
        }
    }
}
