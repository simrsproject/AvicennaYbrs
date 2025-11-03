namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemTariffRequest2Item
    {
        public string ClassName
        {
            get { return GetColumn("refToClass_ClassName").ToString(); }
            set { SetColumn("refToClass_ClassName", value); }
        }

        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }
    }
}
