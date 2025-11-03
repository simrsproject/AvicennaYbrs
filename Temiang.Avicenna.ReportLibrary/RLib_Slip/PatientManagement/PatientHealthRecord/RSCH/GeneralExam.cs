namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.PatientHealthRecord.RSCH
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Temiang.Avicenna.BusinessObject;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Summary description for GeneralExam.
    /// </summary>
    public partial class GeneralExam : Report
    {
        private System.Collections.Generic.List<int> CTColWidth;

        public GeneralExam(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            CTColWidth = new System.Collections.Generic.List<int>();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeLogo(this.pageHeaderSection1);

            FillHeader(printJobParameters[0].ValueString);

            var qf = new QuestionForm();
            if (qf.LoadByPrimaryKey(printJobParameters[1].ValueString))
            {
                textBox2.Value = qf.QuestionFormName;

                // ambil RM NO
                textBox39.Value = qf.RmNO ?? string.Empty;
            }

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(printJobParameters[0].ValueString))
            {
                textBox17.Value = reg.RegistrationDate.Value.ToString("dd/MM/yyyy") + " " + reg.RegistrationTime;
            }
            var nphrColl = new PatientHealthRecordCollection();
            nphrColl.Query.Where(nphrColl.Query.TransactionNo == printJobParameters[2].ValueString
                && nphrColl.Query.RegistrationNo == printJobParameters[0].ValueString
                && nphrColl.Query.QuestionFormID == printJobParameters[1].ValueString);
            nphrColl.LoadAll();
            var nphr = nphrColl.First();

            textBox16.Value = nphr.RecordDate.Value.ToString("dd/MM/yyyy") + " " + nphr.RecordTime;
            textBox33.Value = nphr.LastUpdateDateTime.Value.ToString("dd/MM/yyyy HH:mm");
            var usr = new AppUser();
            if (usr.LoadByPrimaryKey(nphr.LastUpdateByUserID))
            {
                textBox30.Value = usr.UserName;
            }

            var hideSignature = false;

            DataSource = GeneralExamMaster.GetDataSource(printJobParameters[2].ValueString, printJobParameters[0].ValueString,
               printJobParameters[1].ValueString, nphr.RecordDate.Value.ToString("dd/MM/yyyy") + " " + nphr.RecordTime,
               ref hideSignature, nphr.CreateByUserID);

            reportFooterSection1.Visible = !hideSignature;

            this.detail.ItemDataBinding += new EventHandler(detail_ItemDataBinding);

            crosstab1.ItemDataBound += new EventHandler(crosstab_ItemDataBound);
            table1.ItemDataBound += new EventHandler(crosstab_ItemDataBound);
        }

        private void crosstab_ItemDataBound(object sender, EventArgs e)
        {
            GeneralExamMaster.crosstab_ItemDataBound(sender, e, ref CTColWidth);
        }

        private void detail_ItemDataBound(object sender, EventArgs e)
        {
            //GeneralExamMaster.detail_ItemDataBound(sender, e);
            Telerik.Reporting.Processing.DetailSection section = (sender as Telerik.Reporting.Processing.DetailSection);
            Telerik.Reporting.Processing.Table crosstbl = (Telerik.Reporting.Processing.Table)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(section, "crosstab1");
            Telerik.Reporting.Processing.Table tbl = (Telerik.Reporting.Processing.Table)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(section, "table1");
        }

        private void detail_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.DetailSection section = (sender as Telerik.Reporting.Processing.DetailSection);
            Telerik.Reporting.Processing.Table crosstbl = (Telerik.Reporting.Processing.Table)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(section, "crosstab1");
            Telerik.Reporting.Processing.Table tbl = (Telerik.Reporting.Processing.Table)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(section, "table1");
            GeneralExamMaster.SetTableSource(section, crosstbl, tbl, ref CTColWidth);
        }

        public static Telerik.Reporting.Drawing.SizeU CalculateSize(Telerik.Reporting.Processing.TextBox textBox, object o)
        {
            return GeneralExamMaster.CalculateSize(textBox, o);
        }
        public static Unit CalculateLeft(object o)
        {
            return GeneralExamMaster.CalculateLeft(o);
        }
        public static bool SetFontBold(object o)
        {
            return (o.ToString() == "LBL");
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
                    textBox26.Value = pat.MedicalNo + " / " + RegistrationNo;
                    //textBox19.Value = pat.Sex;
                }
            }
        }
    }
}