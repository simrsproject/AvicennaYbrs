using System;
using System.Linq;
namespace Temiang.Avicenna.BusinessObject
{
    public partial class NursingAssessmentDiagnosa
    {
        private string _tmpID = string.Empty;
        public string NursingAssessmentName
        {
            get { return GetColumn("refToNursingAssessment_NursingAssessmentName").ToString(); }
            set { SetColumn("refToNursingAssessment_NursingAssessmentName", value); }
        }

        public string NursingDiagnosaName
        {
            get { return GetColumn("refToNursingDiagnosa_NursingDiagnosaName").ToString(); }
            set { SetColumn("refToNursingDiagnosa_NursingDiagnosaName", value); }
        }

        public string NsDiagnosaPrefixName
        {
            get { return GetColumn("refToAppStdRef_NsDiagnosaPrefixName").ToString(); }
            set { SetColumn("refToAppStdRef_NsDiagnosaPrefixName", value); }
        }

        public string NsDiagnosaSuffixName
        {
            get { return GetColumn("refToAppStdRef_NsDiagnosaSuffixName").ToString(); }
            set { SetColumn("refToAppStdRef_NsDiagnosaSuffixName", value); }
        }

        public string NsMandatoryLevelName
        {
            get { return GetColumn("refToAppStdRef_NsMandatoryLevelName").ToString(); }
            set { SetColumn("refToAppStdRef_NsMandatoryLevelName", value); }
        }
    }
}
