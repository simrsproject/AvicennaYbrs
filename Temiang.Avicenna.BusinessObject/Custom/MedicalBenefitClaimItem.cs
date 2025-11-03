using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MedicalBenefitClaimItem
    {
        public string TreatedName
        {
            get { return GetColumn("refTo_TreatedName").ToString(); }
            set { SetColumn("refTo_TreatedName", value); }
        }

    }
}
