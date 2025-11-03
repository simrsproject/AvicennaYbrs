using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class PositionLicense
    {
        public string HRRequirementName
        {
            get { return GetColumn("refToHRRequirementName_PositionLicense").ToString(); }
            set { SetColumn("refToHRRequirementName_PositionLicense", value); }
        }

        public string LicenseTypeName
        {
            get { return GetColumn("refToLicenseTypeName_PositionLicense").ToString(); }
            set { SetColumn("refToLicenseTypeName_PositionLicense", value); }
        }
    }
}
