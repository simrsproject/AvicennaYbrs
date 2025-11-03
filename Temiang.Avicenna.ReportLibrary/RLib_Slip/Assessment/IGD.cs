using System;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment
{

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class IGD : Telerik.Reporting.Report
    {
        public IGD(string programID, PrintJobParameterCollection printJobParameters)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoAndTextBottom(this.pageHeader);

            var patientID = printJobParameters.FindByParameterName("PatientID").ValueString;
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;

            var pat = new Patient();
            pat.LoadByPrimaryKey(patientID);
            txtPatientName.Value = pat.PatientName;
            txtMedicalNo.Value = pat.MedicalNo;

            var asses = new PatientAssessment();
            asses.LoadByPrimaryKey(rimid);

            var reg = new BusinessObject.Registration();
            reg.LoadByPrimaryKey(asses.RegistrationNo);
            txtBirthDateAge.Value = string.Format("{0} - {1}Y {2}M",
                Convert.ToDateTime(pat.DateOfBirth).ToString(AppConstant.DisplayFormat.Date), reg.AgeInYear,
                reg.AgeInMonth);

            txtRegistrationDate.Value = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.Date);
            txtRegistrationTime.Value = reg.RegistrationTime;
            txtHandleTime.Value = Convert.ToDateTime(asses.AssessmentDateTime).ToString("HH:mm");

            PopulateAllergy(patientID);
            PopulateRiwayatPenyakitDahulu(patientID);

            txtMedikamentosa.Value = asses.Medikamentosa;

            PopulatePhysicalExam(asses);

            RegistrationInfoMedic rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(rimid);
            txtSubjective.Value = rim.Info1;
            txtAssessment.Value = rim.Info3;
            txtPlanning.Value = rim.Info4;

            PopulateLocalistStatus(asses);

            PopulateVitalSign(asses.RegistrationNo);

            PopulateEducation(asses);

            var par = new Paramedic();
            if (par.LoadByPrimaryKey(reg.ParamedicID))
            {
                txtParamedicName.Value = "( "+ par.ParamedicName +" )";
            }
        }

        private void PopulateVitalSign(string registrationNo)
        {
            var phr = new PatientHealthRecordQuery("phr");
            var phrl = new PatientHealthRecordLineQuery("phrl");
            phr.InnerJoin(phrl).On(phr.TransactionNo == phrl.TransactionNo);

            var quest = new QuestionQuery("q");
            phr.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            var vital = new VitalSignQuery("v");
            phr.InnerJoin(vital).On(quest.VitalSignID == vital.VitalSignID);

            phr.Where(phr.RegistrationNo == registrationNo);
            phr.OrderBy(vital.VitalSignID.Ascending);
            phr.Select(quest.VitalSignID, quest.SRAnswerType, phrl.QuestionAnswerText, phrl.QuestionAnswerNum, phrl.QuestionAnswerText2);
            var dtb = phr.LoadDataTable();
            dtb.Columns.Add("QuestionAnswerFormatted", typeof(System.String));

            foreach (System.Data.DataRow r in dtb.Rows)
            {
                var answerText = r["QuestionAnswerText"].ToString();
                switch (r["SRAnswerType"].ToString())
                {
                    case "CNM":
                        try
                        {
                            answerText = answerText.Split('|')[1];
                        }
                        catch
                        {
                        }

                        break;
                    default:
                        answerText = string.IsNullOrEmpty(answerText) ? 
                                RemoveZeroDigits(Convert.ToDecimal(r["QuestionAnswerNum"] == DBNull.Value ? -1 : r["QuestionAnswerNum"])) : (r["QuestionAnswerText"].ToString());
                        break;
                }

                r["QuestionAnswerText"] = answerText;
            }
            dtb.AcceptChanges();

            //Frekuensi Pernapasan	RESP
            //    Suhu	TEMP
            //    Nadi	HEART
            //Panjang / tinggi badan	HEIGHT
            //Lingkar kepala	HCCM
            //    Berat badan bayi	WEIGHT
            //    BB lahir	WEIGHT
            //Tekanan Darah - Diastole	BP2
            //    Tekanan Darah - Sistole	BP1
            //    Lingkar Kepala (Khusus Pediatri)	HCCM
            //    Berat badan	WEIGHT
            //IMT	BMI
            //Tinggi badan	HEIGHT
            //    Frekuensi Pernapasan	RESP
            foreach (DataRow row in dtb.Rows)
            {
                if (row["VitalSignID"].Equals("BP1"))
                    txtTekananDarah.Value = row["QuestionAnswerText"].ToString();
                else if (row["VitalSignID"].Equals("BP2"))
                    txtTekananDarah.Value = string.Format("{0}/{1}", txtTekananDarah.Value, row["QuestionAnswerText"]);
                else if (row["VitalSignID"].Equals("HEART"))
                    txtHeartRate.Value = row["QuestionAnswerText"].ToString();
                else if (row["VitalSignID"].Equals("RESP"))
                    txtRespiratoryRate.Value = row["QuestionAnswerText"].ToString();
                else if (row["VitalSignID"].Equals("TEMP"))
                    txtTemperature.Value = row["QuestionAnswerText"].ToString();
                else if (row["VitalSignID"].Equals("SPO2"))
                    txtSPO2.Value = row["QuestionAnswerText"].ToString();
            }
        }
        private string RemoveZeroDigits(decimal value)
        {
            return value == -1 ? "-" : Convert.ToString(value / 1.000000000000000000000000000000M);
        }
        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<IgdPe>(asses.PhysicalExam);
                var gcs = pexam.Consciousness;
                txtConsciousness.Value = gcs.ConsciousnessDescription;
                txtGcsEye.Value = gcs.Eye.Score.ToString();
                txtGcsMotor.Value = gcs.Motor.Score.ToString();
                txtGcsVerbal.Value = gcs.Verbal.Score.ToString();

                txtPhysicalExam.Value = pexam.PshysicalExam;
                txtAncillaryExam.Value = pexam.AncillaryExam;

                chkDipulangkan.Value = pexam.VisitType=="01";
                chkPulang.Value = pexam.VisitType=="02";

                chkDied.Value  = pexam.VisitType=="03";
                txtVisitDiedTime.Value = pexam.VisitDiedTime == "00:00" ? string.Empty : pexam.VisitDiedTime;

                chkDOA.Value = pexam.VisitType == "04";
                txtVisitDoaTime.Value = pexam.VisitDoaTime == "00:00" ? string.Empty : pexam.VisitDoaTime; 


                chkKamarBersalin.Value = pexam.ReferredAction == "01";
                    chkHemodialisa.Value = pexam.ReferredAction == "02";
                chkKamarOperasi.Value = pexam.ReferredAction == "03";
                    chkPlaningLain.Value = pexam.ReferredAction == "99";
                txtPlaningLain.Value = pexam.ReferredActionEtc;

                txtDirujukKeRS.Value = pexam.ReferredTo;
                txtIndikasi.Value = pexam.ReferredIndication;
                txtInPatientRoom.Value = pexam.InPatientRoom;
                txtDPJP.Value = pexam.InPatientDpjp;
                txtDispositionDate.Value = Convert.ToDateTime(pexam.DispositionDate).ToString(AppConstant.DisplayFormat.Date);
                txtDispositionHour.Value = pexam.DispositionHour;

            }
            catch (Exception)
            {
                //Nothing
            }
        }

        private void PopulateAllergy(string patientID)
        {
            var allgs = new PatientAllergyCollection();
            allgs.Query.Where(allgs.Query.PatientID == patientID,
                allgs.Query.Or(allgs.Query.Allergen.Like("%DRUG%"), allgs.Query.Allergen.Like("%FOOD%")));
            allgs.LoadAll();
            var allergy = string.Empty;
            foreach (PatientAllergy allg in allgs)
            {
                allergy = string.Concat(allergy, ", ", allg.DescAndReaction);
            }
            if (!string.IsNullOrEmpty(allergy))
                allergy = allergy.Substring(2);

            chkALergiTidak.Value = string.IsNullOrEmpty(allergy);
            chkAlergiYa.Value = !string.IsNullOrEmpty(allergy);

            txtAllergy.Value = allergy;
        }
        private void PopulateRiwayatPenyakitDahulu(string patientID)
        {
            var pmhColl = new BusinessObject.PastMedicalHistoryCollection();
            pmhColl.Query.Where(pmhColl.Query.PatientID == patientID);
            pmhColl.LoadAll();

            var rpd = string.Empty;
            foreach (var pmh in pmhColl)
            {
                switch (pmh.SRMedicalDisease)
                {
                    case "001":
                        rpd = AppendStringWithSeparator(rpd, "Hypertensi");
                        break;
                    case "002":
                        rpd = AppendStringWithSeparator(rpd, "Jantung");
                        break;
                    case "003":
                        rpd = AppendStringWithSeparator(rpd, "Tumor");
                        break;
                    case "004":
                        rpd = AppendStringWithSeparator(rpd, "Hepatitis");
                        break;
                    case "005":
                        rpd = AppendStringWithSeparator(rpd, "TB Paru");
                        break;
                    case "006":
                        rpd = AppendStringWithSeparator(rpd, "Struma");
                        break;
                    case "007":
                        rpd = AppendStringWithSeparator(rpd, "DM");
                        break;
                    case "008":
                        rpd = AppendStringWithSeparator(rpd, "Asma");
                        break;
                    case "009":
                        rpd = AppendStringWithSeparator(rpd, "Kelainan Darah");
                        break;
                    case "010":
                        rpd = AppendStringWithSeparator(rpd, "Ginjal");
                        break;
                    case "011":
                        rpd = AppendStringWithSeparator(rpd, "Stroke");
                        break;
                    case "012":
                        rpd = AppendStringWithSeparator(rpd, "Trauma");
                        break;
                    case "014":
                        rpd = AppendStringWithSeparator(rpd, pmh.Notes);
                        break;
                }
            }
            txtRiwayatPenyakit.Value = rpd;
        }

        private string AppendStringWithSeparator(string word, string addWord)
        {
            if (string.IsNullOrEmpty(word))
                word = string.Format("{0}, {1}", word, addWord);
            else
                word = addWord;
            return word;
        }

        private void PopulateLocalistStatus(PatientAssessment asses)
        {
            // Reset Image
            picLocalistStatus01.Value = null;

            // Update Image
            var loc = new RegistrationInfoMedicBodyDiagramCollection();
            loc.Query.Where(loc.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            if (loc.LoadAll())
            {
                if (loc.Count > 0 && loc[0] != null)
                {
                    picLocalistStatus01.Value = (new ImageHelper()).ConvertByteArrayToImage(loc[0].BodyImage);
                }
            }
        }
        private void PopulateEducation(PatientAssessment asses)
        {
            // Get Education
            if (string.IsNullOrEmpty(asses.Education)) return;

            // Convert to class w json
            try
            {
                var edus = JsonConvert.DeserializeObject<Educations>(asses.Education);
                if (edus.Items == null) return;

                var strb = new StringBuilder();
                foreach (var edu in edus.Items)
                {
                    if (edu.ID == "001")
                        strb.AppendLine("- Penggunaan obat-obatan");
                    else if (edu.ID == "002")
                        strb.AppendLine("- Penggunaan peralatan medis");
                    else if (edu.ID == "003")
                        strb.AppendLine("- Potensi interaksi obat yang diresepkan dengan obat lainnya");
                    else if (edu.ID == "004")
                        strb.AppendLine("- Diet dan nutrisi");
                    else if (edu.ID == "005")
                        strb.AppendLine("- Manajemen nyeri");
                    else if (edu.ID == "006")
                        strb.AppendLine("- Tehnik-tehnik Rehabilitasi");
                    else if (edu.ID == "999")
                    {
                        strb.AppendLine("- Lainnya: " + edu.Notes);
                    }
                }

                txtEdukasi.Value = strb.ToString();
            }
            catch (Exception)
            {
                // Nothing
            }
        }
    }
}