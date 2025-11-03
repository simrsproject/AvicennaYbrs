namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemConsumption
    {
        public string DetailItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
    }
}
