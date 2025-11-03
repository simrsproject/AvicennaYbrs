using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ApplicantContact
    {
        public string ContactTypeName
        {
            get { return GetColumn("refTo_ContactTypeName").ToString(); }
            set { SetColumn("refTo_ContactTypeName", value); }
        }
    }
}