using System.Linq;

namespace Temiang.Avicenna.ReportLibrary.RADT
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject;
    using System.Data;

    /// <summary>
    /// Summary description for ResumeSensusHarian.
    /// </summary>
    public partial class ResumeSensusHarian : Report
    {
        public ResumeSensusHarian(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            Helper.InitializeLogo(this.pageHeaderSection1);

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            DataSource = Census1(printJobParameters);
        }

        private DataTable Census1(PrintJobParameterCollection printJobParameters)
        {
            DateTime censusDate = Convert.ToDateTime(printJobParameters[0].ValueString);
            string serviceUnitID = printJobParameters[1].ValueString;
            string classID = printJobParameters[2].ValueString;
            string below48 = printJobParameters[3].ValueString;
            string over48 = printJobParameters[4].ValueString;

            var smf = new SmfQuery("s");
            smf.Select(
                string.Format("<'{0}' AS CensusDate>", censusDate.ToShortDateString()),
                string.Format("<(SELECT ServiceUnitName FROM ServiceUnit WHERE ServiceUnitID = '{0}') AS ServiceUnitName>", serviceUnitID),
                string.Format("<(ISNULL((SELECT ClassName FROM Class WHERE ClassID = '{0}'), 'GABUNGAN')) AS ClassName>", classID),
                smf.SmfID,
                smf.SmfName,
                string.Format(@"<CAST(ISNULL((SELECT cb.Balance FROM CensusBalance AS cb WHERE cb.CensusDate = '{0}' AND cb.ServiceUnitID = '{1}' AND cb.ClassID = '{2}' AND cb.SmfID = s.SmfID), 0) AS VARCHAR(MAX)) AS Sebelumnya>",
                                censusDate.AddDays(-1).ToShortDateString(), serviceUnitID, classID),
                string.Format(@"<CAST((SELECT COUNT(*) FROM PatientTransferHistory AS r WHERE r.DateOfEntry = '{0}' AND r.ServiceUnitID = '{1}' {2} AND r.SmfID = s.SmfID AND r.TransferNo = '') AS VARCHAR(MAX)) AS Masuk>",
                                censusDate.ToShortDateString(), serviceUnitID, string.IsNullOrEmpty(classID) ? string.Empty : string.Format("AND r.ClassID = '{0}'", classID)),
                "<'0' AS Pindahan>",
                "<'0' AS Jumlah345>",
                string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition NOT IN ('{1}', '{2}') AND r.ServiceUnitID = '{3}' {4} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Hidup>",
                                censusDate.ToShortDateString(), below48, over48, serviceUnitID, string.IsNullOrEmpty(classID) ? string.Empty : string.Format("AND r.ClassID = '{0}'", classID)),
                string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition IN ('{1}', '{2}') AND r.ServiceUnitID = '{3}' {4} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Meninggal>",
                                censusDate.ToShortDateString(), below48, over48, serviceUnitID, string.IsNullOrEmpty(classID) ? string.Empty : string.Format("AND r.ClassID = '{0}'", classID)),
                string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition IN ('{1}') AND r.ServiceUnitID = '{2}' {3} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Below48>",
                                censusDate.ToShortDateString(), below48, serviceUnitID, string.IsNullOrEmpty(classID) ? string.Empty : string.Format("AND r.ClassID = '{0}'", classID)),
                string.Format(@"<CAST((SELECT COUNT(*) FROM Registration AS r WHERE r.DischargeDate = '{0}' AND r.SRDischargeCondition IN ('{1}') AND r.ServiceUnitID = '{2}' {3} AND r.SmfID = s.SmfID) AS VARCHAR(MAX)) AS Over48>",
                                censusDate.ToShortDateString(), over48, serviceUnitID, string.IsNullOrEmpty(classID) ? string.Empty : string.Format("AND r.ClassID = '{0}'", classID)),
                "<'0' AS Dipindahkan>",
                "<'0' AS Jumlah7811>",
                "<'0' AS Dirawat>"
                );

            var tab1 = smf.LoadDataTable();

            foreach (DataRow row in tab1.Rows)
            {
                row["Pindahan"] = SelectCountPindahan(row["CensusDate"].ToString(), serviceUnitID, classID, row["SmfID"].ToString()).ToString();  
                row["Jumlah345"] = Convert.ToString(Convert.ToInt32(row["Sebelumnya"]) + Convert.ToInt32(row["Masuk"]) + Convert.ToInt32(row["Pindahan"]));
                row["Dipindahkan"] = SelectCountDipindahkan(row["CensusDate"].ToString(), serviceUnitID, classID, row["SmfID"].ToString()).ToString();  
                row["Jumlah7811"] = Convert.ToString(Convert.ToInt32(row["Hidup"]) + Convert.ToInt32(row["Meninggal"]) + Convert.ToInt32(row["Dipindahkan"]));
                row["Dirawat"] = Convert.ToString(Convert.ToInt32(row["Jumlah345"]) - Convert.ToInt32(row["Jumlah7811"]));
            }

            tab1.AcceptChanges();

            foreach (DataRow row in tab1.Rows)
            {
                for (int i = 2; i < tab1.Columns.Count; i++)
                {
                    row[i] = row[i].ToString() == "0" ? string.Empty : row[i];
                }
            }

            tab1.AcceptChanges();

            return tab1;
        }

        public static int SelectCountPindahan(string censusDate, string serviceUnitID, string classID, string smfID)
        {
            var t = new PatientTransferQuery("t");
            var th = new PatientTransferHistoryQuery("th");
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
            t.InnerJoin(th).On(t.RegistrationNo == th.RegistrationNo && t.TransferNo == th.TransferNo);
            //t.InnerJoin(r).On(t.RegistrationNo == r.RegistrationNo);
            //t.InnerJoin(p).On(r.PatientID == p.PatientID);
            //t.InnerJoin(s).On(r.SmfID == s.SmfID);
            t.Where(
                t.TransferDate == censusDate,
                t.ToServiceUnitID == serviceUnitID,
                //t.ToSpecialtyID == smfID,
                th.SmfID == smfID,
                t.IsApprove == true
                );
            if (!string.IsNullOrEmpty(classID)) t.Where(t.ToClassID == classID);

            var tab = t.LoadDataTable();

            return t.LoadDataTable().Rows.Count;
        }

        public static int SelectCountDipindahkan(string censusDate, string serviceUnitID, string classID, string smfID)
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

            return t.LoadDataTable().Rows.Count;
        }
    }
}