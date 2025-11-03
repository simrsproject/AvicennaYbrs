namespace Temiang.Avicenna.BusinessObject
{
    public partial class GuarantorServiceUnitRule
    {
        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }
    }
}
