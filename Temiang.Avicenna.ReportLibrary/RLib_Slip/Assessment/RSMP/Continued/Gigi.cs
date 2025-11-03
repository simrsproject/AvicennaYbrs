using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.Continued
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
    /// Summary description for GeneralExamOPR.
    /// </summary>
    public partial class Gigi : Report
    {

        public Gigi(string programID, PrintJobParameterCollection printJobParameters)
        {

            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            this.Width=new Unit(7.6,UnitType.Inch);

            Helper.InitializeLogo(this.reportHeaderSection1, 0.1, 0.1);
            var patientID = printJobParameters.FindByParameterName("PatientID").ValueString;
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;


            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(rimid);


            var pass = new PatientAssessment();
            pass.LoadByPrimaryKey(rimid);
            txtMedikamentosa.Value = pass.Medikamentosa;

            textBox34.Value = Utils.PatientAllergy(patientID);


            var reg = new Temiang.Avicenna.BusinessObject.Registration();
            reg.LoadByPrimaryKey(rim.RegistrationNo);
            textBox17.Value = reg.RegistrationDate.Value.ToString("dd-MM-yyyy");
            textBox16.Value = reg.RegistrationTime; ;

            PengkajianDokter(rim, pass.PhysicalExam, reg);

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
                textBox23.Value = pat.MedicalNo + " / " + reg.RegistrationNo;
            }
            var par = new Paramedic();
            if (par.LoadByPrimaryKey(reg.ParamedicID))
            {
                textBox53.Value = par.ParamedicName;
            }

            // Pengkajian Keperawatan
            txtChiefComplaint.Value = reg.Complaint;

            // RM.05.b	ASESMEN LANJUTAN RAWAT JALAN POLI GIGI
            var nphrColl = new PatientHealthRecordCollection();
            var qrLastPhr = new PatientHealthRecordQuery("a");
            qrLastPhr.Where(qrLastPhr.RegistrationNo == reg.RegistrationNo, qrLastPhr.QuestionFormID == "RM.05.b");
            qrLastPhr.es.Top = 1;
            qrLastPhr.OrderBy(qrLastPhr.TransactionNo.Descending);

            if (nphrColl.Load(qrLastPhr) && nphrColl.Count > 0)
            {

                PopulatePengkajianKeperawatan(nphrColl, pat);
            }


        }
        private void PengkajianDokter(RegistrationInfoMedic rim, string jsonPhysicalExam, Registration reg)
        {
            // Convert to class w json
            var pexam = JsonConvert.DeserializeObject<DentisPe>(jsonPhysicalExam);

            Utils.PopulatePengkajianDokter(rim, reg,
                txtAnamnesis,
                null,
                txtPlanning,
                txtDiagnose,
                chkDirujukKe,
                chkKonsulKe,
                chkPulang,
                chkRawatInap
                );

            // IntraOral
            var intraOral = new StringBuilder();
            if (!string.IsNullOrEmpty(pexam.IntraOral.Bibir))
            {
                intraOral.AppendFormat("Bibir: {0}", pexam.IntraOral.Bibir);
                intraOral.AppendLine("");
            }
            if (!string.IsNullOrEmpty(pexam.IntraOral.Palatum))
            {
                intraOral.AppendFormat("Palatum: {0}", pexam.IntraOral.Palatum);
                intraOral.AppendLine("");
            }
            if (!string.IsNullOrEmpty(pexam.IntraOral.Lidah))
            {
                intraOral.AppendFormat("Lidah: {0}", pexam.IntraOral.Lidah);
                intraOral.AppendLine("");

            }
            if (!string.IsNullOrEmpty(pexam.IntraOral.DasarMulut))
            {
                intraOral.AppendFormat("Dasar Mulut: {0}", pexam.IntraOral.DasarMulut);
                intraOral.AppendLine("");

            }
            if (!string.IsNullOrEmpty(pexam.IntraOral.Vestibulum))
            {
                intraOral.AppendFormat("Vestibulum: {0}", pexam.IntraOral.Vestibulum);
                intraOral.AppendLine("");

            }
            if (!string.IsNullOrEmpty(pexam.IntraOral.Ginggiva))
            {
                intraOral.AppendFormat("Ginggiva: {0}", pexam.IntraOral.Ginggiva);
                intraOral.AppendLine("");

            }
            if (!string.IsNullOrEmpty(pexam.IntraOral.MukosaBukal))
            {
                intraOral.AppendFormat("Mukosa Bukal: {0}", pexam.IntraOral.MukosaBukal);
                intraOral.AppendLine("");

            }
            if (!string.IsNullOrEmpty(pexam.IntraOral.Tonsil))
            {
                intraOral.AppendFormat("Tonsil: {0}", pexam.IntraOral.Tonsil);
                intraOral.AppendLine("");

            }
            if (!string.IsNullOrEmpty(pexam.IntraOral.Other))
            {
                intraOral.AppendFormat("{0}", pexam.IntraOral.Other);
            }
            txtIntraOral.Value = intraOral.ToString();

            txtAction.Value = pexam.ActionAndTherapy;
        }

        private void PopulatePengkajianKeperawatan(PatientHealthRecordCollection nphrColl, Patient patient)
        {
            var nphr = nphrColl.First();
            txtJamMenitPengkajianKeperawatan.Value = nphr.RecordTime;
            txtTimePerawatParaf.Value = string.Format("Tanggal/jam: {0}",
                nphr.LastUpdateDateTime.Value.ToString("dd/MM/yyyy HH:mm"));
            var usr = new AppUser();
            if (usr.LoadByPrimaryKey(nphr.LastUpdateByUserID))
            {
                txtNamaPerawat.Value = string.Format("Nama Perawat: {0}", usr.UserName);
            }
            // detail
            var phrlColl = new PatientHealthRecordLineCollection();
            var phrl = new PatientHealthRecordLineQuery("phrl");

            phrl.Where(
                phrl.TransactionNo == nphr.TransactionNo,
                phrl.RegistrationNo == nphr.RegistrationNo,
                phrl.QuestionFormID == nphr.QuestionFormID
                );

            if (phrlColl.Load(phrl))
            {
                if (string.IsNullOrEmpty(txtChiefComplaint.Value))
                    Utils.Fill(phrlColl, txtChiefComplaint, "RIW0001.01");

                Utils.Fill(phrlColl, txtRiwayatKesehatanGigi, "RIW0001.05");
                Utils.Fill(phrlColl, txtKebiasaan, "RIW0001.06");


                Utils.PopulateKeadaanUmum(phrlColl, 
                    txtTekananDarah,
                    txtTemperature,
                    txtRespiratoryRate,
                    txtHeartRate,
                    txtWeight,
                    txtHeight);


            }
        }




    }
}