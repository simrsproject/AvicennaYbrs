using System.Linq;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges.RSCH
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
    /// Summary description for BuktiPemeriksaan.
    /// </summary>
    public partial class BuktiPemeriksaan : Report
    {
        public BuktiPemeriksaan(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            //Helper.InitializeLogo(reportHeaderSection1);
            var dt = new TransChargesItemQuery("a");
            var hd = new TransChargesQuery("b");
            var reg = new RegistrationQuery("c");
            var pat = new PatientQuery("d");
            var grr = new GuarantorQuery("e");
            var item = new ItemQuery("f");
            var unit = new ServiceUnitQuery("g");
            var md = new ParamedicQuery("h");
            var unit2 = new ServiceUnitQuery("i");
            var grr2 = new GuarantorQuery("j");
            var mb = new MergeBillingQuery("k");

            dt.es.Distinct = true;
            dt.Select(
                unit.ServiceUnitName,
                unit2.ServiceUnitName.As("ServiceUnit2Name"),
                hd.TransactionNo,
                hd.ExecutionDate.As("TransactionDate"),
                reg.RegistrationNo,
                pat.MedicalNo,
                pat.PatientName,
                
                "<CAST(c.AgeInYear AS VARCHAR(2)) + 'y ' + CAST(c.AgeInMonth AS VARCHAR(2)) + 'm ' + CAST(c.AgeInDay AS VARCHAR(2)) + 'd' AS Age>",
                //reg.BedID,
                @"<CASE WHEN c.IsConsul = 1 THEN ISNULL((SELECT qr.BedID 
                    FROM MergeBilling qmb 
                    INNER JOIN Registration qr ON qr.RegistrationNo = qmb.FromRegistrationNo
                    WHERE qmb.RegistrationNo = b.RegistrationNo
                ), c.BedID) ELSE c.BedID END AS 'BedID'>",
                "<CASE WHEN ISNULL(c.PhysicianSenders, '') = '' THEN h.ParamedicName ELSE c.PhysicianSenders END AS ParamedicName>",
                pat.Address,
                "<RTRIM(d.PhoneNo + ' ') + d.MobilePhoneNo AS PhoneNo>",
                "<CASE WHEN j.GuarantorID like 'KUM00%' THEN e.GuarantorName ELSE j.GuarantorName END AS GuarantorName>",
                "<CASE WHEN ISNULL(a.ParamedicCollectionName, '') = '' then f.ItemName else f.ItemName + ' (' + a.ParamedicCollectionName + ')' END AS ItemName>" ,
                dt.SequenceNo,
                @"<a.ChargeQuantity + ISNULL((SELECT SUM(x.ChargeQuantity) 
                    FROM TransChargesItem x 
                    INNER JOIN TransCharges y ON y.TransactionNo = x.TransactionNo AND y.IsCorrection = 1
                    WHERE x.ReferenceNo = a.TransactionNo AND x.ReferenceSequenceNo = a.SequenceNo AND x.IsApprove = 1
                ), 0) AS 'ChargeQuantity'>",
                dt.Price,
                dt.DiscountAmount,
                dt.CitoAmount,
                string.Format("<'{0}' AS UserName>", printJobParameters[1].ValueString),
                dt.ReferenceNo, 
                dt.ReferenceSequenceNo,
                @"<CAST(0 AS BIT) AS 'IsDeleted'>",
                @"<ISNULL((SELECT qmb.FromRegistrationNo
                    FROM MergeBilling qmb 
                    INNER JOIN Registration qr ON qr.RegistrationNo = qmb.FromRegistrationNo AND qr.SRRegistrationType = 'IPR'
                    WHERE qmb.RegistrationNo = b.RegistrationNo
                ), '') AS 'RegistrationNoIPR'>"
                );
            dt.InnerJoin(hd).On(dt.TransactionNo == hd.TransactionNo);
            dt.InnerJoin(reg).On(hd.RegistrationNo == reg.RegistrationNo);
            dt.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            dt.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
            dt.InnerJoin(grr2).On(grr.GuarantorHeaderID == grr2.GuarantorID);
            dt.InnerJoin(item).On(dt.ItemID == item.ItemID);
            dt.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            dt.LeftJoin(md).On(reg.ParamedicID == md.ParamedicID);
            dt.LeftJoin(unit2).On(hd.ToServiceUnitID == unit2.ServiceUnitID);
            dt.LeftJoin(mb).On(reg.RegistrationNo == mb.RegistrationNo);

            dt.Where(
                hd.TransactionNo == printJobParameters[0].ValueString,
                hd.IsApproved == true,
                dt.IsVoid == false
                );

            DataTable table = dt.LoadDataTable();

            foreach (
                DataRow row in
                    table.Rows.Cast<DataRow>().Where(
                        row => ((decimal)row["ChargeQuantity"] == 0)))
            {
                row.Delete();
            }

            table.AcceptChanges();

            var noregri = (table.Rows.Count == 0) ? "" : table.Rows[0]["RegistrationNoIPR"].ToString().Trim();

            DataSource = table;

            textBox47.Visible = textBox48.Visible = textBox49.Visible = (noregri != string.Empty);
        }
    }
}