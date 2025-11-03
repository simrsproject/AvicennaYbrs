using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalEmergencyContact
    {
        public string FamilyRelationName
        {
            get { return GetColumn("refTo_FamilyRelationName").ToString(); }
            set { SetColumn("refTo_FamilyRelationName", value); }
        }
        public string StateName
        {
            get { return GetColumn("refToStateName_PersonalEmergencyContact").ToString(); }
            set { SetColumn("refToStateName_PersonalEmergencyContact", value); }
        }
        public string ZipPostalCode
        {
            get { return GetColumn("refToZipCode_ZipPostalCode").ToString(); }
            set { SetColumn("refToZipCode_ZipPostalCode", value); }
        }
    }
}
