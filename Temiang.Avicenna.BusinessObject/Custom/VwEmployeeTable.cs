using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class VwEmployeeTable
    {
        public decimal AmountValue
        {
            get { return Convert.ToDecimal(GetColumn("refTo_AmountValue")); }
            set { SetColumn("refTo_AmountValue", value); }
        }

        public string PositionGradeName
        {
            get { return Convert.ToString(GetColumn("refToPositionGrade_PositionGradeName")); }
            set { SetColumn("refToPositionGrade_PositionGradeName", value); }
        }

        public decimal FromBasicSalaryAmount
        {
            get { return Convert.ToDecimal(GetColumn("refTo_FromBasicSalaryAmount")); }
            set { SetColumn("refTo_FromBasicSalaryAmount", value); }
        }

        public decimal ToBasicSalaryAmount
        {
            get { return Convert.ToDecimal(GetColumn("refTo_ToBasicSalaryAmount")); }
            set { SetColumn("refTo_ToBasicSalaryAmount", value); }
        }
    }
}
