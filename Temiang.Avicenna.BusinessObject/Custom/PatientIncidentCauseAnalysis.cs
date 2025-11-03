namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientIncidentCauseAnalysis
    {
        public string IncidentCauseAnalysis
        {
            get { return GetColumn("refToAppStandardReferenceItem_IncidentCauseAnalysis").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_IncidentCauseAnalysis", value); }
        }
    }
}
