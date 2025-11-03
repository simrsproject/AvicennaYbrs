namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeePerformanceAppraisal
    {
        public string QuarterPeriodName
        {
            get { return GetColumn("refToAppStd_QuarterPeriod").ToString(); }
            set { SetColumn("refToAppStd_QuarterPeriod", value); }
        }
    }
}
