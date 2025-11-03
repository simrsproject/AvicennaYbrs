namespace Temiang.Avicenna.BusinessObject
{
    public partial class RegistrationGuarantor
    {
        public string GuarantorName
        {
            get { return GetColumn("refToGuarantor_GuarantorName").ToString(); }
            set { SetColumn("refToGuarantor_GuarantorName", value); }
        }
    }
}
