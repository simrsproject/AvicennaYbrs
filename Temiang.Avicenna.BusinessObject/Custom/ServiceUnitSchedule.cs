using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ServiceUnitSchedule
    {
        public string DayOfWeekName
        {
            get { return GetColumn("refToServiceUnitSchedule_DayOfWeekName").ToString(); }
            set { SetColumn("refToServiceUnitSchedule_DayOfWeekName", value); }
        }
    }
}
