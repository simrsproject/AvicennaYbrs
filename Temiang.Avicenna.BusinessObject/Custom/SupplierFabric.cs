namespace Temiang.Avicenna.BusinessObject
{
    public partial class SupplierFabric
    {
        public string SupplierName
        {
            get { return GetColumn("refToSupplier_SupplierName").ToString(); }
            set { SetColumn("refToSupplier_SupplierName", value); }
        }
    }
}
