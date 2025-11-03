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


namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.NursingCare
{
    public partial class DiagnoseAndPlanning : Telerik.Reporting.Report
    {
        public DiagnoseAndPlanning(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            FillHeader(printJobParameters[0].ValueString);

            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            var dQuery = new NursingDiagnosaTransDTQuery("a");
            var un = new AppUserQuery("b");

            dQuery.LeftJoin(un).On(dQuery.LastUpdateByUserID == un.UserID)
                .Where(dQuery.TransactionNo == printJobParameters[0].ValueString,
                    dQuery.SRNursingDiagnosaLevel == "10")
                .Select(dQuery, un.UserName)
                .OrderBy(dQuery.Priority.Ascending);
            var dttbl = dQuery.LoadDataTable();

            this.DataSource = dttbl;
            //table1.DataSource = ReportItem.DataObject;

            var report4 = new DiagnoseAndPlanningSub1Rpt();
            report4.NeedDataSource += new System.EventHandler(subReport4_NeedDataSource);
            subReport4.ReportSource = new InstanceReportSource { ReportDocument = report4 };

            var report5 = new DiagnoseAndPlanningSub1Rpt();
            report5.NeedDataSource += new System.EventHandler(subReport5_NeedDataSource);
            subReport5.ReportSource = new InstanceReportSource { ReportDocument = report5 };

            var report6 = new DiagnoseAndPlanningSub1Rpt();
            report6.NeedDataSource += new System.EventHandler(subReport6_NeedDataSource);
            subReport6.ReportSource = new InstanceReportSource { ReportDocument = report6 };
        }

        private void subReport4_NeedDataSource(object sender, System.EventArgs e)
        { 
            var rpt = (Telerik.Reporting.Processing.Report)sender;
            long ID = (long)rpt.Parent.Parent.DataObject["ID"];
            var diagID = rpt.Parent.Parent.DataObject["NursingDiagnosaID"].ToString();
            var diagName = rpt.Parent.Parent.DataObject["NursingDiagnosaName"].ToString();
            var transNo = rpt.Parent.Parent.DataObject["TransactionNo"].ToString();


            var dttbl = NursingDiagnosaTransDT.NursingProblem(transNo, "11", diagID);
            var dstbl = new System.Data.DataTable();
            dstbl.Columns.Add("Header", typeof(string));
            dstbl.Columns.Add("ParentName", typeof(string));
            dstbl.Columns.Add("ChildName", typeof(string));
            dstbl.Columns.Add("HideParent", typeof(bool));
            dstbl.Columns.Add("HideChild", typeof(bool));

            // add Diagnosa Name With Empty Child
            var rowDiag = dstbl.NewRow();
            rowDiag["Header"] = string.Empty;
            rowDiag["ParentName"] = diagName;
            rowDiag["ChildName"] = string.Empty;
            rowDiag["HideParent"] = false;
            rowDiag["HideChild"] = false;
            dstbl.Rows.Add(rowDiag);

            // add etiology / problems
            foreach(System.Data.DataRow r in dttbl.Rows){
                if (r["TransNursingDiagnosaID"] is System.DBNull) continue;
                var row = dstbl.NewRow();
                row["Header"] = string.Empty;
                row["ParentName"] = "Berhubungan dengan:";
                row["ChildName"] = r["NursingDiagnosaNameEdited"];
                row["HideParent"] = false;
                row["HideChild"] = false;
                dstbl.Rows.Add(row);
            }

            // add identifier DS/DO
            var rowDSDO = dstbl.NewRow();
            rowDSDO["Header"] = string.Empty;
            //rowDSDO["ParentName"] = "Ditandai dengan";
            rowDSDO["ParentName"] = HelperMirror.FirstCharToUpper(AppParameter.GetParameterValue(AppParameter.ParameterItem.NsSymptom));
            rowDSDO["ChildName"] = string.Empty;
            rowDSDO["HideParent"] = false;
            rowDSDO["HideChild"] = true;
            dstbl.Rows.Add(rowDSDO);

            var lDSDO = NursingDiagnosaTransDT.NursingAssessmentForPrint(transNo, ID);

            // add DS
            var lDS = lDSDO.Where(x => x.IsSubjective == true);
            foreach (var ds in lDS) {
                var rowDS = dstbl.NewRow();
                rowDS["Header"] = string.Empty;
                rowDS["ParentName"] = "Data Subjektif:";
                rowDS["ChildName"] = ds.AssessmentName;
                rowDS["HideParent"] = false;
                rowDS["HideChild"] = false;
                dstbl.Rows.Add(rowDS);
            }

            // add DO
            var lDO = lDSDO.Where(x => x.IsObjective == true).OrderBy(x => x.IsAlwaysShow);
            foreach (var ds in lDO)
            {
                var rowDO = dstbl.NewRow();
                rowDO["Header"] = string.Empty;
                rowDO["ParentName"] = "Data Objektif:";
                rowDO["ChildName"] = ds.AssessmentName;
                rowDO["HideParent"] = false;
                rowDO["HideChild"] = false;
                dstbl.Rows.Add(rowDO);
            }

            rpt.DataSource = dstbl;
        }

        private void subReport5_NeedDataSource(object sender, System.EventArgs e)
        {
            var rpt = (Telerik.Reporting.Processing.Report)sender;
            //var diagID = rpt.DataObject["NursingDiagnosaID"].ToString();
            var diagID = System.Convert.ToInt64(rpt.Parent.Parent.DataObject["ID"]);
            var diagName = rpt.Parent.Parent.DataObject["NursingDiagnosaName"].ToString();
            var transNo = rpt.Parent.Parent.DataObject["TransactionNo"].ToString();

            var EvalPeriod = System.Convert.ToInt16(rpt.DataObject["EvalPeriod"]);
            var PeriodConversionInHour = System.Convert.ToInt16(rpt.DataObject["PeriodConversionInHour"]);


            var dttbl = NursingDiagnosaTransDT.NursingTarget(transNo, diagID);
            var dstbl = new System.Data.DataTable();
            dstbl.Columns.Add("Header", typeof(string));
            dstbl.Columns.Add("ParentName", typeof(string));
            dstbl.Columns.Add("ChildName", typeof(string));
            dstbl.Columns.Add("HideParent", typeof(bool));
            dstbl.Columns.Add("HideChild", typeof(bool));

            // add Diagnosa Name With Empty Child
            foreach (System.Data.DataRow row in dttbl.Rows)
            {
                if (row["TransNursingDiagnosaID"] is System.DBNull) continue;
                var rowDiag = dstbl.NewRow();
                rowDiag["Header"] = "Setelah dilakukan tindakan keperawatan selama " +
                    EvalPeriod.ToString() + " x " + PeriodConversionInHour.ToString() + " jam " + diagName + " teratasi dengan kriteria hasil:";
                rowDiag["ParentName"] = row["NocName"].ToString();
                rowDiag["ChildName"] = row["NursingDiagnosaNameEdited"].ToString();
                rowDiag["HideParent"] = row["NocName"].ToString().Contains("NOC"); // filter yang NOC-nya ada kata "NOC" brarti virtual, jangan dimunculkan
                rowDiag["HideChild"] = false;
                dstbl.Rows.Add(rowDiag);
            }

            rpt.DataSource = dstbl;
        }

        private void subReport6_NeedDataSource(object sender, System.EventArgs e)
        {
            var rpt = (Telerik.Reporting.Processing.Report)sender;
            //var diagID = rpt.DataObject["NursingDiagnosaID"].ToString();
            var diagID = System.Convert.ToInt64(rpt.Parent.Parent.DataObject["ID"]);
            var diagName = rpt.Parent.Parent.DataObject["NursingDiagnosaName"].ToString();
            var transNo = rpt.Parent.Parent.DataObject["TransactionNo"].ToString();

            var dttbl = NursingDiagnosaTransDT.NursingPlanning(diagID);
            var dstbl = new System.Data.DataTable();
            dstbl.Columns.Add("Header", typeof(string));
            dstbl.Columns.Add("ParentName", typeof(string));
            dstbl.Columns.Add("ChildName", typeof(string));
            dstbl.Columns.Add("HideParent", typeof(bool));
            dstbl.Columns.Add("HideChild", typeof(bool));

            // add Diagnosa Name With Empty Child
            foreach (System.Data.DataRow row in dttbl.Rows)
            {
                if (row["TransNursingDiagnosaID"] is System.DBNull) continue;
                var rowDiag = dstbl.NewRow();
                rowDiag["Header"] = string.Empty;
                rowDiag["ParentName"] = row["NursingParentName"].ToString();
                rowDiag["ChildName"] = row["NursingDiagnosaNameEdited"].ToString();
                rowDiag["HideParent"] = true;
                rowDiag["HideChild"] = false;
                dstbl.Rows.Add(rowDiag);
            }

            rpt.DataSource = dstbl;
        }

        private void FillHeader(string TransactionNo) {
            var nh = new NursingTransHD();
            if (nh.LoadByPrimaryKey(TransactionNo))
            {
                var reg = new Temiang.Avicenna.BusinessObject.Registration();
                if (reg.LoadByPrimaryKey(nh.RegistrationNo))
                {
                    //textBox6.Value = reg.RegistrationNo;
                    //textBox34.Value = reg.BedID;
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
                        textBox6.Value = Medic.ParamedicName;
                    }


                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(reg.PatientID))
                    {

                        int Agey = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Year;
                        int Agem = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Month;
                        int Aged = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Day;
                        textBox23.Value = pat.PatientName;
                        textBox24.Value = pat.DateOfBirth.Value.ToString("dd-MM-yyyy");
                        textBox40.Value = Agey + " th " + Agem + " bl " + Aged + " Hr";
                        textBox26.Value = pat.MedicalNo + " / " + nh.RegistrationNo;
                        textBox20.Value = pat.Ssn;

                        if (pat.Sex == "M")
                        {
                            textBox8.Value = "L";
                        }
                        else
                        {
                            textBox8.Value = "P";
                        }
                    }
                }
            }
        }
    }
}