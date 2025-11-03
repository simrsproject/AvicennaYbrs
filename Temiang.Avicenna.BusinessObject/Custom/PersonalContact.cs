using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PersonalContact
    {
        public string ContactTypeName
        {
            get { return GetColumn("refToContactTypeName_PersonalContact").ToString(); }
            set { SetColumn("refToContactTypeName_PersonalContact", value); }
        }
    }
}
