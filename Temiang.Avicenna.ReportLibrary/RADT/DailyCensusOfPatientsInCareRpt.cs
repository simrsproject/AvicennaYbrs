using System;
using System.Linq;
using System.Data;
using Telerik.Reporting;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;

namespace Temiang.Avicenna.ReportLibrary.ExternalReport
{

    public partial class DailyCensusOfPatientsInCareRpt : Report
    {
        public DailyCensusOfPatientsInCareRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(pageHeader);

            var tab = new DataTable();
            tab.Columns.Add("CensusDate", typeof(DateTime));
            tab.Columns.Add("ServiceUnitName", typeof(string));
            tab.Columns.Add("ClassName", typeof(string));
            tab.Columns.Add("Masuk1", typeof(string));
            tab.Columns.Add("Masuk2", typeof(string));
            tab.Columns.Add("Pindahan1", typeof(string));
            tab.Columns.Add("Pindahan2", typeof(string));
            tab.Columns.Add("Hidup1", typeof(string));
            tab.Columns.Add("Hidup2", typeof(string));
            tab.Columns.Add("Dipindahkan1", typeof(string));
            tab.Columns.Add("Dipindahkan2", typeof(string));
            tab.Columns.Add("Meninggal1", typeof(string));
            tab.Columns.Add("Meninggal2", typeof(string));
            tab.Columns.Add("Below48", typeof(string));
            tab.Columns.Add("Over48", typeof(string));

            var masuk = SelectMasuk(printJobParameters[0].ValueString, printJobParameters[1].ValueString, printJobParameters[2].ValueString);
            var pindahan = SelectPindahan(printJobParameters[0].ValueString, printJobParameters[1].ValueString, printJobParameters[2].ValueString);
            var hidup = SelectHidup(printJobParameters[0].ValueString, printJobParameters[1].ValueString, printJobParameters[2].ValueString, printJobParameters[3].ValueString, printJobParameters[4].ValueString);
            var dipindahkan = SelectDipindahkan(printJobParameters[0].ValueString, printJobParameters[1].ValueString, printJobParameters[2].ValueString);
            var meninggal = SelectMeninggal(printJobParameters[0].ValueString, printJobParameters[1].ValueString, printJobParameters[2].ValueString, printJobParameters[3].ValueString, printJobParameters[4].ValueString);

            var count = (new int[5] { masuk.Rows.Count, pindahan.Rows.Count, hidup.Rows.Count, dipindahkan.Rows.Count, meninggal.Rows.Count }).Max();

            for (int i = 0; i < count; i++)
            {
                var dr = tab.NewRow();
                tab.Rows.Add(dr);
            }

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(printJobParameters[1].ValueString);

            var c = new Class();
            c.LoadByPrimaryKey(printJobParameters[2].ValueString);

            for (int i = 0; i < tab.Rows.Count; i++)
            {
                var dr = tab.Rows[i];

                dr[0] = DateTime.Parse(printJobParameters[0].ValueString);
                dr[1] = unit.ServiceUnitName;
                dr[2] = string.IsNullOrEmpty(c.ClassName) ? "GABUNGAN" : c.ClassName;

                if (masuk.Rows.Count > 0)
                {
                    try
                    {
                        var msk = masuk.Rows[i];
                        dr[3] = string.Format("{0}{1}{2}{3}{4}", msk[0], Environment.NewLine, msk[1], Environment.NewLine, msk[2]);
                        dr[4] = string.Format("{0}{1}{2}", msk[3], Environment.NewLine, msk[4]);
                    }
                    catch { }
                }

                if (pindahan.Rows.Count > 0)
                {
                    try
                    {
                        var pdh = pindahan.Rows[i];
                        dr[5] = string.Format("{0}{1}{2}{3}{4}", pdh[0], Environment.NewLine, pdh[1], Environment.NewLine, pdh[2]);
                        dr[6] = string.Format("{0}{1}{2}", pdh[3], Environment.NewLine, pdh[4]);
                    }
                    catch { }
                }

                if (hidup.Rows.Count > 0)
                {
                    try
                    {
                        var hdp = hidup.Rows[i];
                        dr[7] = string.Format("{0}{1}{2}{3}{4}", hdp[0], Environment.NewLine, hdp[1], Environment.NewLine, hdp[2]);
                        dr[8] = string.Format("{0}{1}{2}", hdp[3], Environment.NewLine, hdp[4]);
                    }
                    catch { }
                }

                if (dipindahkan.Rows.Count > 0)
                {
                    try
                    {
                        var dpd = dipindahkan.Rows[i];
                        dr[9] = string.Format("{0}{1}{2}{3}{4}", dpd[0], Environment.NewLine, dpd[1], Environment.NewLine, dpd[2]);
                        dr[10] = string.Format("{0}{1}{2}", dpd[3], Environment.NewLine, dpd[4]);
                    }
                    catch { }
                }

                if (meninggal.Rows.Count > 0)
                {
                    try
                    {
                        var mng = meninggal.Rows[i];
                        dr[11] = string.Format("{0}{1}{2}{3}{4}", mng[0], Environment.NewLine, mng[1], Environment.NewLine, mng[2]);
                        dr[12] = string.Format("{0}{1}{2}", mng[3], Environment.NewLine, mng[4]);
                        dr[13] = string.Format("{0}", mng[5]);
                        dr[14] = string.Format("{0}", mng[6]);
                    }
                    catch { }
                }
            }

            tab.AcceptChanges();

            DataSource = tab;
        }

