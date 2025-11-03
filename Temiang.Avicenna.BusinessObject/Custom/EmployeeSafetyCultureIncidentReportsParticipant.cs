namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeSafetyCultureIncidentReportsParticipant
    {
        public string ParticipantName
        {
            get { return GetColumn("refToPersonalInfo_EmployeeName").ToString(); }
            set { SetColumn("refToPersonalInfo_EmployeeName", value); }
        }

        public string ParticipantStatusName
        {
            get { return GetColumn("refToAppStdRef_ParticipantStatus").ToString(); }
            set { SetColumn("refToAppStdRef_ParticipantStatus", value); }
        }
    }
}
