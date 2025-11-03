using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalIdentification
    {
        public string IdentificationTypeName
        {
            get { return GetColumn("refToIdentificationTypeName_PersonalIdentification").ToString(); }
            set { SetColumn("refToIdentificationTypeName_PersonalIdentification", value); }
        }
    }
}
