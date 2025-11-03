using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class TransPaymentPatientItem
    {
        public string PaymentTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_PaymentType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_PaymentType", value); }
        }

        public string PaymentMethodName
        {
            get { return GetColumn("refToAppStandardReferenceItem_PaymentMethod").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_PaymentMethod", value); }
        }

        public string CardProviderName
        {
            get { return GetColumn("refToAppStandardReferenceItem_CardProvider").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_CardProvider", value); }
        }

        public string CardTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_CardType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_CardType", value); }
        }

        public string DiscountReasonName
        {
            get { return GetColumn("refToAppStandardReferenceItem_DiscountReason").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_DiscountReason", value); }
        }

        public Decimal Change
        {
            get { return Convert.ToDecimal(GetColumn("refToTransPaymentItem_Change")); }
            set { SetColumn("refToTransPaymentItem_Change", value); }
        }
    }
}
