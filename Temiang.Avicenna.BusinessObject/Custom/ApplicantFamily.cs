using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ApplicantFamily
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
    }
}
