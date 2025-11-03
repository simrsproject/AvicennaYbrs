namespace Temiang.Avicenna.BusinessObject
{
    public partial class SalaryComponentRuleMatrix
    {
        public string SalaryComponentCode
        {
            get { return GetColumn("refTo_SalaryComponentCode").ToString(); }
            set { SetColumn("refTo_SalaryComponentCode", value); }
        }

        public string SalaryComponentName
        {
            get { return GetColumn("refTo_SalaryComponentName").ToString(); }
            set { SetColumn("refTo_SalaryComponentName", value); }
        }

        public string OperandTypeName
        {
            get { return GetColumn("refTo_OperandTypeName").ToString(); }
            set { SetColumn("refTo_OperandTypeName", value); }
        }
    }
}