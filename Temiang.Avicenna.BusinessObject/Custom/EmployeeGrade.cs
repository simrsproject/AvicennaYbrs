using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeGrade
    {
        public string EmployeeGradeName
        {
            get { return GetColumn("refToEmployeeGradeMaster_EmployeeGraderName").ToString(); }
            set { SetColumn("refToEmployeeGradeMaster_EmployeeGraderName", value); }
        }
    }
}
