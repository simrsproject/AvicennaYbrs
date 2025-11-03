namespace Temiang.Avicenna.BusinessObject
{
    public partial class PerformancePlanActivity
    {
        public string ActivityTime
        {
            get { return GetColumn("refTo_ActivityTime").ToString(); }
            set { SetColumn("refTo_ActivityTime", value); }
        }

        public string ActivityCategoryName
        {
            get { return GetColumn("refToAppStdRefItem_ActivityCategory").ToString(); }
            set { SetColumn("refToAppStdRefItem_ActivityCategory", value); }
        }
    }
}
