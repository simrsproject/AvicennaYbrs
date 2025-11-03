namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemTariffRequest2ItemComp
    {
        public string TariffComponentName
        {
            get { return GetColumn("refToTariffComponent_TariffComponentName").ToString(); }
            set { SetColumn("refToTariffComponent_TariffComponentName", value); }
        }
    }
}
