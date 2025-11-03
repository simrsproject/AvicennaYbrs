namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemConditionRule
    {
        public string ItemConditionRuleType
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }
    }
}
