namespace Temiang.Avicenna.BusinessObject
{
    public partial class CompliancePatientFallRiskPreventionDetail
    {
        public string MedicalNo
        {
            get { return GetColumn("refToPatient_MedicalNo").ToString(); }
            set { SetColumn("refToPatient_MedicalNo", value); }
        }
    }
}
