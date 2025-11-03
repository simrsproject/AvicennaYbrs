namespace Temiang.Avicenna.BusinessObject
{
    public partial class PatientDocument
    {
        public string DocumentFolderYearly {
            get {
                var patID = PatientID.Trim();
                if(string.IsNullOrEmpty(patID)) return string.Empty;
                if(patID.Length < 5) return string.Empty; // minimal 5 digit dengan format Pxxxx dengan xxxx adalah identifikasi tahun
                if(!patID.StartsWith("P")) return string.Empty; // bukan format dari avicenna
                var sYear = PatientID.Substring(1, 4);
                var iYear = System.Convert.ToInt32(sYear);
                if(iYear < 2010) return string.Empty;
                return sYear;
            }
        }
    }
}
