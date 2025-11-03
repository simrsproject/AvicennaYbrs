using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeMedicalBenefit
    {
        public string MedicalBenefitName
        {
            get { return GetColumn("refTo_MedicalBenefitName").ToString(); }
            set { SetColumn("refTo_MedicalBenefitName", value); }
        }

        public string PeriodeName
        {
            get { return GetColumn("refTo_PeriodeName").ToString(); }
            set { SetColumn("refTo_PeriodeName", value); }
        }

    }
}