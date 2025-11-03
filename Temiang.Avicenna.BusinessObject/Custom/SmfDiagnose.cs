namespace Temiang.Avicenna.BusinessObject
{
    public partial class SmfDiagnose
    {
        public string DiagnoseName
        {
            get { return GetColumn("refToDiagnose_DiagnoseName").ToString(); }
            set { SetColumn("refToDiagnose_DiagnoseName", value); }
        }

    }
}
