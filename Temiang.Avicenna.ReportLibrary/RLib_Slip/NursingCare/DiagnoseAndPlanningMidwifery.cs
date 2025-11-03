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
    public partial class DiagnoseAndPlanningMidwifery : Telerik.Reporting.Report
    {
        public DiagnoseAndPlanningMidwifery(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            FillHeader(printJobParameters[0].ValueString);

            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            var dttbl = NursingDiagnosaTransDT.NursingDiagnosaFullDefinition(printJobParameters[0].ValueString);
            var cTransNo = dttbl.Columns.Add("TransactionNo", typeof(string));
            foreach (System.Data.DataRow r in dttbl.Rows) {
                r["TransactionNo"] = printJobParameters[0].ValueString;
            }

            this.DataSource = dttbl;
            //table1.DataSource = ReportItem.DataObject;

            var report1 = new NursingCareSub2Rpt();
            //report1.Width = new Telerik.Reporting.Drawing.Unit(1.5,Telerik.Reporting.Drawing.UnitType.Inch);
            ////report1.Groups.
            //var pz = new Telerik.Reporting.Drawing.SizeU(report1.Width, report1.PageSettings.PaperSize.Height);
            //report1.PageSettings.PaperSize = pz;
            report1.NeedDataSource += new System.EventHandler(subReport1_NeedDataSource);
            subReport1.ReportSource = new InstanceReportSource { ReportDocument = report1 };

            var report2 = new NursingCareSub2Rpt();
            report2.NeedDataSource += new System.EventHandler(subReport2_NeedDataSource);
            subReport2.ReportSource = new InstanceReportSource { ReportDocument = report2 };

            var report3 = new NursingCareSub2Rpt();
            report3.NeedDataSource += new System.EventHandler(subReport3_NeedDataSource);
            subReport3.ReportSource = new InstanceReportSource { ReportDocument = report3 };

            var report4 = new NursingCareSub2Rpt();
            report4.NeedDataSource += new System.EventHandler(subReport4_NeedDataSource);
            subReport4.ReportSource = new InstanceReportSource { ReportDocument = report4 };
        }

        private void subReport1_NeedDataSource(object sender, System.EventArgs e)
        { 
            var rpt = (Telerik.Reporting.Processing.Report)sender;
            long ID = (long)rpt.Parent.Parent.DataObject["ID"];
            var diagNSID = rpt.Parent.Parent.DataObject["NursingDiagnosaID"].ToString();
            var diagName = rpt.Parent.Parent.DataObject["NursingDiagnosaNameDisplay"].ToString();
            var transNo = rpt.Parent.Parent.DataObject["TransactionNo"].ToString();


            var dttbl = NursingDiagnosaTransDT.NursingProblem(transNo, "11", diagNSID);
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
                if (r["SRNsEtiologyType"] is System.DBNull) r["SRNsEtiologyType"] = "01";
                if (r["SRNsEtiologyType"].ToString() == "01") continue;

                var row = dstbl.NewRow();
                row["Header"] = string.Empty;
                row["ParentName"] = string.Empty;
                row["ChildName"] = r["NursingDiagnosaNameEdited"];
                row["HideParent"] = false;
                row["HideChild"] = false;
                dstbl.Rows.Add(row);
            }
            rpt.DataSource = dstbl;
        }

        private void subReport2_NeedDataSource(object sender, System.EventArgs e)
        {
            var rpt = (Telerik.Reporting.Processing.Report)sender;
            var diagNSID = rpt.Parent.Parent.DataObject["NursingDiagnosaID"].ToString();
            var diagID = System.Convert.ToInt64(rpt.Parent.Parent.DataObject["ID"]);
            var diagName = rpt.Parent.Parent.DataObject["NursingDiagnosaName"].ToString();
            var transNo = rpt.Parent.Parent.DataObject["TransactionNo"].ToString();

            var dttbl = NursingDiagnosaTransDT.NursingProblem(transNo, "11", diagNSID);
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
                if (row["SRNsEtiologyType"] is System.DBNull) row["SRNsEtiologyType"] = "01";
                if (row["SRNsEtiologyType"].ToString() == "01") continue;

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

        private void subReport3_NeedDataSource(object sender, System.EventArgs e)
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
                rowDiag["Header"] = "";
                rowDiag["ParentName"] = row["NocName"].ToString();
                rowDiag["ChildName"] = row["NursingDiagnosaNameEdited"].ToString();
                rowDiag["HideParent"] = row["NocName"].ToString().Contains("NOC"); // filter yang NOC-nya ada kata "NOC" brarti virtual, jangan dimunculkan
                rowDiag["HideChild"] = false;
                dstbl.Rows.Add(rowDiag);
            }

            rpt.DataSource = dstbl;
        }

        private void subReport4_NeedDataSource(object sender, System.EventArgs e)
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
                    textBox34.Value = reg.BedID;
                    var su = new ServiceUnit();
                    if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                    {
                        textBox27.Value = su.ServiceUnitName;
                    }
                    var room = new ServiceRoom();
                    if (room.LoadByPrimaryKey(reg.RoomID))
                    {
                        textBox34.Value = room.RoomName;
                    }

                    var ClassPt = new Class();
                    if (ClassPt.LoadByPrimaryKey(reg.ClassID))
                    {
                        textBox41.Value = ClassPt.ShortName;
                    }

                    var Medic = new Paramedic();
                    if (Medic.LoadByPrimaryKey(reg.ParamedicID))
                    {
                        textBox45.Value = Medic.ParamedicName;
                    }


                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(reg.PatientID))
                    {

                        int Agey = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Year;
                        int Agem = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Month;
                        int Aged = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Day;
                        textBox23.Value = pat.PatientName;
                        textBox8.Value = pat.DateOfBirth.Value.ToString("dd-MM-yyyy");
                        textBox40.Value = Agey + " th " + Agem + " bl " + Aged + " Hr";
                        textBox26.Value = pat.MedicalNo + " / " + nh.RegistrationNo;

                        if (pat.Sex == "M")
                        {
                            textBox43.Value = "L";
                        }
                        else
                        {
                            textBox43.Value = "P";
                        }
                    }
                }
            }
        }
    }
}