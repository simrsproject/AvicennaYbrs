namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemBalanceDetailEd
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string LocationName
        {
            get { return GetColumn("refToLocation_LocationName").ToString(); }
            set { SetColumn("refToLocation_LocationName", value); }
        }
    }
}
