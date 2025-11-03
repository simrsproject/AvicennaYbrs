namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeSafetyCultureIncidentReportsChronology
    {
        public string Subjects
        {
            get { return GetColumn("refToEmployeeSafetyCultureIncidentReportsChronologySubject_Subjects").ToString(); }
            set { SetColumn("refToEmployeeSafetyCultureIncidentReportsChronologySubject_Subjects", value); }
        }
    }
}
