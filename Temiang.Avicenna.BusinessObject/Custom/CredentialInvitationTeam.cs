namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialInvitationTeam
    {
        public string TeamMemberName
        {
            get { return GetColumn("refToPersonalInfo_EmployeeName").ToString(); }
            set { SetColumn("refToPersonalInfo_EmployeeName", value); }
        }

        public string CredentialingTeamPositionName
        {
            get { return GetColumn("refToAppStdRefItem_CredentialingTeamPosition").ToString(); }
            set { SetColumn("refToAppStdRefItem_CredentialingTeamPosition", value); }
        }
    }
}
