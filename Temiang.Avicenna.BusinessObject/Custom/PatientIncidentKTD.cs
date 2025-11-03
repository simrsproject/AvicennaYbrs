namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientIncidentKTD
    {
        public string IncidentKTD
        {
            get { return GetColumn("refToAppStandardReferenceItem_IncidentKtd").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_IncidentKtd", value); }
        }
    }
}
