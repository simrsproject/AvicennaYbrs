namespace Temiang.Avicenna.BusinessObject
{
    public partial class MembershipItemRedemptionItem
    {
        public string ItemReedemName
        {
            get { return GetColumn("refToItemRedeem_ItemReedemName").ToString(); }
            set { SetColumn("refToItemRedeem_ItemReedemName", value); }
        }

        public string ItemReedemGroup
        {
            get { return GetColumn("refToItemRedeem_ItemReedemGroup").ToString(); }
            set { SetColumn("refToItemRedeem_ItemReedemGroup", value); }
        }
    }
}
