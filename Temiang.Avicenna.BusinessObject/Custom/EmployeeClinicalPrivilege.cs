namespace Temiang.Avicenna.BusinessObject
{
    public partial class EmployeeClinicalPrivilege
    {
        public string ProfessionGroupName
        {
            get { return GetColumn("refToStdRef_ProfessionGroup").ToString(); }
            set { SetColumn("refToStdRef_ProfessionGroup", value); }
        }

        public string ClinicalWorkAreaName
        {
            get { return GetColumn("refToStdRef_ClinicalWorkArea").ToString(); }
            set { SetColumn("refToStdRef_ClinicalWorkArea", value); }
        }

        public string ClinicalAuthorityLevelName
        {
            get { return GetColumn("refToStdRef_ClinicalAuthorityLevel").ToString(); }
            set { SetColumn("refToStdRef_ClinicalAuthorityLevel", value); }
        }
    }
}
