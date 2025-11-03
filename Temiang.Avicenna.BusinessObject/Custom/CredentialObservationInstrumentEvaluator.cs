namespace Temiang.Avicenna.BusinessObject
{
    public partial class CredentialObservationInstrumentEvaluator
    {
        public string EvaluatorName
        {
            get { return GetColumn("refToPersonalInfo_EmployeeName").ToString(); }
            set { SetColumn("refToPersonalInfo_EmployeeName", value); }
        }
        public string CredentialEvaluatorNoName
        {
            get { return GetColumn("refToAppStdField_SRCredentialEvaluatorNo").ToString(); }
            set { SetColumn("refToAppStdField_SRCredentialEvaluatorNo", value); }
        }
        public string CredentialEvaluatorRoleName
        {
            get { return GetColumn("refToAppStdField_SRCredentialEvaluatorRole").ToString(); }
            set { SetColumn("refToAppStdField_SRCredentialEvaluatorRole", value); }
        }
    }
}
