namespace Temiang.Avicenna.BusinessObject
{
    public partial class LocationTemplateItem
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string SRItemUnitName
        {
            get { return GetColumn("refToSRI_ItemUnit").ToString(); }
            set { SetColumn("refToSRI_ItemUnit", value); }
        }
    }
}
