using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.RSMP
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

            this.Width = new Unit(7.6, UnitType.Inch);

            Helper.InitializeLogo(this.reportHeaderSection1, 0.1, 0.1);
            var patientID = printJobParameters.FindByParameterName("PatientID").ValueString;
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;


            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(rimid);


            var pass = new PatientAssessment();
            pass.LoadByPrimaryKey(rimid);
            txtMedikamentosa.Value = pass.Medikamentosa;

            txtDrugAndFoodAllergy.Value = Utils.PatientAllergy(patientID);


            var reg = new Registration();
            reg.LoadByPrimaryKey(rim.RegistrationNo);
            textBox17.Value = reg.RegistrationDate.Value.ToString("dd-MM-yyyy");
            textBox16.Value = reg.RegistrationTime; ;

            Utils.PopulatePengkajianDokter(rim, reg,
                txtAnamnesis,
                txtPhysicalExam,
                txtPlanning,
                txtDiagnose,
                chkDirujukKe,
                chkKonsulKe,
                chkPulang,
                chkRawatInap
                );

            txtChiefComplaint.Value = reg.Complaint;

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


            PopulatePatientChildBirthHistory(reg.PatientID);


            // RM.07.a	ASESMEN AWAL RAWAT JALAN POLI KEBIDANAN
            var nphrColl = new PatientHealthRecordCollection();
            var qrLastPhr = new PatientHealthRecordQuery("a");
            qrLastPhr.Where(qrLastPhr.RegistrationNo == reg.RegistrationNo, qrLastPhr.QuestionFormID == "RM.07.a");
            qrLastPhr.es.Top = 1;
            qrLastPhr.OrderBy(qrLastPhr.TransactionNo.Descending);

            if (nphrColl.Load(qrLastPhr) && nphrColl.Count > 0)
            {
                PopulatePengkajianKeperawatan(nphrColl, pat);
            }


        }

        private void PopulatePatientChildBirthHistory(string patientID)
        {
            var childBirthHistory = new PatientChildBirthHistoryCollection();
            childBirthHistory.Query.Where(childBirthHistory.Query.PatientID == patientID);
            childBirthHistory.LoadAll();
            tblPatientChildBirthHistory.DataSource = childBirthHistory;
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
                txtNamaPerawat.Value = string.Format("Nama Bidan : {0}", usr.UserName);
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

                PopulatePsikologis(phrlColl);
                PopulateStatusSosial(patient);
                PopulateDiagnosisKeperawatan(phrlColl);
                PopulateSkriningGiziAwal(phrlColl);
                PopulateStatusFungsional(phrlColl);
                PopulateAsesmenResikoJatuh(phrlColl);
                PopulateAsesmenNyeri(phrlColl);

                Utils.Fill(phrlColl, txtRencanaPerawat, "IMP.K");
            }
        }

        private void PopulatePsikologis(PatientHealthRecordLineCollection phrlColl)
        {
            Utils.FillCheckBox(phrlColl, chkAp_Tenang, "DIAG.K.12");
            Utils.FillCheckBox(phrlColl, chkAp_Cemas, "DIAG.K.20");
            Utils.FillCheckBox(phrlColl, chkAp_Takut, "DIAG.K.13");
            Utils.FillCheckBox(phrlColl, chkAp_Marah, "DIAG.K.14");
            Utils.FillCheckBox(phrlColl, chkAp_Sedih, "DIAG.K.15");
            Utils.FillCheckBox(phrlColl, chkAp_Kecenderungan, "DIAG.K.16");

            // Masalah perilaku ..CBT to 2 Checkbox & text
            var questionID = "DIAG.K.19";
            var ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                // adt2
                //1	Tidak Ada
                //2	Ada, Sebutkan:

                chkAp_TidakAda.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
                if (ent.QuestionAnswerSelectionLineID.Equals("2"))
                {
                    var values = ent.QuestionAnswerText.Split('|');
                    chkAp_Ada.Value = true;
                    txtAp_Ada.Value = values[1];
                }
            }

        }
        private void PopulateStatusSosial(Patient patient)
        {
            // Status Pernikahan
            var stdri = new AppStandardReferenceItem();
            if (!string.IsNullOrEmpty(patient.SRMaritalStatus))
            {
                if (stdri.LoadByPrimaryKey("MaritalStatus", patient.SRMaritalStatus))
                    txtMaritalStatus.Value = stdri.ItemName;
            }

            // Hubungan Pasien dg keluarga
            stdri = new AppStandardReferenceItem();
            if (!string.IsNullOrEmpty(patient.SRRelationshipQuality))
            {
                if (stdri.LoadByPrimaryKey("RelationshipQuality", patient.SRRelationshipQuality))
                    txtMaritalStatus.Value = stdri.ItemName;
            }

            // Tempat Tinggal
            stdri = new AppStandardReferenceItem();
            if (!string.IsNullOrEmpty(patient.SRResidentialHome))
            {
                if (stdri.LoadByPrimaryKey("ResidentialHome", patient.SRResidentialHome))
                    txtResidenceType.Value = stdri.ItemName;
            }

            // Pekerjaan
            stdri = new AppStandardReferenceItem();
            if (!string.IsNullOrEmpty(patient.SROccupation))
            {
                if (stdri.LoadByPrimaryKey("Occupation", patient.SROccupation))
                    txtOccupation.Value = stdri.ItemName;
            }
        }

        private void PopulateSkriningGiziAwal(PatientHealthRecordLineCollection phrlColl)
        {
            var questionID = "nut2.2"; //1. Apakah pasien mengalami penurunan berat badan yang tidak diinginkan dalam 6 bulan terakhir ?
            var ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkBB_Tidak.Value = ent.QuestionAnswerSelectionLineID.Equals("00");
                chkBB_TidakYakin.Value = ent.QuestionAnswerSelectionLineID.Equals("20");
                chkBB_YaAda.Value = "31_32_33_34".Contains(ent.QuestionAnswerSelectionLineID);
                chkBB_1_5.Value = ent.QuestionAnswerSelectionLineID.Equals("31");
                chkBB_6_10.Value = ent.QuestionAnswerSelectionLineID.Equals("32");
                chkBB_11_15.Value = ent.QuestionAnswerSelectionLineID.Equals("33");
                chkBB_15.Value = ent.QuestionAnswerSelectionLineID.Equals("34");
                chkBB_TidakTahu.Value = ent.QuestionAnswerSelectionLineID.Equals("40");
            }

            questionID = "nut2.4"; //2. Apakah asupan makan pasien berkurang karena penurunan nafsu makan / kesulitan menerima makanan?
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkAsupan_Tidak.Value = ent.QuestionAnswerSelectionLineID.Equals("0");
                chkAsupan_Ya.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
            }
        }

        private void PopulateStatusFungsional(PatientHealthRecordLineCollection phrlColl)
        {
            //Penggunaan alat bantu
            var questionID = "RJ.F.PAB";
            var ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkAlatBantu_Tidak.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
                chkAlatBantu_Tongkat.Value = ent.QuestionAnswerSelectionLineID.Equals("2");
                chkAlatBantu_KursiRoda.Value = ent.QuestionAnswerSelectionLineID.Equals("3");
            }

            // Cacat tubuh
            questionID = "sta.sos1.4";
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkCacat_Tidak.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
                if (ent.QuestionAnswerSelectionLineID.Equals("2"))
                {
                    var values = ent.QuestionAnswerText.Split('|');
                    chkCacat_Ada.Value = true;
                    txtCacat_Ada.Value = values[1];
                }
            }
        }

        private void PopulateAsesmenResikoJatuh(PatientHealthRecordLineCollection phrlColl)
        {
            //Utils.FillCheckBox(phrlColl, chkARJ_Dewasa, "DIAG.K.12");
            // Cara berjalan
            var questionID = "ASS.RSJ.D1";
            var ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkARJ_SempoyonganYa.Value = ent.QuestionAnswerSelectionLineID.Equals("UM.YN1");
                chkARJ_SempoyonganTidak.Value = ent.QuestionAnswerSelectionLineID.Equals("UM.YN2");
            }

            // Pegangan
            questionID = "ASS.RSJ.D2";
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkARJ_PeganganYa.Value = ent.QuestionAnswerSelectionLineID.Equals("UM.YN1");
                chkARJ_PeganganTidak.Value = ent.QuestionAnswerSelectionLineID.Equals("UM.YN2");
            }

            // Hasil ASS.RSJ.D3
            questionID = "ASS.RSJ.D3";
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkARJ_Hasil_TB.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
                chkARJ_Hasil_RR.Value = ent.QuestionAnswerSelectionLineID.Equals("2");
                chkARJ_Hasil_RT.Value = ent.QuestionAnswerSelectionLineID.Equals("3");
            }

            // Skor Anak Humpty
            questionID = "ASS.RSJ.A1";
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkARJ_AnakRT.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
                chkARJ_AnakRR.Value = ent.QuestionAnswerSelectionLineID.Equals("2");
            }
        }

        private void PopulateAsesmenNyeri(PatientHealthRecordLineCollection phrlColl)
        {
            // Nyeri
            var questionID = "ASS.NYE1";
            var ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkNyeri_Tidak.Value = ent.QuestionAnswerSelectionLineID.Equals("YTLOKASI1");
                if (ent.QuestionAnswerSelectionLineID.Equals("YTLOKASI2"))
                {
                    var values = ent.QuestionAnswerText.Split('|');
                    chkNyeri_Ya.Value = true;
                    txtNyeri_Lokasi.Value = values[1];
                }
            }

            // Jenis
            questionID = "ASS.NYE2";
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkJenis_Akut.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
                chkJenis_Kronis.Value = ent.QuestionAnswerSelectionLineID.Equals("2");
            }

            Utils.Fill(phrlColl, txtSkor_Numerik, "ASS.NYE4");
            Utils.Fill(phrlColl, txtSkor_WongBeker, "ASS.NYE5");
            Utils.Fill(phrlColl, txtSkor_Flacc, "ASS.NYE6");

            // Provocation : Faktor yang memperburuk rasa nyeri
            questionID = "ASS.NYE7";
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkANy_Faktor_Cahaya.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
                chkANy_Faktor_Gelap.Value = ent.QuestionAnswerSelectionLineID.Equals("2");
                chkANy_Faktor_Gerakan.Value = ent.QuestionAnswerSelectionLineID.Equals("3");
                chkANy_Faktor_Berbaring.Value = ent.QuestionAnswerSelectionLineID.Equals("4");
                if (ent.QuestionAnswerSelectionLineID.Equals("5"))
                {
                    var values = ent.QuestionAnswerText.Split('|');
                    chkANy_Faktor_Lainnya.Value = true;
                    txtANy_Faktor_Lainnya.Value = values[1];
                }
            }

            // Nyeri Seperti
            questionID = "ASS.NYE8";
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkANy_Spt_Ditusuk.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
                chkANy_Spt_Dipukul.Value = ent.QuestionAnswerSelectionLineID.Equals("2");
                chkANy_Spt_Berdenyut.Value = ent.QuestionAnswerSelectionLineID.Equals("3");
                chkANy_Spt_Ditikam.Value = ent.QuestionAnswerSelectionLineID.Equals("4");
                chkANy_Spt_Kram.Value = ent.QuestionAnswerSelectionLineID.Equals("5");
                chkANy_Spt_Ditarik.Value = ent.QuestionAnswerSelectionLineID.Equals("6");
                chkANy_Spt_Dibakar.Value = ent.QuestionAnswerSelectionLineID.Equals("7");
                chkANy_Spt_Tajam.Value = ent.QuestionAnswerSelectionLineID.Equals("8");
                if (ent.QuestionAnswerSelectionLineID.Equals("9"))
                {
                    var values = ent.QuestionAnswerText.Split('|');
                    chkANy_Spt_Lainnya.Value = true;
                    chkANy_Spt_Lainnya.Text = values[1];
                }
            }

            // Nyeri Menjalar
            questionID = "ASS.NYE9";
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkANy_Menjalar_Tidak.Value = ent.QuestionAnswerSelectionLineID.Equals("UM.YN2");
                chkANy_Menjalar_Ya.Value = ent.QuestionAnswerSelectionLineID.Equals("UM.YN1");
            }

            // Severity
            questionID = "ASS.NYE10";
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkANy_Tingkat_Tidak.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
                chkANy_Tingkat_Ringan.Value = ent.QuestionAnswerSelectionLineID.Equals("2");
                chkANy_Tingkat_Sedang.Value = ent.QuestionAnswerSelectionLineID.Equals("3");
                chkANy_Tingkat_Berat.Value = ent.QuestionAnswerSelectionLineID.Equals("4");
            }

            // Nyeri Berlangsung
            questionID = "ASS.NYE10";
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkANy_Tingkat_Tidak.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
                chkANy_Tingkat_Ringan.Value = ent.QuestionAnswerSelectionLineID.Equals("2");
            }

            // Lama Nyeri 
            questionID = "ASS.NYE12";
            ent = phrlColl.FirstOrDefault(x => questionID.Equals(x.QuestionID));
            if (ent != null)
            {
                chkANy_Tingkat_Tidak.Value = ent.QuestionAnswerSelectionLineID.Equals("1");
                chkANy_Tingkat_Ringan.Value = ent.QuestionAnswerSelectionLineID.Equals("2");
            }
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