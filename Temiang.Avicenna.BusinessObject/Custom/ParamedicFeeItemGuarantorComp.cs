namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeeItemGuarantorComp
    {
        public string TariffComponentName
        {
            get { return GetColumn("refToTariffComponent_TariffComponentName").ToString(); }
            set { SetColumn("refToTariffComponent_TariffComponentName", value); }
        }
    }
}
