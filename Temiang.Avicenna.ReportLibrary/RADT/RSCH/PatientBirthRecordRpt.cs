namespace Temiang.Avicenna.ReportLibrary.RADT.RSCH
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
    /// Summary description for PatientBirthRecordRpt.
    /// </summary>
    public partial class PatientBirthRecordRpt : Report
    {
        public PatientBirthRecordRpt(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            var br = new BirthRecordQuery("a");
            var breg = new RegistrationQuery("b");
            var mreg = new RegistrationQuery("c");
            var bpat = new PatientQuery("d");
            var mpat = new PatientQuery("e");
            var md = new ParamedicQuery("f");
            var su = new ServiceUnitQuery("g");

            // find service unit officer of pav maria
            string SUOfficer = string.Empty;
            var suPavM = new ServiceUnit();
            if (suPavM.LoadByPrimaryKey("D03.A06")) {
                SUOfficer = suPavM.ServiceUnitOfficer;
            } // 
            

            br.Select(
                bpat.MedicalNo,
                "<'' AS DayName>",
                "<'' AS DateOfBirth>",
                bpat.DateOfBirth.As("DOB"),
                br.TimeOfBirth,
                "<(SELECT HealthcareName FROM Healthcare WHERE HealthcareID = '" + printJobParameters[1].ValueString + "') AS HealthcareName>",
                "<(SELECT City FROM Healthcare WHERE HealthcareID = '" + printJobParameters[1].ValueString + "') AS City>",
                "<CASE WHEN d.Sex = 'M' THEN 'Laki-laki' ELSE 'Perempuan' END AS SexName>",
                br.Weight,
                br.Length,
                br.FatherName,
                mpat.PatientName.As("MotherName"),
                mpat.Address,
                "<'" + SUOfficer + "' as ServiceUnitOfficer>",
                md.ParamedicName
                );

            br.InnerJoin(breg).On(br.RegistrationNo == breg.RegistrationNo);
            br.InnerJoin(bpat).On(breg.PatientID == bpat.PatientID);
            br.InnerJoin(mreg).On(br.MotherRegistrationNo == mreg.RegistrationNo);
            br.InnerJoin(mpat).On(mreg.PatientID == mpat.PatientID);
            br.InnerJoin(md).On(mreg.ParamedicID == md.ParamedicID);
            br.InnerJoin(su).On(mreg.ServiceUnitID == su.ServiceUnitID);

            br.Where(br.RegistrationNo == printJobParameters[0].ValueString);

            var tab = br.LoadDataTable();

            var birthAtt = new BirthAttendantsRecordQuery("a");
            var par = new ParamedicQuery("b");
            birthAtt.Select(par.ParamedicName);
            birthAtt.InnerJoin(par).On(birthAtt.ParamedicID == par.ParamedicID);
            birthAtt.Where(birthAtt.RegistrationNo == printJobParameters[0].ValueString);
            birthAtt.es.Top = 1;
            DataTable dtb = birthAtt.LoadDataTable();
            
            foreach (DataRow row in tab.Rows)
            {
                row["DayName"] = Convert.ToDateTime(row["DOB"]).ToString("dddd", Common.Helper.IndonesianCultureInfo);
                row["DateOfBirth"] = Convert.ToDateTime(row["DOB"]).ToString("dd MMMM yyyy", Common.Helper.IndonesianCultureInfo);

                if (dtb.Rows.Count > 0)
                {
                    row["ParamedicName"] = dtb.Rows[0]["ParamedicName"];
                }
            }

            DataSource = tab;
        }
    }
}