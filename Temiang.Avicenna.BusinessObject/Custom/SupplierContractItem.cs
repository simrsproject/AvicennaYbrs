namespace Temiang.Avicenna.BusinessObject
{
    public partial class SupplierContractItem
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
    }
}
