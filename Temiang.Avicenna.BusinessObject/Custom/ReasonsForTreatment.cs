namespace Temiang.Avicenna.BusinessObject
{
    public partial class ReasonsForTreatment
    {
        public string DiagnoseName
        {
            get { return GetColumn("refToDiagnose_DiagnoseName").ToString(); }
            set { SetColumn("refToDiagnose_DiagnoseName", value); }
        }
    }
}
