using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.ReportDataSource.Common;

namespace Temiang.Avicenna.ReportDataSource.RSMM.Emr
{
    /// <summary>
    /// Summary description for Assessment
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PatientFormAndLetter : BaseDataService
    {
        private bool ValidateParameterAndLoadPhr(ref string accessKey, ref string registrationNo, ref string transactionNo,
            ref PatientHealthRecord phr, ref PatientHealthRecordLineCollection phrLines, ref Patient pat, ref Registration reg)
        {
            accessKey = FixParameter(accessKey);
            registrationNo = FixParameter(registrationNo);
            transactionNo = FixParameter(transactionNo);

            var phrQuery = new PatientHealthRecordQuery("a");
            phrQuery.Where(phrQuery.RegistrationNo == registrationNo, phrQuery.TransactionNo == transactionNo);
            phrQuery.es.Top = 1;
            phr = new PatientHealthRecord();
            phr.Load(phrQuery);

            var phrLineQuery = new PatientHealthRecordLineQuery("a");
            phrLineQuery.Where(phrLineQuery.RegistrationNo == registrationNo, phrLineQuery.TransactionNo == transactionNo);
            phrLines = new PatientHealthRecordLineCollection();
            phrLines.Load(phrLineQuery);

            reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            return true;
        }

        #region Surat
        /// <summary>
        /// SURAT KETERANGAN KEMATIAN
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RM19E(string accessKey, string p_RegistrationNo, string p_TransactionNo)
        {
            try
            {
                var reg = new Registration();
                var pat = new Patient();
                var phr = new PatientHealthRecord();
                var phrLines = new PatientHealthRecordLineCollection();

                if (ValidateParameterAndLoadPhr(ref accessKey, ref p_RegistrationNo, ref p_TransactionNo, ref phr, ref phrLines, ref pat, ref reg))
                {
                    var strb = new StringBuilder();
                    strb.AppendFormat("Yang bertanda tangan di bawah ini, menerangkan bahwa pada hari ini {0}", Util.DayName(phr.RecordDate.Value, true));
                    strb.AppendLine(String.Empty);

                    var deadDate = Convert.ToDateTime(phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.DeadDate).QuestionAnswerText);
                    strb.AppendFormat("tanggal {0} pukul {1} WIT, telah meninggal dunia di Rumah Sakit Mitra Masyarakat, ", deadDate.ToString(AppConstant.DisplayFormat.DateShortMonth), deadDate.ToString("HH:mm"));
                    strb.AppendLine(String.Empty);


                    strb.AppendFormat("seorang bernama {4}, umur {0} tahun / {1} bulan / {2} hari, yang dilahirkan di {3}", reg.AgeInYear, reg.AgeInMonth, reg.AgeInDay, pat.CityOfBirth, pat.PatientName);
                    strb.AppendLine(String.Empty);

                    if (!string.IsNullOrWhiteSpace(pat.ParentSpouseName))
                        strb.AppendFormat("{0} dari {1}, anak {2} dari {3} dan yang bertempat tinggal di {4}", pat.Sex == "F" ? "istri" : "suami", pat.ParentSpouseName, pat.Sex == "F" ? "perempuan" : "laki-laki", pat.MotherName, pat.Address);
                    else
                        strb.AppendFormat("suami /istri dari -, anak {0} dari {1} dan yang bertempat tinggal di {2}", pat.Sex == "F" ? "perempuan" : "laki-laki", pat.MotherName, pat.Address);

                    var additionalField = new
                    {
                        NamaDokter = ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName,
                        LetterContent = strb.ToString(),
                        reg.DeathCertificateNo,
                        DeadDate = deadDate.ToString(AppConstant.DisplayFormat.DateShortMonth),
                        DeadTime = deadDate.ToString("HH:mm"),
                        LetterDate = phr.RecordDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth)
                    };

                    var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);

                    ResponseWrite(retField);
                }
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
        }


        /// <summary>
        /// SURAT KETERANGAN KEMATIAN
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeadLetter(string accessKey, string p_RegistrationNo, string p_TransactionNo)
        {
            try
            {
                var reg = new Registration();
                var pat = new Patient();
                var phr = new PatientHealthRecord();
                var phrLines = new PatientHealthRecordLineCollection();

                if (ValidateParameterAndLoadPhr(ref accessKey, ref p_RegistrationNo, ref p_TransactionNo, ref phr, ref phrLines, ref pat, ref reg))
                {
                    var deadDate = Convert.ToDateTime(phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.DeadDate).QuestionAnswerText);
                    var namaPasangan = phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.SpouseName).QuestionAnswerText;

                    var additionalField = new
                    {
                        NamaHari = Util.DayName(deadDate, true),
                        Tanggal = deadDate.ToString(AppConstant.DisplayFormat.DateShortMonth),
                        Pukul = deadDate.ToString("HH:mm"),
                        NamaRS = Healthcare.GetHealthcareName(),
                        SuIs = string.IsNullOrWhiteSpace(namaPasangan) ? "suami / istri" :
                            pat.Sex == "F" ? "istri" : "suami",
                        NamaSuIs = namaPasangan,
                        NamaAyah = phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.FatherName).QuestionAnswerText,
                        NamaIbu = phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.MotherName).QuestionAnswerText,
                        KotaKelahiran = phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.CityOfBirth).QuestionAnswerText,
                        reg.DeathCertificateNo,
                        DeadDate = deadDate.ToString(AppConstant.DisplayFormat.DateShortMonth),
                        DeadTime = deadDate.ToString("HH:mm"),
                        LetterDate = phr.RecordDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth)
                    };

                    var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);

                    ResponseWrite(retField);
                }
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
        }

        /// <summary>
        /// BERITA ACARA PENYERAHAN JENAZAH
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void CorpseLetter(string accessKey, string p_RegistrationNo, string p_TransactionNo)
        {
            try
            {
                var reg = new Registration();
                var pat = new Patient();
                var phr = new PatientHealthRecord();
                var phrLines = new PatientHealthRecordLineCollection();

                if (ValidateParameterAndLoadPhr(ref accessKey, ref p_RegistrationNo, ref p_TransactionNo, ref phr, ref phrLines, ref pat, ref reg))
                {
                    var createDate = phr.RecordDate.Value;
                    var deadDate = Convert.ToDateTime(phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.DeadDate).QuestionAnswerText);

                    var additionalField = new
                    {
                        Tanggal = createDate.ToString(AppConstant.DisplayFormat.DateShortMonth),
                        Pukul = phr.RecordTime,
                        NamaHari = Util.DayName(createDate, true),
                        TanggalMeninggal = deadDate.ToString(AppConstant.DisplayFormat.DateShortMonth),
                        PukulMeninggal = deadDate.ToString("HH:mm"),

                        NamaPenerima = phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.CorpseToName).QuestionAnswerText,
                        NoIdentitasPenerima = phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.CorpseToID).QuestionAnswerText,
                        AlamatPenerima = phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.CorpseToAddress).QuestionAnswerText,
                        HubunganKeluarga = phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.CorpseToRelationship).QuestionAnswerText,


                        CreateByUserName = AppUser.GetUserName(phr.CreateByUserID)
                    };

                    var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);

                    ResponseWrite(retField);
                }
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
        }
        /// <summary>
        /// SURAT SAKIT
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SickLetter(string accessKey, string p_RegistrationNo, string p_TransactionNo)
        {
            try
            {
                var reg = new Registration();
                var healthcare = new Healthcare();
                healthcare.LoadByPrimaryKey(AppSession.Parameter.HealthcareID);
                var pat = new Patient();
                var phr = new PatientHealthRecord();
                var phrLines = new PatientHealthRecordLineCollection();

                if (ValidateParameterAndLoadPhr(ref accessKey, ref p_RegistrationNo, ref p_TransactionNo, ref phr, ref phrLines, ref pat, ref reg))
                {

                    var fromDate = Convert.ToDateTime(phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.FromDate)
                        .QuestionAnswerText);
                    var toDate = Convert.ToDateTime(phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.ToDate)
                        .QuestionAnswerText);
                    var jmlHari = (toDate.AddDays(1) - fromDate).Days;
                    var additionalField = new
                    {
                        DariTanggal = fromDate.ToString(AppConstant.DisplayFormat.DateShortMonth),
                        SampaiTanggal = toDate.ToString(AppConstant.DisplayFormat.DateShortMonth),
                        JumlahHari = jmlHari,
                        Tglberobat = reg.RegistrationDate,
                        DenganHuruf = (new Temiang.Avicenna.Common.Convertion()).NumericToWordsWithoutCurrency(jmlHari),
                        Catatan = phrLines.FindInSingleGroup(PhrUtil.PatientLetterQuestion.Note).QuestionAnswerText,
                    };

                    var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);
                    ResponseWrite(retField);
                }
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
        }
        #endregion

        #region Resume Medis
        private List<ControlPlanItemExt> ControlPlanExtItems(string controlPlan)
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalDischargeSummaryInPatient(string accessKey, string p_RegistrationNo, string p_IsForEsign = "No", string p_IsForCasemix = "0")
        {
            accessKey = FixParameter(accessKey);
            var registrationNo = FixParameter(p_RegistrationNo);

            try
            {
                int LengthOfStay;

                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);
                {
                    var x = reg.DischargeDate != null ? reg.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
                    var y = reg.RegistrationDate.Value.Date;

                    double v = (x - y).TotalDays == 0 ? 1 : (x - y).TotalDays + 1;
                    LengthOfStay = (int)v;

                }

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);

                var kelas = new Class();
                kelas.LoadByPrimaryKey(reg.ClassID);

                var medsum = new MedicalDischargeSummary();
                if (p_IsForCasemix == "1")
                    medsum.Query.es.QuerySource = "MedicalDischargeSummaryCmx";

                medsum.LoadByPrimaryKey(registrationNo);

                if (p_IsForCasemix == "1") //Harus dikembalikan karena kalau tidak akan selalu menggunakan setingan terakhir
                    medsum.Query.es.QuerySource = "MedicalDischargeSummary";

                //var signimg = new AppUser();
                //signimg.LoadByPrimaryKey(medsum.LastUpdateByUserID);

                var signImg = SignParamedic(registrationNo).Rows.Count >= 1 ? SignParamedic(registrationNo).Rows[0]["SignatureImage"] : null;

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(reg.GuarantorID);

                var mainDiag = MdsDiagnoseMain(registrationNo, p_IsForCasemix);

                var usersign = SignUser(registrationNo, p_IsForCasemix);

                var signParamedic = SignParamedic(registrationNo).Rows.Count > 1 ? SignParamedic(registrationNo).Rows[0]["SignatureImage"] : null;

                var referralName = string.Empty;
                var referralAddress = string.Empty;
                var stdi = new AppStandardReferenceItem();
                stdi.LoadByPrimaryKey("ReferralGroup", reg.SRReferralGroup);
                if (stdi.ReferenceID.Contains("dokter") || stdi.ReferenceID.Contains("bidan"))
                {
                    referralName = reg.ReferralName;
                    referralAddress = "Tempat";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(reg.ReferralID))
                    {
                        var rfr = new Referral();
                        rfr.LoadByPrimaryKey(reg.ReferralID);
                        referralName = reg.ReferralName;
                        referralAddress = rfr.ReferralName;
                    }
                }

                var dcDate = medsum.DischargeDate.Value;
                var dcTime = medsum.DischargeTime.Split(':');
                var mergeRegs = Registration.RelatedRegistrations(registrationNo);
                var dtbLastVs = VitalSign.VitalSignLastValue(registrationNo, mergeRegs, true, new DateTime(dcDate.Year, dcDate.Month, dcDate.Day, dcTime[0].ToInt(), dcTime[1].ToInt(), 0));


                // Control Plan
                var plan = new MedicalDischargeSummaryByNurse();
                if (p_IsForCasemix == "1")
                    plan.Query.es.QuerySource = "MedicalDischargeSummaryCmx";
                plan.LoadByPrimaryKey(registrationNo);

                if (p_IsForCasemix == "1") //Harus dikembalikan karena kalau tidak akan selalu menggunakan setingan terakhir
                    plan.Query.es.QuerySource = "MedicalDischargeSummaryByNurse";

                // Healthcare
                var hc = new Healthcare();
                hc.LoadByPrimaryKey(AppSession.Parameter.HealthcareID);


                // Ppa Sign
                var url = HttpContext.Current.Request.Url;
                var segLength = url.Segments.Length;
                var subUrl = string.Empty;
                for (int i = 0; i < segLength - 2; i++)
                {
                    subUrl = string.Concat(subUrl, url.Segments[i]);
                }

                var ppaSignUrl = string.Concat(url.Scheme, "://", url.Authority, subUrl,
                    string.Format("ImgHandler.ashx?type=mdsppasign&regno={0}&csmix={1}", medsum.RegistrationNo, p_IsForCasemix));

                // Patient Sign
                var patientSignUrl = string.Concat(url.Scheme, "://", url.Authority, subUrl,
                    string.Format("ImgHandler.ashx?type=mdspatientsign&regno={0}&csmix={1}", medsum.RegistrationNo, p_IsForCasemix));

                // ParamedicTeam
                //var pt = new ParamedicTeamQuery();
                //pt.Where(pt.RegistrationNo == registrationNo);

                var emrct = new PatientEmergencyContact();
                emrct.LoadByPrimaryKey(pat.PatientID);

                var refer = new ReferExternal();
                refer.LoadByPrimaryKey(registrationNo);

                // DPJP Sign
                var userDpjpsign = DpjpSignUser(registrationNo);

                DataRow row = userDpjpsign.Rows[0];
                byte[] imgDpjp = row["SignatureImage"] as byte[];
                imgDpjp = imgDpjp ?? new byte[0];


                //var rel = new AppStandardReferenceItem();
                //rel.LoadByPrimaryKey("RelationShip", emrct.SRRelationship);

                var additionalField = new
                {
                    ReferralName = referralName,
                    ReferralAddress = referralAddress,
                    NamaDokter = Paramedic.GetParamedicName(medsum.ParamedicID), //ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName,
                    //DokterTeam = Paramedic.GetParamedicName()
                    DokterTeam = ConvertDataTabletoObject(ParamedicTeamWithDpjpAtFirstLine(registrationNo)),
                    TglPulang = string.Format("{0:dd-MMM-yyyy}", reg.DischargeDate ?? medsum.DischargeDate),
                    SukuBangsa = StandardReference.GetItemName(AppEnum.StandardReference.Nationality, pat.SRNationality),
                    GolDarah = StandardReference.GetItemName(AppEnum.StandardReference.BloodType, pat.SRBloodType),

                    Indikasi = RichTextValidation(medsum.TreatmentIndications, medsum.IsRichTextMode),
                    ChiefComplaint = RichTextValidation(medsum.ChiefComplaint, medsum.IsRichTextMode),
                    Anamnesis = RichTextValidation(medsum.HistOfPresentIllness, medsum.IsRichTextMode),
                    Komorbiditas = RichTextValidation(medsum.PastMedicalHistory, medsum.IsRichTextMode),
                    Fisik = RichTextValidation(medsum.PhysicalExam, medsum.IsRichTextMode),
                    Penunjang = RichTextValidation(medsum.AncillaryExam, medsum.IsRichTextMode),
                    MedicalProcedures = RichTextValidation(medsum.MedicalProcedures, medsum.IsRichTextMode),
                    DiagnosaAkhir = RichTextValidation(mainDiag.DiagnosisText, medsum.IsRichTextMode),
                    DianosaIdAkhir = mainDiag.DiagnoseID,
                    DiagnosaSinonim = mainDiag.DiagnoseSynonym,
                    DiagnosaSekunder = ConvertDataTabletoObject(MdsDiagnoseOther(registrationNo, p_IsForCasemix)),
                    DiagnosaAwal = ConvertDataTabletoObject(DiagnoseAwal(reg.RegistrationNo, reg.FromRegistrationNo)),
                    Operasi = ConvertDataTabletoObject(Procedure(registrationNo, p_IsForCasemix)),
                    LamaRawat = LengthOfStay,
                    Medications = RichTextValidation(medsum.Medications, medsum.IsRichTextMode),
                    KondisiPulang = StandardReference.GetItemName(AppEnum.StandardReference.DischargeCondition, medsum.SRDischargeCondition),
                    SuggestionFollowUp = RichTextValidation(medsum.SuggestionFollowUp, medsum.IsRichTextMode),
                    AlasanPulang = StandardReference.GetItemName(AppEnum.StandardReference.DischargeMethod, medsum.SRDischargeMethod),
                    HomeMedications = ConvertDataTabletoObject(HomeMedication(reg.RegistrationNo, reg.FromRegistrationNo)),
                    Prognosis = RichTextValidation(medsum.Prognosis, medsum.IsRichTextMode),
                    PenjaminBayar = guar.GuarantorName,
                    ProcedureName = medsum.ProcedureName,
                    HistoryOfPresentIllness = medsum.HistOfPresentIllness,
                    VitalSign = ConvertDataTabletoObject(dtbLastVs),
                    NextControlPlan = ControlPlanExtItems(plan.ControlPlan),
                    followup = medsum.SuggestionFollowUp,
                    DokterTeamm = ConvertDataTabletoObject(DokterTeam(reg.RegistrationNo)),
                    AncillaryExamOther = RichTextValidation(medsum.AncillaryExamOther, medsum.IsRichTextMode),
                    Diet = RichTextValidation(medsum.Diet, medsum.IsRichTextMode),
                    EmergencyContact = emrct.ContactName,
                    Relation = StandardReference.GetItemName(AppEnum.StandardReference.Relationship, emrct.SRRelationship),
                    EmployeeNo = pat.EmployeeNo,
                    GuarantorCard = pat.GuarantorCardNo,
                    PatientInType = StandardReference.GetItemName(AppEnum.StandardReference.PatientInType, reg.SRPatientInType),
                    smf = StandardReference.GetItemName(AppEnum.StandardReference.ParamedicRL1, par.SRParamedicRL1),
                    kelas.ClassName,
                    refer.OtherInformation,
                    //PpaSignUrl = ppaSignUrl,
                    //PpaSignUrl = signParamedic != null ? signParamedic : string.Empty,
                    PpaSignUrl = !string.IsNullOrEmpty(Convert.ToString(signParamedic)) ? Encoding.UTF8.GetBytes(Convert.ToString(signParamedic)) : medsum.PpaSign,
                    PatientSignUrl = patientSignUrl,
                    HealthcareLogo = hc != null && hc.HealthcareLogo != null ? Convert.ToBase64String(hc.HealthcareLogo) : String.Empty,
                    SignDescription = p_IsForEsign == "Yes" ? "* Dokumen ini telah ditandatangani secara elektronik menggunakan sertifikat elektronik yang diterbitkan oleh BSrE BSSN" : "* Dokumen dicetak secara komputerisasi dan tidak memerlukan tandatangan",
                    //SignatureImage = hc != null && signimg.SignatureImage != null ? Convert.ToBase64String(signimg.SignatureImage) : string.Empty,
                    SignatureImage = hc != null && signImg != null ? signImg : null,
                    UserSign = usersign,
                    ReferExternal = ConvertDataTabletoObject(ReferExternal(medsum.RegistrationNo, p_IsForCasemix)),
                    homecare = medsum.HomeCare,
                    EducationAtHome = medsum.EducationAtHome,
                    Consultation = medsum.Consul,
                    MedicalSupport = medsum.MedicalSupport,
                    InLocation = medsum.InLocation,
                    CollectionDateTime = medsum.CollectionDateTime,
                    InitialDiagnose = medsum.InitialDiagnose,
                    ExternalCause = ConvertDataTabletoObject(ExternalCause(registrationNo)),
                    DpjpName = par.ParamedicName,
                    DpjpSign = Convert.ToBase64String(imgDpjp)

                };

                var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);
                ResponseWrite(retField);
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message + " " + ex.StackTrace);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalDischargeSummaryInPatientBak(string accessKey, string p_RegistrationNo, string p_IsForEsign = "No")
        {
            accessKey = FixParameter(accessKey);
            var registrationNo = FixParameter(p_RegistrationNo);

            try
            {
                int LengthOfStay;

                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);
                {
                    var x = reg.DischargeDate != null ? reg.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
                    var y = reg.RegistrationDate.Value.Date;

                    double v = (x - y).TotalDays == 0 ? 1 : (x - y).TotalDays + 1;
                    LengthOfStay = (int)v;

                }

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);


                var medsum = new MedicalDischargeSummaryBak();
                medsum.LoadByPrimaryKey(registrationNo);

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(reg.GuarantorID);

                //var mergeRegs = MergeBilling.GetFullMergeRegistration(registrationNo, reg.PatientID);
                var mergeRegs = Registration.RelatedRegistrations(registrationNo);

                var mainDiag = DiagnoseMainBak(registrationNo, mergeRegs);

                var referralName = string.Empty;
                var referralAddress = string.Empty;
                var stdi = new AppStandardReferenceItem();
                stdi.LoadByPrimaryKey("ReferralGroup", reg.SRReferralGroup);
                if (stdi.ReferenceID.Contains("dokter") || stdi.ReferenceID.Contains("bidan"))
                {
                    referralName = reg.ReferralName;
                    referralAddress = "Tempat";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(reg.ReferralID))
                    {
                        var rfr = new Referral();
                        rfr.LoadByPrimaryKey(reg.ReferralID);
                        referralName = reg.ReferralName;
                        referralAddress = rfr.ReferralName;
                    }
                }

                var dcDate = medsum.DischargeDate.Value;
                var dcTime = medsum.DischargeTime.Split(':');
                var dtbLastVs = VitalSign.VitalSignLastValue(registrationNo, mergeRegs, true, new DateTime(dcDate.Year, dcDate.Month, dcDate.Day, dcTime[0].ToInt(), dcTime[1].ToInt(), 0));




                // Control Plan
                var plan = new MedicalDischargeSummaryByNurse();
                plan.LoadByPrimaryKey(registrationNo);

                // Healthcare
                var hc = new Healthcare();
                hc.LoadByPrimaryKey(AppSession.Parameter.HealthcareID);


                // Ppa Sign
                var url = HttpContext.Current.Request.Url;
                var segLength = url.Segments.Length;
                var subUrl = string.Empty;
                for (int i = 0; i < segLength - 2; i++)
                {
                    subUrl = string.Concat(subUrl, url.Segments[i]);
                }

                var ppaSignUrl = string.Concat(url.Scheme, "://", url.Authority, subUrl,
                    string.Format("ImgHandler.ashx?type=mdsppasign&regno={0}", medsum.RegistrationNo));

                // ParamedicTeam
                //var pt = new ParamedicTeamQuery();
                //pt.Where(pt.RegistrationNo == registrationNo);

                var emrct = new PatientEmergencyContact();
                emrct.LoadByPrimaryKey(pat.PatientID);


                //var rel = new AppStandardReferenceItem();
                //rel.LoadByPrimaryKey("RelationShip", emrct.SRRelationship);

                var additionalField = new
                {
                    ReferralName = referralName,
                    ReferralAddress = referralAddress,
                    NamaDokter = Paramedic.GetParamedicName(medsum.ParamedicID), //ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName,
                    //DokterTeam = Paramedic.GetParamedicName()
                    DokterTeam = ConvertDataTabletoObject(ParamedicTeamWithDpjpAtFirstLine(registrationNo)),
                    TglPulang = string.Format("{0:dd-MMM-yyyy}", reg.DischargeDate ?? medsum.DischargeDate),
                    SukuBangsa = StandardReference.GetItemName(AppEnum.StandardReference.Nationality, pat.SRNationality),
                    GolDarah = StandardReference.GetItemName(AppEnum.StandardReference.BloodType, pat.SRBloodType),

                    Indikasi = RichTextValidation(medsum.TreatmentIndications, medsum.IsRichTextMode),
                    ChiefComplaint = RichTextValidation(medsum.ChiefComplaint, medsum.IsRichTextMode),
                    Anamnesis = RichTextValidation(medsum.HistOfPresentIllness, medsum.IsRichTextMode),
                    Komorbiditas = RichTextValidation(medsum.PastMedicalHistory, medsum.IsRichTextMode),
                    Fisik = RichTextValidation(medsum.PhysicalExam, medsum.IsRichTextMode),
                    Penunjang = RichTextValidation(medsum.AncillaryExam, medsum.IsRichTextMode),
                    MedicalProcedures = RichTextValidation(medsum.MedicalProcedures, medsum.IsRichTextMode),
                    DiagnosaAkhir = RichTextValidation(mainDiag.DiagnosisText, medsum.IsRichTextMode),
                    DianosaIdAkhir = mainDiag.DiagnoseID,
                    DiagnosaSekunder = ConvertDataTabletoObject(DiagnoseOtherBak(registrationNo, mergeRegs)),
                    Operasi = ConvertDataTabletoObject(ProcedureBak(registrationNo)),
                    LamaRawat = LengthOfStay,
                    Medications = RichTextValidation(medsum.Medications, medsum.IsRichTextMode),
                    KondisiPulang = StandardReference.GetItemName(AppEnum.StandardReference.DischargeCondition, medsum.SRDischargeCondition),
                    SuggestionFollowUp = RichTextValidation(medsum.SuggestionFollowUp, medsum.IsRichTextMode),
                    AlasanPulang = StandardReference.GetItemName(AppEnum.StandardReference.DischargeMethod, medsum.SRDischargeMethod),
                    HomeMedications = ConvertDataTabletoObject(HomeMedication(reg.RegistrationNo, reg.FromRegistrationNo)),
                    Prognosis = RichTextValidation(medsum.Prognosis, medsum.IsRichTextMode),
                    PenjaminBayar = guar.GuarantorName,
                    ProcedureName = medsum.ProcedureName,
                    HistoryOfPresentIllness = medsum.HistOfPresentIllness,
                    VitalSign = ConvertDataTabletoObject(dtbLastVs),
                    NextControlPlan = ControlPlanExtItems(plan.ControlPlan),
                    followup = medsum.SuggestionFollowUp,
                    DokterTeamm = ConvertDataTabletoObject(DokterTeam(reg.RegistrationNo)),
                    AncillaryExamOther = RichTextValidation(medsum.AncillaryExamOther, medsum.IsRichTextMode),
                    EmergencyContact = emrct.ContactName,
                    Relation = StandardReference.GetItemName(AppEnum.StandardReference.Relationship, emrct.SRRelationship),
                    EmployeeNo = pat.EmployeeNo,
                    GuarantorCard = pat.GuarantorCardNo,
                    PatientInType = StandardReference.GetItemName(AppEnum.StandardReference.PatientInType, reg.SRPatientInType),
                    smf = StandardReference.GetItemName(AppEnum.StandardReference.ParamedicRL1, par.SRParamedicRL1),
                    PpaSignUrl = ppaSignUrl,
                    HealthcareLogo = hc != null && hc.HealthcareLogo != null ? Convert.ToBase64String(hc.HealthcareLogo) : String.Empty,
                    SignDescription = p_IsForEsign == "Yes" ? "* Dokumen ini telah ditandatangani secara elektronik menggunakan sertifikat elektronik yang diterbitkan oleh BSrE BSSN" : "* Dokumen dicetak secara komputerisasi dan tidak memerlukan tandatangan"
                };

                var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);
                ResponseWrite(retField);
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message + " " + ex.StackTrace);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalDischargeSummaryEmPatient(string accessKey, string p_RegistrationNo, string p_IsForCasemix = "0")
        {
            accessKey = FixParameter(accessKey);
            var registrationNo = FixParameter(p_RegistrationNo);

            try
            {
                //int LengthOfStay;

                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);
                //{
                //    var x = reg.DischargeDate != null ? reg.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
                //    var y = reg.RegistrationDate.Value.Date;

                //    double v = (x - y).TotalDays == 0 ? 1 : (x - y).TotalDays + 1;
                //    LengthOfStay = (int)v;

                //}

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);


                var medsum = new MedicalDischargeSummary();
                medsum.LoadByPrimaryKey(registrationNo);

                var signimg = new AppUser();
                signimg.LoadByPrimaryKey(medsum.LastUpdateByUserID);

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(reg.GuarantorID);

                var mainDiag = MdsDiagnoseMain(registrationNo, p_IsForCasemix);

                var UserSign = SignUser(registrationNo, "0");

                var referralName = string.Empty;
                var referralAddress = string.Empty;
                var stdi = new AppStandardReferenceItem();
                stdi.LoadByPrimaryKey("ReferralGroup", reg.SRReferralGroup);
                if (stdi.ReferenceID.Contains("dokter") || stdi.ReferenceID.Contains("bidan"))
                {
                    referralName = reg.ReferralName;
                    referralAddress = "Tempat";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(reg.ReferralID))
                    {
                        var rfr = new Referral();
                        rfr.LoadByPrimaryKey(reg.ReferralID);
                        referralName = reg.ReferralName;
                        referralAddress = rfr.ReferralName;
                    }
                }

                //Refer External
                var refer = new ReferExternal();
                refer.LoadByPrimaryKey(registrationNo);


                var dcDate = medsum.DischargeDate.Value;
                var dcTime = medsum.DischargeTime.Split(':');
                var mergeRegs = Registration.RelatedRegistrations(registrationNo);
                var dtbLastVs = VitalSign.VitalSignLastValue(registrationNo, mergeRegs, true, new DateTime(dcDate.Year, dcDate.Month, dcDate.Day, dcTime[0].ToInt(), dcTime[1].ToInt(), 0));



                // Control Plan
                var plan = new MedicalDischargeSummaryByNurse();
                plan.LoadByPrimaryKey(registrationNo);

                // Healthcare
                var hc = new Healthcare();
                hc.LoadByPrimaryKey(AppSession.Parameter.HealthcareID);


                var emrct = new PatientEmergencyContact();
                emrct.LoadByPrimaryKey(pat.PatientID);

                var signParamedic = SignParamedic(registrationNo).Rows.Count > 1 ? SignParamedic(registrationNo).Rows[0]["SignatureImage"] : null;

                //var rel = new AppStandardReferenceItem();
                //rel.LoadByPrimaryKey("RelationShip", emrct.SRRelationship);
                //// Ppa Sign
                //var url = HttpContext.Current.Request.Url;
                //var segLength = url.Segments.Length;
                //var subUrl = string.Empty;
                //for (int i = 0; i < segLength - 2; i++)
                //{
                //    subUrl = string.Concat(subUrl, url.Segments[i]);
                //}

                //var ppaSignUrl = string.Concat(url.Scheme, "://", url.Authority, subUrl,
                //    string.Format("ImgHandler.ashx?type=mdsppasign&regno={0}", medsum.RegistrationNo));

                // DPJP Sign
                var userDpjpsign = DpjpSignUser(registrationNo);

                DataRow row = userDpjpsign.Rows[0];
                byte[] imgDpjp = row["SignatureImage"] as byte[];
                imgDpjp = imgDpjp ?? new byte[0];


                var additionalField = new
                {
                    ReferralName = referralName,
                    ReferralAddress = referralAddress,
                    NamaDokter = Paramedic.GetParamedicName(medsum.ParamedicID), //ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName,
                    TglPulang = string.Format("{0:dd-MMM-yyyy}", reg.DischargeDate ?? medsum.DischargeDate),
                    JamPulang = string.Format("{0:HH:mm}", reg.DischargeTime ?? medsum.DischargeTime),

                    Indikasi = RichTextValidation(medsum.TreatmentIndications, medsum.IsRichTextMode),
                    ChiefComplaint = RichTextValidation(medsum.ChiefComplaint, medsum.IsRichTextMode),
                    Anamnesis = RichTextValidation(medsum.HistOfPresentIllness, medsum.IsRichTextMode),
                    Komorbiditas = RichTextValidation(medsum.PastMedicalHistory, medsum.IsRichTextMode),
                    Fisik = RichTextValidation(medsum.PhysicalExam, medsum.IsRichTextMode),
                    Penunjang = RichTextValidation(medsum.AncillaryExam, medsum.IsRichTextMode),
                    DiagnosaAkhir = RichTextValidation(mainDiag.DiagnosisText, medsum.IsRichTextMode),
                    DianosaIdAkhir = mainDiag.DiagnoseID,
                    DiagnosaSinonim = mainDiag.DiagnoseSynonym,
                    DiagnosaSekunder = ConvertDataTabletoObject(DiagnoseOther(registrationNo, mergeRegs)),
                    Operasi = ConvertDataTabletoObject(Procedure(registrationNo, p_IsForCasemix)),
                    Medications = RichTextValidation(medsum.Medications, medsum.IsRichTextMode),
                    KondisiPulang = StandardReference.GetItemName(AppEnum.StandardReference.DischargeCondition, medsum.SRDischargeCondition),
                    SuggestionFollowUp = RichTextValidation(medsum.SuggestionFollowUp, medsum.IsRichTextMode),
                    AlasanPulang = StandardReference.GetItemName(AppEnum.StandardReference.DischargeMethod, medsum.SRDischargeMethod),
                    HomeMedications = ConvertDataTabletoObject(HomeMedication(reg.RegistrationNo, reg.FromRegistrationNo)),
                    Prognosis = RichTextValidation(medsum.Prognosis, medsum.IsRichTextMode),
                    PenjaminBayar = guar.GuarantorName,
                    ProcedureName = medsum.ProcedureName,
                    HistoryOfPresentIllness = medsum.HistOfPresentIllness,
                    VitalSign = ConvertDataTabletoObject(dtbLastVs),
                    NextControlPlan = ControlPlanExtItems(plan.ControlPlan),
                    followup = medsum.SuggestionFollowUp,
                    DokterTeamm = ConvertDataTabletoObject(DokterTeam(reg.RegistrationNo)),
                    AncillaryExamOther = RichTextValidation(medsum.AncillaryExamOther, medsum.IsRichTextMode),
                    HealthcareLogo = Convert.ToBase64String(hc.HealthcareLogo),
                    EmergencyContact = emrct.ContactName,
                    EmployeeNo = pat.EmployeeNo,
                    GuarantorCard = pat.GuarantorCardNo,
                    PatientInType = StandardReference.GetItemName(AppEnum.StandardReference.PatientInType, reg.SRPatientInType),
                    Relation = StandardReference.GetItemName(AppEnum.StandardReference.Relationship, emrct.SRRelationship),
                    smf = StandardReference.GetItemName(AppEnum.StandardReference.ParamedicRL1, par.SRParamedicRL1),
                    //PpaSignUrl = signParamedic != null ? signParamedic : string.Empty,
                    PpaSignUrl = !string.IsNullOrEmpty(Convert.ToString(signParamedic)) ? Encoding.UTF8.GetBytes(Convert.ToString(signParamedic)) : medsum.PpaSign,
                    SignatureImage = hc != null && signimg.SignatureImage != null ? Convert.ToBase64String(signimg.SignatureImage) : string.Empty,
                    education = ConvertDataTabletoObject(PatientEducation(registrationNo)),
                    UnitIntended = StandardReference.GetItemName(AppEnum.StandardReference.UnitIntended, medsum.SRUnitIntended),
                    UserSign = UserSign,
                    ReferExternal = ConvertDataTabletoObject(ReferExternal(medsum.RegistrationNo, p_IsForCasemix)),
                    DpjpName = par.ParamedicName,
                    DpjpSign = Convert.ToBase64String(imgDpjp)
                };

                var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);
                ResponseWrite(retField);
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message + " " + ex.StackTrace);
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MedicalDischargeSummaryOutPatient(string accessKey, string p_RegistrationNo, string p_IsForCasemix)
        {
            accessKey = FixParameter(accessKey);
            var registrationNo = FixParameter(p_RegistrationNo);

            try
            {


                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);

                var kelas = new Class();
                kelas.LoadByPrimaryKey(reg.ClassID);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);

                var medsum = new MedicalDischargeSummary();
                if (p_IsForCasemix == "1")
                    medsum.Query.es.QuerySource = "MedicalDischargeSummaryCmx";

                medsum.LoadByPrimaryKey(registrationNo);

                if (p_IsForCasemix == "1") //Harus dikembalikan karena kalau tidak akan selalu menggunakan setingan terakhir
                    medsum.Query.es.QuerySource = "MedicalDischargeSummary";

                var signimg = new AppUser();
                signimg.LoadByPrimaryKey(medsum.LastUpdateByUserID);

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(reg.GuarantorID);

                var mainDiag = MdsDiagnoseMain(registrationNo, p_IsForCasemix);



                var dcDate = medsum.DischargeDate.Value;
                var dcTime = medsum.DischargeTime.Split(':');
                var mergeRegs = Registration.RelatedRegistrations(registrationNo);
                var dtbLastVs = VitalSign.VitalSignLastValue(registrationNo, mergeRegs, true, new DateTime(dcDate.Year, dcDate.Month, dcDate.Day, dcTime[0].ToInt(), dcTime[1].ToInt(), 0));


                // Control Plan
                var plan = new MedicalDischargeSummaryByNurse();
                if (p_IsForCasemix == "1")
                    plan.Query.es.QuerySource = "MedicalDischargeSummaryCmx";

                plan.LoadByPrimaryKey(registrationNo);

                if (p_IsForCasemix == "1") //Harus dikembalikan karena kalau tidak akan selalu menggunakan setingan terakhir
                    plan.Query.es.QuerySource = "MedicalDischargeSummaryByNurse";

                // Healthcare
                var hc = new Healthcare();
                hc.LoadByPrimaryKey(AppSession.Parameter.HealthcareID);

                var emrct = new PatientEmergencyContact();
                emrct.LoadByPrimaryKey(pat.PatientID);

                //var rel = new AppStandardReferenceItem();
                //rel.LoadByPrimaryKey("RelationShip", emrct.SRRelationship);

                // Ppa Sign
                var url = HttpContext.Current.Request.Url;
                var segLength = url.Segments.Length;
                var subUrl = string.Empty;
                for (int i = 0; i < segLength - 2; i++)
                {
                    subUrl = string.Concat(subUrl, url.Segments[i]);
                }

                var ppaSignUrl = string.Concat(url.Scheme, "://", url.Authority, subUrl,
                    string.Format("ImgHandler.ashx?type=mdsppasign&regno={0}&csmix={1}", medsum.RegistrationNo, p_IsForCasemix));

                //Refer External
                var refer = new ReferExternal();
                refer.LoadByPrimaryKey(registrationNo);

                var signParamedic = SignParamedic(registrationNo).Rows.Count > 1 ? SignParamedic(registrationNo).Rows[0]["SignatureImage"] : null;

                // DPJP Sign
                var userDpjpsign = DpjpSignUser(registrationNo);

                DataRow row = userDpjpsign.Rows[0];
                byte[] imgDpjp = row["SignatureImage"] as byte[];
                imgDpjp = imgDpjp ?? new byte[0];


                var additionalField = new
                {

                    NamaDokter = Paramedic.GetParamedicName(medsum.ParamedicID), //ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName,
                    TglPulang = string.Format("{0:dd-MMM-yyyy}", reg.DischargeDate ?? medsum.DischargeDate),

                    Indikasi = RichTextValidation(medsum.TreatmentIndications, medsum.IsRichTextMode),
                    ChiefComplaint = RichTextValidation(medsum.ChiefComplaint, medsum.IsRichTextMode),
                    Anamnesis = RichTextValidation(medsum.HistOfPresentIllness, medsum.IsRichTextMode),
                    Komorbiditas = RichTextValidation(medsum.PastMedicalHistory, medsum.IsRichTextMode),
                    Fisik = RichTextValidation(medsum.PhysicalExam, medsum.IsRichTextMode),
                    Penunjang = RichTextValidation(medsum.AncillaryExam, medsum.IsRichTextMode),
                    MedicalProcedures = RichTextValidation(medsum.MedicalProcedures, medsum.IsRichTextMode),
                    DiagnosaAkhir = RichTextValidation(mainDiag.DiagnosisText, medsum.IsRichTextMode),
                    DianosaIdAkhir = mainDiag.DiagnoseID,
                    DiagnosaSinonim = mainDiag.DiagnoseSynonym,
                    kelas.ClassName,
                    refer.OtherInformation,
                    //DiagnosaSekunder = ConvertDataTabletoObject(DiagnoseOther(registrationNo, mergeRegs)), //Request RSI : disamakan dengan inpatient. (Fajri 2023/06/26)
                    DiagnosaSekunder = ConvertDataTabletoObject(MdsDiagnoseOther(registrationNo, p_IsForCasemix)),
                    Operasi = ConvertDataTabletoObject(Procedure(registrationNo, p_IsForCasemix)),
                    Medications = RichTextValidation(medsum.Medications, medsum.IsRichTextMode),
                    KondisiPulang = StandardReference.GetItemName(AppEnum.StandardReference.DischargeCondition, medsum.SRDischargeCondition),
                    SuggestionFollowUp = RichTextValidation(medsum.SuggestionFollowUp, medsum.IsRichTextMode),
                    AlasanPulang = StandardReference.GetItemName(AppEnum.StandardReference.DischargeMethod, medsum.SRDischargeMethod),
                    HomeMedications = ConvertDataTabletoObject(HomeMedication(reg.RegistrationNo, reg.FromRegistrationNo)),
                    Prognosis = RichTextValidation(medsum.Prognosis, medsum.IsRichTextMode),
                    PenjaminBayar = guar.GuarantorName,
                    ProcedureName = medsum.ProcedureName,
                    HistoryOfPresentIllness = medsum.HistOfPresentIllness,
                    VitalSign = ConvertDataTabletoObject(dtbLastVs),
                    NextControlPlan = ControlPlanExtItems(plan.ControlPlan),
                    followup = medsum.SuggestionFollowUp,
                    //HealthcareLogo = Convert.ToBase64String(hc.HealthcareLogo),
                    HealthcareLogo = hc != null && hc.HealthcareLogo != null ? Convert.ToBase64String(hc.HealthcareLogo) : String.Empty,
                    DokterTeamm = ConvertDataTabletoObject(DokterTeam(reg.RegistrationNo)),
                    Diet = RichTextValidation(medsum.Diet, medsum.IsRichTextMode),
                    AncillaryExamOther = RichTextValidation(medsum.AncillaryExamOther, medsum.IsRichTextMode),
                    EmergencyContact = emrct.ContactName,
                    Relation = StandardReference.GetItemName(AppEnum.StandardReference.Relationship, emrct.SRRelationship),
                    EmployeeNo = pat.EmployeeNo,
                    GuarantorCard = pat.GuarantorCardNo,
                    PatientInType = StandardReference.GetItemName(AppEnum.StandardReference.PatientInType, reg.SRPatientInType),
                    smf = StandardReference.GetItemName(AppEnum.StandardReference.ParamedicRL1, par.SRParamedicRL1),
                    //PpaSignUrl = ppaSignUrl,
                    //PpaSignUrl = signParamedic != null ? signParamedic : string.Empty,
                    PpaSignUrl = !string.IsNullOrEmpty(Convert.ToString(signParamedic)) ? Encoding.UTF8.GetBytes(Convert.ToString(signParamedic)) : medsum.PpaSign,
                    SignatureImage = hc != null && signimg.SignatureImage != null ? Convert.ToBase64String(signimg.SignatureImage) : string.Empty,
                    PatientAssessment = ConvertDataTabletoObject(PatientAssessment(registrationNo)),
                    education = ConvertDataTabletoObject(PatientEducation(registrationNo)),
                    UnitIntended = StandardReference.GetItemName(AppEnum.StandardReference.UnitIntended, medsum.SRUnitIntended),
                    Dpjp = par.ParamedicName,
                    DpjpSign = Convert.ToBase64String(imgDpjp)

                };

                var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);
                ResponseWrite(retField);
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message + " " + ex.StackTrace);
            }
        }


        /// <summary>
        /// Tambahan Form Verification INACBGS untuk keperluan ke BPJS
        /// </summary>
        /// Create By: Fajri
        /// Create Date: 2023-March-20
        /// Clinet Req: RSYS
        /// <param name="accessKey"></param>
        /// <param name="p_RegistrationNo"></param>
        /// <param name="p_IsForEsign"></param>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void FormVerificationINACBGS(string accessKey, string p_RegistrationNo, string p_IsForEsign = "No")
        {
            accessKey = FixParameter(accessKey);
            var registrationNo = FixParameter(p_RegistrationNo);

            try
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                var medsum = new MedicalDischargeSummary();
                medsum.LoadByPrimaryKey(registrationNo);

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(reg.GuarantorID);

                var mainDiag = MdsDiagnoseMain(registrationNo, "0");

                // Ppa Sign
                var url = HttpContext.Current.Request.Url;
                var segLength = url.Segments.Length;
                var subUrl = string.Empty;
                for (int i = 0; i < segLength - 2; i++)
                {
                    subUrl = string.Concat(subUrl, url.Segments[i]);
                }

                var ppaSignUrl = string.Concat(url.Scheme, "://", url.Authority, subUrl,
                    string.Format("ImgHandler.ashx?type=mdsppasign&regno={0}", medsum.RegistrationNo));

                // Patient Sign
                var patientSignUrl = string.Concat(url.Scheme, "://", url.Authority, subUrl,
                    string.Format("ImgHandler.ashx?type=mdspatientsign&regno={0}", medsum.RegistrationNo));

                var additionalField = new
                {
                    NamaDokter = Paramedic.GetParamedicName(medsum.ParamedicID),
                    DiagnosaAkhir = RichTextValidation(mainDiag.DiagnosisText, medsum.IsRichTextMode),
                    DianosaIdAkhir = mainDiag.DiagnoseID,
                    DiagnosaSekunder = ConvertDataTabletoObject(MdsDiagnoseOther(registrationNo, "0")),
                    DiagnosaLengkap = ConvertDataTabletoObject(DiagnoseINACBGS(registrationNo)),
                    Operasi = ConvertDataTabletoObject(Procedure(registrationNo, "0")),
                    SuggestionFollowUp = RichTextValidation(medsum.SuggestionFollowUp, medsum.IsRichTextMode),
                    ProcedureName = medsum.ProcedureName,
                    FormatTgl = DateTime.Now.ToString(AppConstant.DisplayFormat.Date2, new CultureInfo("id-ID")),
                    PpaSignUrl = ppaSignUrl,
                    PatientSignUrl = patientSignUrl,
                    SignDescription = p_IsForEsign == "Yes" ? "* Dokumen ini telah ditandatangani secara elektronik menggunakan sertifikat elektronik yang diterbitkan oleh BSrE BSSN" : "* Dokumen dicetak secara komputerisasi dan tidak memerlukan tandatangan"
                };

                var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);
                ResponseWrite(retField);
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message + " " + ex.StackTrace);
            }
        }

        private static string RichTextValidation(string richText, bool? isRichTextMode)
        {
            if (string.IsNullOrWhiteSpace(richText)) return string.Empty;
            return (isRichTextMode ?? false) ? richText : HttpUtility.HtmlEncode(richText).Replace("\r\n", "<br/>");
        }

        private class Diagnose
        {
            public string DiagnoseID { get; set; }
            public string DiagnosisText { get; set; }
            public string DiagnoseSynonym { get; set; }
        }
        private Diagnose MdsDiagnoseMain(string registrationNo, string p_IsForCasemix)
        {

            var diagQr = new MedicalDischargeSummaryDiagnoseQuery("ep");
            if (p_IsForCasemix == "1")
                diagQr.es.QuerySource = "MedicalDischargeSummaryDiagnoseCmx";

            diagQr.Where(diagQr.RegistrationNo == registrationNo, diagQr.IsVoid == false, diagQr.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            diagQr.es.Top = 1;
            diagQr.OrderBy(diagQr.CreatedDateTime.Descending);

            var diag = new MedicalDischargeSummaryDiagnose();
            if (p_IsForCasemix == "1")
                diag.Query.es.QuerySource = "MedicalDischargeSummaryDiagnoseCmx";

            if (diag.Load(diagQr))
            {
                if (p_IsForCasemix == "1") //Harus dikembalikan karena kalau tidak akan selalu menggunakan setingan terakhir
                {
                    diagQr.es.QuerySource = "MedicalDischargeSummaryDiagnose";
                    diag.Query.es.QuerySource = "MedicalDischargeSummaryDiagnose";
                }

                return new Diagnose() { DiagnoseID = diag.DiagnoseID, DiagnosisText = diag.DiagnosisText, DiagnoseSynonym = diag.DiagnoseSynonym };
            }

            if (p_IsForCasemix == "1") //Harus dikembalikan karena kalau tidak akan selalu menggunakan setingan terakhir
            {
                diagQr.es.QuerySource = "MedicalDischargeSummaryDiagnose";
                diag.Query.es.QuerySource = "MedicalDischargeSummaryDiagnose";
            }
            return new Diagnose() { DiagnoseID = string.Empty, DiagnosisText = string.Empty, DiagnoseSynonym = string.Empty };

            //// Ambil dari merge reg
            //diagQr = new MedicalDischargeSummaryDiagnoseQuery("ep");
            //diagQr.Where(diagQr.RegistrationNo.In(mergeRegs), diagQr.IsVoid == false, diagQr.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            //diagQr.es.Top = 1;
            //diagQr.OrderBy(diagQr.CreatedDateTime.Descending);
            //if (diag.Load(diagQr))
            //{
            //    return new Diagnose() { DiagnoseID = diag.DiagnoseID, DiagnosisText = diag.DiagnosisText };
            //}

            //// Data versi app sebelumnya disimpan di EpisodeDiagnose diperlukan sebelum data diimport ke MedicalDischargeSummaryDiagnose

            //var ep = new EpisodeDiagnoseQuery("ep");
            //ep.Where(ep.RegistrationNo == registrationNo, ep.IsVoid == false, ep.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            //ep.es.Top = 1;
            //ep.OrderBy(ep.CreateDateTime.Descending);

            //var ent = new EpisodeDiagnose();
            //if (ent.Load(ep))
            //{
            //    return new Diagnose() { DiagnoseID = ent.DiagnoseID, DiagnosisText = ent.DiagnosisText };
            //}
            //// Ambil dari merge reg
            //ep = new EpisodeDiagnoseQuery("ep");
            //ep.Where(ep.RegistrationNo.In(mergeRegs), ep.IsVoid == false, ep.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            //ep.es.Top = 1;
            //ep.OrderBy(ep.CreateDateTime.Descending);
            //ent.Load(ep);

            //return new Diagnose() { DiagnoseID = ent.DiagnoseID, DiagnosisText = ent.DiagnosisText };
        }

        /// <summary>
        /// Diagnosa Sekunder versi Medical Discharge Summary
        /// </summary>
        /// <param name="registrationNo"></param>
        /// <param name="mergeRegs"></param>
        /// <returns></returns>
        /// Create by: Handono (23 April 04)
        private DataTable MdsDiagnoseOther(string registrationNo, string p_IsForCasemix)
        {
            var diag = new MedicalDischargeSummaryDiagnoseQuery("diag");
            if (p_IsForCasemix == "1")
                diag.es.QuerySource = "MedicalDischargeSummaryDiagnoseCmx";

            diag.Where(diag.RegistrationNo == registrationNo, diag.IsVoid == false, diag.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            diag.Select(diag.DiagnoseID, diag.DiagnosisText, diag.SRDiagnoseType, diag.DiagnoseSynonym);
            diag.OrderBy(diag.DiagnosisText.Ascending);
            var dtbDiag = diag.LoadDataTable();

            if (p_IsForCasemix == "1") //Harus dikembalikan karena kalau tidak akan selalu menggunakan setingan terakhir
            {
                diag.es.QuerySource = "MedicalDischargeSummaryDiagnose";
            }
            return dtbDiag;
        }

        private Diagnose DiagnoseMainBak(string registrationNo, List<string> mergeRegs)
        {

            //var diagQr = new MedicalDischargeSummaryDiagnoseQuery("ep");
            var diagQr = new MedicalDischargeSummaryDiagnoseBakQuery("ep");

            diagQr.Where(diagQr.RegistrationNo == registrationNo, diagQr.IsVoid == false, diagQr.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            diagQr.es.Top = 1;
            diagQr.OrderBy(diagQr.CreatedDateTime.Descending);

            //var diag = new MedicalDischargeSummaryDiagnose();
            var diag = new MedicalDischargeSummaryDiagnoseBak();
            if (diag.Load(diagQr))
            {
                return new Diagnose() { DiagnoseID = diag.DiagnoseID, DiagnosisText = diag.DiagnosisText };

            }
            // Ambil dari merge reg
            //diagQr = new MedicalDischargeSummaryDiagnoseQuery("ep");
            diagQr = new MedicalDischargeSummaryDiagnoseBakQuery("ep");
            diagQr.Where(diagQr.RegistrationNo.In(mergeRegs), diagQr.IsVoid == false, diagQr.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            diagQr.es.Top = 1;
            diagQr.OrderBy(diagQr.CreatedDateTime.Descending);
            if (diag.Load(diagQr))
            {
                return new Diagnose() { DiagnoseID = diag.DiagnoseID, DiagnosisText = diag.DiagnosisText };
            }

            // Data versi app sebelumnya disimpan di EpisodeDiagnose diperlukan sebelum data diimport ke MedicalDischargeSummaryDiagnose

            var ep = new EpisodeDiagnoseQuery("ep");
            ep.Where(ep.RegistrationNo == registrationNo, ep.IsVoid == false, ep.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            ep.es.Top = 1;
            ep.OrderBy(ep.CreateDateTime.Descending);

            var ent = new EpisodeDiagnose();
            if (ent.Load(ep))
            {
                return new Diagnose() { DiagnoseID = ent.DiagnoseID, DiagnosisText = ent.DiagnosisText };
            }
            // Ambil dari merge reg
            ep = new EpisodeDiagnoseQuery("ep");
            ep.Where(ep.RegistrationNo.In(mergeRegs), ep.IsVoid == false, ep.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            ep.es.Top = 1;
            ep.OrderBy(ep.CreateDateTime.Descending);
            ent.Load(ep);

            return new Diagnose() { DiagnoseID = ent.DiagnoseID, DiagnosisText = ent.DiagnosisText };
        }
        private DataTable DiagnoseOther(string registrationNo, List<string> mergeRegs)
        {
            //Data versi app sebelumnya disimpan di EpisodeDiagnose diperlukan sebelum data diimport ke MedicalDischargeSummaryDiagnose
            var ep = new EpisodeDiagnoseQuery("ep");

            ep.Where(ep.RegistrationNo == registrationNo, ep.IsVoid == false, ep.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            ep.Select(ep.DiagnoseID, ep.DiagnosisText, ep.SRDiagnoseType, ep.DiagnoseSynonym);
            ep.GroupBy(ep.DiagnoseID, ep.DiagnosisText, ep.SRDiagnoseType, ep.DiagnoseSynonym);
            ep.OrderBy(ep.DiagnosisText.Ascending);
            var dtb = ep.LoadDataTable();
            if (dtb.Rows.Count > 0) return dtb;

            // Amil dari merge reg
            ep = new EpisodeDiagnoseQuery("ep");

            ep.Where(ep.RegistrationNo.In(mergeRegs), ep.IsVoid == false, ep.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            ep.Select(ep.DiagnoseID, ep.DiagnosisText, ep.SRDiagnoseType, ep.DiagnoseSynonym);
            ep.GroupBy(ep.DiagnoseID, ep.DiagnosisText, ep.SRDiagnoseType, ep.DiagnoseSynonym);
            ep.OrderBy(ep.DiagnosisText.Ascending);
            return ep.LoadDataTable();


            var diag = new MedicalDischargeSummaryDiagnoseQuery("diag");
            diag.Where(diag.RegistrationNo == registrationNo, diag.IsVoid == false, diag.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            diag.Select(diag.DiagnoseID, diag.DiagnosisText, diag.SRDiagnoseType);
            diag.OrderBy(diag.DiagnosisText.Ascending);
            var dtbDiag = diag.LoadDataTable();
            if (dtbDiag.Rows.Count > 0) return dtbDiag;

            // Ambil dari merge reg
            diag = new MedicalDischargeSummaryDiagnoseQuery("diag");
            diag.Where(diag.RegistrationNo.In(mergeRegs), diag.IsVoid == false, diag.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            diag.Select(diag.DiagnoseID, diag.DiagnosisText, diag.SRDiagnoseType);
            diag.OrderBy(diag.DiagnosisText.Ascending);
            var dtbDiagMer = diag.LoadDataTable();
            if (dtbDiagMer != null && dtbDiagMer.Rows.Count > 0)
                return dtbDiagMer;

        }

        private DataTable DiagnoseOtherBak(string registrationNo, List<string> mergeRegs)
        {


            //var diag = new MedicalDischargeSummaryDiagnoseQuery("diag");
            var diag = new MedicalDischargeSummaryDiagnoseBakQuery("diag");
            diag.Where(diag.RegistrationNo == registrationNo, diag.IsVoid == false, diag.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            diag.Select(diag.DiagnoseID, diag.DiagnosisText, diag.SRDiagnoseType);
            diag.OrderBy(diag.DiagnosisText.Ascending);
            var dtbDiag = diag.LoadDataTable();
            if (dtbDiag.Rows.Count > 0) return dtbDiag;

            // Ambil dari merge reg
            //diag = new MedicalDischargeSummaryDiagnoseQuery("diag");
            diag = new MedicalDischargeSummaryDiagnoseBakQuery("diag");
            diag.Where(diag.RegistrationNo.In(mergeRegs), diag.IsVoid == false, diag.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            diag.Select(diag.DiagnoseID, diag.DiagnosisText, diag.SRDiagnoseType);
            diag.OrderBy(diag.DiagnosisText.Ascending);
            var dtbDiagMer = diag.LoadDataTable();
            if (dtbDiagMer != null && dtbDiagMer.Rows.Count > 0)
                return dtbDiagMer;


            // Apip : Ini saya komen karena masih muncul diagnosa lamanya di cetakan surat lepaas rawaat

            //Data versi app sebelumnya disimpan di EpisodeDiagnose diperlukan sebelum data diimport ke MedicalDischargeSummaryDiagnose
            //var ep = new EpisodeDiagnoseQuery("ep");

            //ep.Where(ep.RegistrationNo == registrationNo, ep.IsVoid == false, ep.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            //ep.Select(ep.DiagnoseID, ep.DiagnosisText);
            //ep.GroupBy(ep.DiagnoseID, ep.DiagnosisText);
            //ep.OrderBy(ep.DiagnosisText.Ascending);
            //var dtb = ep.LoadDataTable();
            //if (dtb.Rows.Count > 0) return dtb;

            //// Amil dari merge reg
            //ep = new EpisodeDiagnoseQuery("ep");

            //ep.Where(ep.RegistrationNo.In(mergeRegs), ep.IsVoid == false, ep.SRDiagnoseType != AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            //ep.Select(ep.DiagnoseID, ep.DiagnosisText);
            //ep.GroupBy(ep.DiagnoseID, ep.DiagnosisText);
            //ep.OrderBy(ep.DiagnosisText.Ascending);
            return diag.LoadDataTable();

        }

        private DataTable DiagnoseAwal(string registrationNo, string fromRegistrationNo)
        {
            // Amil dari merge reg
            var ep = new EpisodeDiagnoseQuery("ep");
            //var reg = new RegistrationQuery("reg");

            //ep.InnerJoin(reg).On(ep.RegistrationNo == reg.FromRegistrationNo);

            //if (string.IsNullOrWhiteSpace(fromRegistrationNo))
            //    ep.Where(ep.RegistrationNo == registrationNo);
            //else
            //    ep.Where(ep.Or(ep.RegistrationNo == fromRegistrationNo, ep.RegistrationNo == registrationNo));

            ep.Where(ep.RegistrationNo == fromRegistrationNo, ep.IsVoid == false, ep.SRDiagnoseType == AppParameter.GetParameterValue(AppParameter.ParameterItem.DiagnoseTypeMain));
            ep.Select(ep.DiagnoseID, ep.DiagnosisText, ep.SRDiagnoseType);
            ep.GroupBy(ep.DiagnoseID, ep.DiagnosisText, ep.SRDiagnoseType);
            ep.OrderBy(ep.DiagnosisText.Ascending);
            return ep.LoadDataTable();
        }

        private DataTable Procedure(string registrationNo, string p_IsForCasemix)
        {
            if (p_IsForCasemix == "1")
            {
                var epCmx = new MedicalDischargeSummaryProcedureCmxQuery("ep");
                epCmx.Where(epCmx.RegistrationNo == registrationNo, epCmx.IsVoid == false);
                epCmx.Select(epCmx.ProcedureID, epCmx.ProcedureName, epCmx.ProcedureSynonym);
                epCmx.OrderBy(epCmx.ProcedureName.Ascending);
                return epCmx.LoadDataTable();
            }

            var ep = new MedicalDischargeSummaryProcedureQuery("ep");
            ep.Where(ep.RegistrationNo == registrationNo, ep.IsVoid == false);
            ep.Select(ep.ProcedureID, ep.ProcedureName, ep.ProcedureSynonym);
            ep.OrderBy(ep.ProcedureName.Ascending);
            return ep.LoadDataTable();
        }

        private DataTable ProcedureBak(string registrationNo)
        {
            var ep = new MedicalDischargeSummaryProcedureBakQuery("ep");
            ep.Where(ep.RegistrationNo == registrationNo, ep.IsVoid == false);
            ep.Select(ep.ProcedureID, ep.ProcedureName);
            ep.OrderBy(ep.ProcedureName.Ascending);
            return ep.LoadDataTable();
        }


        //private DataTable ParamedicT(string registrationNo)
        //{
        //    var pt = new ParamedicTeamQuery("pt");
        //    var pnm = new ParamedicQuery("pnm");
        //    pt.InnerJoin(pnm).On(pt.ParamedicID == pnm.ParamedicID);

        //    pt.Where(pt.RegistrationNo == registrationNo);
        //    pt.Select(pt.RegistrationNo, pnm.ParamedicID, pnm.ParamedicName);
        //    pt.OrderBy(pt.RegistrationNo.Ascending);
        //    return pt.LoadDataTable();
        //}

        /// <summary>
        /// Paramedic Team dengan DPJP nya diurutan pertama
        /// </summary>
        /// <param name="registrationNo"></param>
        /// <returns></returns>
        /// Create by: Handono 230320
        /// Client Req: RSYS
        private DataTable ParamedicTeamWithDpjpAtFirstLine(string registrationNo)
        {
            //var dpjpStatus = AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicTeamStatusDpjpID);
            //var pt = new ParamedicTeamQuery("pt");
            //var pnm = new ParamedicQuery("pnm");
            //pt.InnerJoin(pnm).On(pt.ParamedicID == pnm.ParamedicID);

            //pt.Where(pt.RegistrationNo == registrationNo);
            //pt.Select(
            //    string.Format("<CASE WHEN pt.SRParamedicTeamStatus = '{0}' THEN 1 ELSE 2 END as SortStat>", dpjpStatus),
            //    pt.RegistrationNo, pnm.ParamedicID, pnm.ParamedicName);
            //pt.OrderBy("<1>", pt.StartDate.Descending);
            //pt.es.Distinct = true;
            //return pt.LoadDataTable();


            var dpjpStatus = AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicTeamStatusDpjpID);
            var qrPt = new ParamedicTeamQuery("pt");
            qrPt = new ParamedicTeamQuery("pt");
            qrPt.Where(qrPt.RegistrationNo == registrationNo);
            qrPt.Select(string.Format("<CASE WHEN pt.SRParamedicTeamStatus = '{0}' THEN 1 ELSE 2 END as SortStat>", dpjpStatus),
                qrPt.ParamedicID, qrPt.SRParamedicTeamStatus);
            qrPt.OrderBy("<1>", qrPt.StartDate.Descending);

            var dtb = qrPt.LoadDataTable();

            var paramedicIds = string.Empty;

            var dtbResult = dtb.Clone();
            dtbResult.Columns.Add("ParamedicName", typeof(string));
            foreach (DataRow row in dtb.Rows)
            {
                var parId = row["ParamedicID"].ToString();

                //Skip jika ParamedicID sudah ada
                if (paramedicIds.Contains(parId)) continue;

                var qrPar = new ParamedicQuery("p");
                qrPar.Select(qrPar.ParamedicID, qrPar.ParamedicName);
                qrPar.Where(qrPar.ParamedicID == parId);
                var medic = new Paramedic();
                if (medic.Load(qrPar))
                {
                    paramedicIds = string.Concat(paramedicIds, ";", parId);
                    var newRow = dtbResult.NewRow();
                    newRow["ParamedicID"] = row["ParamedicID"];
                    newRow["ParamedicName"] = medic.ParamedicName;
                    dtbResult.Rows.Add(newRow);
                }
            }

            return dtbResult;
        }

        private DataTable HomeMedication(string registrationNo, string fromRegistrationNo)
        {
            var mr = new MedicationReceiveQuery("mr");
            var cm = new ConsumeMethodQuery("cm");
            mr.InnerJoin(cm).On(mr.SRConsumeMethod == cm.SRConsumeMethod);

            if (string.IsNullOrWhiteSpace(fromRegistrationNo))
                mr.Where(mr.RegistrationNo == registrationNo);
            else
                mr.Where(mr.Or(mr.RegistrationNo == fromRegistrationNo, mr.RegistrationNo == registrationNo));

            //mr.Where(mr.IsBroughtHome == true, mr.BalanceQty > 0, mr.SRMedicationConsume != "");
            mr.Where(mr.IsBroughtHome == true, mr.BalanceQty > 0); //Ac Pc nya diabaikan
            mr.Select(mr.ItemDescription, mr.BalanceQty, cm.SRConsumeMethodName, mr.ConsumeQty, mr.SRConsumeUnit, mr.Note, mr.RefTransactionNo, mr.RefSequenceNo);
            mr.OrderBy(mr.MedicationReceiveNo.Descending);
            var dtb = mr.LoadDataTable();
            dtb.Columns.Add("ConsumeMethod", typeof(string));

            foreach (DataRow row in dtb.Rows)
            {
                row["ConsumeMethod"] = string.Format("{0} {1} {2}", row["SRConsumeMethodName"], row["ConsumeQty"],
                    row["SRConsumeUnit"]);

                var itemDescription = row["ItemDescription"].ToString();
                if (row["RefTransactionNo"] != DBNull.Value && !string.IsNullOrEmpty((row["RefTransactionNo"]).ToString()))
                {

                    // Ambil ulang item descriptionnya spy tanpa format HTML
                    try
                    {
                        row["ItemDescription"] = ItemDescription(row["RefTransactionNo"].ToString(),
                            row["RefSequenceNo"].ToString());

                    }
                    catch (Exception e)
                    {
                        row["ItemDescription"] = itemDescription;
                    }



                }
            }

            return dtb;
        }

        private DataTable MedicationReceiveDataTable(string registrationNo, string fromRegistrationNo)
        {
            var query = new MedicationReceiveQuery("a");
            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var patrec = new MedicationReceiveFromPatientQuery("b");
            query.LeftJoin(patrec).On(query.MedicationReceiveNo == patrec.MedicationReceiveNo);

            query.Select(query, patrec.Condition, patrec.ExpireDate, patrec.ApprovedByParamedicID, patrec.LastConsumeDateTime, cm.SRConsumeMethodName,
                query.IsBroughtHome.As("IsBroughtHomeOri"));

            query.Where(query.IsVoid != true,
                query.BalanceQty > 0,
                query.IsContinue == true,
                query.Or(query.RegistrationNo == fromRegistrationNo, query.RegistrationNo == registrationNo));

            query.Where(query.IsBroughtHome == true);

            query.OrderBy(query.MedicationReceiveNo.Descending);
            var dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                row["ItemDescription"] = row["ItemDescription"].ToString().Replace(Environment.NewLine, "<br>");
            }

            return dtb;
        }

        private string ItemDescription(string prescriptionNo, string sequenceNo)
        {
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemMedic = new ItemProductMedicQuery("im");
            var qItemIntervention = new ItemQuery("c");

            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.InnerJoin(qItemMedic).On(query.ItemID == qItemMedic.ItemID);
            query.LeftJoin(qItemIntervention).On(query.ItemInterventionID == qItemIntervention.ItemID);

            query.Select
            (
                query.ItemInterventionID,
                query.IsCompound,
                qItem.ItemName,
                qItemIntervention.ItemName.Coalesce("''").As("ItemNameIntervention")
            );

            query.Where(query.PrescriptionNo == prescriptionNo, query.SequenceNo == sequenceNo);

            var dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0 && dtb.Rows[0]["IsCompound"].ToBoolean() == true)
            {
                // Racikan
                query = new TransPrescriptionItemQuery("a");
                qItem = new ItemQuery("b");
                qItemMedic = new ItemProductMedicQuery("im");
                qItemIntervention = new ItemQuery("c");

                query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
                query.InnerJoin(qItemMedic).On(query.ItemID == qItemMedic.ItemID);
                query.LeftJoin(qItemIntervention).On(query.ItemInterventionID == qItemIntervention.ItemID);

                query.Select
                (
                    query.ItemInterventionID, query.ParentNo, query.IsRFlag,
                    qItem.ItemName, query.DosageQty, query.SRDosageUnit,
                    qItemIntervention.ItemName.Coalesce("''").As("ItemNameIntervention")
                );

                query.Where(query.PrescriptionNo == prescriptionNo, query.Or(query.SequenceNo == sequenceNo, query.ParentNo == sequenceNo));
                query.OrderBy(query.SequenceNo.Ascending);

                dtb = query.LoadDataTable();
                var sbItem = new StringBuilder();
                foreach (DataRow row in dtb.Rows)
                {
                    var itemName = row["ItemName"].ToString();


                    if (row["ItemInterventionID"] != DBNull.Value &&
                        !string.IsNullOrEmpty(row["ItemInterventionID"].ToString()))
                    {
                        itemName = row["ItemNameIntervention"].ToString();
                    }

                    if (row["ParentNo"] != DBNull.Value && string.IsNullOrEmpty(row["ParentNo"].ToString()))
                    {
                        //Header
                        sbItem = new StringBuilder();
                        sbItem.AppendFormat("{0} {1} @ {2} {3}{4}",
                            Convert.ToBoolean(row["IsRFlag"])
                                ? "R/ "
                                : string.Empty, itemName, row["DosageQty"], row["SRDosageUnit"], Environment.NewLine);

                    }
                    else
                    {
                        sbItem.AppendFormat("{0} {1} @ {2} {3}{4}",
                            Convert.ToBoolean(row["IsRFlag"])
                                ? "R/ "
                                : string.Empty,
                            itemName, row["DosageQty"], row["SRDosageUnit"], Environment.NewLine);

                    }
                }
                return sbItem.ToString();

            }

            // Obat Paten
            var rowPaten = dtb.Rows[0];
            if (rowPaten["ItemInterventionID"] != DBNull.Value &&
                    !string.IsNullOrEmpty(rowPaten["ItemInterventionID"].ToString()))
            {
                return rowPaten["ItemNameIntervention"].ToString();
            }

            return rowPaten["ItemName"].ToString();
        }

        private DataTable PatientEducation(string registrationNo)
        {
            var pe = new PatientEducationQuery("pe");
            var pa = new PatientAssessmentQuery("pa");
            var pel = new PatientEducationLineQuery("pel");

            var Method = new AppStandardReferenceItemQuery("Method");
            var Recipient = new AppStandardReferenceItemQuery("Recipient");
            var Evaluation = new AppStandardReferenceItemQuery("Evaluation");
            var Goal = new AppStandardReferenceItemQuery("Goal");
            var asri = new AppStandardReferenceItemQuery("asri");



            pe.InnerJoin(pa).On(pe.ReferenceNo == pa.RegistrationInfoMedicID);
            pe.InnerJoin(pel).On(pe.RegistrationNo == pel.RegistrationNo);
            pe.LeftJoin(Method).On(pe.SRPatientEducationMethod == Method.ItemID & Method.StandardReferenceID == "PatientEducationMethod");
            pe.LeftJoin(Recipient).On(pe.SRPatientEducationRecipient == Recipient.ItemID & Recipient.StandardReferenceID == "PatientEducationRecipient");
            pe.LeftJoin(Evaluation).On(pe.SRPatientEducationEvaluation == Evaluation.ItemID & Evaluation.StandardReferenceID == "PatientEducationEvaluation");
            pe.LeftJoin(Goal).On(pe.SRPatientEducationGoal == Goal.ItemID & Goal.StandardReferenceID == "PatientEducationGoal");
            pe.LeftJoin(asri).On(pel.SRPatientEducation == asri.ItemID & asri.StandardReferenceID == "PatientEducation");



            pe.Where(pe.RegistrationNo == registrationNo);
            pe.Select(Method.ItemName.As("EducationMethod"), pe.MethodOther,
                Recipient.ItemName.As("EducationRecipient"), pe.RecipientName,
                Evaluation.ItemName.As("EducationEvaluation"), pe.PatientEducationEvaluationOth,
                pe.Duration,
                Goal.ItemName.As("PatientEducationGoal"), pe.PatientEducationGoalOth,
                asri.ItemName.As("PatientEducation"), pel.EducationNotes
                );


            var dtb = pe.LoadDataTable();
            return dtb;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ReferralReply(string accessKey, string registrationNo)
        {
            accessKey = FixParameter(accessKey);
            registrationNo = FixParameter(registrationNo);

            try
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                var medsum = new MedicalDischargeSummary();
                medsum.LoadByPrimaryKey(registrationNo);

                var referralName = string.Empty;
                var referralAddress = string.Empty;
                var stdi = new AppStandardReferenceItem();
                stdi.LoadByPrimaryKey("ReferralGroup", reg.SRReferralGroup);
                if (stdi.ReferenceID.Contains("dokter") || stdi.ReferenceID.Contains("bidan"))
                {
                    referralName = reg.ReferralName;
                    referralAddress = "Tempat";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(reg.ReferralID))
                    {
                        var rfr = new Referral();
                        rfr.LoadByPrimaryKey(reg.ReferralID);
                        referralName = reg.ReferralName;
                        referralAddress = rfr.ReferralName;
                    }
                }

                var additionalField = new
                {
                    ReferralName = referralName,
                    ReferralAddress = referralAddress,
                    TglPulang = string.Format("{0:dd-MMM-yyyy}", medsum.DischargeDate),
                    NamaDokter = ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName,

                    Indikasi = medsum.TreatmentIndications,
                    ChiefComplaint = medsum.ChiefComplaint,
                    Anamnesis = medsum.HistOfPresentIllness,
                    PastMedicalHist = medsum.PastMedicalHistory,
                    PhysicalExam = medsum.PhysicalExam,
                    AncillaryExam = medsum.AncillaryExam,
                    MedicalProcedures = medsum.MedicalProcedures.Replace("â€¢", "\u2022"),
                    Medications = medsum.Medications.Replace("â€¢", "\u2022"),
                    KondisiPulang = StandardReference.GetItemName(AppEnum.StandardReference.DischargeCondition, medsum.SRDischargeCondition),
                    SuggestionFollowUp = medsum.SuggestionFollowUp,
                    AlasanPulang = StandardReference.GetItemName(AppEnum.StandardReference.DischargeMethod, medsum.SRDischargeMethod),
                    HomeMedications = ConvertDataTabletoObject(HomeMedication(reg.RegistrationNo, reg.FromRegistrationNo))
                };

                var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);
                ResponseWrite(retField);
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message + " " + ex.StackTrace);
            }
        }


        private DataTable DokterTeam(string registrationNo)
        {

            //Dokter Team
            var dtr = new ParamedicTeamQuery("a");
            var parmedic = new ParamedicQuery("b");
            dtr.InnerJoin(parmedic).On(dtr.ParamedicID == parmedic.ParamedicID);

            dtr.Where(dtr.RegistrationNo == registrationNo);
            dtr.Select(parmedic.ParamedicID, parmedic.ParamedicName);
            dtr.OrderBy(parmedic.ParamedicName.Ascending);
            return dtr.LoadDataTable();
        }

        private DataTable DiagnoseINACBGS(string registrationNo)
        {
            var diag = new MedicalDischargeSummaryDiagnoseQuery("diag");
            diag.Where(diag.RegistrationNo == registrationNo, diag.IsVoid == false);
            diag.Select(diag.DiagnoseID, diag.DiagnosisText);
            diag.OrderBy(diag.SRDiagnoseType.Ascending);
            var dtbDiag = diag.LoadDataTable();
            return dtbDiag;

            //// Ambil dari merge reg
            //diag = new MedicalDischargeSummaryDiagnoseQuery("diag");
            //diag.Where(diag.RegistrationNo.In(mergeRegs), diag.IsVoid == false);
            //diag.Select(diag.DiagnoseID, diag.DiagnosisText);
            //diag.OrderBy(diag.SRDiagnoseType.Ascending);
            //return diag.LoadDataTable();
        }

        private DataTable SignUser(string registrationNo, string p_IsForCasemix)
        {
            var au = new AppUserQuery("a");
            var mds = new MedicalDischargeSummaryQuery("b");
            if (p_IsForCasemix == "1")
                mds.es.QuerySource = "MedicalDischargeSummaryCmx";

            var par = new ParamedicQuery("c");
            au.InnerJoin(mds).On(au.ParamedicID == mds.ParamedicID);
            au.InnerJoin(par).On(au.UserName == par.ParamedicName);

            au.Where(mds.RegistrationNo == registrationNo);
            au.Select(au.SignatureImage);
            au.OrderBy(au.UserID.Ascending);
            var dtb = au.LoadDataTable();

            if (p_IsForCasemix == "1") //Harus dikembalikan karena kalau tidak akan selalu menggunakan setingan terakhir
                mds.es.QuerySource = "MedicalDischargeSummary";

            return dtb;
        }

        private DataTable SignParamedic(string registrationNo)
        {
            var au = new AppUserQuery("a");
            var rim = new RegistrationInfoMedicQuery("rim");

            au.InnerJoin(rim).On(au.ParamedicID == rim.ParamedicID);
            au.Where(rim.RegistrationNo == registrationNo, rim.SRMedicalNotesInputType == "MDS", au.UserID == rim.CreatedByUserID);
            au.Select(au.SignatureImage);
            au.OrderBy(au.UserID.Ascending);

            var dt = au.LoadDataTable();

            return dt;
        }

        private DataTable ReferExternal(string registrationNo, string p_IsForCasemix)
        {

            var refExt = new ReferExternalQuery("a");
            if (p_IsForCasemix == "1")
                refExt.es.QuerySource = "ReferExternalCmx";

            var refMast = new ReferralQuery("b");
            var refReason = new AppStandardReferenceItemQuery("c");

            refExt.LeftJoin(refMast).On(refExt.ReferralID == refMast.ReferralID);
            refExt.LeftJoin(refReason).On(refExt.SRReferReason == refReason.ItemID & refReason.StandardReferenceID == "ReferReason");

            refExt.Where(refExt.RegistrationNo == registrationNo);
            refExt.Select(refExt.ReferralID, refMast.ReferralName, refReason.ItemName, refExt.ReferReasonOther, refExt.OtherInformation);
            var dtb = refExt.LoadDataTable();

            if (p_IsForCasemix == "1") //Harus dikembalikan karena kalau tidak akan selalu menggunakan setingan terakhir
                refExt.es.QuerySource = "ReferExternal";
            return dtb;
        }

        private DataTable ExternalCause(string registrationNo)
        {
            // Amil dari merge reg
            var ep = new EpisodeDiagnoseQuery("ep");
            var Dia = new DiagnoseQuery("Dia");
            ep.InnerJoin(Dia).On(ep.ExternalCauseID == Dia.DiagnoseID);

            ep.Where(ep.RegistrationNo == registrationNo, ep.IsVoid == false);
            ep.Select(Dia.DiagnoseID, Dia.DiagnoseName);
            return ep.LoadDataTable();
        }

        private DataTable DpjpSignUser(string registrationNo)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            var au = new AppUserQuery("a");
            au.Where(au.ParamedicID == reg.ParamedicID, au.SignatureImage.IsNotNull());
            au.es.Top = 1;
            au.OrderBy(au.UserID.Ascending);
            au.Select(au.SignatureImage);
            return au.LoadDataTable();
        }

        private DataTable PatientAssessment(string registrationNo)
        {
            var pas = new PatientAssessmentQuery("pas");
            var rim = new RegistrationInfoMedicQuery("rim");
            pas.InnerJoin(rim).On(pas.RegistrationInfoMedicID == rim.RegistrationInfoMedicID);

            pas.Where(pas.RegistrationNo == registrationNo);
            pas.es.Top = 1;
            pas.Select(pas.ToInpatient, pas.ReferTo, pas.ConsulTo, pas.Therapy, rim.Info4);
            pas.OrderBy(pas.PatientEducationSeqNo.Ascending);

            return pas.LoadDataTable();
        }

        #endregion

    }
}
