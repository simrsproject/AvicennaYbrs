using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientMembershipDetail
    {
        public decimal Balance
        {
            get { return (decimal)GetColumn("refToBalance"); }
            set { SetColumn("refToBalance", value); }
        }
    }
}
