using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalLicence
    {
        public string LicenceTypeName
        {
            get { return GetColumn("refToLicenceTypeName_PersonalLicence").ToString(); }
            set { SetColumn("refToLicenceTypeName_PersonalLicence", value); }
        }
    }
}
