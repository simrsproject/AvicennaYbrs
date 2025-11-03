namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.PatientHealthRecord.RSCH
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
            if (qf.LoadByPrimaryKey(printJobParameters[1].ValueString))
            {
                textBox1.Value = qf.QuestionFormName;
            }
            var rptData = new ReportDataSource();
            DataTable dtb = rptData.GetDataTable(programID, printJobParameters);
            crosstab1.DataSource = dtb;

            DataTable dtK = new DataTable();
            dtK.Columns.Add("Range", typeof(string));
            dtK.Columns.Add("Ket", typeof(string));
            if (dtb.Rows.Count > 0)
            {
                var strs = dtb.Rows[0]["TableInfo"].ToString().Split('|');
                foreach (var str in strs)
                {
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
                //textBox24.Value = reg.AgeInYear.ToString() + "yr " + reg.AgeInMonth.ToString() + "mth";
                textBox34.Value = reg.BedID;
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                {
                    textBox27.Value = su.ServiceUnitName;
                }
                //var room = new ServiceRoom();
                //if (room.LoadByPrimaryKey(reg.RoomID))
                //{
                //    textBox34.Value = room.RoomName;
                //}
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(reg.PatientID))
                {
                    textBox23.Value = pat.PatientName;
                    textBox24.Value = pat.DateOfBirth.Value.ToString("dd-MM-yyyy");
                    textBox26.Value = pat.MedicalNo;
                    //textBox19.Value = pat.Sex;
                }
            }
        }
    }
}