namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceRoomAutoBillItem
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string ItemUnit
        {
            get { return GetColumn("refToItem_ItemUnit").ToString(); }
            set { SetColumn("refToItem_ItemUnit", value); }
        }
    }
}
