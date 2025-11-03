namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientIncidentSafetyGoals
    {
        public string SafetyGoals
        {
            get { return GetColumn("refToAppStandardReferenceItem_SafetyGoals").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_SafetyGoals", value); }
        }
    }
}
