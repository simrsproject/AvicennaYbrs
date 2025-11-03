using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ClosingVisiteDownPaymentItem
    {
        public DateTime? PaymentDate
        {
            get { return (DateTime?)GetColumn("refToTransPayment_PaymentDate"); }
            set { SetColumn("refToTransPayment_PaymentDate", value); }
        }

        public string PaymentTime
        {
            get { return GetColumn("refToTransPayment_PaymentTime").ToString(); }
            set { SetColumn("refToTransPayment_PaymentTime", value); }
        }
    }
}
