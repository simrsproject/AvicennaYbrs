namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeSafetyCultureIncidentReportsMeetingParticipant
    {
        public string ParticipantName
        {
            get { return GetColumn("refToPersonalInfo_EmployeeName").ToString(); }
            set { SetColumn("refToPersonalInfo_EmployeeName", value); }
        }
    }
}
