using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ApplicantLicence
    {
        public string LicenceTypeName
        {
            get { return GetColumn("refTo_LicenceType").ToString(); }
            set { SetColumn("refTo_LicenceType", value); }
        }
    }
}
