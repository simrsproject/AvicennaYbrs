namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientIncidentUnderlyingCausesItemComponent
    {
        public string FactorName
        {
            get { return GetColumn("refToContributoryFactorsClassificationFramework_FactorName").ToString(); }
            set { SetColumn("refToContributoryFactorsClassificationFramework_FactorName", value); }
        }

        public string FactorItemName
        {
            get { return GetColumn("refToContributoryFactorsClassificationFrameworkItem_FactorItemName").ToString(); }
            set { SetColumn("refToContributoryFactorsClassificationFrameworkItem_FactorItemName", value); }
        }

        public string Component
        {
            get { return GetColumn("refToContributoryFactorsClassificationFrameworkItemComp_ComponentName").ToString(); }
            set { SetColumn("refToContributoryFactorsClassificationFrameworkItemComp_ComponentName", value); }
        }
    }
}
