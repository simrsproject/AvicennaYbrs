using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class MedicalBenefitRuleDefinition : esMedicalBenefitRuleDefinition 
    {
        public string EmploymentTypeName
        {
            get { return GetColumn("refTo_EmploymentTypeName").ToString(); }
            set { SetColumn("refTo_EmploymentTypeName", value); }
        }

        public string EmployeeStatusName
        {
            get { return GetColumn("refTo_EmployeeStatusName").ToString(); }
            set { SetColumn("refTo_EmployeeStatusName", value); }
        }

        public string PositionGradeName
        {
            get { return GetColumn("refTo_PositionGradeName").ToString(); }
            set { SetColumn("refTo_PositionGradeName", value); }
        }

        public string EmployeeGradeName
        {
            get { return GetColumn("refTo_EmployeeGradeName").ToString(); }
            set { SetColumn("refTo_EmployeeGradeName", value); }
        }

        public string EmployeeName
        {
            get { return GetColumn("refTo_EmployeeName").ToString(); }
            set { SetColumn("refTo_EmployeeName", value); }
        }
    }
}
