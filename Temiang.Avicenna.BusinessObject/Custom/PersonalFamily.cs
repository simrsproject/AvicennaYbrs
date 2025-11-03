using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalFamily
    {
        public string FamilyRelationName
        {
            get { return GetColumn("refTo_FamilyRelationName").ToString(); }
            set { SetColumn("refTo_FamilyRelationName", value); }
        }

        public string MaritalStatusName
        {
            get { return GetColumn("refTo_MaritalStatusName").ToString(); }
            set { SetColumn("refTo_MaritalStatusName", value); }
        }

        public string GenderTypeName
        {
            get { return GetColumn("refTo_GenderTypeName").ToString(); }
            set { SetColumn("refTo_GenderTypeName", value); }
        }

        public string EducationLevelName
        {
            get { return GetColumn("refTo_EducationLevelName").ToString(); }
            set { SetColumn("refTo_EducationLevelName", value); }
        }

        public string StateName
        {
            get { return GetColumn("refTo_StateName").ToString(); }
            set { SetColumn("refTo_StateName", value); }
        }

        public string CityName
        {
            get { return GetColumn("refTo_CityName").ToString(); }
            set { SetColumn("refTo_CityName", value); }
        }

        public string MedicalNo
        {
            get { return GetColumn("refToPatient_Medical").ToString(); }
            set { SetColumn("refToPatient_Medical", value); }
        }

        public string CoverageTypeName
        {
            get { return GetColumn("refTo_CoverageTypeName").ToString(); }
            set { SetColumn("refTo_CoverageTypeName", value); }
        }

        public string ZipPostalCode
        {
            get { return GetColumn("refToZipCode_ZipPostalCode").ToString(); }
            set { SetColumn("refToZipCode_ZipPostalCode", value); }
        }
        
        public string CoverageClassName
        {
            get { return GetColumn("refTo_CoverageClassName").ToString(); }
            set { SetColumn("refTo_CoverageClassName", value); }
        }

        public string CoverageClassBPJSName
        {
            get { return GetColumn("refTo_CoverageClassBPJSName").ToString(); }
            set { SetColumn("refTo_CoverageClassBPJSName", value); }
        }
    }
}
