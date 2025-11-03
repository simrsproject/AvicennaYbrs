using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalOrganization
    {
        public string OrganizationTypeName
        {
            get { return GetColumn("OrganizationTypeName").ToString(); }
            set { SetColumn("OrganizationTypeName", value); }
        }

        public string OrganizationRoleName
        {
            get { return GetColumn("refToHR_OrganizationRoleName").ToString(); }
            set { SetColumn("refToHR_OrganizationRoleName", value); }
        }
    }
}
