namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemConditionRuleServiceUnit
    {
        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }
    }
}
