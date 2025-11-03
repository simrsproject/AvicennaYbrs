namespace Temiang.Avicenna.BusinessObject
{
    public partial class SupplierItem
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
        public string SRItemType
        {
            get { return GetColumn("refToItem_ItemType").ToString(); }
            set { SetColumn("refToItem_ItemType", value); }
        }
        public string SupplierName
        {
            get { return GetColumn("refToSupplier_SupplierName").ToString(); }
            set { SetColumn("refToSupplier_SupplierName", value); }
        }
    }
}
