namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeOvertimeItem
    {
        public string EmployeeNumber
        {
            get { return GetColumn("refToPersonalInfo_EmployeeNumber").ToString(); }
            set { SetColumn("refToPersonalInfo_EmployeeNumber", value); }
        }

        public string EmployeeName
        {
            get { return GetColumn("refToPersonalInfo_EmployeeName").ToString(); }
            set { SetColumn("refToPersonalInfo_EmployeeName", value); }
        }

        public string SalaryComponentName
        {
            get { return GetColumn("refToSalaryComponent_SalaryComponentName").ToString(); }
            set { SetColumn("refToSalaryComponent_SalaryComponentName", value); }
        }
    }
}
