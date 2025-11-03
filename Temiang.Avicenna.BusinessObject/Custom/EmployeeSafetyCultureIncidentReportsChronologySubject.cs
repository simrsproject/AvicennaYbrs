namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeSafetyCultureIncidentReportsChronologySubject
    {
        public string SubjectName
        {
            get { return GetColumn("refToPersonalInfo_EmployeeName").ToString(); }
            set { SetColumn("refToPersonalInfo_EmployeeName", value); }
        }
    }
}
