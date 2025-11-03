namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.PatientHealthRecord.RSETA
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject.Util;
    using Temiang.Avicenna.BusinessObject;
    using System.Data;

    /// <summary>
    /// Summary description for GeneralExamPivot.
    /// </summary>
    public partial class GeneralExamPivot : Report
    {
        public GeneralExamPivot(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeLogo(this.pageHeaderSection1);

            FillHeader(printJobParameters[0].ValueString);

            var qf = new QuestionForm();
            if (qf.LoadByPrimaryKey(printJobParameters[1].ValueString)) {
                textBox1.Value = qf.QuestionFormName;
            }
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            crosstab1.DataSource = dtb;

            DataTable dtK = new DataTable();
            dtK.Columns.Add("Range", typeof(string));
            dtK.Columns.Add("Ket", typeof(string));
            if (dtb.Rows.Count > 0) {
                var strs = dtb.Rows[0]["TableInfo"].ToString().Split('|');
                foreach (var str in strs) {
                    var strs2 = str.Split(':');
                    var row = dtK.NewRow();
                    row["Range"] = strs2.Length > 0 ? strs2[0] : string.Empty;
                    row["Ket"] = strs2.Length > 1 ? strs2[1] : string.Empty;
                    dtK.Rows.Add(row);
                }
                table1.DataSource = dtK;
                textBox32.Value = dtb.Rows[0]["TextInfo"].ToString();
            }
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
                    var deceasedDate = pat.DeceasedDateTime ?? DateTime.Now;
                    int Agey = pat.IsAlive == true ? new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Year : new DateTime((deceasedDate - pat.DateOfBirth.Value).Ticks).Year - 1;
                    int Agem = pat.IsAlive == true ? new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Month : new DateTime((deceasedDate - pat.DateOfBirth.Value).Ticks).Month - 1;
                    int Aged = pat.IsAlive == true ? new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Day : new DateTime((deceasedDate - pat.DateOfBirth.Value).Ticks).Day - 1;
                    textBox23.Value = pat.PatientName;
                    textBox24.Value = pat.DateOfBirth.Value.ToString("dd-MM-yyyy");
                    textBox40.Value = Agey + " th " + Agem + " bl " + Aged + " Hr";
                    textBox26.Value = pat.MedicalNo + " / " + RegistrationNo;

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