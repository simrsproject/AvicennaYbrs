namespace Temiang.Avicenna.BusinessObject
{
    public partial class HandHygieneItem
    {
        public string OpportunityName
        {
            get { return GetColumn("refToStdRef_Opportunity").ToString(); }
            set { SetColumn("refToStdRef_Opportunity", value); }
        }
        public string HandWashTypeName
        {
            get { return GetColumn("refToStdRef_HandWashType").ToString(); }
            set { SetColumn("refToStdRef_HandWashType", value); }
        }
        public string HandHygieneNoteName
        {
            get { return GetColumn("refToStdRef_HandHygieneNote").ToString(); }
            set { SetColumn("refToStdRef_HandHygieneNote", value); }
        }
        public string Apply6StepsResultName
        {
            get { return GetColumn("refToStdRef_Apply6StepsResult").ToString(); }
            set { SetColumn("refToStdRef_Apply6StepsResult", value); }
        }
    }
}
