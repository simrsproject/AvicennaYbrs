namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.PatientHealthRecord.RSMP
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
    using Temiang.Avicenna.BusinessObject.Util;

    /// <summary>
    /// Summary description for GeneralExamPoliGigi.
    /// </summary>
    public partial class GeneralExamPoliGigi : Report
    {

        public GeneralExamPoliGigi(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            Helper.InitializeLogo(this.pageHeaderSection1);

            var QuestionFormID = printJobParameters[1].ValueString;
            var RegistrationNo = printJobParameters[0].ValueString;
            var TransactionNo = printJobParameters[2].ValueString;

            // ASSIGN VALUE HERE
            var Allergy = string.Empty;
            var Drugs = string.Empty;
            var Anamnesis = string.Empty;
            var ExtraOral = string.Empty; 
            var IntraOral = string.Empty;
            var Planning = string.Empty;
            var Diagnose1 = string.Empty;
            var Diagnose2 = string.Empty;
            var Diagnose3 = string.Empty;
            var ICD1 = string.Empty;
            var ICD2 = string.Empty;
            var ICD3 = string.Empty;
            var Icd10 = string.Empty;
            var TindakanDanTerapy = string.Empty;
            var Rencana = string.Empty;
            // ----------------------
            textBox34.Value = Allergy;
            textBox40.Value = Drugs;
            textBox105.Value = Anamnesis;
            textBox108.Value = ExtraOral;
            textBox111.Value = IntraOral;
            textBox115.Value = Diagnose1;
            textBox117.Value = Diagnose2;
            textBox119.Value = Diagnose3;
            textBox91.Value = ICD1;
            textBox92.Value = ICD2;
            textBox93.Value = ICD3;
            textBox123.Value = TindakanDanTerapy;
            textBox127.Value = Rencana;

            var qf = new QuestionForm();
            if (qf.LoadByPrimaryKey(QuestionFormID))
            {
                textBox2.Value = qf.QuestionFormName;
                // ambil RM NO
                textBox39.Value = qf.RmNO ?? textBox39.Value;
            }

            var reg = new Temiang.Avicenna.BusinessObject.Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
            {
                textBox17.Value = reg.RegistrationDate.Value.ToString("dd-MM-yyyy");
                textBox16.Value = reg.RegistrationTime; ;
                textBox34.Value = reg.BedID;
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(reg.ServiceUnitID))
                {
                    textBox6.Value = su.ServiceUnitName;
                }

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(reg.PatientID))
                {
                    textBox24.Value = pat.PatientName;
                    textBox26.Value = pat.DateOfBirth.Value.ToString("dd-MM-yyyy");
                    textBox23.Value = pat.MedicalNo + " / " + RegistrationNo;
                }

                var par = new Paramedic();
                if (par.LoadByPrimaryKey(reg.ParamedicID)) {
                    textBox53.Value = par.ParamedicName;
                }
            }

            // question form
            // header
            var nphrColl = new PatientHealthRecordCollection();
            nphrColl.Query.Where(nphrColl.Query.TransactionNo == TransactionNo
                && nphrColl.Query.RegistrationNo == RegistrationNo
                && nphrColl.Query.QuestionFormID == QuestionFormID); 
            nphrColl.LoadAll();
            var nphr = nphrColl.First();
                    textBox35.Value = /*nphr.RecordDate.Value.ToString("dd/MM/yyyy") + " " +*/ nphr.RecordTime;
                    textBox96.Value = nphr.LastUpdateDateTime.Value.ToString("dd/MM/yyyy HH:mm");
                var usr = new AppUser();
                if(usr.LoadByPrimaryKey(nphr.LastUpdateByUserID)){
                    textBox95.Value = "Nama Perawat: " + usr.UserName;
                }
            // detail
                var phrlColl = new PatientHealthRecordLineCollection();
            var phrl = new PatientHealthRecordLineQuery("phrl");
            var q = new QuestionQuery("q");

            phrl.InnerJoin(q).On(phrl.QuestionID == q.QuestionID)
                .Where(
                    phrl.TransactionNo == TransactionNo,
                    phrl.RegistrationNo == RegistrationNo,
                    phrl.QuestionFormID == QuestionFormID
                ).Select(phrl, q.QuestionText.As("refToQuestion_QuestionText"));

            if (phrlColl.Load(phrl)) {
                GeneralExamMaster.Fill(phrlColl, textBox20, "RIW0001.01");
                GeneralExamMaster.Fill(phrlColl, textBox113, "RIW0001.05");
                GeneralExamMaster.Fill(phrlColl, textBox130, "RIW0001.06");
                // sistole diastole dijadi satu
                GeneralExamMaster.Fill(phrlColl, textBox38, new string[] { "VIT.SGN.01", "VIT.SGN.02" }, "/");
                GeneralExamMaster.Fill(phrlColl, textBox63, "VIT.SGN.05");
                GeneralExamMaster.Fill(phrlColl, textBox66, "VIT.SGN.04");
                GeneralExamMaster.Fill(phrlColl, textBox71, "VIT.SGN.03");
                GeneralExamMaster.Fill(phrlColl, textBox74, "GEN.SGN.02");
                GeneralExamMaster.Fill(phrlColl, textBox75, "GEN.SGN.01");
            }

            // RUJUK KE, KONSUL KE
            var regRujuk = new RegistrationCollection();
            regRujuk.Query.Where(regRujuk.Query.FromRegistrationNo.Equal(RegistrationNo),
                regRujuk.Query.IsVoid.Equal(false));
            if (regRujuk.LoadAll())
            {
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(regRujuk.First().ServiceUnitID); 
                // if rujuk
                if (regRujuk.First().IsConsul ?? false)
                {
                    // konsul poli lain
                    GeneralExamMaster.FillCheckBox(textBox50, textBox56, false);
                    GeneralExamMaster.FillCheckBox(textBox99, textBox100, false);
                    // sebut polinya
                    textBox102.Value = su.ServiceUnitName;
                }
                else { 
                    // rujuk rawat inap
                    GeneralExamMaster.FillCheckBox(textBox50, true);
                    GeneralExamMaster.FillCheckBox(textBox99, false);
                    // sebut unitnya
                    textBox101.Value = su.ServiceUnitName;
                }
            }
            else { 
                // pulang
                GeneralExamMaster.FillCheckBox(textBox50, false);
                GeneralExamMaster.FillCheckBox(textBox99, true);
            }
        }
    }
}