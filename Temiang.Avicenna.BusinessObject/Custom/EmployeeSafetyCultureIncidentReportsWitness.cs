namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeSafetyCultureIncidentReportsWitness
    {
        public string WitnessName
        {
            get { return GetColumn("refToPersonalInfo_EmployeeName").ToString(); }
            set { SetColumn("refToPersonalInfo_EmployeeName", value); }
        }

        public string WitnessProfessionTypeName
        {
            get { return GetColumn("refToAppStdField_ProfessionType").ToString(); }
            set { SetColumn("refToAppStdField_ProfessionType", value); }
        }

        public string WitnessOrganizationName
        {
            get { return GetColumn("refToOrganization_Organization").ToString(); }
            set { SetColumn("refToOrganization_Organization", value); }
        }

        public string WitnessSubOrganizationName
        {
            get { return GetColumn("refToOrganization_SubOrganization").ToString(); }
            set { SetColumn("refToOrganization_SubOrganization", value); }
        }

        public string WitnessSubDivisonName
        {
            get { return GetColumn("refToOrganization_SubDivison").ToString(); }
            set { SetColumn("refToOrganization_SubDivison", value); }
        }

        public string WitnessServiceUnitName
        {
            get { return GetColumn("refToOrganization_ServiceUnit").ToString(); }
            set { SetColumn("refToOrganization_ServiceUnit", value); }
        }
    }
}
