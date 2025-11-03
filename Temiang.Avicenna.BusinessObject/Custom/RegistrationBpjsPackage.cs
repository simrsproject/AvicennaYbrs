namespace Temiang.Avicenna.BusinessObject
{
    public partial class RegistrationBpjsPackage
    {
        public string PackageName
        {
            get { return GetColumn("refToBpjsPackage_PackageName").ToString(); }
            set { SetColumn("refToBpjsPackage_PackageName", value); }
        }
    }
}
