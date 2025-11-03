using System;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialInvitationItem
    {
        public DateTime? TransactionDate
        {
            get { return Convert.ToDateTime(GetColumn("refToCredentialProcess_TransactionDate")); }
            set { SetColumn("refToCredentialProcess_TransactionDate", value); }
        }

        public Int32? PersonID
        {
            get { return Convert.ToInt32(GetColumn("refToCredentialProcess_PersonID")); }
            set { SetColumn("refToCredentialProcess_PersonID", value); }
        }

        public string EmployeeNumber
        {
            get { return GetColumn("refToEmp_EmployeeNumber").ToString(); }
            set { SetColumn("refToEmp_EmployeeNumber", value); }
        }

        public string EmployeeName
        {
            get { return GetColumn("refToEmp_EmployeeName").ToString(); }
            set { SetColumn("refToEmp_EmployeeName", value); }
        }

        public string ProfessionGroupName
        {
            get { return GetColumn("refToAppStdField_ProfessionGroup").ToString(); }
            set { SetColumn("refToAppStdField_ProfessionGroup", value); }
        }

        public string ClinicalWorkAreaName
        {
            get { return GetColumn("refToAppStdField_ClinicalWorkArea").ToString(); }
            set { SetColumn("refToAppStdField_ClinicalWorkArea", value); }
        }

        public string ClinicalAuthorityLevelName
        {
            get { return GetColumn("refToAppStdField_ClinicalAuthorityLevel").ToString(); }
            set { SetColumn("refToAppStdField_ClinicalAuthorityLevel", value); }
        }
    }
}
