using System;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport53
    {
        public string DiagnoseName
        {
            get { return GetColumn("refToDiagnose_DiagnoseName").ToString(); }
            set { SetColumn("refToDiagnose_DiagnoseName", value); }
        }

        public static void Process(int fromMonth, int toMonth, int year, string transactionNo, string dischargeConditionDieLessThen48, string dischargeConditionDieMoreThen48,
            string userId, RlTxReport53Collection coll, out RlTxReport53Collection newColl)
        {
            var reg = new RegistrationQuery("r");
            var epd = new EpisodeDiagnoseQuery("e");
            var dia = new DiagnoseQuery("d");
            var pat = new PatientQuery("p");

            reg.Select(
                dia.DiagnoseID,
                dia.DiagnoseName,
                string.Format(@"<CASE WHEN p.Sex = 'M' AND r.SRDischargeCondition IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarMatiL>", dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'M' AND r.SRDischargeCondition NOT IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarHidupL>", dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'F' AND r.SRDischargeCondition IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarMatiP>", dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48),
                string.Format(@"<CASE WHEN p.Sex = 'F' AND r.SRDischargeCondition NOT IN ('{0}', '{1}') 
                                      THEN COUNT(r.RegistrationNo) 
                                      ELSE 0 
                                 END AS KeluarHidupP>", dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48)
                );
            reg.InnerJoin(epd).On(reg.RegistrationNo == epd.RegistrationNo);
            reg.InnerJoin(dia).On(epd.DiagnoseID == dia.DiagnoseID);
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.Where(
                reg.SRRegistrationType == "IPR",
                reg.IsVoid == false,
                epd.SRDiagnoseType == "DiagnoseType-001"
                );
            reg.Where(dia.DiagnoseID.Substring(1, 1) != "Z", dia.DiagnoseID.Substring(1, 1) != "O",
                      dia.DiagnoseID.Substring(1, 1) != "V", dia.DiagnoseID.Substring(1, 1) != "W",
                      dia.DiagnoseID.Substring(1, 1) != "X", dia.DiagnoseID.Substring(1, 1) != "Y");
            reg.Where(string.Format("<MONTH(r.DischargeDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            reg.Where(string.Format("<YEAR(r.DischargeDate) = {0}>", year.ToString()));

            reg.GroupBy(
                dia.DiagnoseID,
                dia.DiagnoseName,
                pat.Sex,
                reg.SRDischargeCondition
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
                          KeluarHidupL = g.Sum(s => s.Field<int>("KeluarHidupL")),
                          KeluarHidupP = g.Sum(s => s.Field<int>("KeluarHidupP")),
                          KeluarMatiL = g.Sum(s => s.Field<int>("KeluarMatiL")),
                          KeluarMatiP = g.Sum(s => s.Field<int>("KeluarMatiP"))
                      };

            foreach (var row in dtb)
            {
                var entity = coll.AddNew();

                entity.RlTxReportNo = transactionNo;
                entity.DiagnosaID = row.DiagnoseID;
                entity.DiagnoseName = row.DiagnoseName;
                entity.KeluarHidupL = row.KeluarHidupL;
                entity.KeluarHidupP = row.KeluarHidupP;
                entity.KeluarMatiL = row.KeluarMatiL;
                entity.KeluarMatiP = row.KeluarMatiP;
                entity.Total = entity.KeluarHidupL + entity.KeluarHidupP + entity.KeluarMatiL + entity.KeluarMatiP;
                entity.LastUpdateByUserID = userId;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            newColl = coll;
        }
    }
}
