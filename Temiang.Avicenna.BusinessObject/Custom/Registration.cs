using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using System.Linq;
using Temiang.Dal.Interfaces;
using fastJSON;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class Registration
    {
        //public override void Save()
        //{
        //    if (DateTime.Now > new DateTime(2010, 7, 31))
        //    {
        //        throw new Exception("ERR:999 Service Expired, please contact Administrator");
        //        return;
        //    }

        //    base.Save();
        //}

        public static List<string> RelatedRegistrations(string registrationNo)
        {
            if (string.IsNullOrWhiteSpace(registrationNo))
                return null;

            var regs = new List<string>();
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);
            regs.Add(reg.RegistrationNo);

            // Cari registrasi hasil refer
            AddToRelatedRegistrations(regs, registrationNo);

            //Cari Registrasi yang me-refer
            if (!string.IsNullOrWhiteSpace(reg.FromRegistrationNo))
                AddFromRelatedRegistrations(regs, reg.FromRegistrationNo);

            return regs;
        }
        private static void AddFromRelatedRegistrations(List<string> regs, string registrationNo)
        {
            if (string.IsNullOrWhiteSpace(registrationNo)) return;

            regs.Add(registrationNo);

            //Cari Reg lain yg sebelumnya
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);
            if (!string.IsNullOrWhiteSpace(reg.FromRegistrationNo))
                AddFromRelatedRegistrations(regs, reg.FromRegistrationNo);

            // Cari registrasi hasil refer-nya
            AddToRelatedRegistrations(regs, registrationNo);
        }

        private static void AddToRelatedRegistrations(List<string> regs, string registrationNo)
        {
            if (string.IsNullOrWhiteSpace(registrationNo)) return;

            // Tambahkan reg refer to lainnya
            var qr = new RegistrationQuery();
            qr.Where(qr.FromRegistrationNo == registrationNo);
            qr.Select(qr.RegistrationNo);
            var dtb = qr.LoadDataTable();
            foreach (DataRow row in dtb.Rows)
            {
                var regNo = row["RegistrationNo"].ToString();
                if (!regs.Contains(regNo))
                {
                    regs.Add(regNo);
                    AddToRelatedRegistrations(regs, regNo);
                }
            }
        }

        public static string GetShiftID()
        {
            //deklarasi jam sekarang
            int currentHour = int.Parse(DateTime.Now.ToString("HHmm"));

            string parValue = string.Empty;

            //shift pagi
            parValue = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftStartMorning);
            int shiftPagi = int.Parse(parValue.Remove(parValue.IndexOf(':'), 1));

            //shift siang
            parValue = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftStartAfternoon);
            int shiftSiang = int.Parse(parValue.Remove(parValue.IndexOf(':'), 1));

            //shiftPagi malam
            parValue = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftStartNight);
            int shiftMalam = int.Parse(parValue.Remove(parValue.IndexOf(':'), 1));


            //cek validasi
            string shiftID = null;

            if (currentHour >= shiftPagi && currentHour < shiftSiang)
                shiftID = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftMorning);
            else if (currentHour >= shiftSiang && currentHour < shiftMalam)
                shiftID = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftAfternoon);
            else if (currentHour >= shiftMalam || currentHour < shiftPagi)
                shiftID = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftNight);

            return shiftID;
        }

        public static string GetShiftNo()
        {
            //deklarasi jam sekarang
            int currentHour = int.Parse(DateTime.Now.ToString("HHmm"));

            string parValue = string.Empty;

            //shift pagi
            parValue = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftStartMorning);
            int shiftPagi = int.Parse(parValue.Remove(parValue.IndexOf(':'), 1));

            //shift siang
            parValue = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftStartAfternoon);
            int shiftSiang = int.Parse(parValue.Remove(parValue.IndexOf(':'), 1));

            //shiftPagi malam
            parValue = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftStartNight);
            int shiftMalam = int.Parse(parValue.Remove(parValue.IndexOf(':'), 1));


            //cek validasi
            string shiftID = string.Empty;

            if (currentHour >= shiftPagi && currentHour < shiftSiang)
                shiftID = "1";
            else if (currentHour >= shiftSiang && currentHour < shiftMalam)
                shiftID = "2";
            else if (currentHour >= shiftMalam || currentHour < shiftPagi)
                shiftID = "3";

            return shiftID;
        }

        public static string GetPhysicianOnLeave(DateTime regDate, string regTime, string regType, string paramedicId, string toUnitId)
        {
            var retValue = string.Empty;

            //-- physician on leave
            var plQuery = new ParamedicLeaveQuery("a");
            var pldQuery = new ParamedicLeaveDateQuery("b");
            plQuery.InnerJoin(pldQuery).On(plQuery.TransactionNo == pldQuery.TransactionNo &&
                                           plQuery.IsApproved == true);
            plQuery.Select(plQuery.TransactionNo, plQuery.SubsParamedicEMR, plQuery.SubsParamedicIP, plQuery.SubsParamedicOP);
            plQuery.Where(
                plQuery.ParamedicID == paramedicId &&
                pldQuery.LeaveDate.Date() == regDate.Date
                );
            plQuery.OrderBy(plQuery.TransactionNo.Descending);
            plQuery.es.Top = 1;

            DataTable dtpl = plQuery.LoadDataTable();
            if (dtpl.Rows.Count > 0)
            {
                var exc = new ParamedicLeaveExeptionUnitCollection();
                exc.Query.Where(exc.Query.TransactionNo == dtpl.Rows[0]["TransactionNo"].ToString(),
                                exc.Query.ServiceUnitID == toUnitId);
                if (regType == "OPR")
                    exc.Query.Where(exc.Query.StartTime <= regTime, exc.Query.EndTime >= regTime);

                exc.LoadAll();

                if (exc.Count == 0)
                {
                    var subIp = "-";
                    var subOp = "-";
                    var onCall = "-";

                    var p = new Paramedic();
                    if (p.LoadByPrimaryKey(dtpl.Rows[0]["SubsParamedicIP"].ToString()))
                        subIp = p.ParamedicName;

                    p = new Paramedic();
                    if (p.LoadByPrimaryKey(dtpl.Rows[0]["SubsParamedicOP"].ToString()))
                        subOp = p.ParamedicName;

                    p = new Paramedic();
                    if (p.LoadByPrimaryKey(dtpl.Rows[0]["SubsParamedicEMR"].ToString()))
                        onCall = p.ParamedicName;

                    p = new Paramedic();
                    var paramedicName = string.Empty;
                    if (p.LoadByPrimaryKey(paramedicId))
                        paramedicName = p.ParamedicName;

                    retValue = paramedicName + " is on leave. Please select another physician (Inpatient: " + subIp + "; Outpatient: " + subOp + "; On Call: " + onCall + ")";
                }
            }

            return retValue;
        }
        
        public static long? GetMembershipDetailId(string patientId, DateTime registrationDate)
        {
            long retValue = -1;

            //header
            var pmdColl = new MembershipDetailCollection();
            var pmdQuery = new MembershipDetailQuery("a");
            var pmQuery = new MembershipQuery("b");
            pmdQuery.InnerJoin(pmQuery).On(pmQuery.MembershipNo == pmdQuery.MembershipNo);
            pmdQuery.Where(pmQuery.PatientID == patientId, pmQuery.IsActive == true,
                pmdQuery.StartDate.Date() <= registrationDate.Date,
                pmdQuery.EndDate.Date() >= registrationDate.Date);
            pmdQuery.OrderBy(pmdQuery.StartDate.Ascending);
            pmdQuery.es.Top = 1;
            pmdColl.Load(pmdQuery);

            if (pmdColl.Count <= 0) 
            {
                pmdColl = new MembershipDetailCollection();
                pmdQuery = new MembershipDetailQuery("a");
                pmQuery = new MembershipQuery("b");
                var pmmQuery = new MembershipMemberQuery("c");
                pmdQuery.InnerJoin(pmQuery).On(pmQuery.MembershipNo == pmdQuery.MembershipNo);
                pmdQuery.InnerJoin(pmmQuery).On(pmmQuery.MembershipNo == pmQuery.MembershipNo);
                pmdQuery.Where(pmmQuery.PatientID == patientId, pmmQuery.IsActive == true, pmQuery.IsActive == true,
                    pmdQuery.StartDate.Date() <= registrationDate.Date,
                    pmdQuery.EndDate.Date() >= registrationDate.Date);
                pmdQuery.OrderBy(pmdQuery.StartDate.Ascending);
                pmdQuery.es.Top = 1;
                pmdColl.Load(pmdQuery);

                if (pmdColl.Count <= 0) return retValue;
            }

            var pmd = new BusinessObject.MembershipDetail();
            pmd.Load(pmdQuery);

            retValue = pmd.MembershipDetailID ?? -1;

            return retValue;
        }

        public static string GetRegStatusVoidOrClose(string registrationNo)
        {
            var r = new Registration();
            if (r.LoadByPrimaryKey(registrationNo))
            {
                if (r.IsVoid ?? false)
                    return "Registration has void.";
                if (r.IsClosed ?? false)
                    return "Registration has closed.";
            }

            return string.Empty;
        }

        public static string GetRegistrationPathwayName(string registrationNo)
        {
            var rp = new RegistrationPathwayQuery("a");
            var p = new PathwayQuery("b");
            rp.InnerJoin(p).On(p.PathwayID == rp.PathwayID);
            rp.Where(rp.RegistrationNo == registrationNo, rp.PathwayID != string.Empty);
            rp.Select(p.PathwayID, p.PathwayName, rp.PathwayStatus);
            rp.es.Top = 1;
            DataTable dtb = rp.LoadDataTable();
            if (dtb.Rows.Count > 0)
                return dtb.Rows[0]["PathwayName"].ToString() + " (" + (dtb.Rows[0]["PathwayStatus"].ToString() == "A" ? "ACCEPT" : (dtb.Rows[0]["PathwayStatus"].ToString() == "F" ? "FAIL" : "-")) + ")";


            return "-";
        }

        #region static fn
        public static DataTable AppointmentSlotTime(string serviceUnitID, string paramedicID, DateTime date, bool isNew)
        {
            var dtb = new DataTable("AppointmentSlotTime");

            //column
            var dc = new DataColumn("SlotNo", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Start", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("End", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Subject", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("Description", Type.GetType("System.String"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("OperationalStart", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            dc = new DataColumn("OperationalEnd", Type.GetType("System.DateTime"));
            dtb.Columns.Add(dc);

            if (!string.IsNullOrEmpty(serviceUnitID) && !string.IsNullOrEmpty(paramedicID))
            {
                DataRow r = dtb.NewRow();
                r[0] = 0;
                r[1] = DateTime.Now;
                r[2] = DateTime.Now;
                r[3] = string.Empty;
                r[4] = string.Empty;
                r[5] = DateTime.Now;
                r[6] = DateTime.Now;
                dtb.Rows.Add(r);

                var sch = new ParamedicScheduleDateQuery("a");
                var ot = new OperationalTimeQuery("b");
                var par = new ParamedicScheduleQuery("c");

                sch.Select(
                    sch.ScheduleDate,
                    ot.StartTime1,
                    ot.EndTime1,
                    ot.StartTime2,
                    ot.EndTime2,
                    ot.StartTime3,
                    ot.EndTime3,
                    ot.StartTime4,
                    ot.EndTime4,
                    ot.StartTime5,
                    ot.EndTime5,
                    par.ExamDuration,
                    @"<CAST(ISNULL(a.IsClosedTime1, 0) AS VARCHAR) AS 'IsClosedTime1'>",
                    @"<CAST(ISNULL(a.IsClosedTime2, 0) AS VARCHAR) AS 'IsClosedTime2'>",
                    @"<CAST(ISNULL(a.IsClosedTime3, 0) AS VARCHAR) AS 'IsClosedTime3'>",
                    @"<CAST(ISNULL(a.IsClosedTime4, 0) AS VARCHAR) AS 'IsClosedTime4'>",
                    @"<CAST(ISNULL(a.IsClosedTime5, 0) AS VARCHAR) AS 'IsClosedTime5'>",
                    @"<CASE ISNULL(c.Quota, 0) WHEN 0 THEN 999 ELSE ISNULL(c.Quota, 0) END + " +
                    "CASE ISNULL(c.QuotaOnline, 0) WHEN 0 THEN 999 ELSE ISNULL(c.QuotaOnline, 0) END + " +
                    "CASE ISNULL(c.QuotaBpjs, 0) WHEN 0 THEN 999 ELSE ISNULL(c.QuotaBpjs, 0) END + " +
                    "CASE ISNULL(c.QuotaBpjsOnline, 0) WHEN 0 THEN 999 ELSE ISNULL(c.QuotaBpjsOnline, 0) END AS 'Quota'>",
                    @"<ISNULL(a.AddQuota, 0)+ISNULL(a.AddQuotaOnline, 0)+ISNULL(a.AddQuotaBpjs, 0)+ISNULL(a.AddQuotaBpjsOnline, 0) AS 'AddQuota'>"
                    );
                sch.InnerJoin(ot).On(sch.OperationalTimeID == ot.OperationalTimeID);
                sch.InnerJoin(par).On(
                    sch.ServiceUnitID == par.ServiceUnitID &&
                    sch.ParamedicID == par.ParamedicID &&
                    sch.PeriodYear == par.PeriodYear
                    );
                sch.Where(
                    sch.ServiceUnitID == serviceUnitID,
                    sch.ParamedicID == paramedicID,
                    sch.PeriodYear == date.Year,
                    sch.ScheduleDate == date
                    );
                var list = sch.LoadDataTable();

                double duration = 0;
                if (list.Rows.Count > 0)
                    duration = Convert.ToDouble(list.Rows[0][11]);

                foreach (DataRow row in list.Rows)
                {
                    int quota = 0;
                    if (Convert.ToInt32(row[17]) > 0)
                        quota = Convert.ToInt32(row[17]) + Convert.ToInt32(row[18]);

                    //time 1
                    if (isNew)
                    {
                        if (row[1].ToString().Trim() != string.Empty && row[2].ToString().Trim() != string.Empty && row[12].ToString().Trim() == "0")
                        {
                            var i = 1;
                            var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[1].ToString().Trim());
                            var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[2].ToString().Trim());
                            while (dt1 < dt2)
                            {
                                if (quota == 0 || i <= quota)
                                {
                                    DataRow dr = dtb.NewRow();
                                    dr[0] = i;
                                    dr[1] = dt1;
                                    dr[2] = dt1.AddMinutes(duration);
                                    dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                                    dr[4] = string.Empty;
                                    dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[1].ToString().Trim());
                                    dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[2].ToString().Trim());
                                    dtb.Rows.Add(dr);

                                    dt1 = dt1.AddMinutes(duration);
                                    i++;
                                }
                                else
                                    break;
                            }
                        }
                        //time 2
                        if (row[3].ToString().Trim() != string.Empty && row[4].ToString().Trim() != string.Empty && row[13].ToString().Trim() == "0")
                        {
                            var i = 1;
                            var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[3].ToString().Trim());
                            var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[4].ToString().Trim());
                            while (dt1 < dt2)
                            {
                                if (quota == 0 || i <= quota)
                                {
                                    DataRow dr = dtb.NewRow();
                                    dr[0] = i;
                                    dr[1] = dt1;
                                    dr[2] = dt1.AddMinutes(duration);
                                    dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                                    dr[4] = string.Empty;
                                    dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[3].ToString().Trim());
                                    dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[4].ToString().Trim());
                                    dtb.Rows.Add(dr);

                                    dt1 = dt1.AddMinutes(duration);
                                    i++;
                                }
                                else
                                    break;
                            }
                        }
                        //time 3
                        if (row[5].ToString().Trim() != string.Empty && row[6].ToString().Trim() != string.Empty && row[14].ToString().Trim() == "0")
                        {
                            var i = 1;
                            var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[5].ToString().Trim());
                            var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[6].ToString().Trim());
                            while (dt1 < dt2)
                            {
                                if (quota == 0 || i <= quota)
                                {
                                    DataRow dr = dtb.NewRow();
                                    dr[0] = i;
                                    dr[1] = dt1;
                                    dr[2] = dt1.AddMinutes(duration);
                                    dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                                    dr[4] = string.Empty;
                                    dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[5].ToString().Trim());
                                    dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[6].ToString().Trim());
                                    dtb.Rows.Add(dr);

                                    dt1 = dt1.AddMinutes(duration);
                                    i++;
                                }
                                else
                                    break;
                            }
                        }
                        //time 4
                        if (row[7].ToString().Trim() != string.Empty && row[8].ToString().Trim() != string.Empty && row[15].ToString().Trim() == "0")
                        {
                            var i = 1;
                            var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[7].ToString().Trim());
                            var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[8].ToString().Trim());
                            while (dt1 < dt2)
                            {
                                if (quota == 0 || i <= quota)
                                {
                                    DataRow dr = dtb.NewRow();
                                    dr[0] = i;
                                    dr[1] = dt1;
                                    dr[2] = dt1.AddMinutes(duration);
                                    dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                                    dr[4] = string.Empty;
                                    dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[7].ToString().Trim());
                                    dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[8].ToString().Trim());
                                    dtb.Rows.Add(dr);

                                    dt1 = dt1.AddMinutes(duration);
                                    i++;
                                }
                                else
                                    break;
                            }
                        }
                        //time 5
                        if (row[9].ToString().Trim() != string.Empty && row[10].ToString().Trim() != string.Empty && row[16].ToString().Trim() == "0")
                        {
                            var i = 1;
                            var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[9].ToString().Trim());
                            var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[10].ToString().Trim());
                            while (dt1 < dt2)
                            {
                                if (quota == 0 || i <= quota)
                                {
                                    DataRow dr = dtb.NewRow();
                                    dr[0] = i;
                                    dr[1] = dt1;
                                    dr[2] = dt1.AddMinutes(duration);
                                    dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                                    dr[4] = string.Empty;
                                    dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[9].ToString().Trim());
                                    dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[10].ToString().Trim());
                                    dtb.Rows.Add(dr);

                                    dt1 = dt1.AddMinutes(duration);
                                    i++;
                                }
                                else
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (row[1].ToString().Trim() != string.Empty && row[2].ToString().Trim() != string.Empty)
                        {
                            var i = 1;
                            var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[1].ToString().Trim());
                            var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[2].ToString().Trim());
                            while (dt1 < dt2)
                            {
                                if (quota == 0 || i <= quota)
                                {
                                    DataRow dr = dtb.NewRow();
                                    dr[0] = i;
                                    dr[1] = dt1;
                                    dr[2] = dt1.AddMinutes(duration);
                                    dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                                    dr[4] = string.Empty;
                                    dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[1].ToString().Trim());
                                    dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[2].ToString().Trim());
                                    dtb.Rows.Add(dr);

                                    dt1 = dt1.AddMinutes(duration);
                                    i++;
                                }
                                else
                                    break;
                            }
                        }
                        //time 2
                        if (row[3].ToString().Trim() != string.Empty && row[4].ToString().Trim() != string.Empty)
                        {
                            var i = 1;
                            var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[3].ToString().Trim());
                            var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[4].ToString().Trim());
                            while (dt1 < dt2)
                            {
                                if (quota == 0 || i <= quota)
                                {
                                    DataRow dr = dtb.NewRow();
                                    dr[0] = i;
                                    dr[1] = dt1;
                                    dr[2] = dt1.AddMinutes(duration);
                                    dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                                    dr[4] = string.Empty;
                                    dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[3].ToString().Trim());
                                    dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[4].ToString().Trim());
                                    dtb.Rows.Add(dr);

                                    dt1 = dt1.AddMinutes(duration);
                                    i++;
                                }
                                else
                                    break;
                            }
                        }
                        //time 3
                        if (row[5].ToString().Trim() != string.Empty && row[6].ToString().Trim() != string.Empty)
                        {
                            var i = 1;
                            var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[5].ToString().Trim());
                            var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[6].ToString().Trim());
                            while (dt1 < dt2)
                            {
                                if (quota == 0 || i <= quota)
                                {
                                    DataRow dr = dtb.NewRow();
                                    dr[0] = i;
                                    dr[1] = dt1;
                                    dr[2] = dt1.AddMinutes(duration);
                                    dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                                    dr[4] = string.Empty;
                                    dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[5].ToString().Trim());
                                    dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[6].ToString().Trim());
                                    dtb.Rows.Add(dr);

                                    dt1 = dt1.AddMinutes(duration);
                                    i++;
                                }
                                else
                                    break;
                            }
                        }
                        //time 4
                        if (row[7].ToString().Trim() != string.Empty && row[8].ToString().Trim() != string.Empty)
                        {
                            var i = 1;
                            var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[7].ToString().Trim());
                            var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[8].ToString().Trim());
                            while (dt1 < dt2)
                            {
                                if (quota == 0 || i <= quota)
                                {
                                    DataRow dr = dtb.NewRow();
                                    dr[0] = i;
                                    dr[1] = dt1;
                                    dr[2] = dt1.AddMinutes(duration);
                                    dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                                    dr[4] = string.Empty;
                                    dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[7].ToString().Trim());
                                    dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[8].ToString().Trim());
                                    dtb.Rows.Add(dr);

                                    dt1 = dt1.AddMinutes(duration);
                                    i++;
                                }
                                else
                                    break;
                            }
                        }
                        //time 5
                        if (row[9].ToString().Trim() != string.Empty && row[10].ToString().Trim() != string.Empty)
                        {
                            var i = 1;
                            var dt1 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[9].ToString().Trim());
                            var dt2 = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[10].ToString().Trim());
                            while (dt1 < dt2)
                            {
                                if (quota == 0 || i <= quota)
                                {
                                    DataRow dr = dtb.NewRow();
                                    dr[0] = i;
                                    dr[1] = dt1;
                                    dr[2] = dt1.AddMinutes(duration);
                                    dr[3] = i.ToString() + " - " + dt1.ToString("HH:mm");
                                    dr[4] = string.Empty;
                                    dr[5] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[9].ToString().Trim());
                                    dr[6] = Convert.ToDateTime(row[0]).Date + TimeSpan.Parse(row[10].ToString().Trim());
                                    dtb.Rows.Add(dr);

                                    dt1 = dt1.AddMinutes(duration);
                                    i++;
                                }
                                else
                                    break;
                            }
                        }
                    }
                }

                //if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial) != "RSYS")
                {
                    var appt = AppointmentList(serviceUnitID, paramedicID, date);

                    foreach (DataRow slot in dtb.Rows)
                    {
                        foreach (var entity in from entity in appt let dateTime = entity.AppointmentDate.Value.Date + TimeSpan.Parse(entity.AppointmentTime) where Convert.ToDateTime(slot[1]) == dateTime select entity)
                        {
                            slot[0] = entity.AppointmentNo;
                            slot[3] = entity.AppointmentQue + " - " + entity.AppointmentTime + " - " + entity.GetColumn("PatientName").ToString() + (entity.SRAppoinmentType == "QRS" ? " Queueing" : " [A]");
                            break;
                        }
                    }

                    dtb.AcceptChanges();
                }

                var regs = new RegistrationCollection();

                var query = new RegistrationQuery("a");
                var pq = new PatientQuery("b");

                query.Select(
                    query,
                    pq.PatientName
                    );
                query.InnerJoin(pq).On(query.PatientID == pq.PatientID);
                query.Where(
                    query.RegistrationDate == date,
                    query.ServiceUnitID == serviceUnitID,
                    query.ParamedicID == paramedicID,
                    query.IsVoid == false
                    );
                regs.Load(query);

                foreach (var reg in regs)
                {
                    DateTime dateTime = reg.RegistrationDate.Value.Date + TimeSpan.Parse(reg.RegistrationTime);

                    var slot = dtb.AsEnumerable().SingleOrDefault(d => d.Field<string>("SlotNo") == reg.RegistrationQue.ToString() &&
                                                                       d.Field<DateTime>("Start") == dateTime);

                    if (slot != null)
                    {
                        slot[0] = reg.RegistrationNo;
                        if (reg.IsVoid ?? false)
                            slot[3] = slot[3].ToString().Split('-')[0] + "- " + reg.RegistrationTime + " - " + reg.GetColumn("PatientName") + " [X]";
                        else if (reg.IsClosed ?? false)
                            slot[3] = slot[3].ToString().Split('-')[0] + "- " + reg.RegistrationTime + " - " + reg.GetColumn("PatientName") + " [OK]";
                        else
                            slot[3] = slot[3].ToString().Split('-')[0] + "- " + reg.RegistrationTime + " - " + reg.GetColumn("PatientName");
                    }
                }

                dtb.AcceptChanges();
            }

            return dtb;
        }

        private static BusinessObject.AppointmentCollection AppointmentList(string serviceUnitID, string paramedicID, DateTime regdate)
        {
            var query = new AppointmentQuery("a");
            var unit = new ServiceUnitQuery("b");
            var medic = new ParamedicQuery("c");
            var patient = new PatientQuery("e");

            query.Select(
                query.AppointmentNo,
                query.AppointmentQue,
                query.AppointmentDate,
                query.AppointmentTime,
                query.PatientName,
                (medic.ParamedicName + "<br />" + unit.ServiceUnitName + "<br />" + query.Notes).As("Description"),
                query.SRAppointmentStatus,
                query.SRAppoinmentType
                );
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.LeftJoin(patient).On(query.PatientID == patient.PatientID);

            if (!string.IsNullOrEmpty(serviceUnitID))
                query.Where(query.ServiceUnitID == serviceUnitID);

            if (!string.IsNullOrEmpty(paramedicID))
                query.Where(query.ParamedicID == paramedicID);

            query.Where(
                query.AppointmentDate == regdate,
                "<a.SRAppointmentStatus <> " +
                "(SELECT ap.ParameterValue FROM AppParameter AS ap WHERE ap.ParameterID = 'AppointmentStatusCancel')>"
                );

            var coll = new BusinessObject.AppointmentCollection();
            coll.Load(query);

            return coll;
        }
        #endregion
        
        #region Additional Field
        public string MedicalNo
        {
            get { return GetColumn("refToPatient_MedicalNo").ToString(); }
            set { SetColumn("refToPatient_MedicalNo", value); }
        }

        public string PatientName
        {
            get { return GetColumn("refToPatient_PatientName").ToString(); }
            set { SetColumn("refToPatient_PatientName", value); }
        }

        public string Sex
        {
            get { return GetColumn("refToPatient_Sex").ToString(); }
            set { SetColumn("refToPatient_Sex", value); }
        }

        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }

        public string ServiceUnitName
        {
            get { return GetColumn("refToServiceUnit_ServiceUnitName").ToString(); }
            set { SetColumn("refToServiceUnit_ServiceUnitName", value); }
        }

        public string RoomName
        {
            get { return GetColumn("refToServiceRoom_RoomName").ToString(); }
            set { SetColumn("refToServiceRoom_RoomName", value); }
        }

        public string ClassName
        {
            get { return GetColumn("refToClass_ClassName").ToString(); }
            set { SetColumn("refToClass_ClassName", value); }
        }

        public string RegistrationTypeName
        {
            get { return GetColumn("refToAppStandardReferenceItem_RegistrationType").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_RegistrationType", value); }
        }
        #endregion

        #region Public Methods
        public decimal Plavon
        {
            get
            {
                return PlavonAmount ?? 0;
            }
        }
        #endregion
    }

    public class BillingAdjustment : Registration
    {
        #region Adjustment Proporsional
        private CostCalculationCollection ccColl = new CostCalculationCollection();
        private TransChargesItemCollection tciColl = new TransChargesItemCollection();
        private TransChargesItemCompCollection tcicColl = new TransChargesItemCompCollection();
        private IntermBillCollection ibColl = new IntermBillCollection();
        private ItemGroupCollection igColl = new ItemGroupCollection();
        decimal _prorataBaseVal = 0;

        public CostCalculationCollection CostCalculations
        {
            get
            {
                return ccColl;
            }
        }
        public TransChargesItemCollection TransChargesItems
        {
            get
            {
                return tciColl;
            }
        }
        public TransChargesItemCompCollection TransChargesItemComps
        {
            get
            {
                return tcicColl;
            }
        }
        public IntermBillCollection Intermbills {
            get
            {
                return ibColl;
            }
        }

        public ItemGroupCollection ItemGroups
        {
            get
            {
                return igColl;
            }
        }
        private decimal ProrataBaseValue
        {
            // get payment
            get {
                return _prorataBaseVal;
            }
            set {
                _prorataBaseVal = value;
            }
        }
        private bool HasValue {
            get {
                return tcicColl.Any();
            }
        }

        public BillingAdjustment(string RegistrationNo, bool IntermbilledOnly)
        {
            this.LoadByPrimaryKey(RegistrationNo);
            var guar = new Guarantor();
            if (guar.LoadByPrimaryKey(this.GuarantorID))
            {
                if (guar.IsProrateParamedicFee ?? false)
                {
                    var registrationNoList = MergeBilling.GetMergeRegistration(this.RegistrationNo);
                    ccColl.GetCostCalculationsByRegWithMergeBilling(registrationNoList, false, IntermbilledOnly);
                    tciColl.GetTciByNoRegWithMergeBilling(registrationNoList, IntermbilledOnly);
                    tcicColl.GetTcicByNoRegWithMergeBilling(registrationNoList, IntermbilledOnly);
                    igColl.GetByCostCalculation(ccColl);
                    foreach (var ig in igColl)
                    {
                        ig.AutoAdjust = true;
                    }

                    ibColl.GetIbByNoRegWithMergeBilling(registrationNoList);

                    //ProrataBaseValue = InvoicesCollection.GetTotalPaymentByRegistrationNo(registrationNoList);
                    //if (ProrataBaseValue == 0) ProrataBaseValue = 
                    //InvoicesCollection.GetTotalInvoicedByRegistrationNo(registrationNoList);
                    if (ProrataBaseValue == 0) ProrataBaseValue = TransPaymentCollection.GetTotalPayment(registrationNoList, true, IntermbilledOnly);
                    if (ProrataBaseValue == 0) ProrataBaseValue = Plavon;
                }
            }
            // 
        }

        public string CalculateAndSaveProrata_NoTransactionScope(string UserID) {
            if (HasValue)
            {
                string msg = string.Empty;
                //msg = ValidateFee();
                //if (!string.IsNullOrEmpty(msg))
                //{
                //    return msg;
                //}
                msg = CalculateAdjustment();
                if (!string.IsNullOrEmpty(msg))
                {
                    return msg;
                }
                msg = SaveAdjustment_NoTransactionScope(UserID);
                if (!string.IsNullOrEmpty(msg))
                {
                    return msg;
                }
            }
            return string.Empty;
        }

        public void CopyAdjustmentDefault()
        {
            foreach (var cc in ccColl)
            {
                cc.ResetAdjustment();
                cc.CopyAdjustmentDefault(tciColl, tcicColl);
            }
        }

        public string CalculateAdjustment()
        {
            // item group
            //decimal plafon = Plavon;

            // RESET
            CopyAdjustmentDefault();

            //#region Adjust by master item
            //// read default setting per item
            //var iDefault = new BillingAdjustItemSettingCollection();
            //iDefault.LoadAll();
            //foreach (var cc in ccColl)
            //{
            //    // find related setting
            //    var iSet = iDefault.Where(x => x.ItemID == cc.ItemID && x.TariffComponentID != string.Empty);
            //    if (iSet.Any())
            //    {
            //        var tcics = tcicColl.Where(x => x.TransactionNo == cc.TransactionNo &&
            //            x.SequenceNo == cc.SequenceNo);
            //        foreach (var tcic in tcics)
            //        {
            //            var iSetComp = iSet.Where(x => x.TariffComponentID == tcic.TariffComponentID).FirstOrDefault();
            //            if (iSetComp != null)
            //            {
            //                if (iSetComp.HasReplacement(ccColl))
            //                {
            //                    tcic.PriceAdjusted = 0;
            //                }
            //                else
            //                {
            //                    tcic.PriceAdjusted = (iSetComp.IsFeeValueInPercent ?? false) ? (iSetComp.FeeValue / 100 * tcic.GetFinalValue()) : iSetComp.FeeValue;
            //                }
            //            }
            //            else
            //            {
            //                tcic.PriceAdjusted = 0;
            //            }
            //        }
            //        var discAdj = new AdjustedDisc();
            //        cc.SetAdjustmentDisc(discAdj);
            //        cc.SetAdjustmentValueWithoutTCIC(tciColl, tcicColl, tcics.Sum(x => x.PriceAdjusted ?? 0));

            //    }
            //    else
            //    {
            //        // find setting without tariffcomponentid
            //        var iSet2 = iDefault.Where(x => x.ItemID == cc.ItemID && x.TariffComponentID == string.Empty).FirstOrDefault();
            //        if (iSet2 != null)
            //        {
            //            var discAdj = new AdjustedDisc();
            //            cc.SetAdjustmentDisc(discAdj);
            //            if (iSet2.HasReplacement(ccColl))
            //            {
            //                cc.SetAdjustmentValue(tciColl, tcicColl, 0);
            //            }
            //            else
            //            {
            //                if (iSet2.IsFeeValueInPercent ?? false)
            //                {
            //                    cc.SetAdjustmentValue(tciColl, tcicColl, cc.AmountTransaction - (cc.AmountTransaction * (iSet2.FeeValue ?? 0) / 100));
            //                }
            //                else
            //                {
            //                    cc.SetAdjustmentValue(tciColl, tcicColl, iSet2.FeeValue ?? 0);
            //                }
            //            }
            //        }
            //    }
            //}
            //#endregion

            //#region adjust by master item group
            //// read default setting
            //var IGDefault = new BillingAdjustItemGroupSettingCollection();
            //IGDefault.LoadAll();

            //// Item Group
            //foreach (var ig in igColl)
            //{
            //    if (ig.AdjustedDisc.AdjustedDiscAmount.HasValue)
            //    {
            //        {
            //            if (ig.AdjustedDisc.AdjustedDiscSelection == 0/*tarif*/)
            //            {
            //                var ccSelected = ccColl.Where(x => x.ItemGroupID == ig.ItemGroupID);
            //                foreach (var cc in ccSelected)
            //                {
            //                    cc.SetAdjustmentDisc(ig.AdjustedDisc);
            //                    cc.SetAdjustmentValue(tciColl, tcicColl, cc.AmountTransaction - (cc.AmountTransaction * (ig.AdjustedDisc.AdjustedDiscAmount ?? 0) / 100));
            //                }
            //            }
            //            else if (ig.AdjustedDisc.AdjustedDiscSelection == 1/*plafon*/)
            //            {
            //                var plafonDiscAmount = Plavon * (ig.AdjustedDisc.AdjustedDiscAmount ?? 0) / 100;
            //                var ccSelected = ccColl.Where(x => x.ItemGroupID == ig.ItemGroupID);
            //                var ccSum = ccSelected.Sum(x => x.AmountTransaction);

            //                foreach (var cc in ccSelected)
            //                {
            //                    cc.SetAdjustmentDisc(ig.AdjustedDisc);
            //                    cc.SetAdjustmentValue(tciColl, tcicColl, plafonDiscAmount / ccSum * cc.AmountTransaction);
            //                }
            //            }
            //        }
            //    }
            //}
            //#endregion

            #region hitung proporsional yang itemgroupnya auto adjust
            // hitung proporsional

            // cc sudah adjust atau cc belum adjust tapi yang groupnya tidak centang auto adjust
            var totalAdjusted = ccColl.Where(x => x.IsAdjusted == true || (x.IsAdjusted == false && igColl.Where(ig => ig.AutoAdjust == false).Select(ig => ig.ItemGroupID).Contains(x.ItemGroupID)))
                .Sum(x => x.AmountAdjusted ?? 0);

            // cc belum adjust dan cc yang groupnya centang auto adjust
            var ccUnadjusted = ccColl.Where(x => x.IsAdjusted == false && igColl.Where(ig => ig.AutoAdjust == true).Select(ig => ig.ItemGroupID).Contains(x.ItemGroupID));

            var totalUnAdjusted = ccUnadjusted.Sum(x => (x.PatientAmount ?? 0) + (x.GuarantorAmount ?? 0));

            // ditambah adm
            var ibNos = ccUnadjusted.Select(c => c.IntermBillNo).Distinct().ToList();
            if (ibNos.Count > 0) {
                var ibColl = new IntermBillCollection();
                ibColl.Query.Where(ibColl.Query.IntermBillNo.In(ibNos));
                if (ibColl.LoadAll()) {
                    var adm = ibColl.Sum(i => (i.AdministrationAmount ?? 0) + (i.GuarantorAdministrationAmount ?? 0)
                        - (i.DiscAdmPatient ?? 0) - (i.DiscAdmGuarantor ?? 0));
                    totalUnAdjusted += adm;
                }
            }

            decimal sisa = 0;
            DateTime? dischargeDate = this.RegistrationDate;
            if (this.SRRegistrationType == "IPR") dischargeDate = this.DischargeDate;
            //if ((dischargeDate ?? DateTime.Now) < new DateTime(2021, 4, 13))
            //{
                sisa = ProrataBaseValue - totalAdjusted;
            //}
            //else {
            //    sisa = (ProrataBaseValue - ibColl.Sum(x =>
            //        (x.AdministrationAmount ?? 0) +
            //        (x.GuarantorAdministrationAmount ?? 0) -
            //        (x.DiscAdmPatient ?? 0) -
            //        (x.DiscAdmGuarantor ?? 0))) - totalAdjusted;
            //}

            decimal newValSum = 0;
            if (sisa > 0 && totalUnAdjusted > 0)
            {
                int ccUnadjustedCount = ccUnadjusted.Count();
                int idx = 0;
                foreach (var cc in ccUnadjusted)
                {
                    idx++;
                    var newVal = sisa * ((cc.PatientAmount ?? 0) + (cc.GuarantorAmount ?? 0)) / totalUnAdjusted;
                    newVal = Math.Round(newVal, 0);
                    newValSum += newVal;

                    //if (idx == ccUnadjustedCount)
                    //{ // finishing
                    //    // tambahkan di cc terakhir jika ada sisa-sisa pembulatan
                    //    newVal += sisa - newValSum;
                    //}

                    var discAdj = new AdjustedDisc();
                    cc.SetAdjustmentDisc(discAdj);
                    cc.SetAdjustmentValue(tciColl, tcicColl, newVal);
                }
            }
            else if (sisa == 0) { 
            
            }
            else
            {
                //return "Auto calculate can not be done due to minus result!";
            }

            #endregion

            return string.Empty;
        }

        #region Paramedic Fee
        public ParamedicFeeTransChargesItemCompByDischargeDateCollection RecalculateFeeByAdjustment(string UserID)
        {
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.SetFeeByTCIC(tcicColl, UserID);
            return feeColl;
        }
        public string ValidateFee()
        {
            // cek sudah ada jasmed yang verif blm?
            // jika sudah ada yang verif maka adjust tidak dapat dilakukan
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.Query.Where(
                feeColl.Query.Or(
                    feeColl.Query.RegistrationNo == RegistrationNo,
                    feeColl.Query.RegistrationNoMergeTo == RegistrationNo
                ), feeColl.Query.VerificationNo.IsNotNull()
            );

            feeColl.LoadAll();

            if (feeColl.Count > 0)
            {
                return "Some physician fee have been verified!";
            }

            return string.Empty;
        }
        public string ValidateFeePayment()
        {
            // cek sudah ada jasmed yang bayar blm?
            // jika sudah ada pembayaran maka adjust tidak dapat dilakukan
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.Query.Where(
                feeColl.Query.Or(
                    feeColl.Query.RegistrationNo == RegistrationNo,
                    feeColl.Query.RegistrationNoMergeTo == RegistrationNo
                ), feeColl.Query.PaymentGroupNo.IsNotNull()
            );

            feeColl.LoadAll();

            if (feeColl.Count > 0)
            {
                return "Some physician fee have been paid!";
            }

            return string.Empty;
        }
        #endregion

        private string SaveAdjustment_NoTransactionScope(string UserID)
        {
            string valMsg = ValidateFeePayment();
            if (!string.IsNullOrEmpty(valMsg)) return valMsg;

            var log = GetJsonAdjustLog();

            this.IsAdjusted = true;
            this.AdjustLog = log;// "what the ffff...ffffff...ff..fun!!!";

            tciColl.Save();
            tcicColl.Save();
            ccColl.Save();

            var fees = RecalculateFeeByAdjustment(UserID);
            this.Save();

            fees.Save();

            return string.Empty;
        }
        public string SaveAdjustment(string UserID)
        {
            string valMsg = ValidateFee();
            if (!string.IsNullOrEmpty(valMsg)) return valMsg;

            var log = GetJsonAdjustLog();

            this.IsAdjusted = true;
            this.AdjustLog = log;// "what the ffff...ffffff...ff..fun!!!";

            using (var trans = new esTransactionScope())
            {
                tciColl.Save();
                tcicColl.Save();
                ccColl.Save();
                this.Save();

                var fees = RecalculateFeeByAdjustment(UserID);
                fees.Save();

                trans.Complete();
            }

            return string.Empty;
        }

        public string DeleteAdjustment(string UserID)
        {
            string valMsg = ValidateFee();
            if (!string.IsNullOrEmpty(valMsg)) return valMsg;

            ccColl.ResetAdjustment(tciColl, tcicColl);

            this.AdjustLog = string.Empty;
            this.IsAdjusted = false;

            var fees = RecalculateFeeByAdjustment(UserID);

            using (var trans = new esTransactionScope())
            {
                tciColl.Save();
                tcicColl.Save();
                ccColl.Save();
                this.Save();

                fees.Save();

                trans.Complete();
            }

            return string.Empty;
        }

        private string GetJsonAdjustLog()
        {
            List<AdjustLog> log = new List<AdjustLog>();

            var igs = igColl.Where(x => x.AdjustedDisc.AdjustedDiscAmount.HasValue)
                .Select(x => new AdjustLog() { AdjustType = "ItemGroup", Key = x.ItemGroupID, AdjustDisc = x.AdjustedDisc });
            log.AddRange(igs);

            //var sus = ServiceUnits.Where(x => x.AdjustedDisc.AdjustedDiscAmount.HasValue)
            //    .Select(x => new AdjustLog() { AdjustType = "ServiceUnit", Key = x.ServiceUnitID, AdjustDisc = x.AdjustedDisc });
            //log.AddRange(sus);
            //var its = ItemTypes.Where(x => x.AdjustedDisc.AdjustedDiscAmount.HasValue)
            //    .Select(x => new AdjustLog() { AdjustType = "ItemType", Key = x.ItemID, AdjustDisc = x.AdjustedDisc });
            //log.AddRange(its);
            return JSON.ToJSON(log.ToList());
        }
        #endregion

    }

    public class AdjustLog
    {
        private string _AdjustType;
        private string _Key;
        private AdjustedDisc _AdjustDisc = new AdjustedDisc();

        /// <summary>
        /// ItemGroup, ServiceUnit, ItemType
        /// </summary>
        public string AdjustType
        {
            get { return _AdjustType; }
            set { _AdjustType = value; }
        }
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        public AdjustedDisc AdjustDisc
        {
            get { return _AdjustDisc; }
            set { _AdjustDisc = value; }
        }
    }
}