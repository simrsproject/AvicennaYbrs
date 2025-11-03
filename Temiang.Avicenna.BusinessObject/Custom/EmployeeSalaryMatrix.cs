using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeSalaryMatrix
    {
        public string SalaryComponentName
        {
            get { return GetColumn("refTo_SalaryComponentName").ToString(); }
            set { SetColumn("refTo_SalaryComponentName", value); }
        }
    }
}