        private static DataTable SelectMasuk(string censusDate, string serviceUnitID, string classID)
        {
            //var r = new RegistrationQuery("a");
            //var p = new PatientQuery("b");
            //var s = new SmfQuery("c");

            //r.Select(
            //    r.RegistrationNo,
            //    p.MedicalNo,
            //    r.BedID,
            //    "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
            //    s.SmfName
            //    );
            //r.InnerJoin(p).On(r.PatientID == p.PatientID);
            //r.InnerJoin(s).On(r.SmfID == s.SmfID);
            //r.Where(
            //    r.RegistrationDate == censusDate,
            //    r.ServiceUnitID == serviceUnitID,
            //    r.IsVoid == false
            //    );
            //if (!string.IsNullOrEmpty(classID)) r.Where(r.ClassID == classID);
            //r.OrderBy(r.RegistrationNo.Ascending);
            //return r.LoadDataTable();

            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            var s = new SmfQuery("c");
            var c = new ClassQuery("d");

            var t = new PatientTransferHistoryQuery("e");

            t.Select(
                r.RegistrationNo,
                p.MedicalNo,
                t.BedID,
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                s.SmfName//,
                //c.ClassName
                );
            t.InnerJoin(r).On(r.RegistrationNo == t.RegistrationNo);
            t.InnerJoin(p).On(r.PatientID == p.PatientID);
            t.InnerJoin(s).On(t.SmfID == s.SmfID);
            //t.InnerJoin(c).On(t.ClassID == c.ClassID);
            t.Where(
                t.TransferNo == string.Empty,
                t.DateOfEntry == censusDate,
                t.ServiceUnitID == serviceUnitID//,
                //t.SmfID == smfID//,
                //r.IsVoid == false
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.ClassID == classID);
            return t.LoadDataTable();
        }

        private static DataTable SelectPindahan(string censusDate, string serviceUnitID, string classID)
        {
            var t = new PatientTransferQuery("t");
            var th = new PatientTransferHistoryQuery("th");
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            var s = new SmfQuery("c");

            t.Select(
                r.RegistrationNo,
                p.MedicalNo,
                t.ToBedID, //r.BedID,
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                s.SmfName,

                t.FromServiceUnitID,
                t.ToServiceUnitID,
                t.FromClassID,
                t.ToClassID
                );
            t.InnerJoin(th).On(t.RegistrationNo == th.RegistrationNo && t.TransferNo == th.TransferNo);
            t.InnerJoin(r).On(t.RegistrationNo == r.RegistrationNo);
            t.InnerJoin(p).On(r.PatientID == p.PatientID);
            //t.InnerJoin(s).On(t.ToSpecialtyID == s.SmfID);
            t.InnerJoin(s).On(th.SmfID == s.SmfID);
            t.Where(
                t.TransferDate == censusDate,
                t.ToServiceUnitID == serviceUnitID,
                t.IsApprove == true,
                r.IsVoid == false
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.ToClassID == classID);
            r.OrderBy(r.RegistrationNo.Ascending);

            return t.LoadDataTable();
        }

        private static DataTable SelectHidup(string censusDate, string serviceUnitID, string classID, string below48, string over48)
        {
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            var s = new SmfQuery("c");

            r.Select(
                r.RegistrationNo,
                p.MedicalNo,
                r.BedID,
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                s.SmfName
                );
            r.InnerJoin(p).On(r.PatientID == p.PatientID);
            r.InnerJoin(s).On(r.SmfID == s.SmfID);
            r.Where(
                r.DischargeDate == censusDate,
                r.SRDischargeCondition.NotIn(below48, over48),
                r.ServiceUnitID == serviceUnitID
                );
            if (!string.IsNullOrEmpty(classID)) r.Where(r.ClassID == classID);
            r.OrderBy(r.RegistrationNo.Ascending);
            return r.LoadDataTable();
        }

        private static DataTable SelectDipindahkan(string censusDate, string serviceUnitID, string classID)
        {
            var t = new PatientTransferQuery("t");
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            var s = new SmfQuery("c");

            t.Select(
                r.RegistrationNo,
                p.MedicalNo,
                r.BedID,
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                s.SmfName,

                t.FromServiceUnitID,
                t.ToServiceUnitID,
                t.FromClassID,
                t.ToClassID
                );
            t.InnerJoin(r).On(t.RegistrationNo == r.RegistrationNo);
            t.InnerJoin(p).On(r.PatientID == p.PatientID);
            t.InnerJoin(s).On(t.FromSpecialtyID == s.SmfID);
            t.Where(
                t.TransferDate == censusDate,
                t.FromServiceUnitID == serviceUnitID,
                t.IsApprove == true,
                r.IsVoid == false
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.FromClassID == classID);
            r.OrderBy(r.RegistrationNo.Ascending);

            return t.LoadDataTable();
        }

        private static DataTable SelectMeninggal(string censusDate, string serviceUnitID, string classID, string below48, string over48)
        {
            var r = new RegistrationQuery("a");
            var p = new PatientQuery("b");
            var s = new SmfQuery("c");

            r.Select(
                r.RegistrationNo,
                p.MedicalNo,
                r.BedID,
                "<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) AS PatientName>",
                s.SmfName,
                string.Format("<(CASE WHEN a.SRDischargeCondition = '{0}' THEN '[X]' ELSE '' END) AS Below48>", below48),
                string.Format("<(CASE WHEN a.SRDischargeCondition != '{0}' THEN '[X]' ELSE '' END) AS Over48>", below48)
                );
            r.InnerJoin(p).On(r.PatientID == p.PatientID);
            r.InnerJoin(s).On(r.SmfID == s.SmfID);
            r.Where(
                r.DischargeDate == censusDate,
                r.SRDischargeCondition.In(below48, over48),
                r.ServiceUnitID == serviceUnitID,
                r.IsVoid == false
                );
            if (!string.IsNullOrEmpty(classID)) r.Where(r.ClassID == classID);
            r.OrderBy(r.RegistrationNo.Ascending);

            return r.LoadDataTable();
        }
    }
}