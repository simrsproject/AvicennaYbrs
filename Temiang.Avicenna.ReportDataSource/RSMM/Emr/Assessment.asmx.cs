using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
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
    public class Assessment : BaseDataService
    {
        #region Shared Method
        private bool ValidateParameterAndLoadAssessment(string accessKey,
            string registrationInfoMedicID, ref PatientAssessment asses, ref Patient pat, ref Registration reg, ref AppUser au)
        {
            accessKey = FixParameter(accessKey);
            registrationInfoMedicID = FixParameter(registrationInfoMedicID);

            asses = new PatientAssessment();
            asses.LoadByPrimaryKey(registrationInfoMedicID);

            reg = new Registration();
            reg.LoadByPrimaryKey(asses.RegistrationNo);

            pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            au = new AppUser();
            au.LoadByPrimaryKey(asses.CreatedByUserID);

            return true;
        }

        /// <summary>
        /// ASESMEN AWAL RAWAT INAP PENYAKIT DALAM
        /// Internal Disease Inpatient Medical Assessment
        /// </summary>
        public object InPatientStd(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs, object peObj, object localist)
        {
            var additionalField = new
            {
                PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value), peObj, localist),
                HandOver = AssessmentUtil.TransferToInPatient(reg.FromRegistrationNo),
                PemeriksaanPenunjang = asses.OtherExam,
                DischargePlan = new
                {
                    LamaRawat = asses.EstimatedDayInPatient,
                    Pulang = (asses.DischargeDatePlan == null ? reg.RegistrationDate.Value.AddDays(asses.EstimatedDayInPatient ?? 1) : asses.DischargeDatePlan.Value).ToString(AppConstant.DisplayFormat.DateShortMonth),
                    TindakanMedis = asses.DischargeMedicalPlan
                },
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg,
                DiagnosaLengkap = Diagnosis(asses),
                PerencanaanPulang = ConvertDataTabletoObject(DischargePlanning(asses.RegistrationInfoMedicID))
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;

        }

        public object InPatientStd(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs,
            object peObj)
        {
            return InPatientStd(asses, pat, reg, au, mergeRegs, peObj, new object());
        }

        private static string LocalistUrl(PatientAssessment asses, string bodyId)
        {
            var url = HttpContext.Current.Request.Url;
            var segLength = url.Segments.Length;
            var subUrl = string.Empty;
            for (int i = 0; i < segLength - 2; i++)
            {
                subUrl = string.Concat(subUrl, url.Segments[i]);
            }

            var localistUrl = string.Concat(url.Scheme, "://", url.Authority, subUrl,
                string.Format("Localist.ashx?rimid={0}&bdid={1}", asses.RegistrationInfoMedicID, bodyId));
            return localistUrl;
        }

        private string TranslateCondition(string condition)
        {
            switch (condition)
            {
                case "Mild":
                    condition = "Ringan";
                    break;
                case "Moderate":
                    condition = "Sedang";
                    break;
                case "Severe":
                    condition = "Berat";
                    break;
                case "DOA":
                    condition = "Meninggal";
                    break;
            }

            return condition;
        }

        #endregion
        /// <summary>
        /// General Clinic Outpatient Initial Assessment 
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="registrationInfoMedicID"></param>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetJson(string accessKey, string registrationInfoMedicID)
        {
            try
            {
                var reg = new Registration();
                var pat = new Patient();
                var asses = new PatientAssessment();
                var au = new AppUser();
                if (ValidateParameterAndLoadAssessment(accessKey, registrationInfoMedicID, ref asses, ref pat, ref reg, ref au))
                {
                    //var mergeRegs = MergeBilling.GetFullMergeRegistration(reg.RegistrationNo, reg.PatientID);
                    var mergeRegs = Registration.RelatedRegistrations(reg.RegistrationNo);

                    object retField = null;
                    switch (asses.SRAssessmentType)
                    {
                        #region Rawat Inap

                        case "IINTR":
                            {
                                var pe = JsonConvert.DeserializeObject<InternalPe>(asses.PhysicalExam);
                                pe.Condition = TranslateCondition(pe.Condition);
                                retField = InPatientStd(asses, pat, reg, au, mergeRegs, pe);
                                break;
                            }
                        case "IHERT":
                            {
                                var pe = JsonConvert.DeserializeObject<HeartPe>(asses.PhysicalExam);
                                pe.Condition = TranslateCondition(pe.Condition);
                                retField = InPatientStd(asses, pat, reg, au, mergeRegs, pe);
                                break;
                            }
                        case "IKID":
                            {
                                retField = KidInPatient(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "ILUNG":
                            {
                                var pe = JsonConvert.DeserializeObject<LungPe>(asses.PhysicalExam);
                                pe.Condition = TranslateCondition(pe.Condition);
                                retField = InPatientStd(asses, pat, reg, au, mergeRegs, pe);
                                break;
                            }
                        case "INEUR":
                            {
                                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSYS")
                                {
                                    var pe = JsonConvert.DeserializeObject<NeurologiPe>(asses.PhysicalExam);
                                    pe.Condition = TranslateCondition(pe.Condition);
                                    retField = InPatientStd(asses, pat, reg, au, mergeRegs, pe);
                                    break;
                                }
                                else
                                {
                                    var pe = JsonConvert.DeserializeObject<NeurologiPeIp>(asses.PhysicalExam);
                                    pe.Condition = TranslateCondition(pe.Condition);
                                    retField = InPatientStd(asses, pat, reg, au, mergeRegs, pe);
                                    break;
                                }
                            }
                        case "IPSYI":
                            {
                                var pe = JsonConvert.DeserializeObject<PsychiatryPe>(asses.PhysicalExam);
                                pe.Condition = TranslateCondition(pe.Condition);
                                retField = InPatientStd(asses, pat, reg, au, mergeRegs, pe);
                                break;
                            }
                        case "ISKIN":
                            {
                                var pe = JsonConvert.DeserializeObject<SkinPe>(asses.PhysicalExam);
                                pe.Condition = TranslateCondition(pe.Condition);
                                retField = InPatientStd(asses, pat, reg, au, mergeRegs, pe);
                                break;
                            }
                        case "ISTRK":
                            {
                                var pe = JsonConvert.DeserializeObject<StrokePe>(asses.PhysicalExam);
                                pe.Condition = TranslateCondition(pe.Condition);
                                retField = InPatientStd(asses, pat, reg, au, mergeRegs, pe);
                                break;
                            }
                        case "ISURG":
                            {
                                retField = SurgicalInPatient(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "ITHT":
                            {
                                retField = ThtInPatient(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "IEYE":
                            {
                                retField = EyeInPatient(asses, pat, reg, au, mergeRegs);
                                break;

                            }
                        case "INEO":
                            {
                                retField = NeonatusInPatient(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "INURS":
                        case "IPKAN":
                            {
                                retField = KebidananInPatient(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "IIAIP": //tambahan untuk General Initial Assesment (Fajri)
                            {
                                var pe = JsonConvert.DeserializeObject<GeneralPe>(asses.PhysicalExam);
                                pe.Condition = TranslateCondition(pe.Condition);
                                retField = GeneralInitialInPatient(asses, pat, reg, au, mergeRegs, pe);
                                break;
                            }
                        #endregion

                        #region Rawat Jalan
                        case "INTER":
                            {
                                var pe = JsonConvert.DeserializeObject<InternalPe>(asses.PhysicalExam);
                                pe.Condition = TranslateCondition(pe.Condition);
                                retField = OutPatientStd(asses, pat, reg, au, mergeRegs, pe, new object());
                                break;
                            }
                        case "GENRL":
                        case "PSYCY":
                        case "PSYCO":
                            {
                                var pe = JsonConvert.DeserializeObject<GeneralPe>(asses.PhysicalExam);
                                pe.Condition = TranslateCondition(pe.Condition);

                                retField = OutPatientStd(asses, pat, reg, au, mergeRegs, pe, new object());
                                break;
                            }
                        case "SURGI":
                            {
                                retField = SurgicalClinic(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "IGD":
                            {
                                // TODO: Ambil info pengantar dimana ?  phr kagak ada ktnye
                                retField = EmergencyClinic(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "EYE":
                            {
                                retField = EyeClinic(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "THT":
                            {
                                retField = ThtClinic(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "KID":
                            {
                                retField = KidClinic(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "DENTS":
                            {
                                retField = DentisClinic(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "NEURO": // tambah baru standar aja
                            {
                                retField = NeurologyClinic(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "SKIN": // tambah baru standar aja
                            {
                                retField = SkinClinic(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "LUNG": // tambah baru standar ajaa
                            {
                                retField = lungClinic(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "HEART": // tambah baru standar ajaa
                            {
                                retField = HEARTClinic(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "REHAB": // tambah baru standar ajaa
                            {
                                retField = REHAB(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "NURSE":
                        case "PKAND":
                            {
                                retField = KebidananClinic(asses, pat, reg, au, mergeRegs);
                                break;
                            }
                        case "CONT":
                            {
                                var rim = new RegistrationInfoMedic();
                                rim.LoadByPrimaryKey(asses.RegistrationInfoMedicID);

                                var oSoa = new
                                {
                                    Subjective = rim.Info1Entry,
                                    Objective = rim.Info2,
                                    Assessment = rim.Info3Entry
                                };

                                var additionalField = new
                                {
                                    PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value)),
                                    PemeriksaanPenunjang = asses.OtherExam,
                                    //SignImg = asses.SignImg,
                                    SignImg = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                                    PatientSignImg = asses.PatientSignImg,
                                };

                                retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                                   AssessmentUtil.PhysicianCommonField(pat, reg, asses), oSoa, additionalField);

                                break;
                            }
                        case "COAT":
                            {
                                var rim = new RegistrationInfoMedic();
                                rim.LoadByPrimaryKey(asses.RegistrationInfoMedicID);

                                var oSoa = new
                                {
                                    Subjective = rim.Info1Entry,
                                    Objective = asses.PhysicalExam,
                                    Assessment = rim.Info3Entry,
                                    Planning = rim.Info4
                                };

                                var additionalField = new
                                {
                                    PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value)),
                                    DiagnosaLengkap = Diagnosis(asses),
                                    PemeriksaanPenunjang = asses.OtherExam
                                };

                                retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                                   AssessmentUtil.PhysicianCommonField(pat, reg, asses), oSoa, additionalField);
                                break;
                            }
                        #endregion

                        default:
                            {
                                var pe = JsonConvert.DeserializeObject<GeneralPe>(asses.PhysicalExam);
                                pe.Condition = TranslateCondition(pe.Condition);
                                if (reg.SRRegistrationType == "IPR")
                                {
                                    retField = InPatientStd(asses, pat, reg, au, mergeRegs, pe);
                                }
                                else
                                {
                                    retField = OutPatientStd(asses, pat, reg, au, mergeRegs, pe, new object());
                                }
                                break;
                            }
                    }

                    if (retField != null)
                        ResponseWrite(retField);
                }
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
        }


        private static void AppendBuilder(string caption, string value, StringBuilder strBuilder)
        {
            if (!string.IsNullOrEmpty(value))
            {
                strBuilder.AppendLine(caption);
                strBuilder.AppendLine(value);
            }
        }

        #region Emergency CLinic Assessment
        private object EmergencyClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var pe = JsonConvert.DeserializeObject<Igd>(asses.PhysicalExam);
            pe.Condition = TranslateCondition(pe.Condition);

            //Diremark dulu, agar bisa filter berdasarkan BodyID (Fajri 15/03/2023)
            //var lanjtnNotes = string.Empty;
            //var localistNotes = string.Empty;
            //var lanjtn = new RegistrationInfoMedicBodyDiagram();
            //if (lanjtn.LoadByPrimaryKey(asses.RegistrationInfoMedicID, "LIGD02"))
            //    lanjtnNotes = lanjtn.Notes;

            var bodyIgd = "LIGD";
            var bodyIgdLanjutan = "LIGD02";
            var localistNotes = string.Empty;
            var lanjtnNotes = string.Empty;
            var lcls = new RegistrationInfoMedicBodyDiagramCollection();
            lcls.Query.Where(lcls.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            lcls.Query.Select(lcls.Query.BodyID, lcls.Query.Notes);
            lcls.Query.Where(lcls.Query.Or(lcls.Query.BodyID == bodyIgd, lcls.Query.BodyID == bodyIgdLanjutan));
            if (lcls.Query.Load())
            {
                foreach (var line in lcls)
                {
                    if (line.BodyID == bodyIgd)
                        localistNotes = line.Notes;
                    else
                        lanjtnNotes = line.Notes;
                }
            }

            var strBuilder = new StringBuilder();
            AppendBuilder("Jalan Nafas:", string.IsNullOrWhiteSpace(pe.JalanNapas.Summary) ? GetSummaryValue(pe.JalanNapas) : pe.JalanNapas.Summary, strBuilder);
            AppendBuilder("Pernafasan:", string.IsNullOrWhiteSpace(pe.Pernapasan.Summary) ? GetSummaryValue(pe.Pernapasan) : pe.Pernapasan.Summary, strBuilder);
            AppendBuilder("Sirkulasi:", string.IsNullOrWhiteSpace(pe.Sirkulasi.Summary) ? GetSummaryValue(pe.Sirkulasi) : pe.Sirkulasi.Summary, strBuilder);
            AppendBuilder("Penilaian Bayi Baru Lahir:", string.IsNullOrWhiteSpace(pe.PenilaianBayi.Summary) ? GetSummaryValue(pe.PenilaianBayi) : pe.PenilaianBayi.Summary, strBuilder);
            AppendBuilder("Disabilitas:", string.IsNullOrWhiteSpace(pe.Disabilitas.Summary) ? GetSummaryValue(pe.Disabilitas) : pe.Disabilitas.Summary, strBuilder);
            AppendBuilder("Exposur:", string.IsNullOrWhiteSpace(pe.Eksposur.Summary) ? GetSummaryValue(pe.Eksposur) : pe.Eksposur.Summary, strBuilder);
            AppendBuilder("Kepala dan Leher:", string.IsNullOrWhiteSpace(pe.KepalaLeher.Summary) ? GetSummaryValue(pe.KepalaLeher) : pe.KepalaLeher.Summary, strBuilder);
            AppendBuilder("Thorax:", string.IsNullOrWhiteSpace(pe.Thorax.Summary) ? GetSummaryValue(pe.Thorax) : pe.Thorax.Summary, strBuilder);
            AppendBuilder("Abdomen & Pelvis:", string.IsNullOrWhiteSpace(pe.AbdomenPelvis.Summary) ? GetSummaryValue(pe.AbdomenPelvis) : pe.AbdomenPelvis.Summary, strBuilder);


            /// <summary>
            /// Tambahan u/ cetakan assessment igd : Triase, Skala nyeri, Flacc, Esi, Subjective, Objective, Diagnose
            /// </summary>
            /// Create By: Fajri
            /// Create Date: 2023-March-07
            /// Clinet Req: RSYS
            var kepala = new
            {
                IsAbnormal = pe.Head.IsAbNormal ?? false,
                Notes = pe.Head.Notes
            };

            var eyeReason = pe.Eye.Reasons ?? new List<string>();
            var mata = new
            {
                IsAbnormal = pe.Eye.IsAbNormal ?? false,
                Notes = new
                {
                    IsAnemia = eyeReason.Contains("ANM"),
                    IsIkterik = eyeReason.Contains("IKT"),
                    IsPupil = eyeReason.FirstOrDefault(strCheck => strCheck.Contains("PPL|")) != null ? true : false,
                    IslainLain = eyeReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")) != null ? true : false,
                    Pupil = (eyeReason.FirstOrDefault(strCheck => strCheck.Contains("PPL|")) != null ? true : false) == true ? eyeReason.FirstOrDefault(strCheck => strCheck.Contains("PPL|")).Split('|')[1] : string.Empty,
                    LainLain = (eyeReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")) != null ? true : false) == true ? eyeReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")).Split('|')[1] : string.Empty
                }
            };

            var leher = new
            {
                IsAbnormal = pe.Neck.IsAbNormal ?? false,
                Notes = pe.Neck.Notes
            };

            var pulmoReason = pe.Pulmo.Reasons ?? new List<string>();
            var paruParu = new
            {
                IsAbnormal = pe.Pulmo.IsAbNormal ?? false,
                Notes = new
                {
                    IsRonki = pulmoReason.Contains("RNK"),
                    IsWheezing = pulmoReason.Contains("WZG"),
                    IsRetraction = pulmoReason.Contains("RTC"),
                    IslainLain = pulmoReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")) != null ? true : false,
                    LainLain = (pulmoReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")) != null ? true : false) == true ? pulmoReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")).Split('|')[1] : string.Empty
                }
            };

            var corReason = pe.Cor.Reasons ?? new List<string>();
            var jantung = new
            {
                IsAbnormal = pe.Cor.IsAbNormal ?? false,
                Notes = new
                {
                    IsGallop = corReason.Contains("GLP"),
                    IsMurmur = corReason.Contains("MMR"),
                    IslainLain = corReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")) != null ? true : false,
                    LainLain = (corReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")) != null ? true : false) == true ? corReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")).Split('|')[1] : string.Empty
                }
            };

            var abdoReason = pe.Abdomen.Reasons ?? new List<string>();
            var perut = new
            {
                IsAbnormal = pe.Abdomen.IsAbNormal ?? false,
                Notes = new
                {
                    IsBisingUsusNaik = abdoReason.Contains("BSU"),
                    IsBisingUsusTurun = abdoReason.Contains("BSD"),
                    IsNyeriTekan = abdoReason.Contains("PPN"),
                    IsHepatomegali = abdoReason.Contains("HEP"),
                    IsSplenomegali = abdoReason.Contains("SPL"),
                    IsNyeriLepas = abdoReason.Contains("RPN"),
                    IslainLain = abdoReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")) != null ? true : false,
                    LainLain = (abdoReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")) != null ? true : false) == true ? abdoReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")).Split('|')[1] : string.Empty
                }
            };

            var extremReason = pe.Extremity.Reasons ?? new List<string>();
            var ekstremitas = new
            {
                IsAbnormal = pe.Extremity.IsAbNormal ?? false,
                Notes = new
                {
                    IsAkralDingin = extremReason.Contains("CAK"),
                    IsPittingEdema = extremReason.Contains("PED"),
                    IsNadiLemah = extremReason.Contains("WPL"),
                    IsSplenomegali = extremReason.Contains("SPL"),
                    IsParesis = extremReason.Contains("PRS"),
                    IsCrt2Up = extremReason.Contains("C2U"),
                    IsMeningeal = extremReason.Contains("MSS"),
                    IsParesthesia = extremReason.Contains("PRT"),
                    IsOtotLengan = extremReason.FirstOrDefault(strCheck => strCheck.Contains("ARM|")) != null ? true : false,
                    IsOtotTungkai = extremReason.FirstOrDefault(strCheck => strCheck.Contains("LGM|")) != null ? true : false,
                    IsLainLain = extremReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")) != null ? true : false,
                    OtotLengan = (extremReason.FirstOrDefault(strCheck => strCheck.Contains("ARM|")) != null ? true : false) == true ? extremReason.FirstOrDefault(strCheck => strCheck.Contains("ARM|")).Split('|')[1] : string.Empty,
                    OtotTungkai = (extremReason.FirstOrDefault(strCheck => strCheck.Contains("LGM|")) != null ? true : false) == true ? extremReason.FirstOrDefault(strCheck => strCheck.Contains("LGM|")).Split('|')[1] : string.Empty,
                    LainLain = (extremReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")) != null ? true : false) == true ? extremReason.FirstOrDefault(strCheck => strCheck.Contains("OTH|")).Split('|')[1] : string.Empty
                }
            };
            var kulit = new
            {
                IsAbnormal = pe.Skin.IsAbNormal ?? false,
                Notes = pe.Skin.Notes
            };

            var peObj = new
            {
                Triase = new
                {
                    //00	Resusitasi
                    //01	Gawat Darurat (Kode=Merah)
                    //02	Gawat Tidak Darurat (Kode=Kuning)
                    //03	Darurat Tidak Gawat (Kode=Kuning)
                    //04	Tidak Gawat Tidak Darurat (Kode=Hijau)
                    //05	Death On Arrival (Kode=Hitam)
                    IsR = "00".Equals(reg.SRTriage),
                    IsGD = "01".Equals(reg.SRTriage),
                    IsGTD = "02".Equals(reg.SRTriage),
                    IsDTG = "03".Equals(reg.SRTriage),
                    IsTGTD = "04".Equals(reg.SRTriage),
                    IsDoa = "05".Equals(reg.SRTriage),
                },
                PemeriksaanDokter = strBuilder.ToString(),
                LocalistUrl = LocalistUrl(asses, "LIGD"),
                LocalistUrl2 = LocalistUrl(asses, "NYERI"),
                LocalistUrl3 = LocalistUrl(asses, "Nyeri(NRS)"),
                LocalistLanjutanUrl = LocalistUrl(asses, "LIGD02"),
                LocalistLanjutanNotes = lanjtnNotes,
                Gcs = pe.Consciousness,
                GcsTotal = pe.Consciousness.Eye.Score + pe.Consciousness.Motor.Score + pe.Consciousness.Verbal.Score,

                TriaseFlaccEsi = new
                {
                    SkalaNyeri = pe.Consciousness.PainScale,
                    Wajah = StandardReference.GetItemName(AppEnum.StandardReference.Flacc, pe.Flacc.Face),
                    Kaki = StandardReference.GetItemName(AppEnum.StandardReference.Flacc, pe.Flacc.Legs),
                    Aktivitas = StandardReference.GetItemName(AppEnum.StandardReference.Flacc, pe.Flacc.Activity),
                    Menangis = StandardReference.GetItemName(AppEnum.StandardReference.Flacc, pe.Flacc.Cry),
                    Konsolabilitas = StandardReference.GetItemName(AppEnum.StandardReference.Flacc, pe.Flacc.Consolability),
                    Esi = StandardReference.GetItemName(AppEnum.StandardReference.Triage5Level, pe.Esi.Id),
                    EsiKondisi = ConvertDataTabletoObject(EsiCondition(pe.Esi.Id, pe.Esi.ConditionIds))
                },
                Kepala = kepala,
                Mata = mata,
                Leher = leher,
                ParuParu = paruParu,
                Jantung = jantung,
                Perut = perut,
                Ekstremitas = ekstremitas,
                Kulit = kulit,
                ObjectiveNotes = pe.Notes,
                LocalistNotes = localistNotes
            };

            //Penjamin
            var bayar = new Guarantor();
            bayar.LoadByPrimaryKey(reg.GuarantorID);

            //InfoMedic
            var infoMedic = new RegistrationInfoMedic();
            infoMedic.LoadByPrimaryKey(asses.RegistrationInfoMedicID);

            //Paramedic
            var Par = new Paramedic();
            Par.LoadByPrimaryKey(reg.ParamedicID);

            //var diags = new EpisodeDiagnose();
            //diags.LoadByPrimaryKey(asses.RegistrationInfoMedicID, asses.PatientEducationSeqNo.ToString());

            var diag = new EpisodeDiagnoseCollection();
            diag.Query.Where(diag.Query.RegistrationNo == asses.RegistrationNo);
            diag.LoadAll();

            var ancil = string.IsNullOrWhiteSpace(pe.AncillaryExam.Summary)
                ? GetSummaryValue(pe.AncillaryExam)
                : pe.AncillaryExam.Summary;
            var additionalField = new
            {
                Alamat = pat.Address,
                Suku = StandardReference.GetItemName(AppEnum.StandardReference.Ethnic, pat.SREthnic),
                Pekerjaan = StandardReference.GetItemName(AppEnum.StandardReference.Occupation, pat.SROccupation),
                PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value), peObj),
                HandOver = AssessmentUtil.TransferToInPatient(reg.RegistrationNo),
                PemeriksaanPenunjang = string.IsNullOrWhiteSpace(ancil) ? asses.OtherExam : string.Concat(ancil, Environment.NewLine, asses.OtherExam),
                StatusPernikahan = StandardReference.GetItemName(AppEnum.StandardReference.MaritalStatus, pat.SRMaritalStatus),
                HubunganPasienDenganKeluarga = "Good",
                TempatTinggal = StandardReference.GetItemName(AppEnum.StandardReference.ResidentialHome, pat.SRResidentialHome),
                CaraBayar = StandardReference.GetItemName(AppEnum.StandardReference.GuarantorType, bayar.SRGuarantorType),
                Bahasa = StandardReference.GetItemName(AppEnum.StandardReference.Nationality, pat.SRNationality),
                Anamnesis = infoMedic.Info1,
                PemeriksaanFisik = infoMedic.Info2,
                DiagnosisText = infoMedic.Info3,
                Planning = infoMedic.Info4,
                Discharge = new
                {
                    IsRujukNew = reg.SRDischargeMethod == "E09_E11_E12",
                    IsKonsulNew = reg.SRDischargeMethod == "O01" || reg.SRDischargeMethod == "E01",
                },
                NamaDokter = Par.ParamedicName,
                DiagnosaNotes = asses.AnamnesisNotes,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg,

                //Tambahan Assessment IGD RSYS
                DiagnosaLengkap = Diagnosis(asses)
            };



            // Asesmen Perawat
            var phrlines = new PatientHealthRecordLineCollection();
            phrlines.Query.Where(phrlines.Query.RegistrationNo == reg.RegistrationNo, phrlines.Query.QuestionFormID == "RM.04.a");
            phrlines.Query.Load();

            var perawat = phrlines.FindInSingleGroup("DIAG.K.12");


            var nurseField = new
            {
                UserPerawat = string.IsNullOrEmpty(perawat.LastUpdateByUserID) ? string.Empty : (AppUser.GetUserName(perawat.LastUpdateByUserID)),
                Psikologis = new
                {
                    IsTenang = phrlines.FindInSingleGroup("DIAG.K.12").QuestionAnswerText == "1",
                    IsCemas = phrlines.FindInSingleGroup("DIAG.K.03").QuestionAnswerText == "1",
                    IsTakut = phrlines.FindInSingleGroup("DIAG.K.13").QuestionAnswerText == "1",
                    IsMarah = phrlines.FindInSingleGroup("DIAG.K.14").QuestionAnswerText == "1",
                    IsSedih = phrlines.FindInSingleGroup("DIAG.K.15").QuestionAnswerText == "1",
                    IsBunuhDiri = phrlines.FindInSingleGroup("DIAG.K.16").QuestionAnswerText == "1",
                    MasalahPerilaku = new
                    {
                        IsTidakAda = phrlines.FindInSingleGroup("DIAG.K.19").QuestionAnswerSelectionLineID == "1",
                        IsAda = phrlines.FindInSingleGroup("DIAG.K.19").QuestionAnswerSelectionLineID == "2",
                    }
                },
                SkriningGiziAwal = new
                {
                    Satu = new
                    {
                        IsTidak = phrlines.FindInSingleGroup("nut2.2").QuestionAnswerSelectionLineID == "00",
                        IsTidakYakin = phrlines.FindInSingleGroup("nut2.2").QuestionAnswerSelectionLineID == "20",
                        Is1Sampai5Kg = phrlines.FindInSingleGroup("nut2.2").QuestionAnswerSelectionLineID == "31",
                        Is6Sampai10Kg = phrlines.FindInSingleGroup("nut2.2").QuestionAnswerSelectionLineID == "32",
                        Is11Sampai15Kg = phrlines.FindInSingleGroup("nut2.2").QuestionAnswerSelectionLineID == "33",
                        IsLebihBesar15Kg = phrlines.FindInSingleGroup("nut2.2").QuestionAnswerSelectionLineID == "34",
                        IsTidakTahu = phrlines.FindInSingleGroup("nut2.2").QuestionAnswerSelectionLineID == "40",
                    },
                    dua = new
                    {
                        IsTidak = phrlines.FindInSingleGroup("nut2.2").QuestionAnswerSelectionLineID == "0",
                        IsTidakYakin = phrlines.FindInSingleGroup("nut2.2").QuestionAnswerSelectionLineID == "1",
                    }

                },
                StatusFungsional = new
                {
                    PenggunaanAlatBantu = new
                    {
                        IsTidak = phrlines.FindInSingleGroup("RJ.F.PAB").QuestionAnswerSelectionLineID == "1",
                        IsTongkat = phrlines.FindInSingleGroup("RJ.F.PAB").QuestionAnswerSelectionLineID == "2",
                        IsKursiRoda = phrlines.FindInSingleGroup("RJ.F.PAB").QuestionAnswerSelectionLineID == "3",
                    },
                    CacatTubuh = new
                    {
                        IsTidak = phrlines.FindInSingleGroup("RJ.F.CT2").QuestionAnswerSelectionLineID == "1",
                        IsAda = phrlines.FindInSingleGroup("RJ.F.CT2").QuestionAnswerSelectionLineID == "2",
                    },
                },
                RisikoJatuh = new
                {
                    Dewasa = new
                    {
                        CaraBerjalan = new
                        {
                            IsYa = phrlines.FindInSingleGroup("ASS.RSJ.D2").QuestionAnswerSelectionLineID == "UM.YN1",
                            IsTidak = phrlines.FindInSingleGroup("ASS.RSJ.D2").QuestionAnswerSelectionLineID == "UM.YN2",
                        },
                        Penompang = new
                        {
                            IsYa = phrlines.FindInSingleGroup("ASS.RSJ.D2").QuestionAnswerSelectionLineID == "UM.YN1",
                            IsTidak = phrlines.FindInSingleGroup("ASS.RSJ.D2").QuestionAnswerSelectionLineID == "UM.YN2",
                        }
                    },
                    AnakHumptyDumpty = new
                    {
                        Skor = new
                        {
                            IsRisikoTinggi = phrlines.FindInSingleGroup("ASS.RSJ.A1").QuestionAnswerSelectionLineID == "1",
                            IsRisikoRendah = phrlines.FindInSingleGroup("ASS.RSJ.A1").QuestionAnswerSelectionLineID == "2",
                        }
                    },
                    Hasil = new
                    {
                        IsTidakBersiko = phrlines.FindInSingleGroup("ASS.RSJ.D3").QuestionAnswerSelectionLineID == "1",
                        IsRisikoRendah = phrlines.FindInSingleGroup("ASS.RSJ.D3").QuestionAnswerSelectionLineID == "2",
                        IsRisikoTinggi = phrlines.FindInSingleGroup("ASS.RSJ.D3").QuestionAnswerSelectionLineID == "3",
                    }
                },
                KebutuhanFungsional = new
                {
                    AlatBantu = new
                    {
                        IsTidak = phrlines.FindInSingleGroup("ASS.KFN1").QuestionAnswerSelectionLineID == "1",
                        IsTongkat = phrlines.FindInSingleGroup("ASS.KFN1").QuestionAnswerSelectionLineID == "2",
                        IsKursiRoda = phrlines.FindInSingleGroup("ASS.KFN1").QuestionAnswerSelectionLineID == "3",
                    },
                    CacatTubuh = new
                    {
                        IsTidak = phrlines.FindInSingleGroup("ASS.KFN2").QuestionAnswerSelectionLineID == "ADATDKSBT01",
                        IsAda = phrlines.FindInSingleGroup("ASS.KFN2").QuestionAnswerSelectionLineID == "ADATDKSBT02",
                    },
                    Defekasi = new
                    {
                        IsNormal = phrlines.FindInSingleGroup("ASS.KFN3").QuestionAnswerSelectionLineID == "EL.BAB1",
                        IsInkontinensia = phrlines.FindInSingleGroup("ASS.KFN3").QuestionAnswerSelectionLineID == "EL.BAB2",
                        IsKonstipasi = phrlines.FindInSingleGroup("ASS.KFN3").QuestionAnswerSelectionLineID == "EL.BAB3",
                        IsFeacesBerdarah = phrlines.FindInSingleGroup("ASS.KFN3").QuestionAnswerSelectionLineID == "EL.BAB4",
                        IsKolostomi = phrlines.FindInSingleGroup("ASS.KFN3").QuestionAnswerSelectionLineID == "EL.BAB5",
                        IsDiare = phrlines.FindInSingleGroup("ASS.KFN3").QuestionAnswerSelectionLineID == "EL.BAB6",
                        IsPencahar = phrlines.FindInSingleGroup("ASS.KFN3").QuestionAnswerSelectionLineID == "EL.BAB7",
                    },
                    Miksi = new
                    {
                        Normal = phrlines.FindInSingleGroup("ASS.KFN4").QuestionAnswerSelectionLineID == "1",
                        Retensi = phrlines.FindInSingleGroup("ASS.KFN4").QuestionAnswerSelectionLineID == "2",
                        InkontinensiaUrin = phrlines.FindInSingleGroup("ASS.KFN4").QuestionAnswerSelectionLineID == "3",
                    },
                    GastoIntestinal = new
                    {
                        IsNormal = phrlines.FindInSingleGroup("ASS.KFN5").QuestionAnswerSelectionLineID == "1",
                        IsRefluks = phrlines.FindInSingleGroup("ASS.KFN5").QuestionAnswerSelectionLineID == "2",
                        IsNausea = phrlines.FindInSingleGroup("ASS.KFN5").QuestionAnswerSelectionLineID == "3",
                        IsMuntah = phrlines.FindInSingleGroup("ASS.KFN5").QuestionAnswerSelectionLineID == "4",
                    },
                    PolaTidur = new
                    {
                        IsNormal = phrlines.FindInSingleGroup("ASS.KFN6").QuestionAnswerSelectionLineID == "1",
                        IsMasalah = phrlines.FindInSingleGroup("ASS.KFN6").QuestionAnswerSelectionLineID == "2",
                    },
                    Cairan = new
                    {
                        IsNormal = phrlines.FindInSingleGroup("ASS.KFN7").QuestionAnswerSelectionLineID == "1",
                        IsDehidrasi = phrlines.FindInSingleGroup("ASS.KFN7").QuestionAnswerSelectionLineID == "2",
                    },
                    Pernafasan = new
                    {
                        IsNormal = phrlines.FindInSingleGroup("ASS.KFN8").QuestionAnswerSelectionLineID == "1",
                        IsSesak = phrlines.FindInSingleGroup("ASS.KFN8").QuestionAnswerSelectionLineID == "2",
                    },
                    Kardiovaskuler = new
                    {
                        IsNormal = phrlines.FindInSingleGroup("ASS.KFN9").QuestionAnswerSelectionLineID == "1",
                        IsBerdebarDebar = phrlines.FindInSingleGroup("ASS.KFN9").QuestionAnswerSelectionLineID == "2",
                        IsAritmia = phrlines.FindInSingleGroup("ASS.KFN9").QuestionAnswerSelectionLineID == "3",
                    },
                    Berpakaian = new
                    {
                        IsMandiri = phrlines.FindInSingleGroup("ASS.KFN10").QuestionAnswerSelectionLineID == "1",
                        IsDibantuAlat = phrlines.FindInSingleGroup("ASS.KFN10").QuestionAnswerSelectionLineID == "2",
                        IsDibantuOrangl = phrlines.FindInSingleGroup("ASS.KFN10").QuestionAnswerSelectionLineID == "3",
                        IsDibantuTotal = phrlines.FindInSingleGroup("ASS.KFN10").QuestionAnswerSelectionLineID == "4",
                    },
                    BuangAir = new
                    {
                        IsMandiri = phrlines.FindInSingleGroup("ASS.KFN11").QuestionAnswerSelectionLineID == "1",
                        IsDibantuAlat = phrlines.FindInSingleGroup("ASS.KFN11").QuestionAnswerSelectionLineID == "2",
                        IsDibantuOrangl = phrlines.FindInSingleGroup("ASS.KFN11").QuestionAnswerSelectionLineID == "3",
                        IsDibantuTotal = phrlines.FindInSingleGroup("ASS.KFN11").QuestionAnswerSelectionLineID == "4",
                    },
                    PersonalHygiene = new
                    {
                        IsMandiri = phrlines.FindInSingleGroup("ASS.KFN12").QuestionAnswerSelectionLineID == "1",
                        IsDibantuAlat = phrlines.FindInSingleGroup("ASS.KFN12").QuestionAnswerSelectionLineID == "2",
                        IsDibantuOrangl = phrlines.FindInSingleGroup("ASS.KFN12").QuestionAnswerSelectionLineID == "3",
                        IsDibantuTotal = phrlines.FindInSingleGroup("ASS.KFN12").QuestionAnswerSelectionLineID == "4",
                    },
                    Berpindah = new
                    {
                        IsMandiri = phrlines.FindInSingleGroup("ASS.KFN13").QuestionAnswerSelectionLineID == "1",
                        IsDibantuAlat = phrlines.FindInSingleGroup("ASS.KFN13").QuestionAnswerSelectionLineID == "2",
                        IsDibantuOrangl = phrlines.FindInSingleGroup("ASS.KFN13").QuestionAnswerSelectionLineID == "3",
                        IsDibantuTotal = phrlines.FindInSingleGroup("ASS.KFN13").QuestionAnswerSelectionLineID == "4",
                    }
                },
                AssesmenNyeri = new
                {
                    Nyeri = new
                    {
                        IsTidak = phrlines.FindInSingleGroup("ASS.NYE1").QuestionAnswerSelectionLineID == "YTLOKASI1",
                        IsIya = phrlines.FindInSingleGroup("ASS.NYE1").QuestionAnswerSelectionLineID == "YTLOKASI2",
                    },
                    Jenis = new
                    {
                        IsAkut = phrlines.FindInSingleGroup("JNS").QuestionAnswerSelectionLineID == "1",
                        IsKronis = phrlines.FindInSingleGroup("JNS").QuestionAnswerSelectionLineID == "2",
                    },
                    Numerik = phrlines.FindInSingleGroup("ASS.NYE4").QuestionAnswerText,
                    WongBeker = phrlines.FindInSingleGroup("ASS.NYE5").QuestionAnswerText,
                    Flacc = phrlines.FindInSingleGroup("ASS.NYE6").QuestionAnswerText,
                    Provocation = new
                    {
                        IsCahaya = phrlines.FindInSingleGroup("ASS.NYE7").QuestionAnswerSelectionLineID == "1",
                        IsGelap = phrlines.FindInSingleGroup("ASS.NYE7").QuestionAnswerSelectionLineID == "2",
                        IsGerakan = phrlines.FindInSingleGroup("ASS.NYE7").QuestionAnswerSelectionLineID == "3",
                        IsBerbaring = phrlines.FindInSingleGroup("ASS.NYE7").QuestionAnswerSelectionLineID == "4",
                        IsLainnya = phrlines.FindInSingleGroup("ASS.NYE7").QuestionAnswerSelectionLineID == "5",
                    },
                    Quality = new
                    {
                        IsDitusuk = phrlines.FindInSingleGroup("ASS.NYE8").QuestionAnswerSelectionLineID == "1",
                        IsDipukul = phrlines.FindInSingleGroup("ASS.NYE8").QuestionAnswerSelectionLineID == "2",
                        IsBerdenyut = phrlines.FindInSingleGroup("ASS.NYE8").QuestionAnswerSelectionLineID == "3",
                        IsDitikam = phrlines.FindInSingleGroup("ASS.NYE8").QuestionAnswerSelectionLineID == "4",
                        IsKram = phrlines.FindInSingleGroup("ASS.NYE8").QuestionAnswerSelectionLineID == "5",
                        IsDitarik = phrlines.FindInSingleGroup("ASS.NYE8").QuestionAnswerSelectionLineID == "6",
                        IsDibakar = phrlines.FindInSingleGroup("ASS.NYE8").QuestionAnswerSelectionLineID == "7",
                        vTajam = phrlines.FindInSingleGroup("ASS.NYE8").QuestionAnswerSelectionLineID == "8",
                        IsLainnya = phrlines.FindInSingleGroup("ASS.NYE8").QuestionAnswerSelectionLineID == "9",
                    },
                    Radiation = new
                    {
                        IsIya = phrlines.FindInSingleGroup("ASS.NYE9").QuestionAnswerSelectionLineID == "UM.YN1",
                        IsTidak = phrlines.FindInSingleGroup("ASS.NYE9").QuestionAnswerSelectionLineID == "UM.YN2",
                    },
                    Severty = new
                    {
                        IsTidakNyeri = phrlines.FindInSingleGroup("ASS.NYE10").QuestionAnswerSelectionLineID == "1",
                        IsRingan = phrlines.FindInSingleGroup("ASS.NYE10").QuestionAnswerSelectionLineID == "2",
                        IsSedang = phrlines.FindInSingleGroup("ASS.NYE10").QuestionAnswerSelectionLineID == "3",
                        IsBerat = phrlines.FindInSingleGroup("ASS.NYE10").QuestionAnswerSelectionLineID == "4",
                    },
                    Time = new
                    {
                        IsTerusMenerus = phrlines.FindInSingleGroup("ASS.NYE11").QuestionAnswerSelectionLineID == "1",
                        IsHilangTimbul = phrlines.FindInSingleGroup("ASS.NYE11").QuestionAnswerSelectionLineID == "2",
                    },
                    Lama = new
                    {
                        IsKurang30Menit = phrlines.FindInSingleGroup("ASS.NYE12").QuestionAnswerSelectionLineID == "1",
                        IsLebih30Menit = phrlines.FindInSingleGroup("ASS.NYE12").QuestionAnswerSelectionLineID == "2",
                    },
                },
                DiagnosaKeperawatan = new
                {
                    IsNyeri = phrlines.FindInSingleGroup("DIAG.K.01").QuestionAnswerText == "1",
                    IsPerfusiCelebral = phrlines.FindInSingleGroup("DIAG.K.02").QuestionAnswerText == "1",
                    IsCemas = phrlines.FindInSingleGroup("DIAG.K.03").QuestionAnswerText == "1",
                    IsNyeriDiag = phrlines.FindInSingleGroup("DIAG.K.04").QuestionAnswerText == "1",
                    IsSensori = phrlines.FindInSingleGroup("DIAG.K.05").QuestionAnswerText == "1",
                    IsHipertemi = phrlines.FindInSingleGroup("DIAG.K.06").QuestionAnswerText == "1",
                    IsKerusakanIntegritas = phrlines.FindInSingleGroup("DIAG.K.07").QuestionAnswerText == "1",
                    IsPerfusiJaringan = phrlines.FindInSingleGroup("DIAG.K.08").QuestionAnswerText == "1",
                    IsBodyImage = phrlines.FindInSingleGroup("DIAG.K.09").QuestionAnswerText == "1",
                    IsGangguanMobilitas = phrlines.FindInSingleGroup("DIAG.K.10").QuestionAnswerText == "1",
                    IsKurangPengetahuan = phrlines.FindInSingleGroup("DIAG.K.11").QuestionAnswerText == "1",
                    IsPerubahanNutrisi = phrlines.FindInSingleGroup("DIAG.K.11").QuestionAnswerText == "1",
                    RencanaAsuhanKeperawatan = phrlines.FindInSingleGroup("IMP.K").QuestionAnswerText,
                }
            };


            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField, nurseField);
            return retField;
        }

        #region Generate Summary from Question Answer dipakai hanya utk data lama yg tidak terisi summary nya
        private DataTable QuestionDataTable(string questionGroupID)
        {
            var questionQuery = new QuestionQuery("a");
            var qrQInGroup = new QuestionInGroupQuery("c");
            questionQuery.InnerJoin(qrQInGroup).On(questionQuery.QuestionID == qrQInGroup.QuestionID);
            questionQuery.OrderBy(qrQInGroup.RowIndex.Ascending);
            questionQuery.Where(qrQInGroup.QuestionGroupID == questionGroupID);
            questionQuery.Select
            (
                questionQuery,
                qrQInGroup.QuestionGroupID,
                qrQInGroup.RowIndex
            );

            var dtb = questionQuery.LoadDataTable();
            return dtb;
        }

        private QuestionAnswerValue GetQuestionAnswerValue(QuestionGroupAnswerValue answer, string questionID)
        {
            foreach (var answerValue in answer.QuestionAnswerValues)
            {
                if (answerValue.QuestionID == questionID)
                    return answerValue;
            }
            return new QuestionAnswerValue();
        }

        public string GetSummaryValue(QuestionGroupAnswerValue answer)
        {
            var strbSummary = new StringBuilder();
            var dtbQuestion = QuestionDataTable(answer.QuestionGroupID);
            foreach (DataRow rowQuestion in dtbQuestion.Rows)
            {
                var qId = rowQuestion["QuestionID"].ToString();
                var answerVal = GetQuestionAnswerValue(answer, qId);
                var normalVal = rowQuestion["QuestionAnswerDefaultSelectionID"];
                // Summary
                AppendSummary(answerVal, normalVal, rowQuestion["QuestionText"].ToString(), strbSummary);

                // Child Question
                var quest = new QuestionQuery();
                quest.Where(quest.ParentQuestionID == rowQuestion["QuestionID"] && quest.SRAnswerType != string.Empty);
                var dtbSubQuestion = quest.LoadDataTable();

                foreach (DataRow rowSubQuestion in dtbSubQuestion.Rows)
                {
                    qId = rowSubQuestion["QuestionID"].ToString();
                    var childAnswerVal = GetQuestionAnswerValue(answer, qId);
                    normalVal = rowSubQuestion["QuestionAnswerDefaultSelectionID"];

                    // Summary
                    AppendSummary(childAnswerVal, normalVal, rowSubQuestion["QuestionText"].ToString(), strbSummary);
                }
            }

            return strbSummary.ToString();
        }

        private static void AppendSummary(QuestionAnswerValue answerVal, object normalVal, string questionText,
            StringBuilder strbSummary)
        {
            if (!((answerVal.QuestionAnswerSelectionLineID != null && answerVal.QuestionAnswerSelectionLineID.Equals(normalVal))
                  || (answerVal.QuestionAnswerText != null && answerVal.QuestionAnswerText.Equals(normalVal))))
            {
                if (answerVal.QuestionAnswerText != null)
                {
                    if (answerVal.QuestionAnswerText.Equals(questionText)) // Berarti Checkbox
                    {
                        strbSummary.AppendFormat("- {0}", answerVal.QuestionAnswerText);
                        strbSummary.AppendLine(string.Empty);
                    }
                    else
                    {
                        if (answerVal.QuestionAnswerText.Contains('|'))
                        {
                            var vals = answerVal.QuestionAnswerText.Split('|');
                            if (!string.IsNullOrWhiteSpace(vals[0]))
                            {
                                if (vals[0].Equals(questionText))
                                {
                                    strbSummary.AppendFormat("- {0}: {1}", questionText, vals[1]);
                                    strbSummary.AppendLine(string.Empty);
                                }
                                else
                                {
                                    strbSummary.AppendFormat("- {0}: {1}", questionText, answerVal.QuestionAnswerText.Replace('|', ' '));
                                    strbSummary.AppendLine(string.Empty);
                                }
                            }
                        }
                        else
                        {
                            strbSummary.AppendFormat("- {0}: {1}", questionText,
                                answerVal.QuestionAnswerText);
                            strbSummary.AppendLine(string.Empty);
                        }
                    }


                }
            }
        }

        #endregion
        #endregion


        #region Rawat Jalan

        public object OutPatientStd(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs, object peObj, object peObj2)
        {

            var additionalField = new
            {
                PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value), peObj, peObj2),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                //SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? au.SignatureImage : asses.SignImg,
                PatientSignImg = asses.PatientSignImg,
                ReviewOfSystem = JsonConvert.DeserializeObject<InternalRos>(asses.ReviewOfSystem ?? JsonConvert.SerializeObject(new InternalRos()))
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }

        #region Kid  Assessment
        /// <summary>
        /// ASESMEN AWAL RAWAT JALAN KLINIK ANAK
        /// Kid Clinic Outpatient Initial Assessment
        /// </summary>
        public object KidClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var nurseField = KidNurseAssessment(asses, pat, reg);

            var additionalField = new
            {
                Perawat = nurseField,
                PemeriksaanUmum = JsonConvert.DeserializeObject<KidPe>(asses.PhysicalExam),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }

        private static object KidNurseAssessment(PatientAssessment asses, Patient pat, Registration reg)
        {
            // Asesmen Perawat
            var astp = new AppSRAssessmentType();
            astp.LoadByPrimaryKey(asses.SRAssessmentType);

            var phrlines = new PatientHealthRecordLineCollection();
            phrlines.Query.Where(phrlines.Query.RegistrationNo == reg.RegistrationNo,
                phrlines.Query.QuestionFormID == astp.NursingQuestionFormID);
            phrlines.Query.Load();

            var tmp = phrlines.FindInSingleGroup(PhrUtil.Psikologis.Lainnya).QuestionAnswerText;
            var psiLainnya = !string.IsNullOrWhiteSpace(tmp) && tmp.Contains('|') ? tmp.Split('|') : "|".Split('|');


            var lingkarKpl = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.LingkarKpl).QuestionAnswerText;
            var tbLahir = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.TBLahir).QuestionAnswerText;
            var bbLahir = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.BBLahir).QuestionAnswerText;
            var asi = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.ASI).QuestionAnswerText;
            var susuFormula = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.SusuFormula).QuestionAnswerText;
            var makanan = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.Makanan).QuestionAnswerText;


            var angkatKpla = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.AngkatKpla).QuestionAnswerText;
            var tengkurap = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.Tengkurap).QuestionAnswerText;
            var duduk = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.Duduk).QuestionAnswerText;
            var merangkak = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.Merangkak).QuestionAnswerText;
            var berdiri = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.Berdiri).QuestionAnswerText;
            var berjalan = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.Berjalan).QuestionAnswerText;
            var meraihBnda = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.MeraihBnda).QuestionAnswerText;
            var memegang = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.Memegang).QuestionAnswerText;
            var mengoceh = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.Mengoceh).QuestionAnswerText;
            var berbicara = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.Berbicara).QuestionAnswerText;
            var keluhanSkrg = phrlines.FindInSingleGroup(PhrUtil.TumbuhKembang.Keluhan).QuestionAnswerText;


            var answer = phrlines.FindInSingleGroup(PhrUtil.Perinatal.CaraLahir);
            var birthMethod = answer.QuestionAnswerSelectionLineID;
            var perawat = AppUser.GetUserName(answer.LastUpdateByUserID);

            var birthComplication =
                phrlines.FindInSingleGroup(PhrUtil.Perinatal.Komplikasi).QuestionAnswerSelectionLineID;
            var birthIndication = phrlines.FindInSingleGroup(PhrUtil.Perinatal.Penyulit).QuestionAnswerSelectionLineID;

            // Perawt Entry
            var nurseField = new
            {
                AlasanOrtu = phrlines.FindInSingleGroup("PRTREASN").QuestionAnswerText,
                Perinatal = new
                {
                    LamaKehmlan = string.Format("{0} Bulan / {1} Minggu",
                        phrlines.FindInSingleGroup(PhrUtil.Perinatal.LamaBulan).QuestionAnswerNum.ToInt(),
                        phrlines.FindInSingleGroup(PhrUtil.Perinatal.LamaMinggu).QuestionAnswerNum.ToInt()),
                    Penolong = phrlines.FindInSingleGroup(PhrUtil.Perinatal.Penolong).QuestionAnswerText,
                    IsKomplikasi = !string.IsNullOrWhiteSpace(birthComplication),
                    Komplikasi = string.IsNullOrWhiteSpace(birthComplication)
                        ? string.Empty
                        : StandardReference.GetItemName(AppEnum.StandardReference.BirthComplication, birthComplication),
                    RiwytPerslnan = new
                    {
                        //BirthMethod	01	Spontan
                        //BirthMethod	02	Spontan Sungsang
                        //BirthMethod	03	Spontan Immaturus
                        //BirthMethod	04	Vacum Extraction
                        //BirthMethod	05	Forcep Extraction
                        //BirthMethod	06	IUFD
                        //BirthMethod	07	SC (Sectio Caesaria)

                        IsSpontan = "01_02_03".Contains(birthMethod),
                        IsOperasiSC = birthMethod == "07",
                        IsVakum = birthMethod == "04",
                        IsForcep = birthMethod == "05"
                    },
                    IsPenyulit = !string.IsNullOrWhiteSpace(birthIndication),
                    Penyulit = string.IsNullOrWhiteSpace(birthComplication)
                        ? string.Empty
                        : StandardReference.GetItemName(AppEnum.StandardReference.BirthIndication, birthIndication)
                },
                Imunisasi = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ? ImmunizationStatusInPatient(pat) : ImmunizationStatusOutPatient(pat.PatientID),
                TumbuhKembang = new
                {
                    LingkarKpl = string.Format("{0} cm", lingkarKpl),
                    TBLahir = string.Format("{0} cm", tbLahir),
                    BBLahir = string.Format("{0} gr", bbLahir),
                    ASI = string.Format("{0} bulan", asi),
                    SusuFormula = string.Format("{0} bulan", susuFormula),
                    Makanan = string.Format("{0} bulan", makanan),

                    IsAngkatKpla = angkatKpla != "0",
                    AngkatKpla = string.Format("{0} bulan", angkatKpla),
                    IsTengkurap = tengkurap != "0",
                    Tengkurap = string.Format("{0} bulan", tengkurap),
                    IsDuduk = duduk != "0",
                    Duduk = string.Format("{0} bulan", duduk),
                    IsMerangkak = merangkak != "0",
                    Merangkak = string.Format("{0} bulan", merangkak),
                    IsBerdiri = berdiri != "0",
                    Berdiri = string.Format("{0} bulan", berdiri),
                    IsBerjalan = berjalan != "0",
                    Berjalan = string.Format("{0} bulan", berjalan),
                    IsMeraihBnda = meraihBnda != "0",
                    MeraihBnda = string.Format("{0} bulan", meraihBnda),
                    IsMemegang = memegang != "0",
                    Memegang = string.Format("{0} bulan", memegang),
                    IsMengoceh = mengoceh != "0",
                    Mengoceh = string.Format("{0} bulan", mengoceh),
                    IsBerbicara = berbicara != "0",
                    Berbicara = string.Format("{0} bulan", berbicara),
                    KeluhanSkrg = keluhanSkrg
                },
                SosEkonomi = SosialEkonomiKid(phrlines),
                KdanPsikologis = new
                {
                    EksWajah = new
                    {
                        IsTenang = phrlines.FindInSingleGroup(PhrUtil.Psikologis.Tenang).QuestionAnswerText == "1",
                        IsSedih = phrlines.FindInSingleGroup(PhrUtil.Psikologis.Sedih).QuestionAnswerText == "1",
                        IsTakut = phrlines.FindInSingleGroup(PhrUtil.Psikologis.Takut).QuestionAnswerText == "1",
                    },
                    IsMenghindarKtkMata =
                        phrlines.FindInSingleGroup(PhrUtil.Psikologis.Menghindar).QuestionAnswerText == "1",
                    IsTrluPenrut = phrlines.FindInSingleGroup(PhrUtil.Psikologis.Penurut).QuestionAnswerText == "1",
                    IsAgresif = phrlines.FindInSingleGroup(PhrUtil.Psikologis.Agresif).QuestionAnswerText == "1",
                    IsPasif = phrlines.FindInSingleGroup(PhrUtil.Psikologis.Pasif).QuestionAnswerText == "1",
                    IsLainnya = psiLainnya[0] == "1" || !string.IsNullOrWhiteSpace(psiLainnya[1]),
                    Lainnya = psiLainnya[1]
                },
                PemeriksaanUmum = new
                {
                    Keadaan = phrlines.FindInSingleGroup(PhrUtil.Pemeriksaan.Keadaan).QuestionAnswerText,
                    Kesadaran = phrlines.FindInSingleGroup(PhrUtil.Pemeriksaan.Kesadaran).QuestionAnswerText,
                    VitalSign = VitalSignAsmByNurse(phrlines),
                },
                PerawatPengkji = perawat
            };
            return nurseField;
        }

        private static object ImmunizationStatusOutPatient(string patientID)
        {
            // Imunisasi
            var imuns = new PatientImmunizationCollection();
            imuns.Query.Where(imuns.Query.PatientID == patientID);
            imuns.Query.Load();

            var retval = new
            {
                IsBcg = imuns.Any(imun => imun.ImmunizationID == "BCG" && imun.ImmunizationNo == 1),

                IsHepB1 = imuns.Any(imun => imun.ImmunizationID == "HPB" && imun.ImmunizationNo == 1),
                IsHepB2 = imuns.Any(imun => imun.ImmunizationID == "HPB" && imun.ImmunizationNo == 2),
                IsHepB3 = imuns.Any(imun => imun.ImmunizationID == "HPB" && imun.ImmunizationNo == 3),

                IsPolio0 = imuns.Any(imun => imun.ImmunizationID == "POL" && imun.ImmunizationNo == 1),
                IsPolio1 = imuns.Any(imun => imun.ImmunizationID == "POL" && imun.ImmunizationNo == 2),
                IsPolio2 = imuns.Any(imun => imun.ImmunizationID == "POL" && imun.ImmunizationNo == 3),
                IsPolio3 = imuns.Any(imun => imun.ImmunizationID == "POL" && imun.ImmunizationNo == 4),

                IsDPt1 = imuns.Any(imun => imun.ImmunizationID == "DPT" && imun.ImmunizationNo == 1),
                IsDPt2 = imuns.Any(imun => imun.ImmunizationID == "DPT" && imun.ImmunizationNo == 2),
                IsDPt3 = imuns.Any(imun => imun.ImmunizationID == "DPT" && imun.ImmunizationNo == 3),

                IsCampak = imuns.Any(imun => imun.ImmunizationID == "CAM" && imun.ImmunizationNo == 1),
            };
            return retval;
        }

        private static object ImmunizationStatusInPatient(Patient pat)
        {
            // Imunisasi
            var imuns = new PatientImmunizationCollection();
            imuns.Query.Where(imuns.Query.PatientID == pat.PatientID);
            imuns.Query.Load();

            PatientImmunization bcg = null;
            PatientImmunization hepB1 = null;
            PatientImmunization hepB2 = null;
            PatientImmunization hepB3 = null;

            PatientImmunization polio0 = null;
            PatientImmunization polio1 = null;
            PatientImmunization polio2 = null;
            PatientImmunization polio3 = null;

            PatientImmunization dpt1 = null;
            PatientImmunization dpt2 = null;
            PatientImmunization dpt3 = null;

            PatientImmunization campak = null;

            foreach (PatientImmunization imun in imuns)
            {
                if (imun.ImmunizationID == "BCG" && imun.ImmunizationNo == 1)
                    bcg = imun;
                else if (imun.ImmunizationID == "HPB" && imun.ImmunizationNo == 1)
                    hepB1 = imun;
                else if (imun.ImmunizationID == "HPB" && imun.ImmunizationNo == 2)
                    hepB2 = imun;
                else if (imun.ImmunizationID == "HPB" && imun.ImmunizationNo == 3)
                    hepB3 = imun;
                else if (imun.ImmunizationID == "POL" && imun.ImmunizationNo == 1)
                    polio0 = imun;
                else if (imun.ImmunizationID == "POL" && imun.ImmunizationNo == 2)
                    polio1 = imun;
                else if (imun.ImmunizationID == "POL" && imun.ImmunizationNo == 3)
                    polio2 = imun;
                else if (imun.ImmunizationID == "POL" && imun.ImmunizationNo == 4)
                    polio3 = imun;
                else if (imun.ImmunizationID == "DPT" && imun.ImmunizationNo == 1)
                    dpt1 = imun;
                else if (imun.ImmunizationID == "DPT" && imun.ImmunizationNo == 2)
                    dpt2 = imun;
                else if (imun.ImmunizationID == "DPT" && imun.ImmunizationNo == 3)
                    dpt3 = imun;
                else if (imun.ImmunizationID == "CAM" && imun.ImmunizationNo == 1)
                    campak = imun;
            }

            var dob = pat.DateOfBirth.Value;
            var retval = new
            {
                IsBcg = bcg != null,
                BcgAge = bcg != null && bcg.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, bcg.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, bcg.ImmunizationDate.Value)) : string.Empty,

                IsHepB1 = hepB1 != null,
                HepB1Age = hepB1 != null && hepB1.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, hepB1.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, hepB1.ImmunizationDate.Value)) : string.Empty,

                IsHepB2 = hepB2 != null,
                HepB2Age = hepB2 != null && hepB2.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, hepB2.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, hepB2.ImmunizationDate.Value)) : string.Empty,

                IsHepB3 = hepB3 != null,
                HepB3Age = hepB3 != null && hepB3.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, hepB3.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, hepB3.ImmunizationDate.Value)) : string.Empty,

                IsPolio0 = polio0 != null,
                Polio0Age = polio0 != null && polio0.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, polio0.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, polio0.ImmunizationDate.Value)) : string.Empty,

                IsPolio1 = polio1 != null,
                Polio1Age = polio1 != null && polio1.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, polio1.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, polio1.ImmunizationDate.Value)) : string.Empty,

                IsPolio2 = polio2 != null,
                Polio2Age = polio2 != null && polio2.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, polio2.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, polio2.ImmunizationDate.Value)) : string.Empty,

                IsPolio3 = polio3 != null,
                Polio3Age = polio3 != null && polio3.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, polio3.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, polio3.ImmunizationDate.Value)) : string.Empty,

                IsDPt1 = dpt1 != null,
                DPt1Age = dpt1 != null && dpt1.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, dpt1.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, dpt1.ImmunizationDate.Value)) : string.Empty,

                IsDPt2 = dpt2 != null,
                DPt2Age = dpt2 != null && dpt2.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, dpt2.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, dpt2.ImmunizationDate.Value)) : string.Empty,

                IsDPt3 = dpt3 != null,
                DPt3Age = dpt3 != null && dpt3.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, dpt3.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, dpt3.ImmunizationDate.Value)) : string.Empty,

                IsCampak = campak != null,
                CampakAge = campak != null && campak.ImmunizationDate != null ? string.Format("{0} thn {1} bln", Helper.GetAgeInYear(dob, campak.ImmunizationDate.Value), Helper.GetAgeInMonth(dob, campak.ImmunizationDate.Value)) : string.Empty

            };
            return retval;
        }

        public static object VitalSignAsmByNurse(PatientHealthRecordLineCollection phrlines)
        {

            var vs = new
            {
                //BB = string.Format("{0} kg", phrlines.FindInSingleGroup(PhrUtil.VitalSign.BB).QuestionAnswerText),
                //TB = string.Format("{0} cm", phrlines.FindInSingleGroup(PhrUtil.VitalSign.TB).QuestionAnswerText),
                //Suhu = string.Format("{0} °C", phrlines.FindInSingleGroup(PhrUtil.VitalSign.Suhu).QuestionAnswerText),
                //TekananDarah = string.Format("{0} / {1} mmHg", phrlines.FindInSingleGroup(PhrUtil.VitalSign.TDS).QuestionAnswerText, phrlines.FindInSingleGroup(PhrUtil.VitalSign.TDD).QuestionAnswerText),
                //Nadi = string.Format("{0} x/mnt", phrlines.FindInSingleGroup(PhrUtil.VitalSign.Nadi).QuestionAnswerText),
                //Pernafasan = string.Format("{0} x.mnt", phrlines.FindInSingleGroup(PhrUtil.VitalSign.Pernafasan).QuestionAnswerText),
                //Nutrisi = phrlines.FindInSingleGroup(PhrUtil.VitalSign.Gizi).QuestionAnswerText

                BB = string.Format("{0} kg", phrlines.FindInSingleGroup(VitalSign.GetVitalSignID(VitalSign.VitalSignEnum.BodyWeight)).QuestionAnswerText),
                TB = string.Format("{0} cm", phrlines.FindInSingleGroup(VitalSign.GetVitalSignID(VitalSign.VitalSignEnum.BodyHeight)).QuestionAnswerText),
                Suhu = string.Format("{0} °C", phrlines.FindInSingleGroup(VitalSign.GetVitalSignID(VitalSign.VitalSignEnum.Temperature)).QuestionAnswerText),
                TekananDarah = string.Format("{0} / {1} mmHg", phrlines.FindInSingleGroup(VitalSign.GetVitalSignID(VitalSign.VitalSignEnum.BloodPressureSistolic)).QuestionAnswerText, phrlines.FindInSingleGroup(VitalSign.GetVitalSignID(VitalSign.VitalSignEnum.BloodPressureDiastolic)).QuestionAnswerText),
                Nadi = string.Format("{0} x/mnt", phrlines.FindInSingleGroup(VitalSign.GetVitalSignID(VitalSign.VitalSignEnum.HeartRate)).QuestionAnswerText),
                Pernafasan = string.Format("{0} x.mnt", phrlines.FindInSingleGroup(VitalSign.GetVitalSignID(VitalSign.VitalSignEnum.RespiratoryRate)).QuestionAnswerText),
                Nutrisi = phrlines.FindInSingleGroup(VitalSign.GetVitalSignID(VitalSign.VitalSignEnum.Nutrition)).QuestionAnswerText
            };
            return vs;
        }

        public static object SosialEkonomiKid(PatientHealthRecordLineCollection phrlines)
        {
            var fatherOcc = phrlines.FindInSingleGroup("SOSE-0001")
                .QuestionAnswerSelectionLineID;
            var isWiraswasta = fatherOcc == "12";
            var isPNS = fatherOcc == "01";
            var isSwasta = fatherOcc == "06";
            var isPensiunan = false; //TODO: Occupation Tambah Pensiunan

            var tmp = phrlines.FindInSingleGroup("SOSE-0004").QuestionAnswerText;
            var hmLainnya = !string.IsNullOrWhiteSpace(tmp) && tmp.Contains('|') ? tmp.Split('|') : "|".Split('|');

            var sosEkonomi = new
            {
                PekerjaanOrtu = new
                {
                    IsWiraswasta = isWiraswasta,
                    IsPNS = isPNS,
                    IsSwasta = isSwasta,
                    IsPensiunan = isPensiunan,
                    IsLainnya = !(isWiraswasta || isPNS || isSwasta || isPensiunan),
                    Lainnya = StandardReference.GetItemName(AppEnum.StandardReference.Occupation, fatherOcc)
                },
                TinggalBrsma = new
                {
                    IsOrtu = phrlines.FindInSingleGroup("SOSE-0003").QuestionAnswerText == "1",
                    IsLainnya = hmLainnya[0] == "1" || !string.IsNullOrWhiteSpace(hmLainnya[1]),
                    Lainnya = hmLainnya[1]
                }
            };
            return sosEkonomi;
        }
        #endregion

        #region ASESMEN KEBIDANAN
        public object KebidananInPatient(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {

            var nurseField = KebidananNurseAssessment(asses, pat, reg);

            var additionalField = new
            {
                Perawat = nurseField,
                PartusHist = ConvertDataTabletoObject(PartusHist(reg.PatientID)),
                PemeriksaanUmum = JsonConvert.DeserializeObject<KidPe>(asses.PhysicalExam),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                //SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                SignImgUrl = (au.SignatureImage != null ? au.SignatureImage : asses.SignImg) ?? new byte[0],

                PatientSignImg = asses.PatientSignImg
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;

        }

        /// <summary>
        /// ASESMEN AWAL RAWAT JALAN KLINIK KEBIDANAN 
        /// </summary>
        public object KebidananClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {

            var nurseField = KebidananNurseAssessment(asses, pat, reg);

            var additionalField = new
            {
                Perawat = nurseField,
                PartusHist = ConvertDataTabletoObject(PartusHist(reg.PatientID)),
                PemeriksaanUmum = JsonConvert.DeserializeObject<KidPe>(asses.PhysicalExam),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                //SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                SignImgUrl = (au.SignatureImage != null ? au.SignatureImage : asses.SignImg) ?? new byte[0],
                PatientSignImg = asses.PatientSignImg
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;

        }

        private object KebidananNurseAssessment(PatientAssessment asses, Patient pat, Registration reg)
        {
            // Asesmen Perawat
            var astp = new AppSRAssessmentType();
            astp.LoadByPrimaryKey(asses.SRAssessmentType);

            var phrlines = new PatientHealthRecordLineCollection();
            phrlines.Query.Where(phrlines.Query.RegistrationNo == reg.RegistrationNo,
                phrlines.Query.QuestionFormID == astp.NursingQuestionFormID);
            phrlines.Query.Load();

            var answer = phrlines.FindInSingleGroup("AN001.41");
            var keluhanHamilLain = !string.IsNullOrWhiteSpace(answer.QuestionAnswerText) && answer.QuestionAnswerText.Contains('|') ? answer.QuestionAnswerText.Split('|') : "|".Split('|');

            answer = phrlines.FindInSingleGroup("RM6D.14");
            var riwayatKB = !string.IsNullOrWhiteSpace(answer.QuestionAnswerText) && answer.QuestionAnswerText.Contains('|') ? answer.QuestionAnswerText.Split('|') : "|".Split('|');

            answer = phrlines.FindInSingleGroup("RM6D.16");
            var ginekologi = !string.IsNullOrWhiteSpace(answer.QuestionAnswerText) && answer.QuestionAnswerText.Contains('|') ? answer.QuestionAnswerText.Split('|') : "|".Split('|');

            var perawat = AppUser.GetUserName(answer.LastUpdateByUserID);

            var anteNatalAnswer = phrlines.FindInSingleGroup("RM6D.07");
            var anteNatal = !string.IsNullOrWhiteSpace(anteNatalAnswer.QuestionAnswerText) && anteNatalAnswer.QuestionAnswerText.Contains('|') ? anteNatalAnswer.QuestionAnswerText.Split('|') : "|".Split('|');

            var imunTtAnswer = phrlines.FindInSingleGroup("RM6D.09");
            var imunTT = !string.IsNullOrWhiteSpace(imunTtAnswer.QuestionAnswerText) && imunTtAnswer.QuestionAnswerText.Contains('|') ? imunTtAnswer.QuestionAnswerText.Split('|') : "|".Split('|');

            var frekwensi = phrlines.FindInSingleGroup("RM6D.08").QuestionAnswerSelectionLineID;
            var kehamilanSkrg = new
            {
                IsAnteNatalCare = anteNatalAnswer.QuestionAnswerSelectionLineID == "YATDKJLSKN2",
                AnteNatalCare = anteNatal[1],
                IsFrekwensi1x = frekwensi == "FREKANTE1",
                IsFrekwensi2x = frekwensi == "FREKANTE2",
                IsFrekwensi3x = frekwensi == "FREKANTE3",
                IsFrekwensiUp3x = frekwensi == "FREKANTE4",
                IsImunisasiTT = imunTtAnswer.QuestionAnswerSelectionLineID == "UM.YN1",
                ImunisasiTT = imunTT[1]
            };
            var menstruasi = new
            {
                UmurMenar = phrlines.FindInSingleGroup("UM001").QuestionAnswerText,
                LamaHaid = phrlines.FindInSingleGroup("MOBS1.003").QuestionAnswerText,
                GantiPemblt = phrlines.FindInSingleGroup("GP0001").QuestionAnswerText,
                Hpht = phrlines.FindInSingleGroup("HP0001").QuestionAnswerText,
                TaksiranPrslnan = phrlines.FindInSingleGroup("RM6D.01").QuestionAnswerText,
                IsDismenorhoe = phrlines.FindInSingleGroup("RM6D.02").QuestionAnswerText == "1",
                IsSpotting = phrlines.FindInSingleGroup("RM6D.03").QuestionAnswerText == "1",
                IsMenorrhagia = phrlines.FindInSingleGroup("RM6D.04").QuestionAnswerText == "1",
                IsMetrorhagia = phrlines.FindInSingleGroup("RM6D.05").QuestionAnswerText == "1"
            };
            var perkawinan = new
            {
                IsKawin = phrlines.FindInSingleGroup("RM23.10.27").QuestionAnswerSelectionLineID == "2",
                IsBelumkawin = phrlines.FindInSingleGroup("RM23.10.27").QuestionAnswerSelectionLineID == "1",
                IsJanda = phrlines.FindInSingleGroup("RM23.10.27").QuestionAnswerSelectionLineID == "3",
                JmlKawinIstri = phrlines.FindInSingleGroup("DSR.MR.XW").QuestionAnswerText,
                JmlKawinSuami = phrlines.FindInSingleGroup("DSR.MR.XH").QuestionAnswerText,
                UsiaPerkwn = phrlines.FindInSingleGroup("UP0001").QuestionAnswerText
            };
            var kehamilanDulu = new
            {
                G = phrlines.FindInSingleGroup("RM6J.01.G").QuestionAnswerText,
                P = phrlines.FindInSingleGroup("RM6J.01.P").QuestionAnswerText,
                A = phrlines.FindInSingleGroup("RM6J.01.A").QuestionAnswerText,
            };
            var keluhanHamil = new
            {
                IsMual = phrlines.FindInSingleGroup("RM2AD.0105").QuestionAnswerText == "1",
                IsMuntah = phrlines.FindInSingleGroup("HD.I.MUN").QuestionAnswerText == "1",
                IsPendarahan = phrlines.FindInSingleGroup("RM23.02.40").QuestionAnswerText == "1",
                IsSakitKepala = phrlines.FindInSingleGroup("RM23.08.53").QuestionAnswerText == "1",
                IsPusing = phrlines.FindInSingleGroup("RM23.02.25").QuestionAnswerText == "1",
                IsLainLain = keluhanHamilLain[0] == "1",
                KeluhanLain = keluhanHamilLain[1]
            };

            var penyakitKeluarga = FamilyMedicalHistory.ToString(reg.PatientID);

            var polaEliminasi = new
            {
                BAK = phrlines.FindInSingleGroup("RM6D.19").QuestionAnswerText,
                BAKWarna = phrlines.FindInSingleGroup("RM6D.20").QuestionAnswerText,
                BAB = phrlines.FindInSingleGroup("RM6D.21").QuestionAnswerText,
                BABKarakteristik = phrlines.FindInSingleGroup("RM6D.22").QuestionAnswerText,
                Tidur = phrlines.FindInSingleGroup("RM6D.24").QuestionAnswerText

            };
            // Perawt Entry
            var retval = new
            {
                Menstruasi = menstruasi,
                Perkawinan = perkawinan,
                KehamilanDulu = kehamilanDulu,
                KehamilanSkrg = kehamilanSkrg,
                KeluhanHamil = keluhanHamil,
                PenyakitKeluarga = new
                {
                    IsTidakAda = string.IsNullOrWhiteSpace(penyakitKeluarga),
                    IsAda = !string.IsNullOrWhiteSpace(penyakitKeluarga),
                    PenyakitKeluarga = penyakitKeluarga
                },
                RiwayatKB = new
                {
                    IsKB = riwayatKB[0] == "1",
                    IsTidakKB = riwayatKB[0] != "1",
                    JenisKB = riwayatKB[1],
                    LamaKB = phrlines.FindInSingleGroup("RM6D.15").QuestionAnswerText
                },

                RiwayatGinokologi = new
                {
                    IsAda = ginekologi[0] == "ADATDKSBT02",
                    RiwayatGinokologi = ginekologi[1]
                },

                PolaEliminasi = polaEliminasi,

                PemeriksaanUmum = new
                {
                    Keadaan = phrlines.FindInSingleGroup(PhrUtil.Pemeriksaan.Keadaan).QuestionAnswerText,
                    Kesadaran = phrlines.FindInSingleGroup(PhrUtil.Pemeriksaan.Kesadaran).QuestionAnswerText,
                    VitalSign = VitalSignAsmByNurse(phrlines),

                },
                PerawatPengkji = perawat

            };
            return retval;
        }

        private static DataTable PartusHist(string patientId)
        {
            var que = new PatientChildBirthHistoryQuery("a");
            var stdi = new AppStandardReferenceItemQuery("i");
            que.LeftJoin(stdi).On(que.SRBirthMethod == stdi.ItemID & stdi.StandardReferenceID == "BirthMethod");
            que.Where(que.PatientID == patientId);
            que.Select(que.ChildBirth.As("TahunPartus"),
                que.Location.As("TempatPartus"),
                "<CONVERT(VARCHAR, a.PregnanDurationMonth)+ 'B '+  CONVERT(VARCHAR, a.PregnanDurationWeek) +'M' as UmurHamil>",
                stdi.ItemName.As("JenisPersalinan"),
                que.Helper.As("Penolong"),
                que.Complication.As("Penyulit"),
                "<CASE WHEN a.Sex='F' THEN 'W' ELSE 'L' END + '/' + a.BBL +'gram' as JkBl>",
                que.Notes

                );
            var dtb = que.LoadDataTable();

            dtb.Columns.Add("RowNo", typeof(int));

            var no = 1;
            foreach (DataRow row in dtb.Rows)
            {
                row["RowNo"] = no;
                no++;
            }

            return dtb;
        }

        #endregion

        /// <summary>
        /// ASESMEN AWAL RAWAT JALAN KLINIK MATA
        /// </summary>
        public object EyeClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var eyeLeft = "EYELEFT";
            var eyeRight = "EYERIGHT";
            var fundoskopi = "FM";
            var mata = "mt1";
            var eyeLeftNotes = string.Empty;
            var eyeRightNotes = string.Empty;
            var fundoskopiNotes = string.Empty;
            var mataNotes = string.Empty;

            // Get last update Localist
            var lcls = new RegistrationInfoMedicBodyDiagramCollection();
            lcls.Query.Where(lcls.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            lcls.Query.Select(lcls.Query.BodyID, lcls.Query.Notes);
            lcls.Query.Where(lcls.Query.Or(lcls.Query.BodyID == eyeLeft, lcls.Query.BodyID == eyeRight, lcls.Query.BodyID == fundoskopi, lcls.Query.BodyID == mata));
            if (lcls.Query.Load())
            {
                foreach (var line in lcls)
                {
                    //if (line.BodyID == eyeLeft)
                    //    eyeLeftNotes = line.Notes;
                    //else
                    //eyeRightNotes = line.Notes;
                    //fundoskopiNotes = line.Notes;
                    //mataNotes = line.Notes;

                    switch (line.BodyID)
                    {
                        case "EYELEFT":
                            eyeLeftNotes = line.Notes;
                            break;
                        case "EYERIGHT":
                            eyeRightNotes = line.Notes;
                            break;
                        case "FM":
                            fundoskopiNotes = line.Notes;
                            break;
                        case "mt1":
                            mataNotes = line.Notes;
                            break;
                    }


                }
            }

            //object retField;
            var pe = JsonConvert.DeserializeObject<EyePe>(asses.PhysicalExam);
            pe.Condition = TranslateCondition(pe.Condition);

            //var loc = new
            //{
            //    EyeRightUrl = LocalistUrl(asses, eyeRight),
            //    EyeRightNotes = eyeRightNotes,
            //    EyeLeftUrl = LocalistUrl(asses, eyeLeft),
            //    EyeLeftNotes = eyeLeftNotes,
            //};
            //retField = OutPatientStd(asses, pat, reg, mergeRegs, pe, loc);

            var additionalField = new
            {
                PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value), JsonConvert.DeserializeObject<EyePe>(asses.PhysicalExam), pe),
                EyeRightUrl = LocalistUrl(asses, eyeRight),
                EyeRightNotes = eyeRightNotes,
                EyeLeftUrl = LocalistUrl(asses, eyeLeft),
                EyeLeftNotes = eyeLeftNotes,

                fundoskopiUrl = LocalistUrl(asses, fundoskopi),
                FundoskopiNotes = fundoskopiNotes,
                mataUrl = LocalistUrl(asses, mata),
                MataNotes = mataNotes,




                //SignImgUrl = asses.SignImg,
                SignImgUrl = (au.SignatureImage != null ? au.SignatureImage : asses.SignImg) ?? new byte[0],
                PatientSignImg = asses.PatientSignImg
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }


        /// <summary>
        /// ASESMEN AWAL RAWAT JALAN KLINIK BEDAH
        /// </summary>
        public object SurgicalClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var bodyId = "PLOP";
            var bodyNotes = string.Empty;
            // Get last update Localist
            var lcls = new RegistrationInfoMedicBodyDiagram();
            lcls.Query.Where(lcls.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            lcls.Query.Select(lcls.Query.BodyID, lcls.Query.Notes);
            lcls.Query.es.Top = 1;
            lcls.Query.OrderBy(lcls.Query.LastUpdateDateTime.Descending);
            if (lcls.Query.Load())
            {
                bodyId = lcls.BodyID;
                bodyNotes = lcls.Notes;
            }

            object retField;
            var pe = JsonConvert.DeserializeObject<SurgicalPe>(asses.PhysicalExam);
            pe.Condition = TranslateCondition(pe.Condition);

            var loc = new
            {
                LocalistUrl = LocalistUrl(asses, bodyId),
                LocalistNotes = bodyNotes
            };
            retField = OutPatientStd(asses, pat, reg, au, mergeRegs, pe, loc);
            return retField;
        }


        /// <summary>
        /// ASESMEN AWAL RAWAT JALAN KLINIK PENYAKIT DALAM
        /// </summary>
        public object InternalDiseaseClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var additionalField = new
            {
                PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value),
                    JsonConvert.DeserializeObject<InternalPe>(asses.PhysicalExam)),
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }

        /// <summary>
        /// Asesmen awal rawat jalan klinik THT - KL
        /// </summary>
        public object ThtClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var telinga = "THT004";
            var hidung = "THT003";
            var tgrkan = "THT002";

            var telingaNotes = string.Empty;
            var hidungNotes = string.Empty;
            var tgrkanNotes = string.Empty;

            var lcls = new RegistrationInfoMedicBodyDiagramCollection();
            lcls.Query.Where(lcls.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            lcls.Query.Select(lcls.Query.BodyID, lcls.Query.Notes);
            lcls.Query.Where(lcls.Query.Or(lcls.Query.BodyID == telinga, lcls.Query.BodyID == hidung, lcls.Query.BodyID == tgrkan));
            if (lcls.Query.Load())
            {
                foreach (var line in lcls)
                {
                    if (line.BodyID == telinga)
                        telingaNotes = line.Notes;
                    else if (line.BodyID == hidung)
                        hidungNotes = line.Notes;
                    else if (line.BodyID == tgrkan)
                        tgrkanNotes = line.Notes;
                }
            }

            object retField;
            var pe = JsonConvert.DeserializeObject<ThtPe>(asses.PhysicalExam);
            pe.Condition = TranslateCondition(pe.Condition);

            var loc = new
            {
                ImgTelingatUrl = LocalistUrl(asses, telinga),
                ImgHidungUrl = LocalistUrl(asses, hidung),
                ImgTgrkanUrl = LocalistUrl(asses, tgrkan),
                ImgTelingaNotes = telingaNotes,
                ImgHidungNotes = hidungNotes,
                ImgTgrkanNotes = tgrkanNotes,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg,
                LocalistTelingaUrl = LocalistUrl(asses, "EAR001"),
                LocalisTriangleUrl = LocalistUrl(asses, "THT006"),
                LocalisMulutUrl = LocalistUrl(asses, "THT001")
            };
            retField = OutPatientStd(asses, pat, reg, au, mergeRegs, pe, loc);
            return retField;
        }

        /// <summary>
        /// ASESMEN AWAL RAWAT JALAN GIGI
        /// </summary>
        public object DentisClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            // Initial Odontogram
            var odon = new PatientOdontogramQuery();
            odon.Where(odon.RegistrationNo == reg.RegistrationNo);
            odon.es.Top = 1;
            odon.OrderBy(odon.OdontogramDateTime.Ascending);
            var dtbOdon = odon.LoadDataTable();

            //Ambil GCS
            var pe = JsonConvert.DeserializeObject<GeneralPe>(asses.PhysicalExam);

            var additionalField = new
            {
                PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value),
                    JsonConvert.DeserializeObject<DentisPe>(asses.PhysicalExam), pe),
                ExternalDrugs = ConvertDataTabletoObject(ExternalDrugs(reg.RegistrationNo, reg.FromRegistrationNo)),
                Odon = ConvertDataTabletoObject(dtbOdon),
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg,

            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }

        #region Neurology  Assessment
        /// <summary>
        /// ASESMEN AWAL RAWAT JALAN KLINIK NEUROLOGY
        /// Neurology Clinic Outpatient Initial Assessment
        /// </summary>
        public object NeurologyClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var vsField = AssessmentUtil.VitalSignField(mergeRegs, asses.AssessmentDateTime.Value);
            var nurseField = string.Empty;

            var additionalField = new
            {
                Perawat = nurseField,
                PemeriksaanUmum = JsonConvert.DeserializeObject<NeurologiPe>(asses.PhysicalExam),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg,
                DiagnosaLengkap = Diagnosis(asses)
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }


        #endregion

        #region Skin  Assessment
        /// <summary>
        /// ASESMEN AWAL RAWAT JALAN KLINIK KULIT KELAMIN
        /// SKIN Clinic Outpatient Initial Assessment
        /// </summary>
        public object SkinClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var vsField = AssessmentUtil.VitalSignField(mergeRegs, asses.AssessmentDateTime.Value);
            var nurseField = string.Empty;

            var additionalField = new
            {
                Perawat = nurseField,
                PemeriksaanUmum = JsonConvert.DeserializeObject<SkinPe>(asses.PhysicalExam),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }


        #endregion

        #region Lung  Assessment
        /// <summary>
        /// ASESMEN AWAL RAWAT JALAN KLINIK PARU
        /// SKIN Clinic Outpatient Initial Assessment
        /// </summary>
        public object lungClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var vsField = AssessmentUtil.VitalSignField(mergeRegs, asses.AssessmentDateTime.Value);
            var nurseField = string.Empty;

            ///////Localist
            //var lanjtnNotes = string.Empty;
            //var lanjtn = new RegistrationInfoMedicBodyDiagram();
            //if (lanjtn.LoadByPrimaryKey(asses.RegistrationInfoMedicID, "LIGD02"))
            //    lanjtnNotes = lanjtn.Notes;

            var additionalField = new
            {
                Perawat = nurseField,
                PemeriksaanUmum = JsonConvert.DeserializeObject<LungPe>(asses.PhysicalExam),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg,
                LocalistUrl = LocalistUrl(asses, "PARU 2")
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }


        #endregion

        #region HEART  Assessment
        /// <summary>
        /// ASESMEN AWAL RAWAT JALAN KLINIK Jantung
        /// SKIN Clinic Outpatient Initial Assessment
        /// </summary>
        public object HEARTClinic(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var vsField = AssessmentUtil.VitalSignField(mergeRegs, asses.AssessmentDateTime.Value);
            var nurseField = string.Empty;

            var additionalField = new
            {
                Perawat = nurseField,
                PemeriksaanUmum = JsonConvert.DeserializeObject<HeartPe>(asses.PhysicalExam),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }


        #endregion

        #region REHAB  Assessment
        /// <summary>
        /// ASESMEN AWAL RAWAT JALAN REHAB
        /// REHAB Clinic Outpatient Initial Assessment
        /// </summary>
        public object REHAB(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var vsField = AssessmentUtil.VitalSignField(mergeRegs, asses.AssessmentDateTime.Value);
            var nurseField = string.Empty;

            var additionalField = new
            {
                Perawat = nurseField,
                PemeriksaanUmum = JsonConvert.DeserializeObject<RehabilitationPe>(asses.PhysicalExam),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }


        #endregion

        private DataTable ExternalDrugs(string registrationNo, string fromRegistrationNo)
        {
            var mr = new MedicationReceiveQuery("mr");
            var cm = new ConsumeMethodQuery("cm");
            mr.InnerJoin(cm).On(mr.SRConsumeMethod == cm.SRConsumeMethod);

            var xd = new MedicationReceiveFromPatientQuery("xd");
            mr.InnerJoin(xd).On(mr.MedicationReceiveNo == xd.MedicationReceiveNo);

            mr.Where(mr.Or(mr.RegistrationNo == fromRegistrationNo, mr.RegistrationNo == registrationNo));
            mr.Select(mr.ItemDescription, cm.SRConsumeMethodName, mr.ConsumeQty, mr.SRConsumeUnit, mr.Note, xd.Reason, xd.Duration, mr.IsContinue);
            mr.OrderBy(mr.MedicationReceiveNo.Descending);
            var dtb = mr.LoadDataTable();
            dtb.Columns.Add("ConsumeMethod", typeof(string));
            dtb.Columns.Add("RowNo", typeof(int));

            var no = 1;
            foreach (DataRow row in dtb.Rows)
            {
                row["ConsumeMethod"] = string.Format("{0} @{1} {2}", row["SRConsumeMethodName"], row["ConsumeQty"],
                    row["SRConsumeUnit"]);
                row["RowNo"] = no;
                no++;
            }

            return dtb;
        }

        #endregion

        #region Rawat Inap
        /// <summary>
        /// ASESMEN MEDIS RAWAT INAP PASIEN BEDAH
        /// </summary>
        private object SurgicalInPatient(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var bodyId = "PLOP";
            var bodyNotes = string.Empty;
            // Get last update Localist
            var lcls = new RegistrationInfoMedicBodyDiagram();
            lcls.Query.Where(lcls.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            lcls.Query.Select(lcls.Query.BodyID, lcls.Query.Notes);
            lcls.Query.es.Top = 1;
            lcls.Query.OrderBy(lcls.Query.LastUpdateDateTime.Descending);
            if (lcls.Query.Load())
            {
                bodyId = lcls.BodyID;
                bodyNotes = lcls.Notes;
            }

            object retField;
            var pe = JsonConvert.DeserializeObject<SurgicalPe>(asses.PhysicalExam);
            pe.Condition = TranslateCondition(pe.Condition);

            var loc = new
            {
                LocalistUrl = LocalistUrl(asses, bodyId),
                LocalistNotes = bodyNotes
            };

            retField = InPatientStd(asses, pat, reg, au, mergeRegs, pe, loc);
            return retField;
        }

        /// <summary>
        /// ASESMEN MEDIS RAWAT INAP PASIEN THT-KL
        /// </summary>
        private object ThtInPatient(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            //object retField;
            //var pe = JsonConvert.DeserializeObject<ThtPe>(asses.PhysicalExam);
            //pe.Condition = TranslateCondition(pe.Condition);

            var telinga = "THT004";
            var hidung = "THT003";
            var tgrkan = "THT002";

            var telingaNotes = string.Empty;
            var hidungNotes = string.Empty;
            var tgrkanNotes = string.Empty;

            var lcls = new RegistrationInfoMedicBodyDiagramCollection();
            lcls.Query.Where(lcls.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            lcls.Query.Select(lcls.Query.BodyID, lcls.Query.Notes);
            lcls.Query.Where(lcls.Query.Or(lcls.Query.BodyID == telinga, lcls.Query.BodyID == hidung, lcls.Query.BodyID == tgrkan));
            if (lcls.Query.Load())
            {
                foreach (var line in lcls)
                {
                    if (line.BodyID == telinga)
                        telingaNotes = line.Notes;
                    else if (line.BodyID == hidung)
                        hidungNotes = line.Notes;
                    else if (line.BodyID == tgrkan)
                        tgrkanNotes = line.Notes;
                }
            }

            object retField;
            var pe = JsonConvert.DeserializeObject<ThtPe>(asses.PhysicalExam);
            pe.Condition = TranslateCondition(pe.Condition);

            var loc = new
            {
                ImgTelingatUrl = LocalistUrl(asses, telinga),
                ImgHidungUrl = LocalistUrl(asses, hidung),
                ImgTgrkanUrl = LocalistUrl(asses, tgrkan),
                ImgTelingaNotes = telingaNotes,
                ImgHidungNotes = hidungNotes,
                ImgTgrkanNotes = tgrkanNotes
            };

            retField = InPatientStd(asses, pat, reg, au, mergeRegs, pe, loc);
            return retField;
        }

        /// <summary>
        /// ASESMEN AWAL RAWAT INAP  ANAK 
        /// </summary>
        public object KidInPatient(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var nurseField = KidNurseAssessment(asses, pat, reg);


            var bodyId = "KidBody";
            var bodyNotes = string.Empty;
            // Get last update Localist
            var lcls = new RegistrationInfoMedicBodyDiagram();
            lcls.Query.Where(lcls.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            lcls.Query.Select(lcls.Query.BodyID, lcls.Query.Notes);
            lcls.Query.es.Top = 1;
            lcls.Query.OrderBy(lcls.Query.LastUpdateDateTime.Descending);
            if (lcls.Query.Load())
            {
                bodyId = lcls.BodyID;
                bodyNotes = lcls.Notes;
            }
            var localist = new
            {
                LocalistUrl = LocalistUrl(asses, bodyId),
                LocalistNotes = bodyNotes
            };

            var peObj = JsonConvert.DeserializeObject<KidPe>(asses.PhysicalExam);
            var additionalField = new
            {
                Perawat = nurseField,
                PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value), peObj, localist),
                HandOver = AssessmentUtil.TransferToInPatient(reg.FromRegistrationNo),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg,
                DischargePlan = new
                {
                    LamaRawat = asses.EstimatedDayInPatient,
                    Pulang = (asses.DischargeDatePlan == null ? reg.RegistrationDate.Value.AddDays(asses.EstimatedDayInPatient ?? 1) : asses.DischargeDatePlan.Value).ToString(AppConstant.DisplayFormat.DateShortMonth),
                    TindakanMedis = asses.DischargeMedicalPlan
                }
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;

        }

        /// <summary>
        /// ASESMEN AWAL RAWAT INAP KEBIDANAN 
        /// </summary>
        public object KebidananIpr(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var vsField = AssessmentUtil.VitalSignField(mergeRegs, asses.AssessmentDateTime.Value);

            // Perawt Entry
            var nurseField = new
            {
                Menstruasi = new
                {
                    UmurMenar = string.Empty,
                    LamaHaid = string.Empty,
                    GantiPemblt = string.Empty,
                    Hpht = string.Empty,
                    TaksiranPrslnan = string.Empty,
                    IsDismenorhoe = false,
                    IsSpotting = false,
                    IsMenorrhagia = false,
                    IsMetrorhagia = false

                },
                Perkawinan = new
                {
                    IsKawin = false,
                    IsBelumkawin = false,
                    IsJanda = false,
                    JmlKawinIstri = 2,
                    JmlKawinSuami = 1,
                    UsiaPerkwn = string.Empty
                },

                PemeriksaanUmum = new
                {
                    Keadaan = string.Empty,
                    Kesadaran = string.Empty,
                    VitalSign = vsField
                },
                PerawatPengkji = string.Empty
            };


            // Dokter Entry
            var peField = new
            {
                PemeriksaanUmum = MergeJsonData(new { VitalSign = vsField },
                    JsonConvert.DeserializeObject<NursingPe>(asses.PhysicalExam)),
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime), new
            {
                Perawat = nurseField,
                Dokter = MergeJsonData(AssessmentUtil.PhysicianCommonField(pat, reg, asses), peField)
            });

            return retField;

        }


        /// <summary>
        /// ASESMEN MEDIS RAWAT INAP PASIEN PENYAKIT MATA
        /// </summary>
        public object EyeInPatient(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var eyeLeft = "EYELEFT";
            var eyeRight = "EYERIGHT";
            var eyeLeftNotes = string.Empty;
            var eyeRightNotes = string.Empty;
            // Get last update Localist
            var lcls = new RegistrationInfoMedicBodyDiagramCollection();
            lcls.Query.Where(lcls.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            lcls.Query.Select(lcls.Query.BodyID, lcls.Query.Notes);
            lcls.Query.Where(lcls.Query.Or(lcls.Query.BodyID == eyeLeft, lcls.Query.BodyID == eyeRight));
            if (lcls.Query.Load())
            {
                foreach (var line in lcls)
                {
                    if (line.BodyID == eyeLeft)
                        eyeLeftNotes = line.Notes;
                    else
                        eyeRightNotes = line.Notes;
                }
            }

            //object retField;
            var pe = JsonConvert.DeserializeObject<EyePe>(asses.PhysicalExam);
            pe.Condition = TranslateCondition(pe.Condition);


            var additionalField = new
            {

                EyeRightUrl = LocalistUrl(asses, eyeRight),
                EyeRightNotes = eyeRightNotes,
                EyeLeftUrl = LocalistUrl(asses, eyeLeft),
                EyeLeftNotes = eyeLeftNotes,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }

        /// <summary>
        /// ASESMEN MEDIS RAWAT INAP PASIEN NEONATUS
        /// </summary>

        public object NeonatusInPatient(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var vsField = AssessmentUtil.VitalSignField(mergeRegs, asses.AssessmentDateTime.Value);

            //var nurseField = KidNurseAssessment(asses, pat, reg);
            var nurseField = string.Empty;

            var bodyId = "KidBody";
            var bodyNotes = string.Empty;
            // Get last update Localist
            var lcls = new RegistrationInfoMedicBodyDiagram();
            lcls.Query.Where(lcls.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            lcls.Query.Select(lcls.Query.BodyID, lcls.Query.Notes);
            lcls.Query.es.Top = 1;
            lcls.Query.OrderBy(lcls.Query.LastUpdateDateTime.Descending);
            if (lcls.Query.Load())
            {
                bodyId = lcls.BodyID;
                bodyNotes = lcls.Notes;
            }
            var localist = new
            {
                LocalistUrl = LocalistUrl(asses, bodyId),
                LocalistNotes = bodyNotes
            };

            var peObj = JsonConvert.DeserializeObject<KidPe>(asses.PhysicalExam);
            var additionalField = new
            {
                Perawat = nurseField,
                PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value), peObj, localist),
                HandOver = AssessmentUtil.TransferToInPatient(reg.FromRegistrationNo),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg,
                DischargePlan = new
                {
                    LamaRawat = asses.EstimatedDayInPatient,
                    Pulang = (asses.DischargeDatePlan == null ? reg.RegistrationDate.Value.AddDays(asses.EstimatedDayInPatient ?? 1) : asses.DischargeDatePlan.Value).ToString(AppConstant.DisplayFormat.DateShortMonth),
                    TindakanMedis = asses.DischargeMedicalPlan
                }
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }



        /// <summary>
        /// ASESMEN MEDIS RAWAT INAP PASIEN NEUROLOGY
        /// </summary>
        public object NeurologiInPatient(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs)
        {
            var vsField = AssessmentUtil.VitalSignField(mergeRegs, asses.AssessmentDateTime.Value);

            //var nurseField = KidNurseAssessment(asses, pat, reg);
            var nurseField = string.Empty;

            var bodyId = "KidBody";
            var bodyNotes = string.Empty;
            // Get last update Localist
            var lcls = new RegistrationInfoMedicBodyDiagram();
            lcls.Query.Where(lcls.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
            lcls.Query.Select(lcls.Query.BodyID, lcls.Query.Notes);
            lcls.Query.es.Top = 1;
            lcls.Query.OrderBy(lcls.Query.LastUpdateDateTime.Descending);
            if (lcls.Query.Load())
            {
                bodyId = lcls.BodyID;
                bodyNotes = lcls.Notes;
            }
            var localist = new
            {
                LocalistUrl = LocalistUrl(asses, bodyId),
                LocalistNotes = bodyNotes
            };

            var peObj = JsonConvert.DeserializeObject<NeurologiPeIp>(asses.PhysicalExam);
            var additionalField = new
            {
                Perawat = nurseField,
                PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value), peObj, localist),
                HandOver = AssessmentUtil.TransferToInPatient(reg.FromRegistrationNo),
                PemeriksaanPenunjang = asses.OtherExam,
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg,
                DischargePlan = new
                {
                    LamaRawat = asses.EstimatedDayInPatient,
                    Pulang = (asses.DischargeDatePlan == null ? reg.RegistrationDate.Value.AddDays(asses.EstimatedDayInPatient ?? 1) : asses.DischargeDatePlan.Value).ToString(AppConstant.DisplayFormat.DateShortMonth),
                    TindakanMedis = asses.DischargeMedicalPlan
                }
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;
        }


        /// <summary>
        /// ASESMEN GENERAL AWAL RAWAT INAP
        /// General Inpatient Intial Assessment
        /// </summary>
        /// Create by : Fajri
        /// Create Date : 28-March-2023
        /// Req Client : RSYS
        public object GeneralInitialInPatient(PatientAssessment asses, Patient pat, Registration reg, AppUser au, List<string> mergeRegs, object peObj)
        {
            var pe = JsonConvert.DeserializeObject<GeneralPe>(asses.PhysicalExam);
            pe.Condition = TranslateCondition(pe.Condition);

            var additionalField = new
            {
                PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(mergeRegs, asses.AssessmentDateTime.Value), peObj),
                DiagnosaLengkap = Diagnosis(asses),
                HandOver = AssessmentUtil.TransferToInPatient(reg.FromRegistrationNo),
                PemeriksaanPenunjang = asses.OtherExam,
                DischargePlan = ConvertDataTabletoObject(DischargePlanning(asses.RegistrationInfoMedicID)),
                //SignImgUrl = asses.SignImg,
                SignImgUrl = !string.IsNullOrEmpty(Convert.ToString(au.SignatureImage)) ? Encoding.UTF8.GetBytes(Convert.ToString(au.SignatureImage)) : asses.SignImg,
                PatientSignImg = asses.PatientSignImg,
                Kesadaran = new
                {
                    Eye = StandardReference.GetItemName(AppEnum.StandardReference.GcsEye, pe.Consciousness.Eye.Code) + " (" + pe.Consciousness.Eye.Score + ")",
                    Motor = StandardReference.GetItemName(AppEnum.StandardReference.GcsMotor, pe.Consciousness.Motor.Code) + " (" + pe.Consciousness.Motor.Score + ")",
                    Verbal = StandardReference.GetItemName(AppEnum.StandardReference.GcsVerbal, pe.Consciousness.Verbal.Code) + " (" + pe.Consciousness.Verbal.Score + ")"
                }
            };

            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

            return retField;

        }

        ///// <summary>
        ///// ASESMEN AWAL RAWAT JALAN KLINIK BEDAH
        ///// </summary>
        //public void RM6M(string accessKey, string registrationInfoMedicID)
        //{
        //    try
        //    {
        //        var reg = new Registration();
        //        var pat = new Patient();
        //        var asses = new PatientAssessment();
        //        if (ValidateParameterAndLoadAssessment(accessKey, registrationInfoMedicID, ref asses, ref pat, ref reg))
        //        {
        //            // Update Image
        //            var loc = new RegistrationInfoMedicBodyDiagramCollection();
        //            var lokalis = string.Empty;
        //            loc.Query.Where(loc.Query.RegistrationInfoMedicID == asses.RegistrationInfoMedicID);
        //            if (loc.LoadAll())
        //            {
        //                if (loc.Count > 0 && loc[0] != null)
        //                {
        //                    lokalis = ImageHelper.ConvertByteArrayToImage(loc[0].BodyImage).ToBase64String(ImageFormat.Jpeg);

        //                }

        //                //if (loc.Count > 1 && loc[1] != null)
        //                //{
        //                //    picLocalistStatus02.Value = ImageHelper.ConvertByteArrayToImage(loc[1].BodyImage);
        //                //}
        //            }

        //            var additionalField = new
        //            {
        //                PemeriksaanUmum = MergeJsonData(AssessmentUtil.VitalSignFolderField(reg.RegistrationNo, reg.FromRegistrationNo, asses.AssessmentDateTime.Value),
        //                    JsonConvert.DeserializeObject<SurgicalPe>(asses.PhysicalExam)),
        //                StatusLokalis = lokalis,
        //                HandOver = AssessmentUtil.TransferToInPatient(reg)
        //            };

        //            var retField = MergeJsonData(HeaderField(pat, reg, asses.AssessmentDateTime),
        //                AssessmentUtil.PhysicianCommonField(pat, reg, asses), additionalField);

        //            ResponseWrite(retField);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Context.Response.Write(ex.Message);
        //    }
        //}



        #endregion

        /// <summary>
        /// Tambahan u/ cetakan assessment igd : Esi, Diagnosis Utama, Diagnosis Banding, ICD X
        /// </summary>
        /// Create By: Fajri
        /// Create Date: 2023-March-07
        /// Clinet Req: RSYS
        private static DataTable EsiCondition(string standardRefId, string[] itemIds)
        {
            if (itemIds != null && itemIds.Length > 0)
            {
                var srItem = new AppStandardReferenceItemQuery("a");
                srItem.Where(srItem.StandardReferenceID == standardRefId);
                srItem.Where(srItem.ItemID.In(itemIds));
                srItem.Where(srItem.Or(srItem.ReferenceID.In("CDT", "AWS", "BRT", "CIR", "AWY")));
                srItem.Select(srItem.ItemID, srItem.ItemName);
                srItem.OrderBy(srItem.ItemName.Ascending);
                return srItem.LoadDataTable();
            }
            else
            {
                var dtb = new DataTable();
                dtb.Columns.Add("ItemID", typeof(string));
                dtb.Columns.Add("ItemName", typeof(string));
                return dtb;
            }
        }
        public static object Diagnosis(PatientAssessment asses)
        {
            var diagMain = string.Empty;
            var icdMain = string.Empty;
            var diagBanding = string.Empty;
            var icdBanding = string.Empty;

            var diags = new EpisodeDiagnoseCollection();
            diags.Query.Where(diags.Query.RegistrationNo == asses.RegistrationNo);
            diags.LoadAll();

            foreach (EpisodeDiagnose diag in diags)
            {
                if (diag.SRDiagnoseType == "DiagnoseType-001")
                {
                    diagMain = diag.DiagnosisText;
                    icdMain = diag.DiagnoseID;
                }
                if (diag.SRDiagnoseType == "DiagnoseType-003")
                {
                    diagBanding = diag.DiagnosisText;
                    icdBanding = diag.DiagnoseID;
                }
            }

            var obj = new
            {
                Kerja = diagMain,
                IcdXKerja = icdMain,
                IcdXBanding = diagBanding,
                IcdXBandingKode = icdBanding,
                Banding = asses.DiagnoseDiff,
                Utama = asses.Diagnose
            };

            return obj;
        }

        /// <summary>
        /// Tambahan u/ cetakan assessment general inpatient, neurology inpatient, neurology outpatient
        /// </summary>
        /// Create By: Fajri
        /// Create Date: 2023-April-14
        /// Clinet Req: RSYS
        private static DataTable DischargePlanning(string rim)
        {
            var dtbQuest = new QuestionQuery("q");
            var questIG = new QuestionInGroupQuery("qig");
            dtbQuest.InnerJoin(questIG).On(questIG.QuestionID == dtbQuest.QuestionID);
            dtbQuest.Where(questIG.QuestionGroupID == "ASKR.PLG");
            dtbQuest.OrderBy(dtbQuest.IndexNo.Ascending);
            dtbQuest.Select(dtbQuest.QuestionID, dtbQuest.QuestionText, dtbQuest.SRAnswerType);
            var dtb = dtbQuest.LoadDataTable();
            dtb.Columns.Add("RowIndex", typeof(int));
            dtb.Columns.Add("SubRowIndex", typeof(int));
            dtb.Columns.Add("ParentQuestionID", typeof(string));
            var i = 0;
            var newRows = new List<DataRow>();

            foreach (DataRow rowQuestion in dtb.Rows)
            {
                i++;
                var quest = new QuestionQuery();
                quest.Where(quest.ParentQuestionID == rowQuestion["QuestionID"], quest.SRAnswerType != string.Empty);
                quest.OrderBy(quest.IndexNo.Ascending);
                var subQuest = quest.LoadDataTable();
                rowQuestion["RowIndex"] = i;
                var j = 0;

                foreach (DataRow rowSubQuest in subQuest.Rows)
                {
                    j++;
                    DataRow newRow = dtb.NewRow();
                    newRow["QuestionID"] = rowSubQuest["QuestionID"];
                    newRow["QuestionText"] = rowSubQuest["QuestionText"];
                    newRow["SubRowIndex"] = j;
                    newRow["RowIndex"] = i;
                    newRow["SRAnswerType"] = rowSubQuest["SRAnswerType"];
                    newRow["ParentQuestionID"] = rowSubQuest["ParentQuestionID"];
                    newRows.Add(newRow);
                }
            }

            foreach (DataRow newRow in newRows)
            {
                dtb.Rows.Add(newRow);
            }

            dtb.DefaultView.Sort = "RowIndex ASC";
            var dtbSkrinning = dtb.DefaultView.ToTable();
            dtbSkrinning.Columns.Add("IsYes", typeof(bool));
            dtbSkrinning.Columns.Add("Notes", typeof(string));

            var paQuest = new PatientAssessmentQuestField();
            if (paQuest.LoadByPrimaryKey(rim, "ASKR.PLG"))
            {
                var qaSkrn = JsonConvert.DeserializeObject<QuestionGroupAnswerValue>(paQuest.QuestionAnswer);
                foreach (DataRow row in dtbSkrinning.Rows)
                {
                    var answerValue = qaSkrn.QuestionAnswerValues.FirstOrDefault(x => x.QuestionID == row["QuestionID"].ToString());
                    if (answerValue != null)
                    {
                        row["IsYes"] = answerValue.QuestionAnswerSelectionLineID == "Y";
                        row["Notes"] = answerValue.QuestionAnswerText.Length > 1 ? answerValue.QuestionAnswerText.Split('|')[1] : string.Empty;
                    }
                    else
                    {
                        row["IsYes"] = false;
                    }
                }
            }
            else
            {
                foreach (DataRow row in dtbSkrinning.Rows)
                {
                    row["IsYes"] = false;
                    row["Notes"] = string.Empty;
                }
            }
            return dtbSkrinning;
        }
    }
}
