namespace Temiang.Avicenna.BusinessObject
{
    public partial class PpiRiskFactors
    {
        public string RiskFactorsTypeName
        {
            get { return GetColumn("refToAppStdRef_RiskFactorsType").ToString(); }
            set { SetColumn("refToAppStdRef_RiskFactorsType", value); }
        }

        public string RiskFactorsName
        {
            get { return GetColumn("refToRiskFactors_RiskFactorsName").ToString(); }
            set { SetColumn("refToRiskFactors_RiskFactorsName", value); }
        }

        public string RiskFactorsLocationName
        {
            get { return GetColumn("refToAppStdRef_RiskFactorsLocation").ToString(); }
            set { SetColumn("refToAppStdRef_RiskFactorsLocation", value); }
        }
    }
}
