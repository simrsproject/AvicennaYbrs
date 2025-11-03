namespace Temiang.Avicenna.BusinessObject
{
    public partial class NumberOfBedSmf
    {
        public string SmfName
        {
            get { return GetColumn("refToSmf_SmfName").ToString(); }
            set { SetColumn("refToSmf_SmfName", value); }
        }
    }
}
