namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.PatientHealthRecord.RSCH
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject;
    using System.Data;

    /// <summary>
    /// Summary description for PreOperasi.
    /// </summary>
    public partial class PreOperasi : Report
    {
        public PreOperasi(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeLogo(this.reportHeaderSection1);

            var hd = new PatientHealthRecordQuery("a");
            var dt = new PatientHealthRecordLineQuery("b");
            var q = new QuestionQuery("c");
            var r = new RegistrationQuery("d");
            var p = new PatientQuery("e");
            var d = new ParamedicQuery("f");
            var su = new ServiceUnitQuery("g");
            var sr = new ServiceRoomQuery("h");
            var g = new QuestionInGroupQuery("i");
            var sub = new ServiceUnitBookingQuery("j");

            dt.Select(
                p.PatientName,
                "<CAST(d.AgeInYear AS VARCHAR(MAX)) + 't ' + CAST(d.AgeInMonth AS VARCHAR(MAX)) + 'm ' + CAST(d.AgeInDay AS VARCHAR(MAX)) + 'd' AS Age>",
                "<CASE WHEN e.Sex = 'F' THEN 'Perempuan' ELSE 'Laki-laki' END AS SexName>",
                d.ParamedicName,
                p.MedicalNo,
                r.RegistrationNo,
                su.ServiceUnitName,
                sr.RoomName,
                r.BedID,
                @"< REPLICATE('&nbsp;&nbsp;&nbsp;&nbsp;', c.QuestionLevel) + c.QuestionText + ' : ' + 
	                RTRIM(b.QuestionAnswerPrefix + ' ') + 
	                RTRIM(CASE WHEN b.QuestionAnswerNum IS NULL THEN (CASE WHEN c.SRAnswerType = 'CHK' THEN (CASE WHEN b.QuestionAnswerText = '0' THEN 'Tidak' 
																								             ELSE 'Ya' END) 
														              ELSE b.QuestionAnswerText END)
		                  ELSE 'NUM3R1CV4LU3 ' + 
                    b.QuestionAnswerSuffix END) AS QuestionAnswer>",
                dt.QuestionAnswerNum.Coalesce("-1"),
                q.SRAnswerType,
                sub.Diagnose
                );

            dt.InnerJoin(hd).On(
                dt.TransactionNo == hd.TransactionNo &&
                dt.RegistrationNo == hd.RegistrationNo &&
                dt.QuestionFormID == hd.QuestionFormID
                );
            dt.InnerJoin(g).On(dt.QuestionGroupID == g.QuestionGroupID && dt.QuestionID == g.QuestionID);
            dt.InnerJoin(q).On(dt.QuestionID == q.QuestionID && q.IsAlwaysPrint == true && q.IsActive == true);
            dt.InnerJoin(r).On(hd.RegistrationNo == r.RegistrationNo);
            dt.InnerJoin(p).On(r.PatientID == p.PatientID);
            dt.InnerJoin(d).On(r.ParamedicID == d.ParamedicID);
            dt.InnerJoin(su).On(r.ServiceUnitID == su.ServiceUnitID);
            dt.InnerJoin(sr).On(r.RoomID == sr.RoomID);
            dt.LeftJoin(sub).On(hd.ReferenceNo == sub.BookingNo);

            dt.Where(
                hd.TransactionNo == printJobParameters[2].ValueString,
                hd.RegistrationNo == printJobParameters[0].ValueString,
                hd.QuestionFormID == printJobParameters[1].ValueString
                );

            dt.OrderBy(g.RowIndex.Ascending, q.QuestionID.Ascending);

            var tab = dt.LoadDataTable();

            foreach (DataRow row in tab.AsEnumerable().Where(t => t.Field<decimal>("QuestionAnswerNum") > -1))
            {
                row["QuestionAnswer"] = row["QuestionAnswer"].ToString().Replace("NUM3R1CV4LU3", Common.Helper.RemoveZeroDigits(Convert.ToDecimal(row["QuestionAnswerNum"])));
            }

            tab.AcceptChanges();

            DataSource = tab;
        }
    }
}