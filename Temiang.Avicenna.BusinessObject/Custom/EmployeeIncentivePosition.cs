namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeIncentivePosition
    {
        public string IncentiveServiceUnitGroupName
        {
            get { return GetColumn("refToAppStdRef_IncentiveServiceUnitGroupName").ToString(); }
            set { SetColumn("refToAppStdRef_IncentiveServiceUnitGroupName", value); }
        }
        public string IncentivePositionGroupName
        {
            get { return GetColumn("refToAppStdRef_IncentivePositionGroupName").ToString(); }
            set { SetColumn("refToAppStdRef_IncentivePositionGroupName", value); }
        }
        public string IncentivePositionName
        {
            get { return GetColumn("refToAppStdRef_IncentivePositionName").ToString(); }
            set { SetColumn("refToAppStdRef_IncentivePositionName", value); }
        }
    }
}
