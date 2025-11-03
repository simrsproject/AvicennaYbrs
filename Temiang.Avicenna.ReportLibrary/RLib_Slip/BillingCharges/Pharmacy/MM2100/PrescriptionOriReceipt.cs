namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Pharmacy.MM2100
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
    public partial class PrescriptionOriReceipt : Report
    {
        public PrescriptionOriReceipt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            var hd = new TransPrescriptionQuery("a");
            var dt = new TransPrescriptionItemQuery("b");
            var md = new ParamedicQuery("c");
            var reg = new RegistrationQuery("d");
            var pat = new PatientQuery("e");
            var item = new ItemQuery("f");
            var cons = new ConsumeMethodQuery("g");
            var emb = new EmbalaceQuery("h");
            var item2 = new ItemQuery("i");
            var unit = new ServiceUnitQuery("j");
            var room = new ServiceRoomQuery("k");
            var oricons = new ConsumeMethodQuery("l");

            hd.Select(
                pat.MedicalNo,
                hd.Note.ToUpper().As("Note"),
                md.ParamedicName,
                hd.PrescriptionDate,
                hd.PrescriptionNo,
                "<CASE WHEN e.Sex = 'F' THEN RTRIM(RTRIM(e.FirstName + ' ' + e.MiddleName) + ' ' + e.LastName) + ' (NN)' ELSE RTRIM(RTRIM(e.FirstName + ' ' + e.MiddleName) + ' ' + e.LastName) + ' (TN)' END AS PatientName>",
                "<CASE WHEN b.IsRFlag = 0 THEN '' ELSE 'R/' END AS RFlag>",
                "<f.ItemName AS ItemName>",
                dt.SequenceNo,
                dt.ParentNo,
                "<ISNULL(b.OriItemQtyInString, b.ItemQtyInString) AS ItemQtyInString>",
                "<ISNULL(b.OriSRItemUnit, b.SRItemUnit) AS SRItemUnit>",
                dt.OrderText,
                //"<ISNULL(l.SygnaText, g.SygnaText) AS SygnaText>",
                "<ISNULL(l.SRConsumeMethodName, g.SRConsumeMethodName) AS SygnaText>",
                dt.Notes,
                dt.IterText,
                emb.EmbalaceName,
                "<ISNULL(b.OriDosageQty, b.DosageQty) AS DosageQty>",
                "<ISNULL(b.OriSRDosageUnit, b.SRDosageUnit) AS SRDosageUnit>",
                dt.IsCompound,
                "<'' AS Detail>",
                string.Format("<(SELECT HealthcareName FROM Healthcare WHERE HealthcareID = {0}) AS HealthcareName>", printJobParameters[0].ValueString),
                string.Format("<(SELECT AddressLine1 FROM Healthcare WHERE HealthcareID = {0}) AS Address>", printJobParameters[0].ValueString),
                string.Format("<(SELECT City FROM Healthcare WHERE HealthcareID = {0}) AS City>", printJobParameters[0].ValueString),
                "<ISNULL(b.OriConsumeQty, b.ConsumeQty) AS ConsumeQty>",
                dt.TakenQty,
                "<ISNULL(b.OriSRConsumeUnit, b.SRConsumeUnit) AS SRConsumeUnit>",
                pat.DateOfBirth,
                reg.AgeInYear,
                hd.RegistrationNo,
                reg.PatientID,
                emb.EmbalaceLabel,
                @"<j.ServiceUnitName + ' - ' + k.RoomName AS FromServiceUnitRoom>"
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
            hd.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            hd.InnerJoin(room).On(reg.RoomID == room.RoomID);

            hd.Where(hd.PrescriptionNo == printJobParameters[1].ValueString);
            hd.Where(@"<b.SequenceNo LIKE 'd%'>");

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
                    row["Detail"] += string.Format(@"{0} {1} {2} {3}",
                                            "  da in ", row["EmbalaceLabel"], row["ItemQtyInString"], Environment.NewLine);

                    row["Detail"] += string.Format(@"{0}{1}{2} {3} {4}{5}{6}",
                                        row["OrderText"], Environment.NewLine,
                                        row["SygnaText"], row["ConsumeQty"], row["SRConsumeUnit"], Environment.NewLine,
                                        row["Notes"]);
                }
                else row.Delete();
            }

            tab.AcceptChanges();

            if (tab.Rows.Count == 0)
            {
                hd = new TransPrescriptionQuery("a");
                md = new ParamedicQuery("c");
                reg = new RegistrationQuery("d");
                pat = new PatientQuery("e");
                unit = new ServiceUnitQuery("j");
                room = new ServiceRoomQuery("k");

                hd.Select(
                    pat.MedicalNo,
                    hd.Note.ToUpper().As("Note"),
                    md.ParamedicName,
                    hd.PrescriptionDate,
                    hd.PrescriptionNo,
                    "<CASE WHEN e.Sex = 'F' THEN RTRIM(RTRIM(e.FirstName + ' ' + e.MiddleName) + ' ' + e.LastName) + ' (NN)' ELSE RTRIM(RTRIM(e.FirstName + ' ' + e.MiddleName) + ' ' + e.LastName) + ' (TN)' END AS PatientName>",
                    "<'' AS RFlag>",
                    "<'' AS ItemName>",
                    "<'' AS SequenceNo>",
                    "<'' AS ParentNo>",
                    "<'' AS ItemQtyInString>",
                    "<'' AS SRItemUnit>",
                    "<'' AS OrderText>",
                    "<'' AS SygnaText>",
                    "<'' AS Notes>",
                    "<'' AS IterText>",
                    "<'' AS EmbalaceName>",
                    "<'' AS DosageQty>",
                    "<'' AS SRDosageUnit>",
                    "<'' AS IsCompound>",
                    "<'' AS Detail>",
                    string.Format("<(SELECT HealthcareName FROM Healthcare WHERE HealthcareID = {0}) AS HealthcareName>", printJobParameters[0].ValueString),
                    string.Format("<(SELECT AddressLine1 FROM Healthcare WHERE HealthcareID = {0}) AS Address>", printJobParameters[0].ValueString),
                    string.Format("<(SELECT City FROM Healthcare WHERE HealthcareID = {0}) AS City>", printJobParameters[0].ValueString),
                    "<'' AS ConsumeQty>",
                    "<0 AS TakenQty>",
                    "<'' AS SRConsumeUnit>",
                    pat.DateOfBirth,
                    reg.AgeInYear,
                    hd.RegistrationNo,
                    reg.PatientID,
                    "<'' AS EmbalaceLabel>",
                    @"<j.ServiceUnitName + ' - ' + k.RoomName AS FromServiceUnitRoom>"
                    );

                hd.LeftJoin(md).On(hd.ParamedicID == md.ParamedicID);
                hd.InnerJoin(reg).On(hd.RegistrationNo == reg.RegistrationNo);
                hd.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                hd.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                hd.InnerJoin(room).On(reg.RoomID == room.RoomID);

                hd.Where(hd.PrescriptionNo == printJobParameters[1].ValueString);

                tab = hd.LoadDataTable();
            }

            DataSource = tab;

            var presc = new TransPrescription();
            presc.LoadByPrimaryKey(printJobParameters[1].ValueString);
            var regno = presc.RegistrationNo;

            var r = new Registration();
            r.LoadByPrimaryKey(regno);
            var patid = r.PatientID;

            decimal h = 0, w = 0;
            string alergies = string.Empty;

            if (r.SRRegistrationType == "IPR")
            {
                //tinggi badan
                var phrcoll = new PatientHealthRecordLineCollection();
                phrcoll.Query.Where(phrcoll.Query.RegistrationNo == regno, phrcoll.Query.QuestionFormID == "UMUM",
                                phrcoll.Query.QuestionID == "NUT.TB");
                phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
                phrcoll.Query.es.Top = 1;
                phrcoll.LoadAll();
                foreach (var p in phrcoll)
                {
                    h = p.QuestionAnswerNum ?? 0;
                }

                if (h == 0)
                {
                    phrcoll = new PatientHealthRecordLineCollection();
                    phrcoll.Query.Where(phrcoll.Query.RegistrationNo == regno, phrcoll.Query.QuestionFormID == "UMUM",
                                    phrcoll.Query.QuestionID == "BY.DO.LEN");
                    phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
                    phrcoll.Query.es.Top = 1;
                    phrcoll.LoadAll();
                    foreach (var p in phrcoll)
                    {
                        h = p.QuestionAnswerNum ?? 0;
                    }
                }

                //berat badan
                phrcoll = new PatientHealthRecordLineCollection();
                phrcoll.Query.Where(phrcoll.Query.RegistrationNo == regno, phrcoll.Query.QuestionFormID == "UMUM",
                            phrcoll.Query.QuestionID == "NUT.BB");
                phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
                phrcoll.Query.es.Top = 1;
                phrcoll.LoadAll();
                foreach (var p in phrcoll)
                {
                    w = p.QuestionAnswerNum ?? 0;
                }

                if (w == 0)
                {
                    phrcoll = new PatientHealthRecordLineCollection();
                    phrcoll.Query.Where(phrcoll.Query.RegistrationNo == regno, phrcoll.Query.QuestionFormID == "UMUM",
                                phrcoll.Query.QuestionID == "BY.DO.WGH");
                    phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
                    phrcoll.Query.es.Top = 1;
                    phrcoll.LoadAll();
                    foreach (var p in phrcoll)
                    {
                        w = (p.QuestionAnswerNum ?? 0) / 100;
                    }
                }
            }
            else
            {
                //tinggi badan
                var phrcoll = new PatientHealthRecordLineCollection();
                phrcoll.Query.Where(phrcoll.Query.RegistrationNo == regno, phrcoll.Query.QuestionFormID == "PKJRJUMUM1",
                                phrcoll.Query.QuestionID == "NUT.TB");
                phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
                phrcoll.Query.es.Top = 1;
                phrcoll.LoadAll();
                foreach (var p in phrcoll)
                {
                    h = p.QuestionAnswerNum ?? 0;
                }

                if (h == 0)
                {
                    phrcoll = new PatientHealthRecordLineCollection();
                    phrcoll.Query.Where(phrcoll.Query.RegistrationNo == regno, phrcoll.Query.QuestionFormID == "PKJRJUMUM1",
                                    phrcoll.Query.QuestionID == "BY.DO.LEN");
                    phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
                    phrcoll.Query.es.Top = 1;
                    phrcoll.LoadAll();
                    foreach (var p in phrcoll)
                    {
                        h = p.QuestionAnswerNum ?? 0;
                    }
                }

                //berat badan
                phrcoll = new PatientHealthRecordLineCollection();
                phrcoll.Query.Where(phrcoll.Query.RegistrationNo == regno, phrcoll.Query.QuestionFormID == "PKJRJUMUM1",
                                phrcoll.Query.QuestionID == "NUT.BB");
                phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
                phrcoll.Query.es.Top = 1;
                phrcoll.LoadAll();
                foreach (var p in phrcoll)
                {
                    w = p.QuestionAnswerNum ?? 0;
                }

                if (w == 0)
                {
                    phrcoll = new PatientHealthRecordLineCollection();
                    phrcoll.Query.Where(phrcoll.Query.RegistrationNo == regno, phrcoll.Query.QuestionFormID == "PKJRJUMUM1",
                                    phrcoll.Query.QuestionID == "BY.DO.WGH");
                    phrcoll.Query.OrderBy(phrcoll.Query.LastUpdateDateTime.Descending);
                    phrcoll.Query.es.Top = 1;
                    phrcoll.LoadAll();
                    foreach (var p in phrcoll)
                    {
                        w = (p.QuestionAnswerNum ?? 0) / 100;
                    }
                }
            }

            textBox26.Value = string.Format("{0:n0}", h) + " cm  /  " + string.Format("{0:n0}", w) + " kg";

            //alergi
            var pacoll = new PatientAllergyCollection();
            pacoll.Query.Where(pacoll.Query.PatientID == patid);
            pacoll.Query.OrderBy(pacoll.Query.AllergenName.Ascending);
            pacoll.LoadAll();
            foreach (var pa in pacoll)
            {
                if (alergies == string.Empty)
                    alergies += string.Format(@"{0}", pa.DescAndReaction);
                else
                    alergies += string.Format(@"{0} {1}", ", ", pa.DescAndReaction);
            }

            textBox27.Value = alergies;
        }
    }
}