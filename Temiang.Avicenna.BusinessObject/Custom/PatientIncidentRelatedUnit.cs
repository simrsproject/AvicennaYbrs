namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientIncidentRelatedUnit
    {
        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }
    }
}
