namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitVitalSign
    {
        public string VitalSignName
        {
            get { return GetColumn("refToVitalSign_VitalSignName").ToString(); }
            set { SetColumn("refToVitalSign_VitalSignName", value); }
        }
    }
}
