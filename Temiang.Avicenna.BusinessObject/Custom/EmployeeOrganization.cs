using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeOrganization
    {
        public string OrganizationUnitName
        {
            get { return GetColumn("refToEmployeeOrganization_OrganizationUnitName").ToString(); }
            set { SetColumn("refToEmployeeOrganization_OrganizationUnitName", value); }
        }

        public string SubOrganizationUnitName
        {
            get { return GetColumn("refToEmployeeOrganization_SubOrganizationUnitName").ToString(); }
            set { SetColumn("refToEmployeeOrganization_SubOrganizationUnitName", value); }
        }

        public string SubDivisonName
        {
            get { return GetColumn("refToEmployeeOrganization_SubDivisonName").ToString(); }
            set { SetColumn("refToEmployeeOrganization_SubDivisonName", value); }
        }

        public string OrganizationLevelTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }

        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }
    }
}
