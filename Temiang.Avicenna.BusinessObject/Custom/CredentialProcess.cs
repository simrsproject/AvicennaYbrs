using System;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialProcess
    {
        public string CredentialingConclusionName
        {
            get { return GetColumn("refToAppStdField_CredentialingConclusion").ToString(); }
            set { SetColumn("refToAppStdField_CredentialingConclusion", value); }
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

        public string DispositionDate
        {
            get { return GetColumn("refToCredentialDisposition_DispositionDate").ToString(); }
            set { SetColumn("refToCredentialDisposition_DispositionDate", value); }
        }

        public string DispositionReferenceNo
        {
            get { return GetColumn("refToCredentialDisposition_ReferenceNo").ToString(); }
            set { SetColumn("refToCredentialDisposition_ReferenceNo", value); }
        }
    }
}
