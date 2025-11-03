namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeIncentiveProcessItem
    {
        public string IncentiveServiceUnitGroupName
        {
            get { return GetColumn("refToAppStdRef_IncentiveServiceUnitGroupName").ToString(); }
            set { SetColumn("refToAppStdRef_IncentiveServiceUnitGroupName", value); }
        }
    }
}
