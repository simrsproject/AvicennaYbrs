namespace Temiang.Avicenna.BusinessObject
{
    public partial class LaunderedProcessItemConsumption
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

        public bool IsInventoryItem
        {
            get { return (bool)GetColumn("refToItemProductNonMedic_IsInventoryItem"); }
            set { SetColumn("refToItemProductNonMedic_IsInventoryItem", value); }
        }

        public string ProductAccountId
        {
            get { return GetColumn("refToItem_ProductAccountId").ToString(); }
            set { SetColumn("refToItem_ProductAccountId", value); }
        }
    }
}
