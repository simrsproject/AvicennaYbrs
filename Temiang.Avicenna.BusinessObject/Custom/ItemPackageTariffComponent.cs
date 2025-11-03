namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemPackageTariffComponent
    {
        public string TariffComponentName
        {
            get { return GetColumn("refToTariffComponent_TariffComponentName").ToString(); }
            set { SetColumn("refToTariffComponent_TariffComponentName", value); }
        }
    }
}
