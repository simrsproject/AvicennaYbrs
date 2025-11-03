using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalAddress
    {
        public string AddressTypeName
        {
            get { return GetColumn("refToAddressTypeName_PersonalAddress").ToString(); }
            set { SetColumn("refToAddressTypeName_PersonalAddress", value); }
        }

        public string StateName
        {
            get { return GetColumn("refToStateName_PersonalAddress").ToString(); }
            set { SetColumn("refToStateName_PersonalAddress", value); }
        }

        public string ZipPostalCode
        {
            get { return GetColumn("refToZipCode_ZipPostalCode").ToString(); }
            set { SetColumn("refToZipCode_ZipPostalCode", value); }
        }
    }
}
