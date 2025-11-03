namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeSafetyCultureIncidentReportsSubject
    {
        public string SubjectName
        {
            get { return GetColumn("refToPersonalInfo_EmployeeName").ToString(); }
            set { SetColumn("refToPersonalInfo_EmployeeName", value); }
        }

        public string SubjectProfessionTypeName
        {
            get { return GetColumn("refToAppStdField_ProfessionType").ToString(); }
            set { SetColumn("refToAppStdField_ProfessionType", value); }
        }

        public string SubjectOrganizationName
        {
            get { return GetColumn("refToOrganization_Organization").ToString(); }
            set { SetColumn("refToOrganization_Organization", value); }
        }

        public string SubjectSubOrganizationName
        {
            get { return GetColumn("refToOrganization_SubOrganization").ToString(); }
            set { SetColumn("refToOrganization_SubOrganization", value); }
        }

        public string SubjectSubDivisonName
        {
            get { return GetColumn("refToOrganization_SubDivison").ToString(); }
            set { SetColumn("refToOrganization_SubDivison", value); }
        }

        public string SubjectServiceUnitName
        {
            get { return GetColumn("refToOrganization_ServiceUnit").ToString(); }
            set { SetColumn("refToOrganization_ServiceUnit", value); }
        }
    }
}
