namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemLinenItem
    {
        public string ItemDetailName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
    }
}
