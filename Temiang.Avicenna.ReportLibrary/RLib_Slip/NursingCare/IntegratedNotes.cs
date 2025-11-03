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
    public partial class IntegratedNotes : Telerik.Reporting.Report
    {
        public IntegratedNotes(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();

            Helper.InitializeLogo(this.pageHeaderSection1);

            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);

            FillHeader(printJobParameters[0].ValueString);

            //DataSource = new ReportDataSource().GetDataTable(programID, printJobParameters);
            var dQuery = new RegistrationInfoMedicQuery("a");
            var usr = new AppUserQuery("b");

            dQuery.LeftJoin(usr).On(dQuery.LastUpdateByUserID == usr.UserID)
                .Where(dQuery.RegistrationNo == printJobParameters[0].ValueString)
                .Select(dQuery, "<'' S>", "<'' O>", "<'' A>", "<'' P>" , usr.UserName)
                .OrderBy(dQuery.DateTimeInfo.Ascending);

            var dt = dQuery.LoadDataTable();

            foreach(System.Data.DataRow r in dt.Rows){
                r["S"] = r["SRMedicalNotesInputType"].ToString()[0].ToString();
                r["O"] = r["SRMedicalNotesInputType"].ToString()[1].ToString();
                r["A"] = r["SRMedicalNotesInputType"].ToString()[2].ToString();
                r["P"] = r["SRMedicalNotesInputType"].ToString()[3].ToString();
            }
            dt.AcceptChanges();
            this.DataSource = dt;
        }

        private void FillHeader(string RegistrationNo) {
            var reg = new Temiang.Avicenna.BusinessObject.Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
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
                    textBox8.Value = Medic.ParamedicName;
                }


                var pat = new Patient();
                if (pat.LoadByPrimaryKey(reg.PatientID))
                {

                    int Agey = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Year;
                    int Agem = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Month;
                    int Aged = new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Day;
                    textBox23.Value = pat.PatientName;
                    textBox13.Value = pat.DateOfBirth.Value.ToString("dd-MM-yyyy");
                    textBox40.Value = Agey + " th " + Agem + " bl " + Aged + " Hr";
                    textBox26.Value = pat.MedicalNo + " / " + RegistrationNo;

                    if (pat.Sex == "M")
                    {
                        textBox9.Value = "L";
                    }
                    else
                    {
                        textBox9.Value = "P";
                    }
                }
            }
        }
    }
}