using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;

namespace Temiang.Avicenna.ReportLibrary.RSMP.RLib_Slip.Assessment.Continued
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
    public partial class Kandungan : Report
    {

        public Kandungan(string programID, PrintJobParameterCollection printJobParameters)
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

            PopulatePengkajianDokter(rim, reg);


            txtChiefComplaint.Value = reg.Complaint;

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

            // RM.06.b	ASESMEN LANJUTAN RAWAT JALAN PENYAKIT KANDUNGAN
            var nphrColl = new PatientHealthRecordCollection();
            var qrLastPhr = new PatientHealthRecordQuery("a");
            qrLastPhr.Where(qrLastPhr.RegistrationNo == reg.RegistrationNo, qrLastPhr.QuestionFormID == "RM.06.b");
            qrLastPhr.es.Top = 1;
            qrLastPhr.OrderBy(qrLastPhr.TransactionNo.Descending);

            if (nphrColl.Load(qrLastPhr) && nphrColl.Count > 0)
            {
                PopulatePengkajianKeperawatan(nphrColl, pat);
            }


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
                txtNamaPerawat.Value = string.Format("Nama Bidan: {0}", usr.UserName);
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

                Utils.PopulateKeadaanUmum(phrlColl, 
                    txtTekananDarah,
                    txtTemperature,
                    txtRespiratoryRate,
                    txtHeartRate,
                    txtWeight,
                    txtHeight);

                PopulateDiagnosisKeperawatan(phrlColl);

                Utils.Fill(phrlColl, txtRencanaPerawat, "IMP.K");
            }
        }

        private void PopulatePengkajianDokter(RegistrationInfoMedic rim, Registration reg)
        {
            txtAnamnesis.Value = rim.Info1;
            txtPhysicalExam.Value = rim.Info2;
            txtPlanning.Value = rim.Info4;
            txtDiagnose.Value = rim.Info3;

            // Dirujuk
            //E09 RUJUK RS LAIN - ALAT (-)
            //E11 RUJUK - ATAS PERMINTAAN PASIEN
            //E12 RUJUK - TEMPAT PENUH
            chkDirujukKe.Value = "E09_E11_E12".Contains(reg.SRDischargeMethod);

            // KONSUL KE SPESIALIS 
            chkKonsulKe.Value = reg.SRDischargeMethod == "O01" || reg.SRDischargeMethod == "E01"; //O01 -> Out Patient, E01 -> Emergency

            // Pulang
            // Emergency
            //E03	DIIZINKAN PULANG
            //E04	PULANG PAKSA
            // Outpatient
            //O04 DIIZINKAN PULANG
            //O05 PULANG PAKSA
            chkPulang.Value = "E03_E04_O04_O05".Contains(reg.SRDischargeMethod);


            // Dirawat
            //O02	RAWAT INAP VIA REGISTRASI
            //O03	RAWAT INAP VIA IGD
            //E10 KEMBALI KE RAWAT INAP
            //E02 DIRAWAT (MRS)
            chkRawatInap.Value = "O02_O03_E10_E02".Contains(reg.SRDischargeMethod);
        }



        private void PopulateDiagnosisKeperawatan(PatientHealthRecordLineCollection phrlColl)
        {
            Utils.FillCheckBox(phrlColl, chkKeperawatan_Nyeri, "DIAG.K.01");
            Utils.FillCheckBox(phrlColl, chkKeperawatan_GangguanCerebal, "DIAG.K.02");
            Utils.FillCheckBox(phrlColl, chkKeperawatan_Cemas, "DIAG.K.03");
            Utils.FillCheckBox(phrlColl, chkKeperawatan_Sensori, "DIAG.K.04");
            Utils.FillCheckBox(phrlColl, chkKeperawatan_Hipertemi, "DIAG.K.05");
            Utils.FillCheckBox(phrlColl, chkKeperawatan_Kerusakan, "DIAG.K.06");
            Utils.FillCheckBox(phrlColl, chkKeperawatan_GangguanJaringan, "DIAG.K.07");
            Utils.FillCheckBox(phrlColl, chkKeperawatan_BodyImage, "DIAG.K.08");
            Utils.FillCheckBox(phrlColl, chkKeperawatan_GangguanMobilitas, "DIAG.K.09");
            Utils.FillCheckBox(phrlColl, chkKeperawatan_Kurang, "DIAG.K.10");
            Utils.FillCheckBox(phrlColl, chkKeperawatan_Perubahan, "DIAG.K.11");
        }
    }
}