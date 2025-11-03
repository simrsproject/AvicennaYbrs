namespace Temiang.Avicenna.BusinessObject
{
    public partial class RiskManagementItem
    {
        public string RiskManagementCategoryName
        {
            get { return GetColumn("refToAppStandardReferenceItem_RiskManagementCategory").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_RiskManagementCategory", value); }
        }

        public string RiskManagementImpactName
        {
            get { return GetColumn("refToAppStandardReferenceItem_RiskManagementImpact").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_RiskManagementImpact", value); }
        }

        public string RiskManagementProbabilityName
        {
            get { return GetColumn("refToAppStandardReferenceItem_RiskManagementProbability").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_RiskManagementProbability", value); }
        }

        public string RiskManagementBandsName
        {
            get { return GetColumn("refToAppStandardReferenceItem_RiskManagementBands").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_RiskManagementBands", value); }
        }

        public string RiskManagementBandsColor
        {
            get { return GetColumn("refToAppStandardReferenceItem_RiskManagementBandsColor").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_RiskManagementBandsColor", value); }
        }

        public string RiskManagementControllingName
        {
            get { return GetColumn("refToAppStandardReferenceItem_RiskManagementControlling").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_RiskManagementControlling", value); }
        }
    }
}
