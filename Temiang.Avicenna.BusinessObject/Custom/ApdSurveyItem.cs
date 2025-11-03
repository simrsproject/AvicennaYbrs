namespace Temiang.Avicenna.BusinessObject
{
    public partial class ApdSurveyItem
    {
        public string ApdTypeName
        {
            get { return GetColumn("refToStdReff_ApdType").ToString(); }
            set { SetColumn("refToStdReff_ApdType", value); }
        }
        public bool? IsIncorrectIndication
        {
            get { return (bool)GetColumn("refTo_IsIncorrectIndication"); }
            set { SetColumn("refTo_IsIncorrectIndication", value); }
        }
        public bool? IsIncorrectUsageTime
        {
            get { return (bool)GetColumn("refTo_IsIncorrectUsageTime"); }
            set { SetColumn("refTo_IsIncorrectUsageTime", value); }
        }
        public bool? IsIncorrectUsageTechnique
        {
            get { return (bool)GetColumn("refTo_IsIncorrectUsageTechnique"); }
            set { SetColumn("refTo_IsIncorrectUsageTechnique", value); }
        }
        public bool? IsIncorrectReleaseTechnique
        {
            get { return (bool)GetColumn("refTo_IsIncorrectReleaseTechnique"); }
            set { SetColumn("refTo_IsIncorrectReleaseTechnique", value); }
        }
    }
}
