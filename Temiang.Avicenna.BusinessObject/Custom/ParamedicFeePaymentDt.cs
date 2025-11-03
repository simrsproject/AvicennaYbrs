using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicFeePaymentDt
    {
        public decimal? VerificationAmount
        {
            get { return (decimal?)GetColumn("refToParamedicFeeVerification_VerificationAmount"); }
            set { SetColumn("refToParamedicFeeVerification_VerificationAmount", value); }
        }

        public decimal? TaxAmount
        {
            get { return (decimal?)GetColumn("refToParamedicFeeVerification_TaxAmount"); }
            set { SetColumn("refToParamedicFeeVerification_TaxAmount", value); }
        }

        public DateTime? VerificationDate
        {
            get { return (DateTime?)GetColumn("refToParamedicFeeVerification_VerificationDate"); }
            set { SetColumn("refToParamedicFeeVerification_VerificationDate", value); }
        }
    }
}
