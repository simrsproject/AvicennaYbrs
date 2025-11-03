using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceFeeRemunRsucdrInvoices
    {
        public decimal Amount
        {
            get { return System.Convert.ToDecimal(GetColumn("refToIVI_Amount")); }
            set { SetColumn("refToIVI_Amount", value); }
        }
    }
}
