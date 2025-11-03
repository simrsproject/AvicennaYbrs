using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeTrainingItem
    {
        public string ComponentName
        {
            get { return GetColumn("refToEmployeeTrainingItem_ComponentName").ToString(); }
            set { SetColumn("refToEmployeeTrainingItem_ComponentName", value); }
        }

    }
}
