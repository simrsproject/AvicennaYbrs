using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class WageTransactionItem
    {
        public string SalaryComponentCode
        {
            get { return GetColumn("refToSalaryComponent_SalaryComponentCode").ToString(); }
            set { SetColumn("refToSalaryComponent_SalaryComponentCode", value); }
        }

        public string SalaryComponentName
        {
            get { return GetColumn("refToSalaryComponent_SalaryComponentName").ToString(); }
            set { SetColumn("refToSalaryComponent_SalaryComponentName", value); }
        }

        public string SRSalaryType
        {
            get { return GetColumn("refToSalaryComponent_SRSalaryType").ToString(); }
            set { SetColumn("refToSalaryComponent_SRSalaryType", value); }
        }
    }
}
