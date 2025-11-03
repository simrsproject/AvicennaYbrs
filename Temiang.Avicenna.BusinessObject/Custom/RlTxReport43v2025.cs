using System;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport43V2025
    {
        public string DiagnoseName
        {
            get { return GetColumn("refToDiagnose_DiagnoseName").ToString(); }
            set { SetColumn("refToDiagnose_DiagnoseName", value); }
        }

        public static void Process(int fromMonth, int toMonth, int year, string transactionNo, string dischargeConditionDieLessThen48, string dischargeConditionDieMoreThen48,
            string userId, RlTxReport43V2025Collection coll, out RlTxReport43V2025Collection newColl)
        {
            var reg = new RegistrationQuery("r");
            var epd = new EpisodeDiagnoseQuery("e");
            var dia = new DiagnoseQuery("d");
            var pat = new PatientQuery("p");

            reg.Select(
                dia.DiagnoseID,
                dia.DiagnoseName,
                string.Format(@"<CASE WHEN p.Sex = 'M' AND r.SRDischargeCondition NOT IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS HidupMatiL>", dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'F' AND r.SRDischargeCondition NOT IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS HidupMatiP>", dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'M' AND r.SRDischargeCondition IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarMatiL>", dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'F' AND r.SRDischargeCondition IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarMatiP>", dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48)
                );
            reg.InnerJoin(epd).On(reg.RegistrationNo == epd.RegistrationNo);
            reg.InnerJoin(dia).On(epd.DiagnoseID == dia.DiagnoseID);
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.Where(
                reg.SRRegistrationType == "IPR",
                reg.IsVoid == false,
                epd.SRDiagnoseType == "DiagnoseType-001",
                epd.DiagnoseID != string.Empty,
                dia.DiagnoseID.NotLike("Z%"),
                dia.DiagnoseID.NotLike("V%"),
                dia.DiagnoseID.NotLike("X%"),
                dia.DiagnoseID.NotLike("W%"),
                dia.DiagnoseID.NotLike("Y%"),
                dia.DiagnoseID.NotLike("R%"),
                dia.DiagnoseID.NotLike("080%"),
                dia.DiagnoseID.NotLike("082%")
                );
            //reg.Where(dia.DiagnoseID.Substring(0, 1) != "Z", dia.DiagnoseID.Substring(0, 3) != "O80", dia.DiagnoseID.Substring(0, 3) != "O82",
            //          dia.DiagnoseID.Substring(0, 1) != "V", dia.DiagnoseID.Substring(0, 1) != "W", dia.DiagnoseID.Substring(0, 1) != "R",
            //          dia.DiagnoseID.Substring(0, 1) != "X", dia.DiagnoseID.Substring(0, 1) != "Y");
            reg.Where(string.Format("<MONTH(r.DischargeDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            reg.Where(string.Format("<YEAR(r.DischargeDate) = {0}>", year.ToString()));

            reg.GroupBy(
                dia.DiagnoseID,
                dia.DiagnoseName,
                pat.Sex,
                reg.SRDischargeCondition
                );

            var dtb = (from i in reg.LoadDataTable().AsEnumerable()
                      group i by new
                      {
                          DiagnoseID = i.Field<string>("DiagnoseID"),
                          DiagnoseName = i.Field<string>("DiagnoseName")
                      } into g
                      select new
                      {
                          DiagnoseID = g.Key.DiagnoseID,
                          DiagnoseName = g.Key.DiagnoseName,
                          HidupMatiL = g.Sum(s => s.Field<int>("HidupMatiL")),
                          HidupMatiP = g.Sum(s => s.Field<int>("HidupMatiP")),
                          KeluarMatiL = g.Sum(s => s.Field<int>("KeluarMatiL")),
                          KeluarMatiP = g.Sum(s => s.Field<int>("KeluarMatiP"))
                      })
                      .Take(10);

            foreach (var row in dtb)
            {
                var entity = coll.AddNew();

                entity.RlTxReportNo = transactionNo;
                entity.DiagnosaID = row.DiagnoseID;
                entity.DiagnoseName = row.DiagnoseName;
                entity.HidupMatiL = row.HidupMatiL;
                entity.HidupMatiP = row.HidupMatiP;
                entity.TotalHidupMati = entity.HidupMatiL + entity.HidupMatiP;
                entity.KeluarMatiL = row.KeluarMatiL;
                entity.KeluarMatiP = row.KeluarMatiP;
                entity.TotalKeluarMati = entity.KeluarMatiL + entity.KeluarMatiP;
                entity.LastUpdateByUserID = userId;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            newColl = coll;
        }
    }
}
