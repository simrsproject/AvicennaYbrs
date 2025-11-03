using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Reporting.Drawing;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Telerik.Reporting;
using System.Linq;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.NursingCare.GPI
{
    public partial class NursingCareNotes : Telerik.Reporting.Report
    {
        public NursingCareNotes(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            FillHeader(printJobParameters[0].ValueString);

            // populate registration no
            var regs = Common.Helper.MergeBilling.GetFullMergeRegistration(printJobParameters[0].ValueString);

            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            var dQuery = new NursingDiagnosaTransDTQuery("a");
            var q30 = new NursingDiagnosaTransDTQuery("b");
            var q10 = new NursingDiagnosaTransDTQuery("c");
            var un = new AppUserQuery("d");
            var nh = new NursingTransHDQuery("e");
            var phr = new PatientHealthRecordQuery("f");
            var phrl = new PatientHealthRecordLineQuery("g");
            var q = new QuestionQuery("h");

            //dQuery.LeftJoin(q30).On(
            //    dQuery.NursingDiagnosaParentID == q30.NursingDiagnosaID &&
            //    q30.TransactionNo == printJobParameters[0].ValueString &&
            //    q30.SRNursingDiagnosaLevel == "30")
            //    .LeftJoin(q10).On(q30.NursingDiagnosaParentID == q10.NursingDiagnosaID &&
            //    q10.TransactionNo == printJobParameters[0].ValueString &&
            //    q10.SRNursingDiagnosaLevel == "10")
            //    .Select(
            //        dQuery,
            //        "<'' as FullImplementationName>",
            //        q10.NursingDiagnosaName.As("Diagnosa"),
            //        q10.Priority.As("DiagnosaPriority"),
            //        un.UserName
            //    ).LeftJoin(un).On(dQuery.LastUpdateByUserID == un.UserID)
            //    .Where(dQuery.TransactionNo == printJobParameters[0].ValueString,
            //    dQuery.SRNursingDiagnosaLevel == "31")
            //    .OrderBy(dQuery.ExecuteDateTime.Ascending);
            dQuery.LeftJoin(q30).On(
                dQuery.NursingDiagnosaParentID == q30.NursingDiagnosaID &&
                q30.TransactionNo == dQuery.TransactionNo &&
                q30.SRNursingDiagnosaLevel == "30")
                .LeftJoin(q10).On(q30.NursingDiagnosaParentID == q10.NursingDiagnosaID &&
                q10.TransactionNo == q30.TransactionNo &&
                q10.SRNursingDiagnosaLevel == "10")
                .InnerJoin(nh).On(dQuery.TransactionNo == nh.TransactionNo)
                .InnerJoin(phr).On(dQuery.ReferenceToPhrNo == phr.TransactionNo)
                .InnerJoin(phrl).On(phr.TransactionNo == phrl.TransactionNo)
                .LeftJoin(q).On(phrl.QuestionID == q.QuestionID)
                .Select(
                    dQuery,
                    "<'' as FullImplementationName>",
                    q10.NursingDiagnosaName.As("Diagnosa"),
                    q10.Priority.As("DiagnosaPriority"),
                    un.UserName,
                    phrl.QuestionAnswerNum.As("AnswerNum"),
                    phrl.QuestionAnswerSuffix.As("AnswerSuffix"),
                    q.QuestionText
                ).LeftJoin(un).On(dQuery.CreateByUserID == un.UserID)
                .Where(nh.RegistrationNo.In(regs),
                dQuery.SRNursingDiagnosaLevel == "31",
                phr.QuestionFormID == "1")
                .OrderBy(dQuery.ExecuteDateTime.Ascending);
            var dttbl = dQuery.LoadDataTable();
            foreach (System.Data.DataRow r in dttbl.Rows)
            {
                r["FullImplementationName"] = NursingDiagnosaTransDT.GetFullImplementationNameFormatted(
                r["NursingDiagnosaName"].ToString(),
                r["S"].ToString(),
                r["O"].ToString(),
                r["A"].ToString(),
                r["P"].ToString());
            }
            dttbl.AcceptChanges();


            this.DataSource = dttbl;
        }

        private void FillHeader(string RegistrationNo)
        {
            var reg = new Temiang.Avicenna.BusinessObject.Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
            {
                //textBox6.Value = reg.RegistrationNo;
                textBox34.Value = reg.BedID;
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                {
                    textBox27.Value = su.ServiceUnitName;
                }
                var room = new ServiceRoom();
                if (room.LoadByPrimaryKey(reg.RoomID))
                {
                    textBox34.Value = room.RoomName + "/" + reg.BedID;
                }

                var ClassPt = new Class();
                if (ClassPt.LoadByPrimaryKey(reg.ClassID))
                {
                    textBox41.Value = ClassPt.ShortName;
                }

                var Medic = new Paramedic();
                if (Medic.LoadByPrimaryKey(reg.ParamedicID))
                {
                    textBox9.Value = Medic.ParamedicName;
                }


                var pat = new Patient();
                if (pat.LoadByPrimaryKey(reg.PatientID))
                {

                    int Agey = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Year;
                    int Agem = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Month;
                    int Aged = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Day;
                    textBox23.Value = pat.PatientName;
                    textBox14.Value = pat.DateOfBirth.Value.ToString("dd-MM-yyyy");
                    textBox40.Value = Agey + " th " + Agem + " bl " + Aged + " Hr";
                    textBox26.Value = pat.MedicalNo + " / " + RegistrationNo;

                    if (pat.Sex == "M")
                    {
                        textBox12.Value = "L";
                    }
                    else
                    {
                        textBox12.Value = "P";
                    }
                }
            }
        }
    }
}