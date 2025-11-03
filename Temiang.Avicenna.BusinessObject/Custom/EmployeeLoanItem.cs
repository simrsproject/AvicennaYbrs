using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeLoanItem
    {
        public string PayrollPeriodName
        {
            get { return GetColumn("refToPayrollPeriod_PayrollPeriodName").ToString(); }
            set { SetColumn("refToPayrollPeriod_PayrollPeriodName", value); }
        }
    }
}
