namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPayment
    {
        public string RegistrationTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_RegistrationType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_RegistrationType", value); }
        }
    }
}
