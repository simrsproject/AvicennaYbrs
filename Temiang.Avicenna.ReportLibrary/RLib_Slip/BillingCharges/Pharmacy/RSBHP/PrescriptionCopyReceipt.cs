namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Pharmacy.RSBHP
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
    /// Summary description for PrescriptionCopyReceipt.
    /// </summary>
    public partial class PrescriptionCopyReceipt : Report
    {
        public PrescriptionCopyReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            Helper.InitializeLogoOnlyLeft(reportHeaderSection1);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            var hd = new TransPrescriptionQuery("a");
            var dt = new TransPrescriptionItemQuery("b");
            var md = new ParamedicQuery("c");
            var reg = new RegistrationQuery("d");
            var pat = new PatientQuery("e");
            var item = new ItemQuery("f");
            var item2 = new ItemQuery("x");
            var cons = new ConsumeMethodQuery("g");
            var emb = new EmbalaceQuery("h");
            var oricons = new ConsumeMethodQuery("i");

            hd.Select(
                hd.Note,
                md.ParamedicName,
                hd.PrescriptionDate,
                hd.PrescriptionNo,
                pat.PatientName,
                "<RTRIM(RTRIM(e.FirstName + ' ' + e.MiddleName) + ' ' + e.LastName) AS PatientName>",
                "<CASE WHEN b.IsRFlag = 0 THEN '' ELSE 'R/' END AS RFlag>",
                "<f.ItemName AS ItemName>",
                dt.SequenceNo,
                dt.ParentNo,
                "<ISNULL(b.OriItemQtyInString, b.ItemQtyInString) AS ItemQtyInString>",
                //dt.ItemQtyInString,
                "<ISNULL(b.OriSRItemUnit, b.SRItemUnit) AS SRItemUnit>",
                //dt.SRItemUnit,
                dt.OrderText,
                "<ISNULL(i.SygnaText, g.SygnaText) AS SygnaText>",
                //cons.SygnaText,
                dt.Notes,
                dt.IterText,
                emb.EmbalaceName,
                "<ISNULL(b.OriDosageQty, b.DosageQty) AS DosageQty>",
                //dt.DosageQty,
                "<ISNULL(b.OriSRDosageUnit, b.SRDosageUnit) AS SRDosageUnit>",
                //dt.SRDosageUnit,
                dt.IsCompound,
                "<'' AS Detail>",
                string.Format("<(SELECT HealthcareName FROM Healthcare WHERE HealthcareID = {0}) AS HealthcareName>", printJobParameters[0].ValueString),
                string.Format("<(SELECT City FROM Healthcare WHERE HealthcareID = {0}) AS City>", printJobParameters[0].ValueString),
                "<ISNULL(b.OriConsumeQty, b.ConsumeQty) AS ConsumeQty>",
                //dt.ConsumeQty,
                dt.TakenQty,
                hd.Note,
                "<ISNULL(b.OriSRConsumeUnit, b.SRConsumeUnit) AS SRConsumeUnit>",
                //dt.SRConsumeUnit,
                pat.MedicalNo
                );

            hd.LeftJoin(dt).On(hd.PrescriptionNo == dt.PrescriptionNo);
            hd.LeftJoin(md).On(hd.ParamedicID == md.ParamedicID);
            hd.InnerJoin(reg).On(hd.RegistrationNo == reg.RegistrationNo);
            hd.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            hd.LeftJoin(item).On(dt.ItemID == item.ItemID);
            hd.LeftJoin(item2).On(dt.ItemInterventionID == item2.ItemID);
            hd.LeftJoin(cons).On(dt.SRConsumeMethod == cons.SRConsumeMethod);
            hd.LeftJoin(oricons).On(dt.OriSRConsumeMethod == oricons.SRConsumeMethod);
            hd.LeftJoin(emb).On(dt.EmbalaceID == emb.EmbalaceID);

            hd.Where(hd.PrescriptionNo == printJobParameters[1].ValueString);

            var tab = hd.LoadDataTable();
            var tmp = tab.Copy();

            foreach (DataRow row in tab.Rows)
            {
                if (row["IsCompound"] == DBNull.Value) continue;

                if (!Convert.ToBoolean(row["IsCompound"]))
                {
                    row["Detail"] = string.Format(@"{0} {1} {2}{3}{4}{5}{6} {7} {8}{9}{10}",
                                        row["ItemName"], row["ItemQtyInString"], row["SRItemUnit"], Environment.NewLine,
                                        row["OrderText"], Environment.NewLine,
                                        row["SygnaText"], row["ConsumeQty"], row["SRConsumeUnit"], Environment.NewLine,
                                        row["Notes"]);
                }
                else if (Convert.ToBoolean(row["IsCompound"]) && string.IsNullOrEmpty(row["ParentNo"].ToString()))
                {
                    row["Detail"] = string.Format(@"{0} {1} {2}{3}",
                                        row["ItemName"], row["DosageQty"], row["SRDosageUnit"], Environment.NewLine);

                    var compound = tmp.AsEnumerable().Where(t => t.Field<string>("ParentNo") == row["SequenceNo"].ToString());
                    foreach (DataRow comp in compound)
                    {
                        row["Detail"] += string.Format(@"{0} {1} {2}{3}",
                                            comp["ItemName"], comp["DosageQty"], comp["SRDosageUnit"], Environment.NewLine);
                    }

                    row["Detail"] += string.Format(@"{0}{1}{2} {3} {4}{5}{6}",
                                        row["OrderText"], Environment.NewLine,
                                        row["SygnaText"], row["ConsumeQty"], row["EmbalaceName"], Environment.NewLine,
                                        row["Notes"]);
                }
                else row.Delete();
            }

            tab.AcceptChanges();

            DataSource = tab;
        }
    }
}