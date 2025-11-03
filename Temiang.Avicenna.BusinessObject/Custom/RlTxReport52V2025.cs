using System;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport52V2025
    {
        public string DiagnoseName
        {
            get { return GetColumn("refToDiagnose_DiagnoseName").ToString(); }
            set { SetColumn("refToDiagnose_DiagnoseName", value); }
        }

        public static void Process(int fromMonth, int toMonth, int year, string transactionNo, string userId, RlTxReport52V2025Collection coll, out RlTxReport52V2025Collection newColl)
        {
            var reg = new RegistrationQuery("r");
            var epd = new EpisodeDiagnoseQuery("e");
            var dia = new DiagnoseQuery("d");
            var pat = new PatientQuery("p");

            reg.Select(
                dia.DiagnoseID,
                dia.DiagnoseName,
                @"<CASE WHEN p.Sex = 'M' AND e.IsOldCase = 0 
                        THEN COUNT(r.RegistrationNo) 
                        ELSE 0 
                   END AS KasusBaruL>",
                @"<CASE WHEN p.Sex = 'F' AND e.IsOldCase = 0  
                        THEN COUNT(r.RegistrationNo) 
                        ELSE 0 
                   END AS KasusBaruP>",
                @"<CASE WHEN p.Sex = 'M'
                        THEN COUNT(r.RegistrationNo) 
                        ELSE 0 
                   END AS KunjunganL>",
                @"<CASE WHEN p.Sex = 'M'
                        THEN COUNT(r.RegistrationNo) 
                        ELSE 0 
                   END AS KunjunganP>",
                string.Format(@"<(SELECT COUNT(r2.RegistrationNo) 
                                 FROM Registration r2
       		                     INNER JOIN EpisodeDiagnose AS e2 
       			                     ON e2.RegistrationNo = r2.RegistrationNo
       		                     INNER JOIN Patient AS p2 ON r2.PatientID = p2.PatientID
       		                     WHERE e2.DiagnoseID = d.DiagnoseID
       			                     AND r2.SRRegistrationType != '{0}'
				                     AND r2.IsVoid = 0
				                     AND (MONTH(r2.RegistrationDate) BETWEEN {1} AND {2})
				                     AND (YEAR(r2.RegistrationDate) = {3})
       			                     AND p2.Sex = p.Sex) AS JumlahKunjungan>",
                              "IPR",
                              fromMonth.ToString(),
                              toMonth.ToString(),
                              year.ToString())
                );
            reg.InnerJoin(epd).On(reg.RegistrationNo == epd.RegistrationNo);
            reg.InnerJoin(dia).On(epd.DiagnoseID == dia.DiagnoseID);
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.Where(
                reg.SRRegistrationType != "IPR",
                reg.IsVoid == false,
                epd.SRDiagnoseType == "DiagnoseType-001"
                );
            reg.Where(dia.DiagnoseID.Substring(1, 1) != "Z", dia.DiagnoseID.Substring(1, 1) != "O",
                      dia.DiagnoseID.Substring(1, 1) != "V", dia.DiagnoseID.Substring(1, 1) != "W",
                      dia.DiagnoseID.Substring(1, 1) != "X", dia.DiagnoseID.Substring(1, 1) != "Y");
            reg.Where(string.Format("<MONTH(r.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            reg.Where(string.Format("<YEAR(r.RegistrationDate) = {0}>", year.ToString()));
            reg.GroupBy(
                dia.DiagnoseID,
                dia.DiagnoseName,
                pat.Sex,
                epd.IsOldCase
                );

            var dtb = from i in reg.LoadDataTable().AsEnumerable()
                      group i by new
                      {
                          DiagnoseID = i.Field<string>("DiagnoseID"),
                          DiagnoseName = i.Field<string>("DiagnoseName")
                      } into g
                      select new
                      {
                          DiagnoseID = g.Key.DiagnoseID,
                          DiagnoseName = g.Key.DiagnoseName,
                          KasusBaruL = g.Sum(s => s.Field<int>("KasusBaruL")),
                          KasusBaruP = g.Sum(s => s.Field<int>("KasusBaruP")),
                          KunjunganL = g.Sum(s => s.Field<int>("KunjunganL")),
                          KunjunganP = g.Sum(s => s.Field<int>("KunjunganP")),
                          JumlahKunjungan = g.Sum(s => s.Field<int>("JumlahKunjungan"))
                      };

            foreach (var row in dtb)
            {
                var entity = coll.AddNew();

                entity.RlTxReportNo = transactionNo;
                entity.DiagnosaID = row.DiagnoseID;
                entity.DiagnoseName = row.DiagnoseName;
                entity.KasusBaruL = row.KasusBaruL;
                entity.KasusBaruP = row.KasusBaruP;
                entity.JumlahKasusBaru = entity.KasusBaruL + entity.KasusBaruP;
                entity.KunjunganL = row.KunjunganL;
                entity.KunjunganP = row.KunjunganP;
                entity.JumlahKunjungan = row.JumlahKunjungan;
                entity.LastUpdateByUserID = userId;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            newColl = coll;
        }
    }
}
