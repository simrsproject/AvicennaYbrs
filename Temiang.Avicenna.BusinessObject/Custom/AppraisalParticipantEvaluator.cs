using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject
{
   public partial class AppraisalParticipantEvaluator
    {
        public string EmployeeName
        {
            get { return GetColumn("refToVwEmployeeTableQuery_EmployeeName").ToString(); }
            set { SetColumn("refToVwEmployeeTableQuery_EmployeeName", value); }
        }

        public string ItemName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }
    }
}
