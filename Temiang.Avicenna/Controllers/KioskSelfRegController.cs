using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.RADT;
using Temiang.Avicenna.Module.RADT.Master;
using Temiang.Avicenna.WebService;
using Temiang.Avicenna.Common.BPJS.VClaim.v11;

namespace Temiang.Avicenna.Controllers
{
    public class KioskselfregController : BaseController
    {
        private void InitLocalPage()
        {
            // 
            ViewData["PageTitle"] = "SELF-Registration Service|Registrasi Mandiri";
            ViewData["enableBPJS"] = AppSession.Parameter.IsKioskEnableBPJS;
            ViewData["isBpjsAntrolIntegration"] = Helper.IsBpjsAntrolIntegration;
            ViewData["enableQRCode"] = AppSession.Parameter.IsKioskEnableQRCode;
        }

        private void SetUserLoginSession()
        {
            // session ksjdfjaiweoi adjf dsfklsdfiwjeafds jflksa djfoi a jdfasd;l
            BusinessObject.Common.UserLogin _userLogin = new BusinessObject.Common.UserLogin();
            _userLogin.UserID = "KIOSK";
            _userLogin.UserHostName = Common.Helper.ClientIP();
            AppSession.UserLogin = _userLogin;
            return;
        }

        public ActionResult Index()
        {
            InitConstanta();
            InitLocalPage();
            return View();
        }

