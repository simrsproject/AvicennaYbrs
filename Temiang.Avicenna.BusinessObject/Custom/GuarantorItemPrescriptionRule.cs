namespace Temiang.Avicenna.BusinessObject
{
    public partial class GuarantorItemPrescriptionRule
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string GuarantorRuleTypeName
        {
            get { return GetColumn("refToSRItem_ItemName").ToString(); }
            set { SetColumn("refToSRItem_ItemName", value); }
        }
    }
}
