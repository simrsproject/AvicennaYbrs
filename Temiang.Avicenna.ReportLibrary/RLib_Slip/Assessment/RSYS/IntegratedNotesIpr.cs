using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Telerik.Reporting;
using System.Linq;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.RSYS
{
    public partial class IntegratedNotesIpr : Telerik.Reporting.Report
    {
        public IntegratedNotesIpr(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);


            var regNo = printJobParameters[0].ValueString;
            var srUserType = printJobParameters[1].ValueString;
            FillHeader(regNo, srUserType);



            // populate registration no
            var regs = Common.Helper.MergeBilling.GetFullMergeRegistration(regNo);

            // Nursing Notes
            var dQuery = new NursingDiagnosaTransDTQuery("a");
            var q30 = new NursingDiagnosaTransDTQuery("b");
            var q10 = new NursingDiagnosaTransDTQuery("c");
            var un = new AppUserQuery("d");
            var up = new AppUserQuery("dd");
            var nh = new NursingTransHDQuery("e");

            dQuery.LeftJoin(q30).On(
                dQuery.NursingDiagnosaParentID == q30.NursingDiagnosaID &&
                q30.TransactionNo == dQuery.TransactionNo &&
                q30.SRNursingDiagnosaLevel == "30")
                .LeftJoin(q10).On(q30.NursingDiagnosaParentID == q10.NursingDiagnosaID &&
                q10.TransactionNo == q30.TransactionNo &&
                q10.SRNursingDiagnosaLevel == "10")
                .InnerJoin(nh).On(dQuery.TransactionNo == nh.TransactionNo)
                .Select(
                    dQuery.ID,
                    "<FORMAT(a.ExecuteDateTime, 'dd-MMM-yyyy HH:mm') as ExecuteDateTime>",
                    dQuery.ExecuteDateTime.As("DateTrans"),
                    dQuery.S,
                    dQuery.O,
                    dQuery.A,
                    dQuery.P,
                    dQuery.PpaInstruction,
                    "<CASE WHEN a.NursingDiagnosaName ='' THEN 'S O A P' else a.NursingDiagnosaName END AS NursingDiagnosaName>",
                    //dQuery.NursingDiagnosaName,
                    dQuery.Respond,
                    "<FORMAT (a.ApprovedDatetime, 'dd-MMM-yyyy HH:mm') as ApprovedDatetime>",
                    up.UserName.As("PPA"),
                    "<'' as FullImplementationName>",
                    q10.NursingDiagnosaName.As("Diagnosa"),
                    q10.Priority.As("DiagnosaPriority"),
                    un.UserName
                ).LeftJoin(un).On(dQuery.ApprovedByUserID == un.UserID)
                .LeftJoin(up).On(dQuery.CreateByUserID == up.UserID)
                .Where(nh.RegistrationNo.In(regs))
                //dQuery.SRNursingDiagnosaLevel.In("40","31"), dQuery.NursingDiagnosaName == "S O A P")            
                .Where(@"<(a.SRNursingDiagnosaLevel = '40' OR (a.SRNursingDiagnosaLevel = '31' AND a.NursingDiagnosaName = 'S O A P'))>")            
                .OrderBy(dQuery.ExecuteDateTime.Ascending);
            if (!string.IsNullOrWhiteSpace(srUserType))
                dQuery.Where(dQuery.SRUserType == srUserType);

            var dttbl = dQuery.LoadDataTable();


            // Soap Assessment
            var rimQuery = new RegistrationInfoMedicQuery("rim");
            var rimUsr = new AppUserQuery("d");
            rimQuery.InnerJoin(rimUsr).On(rimQuery.CreatedByUserID == rimUsr.UserID);
            rimQuery.Select(
                "<CONVERT(BIGINT,0) as ID>",
                "<FORMAT(rim.DateTimeInfo, 'dd-MM-yyyy HH:mm') As ExecuteDateTime>",
                rimQuery.DateTimeInfo.As("DateTrans"),
                rimQuery.Info1.As("S"),
                rimQuery.Info2.As("O"),
                rimQuery.Info3.As("A"),
                rimQuery.Info4.As("P"),
                rimQuery.SRMedicalNotesInputType.As("NursingDiagnosaName"),
                "<'' as Respond>",
                 "<FORMAT (rim.ApprovedDatetime, 'dd-MM-yyyy HH:mm') as ApprovedDatetime>",
                "<'' as FullImplementationName>",
                "<'' as Diagnosa>",
            "<0 as DiagnosaPriority>",
                rimUsr.UserName
            ); ;
            rimQuery.Where(rimQuery.RegistrationNo.In(regs), rimQuery.SRMedicalNotesInputType != "MDS", rimQuery.SRMedicalNotesInputType != "CON", rimQuery.SRMedicalNotesInputType != "REF");

            if (!string.IsNullOrWhiteSpace(srUserType))
                rimQuery.Where(rimQuery.SRUserType == srUserType);

            var dtbRim = rimQuery.LoadDataTable();

            dttbl.Merge(dtbRim);

            var sorted = dttbl.Select(null, "DateTrans ASC").CopyToDataTable();

            var i = 0;
            foreach (System.Data.DataRow r in sorted.Rows)
            {
                r["ID"] = i;
                i++;

                r["FullImplementationName"] = NursingDiagnosaTransDT.GetFullImplementationNameFormatted(
                    r["NursingDiagnosaName"].ToString(),
                    r["S"].ToString(),
                    r["O"].ToString(),
                    r["A"].ToString(),
                    r["P"].ToString());
            }
            sorted.AcceptChanges();


            this.DataSource = sorted;
        }

        private void FillHeader(string registrationNo, string srUserType)
        {
                var reg = new Temiang.Avicenna.BusinessObject.Registration();
                if (reg.LoadByPrimaryKey(registrationNo))
                {
                    //textBox6.Value = reg.RegistrationNo;
                    //textBox24.Value = reg.AgeInYear.ToString() + "yr " + reg.AgeInMonth.ToString() + "mth";
                    textBox22.Value = reg.BedID;
                    var su = new ServiceUnit();
                    if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                    {
                        textBox21.Value = su.ServiceUnitName;
                    }
                    var room = new ServiceRoom();
                    if (room.LoadByPrimaryKey(reg.RoomID))
                    {
                        textBox19.Value = room.RoomName;
                    }
                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(reg.PatientID))
                    {
                        textBox8.Value = pat.PatientName;
                        textBox12.Value = pat.DateOfBirth.Value.ToString("dd-MM-yyyy");
                        textBox14.Value = pat.MedicalNo;// +" | " + RegistrationNo;
                        textBox25.Value = pat.Ssn;
                                                    //textBox19.Value = pat.Sex;
                }
                }

            if (!string.IsNullOrWhiteSpace(srUserType))
            {
                var stdi = new AppStandardReferenceItem();
                stdi.LoadByPrimaryKey("UserType", srUserType);
                txtUserType.Value = string.Format("Oleh : {0}",stdi.ItemName);
            }
            else
            {
                txtUserType.Value = "";
            }

        }
    }
}