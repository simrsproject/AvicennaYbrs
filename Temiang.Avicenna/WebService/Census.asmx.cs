using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for Census
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Census : System.Web.Services.WebService
    {
        [WebMethod]
        public string Execute()
        {
            using (var trans = new esTransactionScope())
            {
                var units = new ServiceUnitCollection();
                units.Query.Where(
                    units.Query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    units.Query.IsActive == true
                    );
                units.Query.Load();

                var cls = new ClassCollection();
                cls.Query.Where(
                    cls.Query.IsInPatientClass == true,
                    cls.Query.IsActive == true
                    );
                cls.Query.Load();

                var cbs = new CensusBalanceCollection();

                foreach (var unit in units)
                {
                    foreach (var cl in cls)
                    {
                        cbs = new CensusBalanceCollection();
                        cbs.Query.Where(
                            cbs.Query.CensusDate == DateTime.Now.Date,
                            cbs.Query.ServiceUnitID == unit.ServiceUnitID,
                            cbs.Query.ClassID == cl.ClassID
                            );
                        cbs.Query.Load();

                        cbs.MarkAllAsDeleted();
                        cbs.Save();

                        var dtb = Census1(DateTime.Now.Date, unit.ServiceUnitID, cl.ClassID);
                        foreach (DataRow row in dtb.Rows)
                        {
                            //prev
                            var cb = new CensusBalance();
                            if (!cb.LoadByPrimaryKey(DateTime.Now.Date.AddDays(-1).Date, cl.ClassID, unit.ServiceUnitID, row["SmfID"].ToString()))
                            {
                                cb = new CensusBalance();
                                cb.CensusDate = DateTime.Now.Date.AddDays(-1).Date;
                                cb.ServiceUnitID = cl.ClassID;
                                cb.ClassID = unit.ServiceUnitID;
                                cb.SmfID = row["SmfID"].ToString();
                            }
                            cb.Balance = int.Parse(row["Sebelumnya"].ToString());
                            cb.LastUpdateDateTime = DateTime.Now;
                            cb.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            cb.Save();

                            //next
                            cb = new CensusBalance();
                            if (!cb.LoadByPrimaryKey(DateTime.Now.Date, cl.ClassID, unit.ServiceUnitID, row["SmfID"].ToString()))
                            {
                                cb = new CensusBalance();
                                cb.CensusDate = DateTime.Now.Date;
                                cb.ServiceUnitID = cl.ClassID;
                                cb.ClassID = unit.ServiceUnitID;
                                cb.SmfID = row["SmfID"].ToString();
                            }
                            cb.Balance = (int.Parse(row["Sebelumnya"].ToString()) + (int.Parse(row["Masuk"].ToString()) + int.Parse(row["Pindahan"].ToString()))) -
                                (int.Parse(row["Hidup"].ToString()) + int.Parse(row["Meninggal"].ToString()) + int.Parse(row["Dipindahkan"].ToString()));
                            cb.LastUpdateDateTime = DateTime.Now;
                            cb.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            cb.Save();
                        }
                    }
                }

                //clear zero balance
                cbs = new CensusBalanceCollection();
                cbs.Query.Where(cbs.Query.Balance == 0);
                cbs.Query.Load();

                cbs.MarkAllAsDeleted();
                cbs.Save();

                trans.Complete();
            }

            return "Success";
        }

        private DataTable Census1(DateTime censusDate, string serviceUnitID, string classID)
        {
            var smf = new SmfQuery("s");
            smf.Select(
                string.Format("<'{0}' AS CensusDate>", censusDate,
                string.Format("<'{0}' AS ServiceUnitID>", serviceUnitID),
                string.Format("<'{0}' AS ClassID>", classID),
                smf.SmfID,
                smf.SmfName,
                string.Format(@"<CAST(ISNULL((SELECT cb.Balance FROM CensusBalance AS cb WHERE cb.CensusDate = '{0}' AND cb.ServiceUnitID = '{1}' AND cb.ClassID = '{2}' AND cb.SmfID = s.SmfID), 0) AS VARCHAR(MAX)) AS Sebelumnya>",
                                censusDate.AddDays(-1).ToShortDateString(), serviceUnitID, classID),
                string.Format(@"<CAST((SELECT COUNT(*) FROM PatientTransferHistory AS r WHERE r.DateOfEntry = '{0}' AND r.ServiceUnitID = '{1}' {2} AND r.SmfID = s.SmfID AND r.TransferNo = '') AS VARCHAR(MAX)) AS Masuk>",
                                censusDate.ToShortDateString(), serviceUnitID, string.IsNullOrEmpty(classID) ? string.Empty : string.Format("AND r.ChargeClassID = '{0}'", classID)),
                "<'0' AS Pindahan>",
                "<'0' AS Jumlah345>",
                string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition NOT IN ('{1}', '{2}') AND r.ServiceUnitID = '{3}' {4} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Hidup>",
                                censusDate.ToShortDateString(), AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48,
                                serviceUnitID, string.IsNullOrEmpty(classID) ? string.Empty : string.Format("AND r.ChargeClassID = '{0}'", classID)),
                string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition IN ('{1}', '{2}') AND r.ServiceUnitID = '{3}' {4} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Meninggal>",
                                censusDate.ToShortDateString(), AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48, serviceUnitID,
                                string.IsNullOrEmpty(classID) ? string.Empty : string.Format("AND r.ChargeClassID = '{0}'", classID)),
                string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition IN ('{1}') AND r.ServiceUnitID = '{2}' {3} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Below48>",
                                censusDate.ToShortDateString(), AppSession.Parameter.DischargeConditionDieLessThen48, serviceUnitID,
                                string.IsNullOrEmpty(classID) ? string.Empty : string.Format("AND r.ChargeClassID = '{0}'", classID)),
                string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition IN ('{1}') AND r.ServiceUnitID = '{2}' {3} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Over48>",
                                censusDate.ToShortDateString(), AppSession.Parameter.DischargeConditionDieMoreThen48, serviceUnitID,
                                string.IsNullOrEmpty(classID) ? string.Empty : string.Format("AND r.ChargeClassID = '{0}'", classID)),
                "<'0' AS Dipindahkan>",
                "<'0' AS Jumlah7811>",
                "<'0' AS Dirawat>"
                )
            );

            var tab1 = smf.LoadDataTable();

            foreach (DataRow row in tab1.Rows)
            {
                row["Pindahan"] = SelectCountPindahan(row["CensusDate"].ToString(), row["ServiceUnitID"].ToString(), row["ClassID"].ToString(), row["SmfID"].ToString()).ToString();
                row["Jumlah345"] = Convert.ToString(Convert.ToInt32(row["Sebelumnya"]) + Convert.ToInt32(row["Masuk"]) + Convert.ToInt32(row["Pindahan"]));
                row["Dipindahkan"] = SelectCountDipindahkan(row["CensusDate"].ToString(), row["ServiceUnitID"].ToString(), row["ClassID"].ToString(), row["SmfID"].ToString()).ToString();
                row["Jumlah7811"] = Convert.ToString(Convert.ToInt32(row["Hidup"]) + Convert.ToInt32(row["Meninggal"]) + Convert.ToInt32(row["Dipindahkan"]));
                row["Dirawat"] = Convert.ToString(Convert.ToInt32(row["Jumlah345"]) - Convert.ToInt32(row["Jumlah7811"]));
            }

            tab1.AcceptChanges();

            return tab1;
        }

        private static int SelectCountPindahan(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var t = new PatientTransferQuery("t");
            //var r = new RegistrationQuery("a");
            //var p = new PatientQuery("b");
            //var s = new SmfQuery("c");

            t.Select(
                //r.RegistrationNo,
                //p.MedicalNo,
                //r.BedID,
                //"<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                //s.SmfName,

                t.FromServiceUnitID,
                t.ToServiceUnitID,
                t.FromClassID,
                t.ToClassID
                );
            //t.InnerJoin(r).On(t.RegistrationNo == r.RegistrationNo);
            //t.InnerJoin(p).On(r.PatientID == p.PatientID);
            //t.InnerJoin(s).On(r.SmfID == s.SmfID);
            t.Where(
                t.TransferDate == censusDate,
                t.ToServiceUnitID == serviceUnitID,
                t.ToSpecialtyID == smfID,
                t.IsApprove == true
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.ToClassID == classID);

            var tab = t.LoadDataTable();

            return tab.Rows.Count;
        }

        private static int SelectCountDipindahkan(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var t = new PatientTransferQuery("t");
            //var r = new RegistrationQuery("a");
            //var p = new PatientQuery("b");
            //var s = new SmfQuery("c");

            t.Select(
                //r.RegistrationNo,
                //p.MedicalNo,
                //r.BedID,
                //"<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                //s.SmfName,

                t.FromServiceUnitID,
                t.ToServiceUnitID,
                t.FromClassID,
                t.ToClassID
                );
            //t.InnerJoin(r).On(t.RegistrationNo == r.RegistrationNo);
            //t.InnerJoin(p).On(r.PatientID == p.PatientID);
            //t.InnerJoin(s).On(r.SmfID == s.SmfID);
            t.Where(
                t.TransferDate == censusDate,
                t.FromServiceUnitID == serviceUnitID,
                t.FromSpecialtyID == smfID,
                t.IsApprove == true
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.FromClassID == classID);

            var tab = t.LoadDataTable();

            //foreach (DataRow row in tab.AsEnumerable().Where(d => d.Field<string>("FromServiceUnitID") == d.Field<string>("ToServiceUnitID") &&
            //                                                      d.Field<string>("FromClassID") == d.Field<string>("ToClassID")))
            //{
            //    row.Delete();
            //}

            //tab.AcceptChanges();

            return tab.Rows.Count;
        }
    }
}
