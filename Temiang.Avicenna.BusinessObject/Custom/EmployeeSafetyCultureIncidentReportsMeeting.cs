namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeSafetyCultureIncidentReportsMeeting
    {
        public string Participants
        {
            get { return GetColumn("refToEmployeeSafetyCultureIncidentReportsMeetingParticipant_Participants").ToString(); }
            set { SetColumn("refToEmployeeSafetyCultureIncidentReportsMeetingParticipant_Participants", value); }
        }
    }
}
