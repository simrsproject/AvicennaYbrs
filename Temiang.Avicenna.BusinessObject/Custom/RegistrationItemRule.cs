namespace Temiang.Avicenna.BusinessObject
{
    public partial class RegistrationItemRule
    {
        public string ItemName
        {
            get { return GetColumn("refToItem_ItemName").ToString(); }
            set { SetColumn("refToItem_ItemName", value); }
        }

        public string DiscountType
        {
            get { return GetColumn("refToAppStandardReferenceItem_DiscountType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_DiscountType", value); }
        }

        public string MarginType
        {
            get { return GetColumn("refToAppStandardReferenceItem_MarginType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_MarginType", value); }
        }

        public string FlavonType
        {
            get { return GetColumn("refToAppStandardReferenceItem_FlavonType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_FlavonType", value); }
        }

        public string GuarantorRuleTypeName
        {
            get { return GetColumn("refToSRItem_ItemName").ToString(); }
            set { SetColumn("refToSRItem_ItemName", value); }
        }
    }
}
