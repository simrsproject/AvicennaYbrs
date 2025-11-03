namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemProductFabric
    {
        public string FabricName
        {
            get { return GetColumn("refToFabric_FabricName").ToString(); }
            set { SetColumn("refToFabric_FabricName", value); }
        }
    }
}
