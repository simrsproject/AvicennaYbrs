using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportDataSource.Common
{
    public class AssessmentUtil
    {
        public static object TransferToInPatient(string registrationNo)
        {
            // Serah terima pasien ke ruangan ambil dari PHR  -> PATIENTTRF
            var transferQId = AppParameter.GetParameterValue(AppParameter.ParameterItem.PatientHandOverFormID);

            var isPulang = false;
            var isKeRuangan = false;
            var observasi = string.Empty;
            var kondUmum = string.Empty;
            var tds = string.Empty;
            var tdd = string.Empty;
            var rr = string.Empty;
            var spO2 = string.Empty;
            var nadi = string.Empty;
            var bb = string.Empty;
            var tb = string.Empty;
            var suhu = string.Empty;
            var ruangan = string.Empty;
            var kelas = string.Empty;
            var tiba = string.Empty;
            var caraPindah = string.Empty;
            var pengirim = string.Empty;
            var penerima = string.Empty;


            var phr = new PatientHealthRecord();
            phr.Query.Where(phr.Query.QuestionFormID == transferQId,
                phr.Query.RegistrationNo == registrationNo);
            phr.Query.es.Top = 1;

            if (phr.Query.Load())
            {
                var phrl = new PatientHealthRecordLineCollection();
                phrl.Query.Where(phrl.Query.TransactionNo == phr.TransactionNo,
                    phrl.Query.QuestionFormID == "PATIENTTRF", phrl.Query.RegistrationNo == registrationNo);
                phrl.LoadAll();

                foreach (var line in phrl)
                {
                    if (line.QuestionID == "PATTR.01") // Observasi terakhir pukul
                        observasi = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.02") // Kondisi Umum
                        kondUmum = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.03") // TD Sistolic
                        tds = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.04") // TD Diastolic
                        tdd = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.05") // RR
                        rr = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.06") // Nadi
                        nadi = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.07") // SpO2
                        spO2 = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.08") // Suhu
                        suhu = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.10") // Pindah ke ruangan
                        ruangan = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.11") // Kelas
                        kelas = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.12") // Tiba
                        tiba = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.20") // Cara Pindah
                        caraPindah = line.QuestionAnswerSelectionLineID;

                    if (line.QuestionID == "PATTR.21") // Perawat yg Mengirim
                        pengirim = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.22") // Perawat yg Menerima
                        penerima = line.QuestionAnswerText;

                    if (line.QuestionID == "PATTR.30") // Ruangan / Pulang 
                    {
                        if (line.QuestionAnswerSelectionLineID != null)
                        {
                            isPulang = "PLG".Equals(line.QuestionAnswerSelectionLineID);
                            isKeRuangan = "RNG".Equals(line.QuestionAnswerSelectionLineID);
                        }
                    }

                }
            }

            var obj = new
            {
                IsPulang = isPulang,
                IsKeRuangan = isKeRuangan,
                ObservasiTerakhir = string.IsNullOrWhiteSpace(observasi) ? string.Empty : Convert.ToDateTime(observasi).ToString("HH:mm"),
                KondisiUmum = kondUmum,
                TD = string.Format("{0}/{1} mmHg", tds, tdd),
                RR = string.Format("{0} mmHg", rr),
                SpO2 = string.Format("{0} %", spO2),
                Nadi = string.Format("{0} x/mnt", nadi),
                BB = string.Format("{0} kg", bb),
                TB = string.Format("{0} cm", tb),
                Suhu = string.Format("{0} C", suhu),
                Ruangan = ruangan,
                Kelas = kelas,
                Tiba = string.IsNullOrWhiteSpace(tiba) ? string.Empty : Convert.ToDateTime(tiba).ToString("HH:mm"),
                CaraPindah = new
                {
                    IsBrankar = caraPindah == "01",
                    IsKursiRoda = caraPindah == "02",
                    IsJalan = caraPindah == "03",
                    IsDigendong = caraPindah == "04"
                },
                PerawatPengirim = pengirim,
                PerawatPenerima = penerima
            };

            return obj;
        }

        [Obsolete("Ganti dg PatientEducation", true)]
        public static object Education(Educations edus)
        {
            var isObat = false;
            var isMedis = false;
            var isPotensi = false;
            var isDiet = false;
            var isNyeri = false;
            var isRehab = false;
            var isLainnya = false;
            var lainnya = string.Empty;


            if (edus != null && edus.Items != null)
            {
                foreach (var edu in edus.Items)
                {
                    if (edu.ID == "001")
                        isObat = true;
                    else if (edu.ID == "002")
                        isMedis = true;
                    else if (edu.ID == "003")
                        isPotensi = true;
                    else if (edu.ID == "004")
                        isDiet = true;
                    else if (edu.ID == "005")
                        isNyeri = true;
                    else if (edu.ID == "006")
                        isRehab = true;
                    else if (edu.ID == "999")
                    {
                        isLainnya = true;
                        lainnya = edu.Notes;
                    }
                }
            }


            var obj = new
            {
                IsObat = isObat,
                IsMedis = isMedis,
                IsPotensi = isPotensi,
                IsDiet = isDiet,
                IsNyeri = isNyeri,
                IsRehab = isRehab,
                IsLainnya = isLainnya,
                Lainnya = lainnya
            };

            return obj;

        }
        public static object FollowUpOutPatient(PatientAssessment asses)
        {
            DateTime? deadDate = null;
            var phrl = new PatientHealthRecordLine();
            phrl.Query.Where(phrl.Query.RegistrationNo == asses.RegistrationNo, phrl.Query.QuestionID == PhrUtil.PatientLetterQuestion.DeadDate);
            phrl.Query.es.Top = 1;
            if (phrl.Query.Load())
                deadDate = Convert.ToDateTime(phrl.QuestionAnswerText);

            var followUp = asses.FollowUpPlanType ?? string.Empty;
            var obj = new
            {
                RawatInap = new
                {
                    IsRawatInap = !string.IsNullOrEmpty(followUp) && "INP_SUR_RJT".Contains(followUp),
                    Estimasi = asses.EstimatedDayInPatient,
                    Ruang = asses.RoomInPatient,
                    Dpjp = asses.DpjpInPatient,
                    IsYesPengantar = asses.IsInPatientGuide ?? false,
                    IsNoPengantar = asses.IsInPatientGuide == null ? false : asses.IsInPatientGuide,
                    IsMenolak = "RJT".Equals(followUp),
                    AlasanMenolak = asses.InPatientRejectReason,
                    IsOperasi = "SUR".Equals(followUp),
                    TglOpr = asses.SurgicalDateTime == null ? string.Empty : asses.SurgicalDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),
                    JamOpr = asses.SurgicalDateTime == null ? string.Empty : asses.SurgicalDateTime.Value.ToString(AppConstant.DisplayFormat.HourMin),

                    //Tambahan RSYS
                    IsNone = "NON".Equals(followUp)

                },
                RujukKe = new
                {
                    IsRujuk = !string.IsNullOrEmpty(followUp) && (new string[] { "RHS", "RFD", "RPK", "RDT", "RHC" }).Contains(followUp),
                    IsRS = followUp == "RHS",
                    RS = asses.ReferToHospital,
                    IsDokterKeluarga = followUp == "RFD",
                    DokterKeluarga = asses.ReferToFamilyDoctor,
                    IsPuskesmas = followUp == "RPK",
                    IsDokter = followUp == "RDT",
                    IsHomeCare = followUp == "RHC",
                    Alasan = asses.ReferReason
                },
                KonsulKlinik = new
                {
                    isKonsul = !string.IsNullOrEmpty(followUp) && (new string[] { "CIN", "CPD", "CNR", "CSG", "CTH", "CEY", "PDP", "CMR", "TET", "CNT", "COT"}).Contains(followUp),
                    IsInterna = followUp == "CIN",
                    IsPediatri = followUp == "CPD",
                    IsKebidanan = followUp == "CNR",
                    IsBedah = followUp == "CSG",
                    IsTht = followUp == "CTH",
                    IsMata = followUp == "CEY",
                    IsPDP = followUp == "PDP",
                    Tanggal = asses.ConsultDate == null ? string.Empty : asses.ConsultDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),

                    /// <summary>
                    /// Tambahan u/ cetakan assessment igd : Follow Up
                    /// </summary>
                    /// Create By: Fajri
                    /// Create Date: 2023-March-07
                    /// Clinet Req: RSYS
                    IsRehabMedis = followUp == "CMR",
                    IsNutrisi = followUp == "CNT",
                    IsGigi = followUp == "TET",
                    IsKlinikLainnya = followUp == "COT",
                    KlinikLainnya = asses.ConsulTo

                },
                DOA = new
                {
                    IsDoa = "DOA".Equals(followUp),
                    Tgl = asses.DoaDateTime == null ? string.Empty : asses.DoaDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),
                    Jam = asses.DoaDateTime == null ? string.Empty : asses.DoaDateTime.Value.ToString(AppConstant.DisplayFormat.HourMin)
                },
                Dead = new
                {
                    IsDead = deadDate != null,
                    Tgl = deadDate == null ? string.Empty : deadDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),
                    Jam = deadDate == null ? string.Empty : deadDate.Value.ToString(AppConstant.DisplayFormat.HourMin)
                }

            };

            return obj;
        }

        public static object DischargePlanInPatient(PatientAssessment asses, Registration reg)
        {
            var obj = new
            {
                Therapy = asses.Therapy,
                EstDayInPatient = asses.EstimatedDayInPatient,
                Prognosis = asses.Prognosis,
                DischargeDate = reg.RegistrationDate.Value.AddDays(asses.EstimatedDayInPatient.ToInt()).Date.ToString(AppConstant.DisplayFormat.DateShortMonth)
            };

            return obj;
        }

        public static string TherapyPlanning(string registrationInfoMedicID)
        {
            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(registrationInfoMedicID);
            return rim.Info4;
        }

        //Tambahan untuk penggabungan Planning dan Prescription (Fajri 31-Mar-2023)
        public static string PlanningAndPrescription(string registrationInfoMedicID)
        {
            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(registrationInfoMedicID);
            var planAndPresc = string.Format("{0} \n {1}", rim.Info4, rim.PrescriptionCurrentDay);
            return planAndPresc;
        }

        public static string DrugFoodAllergy(string patientID)
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

            return allergy;
        }

        public static object RiwayatPenyakitKeluarga(string patientID)
        {
            var isHypertensi = false;
            var isJantung = false;
            var isTumor = false;
            var isHepatitis = false;
            var isTBParu = false;
            var isStruma = false;
            var isDM = false;
            var isAsma = false;
            var isKelainanDarah = false;
            var isGinjal = false;
            var isStroke = false;
            var isTrauma = false;
            var isLainnya = false;
            var lainnya = string.Empty;
            var summary = string.Empty;

            var famhColl = new FamilyMedicalHistoryCollection();
            famhColl.Query.Where(famhColl.Query.PatientID == patientID);
            famhColl.LoadAll();
            foreach (var famh in famhColl)
            {
                switch (famh.SRMedicalDisease)
                {
                    case "001":
                        isHypertensi = true;
                        summary = string.Concat(summary, ", Hypertensi");
                        break;
                    case "002":
                        isJantung = true;
                        summary = string.Concat(summary, ", Jantung");
                        break;
                    case "003":
                        isTumor = true;
                        summary = string.Concat(summary, ", Tumor");
                        break;
                    case "004":
                        isHepatitis = true;
                        summary = string.Concat(summary, ", Hepatitis");
                        break;
                    case "005":
                        isTBParu = true;
                        summary = string.Concat(summary, ", TB");
                        break;
                    case "006":
                        isStruma = true;
                        summary = string.Concat(summary, ", Struma");
                        break;
                    case "007":
                        isDM = true;
                        summary = string.Concat(summary, ", DM");
                        break;
                    case "008":
                        isAsma = true;
                        summary = string.Concat(summary, ", Jantung");
                        break;
                    case "009":
                        isKelainanDarah = true;
                        summary = string.Concat(summary, ", Kelainan Darah");
                        break;
                    case "010":
                        isGinjal = true;
                        summary = string.Concat(summary, ", Ginjal");
                        break;
                    case "011":
                        isStroke = true;
                        summary = string.Concat(summary, ", Stroke");
                        break;
                    case "012":
                        isTrauma = true;
                        summary = string.Concat(summary, ", Trauma");
                        break;
                    case "999":
                        isLainnya = true;
                        lainnya = famh.Notes;
                        if (!string.IsNullOrEmpty(lainnya))
                            summary = string.Concat(summary, ", ", lainnya);

                        break;
                }
            }
            var obj = new
            {
                IsHypertensi = isHypertensi,
                IsJantung = isJantung,
                IsTumor = isTumor,
                IsHepatitis = isHepatitis,
                IsTBParu = isTBParu,
                IsStruma = isStruma,
                IsDM = isDM,
                IsAsma = isAsma,
                IsKelainanDarah = isKelainanDarah,
                IsGinjal = isGinjal,
                IsStroke = isStroke,
                IsTrauma = isTrauma,
                IsLainnya = isLainnya,
                Lainnya = lainnya,
                Summary = summary
            };

            return obj;

        }

        public static object RiwayatPenyakitDahulu(string patientID)
        {
            var isHypertensi = false;
            var isJantung = false;
            var isTumor = false;
            var isHepatitis = false;
            var isTBParu = false;
            var isStruma = false;
            var isDM = false;
            var isAsma = false;
            var isKelainanDarah = false;
            var isGinjal = false;
            var isStroke = false;
            var isTrauma = false;
            var isLainnya = false;
            var lainnya = string.Empty;
            var summary = string.Empty;

            var pmhColl = new PastMedicalHistoryCollection();
            pmhColl.Query.Where(pmhColl.Query.PatientID == patientID);
            pmhColl.LoadAll();

            foreach (var pmh in pmhColl)
            {
                switch (pmh.SRMedicalDisease)
                {
                    case "001":
                        isHypertensi = true;
                        summary = string.Concat(summary, ", Hypertensi");
                        break;
                    case "002":
                        isJantung = true;
                        summary = string.Concat(summary, ", Jantung");
                        break;
                    case "003":
                        isTumor = true;
                        summary = string.Concat(summary, ", Tumor");
                        break;
                    case "004":
                        isHepatitis = true;
                        summary = string.Concat(summary, ", Hepatitis");
                        break;
                    case "005":
                        isTBParu = true;
                        summary = string.Concat(summary, ", TB");
                        break;
                    case "006":
                        isStruma = true;
                        summary = string.Concat(summary, ", Struma");
                        break;
                    case "007":
                        isDM = true;
                        summary = string.Concat(summary, ", DM");
                        break;
                    case "008":
                        isAsma = true;
                        summary = string.Concat(summary, ", Jantung");
                        break;
                    case "009":
                        isKelainanDarah = true;
                        summary = string.Concat(summary, ", Kelainan Darah");
                        break;
                    case "010":
                        isGinjal = true;
                        summary = string.Concat(summary, ", Ginjal");
                        break;
                    case "011":
                        isStroke = true;
                        summary = string.Concat(summary, ", Stroke");
                        break;
                    case "012":
                        isTrauma = true;
                        summary = string.Concat(summary, ", Trauma");
                        break;
                    case "999":
                        isLainnya = true;
                        lainnya = pmh.Notes;
                        if (!string.IsNullOrEmpty(lainnya))
                            summary = string.Concat(summary, ", ", lainnya);

                        break;
                }
            }

            var obj = new
            {
                IsHypertensi = isHypertensi,
                IsJantung = isJantung,
                IsTumor = isTumor,
                IsHepatitis = isHepatitis,
                IsTBParu = isTBParu,
                IsStruma = isStruma,
                IsDM = isDM,
                IsAsma = isAsma,
                IsKelainanDarah = isKelainanDarah,
                IsGinjal = isGinjal,
                IsStroke = isStroke,
                IsTrauma = isTrauma,
                IsLainnya = isLainnya,
                Lainnya = lainnya,
                Summary = summary
            };

            return obj;
        }

        /// <summary>
        /// Diagnosis Final
        /// </summary>
        /// <param name="asses"></param>
        /// <returns></returns>
        public static object Diagnosis(PatientAssessment asses)
        {
            var diagMain = string.Empty;
            var icdMain = string.Empty;
            var diagDiff = string.Empty;
            var icdDiff = string.Empty;
            var icdSynon = string.Empty;

            var diags = new EpisodeDiagnoseCollection();
            diags.Query.Where(diags.Query.RegistrationNo == asses.RegistrationNo);
            diags.LoadAll();

            foreach (EpisodeDiagnose diag in diags)
            {
                //DiagnoseType-001	01 - Main Diagnose
                //DiagnoseType-002	02 - Complication
                //DiagnoseType-003	04 - Comparative Diagnosis
                //DiagnoseType-004	03 - Secondary Diagnose
                //DiagnoseType-005	05 - Lain-Lain

                if (diag.SRDiagnoseType == "DiagnoseType-001")
                {
                    diagMain = diag.DiagnosisText;
                    icdMain = diag.DiagnoseID;
                    icdSynon = diag.DiagnoseSynonym;
                }
                if (diag.SRDiagnoseType == "DiagnoseType-003")
                {
                    diagDiff = diag.DiagnosisText;
                    icdDiff = diag.DiagnoseID;
                    icdSynon = diag.DiagnoseSynonym;
                }
            }

            var obj = new
            {
                Kerja = string.IsNullOrEmpty(asses.Diagnose) ? diagMain : string.Format("{0}{1}{1},{2}", asses.Diagnose, Environment.NewLine, diagMain),
                Banding = diagDiff,
                IcdXKerja = icdMain,
                IcdXBanding = icdDiff,
                Synonym = icdSynon
            };

            return obj;
        }


        /// <summary>
        /// Work Diagnosis for InPatient
        /// </summary>
        /// <param name="asses"></param>
        /// <returns></returns>
        public static object WorkDiagnosis(PatientAssessment asses)
        {
            var diagMain = string.Empty;
            var icdMain = string.Empty;
            var diagDiff = string.Empty;
            var icdDiff = string.Empty;
            var icdSynon = string.Empty;

            var diags = new RegistrationInfoMedicDiagnoseCollection();
            diags.Query.Where(diags.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            diags.LoadAll();

            foreach (RegistrationInfoMedicDiagnose diag in diags)
            {
                //DiagnoseType-001	01 - Main Diagnose
                //DiagnoseType-002	02 - Complication
                //DiagnoseType-003	04 - Comparative Diagnosis
                //DiagnoseType-004	03 - Secondary Diagnose
                //DiagnoseType-005	05 - Lain-Lain

                if (diag.SRDiagnoseType == "DiagnoseType-001")
                {
                    diagMain = diag.DiagnosisText;
                    icdMain = diag.DiagnoseID;
                    icdSynon = diag.DiagnoseSynonym;
                }
                if (diag.SRDiagnoseType == "DiagnoseType-003")
                {
                    diagDiff = diag.DiagnosisText;
                    icdDiff = diag.DiagnoseID;
                    icdSynon = diag.DiagnoseSynonym;
                }
            }

            var obj = new
            {
                Kerja = string.IsNullOrEmpty(asses.Diagnose) ? diagMain : string.Format("{0}{1}{1},{2}", asses.Diagnose, Environment.NewLine, diagMain),
                Banding = diagDiff,
                IcdXKerja = icdMain,
                IcdXBanding = icdDiff,
                Synonym = icdSynon
            };

            return obj;
        }

        public static object VitalSignField(List<string> mergeRegs, DateTime lastDateTime)
        {
            var vs = new
            {
                BB = VitalSign.LastVitalSignWithUnit(mergeRegs,
                    VitalSign.VitalSignEnum.BodyWeight, lastDateTime),
                TB = VitalSign.LastVitalSignWithUnit(mergeRegs,
                    VitalSign.VitalSignEnum.BodyHeight, lastDateTime),
                Suhu = VitalSign.LastVitalSignWithUnit(mergeRegs,
                    VitalSign.VitalSignEnum.Temperature, lastDateTime),
                TekananDarah =
                    VitalSign.LastVitalSignWithUnit(mergeRegs,
                        VitalSign.VitalSignEnum.BloodPressure, lastDateTime),
                Nadi = VitalSign.LastVitalSignWithUnit(mergeRegs,
                    VitalSign.VitalSignEnum.HeartRate, lastDateTime),
                Pernafasan = VitalSign.LastVitalSignWithUnit(mergeRegs,
                    VitalSign.VitalSignEnum.RespiratoryRate, lastDateTime)
            };
            return vs;
        }

        public static object VitalSignFolderField(List<string> mergeRegs, DateTime lastDateTime)
        {
            var vs = new
            {
                VitalSign = VitalSignField(mergeRegs, lastDateTime)
            };
            return vs;
        }
        private static string SurgicalHistory(string patientID, DateTime lastDateTime)
        {
            var strb = new StringBuilder();
            //Past Surgery
            var surgicalHist = new PastSurgicalHistory();
            if (surgicalHist.LoadByPrimaryKey(patientID))
            {
                strb.AppendLine(surgicalHist.SurgicalHistory);
            }

            var ep = new EpisodeProcedureQuery("a");
            var reg = new RegistrationQuery("b");
            ep.InnerJoin(reg).On(reg.RegistrationNo == ep.RegistrationNo);
            ep.Where(reg.PatientID == patientID, reg.RegistrationDate < lastDateTime.Date);
            ep.Select(ep.ProcedureDate, ep.ProcedureName);
            ep.OrderBy(ep.ProcedureName.Ascending);

            var dtbSurgery = ep.LoadDataTable();
            foreach (DataRow surgery in dtbSurgery.Rows)
            {
                strb.AppendFormat("{0} {1}", Convert.ToDateTime(surgery["ProcedureDate"]).ToString(AppConstant.DisplayFormat.Date), surgery["ProcedureName"]);
                strb.AppendLine(string.Empty);
            }
            return strb.ToString();
        }

        public static object SosialEkonomiField(Patient pat, Registration reg)
        {
            //reg.SRMaritalStatus = cboSRMaritalStatus.SelectedValue;
            //reg.SRRelationshipQuality = cboSRRelationshipQuality.SelectedValue;
            //reg.SRResidentialHome = cboSRResidentialHome.SelectedValue;
            //reg.SROccupation = cboSROccupation.SelectedValue;


            var isWiraswasta = pat.SRFatherOccupation == "12" || pat.SRMotherOccupation == "12";
            var isPNS = pat.SRFatherOccupation == "01" || pat.SRMotherOccupation == "01";
            var isSwasta = pat.SRFatherOccupation == "06" || pat.SRMotherOccupation == "06";
            var isPensiunan = false; //TODO: Occupation Tambah Pensiunan

            var home = pat.SRResidentialHome ?? string.Empty;
            var sosEkonomi = new
            {
                PekerjaanOrtu = new
                {
                    IsWiraswasta = isWiraswasta,
                    IsPNS = isPNS,
                    IsSwasta = isSwasta,
                    IsPensiunan = isPensiunan,
                    IsLainnya = !(isWiraswasta || isPNS || isSwasta || isPensiunan),
                    Lainnya = StandardReference.GetItemName(AppEnum.StandardReference.Occupation, pat.SRFatherOccupation)
                },
                TinggalBrsma = new
                {
                    IsOrtu = "FamilyHouse".Equals(home),
                    IsLainnya = !"FamilyHouse".Equals(home),
                    Lainnya = StandardReference.GetItemName(AppEnum.StandardReference.ResidentialHome, home)
                }
            };
            return sosEkonomi;
        }
        public static object PhysicianCommonField(Patient pat, Registration reg, PatientAssessment asses)
        {
            var edu = new PatientEducation();
            if (!edu.LoadByPrimaryKey(asses.RegistrationNo, asses.PatientEducationSeqNo ?? 0))
                edu = new PatientEducation();

            var plan = new MedicalDischargeSummaryByNurse();
            plan.LoadByPrimaryKey(asses.RegistrationNo);

            var obj = new
            {
                IsAutoAnamnesa = asses.IsAutoAnamnesis ?? false,
                IsAlloanamnesa = !asses.IsAutoAnamnesis ?? false,
                Alloanamnesa = asses.AllowAnamnesisSource,
                Keluhan = reg.Complaint,
                PenyakitSkrg = asses.Hpi,
                AnamnesisNotes = asses.AnamnesisNotes,
                PenyakitDahulu = RiwayatPenyakitDahulu(reg.PatientID),
                PenyakitKeluarga = RiwayatPenyakitKeluarga(reg.PatientID),
                SurgicalHist = SurgicalHistory(reg.PatientID, reg.RegistrationDate.Value),
                AlergiObatMakanan = DrugFoodAllergy(reg.PatientID),
                asses.Medikamentosa,
                PemeriksaanPenunjang = asses.OtherExam,
                Diagnosis = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? WorkDiagnosis(asses) : Diagnosis(asses),
                Operasi = ConvertDataTabletoObject(PatientProcedure(reg.RegistrationNo)),
                Tatalaksana = TherapyPlanning(asses.RegistrationInfoMedicID),
                FollowUp = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? string.Empty : FollowUpOutPatient(asses),
                NextControlPlan = ControlPlanExtItems(plan.ControlPlan),
                DischargePlan = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? DischargePlanInPatient(asses, reg) : string.Empty,
                EdukasiItems = ConvertDataTabletoObject(PatientEducation(reg.RegistrationNo, asses.PatientEducationSeqNo ?? 0)),
                Dpjp = ParamedicTeam.DPJP(asses.RegistrationNo).ParamedicName,
                IsEdukasiPasien = edu.SRPatientEducationRecipient == "001",
                IsEdukasiKeluarga = edu.SRPatientEducationRecipient != "001",
                PenerimaEdukasi = edu.SRPatientEducationRecipient == "001" ? pat.PatientName : edu.RecipientName,
                Tgljamassement = asses.CreatedDateTime,
                TglPeriksa = Convert.ToDateTime(asses.AssessmentDateTime).ToString(AppConstant.DisplayFormat.DateShortMonth),

                /// <summary>
                /// Tambahan u/ cetakan assessment igd : Education
                /// </summary>
                /// Create By: Fajri
                /// Create Date: 2023-March-07
                /// Clinet Req: RSYS
                OptnMetodeEdukasi = StandardReference.GetItemName(AppEnum.StandardReference.PatientEducationMethod, edu.SRPatientEducationMethod),
                MetodeEdukasi = edu.MethodOther,
                OptnPenerimaEdukasi = StandardReference.GetItemName(AppEnum.StandardReference.PatientEducationRecipient, edu.SRPatientEducationRecipient),
                OptnEvaluasiEdukasi = StandardReference.GetItemName(AppEnum.StandardReference.PatientEducationEvaluation, edu.SRPatientEducationEvaluation),
                EvaluasiEdukasi = edu.PatientEducationEvaluationOth,
                OptnGoalEdukasi = StandardReference.GetItemName(AppEnum.StandardReference.PatientEducationGoal, edu.SRPatientEducationGoal),
                GoalEdukasi = edu.PatientEducationGoalOth,
                DurasiEdukasi = edu.Duration,
                CatatanSubjektif = asses.SubjectiveAddNote,
                PlanAndPrescription = PlanningAndPrescription(asses.RegistrationInfoMedicID),
                DokterPembuat = AppUser.GetUserName(asses.CreatedByUserID)
            };
            return obj;
        }
        private static List<Dictionary<string, object>> ConvertDataTabletoObject(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return rows;
        }
        private static DataTable PatientEducation(string registrationNo, int seqNo)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrLine = new PatientEducationLineQuery("a");

            que.LeftJoin(qrLine)
                .On(que.ItemID == qrLine.SRPatientEducation & qrLine.RegistrationNo == registrationNo & qrLine.SeqNo == seqNo);

            que.Where(que.StandardReferenceID == "PatientEducation");

            que.Where(que.ReferenceID.Like(string.Format("{0}%", "ASMNT")));
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, qrLine.EducationNotes, "<CONVERT(BIT,CASE WHEN a.SRPatientEducation IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }

        private static DataTable PatientProcedure(string registrationNo)
        {
            //var ep = new MedicalDischargeSummaryProcedureQuery("ep");
            //ep.Where(ep.RegistrationNo == registrationNo, ep.IsVoid == false);
            //ep.Select(ep.ProcedureID, ep.ProcedureName);
            //ep.OrderBy(ep.ProcedureName.Ascending);
            //return ep.LoadDataTable();

            var ep = new EpisodeProcedureQuery("ep");
            var qp = new ProcedureQuery("qp");

            ep.InnerJoin(qp).On(qp.ProcedureID == ep.ProcedureID);
            ep.Where(ep.RegistrationNo == registrationNo, ep.IsVoid == false);
            ep.Select(ep.ProcedureID, qp.ProcedureName, ep.ProcedureSynonym);
            ep.OrderBy(qp.ProcedureName.Ascending);
            return ep.LoadDataTable();
        }

        private static List<ControlPlanItemExt> ControlPlanExtItems(string controlPlan)
        {
            var itemPlans = new List<ControlPlanItemExt>();
            //if (controlPlan == null)
            //{
            //    itemPlans.Add(new ControlPlanItemExt());
            //}
            if (string.IsNullOrWhiteSpace(controlPlan))
            {
                itemPlans.Add(new ControlPlanItemExt());
            }
            else
            {
                // ServiceUnitName
                var controlPlans = JsonConvert.DeserializeObject<Temiang.Avicenna.BusinessObject.JsonField.ControlPlan>(controlPlan).Items;
                foreach (var item in controlPlans)
                {

                    var planExt = new ControlPlanItemExt();
                    planExt.ControlPlanDateTime = item.ControlPlanDateTime;
                    planExt.ParamedicName = item.ParamedicName;
                    planExt.ParamedicID = item.ParamedicID;
                    planExt.ServiceUnitID = item.ServiceUnitID;
                    planExt.SpecialtyName = item.SpecialtyName;
                    planExt.AppointmentNo = item.AppointmentNo;
                    planExt.AppointmentQue = item.AppointmentQue;
                    planExt.AppointmentTime = item.AppointmentTime;

                    if (!string.IsNullOrWhiteSpace(item.ServiceUnitID))
                    {
                        var su = new ServiceUnit();
                        su.LoadByPrimaryKey(item.ServiceUnitID);
                        planExt.ServiceUnitName = su.ServiceUnitName;
                    }

                    itemPlans.Add(planExt);
                }
            }
            return itemPlans;
        }
    }
}