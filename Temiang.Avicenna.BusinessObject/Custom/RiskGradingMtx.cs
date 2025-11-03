namespace Temiang.Avicenna.BusinessObject
{
    public partial class RiskGradingMtx
    {
        public string IncidentProbabilityFrequency
        {
            get { return GetColumn("refToAppStandardReferenceItem_IncidentProbabilityFrequency").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_IncidentProbabilityFrequency", value); }
        }

        public string IncidentFollowUp
        {
            get { return GetColumn("refToAppStandardReferenceItem_IncidentFollowUp").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_IncidentFollowUp", value); }
        }

        public string RiskGradingName
        {
            get { return GetColumn("refToRiskGrading_RiskGradingName").ToString(); }
            set { SetColumn("refToRiskGrading_RiskGradingName", value); }
        }
    }
}