        #region public shared
        private static string DayName(int dayNo, string dayName, string lang)
        {
            string[] hari = { "Minggu", "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu" };
            return ((lang == "en") ? dayName : hari[dayNo - 1]);
        }
        public static bool CekSchedule(string SUId, int dayLen)
        {
            var prmColl = new ParamedicCollection();

            // find paramedic
            var qsup = new ServiceUnitParamedicQuery("a");
            var qprm = new ParamedicQuery("b");
            var qpsd = new ParamedicScheduleDateQuery("c");
            var qopt = new OperationalTimeQuery("d");
            qprm.InnerJoin(qsup).On(qprm.ParamedicID == qsup.ParamedicID)
                .InnerJoin(qpsd).On(qprm.ParamedicID == qpsd.ParamedicID && qsup.ServiceUnitID == qpsd.ServiceUnitID)
                .InnerJoin(qopt).On(qpsd.OperationalTimeID == qopt.OperationalTimeID);
            qprm.Where(qsup.ServiceUnitID == SUId);
            qprm.Where("<cast(c.ScheduleDate as date) between cast(GETDATE() as date) and cast(dateadd(d," + dayLen + ",GETDATE()) as date)>");
            qprm.Select(qprm.ParamedicID);
            qprm.es.Distinct = true;
            prmColl.Load(qprm);

            return prmColl.Count > 0;
        }
        public static ParamedicCollection CekSchedule(string SUId, string ParamedicID, int dayLen, string lang)
        {
            DateTime NowSql = (new DateTime()).NowAtSqlServer();
            // find paramedic
            var qsup = new ServiceUnitParamedicQuery("a");
            var qprm = new ParamedicQuery("b");
            var qpsd = new ParamedicScheduleDateQuery("c");
            var qopt = new OperationalTimeQuery("d");

            qprm.InnerJoin(qsup).On(qprm.ParamedicID == qsup.ParamedicID)
                .InnerJoin(qpsd).On(qprm.ParamedicID == qpsd.ParamedicID && qsup.ServiceUnitID == qpsd.ServiceUnitID)
                .InnerJoin(qopt).On(qpsd.OperationalTimeID == qopt.OperationalTimeID);
            qprm.Where(qsup.ServiceUnitID == SUId);
            if (!string.IsNullOrEmpty(ParamedicID))
            {
                qprm.Where(qprm.ParamedicID == ParamedicID);
            }
            qprm.Where("<CAST(c.ScheduleDate AS date) between (CAST(GETDATE() AS date)) AND (CAST(DATEADD(d," + dayLen + ", getdate()) AS date))>");
            qprm.Select(
                qprm.ParamedicID
            );
            qprm.es.Distinct = true;
            var dtb = qprm.LoadDataTable();

            var prmColl = new ParamedicCollection();

            if (dtb.Rows.Count > 0)
            {
                prmColl.Query.Where(prmColl.Query.ParamedicID.In(dtb.AsEnumerable().Select(p => p["ParamedicID"])),
                    prmColl.Query.IsActive == true);
                prmColl.LoadAll();

                // show grid dokter yang sedang dan akan praktek selanjutnya
                foreach (var prm in prmColl)
                {
                    var qSchd = new ParamedicScheduleDateQuery("a");
                    var qOt = new OperationalTimeQuery("b");
                    qSchd.InnerJoin(qOt).On(qSchd.OperationalTimeID == qOt.OperationalTimeID);
                    //qSchd.Where("<a.ScheduleDate BETWEEN '" + (new DateTime()).NowAtSqlServer().Date + "' AND DATEADD(WW, 2, '" + (new DateTime()).NowAtSqlServer().Date + "')>", 
                    qSchd.Where(qSchd.ScheduleDate.Between(NowSql.Date, NowSql.AddDays(dayLen).Date),//, (new DateTime()).NowAtSqlServer().Date.AddDays(14)), 
                        qSchd.ParamedicID == prm.ParamedicID, qSchd.ServiceUnitID == SUId);
                    qSchd.Select(
                            "<datepart(dw, a.ScheduleDate) DayNo>",
                            "<datename(dw, a.ScheduleDate) DayName>",
                            qSchd.ParamedicID,
                            "<CAST((CASE WHEN ((b.EndTime1 > convert(char(5), GETDATE(), 108) AND ISNULL(a.IsClosedTime1, 0) = 0) OR " +
                            "(b.EndTime2 > convert(char(5), GETDATE(), 108) AND ISNULL(a.IsClosedTime2, 0) = 0) OR " +
                            "(b.EndTime3 > convert(char(5), GETDATE(), 108) AND ISNULL(a.IsClosedTime3, 0) = 0) OR " +
                            "(b.EndTime4 > convert(char(5), GETDATE(), 108) AND ISNULL(a.IsClosedTime4, 0) = 0) OR " +
                            "(b.EndTime5 > convert(char(5), GETDATE(), 108) AND ISNULL(a.IsClosedTime5, 0) = 0)) THEN 1 ELSE 0 END) as bit) AS NowActive>",
                            "<CAST((SELECT COUNT(pl.ParamedicID) FROM ParamedicLeave pl " +
                            "   INNER JOIN ParamedicLeaveDate pld ON pl.TransactionNo = pld.TransactionNo " +
                            "   WHERE pl.ParamedicID = '" + prm.ParamedicID + "' AND cast(pld.LeaveDate as date) = cast(GETDATE() as date)) AS bit) AS IsLeave>",
                            qOt.StartTime1, qOt.EndTime1,
                            qOt.StartTime2, qOt.EndTime2,
                            qOt.StartTime3, qOt.EndTime3,
                            qOt.StartTime4, qOt.EndTime4,
                            qOt.StartTime5, qOt.EndTime5
                        );
                    qSchd.es.Distinct = true;
                    qSchd.OrderBy("DayNo", Temiang.Dal.DynamicQuery.esOrderByDirection.Ascending);

                    var dt = qSchd.LoadDataTable();
                    // parsing
                    string sSched = string.Empty;
                    int oldDay = 0;
                    // set now active
                    prm.IsAvailable = false;

                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        sSched += (sSched == string.Empty ? "" :
                            ((int)row["DayNo"] == oldDay ? " " : ", ")) + (((int)row["DayNo"] == oldDay) ?
                            "" : DayName((int)row["DayNo"], row["DayName"].ToString(), lang));
                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            if (i < 5) continue;
                            sSched += (row[i].ToString().Trim() == string.Empty) ? "" : ((i % 2 == 0) ? "-" : " ") + row[i].ToString();
                        }
                        sSched = sSched.Trim();
                        oldDay = (int)row["DayNo"];
                        if ((bool)row["NowActive"] && !(bool)row["IsLeave"])
                        {
                            prm.IsAvailable = true;
                        }
                    }
                    // use field Notes as a container of the schedule
                    prm.ScheduleText = sSched;

                    prm.AcceptChanges();
                }
            }
            return prmColl;
        }
        public static ServiceUnit[] GetPoliWeeklyScheduled(string lang, string serviceUnitID = null)
        {
            var svuColl = new ServiceUnitCollection();
            if (!string.IsNullOrWhiteSpace(serviceUnitID))
                svuColl.Query.Where(
                    svuColl.Query.ServiceUnitID == serviceUnitID,
                    svuColl.Query.IsUsingJobOrder == false,
                    svuColl.Query.IsShowOnKiosk == true,
                    svuColl.Query.IsActive == true);
            else
                svuColl.Query.Where(
                    svuColl.Query.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                    svuColl.Query.IsUsingJobOrder == false,
                    svuColl.Query.IsShowOnKiosk == true,
                    svuColl.Query.IsActive == true);


            svuColl.LoadAll();

            foreach (var su in svuColl)
            {
                su.TodayAvailableParamedics = CekSchedule(su.ServiceUnitID, "", 0, lang).Where(x => x.IsAvailable == true).Count();
                su.ApptHasScheduleWeekly = CekSchedule(su.ServiceUnitID, 6);
                //su.ServiceUnitName = string.IsNullOrEmpty(su.ShortName) ? su.ServiceUnitName : su.ShortName;
                su.ServiceUnitName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(su.ServiceUnitName.ToLower());
                su.ShortName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(su.ShortName.ToLower());
            }

            var orderedSu = svuColl.Where(x => x.ApptHasScheduleWeekly).OrderBy(x => x.ServiceUnitName);
            return orderedSu.ToArray();
        }
        public static AppointmentCollection GetAppointment(string MedicalNo)
        {
            var SelfGuar = (string.IsNullOrEmpty(AppSession.Parameter.DefaultGuarantorKiosk) ?
                AppSession.Parameter.SelfGuarantor : AppSession.Parameter.DefaultGuarantorKiosk);

            var apt = new Appointment();
            var aptQuery = new AppointmentQuery("a");
            var patientQuery = new PatientQuery("p");
            aptQuery.InnerJoin(patientQuery).On(aptQuery.PatientID == patientQuery.PatientID);
            aptQuery.Where(
                aptQuery.Or(
                    aptQuery.Ssn == MedicalNo,
                    string.Format("< OR REPLACE(p.MedicalNo,'-','') like '%{0}%'>", MedicalNo)
                ),
                aptQuery.GuarantorID == SelfGuar,
                aptQuery.AppointmentDate == (new DateTime()).NowAtSqlServer().Date,
                aptQuery.SRAppointmentStatus.NotIn(AppSession.Parameter.AppointmentStatusCancel, AppSession.Parameter.AppointmentStatusClosed));

            var aptCollection = new AppointmentCollection();
            aptCollection.Load(aptQuery);
            return aptCollection;
        }
        public static DataTable GetRegisteredList(string MedicalNo)
        {
            var r = new RegistrationQuery("r");
            var p = new PatientQuery("p");
            var su = new ServiceUnitQuery("su");
            var par = new ParamedicQuery("par");
            r.InnerJoin(p).On(r.PatientID == p.PatientID)
                .InnerJoin(su).On(r.ServiceUnitID == su.ServiceUnitID)
                .InnerJoin(par).On(r.ParamedicID == par.ParamedicID)
                .Select(r.RegistrationNo, p.PatientName, su.ServiceUnitName, "<'Print' ButtonText>", par.ParamedicName)
                .Where(r.Or(
                        p.Ssn == MedicalNo,
                        string.Format("< OR REPLACE(p.MedicalNo,'-','') like '%{0}%'>", MedicalNo)
                    ),
                    r.RegistrationDate == (new DateTime()).NowAtSqlServer().Date,
                    r.IsVoid == false);
            return r.LoadDataTable();
        }
        public static DataTable GetRegisteredListByAppointmentNo(string AppointmentNo)
        {
            var r = new RegistrationQuery("r");
            var p = new PatientQuery("p");
            var su = new ServiceUnitQuery("su");
            var par = new ParamedicQuery("par");
            r.InnerJoin(p).On(r.PatientID == p.PatientID)
                .InnerJoin(su).On(r.ServiceUnitID == su.ServiceUnitID)
                .InnerJoin(par).On(r.ParamedicID == par.ParamedicID)
                .Select(r.RegistrationNo, p.PatientName, su.ServiceUnitName, "<'Print' ButtonText>", par.ParamedicName)
                .Where(
                    r.AppointmentNo == AppointmentNo,
                    r.RegistrationDate == (new DateTime()).NowAtSqlServer().Date,
                    r.IsVoid == false);
            return r.LoadDataTable();
        }
        public static DataTable GetMultiAppointment(string PatientID)
        {
            var apptQ = new AppointmentQuery("a");
            var servUnitQ = new ServiceUnitQuery("s");
            var paramedQ = new ParamedicQuery("p");

            apptQ.LeftJoin(servUnitQ).On(apptQ.ServiceUnitID == servUnitQ.ServiceUnitID)
                .LeftJoin(paramedQ).On(apptQ.ParamedicID == paramedQ.ParamedicID)
                .Where(apptQ.PatientID == PatientID, apptQ.AppointmentDate == (new DateTime()).NowAtSqlServer().Date,
                    apptQ.SRAppointmentStatus.NotIn(AppSession.Parameter.AppointmentStatusCancel, AppSession.Parameter.AppointmentStatusClosed)
                )
                .Select(apptQ.AppointmentNo, apptQ.PatientName,
                    paramedQ.ParamedicName,
                    servUnitQ.ServiceUnitName
                );
            return apptQ.LoadDataTable();
        }
        public static void PrintSlip(Registration reg, bool WithTracer)
        {
            // print slip di counter kiosk
            var parametersSlipKiosk = new PrintJobParameterCollection();
            parametersSlipKiosk.AddNew("p_RegistrationNo", reg.RegistrationNo, null, null);
            parametersSlipKiosk.AddNew("p_PrintCount", null, 1, null);
            PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationSlipKioskRpt, parametersSlipKiosk, AppSession.UserLogin.UserID);

            if (WithTracer)
            {
                RegistrationDetail.PrintTracer(reg);
            }
        }
        #endregion

        public ActionResult GoMRN(string MedicalNo, bool ForceCreateNew, string lang)
        {
            ViewData["Lang"] = lang;
            if (MedicalNo.Trim() == string.Empty)
            {
                ViewData["ErrMsg"] = lang == "en" ? "Please enter a valid Medical Number" : "Silahkan memasukkan Nomor Rekam Medis yang sah";
            }
            else
            {
                ViewData["dtAppt"] = new DataTable();
                ViewData["dtRegistered"] = new DataTable();

                var aptColl = GetAppointment(MedicalNo);

                if (aptColl.Count == 0)
                {
                    // daftar tanpa appointment
                    // tampilkan data registrasi jika sudah pernah registrasi


                    var dtReg = new DataTable();
                    if (!ForceCreateNew) dtReg = GetRegisteredList(MedicalNo);
                    ViewData["dtRegistered"] = dtReg;

                    if (dtReg.Rows.Count == 0)
                    {
                        // show list poli
                        var dtbPatient = (new PatientCollection()).PatientRegisterAbleByMedicalNo(MedicalNo, 50);
                        if (dtbPatient.Rows.Count != 1)
                        {
                            ViewData["ErrMsg"] = lang == "en" ? "Please enter a valid Medical Number" : "Silahkan memasukkan Nomor Rekam Medis yang sah";
                            return View();
                        }
                        else
                        {
                            // POPULATE POLI
                            var orderedSu = GetPoliWeeklyScheduled(lang);
                            ViewData["suColl"] = orderedSu;
                            ViewData["MedicalNo"] = dtbPatient.Rows[0]["MedicalNo"].ToString();
                        }
                    }
                }
                else if (aptColl.Count == 1)
                {
                    // there is only one appointment for this patient
                    var appt = aptColl[0];
                    LoadPatientInfo(appt, appt.ReferenceNumber, "", "", "", lang);
                    return View("PatientInfo");
                }
                else
                {
                    // there are more than one appointments
                    //ShowMessage("DEBUG", "MORE THAN ONE APPOINTMENT FROM THIS PATIENT");

                    // define new datasouce for grid multiple appointment
                    ViewData["dtAppt"] = GetMultiAppointment(aptColl[0].PatientID);
                }
            }
            return View();
        }

        public ActionResult GoAppointmentNo(string AppointmentNo, string lang) {
            if (Helper.IsNumeric(AppointmentNo)) {
                // reformatting AppointmentNo
                if (AppointmentNo.Length < 7) {
                    ViewData["ErrMsg"] = lang == "en" ? "Invalid Appointment Number" : "Nomor perjanjian tidak sah";
                    return View("PatientInfo");
                }
                string n1 = AppointmentNo.Substring(0, 6);
                string n2 = AppointmentNo.Substring(6);

                AppointmentNo = string.Format("APT/{0}-{1}", n1, n2);
            }

            var appt = new Appointment();
            if (appt.LoadByPrimaryKey(AppointmentNo))
            {
                var dtReg = GetRegisteredListByAppointmentNo(AppointmentNo);
                ViewData["dtRegistered"] = dtReg;
                if (dtReg.Rows.Count == 0)
                {
                    if (appt.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusCancel ||
                        appt.SRAppointmentStatus == AppSession.Parameter.AppointmentStatusClosed)
                    {
                        ViewData["ErrMsg"] = lang == "en" ? "Appointment has been canceled or closed" : "Data perjanjian sudah dibatalkan atau ditutup";
                    }
                    else
                    {
                        LoadPatientInfo(appt, appt.ReferenceNumber, "", "", "", lang);
                    }
                }
                else {
                    ViewData["dtAppt"] = new DataTable();
                    return View("GoMRN");
                }
            }
            else {
                ViewData["ErrMsg"] = lang == "en" ? "Appointment not found" : "Data perjanjian tidak ditemukan";
            }
            
            return View("PatientInfo");
        }

        public ActionResult GoReferenceNo(string ReferenceNo, string ReferenceType, bool ForceCreateNew, string lang)
        {
            string medicalNo = string.Empty;
            var errMsg = lang == "en" ? "Please enter a valid Reference Number" : "Silahkan memasukkan Nomor Rujukan yang sah";
            ViewData["Lang"] = lang;
            if (ReferenceNo.Trim() == string.Empty)
            {
                ViewData["ErrMsg"] = errMsg;
                return View();
                //throw new Exception(errMsg);
            }

            //========== TESTING
            //var rujukan = new Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan2();
            //rujukan.PoliRujukan = new Common.BPJS.VClaim.v11.Rujukan.Select.PoliRujukan() { Kode = "INT", Nama = "Poli Penyakit Dalam"};
            //rujukan.Peserta = new Common.BPJS.VClaim.v11.Rujukan.Select.Peserta() { NoKartu = "0001721709224", Nama = "Teh Es"};

            var refInfo = FindReferenceNo(ReferenceNo, ReferenceType);
            if (!refInfo.MetaData.IsValid)
            {
                //ViewData["ErrMsg"] = errMsg;
                //return View();
                throw new Exception(errMsg);
            }
            var rujukan = refInfo.Response.Rujukan;

            // Check Poli Rujukan
            var sub = new ServiceUnitBridging();
            sub.Query.Where(sub.Query.SRBridgingType == "BridgingType-001", sub.Query.BridgingID == rujukan.PoliRujukan.Kode);
            sub.Query.es.Top = 1;
            if (!sub.Query.Load())
            {
                ViewData["ErrMsg"] = lang == "en" ? "Reference Service Unit not registered, please contact the registration officer" : "Poli Rujukan belum terdaftar, silahkan hubungi petugas registrasi"; ;
                return View();
                //throw new Exception(lang == "en" ? "Reference Service Unit not registered, please contact the registration officer" : "Poli Rujukan belum terdaftar, silahkan hubungi petugas registrasi");
            }
            var serviceUnitID = sub.ServiceUnitID;

            // Cari Patient menggunakan No Kartu BPJS
            var pat = new Patient();
            if (!pat.GetpatientByGuarantorCardNo(rujukan.Peserta.NoKartu, AppSession.Parameter.GuarantorTypeBPJS))
            {
                //throw new Exception(lang == "en" ? "Data not found, please contact our registration officer to continue" :
                //    "Data pasien dengan jaminan BPJS tidak ditemukan, silahkan hubungi petugas pendaftaran untuk melanjutkan");
                ViewData["ErrMsg"] = lang == "en" ? "Data not found, please contact our registration officer to continue" :
                    "Data pasien dengan jaminan BPJS tidak ditemukan, silahkan hubungi petugas pendaftaran untuk melanjutkan";
                return View();
            }

            ViewData["dtAppt"] = new DataTable();
            ViewData["dtRegistered"] = new DataTable();

            var aptColl = GetAppointment(pat.MedicalNo);

            if (aptColl.Count == 0)
            {
                // daftar tanpa appointment
                // tampilkan data registrasi jika sudah pernah registrasi


                var dtReg = new DataTable();
                if (!ForceCreateNew) dtReg = GetRegisteredList(pat.MedicalNo);
                ViewData["dtRegistered"] = dtReg;

                if (dtReg.Rows.Count == 0)
                {
                    return RedirectToAction("ParamedicByUnit", new { ServiceUnitID = serviceUnitID, MedicalNo = pat.MedicalNo, lang = lang}); 
                }
            }
            else if (aptColl.Count == 1)
            {
                // there is only one appointment for this patient
                var appt = aptColl[0];
                LoadPatientInfo(appt, appt.ReferenceNumber, "", "", "", lang);
                return View("PatientInfo");
            }
            else
            {
                // there are more than one appointments
                //ShowMessage("DEBUG", "MORE THAN ONE APPOINTMENT FROM THIS PATIENT");

                // define new datasouce for grid multiple appointment
                ViewData["dtAppt"] = GetMultiAppointment(aptColl[0].PatientID);
            }

            return View();
            //========== END TESTING
            /*
            // Check No Rujukan
            var refInfo = FindReferenceNo(ReferenceNo, ReferenceType);
            if (!refInfo.MetaData.IsValid)
            {
                //ViewData["ErrMsg"] = errMsg;
                //return View();
                throw new Exception(errMsg);
            }
            else
            {
                var rujukan = refInfo.Response.Rujukan;

                // Check Poli Rujukan
                var sub = new ServiceUnitBridging();
                sub.Query.Where(sub.Query.SRBridgingType == "BridgingType-001", sub.Query.BridgingID == rujukan.PoliRujukan.Kode);
                sub.Query.es.Top = 1;
                if (!sub.Query.Load())
                {
                    //ViewData["ErrMsg"] = lang == "en" ? "Reference Service Unit not registered, please contact the registration officer" : "Poli Rujukan belum terdaftar, silahkan hubungi petugas registrasi"; ;
                    //return View();
                    throw new Exception(lang == "en" ? "Reference Service Unit not registered, please contact the registration officer" : "Poli Rujukan belum terdaftar, silahkan hubungi petugas registrasi");
                }
                var serviceUnitID = sub.ServiceUnitID;

                // Cari Patient menggunakan No Kartu BPJS
                var pat = new Patient();
                if (!pat.GetpatientByGuarantorCardNo(rujukan.Peserta.NoKartu, AppSession.Parameter.GuarantorTypeBPJS))
                {
                    throw new Exception(lang == "en" ? "Data not found, please contact our registration officer to continue" :
                        "Data pasien dengan jaminan BPJS tidak ditemukan, silahkan hubungi petugas pendaftaran untuk melanjutkan");
                }

                // Lanjut GoMRN
                ViewData["dtAppt"] = new DataTable();
                ViewData["dtRegistered"] = new DataTable();

                // POPULATE POLI
                var orderedSu = GetPoliWeeklyScheduled(lang, serviceUnitID);
                ViewData["suColl"] = orderedSu;
                ViewData["MedicalNo"] = medicalNo;

                return View();

                if (string.IsNullOrWhiteSpace(medicalNo))
                {
                    
                }
                else
                {
                    var aptColl = GetAppointment(medicalNo);
                    if (aptColl.Count == 0)
                    {
                        // daftar tanpa appointment
                        // tampilkan data registrasi jika sudah pernah registrasi

                        var dtReg = new DataTable();
                        if (!ForceCreateNew) dtReg = GetRegisteredList(medicalNo);
                        ViewData["dtRegistered"] = dtReg;

                        if (dtReg.Rows.Count == 0)
                        {
                            // show list poli
                            var dtbPatient = (new PatientCollection()).PatientRegisterAbleByMedicalNo(pat.MedicalNo, 50);
                            if (dtbPatient.Rows.Count != 1)
                            {
                                ViewData["ErrMsg"] = lang == "en" ? "Medical Number not valid, please contact the registration officer" : "Nomor Rekam Medis tidak valid, silahkan hubungi petugas registrasi";
                                return View();
                            }
                            else
                            {
                                // POPULATE POLI
                                var orderedSu = GetPoliWeeklyScheduled(lang, serviceUnitID);
                                ViewData["suColl"] = orderedSu;
                                ViewData["MedicalNo"] = dtbPatient.Rows[0]["MedicalNo"].ToString();
                            }
                        }
                    }
                    else if (aptColl.Count == 1)
                    {
                        // there is only one appointment for this patient
                        var appt = aptColl[0];
                        LoadPatientInfo(appt, "", "", "", lang);
                        return View("PatientInfo");
                    }
                    else
                    {
                        // there are more than one appointments
                        //ShowMessage("DEBUG", "MORE THAN ONE APPOINTMENT FROM THIS PATIENT");

                        // define new datasouce for grid multiple appointment
                        ViewData["dtAppt"] = GetMultiAppointment(aptColl[0].PatientID);
                    }
                }
            }
            return View();
            */
        }

        // referenceType = "1" -> Faskes 1
        // referenceType = "2" -> Faskes 2 (RS)
        private Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan.Select.Rujukan FindReferenceNo(string referenceNo, string referenceType)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetRujukan(true, referenceNo, Common.BPJS.VClaim.Enum.JenisFaskes.Faskes_1);
            if (!response.MetaData.IsValid) response = svc.GetRujukan(true, referenceNo, Common.BPJS.VClaim.Enum.JenisFaskes.RS);
            return response;
        }

        public ActionResult ParamedicByUnit(string ServiceUnitID, string MedicalNo, string lang)
        {
            ViewData["Lang"] = lang;
            var su = new ServiceUnit();
            if (su.LoadByPrimaryKey(ServiceUnitID))
            {
                var prmColl = Temiang.Avicenna.Controllers.KioskselfregController.CekSchedule(ServiceUnitID, "", 0, lang);
                if (prmColl.Count == 0)
                {
                    ViewData["ErrMsg"] = (lang == "en") ?
                        "physician schedule for " + su.ServiceUnitName + " is over. Please contact administrator for detail information." :
                        "Jam praktek dokter di " + su.ServiceUnitName + " sudah habis, silahkan menghubungi petugas kami untuk keterangan lebih lanjut.";
                    return View();
                }
                else
                {

                    ViewData["prmColl"] = prmColl;
                    ViewData["medicalNo"] = MedicalNo;
                    ViewData["serviceUnit"] = su;
                }
            }
            return View();
        }

        public ActionResult PatientInfo(string AppointmentNo, string ReferenceNo, string MedicalNo, string ServiceUnitID, string ParamedicID, string lang)
        {
            var appt = new Appointment();
            if (!string.IsNullOrEmpty(AppointmentNo))
            {
                if (!appt.LoadByPrimaryKey(AppointmentNo))
                {
                    ViewData["ErrMsg"] = (lang == "en") ? "Appointment not found" : "Data booking tidak ditemukan";
                    return View();
                }
            }
            LoadPatientInfo((string.IsNullOrEmpty(AppointmentNo) ? null : appt), ReferenceNo, MedicalNo, ServiceUnitID, ParamedicID, lang);
            return View();
        }

        private void LoadPatientInfo(Appointment appt, string ReferenceNo, string MedicalNo, string ServiceUnitID, string ParamedicID, string lang)
        {
            ViewData["Lang"] = lang;

            var p = new Patient();
            if (appt != null)
            {
                if (!string.IsNullOrEmpty(appt.PatientID))
                {
                    p.LoadByPrimaryKey(appt.PatientID);
                }
                p.FirstName = appt.FirstName;
                p.MiddleName = appt.MiddleName;
                p.LastName = appt.LastName;
                p.DateOfBirth = appt.DateOfBirth;
                p.Sex = appt.Sex;
                ServiceUnitID = appt.ServiceUnitID;
                ParamedicID = appt.ParamedicID;
            }
            else if (!string.IsNullOrEmpty(MedicalNo))
            {
                appt = new Appointment();
                appt.AppointmentNo = "";
                if (!p.GetPatientByNorm(MedicalNo))
                {
                    ViewData["ErrMsg"] = (lang == "en") ? "Please enter a valid Medical Number" : "Silahkan memasukkan Nomor Rekam Medis yang sah";
                }
            }
            else
            {
                ViewData["ErrMsg"] = (lang == "en") ? "Please enter a valid Medical Number" : "Silahkan memasukkan Nomor Rekam Medis yang sah";
            }

            // get service unit
            var servUnit = new ServiceUnit();
            servUnit.LoadByPrimaryKey(ServiceUnitID);

            // get physician
            var physician = new Paramedic();
            physician.LoadByPrimaryKey(ParamedicID);

            var pColl = CekSchedule(ServiceUnitID, ParamedicID, 0, lang);
            if (!pColl.Any())
            {
                ViewData["ErrMsg"] = (lang == "en") ? "An error occured while checking paramedic schedule" : "Pengecekan jadwal dokter gagal";
            }
            {
                if (!pColl.First().IsAvailable.Value)
                {
                    ViewData["ErrMsg"] = (lang == "en") ? "Physician is not available today" : "Dokter tidak praktek hari ini atau jadwal praktek sudah selesai";
                }
            }

            ViewData["appointment"] = appt;
            ViewData["patient"] = p;
            ViewData["serviceUnit"] = servUnit;
            ViewData["paramedic"] = physician;
            ViewData["referenceNo"] = ReferenceNo;
        }

        public ActionResult Register(string AppointmentNo, string ReferenceNo, string MedicalNo, string ServiceUnitID, string ParamedicID, string lang, string TipeRujukan)
        {
            SetUserLoginSession();

            string valMsg = RegistrationWS.ValidatePhycisianOnLeave(ParamedicID, (new DateTime()).NowAtSqlServer().Date, lang);
            if (!string.IsNullOrEmpty(valMsg))
            {
                return Json(JSonRetFormatted(valMsg, false));
            }

            var SepNo = string.Empty;
            if (TipeRujukan == "1" || TipeRujukan == "2")
            {
                //Create SEP
            }

            bool isFromAppt = AppointmentNo != string.Empty;
            Appointment appt = null;
            Registration reg = null;

            var pat = new Patient();
            if (pat.LoadByMedicalNo(MedicalNo))
            {
                var patEmContact = new PatientEmergencyContact();
                var _autoNumberLastPID = new AppAutoNumberLast();

                if (!patEmContact.LoadByPrimaryKey(pat.PatientID))
                {
                    // create new one
                    //patEmContact.AddNew();
                    patEmContact = new PatientEmergencyContact();

                    patEmContact.PatientID = pat.PatientID;
                }

                if (isFromAppt)
                {
                    // cek sudah ada registrasi atau belum untuk appointment bersangkutan
                    var regQuery = new RegistrationQuery();
                    regQuery.es.Top = 1;
                    regQuery.Where(regQuery.AppointmentNo == AppointmentNo && regQuery.IsVoid == false);
                    var regCollection = new RegistrationCollection();
                    regCollection.Load(regQuery);

                    if (regCollection.Count > 0)
                    {
                        // appointment is registered, continue to print
                        reg = regCollection[0];
                    }
                    appt = new Appointment();
                    appt.LoadByPrimaryKey(AppointmentNo);
                }
                else
                {
                    // cek sudah ada registrasi atau belum
                    var regQuery = new RegistrationQuery();
                    regQuery.Where(
                        regQuery.PatientID == pat.PatientID,
                        regQuery.ParamedicID == ParamedicID,
                        regQuery.IsVoid == false,
                        regQuery.IsClosed == false,
                        "<cast(RegistrationDate as date) = cast(getdate() as date)>"
                    );

                    var regCollection = new RegistrationCollection();
                    regCollection.Load(regQuery);

                    if (regCollection.Count > 0)
                    {
                        // appointment is registered, continue to print
                        reg = regCollection[0];
                    }
                }

                if (reg == null)
                {
                    // not yet registered, continue to register
                    // ------------------------start
                    var entity = new Registration();
                    var regResp = new RegistrationResponsiblePerson();
                    var regEmContact = new EmergencyContact();

                    var que = new ServiceUnitQue();
                    var chargesHD = new TransCharges();
                    var fileStatus = new MedicalFileStatus();
                    var mrFileStatus = new MedicalRecordFileStatus();
                    var billing = new MergeBilling();

                    reg = new Registration();

                    TransChargesItemCollection TransChargesItemsDT = new TransChargesItemCollection();
                    TransChargesItemCompCollection TransChargesItemsDTComp = new TransChargesItemCompCollection();
                    TransChargesItemConsumptionCollection TransChargesItemsDTConsumption = new TransChargesItemConsumptionCollection();
                    CostCalculationCollection CostCalculations = new CostCalculationCollection();

                    AppAutoNumberLast _autoNumberReg = new AppAutoNumberLast();
                    AppAutoNumberLast _autoNumberTrans = new AppAutoNumberLast();

                    try
                    {
                        WebService.V1_1.RegistrationWS.RegistrationSetEntityValue(appt, entity, pat, false, regResp, regEmContact,
                            que, billing, chargesHD, TransChargesItemsDT, TransChargesItemsDTComp,
                            TransChargesItemsDTConsumption, CostCalculations,
                            fileStatus, ServiceUnitID, ParamedicID, mrFileStatus,
                            _autoNumberTrans, "", "", "", "", lang);
                    }
                    catch (Exception exc)
                    {
                        // error may occurs when no room is defined for selected service unit;
                        return Json(JSonRetFormatted(string.Format("Mohon maaf, ada kesalahan sistem. Silahkan menghubungi petugas kami. <br />Detail Error: {0}", exc.Message), false));
                    }
                    string itemNoStock;

                    //return Json(JSonRetFormatted("Debugging", false));

                    WebService.V1_1.RegistrationWS.RegistrationSaveEntity(appt, entity, pat, patEmContact, regResp, regEmContact,
                        que, billing, chargesHD, TransChargesItemsDT, TransChargesItemsDTComp,
                        TransChargesItemsDTConsumption, CostCalculations, fileStatus, out itemNoStock,
                        mrFileStatus, _autoNumberReg, _autoNumberLastPID);

                    reg = entity;
                }

                // print slip di bagian pendaftaran, mengikuti setting registrasi rawat jalan
                //if (AppSession.Parameter.IsRegistrationPrintSlip == "Yes")
                //{
                //    var parametersSlip = new PrintJobParameterCollection();
                //    parametersSlip.AddNew("p_RegistrationNo", reg.RegistrationNo, null, null);
                //    PrintManager.CreatePrintJob(AppSession.Parameter.RegistrationSlipRpt, parametersSlip, AppSession.UserLogin.UserID);
                //}

                // print slip di counter kiosk
                PrintSlip(reg, true);

                return Json(JSonRetFormatted((lang == "en") ? "Thank you for using self-registration service" : "Terima kasih sudah menggunakan layanan pendaftaran mandiri"));
            }
            else
            {
                // Error patient not found
                return Json(JSonRetFormatted((lang == "en") ? "Patient not found" : "Data pasien tidak ditemukan", false));
            }
        }

        public ActionResult RegistrationPrint(string RegistrationNo)
        {
            SetUserLoginSession();

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
            {
                PrintSlip(reg, false);

                return Json(JSonRetFormatted("Done"));
            }
            else
            {
                return Json(JSonRetFormatted("Registration Not Found", false));
            }
        }

        public ActionResult GetToDayParamedic(string lang)
        {
            var aSU = GetPoliWeeklyScheduled(lang);

            return Json(JSonRetFormatted(aSU.Select(x =>
                new { ServiceUnitID = x.ServiceUnitID, TodayAvailableParamedics = x.TodayAvailableParamedics })));
        }

        public ActionResult ParamedicSlideShow(string lang)
        {
            InitConstanta();
            var aSU = GetPoliWeeklyScheduled(lang);

            foreach (var su in aSU)
            {
                su.ScheduledParamedics = CekSchedule(su.ServiceUnitID, "", 6, lang);
            }

            ViewData["ServiceUnitPar"] = aSU;

            return View("ParamedicSlideShow2");
        }

        #region Appointment
        public ActionResult ApptIndex() {
            return View();
        }

        public ActionResult ApptIndex2()
        {
            return View();
        }

        public ActionResult CheckInIndex()
        {
            return View();
        }

        #endregion
    }
}