using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeDisciplinary
    {
        public string WarningLevelName
        {
            get { return GetColumn("refToWarningLevelName_EmployeeDisciplinary").ToString(); }
            set { SetColumn("refToWarningLevelName_EmployeeDisciplinary", value); }
        }

        public string ViolationDegreeName
        {
            get { return GetColumn("refToViolationDegreeName_EmployeeDisciplinary").ToString(); }
            set { SetColumn("refToViolationDegreeName_EmployeeDisciplinary", value); }
        }

        public string ViolationTypeName
        {
            get { return GetColumn("refToViolationTypeName_EmployeeDisciplinary").ToString(); }
            set { SetColumn("refToViolationTypeName_EmployeeDisciplinary", value); }
        }
    }
}
