namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialProcessLicense
    {
        public string LicenseTypeName
        {
            get { return GetColumn("refToAppStdField_LicenseType").ToString(); }
            set { SetColumn("refToAppStdField_LicenseType", value); }
        }
    }
}
