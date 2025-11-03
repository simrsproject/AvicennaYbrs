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
    public partial class NursingCareProgressNotes : Telerik.Reporting.Report
    {
        public NursingCareProgressNotes(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            FillHeader(printJobParameters[0].ValueString);

            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            var dQuery = new NursingDiagnosaTransDTQuery("a");
            var d10 = new NursingDiagnosaTransDTQuery("b");
            var stdP = new AppStandardReferenceItemQuery("c");
            var un = new AppUserQuery("d");
            var nde = new NursingDiagnosaEvaluationQuery("nde");
            var iRef = new NursingDiagnosaTransDTQuery("iRef");
            var stdPRef = new AppStandardReferenceItemQuery("stdPRef");

            dQuery.InnerJoin(d10).On(
                 dQuery.ParentID == d10.ID &&
                 d10.TransactionNo == printJobParameters[0].ValueString && 
                 d10.SRNursingDiagnosaLevel == "10" &&
                 dQuery.SRNursingDiagnosaLevel == "40") 
                .LeftJoin(un).On(dQuery.CreateByUserID == un.UserID)
                .LeftJoin(stdP).On(stdP.StandardReferenceID == "NursingCarePlanning" && stdP.ItemID == dQuery.SRNursingCarePlanning)
                .LeftJoin(nde).On(dQuery.ID == nde.EvaluationID)
                .LeftJoin(iRef).On(nde.InterventionID == iRef.ID)
                .LeftJoin(stdPRef).On(stdPRef.StandardReferenceID == "NursingCarePlanning" && stdPRef.ItemID == nde.SRNursingCarePlanning)
                .Select(
                    dQuery,
                    d10.NursingDiagnosaName.As("Diagnosa"),
                    d10.Priority.As("DiagnosaPriority"),
                    stdP.ItemName.As("Planning"),
                    un.UserName,
                    nde.InterventionID, 
                    nde.SRNursingCarePlanning, 
                    iRef.NursingDiagnosaName.As("InterventionName"), 
                    stdPRef.ItemName.As("SRNursingCarePlanningRef")
                )
                .OrderBy(d10.Priority.Ascending, dQuery.ExecuteDateTime.Ascending);
            var dttbl = dQuery.LoadDataTable();

            this.DataSource = dttbl;
        }

        private void FillHeader(string TransactionNo) {
            var nh = new NursingTransHD();
            if (nh.LoadByPrimaryKey(TransactionNo))
            {
                var reg = new Temiang.Avicenna.BusinessObject.Registration();
                if (reg.LoadByPrimaryKey(nh.RegistrationNo))
                {
                    //textBox6.Value = reg.RegistrationNo;
                    textBox14.Value = reg.BedID;
                    var su = new ServiceUnit();
                    if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                    {
                        textBox27.Value = "Ruangan " + su.ServiceUnitName;
                    }
                    var room = new ServiceRoom();
                    if (room.LoadByPrimaryKey(reg.RoomID))
                    {
                        textBox14.Value = room.RoomName + "/" + reg.BedID;
                    }

                    var ClassPt = new Class();
                    if (ClassPt.LoadByPrimaryKey(reg.ClassID))
                    {
                        textBox41.Value = ClassPt.ShortName;
                    }

                    var Medic = new Paramedic();
                    if (Medic.LoadByPrimaryKey(reg.ParamedicID))
                    {
                        textBox12.Value = Medic.ParamedicName;
                    }


                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(reg.PatientID))
                    {

                        int Agey = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Year;
                        int Agem = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Month;
                        int Aged = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Day;
                        textBox23.Value = pat.PatientName;
                        textBox18.Value = pat.DateOfBirth.Value.ToString("dd-MM-yyyy");
                        textBox40.Value = Agey + " th " + Agem + " bl " + Aged + " Hr";
                        textBox26.Value = pat.MedicalNo + " / " + nh.RegistrationNo;

                        if (pat.Sex == "M")
                        {
                            textBox13.Value = "L";
                        }
                        else
                        {
                            textBox13.Value = "P";
                        }
                    }
                }
            }
        }
    }
}