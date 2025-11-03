using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPaymentReceiptItem
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
        public string PrintReceiptAsName
        {
            get { return GetColumn("refToTransPayment_PrintReceiptAsName").ToString(); }
            set { SetColumn("refToTransPayment_PrintReceiptAsName", value); }
        }
        public string Notes
        {
            get { return GetColumn("refToTransPayment_Notes").ToString(); }
            set { SetColumn("refToTransPayment_Notes", value); }
        }
    }
}
