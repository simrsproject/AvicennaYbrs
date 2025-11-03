using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class SalaryComponentRuleDefinition
    {
        public string OrganizationUnitName
        {
            get { return GetColumn("refTo_OrganizationUnitName").ToString(); }
            set { SetColumn("refTo_OrganizationUnitName", value); }
        }

        public string EmployeeStatusName
        {
            get { return GetColumn("refTo_EmployeeStatusName").ToString(); }
            set { SetColumn("refTo_EmployeeStatusName", value); }
        }

        public string PositionName
        {
            get { return GetColumn("refTo_PositionName").ToString(); }
            set { SetColumn("refTo_PositionName", value); }
        }

        public string EmployeeName
        {
            get { return GetColumn("refTo_EmployeeName").ToString(); }
            set { SetColumn("refTo_EmployeeName", value); }
        }

        public string EmploymentTypeName
        {
            get { return GetColumn("refTo_EmploymentTypeName").ToString(); }
            set { SetColumn("refTo_EmploymentTypeName", value); }
        }

        public string PositionGradeName
        {
            get { return GetColumn("refTo_PositionGradeName").ToString(); }
            set { SetColumn("refTo_PositionGradeName", value); }
        }
        public string MaritalStatusName
        {
            get { return GetColumn("refTo_MaritalStatusName").ToString(); }
            set { SetColumn("refTo_MaritalStatusName", value); }
        }
        public string ReligionName
        {
            get { return GetColumn("refTo_ReligionName").ToString(); }
            set { SetColumn("refTo_ReligionName", value); }
        }
        
        public string EmployeeGradeName
        {
            get { return GetColumn("refTo_EmployeeGradeName").ToString(); }
            set { SetColumn("refTo_EmployeeGradeName", value); }
        }

        public string AttedanceMatrixName
        {
            get { return GetColumn("refTo_AttedanceMatrixName").ToString(); }
            set { SetColumn("refTo_AttedanceMatrixName", value); }
        }

        public string EducationLevelName
        {
            get { return GetColumn("refTo_EducationLevelName").ToString(); }
            set { SetColumn("refTo_EducationLevelName", value); }
        }

        public string EmployeeTypeName
        {
            get { return GetColumn("refTo_EmployeeTypeName").ToString(); }
            set { SetColumn("refTo_EmployeeTypeName", value); }
        }

        public string ServiceUnitName
        {
            get { return GetColumn("refTo_ServiceUnitName").ToString(); }
            set { SetColumn("refTo_ServiceUnitName", value); }
        }
    }
    
}
