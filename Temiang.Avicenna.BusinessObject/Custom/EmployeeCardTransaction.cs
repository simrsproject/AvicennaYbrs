using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeCardTransaction
    {
        public string EmployeeName
        {
            get { return GetColumn("refToVwEmployeeTable_EmployeeName").ToString(); }
            set { SetColumn("refToVwEmployeeTable_EmployeeName", value); }
        }
    }
}
