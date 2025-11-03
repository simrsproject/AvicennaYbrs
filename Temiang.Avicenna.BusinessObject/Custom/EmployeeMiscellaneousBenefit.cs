using System;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeMiscellaneousBenefit
    {
        public string MiscellaneousBenefitName
        {
            get { return GetColumn("refToMiscellaneousBenefit_EmployeeMiscellaneousBenefit").ToString(); }
            set { SetColumn("refToMiscellaneousBenefit_EmployeeMiscellaneousBenefit", value); }
        }

    }
}
