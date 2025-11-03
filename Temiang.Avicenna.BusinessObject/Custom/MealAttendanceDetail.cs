using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MealAttendanceDetail
    {
        public string EmployeeName
        {
            get { return GetColumn("refToVwEmployeeTable_EmployeeName").ToString(); }
            set { SetColumn("refToVwEmployeeTable_EmployeeName", value); }
        }

        public string ServiceUnitName
        {
            get { return GetColumn("refToVwEmployeeTable_ServiceUnitName").ToString(); }
            set { SetColumn("refToVwEmployeeTable_ServiceUnitName", value); }
        }
    }
}
