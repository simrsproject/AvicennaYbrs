namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.PatientHealthRecord.RSYS
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
    /// Summary description for GeneralExam1Row.
    /// </summary>
    public partial class GeneralExam1Row1 : Report
    {
        private System.Collections.Generic.List<int> CTColWidth;

        public GeneralExam1Row1(string programID, PrintJobParameterCollection printJobParameters)
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

            var regno = printJobParameters[0].ValueString;
            var formid = printJobParameters[1].ValueString;
            var transno = printJobParameters[2].ValueString;

            FillHeader(regno);

            var qf = new QuestionForm();
            if (qf.LoadByPrimaryKey(formid)) {
                textBox2.Value = qf.QuestionFormName;

                // ambil RM NO
                textBox39.Value = qf.RmNO ?? string.Empty;
            }

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(printJobParameters[0].ValueString)) {
                //textBox17.Value = reg.RegistrationDate.Value.ToString("dd/MM/yyyy") + " " + reg.RegistrationTime;
            }
            var nphrColl = new PatientHealthRecordCollection();
            nphrColl.Query.Where(nphrColl.Query.TransactionNo == printJobParameters[2].ValueString
                && nphrColl.Query.RegistrationNo == printJobParameters[0].ValueString
                && nphrColl.Query.QuestionFormID == printJobParameters[1].ValueString); 
            nphrColl.LoadAll();
            var nphr = nphrColl.First();
                    //textBox16.Value = nphr.RecordDate.Value.ToString("dd/MM/yyyy") + " " + nphr.RecordTime;
                    //textBox33.Value = nphr.LastUpdateDateTime.Value.ToString("dd/MM/yyyy HH:mm");
                //var usr = new AppUser();
                //if(usr.LoadByPrimaryKey(nphr.LastUpdateByUserID)){
                //    if (usr.SRUserType == "DTR")
                //    {
                //        textBox28.Value = "Dokter ruangan";
                //    }
                //    {
                //        textBox28.Value = "Perawat ruangan";
                //    }
                //    textBox30.Value = usr.UserName;
                    
                //}

            var hideSignature = false;

            DataSource = NursingDiagnosaTransDT.ReportDataSourceGeneralV2(transno, regno, formid);

            //reportFooterSection1.Visible = !hideSignature;

            this.detail.ItemDataBinding += new EventHandler(detail_ItemDataBinding);
            crosstab1.ItemDataBound += new EventHandler(crosstab_ItemDataBound);
            table1.ItemDataBound += new EventHandler(crosstab_ItemDataBound);
        }

        private void crosstab_ItemDataBound(object sender, EventArgs e)
        {
            GeneralExamMaster.crosstab_ItemDataBound(sender, e, ref CTColWidth);
        }

        //private void detail_ItemDataBound(object sender, EventArgs e)
        //{
        //    //GeneralExamMaster.detail_ItemDataBound(sender, e);
        //    Telerik.Reporting.Processing.DetailSection section = (sender as Telerik.Reporting.Processing.DetailSection);
        //    Telerik.Reporting.Processing.Table crosstbl = (Telerik.Reporting.Processing.Table)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(section, "crosstab1");
        //    Telerik.Reporting.Processing.Table tbl = (Telerik.Reporting.Processing.Table)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(section, "table1");
        //}

        private void detail_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.DetailSection section = (sender as Telerik.Reporting.Processing.DetailSection);
            Telerik.Reporting.Processing.Table crosstbl = (Telerik.Reporting.Processing.Table)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(section, "crosstab1");
            Telerik.Reporting.Processing.Table tbl = (Telerik.Reporting.Processing.Table)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(section, "table1");
            Telerik.Reporting.Processing.PictureBox pb = (Telerik.Reporting.Processing.PictureBox)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(section, "pictureBox1");

            GeneralExamMaster.SetObjectSource(section, crosstbl, tbl, ref CTColWidth, pb);
        }

        public static Telerik.Reporting.Drawing.SizeU CalculateSize(Telerik.Reporting.Processing.TextBox textBox, object o)
        {
            return GeneralExamMaster.CalculateSize(textBox, o);
        }
        public static Telerik.Reporting.Drawing.SizeU CalculateSizeChk(Telerik.Reporting.Processing.CheckBox chkBox, object o)
        {
            return GeneralExamMaster.CalculateSizeChk(chkBox, o);
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
                textBox34.Value = reg.BedID;
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                {
                    textBox27.Value = "Ruangan " + su.ServiceUnitName;
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
                    DateTime deceasedDate = pat.DeceasedDateTime ?? DateTime.Now;
                    int Agey = pat.IsAlive == true ? new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Year - 1 : new DateTime((deceasedDate - pat.DateOfBirth.Value).Ticks).Year - 1;
                    int Agem = pat.IsAlive == true ? new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Month - 1 : new DateTime((deceasedDate - pat.DateOfBirth.Value).Ticks).Month - 1;
                    int Aged = pat.IsAlive == true ? new DateTime((DateTime.Now - pat.DateOfBirth.Value).Ticks).Day - 1 : new DateTime((deceasedDate - pat.DateOfBirth.Value).Ticks).Day - 1;
                    textBox23.Value = pat.PatientName;
                    textBox24.Value = pat.DateOfBirth.Value.ToString("dd-MM-yyyy");
                    textBox40.Value = Agey + " th " + Agem +" bl " + Aged +" Hr" ;
                    textBox26.Value = pat.MedicalNo + " / " + RegistrationNo;
                    textBox50.Value = pat.Ssn;

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