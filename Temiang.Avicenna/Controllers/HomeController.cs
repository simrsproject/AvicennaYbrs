using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Data;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
//using Temiang.Avicenna.ReportLibrary.RLib_Rpt.PatientManagement.IGD;
using System.Drawing;
using System.IO;
using DevExpress.Utils;

namespace Temiang.Avicenna.Controllers
{
    public class MyFile
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }

    public class ToastMsg { 
        public int type { get; set; } // 0: success, 1: info, 2: warning, 3: error
        public string msg { get; set; }
    }
    public class HomeController : BaseController
    {
        // GET: Home
        private void SetProgramID()
        {
            ProgramID = "";
        }
        private void InitLocalPage()
        {
            // 
            ViewData["IsDemo"] = AppSession.Parameter.IsDemo;
        }
        private AppProgramCollection GetAppLink()
        {
            var prgColl = new AppProgramCollection();
            prgColl.LoadMvcByUserID("LNK", AppSession.UserLogin.UserID);

            //var ancColl = new AnnouncementCollection();
            //ditambahkan filter pengumuman yang sedang aktif
            //ancColl.Query.Where(ancColl.Query.IsActive == true);
            var ancColl = AmbilDataAnnouncement();
            if (ancColl.LoadAll()) {
                var app = new AppProgram();
                app.ProgramID = "16.x1";
                app.ParentProgramID = "16";
                app.ProgramName = "Announcement";
                app.AssemblyClassName = "fas fa-bullhorn nav-icon text-primary";
                app.StoreProcedureName = "javascript:LoadPageAnnouncement('{0}');";
                prgColl.AttachEntity(app);

                prgColl.LoadParent((new string[] { "16" }).ToList());
            }

            if (AppSession.Parameter.IsDemo)
            {
                var app = new AppProgram();
                app.ProgramID = "16.xx";
                app.ParentProgramID = "16";
                app.ProgramName = "Demo Shortcut";
                app.AssemblyClassName = "fas fa-biking nav-icon text-success";
                app.StoreProcedureName = "javascript:LoadPageShortcut('{0}');";
                prgColl.AttachEntity(app);

                prgColl.LoadParent((new string[] { "16" }).ToList());
            }

            return prgColl;
        }
        public ActionResult Index()
        {
            SetProgramID();
            if (!InitStartPage()) return RedirectToAction("Login");
            InitLocalPage();
            var prgColl = GetAppLink();
            ViewData["AppLink"] = prgColl;
            return View();
        }

        public ActionResult HomePage()
        {
            InitAjaxPage();
            var prgColl = GetAppLink();
            if (prgColl.Count == 0)
            {
                return View("Empty");
            }
            else
            {
                ViewData["AppLink"] = prgColl;
                return View();
            }
        }
        #region chart page
        public ActionResult Chart16_01()
        {
            InitAjaxPage();
            ViewData["AppLink"] = GetAppLink();
            return View();
        }
        public ActionResult Chart16_02()
        {
            InitAjaxPage();
            ViewData["AppLink"] = GetAppLink();
            return View();
        }
        public ActionResult Chart16_03()
        {
            InitAjaxPage();
            ViewData["AppLink"] = GetAppLink();
            return View();
        }
        public ActionResult Chart16_04()
        {
            InitAjaxPage();
            ViewData["AppLink"] = GetAppLink();
            return View();
        }
        public ActionResult Chart16_05()
        {
            InitAjaxPage();
            ViewData["AppLink"] = GetAppLink();
            return View();
        }
        #endregion

        #region SOP
        public ActionResult SOP()
        {
            //InitAjaxPage();
            ViewData["SOPDirectoryUrl"] = AppSession.Parameter.SOPDirectoryUrl;
            return View();
        }
        public ActionResult SOP2()
        {
            InitAjaxPage();
            return View();
        }
        public JsonResult SOPFileList()
        {
            string[] fileArray = Directory.GetFiles(@"C:\Data\SourceCodes\Images");

            var lf = new List<MyFile>();
            foreach (var f in fileArray)
            {
                var fl = new MyFile() { FileName = Path.GetFileName(f), FileUrl = SOPGetFileURL(Path.GetFileName(f)) };
                lf.Add(fl);
            }

            return Json(JSonRetFormatted(lf));
        }

        public string SOPGetFileURL(string FileName)
        {
            return string.Format("~/../sop/{0}", FileName);
        }
        #endregion

        public ActionResult Announcement()
        {
            InitAjaxPage();
            var ancColl = AmbilDataAnnouncement();
            //foreach (var anc in ancColl)
            ViewData["ancColl"] = ancColl;
            return View();
        }

        public ActionResult DemoShortcut()
        {
            InitAjaxPage();
            return View();
        }

        #region charts
        public ActionResult Chart()
        {
            var prgColl = new AppProgramCollection();
            prgColl.LoadMvcByUserID("LNK", AppSession.UserLogin.UserID);
            if (prgColl.Count == 0)
            {
                return View("Empty");
            }
            else
            {
                ViewData["AppLink"] = prgColl;
                return View();
            }
        }
        #region Announcement
        public AnnouncementCollection AmbilDataAnnouncement()
        {
            var ancColl = new AnnouncementCollection();
            var anc = new AnnouncementQuery("anc");

            //jt1.Select("<COUNT(DISTINCT jt1.JournalId) AS Count>")
            anc.Select(anc.AnnouncementStartDate,
                anc.AnnouncementEndDate, 
                anc.AnnouncementTitle,
                anc.AnnouncementDesc
                );
            anc.Where(
                anc.IsActive == true,
                anc.AnnouncementEndDate >= DateTime.Now.Date
                );

            ancColl.Load(anc);
            return ancColl;
        }
        #endregion

        #region daily
        public JsonResult ChartRegIpVisitByGuarantor(string DateStart, string DateEnd)
        {
            var dStart = DateTime.ParseExact(DateStart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dEnd = DateTime.ParseExact(DateEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var gr = new GuarantorQuery("gr");
            var gt = new AppStandardReferenceItemQuery("gt");
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .InnerJoin(gr).On(reg.GuarantorID == gr.GuarantorID)
                .InnerJoin(gt).On(gr.SRGuarantorType == gt.ItemID && gt.StandardReferenceID == "GuarantorType")
                .Where(
                    reg.RegistrationDate.Date() <= dEnd.Date,
                    reg.Or(
                        reg.DischargeDate.Date() > dStart.Date,
                        reg.DischargeDate.IsNull()
                    ),
                    reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    reg.IsVoid == false
                ).GroupBy(gr.SRGuarantorType, gt.ItemName)
                .Select(
                    gr.SRGuarantorType, gt.ItemName,
                    reg.RegistrationNo.Count().As("RegistrationCount")
                ).OrderBy(gt.ItemName.Ascending);

            var dtb = reg.LoadDataTable();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ItemName"].ToString()).Distinct().ToList();

            var ds = new dataset();
            ds.label = "Dataset 1";
            ds.data = data.labels.Select(x =>
                dtb.AsEnumerable().Where(y =>
                    y["ItemName"].ToString() == x)
                .Select(y => Convert.ToDouble(y["RegistrationCount"])).FirstOrDefault()).ToList();

            data.datasets.Add(ds);

            RandomRGB("doughnut", "0", data.datasets);

            return Json(JSonRetFormatted(data));
        }
        public JsonResult ChartRegOpVisitByGuarantor(string DateStart, string DateEnd)
        {
            var dStart = DateTime.ParseExact(DateStart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dEnd = DateTime.ParseExact(DateEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var gr = new GuarantorQuery("gr");
            var gt = new AppStandardReferenceItemQuery("gt");
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .InnerJoin(gr).On(reg.GuarantorID == gr.GuarantorID)
                .InnerJoin(gt).On(gr.SRGuarantorType == gt.ItemID && gt.StandardReferenceID == "GuarantorType")
                .Where(
                    //reg.RegistrationDate.Date().Between(dStart.Date, dEnd.Date),
                    reg.RegistrationDate >= dStart.Date,
                    reg.RegistrationDate < dEnd.Date.AddDays(1),
                    reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                    reg.IsVoid == false, reg.IsNonPatient == false
                ).GroupBy(gr.SRGuarantorType, gt.ItemName)
                .Select(
                    gr.SRGuarantorType, gt.ItemName,
                    reg.RegistrationNo.Count().As("RegistrationCount")
                ).OrderBy(gt.ItemName.Ascending);

            var dtb = reg.LoadDataTable();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ItemName"].ToString()).Distinct().ToList();

            var ds = new dataset();
            ds.label = "Dataset 1";
            ds.data = data.labels.Select(x =>
                dtb.AsEnumerable().Where(y =>
                    y["ItemName"].ToString() == x)
                .Select(y => Convert.ToDouble(y["RegistrationCount"])).FirstOrDefault()).ToList();

            data.datasets.Add(ds);

            RandomRGB("doughnut", "0", data.datasets);

            return Json(JSonRetFormatted(data));
        }
        public JsonResult ChartRegEmByTriage(string DateStart, string DateEnd)
        {
            var dStart = DateTime.ParseExact(DateStart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dEnd = DateTime.ParseExact(DateEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var ct = new AppStandardReferenceItemQuery("ct");
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .LeftJoin(ct).On(reg.SRTriage == ct.ItemID && ct.StandardReferenceID == "Triage")
                .Where(
                    //reg.RegistrationDate.Date().Between(dStart.Date, dEnd.Date),
                    reg.RegistrationDate >= dStart.Date,
                    reg.RegistrationDate < dEnd.Date.AddDays(1),
                    reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                    reg.IsVoid == false
                ).GroupBy(reg.SRTriage, ct.Note.Coalesce(ct.ItemName.Coalesce("'Undefined'")))
                .Select(
                    reg.SRTriage,
                    ct.Note.Coalesce(ct.ItemName.Coalesce("'Undefined'")).As("ItemName"),
                    reg.RegistrationNo.Count().As("RegistrationCount")
                ).OrderBy(ct.Note.Coalesce(ct.ItemName.Coalesce("'Undefined'")).Ascending);

            var dtb = reg.LoadDataTable();

            var Triage = dtb.AsEnumerable().Select(x => new { SRTriage = x["SRTriage"].ToString(), SRTriageName = x["ItemName"].ToString() })
                .Distinct().ToList();

            var data = new data();
            data.labels = Triage.Select(x => x.SRTriageName).ToList();

            var ds = new dataset();
            ds.label = "Dataset 1";
            ds.data = data.labels.Select(x =>
                dtb.AsEnumerable().Where(y =>
                    y["ItemName"].ToString() == x)
                .Select(y => Convert.ToDouble(y["RegistrationCount"])).FirstOrDefault()).ToList();

            //RandomRGB("doughnut", data.datasets);
            // set color
            List<string> clr = new List<string>();
            foreach (var label in data.labels)
            {
                var tid = Triage.Where(x => x.SRTriageName == label).Select(x => x.SRTriage).First();
                switch (tid)
                {
                    case "00":
                        {
                            clr.Add("rgb(128,128,128)");
                            break;
                        }
                    case "01":
                        {
                            clr.Add("rgb(255,0,0)");
                            break;
                        }
                    case "02":
                        {
                            clr.Add("rgb(255,100,100)");
                            break;
                        }
                    case "03":
                        {
                            clr.Add("rgb(255,255,0)");
                            break;
                        }
                    case "04":
                        {
                            clr.Add("rgb(0,255,0)");
                            break;
                        }
                    case "05":
                        {
                            clr.Add("rgb(0,0,0)");
                            break;
                        }
                    default:
                        {
                            clr.Add("rgb(0,0,255)");
                            break;
                        }
                }
            }
            ds.backgroundColor = clr;
            data.datasets.Add(ds);

            return Json(JSonRetFormatted(data));
        }

        public JsonResult ChartRegIpCountByClass(string DateStart, string DateEnd)
        {
            var dStart = DateTime.ParseExact(DateStart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dEnd = DateTime.ParseExact(DateEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var cl = new ClassQuery("cl");
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .InnerJoin(cl).On(reg.ClassID == cl.ClassID)
                .Where(
                    reg.RegistrationDate.Date() <= dEnd.Date,
                    reg.Or(
                        reg.DischargeDate.Date() > dStart.Date,
                        reg.DischargeDate.IsNull()
                    ),
                    reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    reg.IsVoid == false
                ).GroupBy(reg.ClassID, cl.ClassName, pat.Sex)
                .Select(
                    reg.ClassID,
                    cl.ClassName,
                    pat.Sex,
                    reg.RegistrationNo.Count().As("RegistrationCount")
                ).OrderBy(cl.ClassName.Ascending);

            var dtb = reg.LoadDataTable();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ClassName"].ToString()).Distinct().ToList();

            var sexs = new string[] { "M", "F" };

            foreach (var sex in sexs)
            {
                var ds = new dataset();
                ds.label = sex;
                ds.data = data.labels.Select(x =>
                    dtb.AsEnumerable().Where(y =>
                        y["ClassName"].ToString() == x &&
                        y["Sex"].ToString() == sex)
                    .Select(y => Convert.ToDouble(y["RegistrationCount"])).FirstOrDefault()).ToList();

                ds.backgroundColor = sex == "M" ? "rgb(0,0,255,0.2)" : "rgb(255,0,128,0.2)";
                ds.borderColor = sex == "M" ? "rgb(0,0,255)" : "rgb(255,0,128)";

                data.datasets.Add(ds);
            }

            //RandomRGB("line", "0.2", data.datasets);

            return Json(JSonRetFormatted(data));
        }
        public JsonResult ChartRegOpCountBySU(string DateStart, string DateEnd)
        {
            var dStart = DateTime.ParseExact(DateStart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dEnd = DateTime.ParseExact(DateEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var su = new ServiceUnitQuery("su");
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .InnerJoin(su).On(reg.ServiceUnitID == su.ServiceUnitID)
                .Where(
                    //reg.RegistrationDate.Date().Between(dStart.Date, dEnd.Date),
                    reg.RegistrationDate >= dStart.Date,
                    reg.RegistrationDate < dEnd.Date.AddDays(1),
                    reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                    reg.IsVoid == false, reg.IsNonPatient == false
                ).GroupBy(reg.ServiceUnitID, su.ShortName.Coalesce(su.ServiceUnitName), pat.Sex)
                .Select(
                    reg.ServiceUnitID,
                    su.ShortName.Coalesce(su.ServiceUnitName),
                    pat.Sex,
                    reg.RegistrationNo.Count().As("RegistrationCount")
                ).OrderBy(su.ShortName.Coalesce(su.ServiceUnitName).Ascending);

            var dtb = reg.LoadDataTable();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ShortName"].ToString()).Distinct().ToList();

            var sexs = new string[] { "M", "F" };

            foreach (var sex in sexs)
            {
                var ds = new dataset();
                ds.label = sex;
                ds.data = data.labels.Select(x =>
                    dtb.AsEnumerable().Where(y =>
                        y["ShortName"].ToString() == x &&
                        y["Sex"].ToString() == sex)
                    .Select(y => Convert.ToDouble(y["RegistrationCount"])).FirstOrDefault()).ToList();

                ds.backgroundColor = sex == "M" ? "rgb(0,0,255,0.2)" : "rgb(255,0,128,0.2)";
                ds.borderColor = sex == "M" ? "rgb(0,0,255)" : "rgb(255,0,128)";

                data.datasets.Add(ds);
            }

            //RandomRGB("line", "0.2", data.datasets);

            return Json(JSonRetFormatted(data));
        }
        public JsonResult ChartRegEmCountByCaseType(string DateStart, string DateEnd)
        {
            var dStart = DateTime.ParseExact(DateStart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dEnd = DateTime.ParseExact(DateEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var ct = new AppStandardReferenceItemQuery("ct");
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .LeftJoin(ct).On(reg.SRERCaseType == ct.ItemID && ct.StandardReferenceID == "ERCaseType")
                .Where(
                    //reg.RegistrationDate.Date().Between(dStart.Date, dEnd.Date),
                    reg.RegistrationDate >= dStart.Date,
                    reg.RegistrationDate < dEnd.Date.AddDays(1),
                    reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient,
                    reg.IsVoid == false
                ).GroupBy(reg.SRERCaseType, ct.ItemName.Coalesce("'Undefined'"), pat.Sex)
                .Select(
                    reg.SRERCaseType,
                    ct.ItemName.Coalesce("'Undefined'"),
                    pat.Sex,
                    reg.RegistrationNo.Count().As("RegistrationCount")
                ).OrderBy(ct.ItemName.Coalesce("'Undefined'").Ascending);

            var dtb = reg.LoadDataTable();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ItemName"].ToString()).Distinct().ToList();

            var sexs = new string[] { "M", "F" };

            foreach (var sex in sexs)
            {
                var ds = new dataset();
                ds.label = sex;
                ds.data = data.labels.Select(x =>
                    dtb.AsEnumerable().Where(y =>
                        y["ItemName"].ToString() == x &&
                        y["Sex"].ToString() == sex)
                    .Select(y => Convert.ToDouble(y["RegistrationCount"])).FirstOrDefault()).ToList();

                ds.backgroundColor = sex == "M" ? "rgb(0,0,255,0.2)" : "rgb(255,0,128,0.2)";
                ds.borderColor = sex == "M" ? "rgb(0,0,255)" : "rgb(255,0,128)";

                data.datasets.Add(ds);
            }

            //RandomRGB("line", "0.2", data.datasets);

            return Json(JSonRetFormatted(data));
        }
        #endregion
        #region monthly
        public JsonResult ChartHI(int Year)
        {
            var rl = new RlTxReportQuery("rl");
            var rl12 = new RlTxReport12Query("rl12");
            rl.InnerJoin(rl12).On(rl.RlTxReportNo == rl12.RlTxReportNo)
                .Where(rl.PeriodYear == Year, rl.PeriodMonthStart == rl.PeriodMonthEnd)
                .Select(
                    rl.PeriodYear,
                    rl.PeriodMonthStart,
                    rl12.Bor, rl12.Los, rl12.Bto, rl12.Toi, rl12.Ndr, rl12.Gdr
                ).OrderBy(rl.PeriodMonthStart.Cast(Dal.DynamicQuery.esCastType.Int16).Ascending);

            var dtb = rl.LoadDataTable();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["PeriodMonthStart"].ToString()).ToList();

            var idcs = new string[] { "Bor", "Los", "Bto", "Toi", "Ndr", "Gdr" };

            foreach (var idc in idcs)
            {
                var ds = new dataset();
                ds.label = idc;
                ds.data = data.labels.Select(x =>
                    dtb.AsEnumerable().Where(y =>
                        y["PeriodMonthStart"].ToString() == x)
                    .Select(y => Convert.ToDouble(y[idc])).FirstOrDefault()).ToList();
                ds.fill = false;
                data.datasets.Add(ds);
            }

            for (var i = 0; i < data.labels.Count; i++)
            {
                data.labels[i] = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(data.labels[i]));
            }

            RandomRGB("line", "0", data.datasets);

            return Json(JSonRetFormatted(data));
        }
        public JsonResult ChartTop10Diagnosis(int Year, int Month, string SRRegType)
        {
            var rl = new RlTxReportQuery("rl");
            var rl53 = new RlTxReport53Query("rl53");
            var rl54 = new RlTxReport54Query("rl54");
            var diag = new DiagnoseQuery("diag");
            if (SRRegType.ToLower() == "ipr")
            {
                rl.InnerJoin(rl53).On(rl.RlTxReportNo == rl53.RlTxReportNo)
                    .InnerJoin(diag).On(rl53.DiagnosaID == diag.DiagnoseID)
                    .Where(rl.PeriodYear == Year, rl.PeriodMonthStart == Month, rl.PeriodMonthStart == rl.PeriodMonthEnd)
                    .Select(
                        rl.PeriodYear,
                        rl53.DiagnosaID,
                        diag.DiagnoseName,
                        rl53.Total
                    ).OrderBy(rl53.Total.Descending);
            }
            else if (SRRegType.ToLower() == "opr")
            {
                rl.InnerJoin(rl54).On(rl.RlTxReportNo == rl54.RlTxReportNo)
                    .InnerJoin(diag).On(rl54.DiagnosaID == diag.DiagnoseID)
                    .Where(rl.PeriodYear == Year, rl.PeriodMonthStart == Month, rl.PeriodMonthStart == rl.PeriodMonthEnd)
                    .Select(
                        rl.PeriodYear,
                        rl54.DiagnosaID,
                        diag.DiagnoseName,
                        rl54.JumlahKunjungan.As("Total")
                    ).OrderBy(rl54.JumlahKunjungan.Descending);
            }
            rl.es.Top = 10;

            var dtb = rl.LoadDataTable();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["DiagnoseName"].ToString()).ToList();

            var ds = new dataset();
            ds.label = "Dataset 1";
            ds.data = data.labels.Select(x =>
                dtb.AsEnumerable().Where(y =>
                    y["DiagnoseName"].ToString() == x)
                .Select(y => Convert.ToDouble(y["Total"])).FirstOrDefault()).ToList();
            data.datasets.Add(ds);

            RandomRGB("line", "1", data.datasets);

            return Json(JSonRetFormatted(data));
        }
        public JsonResult ChartKunjungan(int Year)
        {
            var rl = new RlTxReportQuery("rl");
            var rl51 = new RlTxReport51Query("rl51");
            var rlItem = new RlMasterReportItemQuery("rlItem");
            rl.InnerJoin(rl51).On(rl.RlTxReportNo == rl51.RlTxReportNo)
                .InnerJoin(rlItem).On(rl51.RlMasterReportItemID == rlItem.RlMasterReportItemID)
                .Where(rl.PeriodYear == Year, rl.PeriodMonthStart == rl.PeriodMonthEnd)
                .Select(
                    rl.PeriodYear,
                    rl.PeriodMonthStart,
                    rlItem.RlMasterReportItemName,
                    rl51.Jumlah
                ).OrderBy(rl.PeriodMonthStart.Cast(Dal.DynamicQuery.esCastType.Int16).Ascending);

            var dtb = rl.LoadDataTable();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["PeriodMonthStart"].ToString()).Distinct().ToList();

            var itemNames = dtb.AsEnumerable().Select(x => x["RlMasterReportItemName"].ToString()).Distinct().ToList();

            foreach (var itemName in itemNames)
            {
                var ds = new dataset();
                ds.label = itemName;
                ds.data = data.labels.Select(x =>
                    dtb.AsEnumerable().Where(y =>
                        y["PeriodMonthStart"].ToString() == x &&
                        y["RlMasterReportItemName"].ToString() == itemName)
                    .Select(y => Convert.ToDouble(y["Jumlah"])).FirstOrDefault()).ToList();

                data.datasets.Add(ds);
            }

            for (var i = 0; i < data.labels.Count; i++)
            {
                data.labels[i] = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(data.labels[i]));
            }

            RandomRGB("line", "1", data.datasets);

            return Json(JSonRetFormatted(data));
        }
        #endregion
        #region finance
        public JsonResult ChartBudgetYearly(int Year)
        {
            var bColl = new BudgetingCollection();

            var dtb = bColl.GetBudgetRealizationForChart(Year);

            var data = new data();
            data.labels = (Enumerable.Range(1, 12)).ToList().Select(x => x.ToString()).ToList();

            var fNames = new string[] { "Budget", "Realization" };
            var nbs = dtb.AsEnumerable().Select(x => x["NormalBalance"].ToString()).Distinct();

            foreach (var fName in fNames)
            {
                foreach (var nb in nbs)
                {
                    var ds = new dataset();
                    ds.label = string.Format("{0} ({1})", fName, nb);
                    //ds.fill = false;
                    foreach (var lbl in data.labels)
                    {
                        ds.data.Add(dtb.AsEnumerable().Where(x => x["NormalBalance"].ToString() == nb)
                            .Sum(x => x[fName + lbl.ToString()].ToDouble()));
                    }
                    data.datasets.Add(ds);
                }
            }

            for (var i = 0; i < data.labels.Count; i++)
            {
                data.labels[i] = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Convert.ToInt32(data.labels[i]));
            }

            RandomRGB("line", "0.1", data.datasets);

            return Json(JSonRetFormatted(data));
        }
        public JsonResult ChartBudgetMonthly(int Year, int Month)
        {
            var bColl = new BudgetingCollection();

            var dtb = bColl.GetBudgetRealizationForChart(Year);

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ChartOfAccountName"].ToString()).ToList();

            var fNames = new string[] { "Budget", "Realization" };

            foreach (var fName in fNames)
            {
                var ds = new dataset();
                ds.label = fName;
                foreach (var lbl in data.labels)
                {
                    ds.data.Add(dtb.AsEnumerable().Where(x => x["ChartOfAccountName"].ToString() == lbl)
                        .Select(x => x[fName + Month.ToString()].ToDouble()).FirstOrDefault());
                }
                data.datasets.Add(ds);
            }

            RandomRGB("line", "0.5", data.datasets);

            return Json(JSonRetFormatted(data));
        }
        #endregion
        #region beds
        public JsonResult ChartBedByClass()
        {
            var bed = new BedQuery("bed");
            var room = new ServiceRoomQuery("room");
            var cls = new ClassQuery("cls");
            var std = new AppStandardReferenceItemQuery("std");
            bed.InnerJoin(room).On(bed.RoomID == room.RoomID)
                .InnerJoin(cls).On(bed.ClassID == cls.ClassID)
                .InnerJoin(std).On(std.StandardReferenceID == "BedStatus" && std.ItemID == bed.SRBedStatus)
                .Where(bed.IsActive == true, room.IsActive == true, bed.IsTemporary == false)
                .Select(cls.ClassID, cls.ClassName, std.ItemName.As("BedStatusName"), bed.BedID
                );

            var dtb = bed.LoadDataTable();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ClassName"].ToString()).Distinct().ToList();

            var ctgrs = dtb.AsEnumerable().Select(x => x["BedStatusName"].ToString()).Distinct().ToList();

            foreach (var ctgr in ctgrs)
            {
                var ds = new dataset();
                ds.label = ctgr;
                ds.data = data.labels.Select(x =>
                    dtb.AsEnumerable().Where(y =>
                        y["ClassName"].ToString() == x &&
                        y["BedStatusName"].ToString() == ctgr)
                    .Count().ToDouble()).ToList();

                //ds.backgroundColor = sex == "M" ? "rgb(0,0,255,0.2)" : "rgb(255,0,128,0.2)";
                //ds.borderColor = sex == "M" ? "rgb(0,0,255)" : "rgb(255,0,128)";

                data.datasets.Add(ds);
            }

            // default random
            RandomRGB("line", "0.2", data.datasets);

            foreach (var ds in data.datasets)
            {
                switch (ds.label)
                {
                    case "Ready":
                        {
                            ds.backgroundColor = "rgb(0,255,0,1)";
                            ds.borderColor = "rgb(0,255,0)";
                            break;
                        }
                    case "Occupied":
                        {
                            ds.backgroundColor = "rgb(255,0,0,1)";
                            ds.borderColor = "rgb(255,0,0)";
                            break;
                        }
                    case "Booked":
                        {
                            ds.backgroundColor = "rgb(128,0,0,1)";
                            ds.borderColor = "rgb(128,0,0)";
                            break;
                        }
                    case "Pending":
                        {
                            ds.backgroundColor = "rgb(255,128,0,1)";
                            ds.borderColor = "rgb(255,128,0)";
                            break;
                        }
                    case "Cleaning":
                        {
                            ds.backgroundColor = "rgb(255,255,0,1)";
                            ds.borderColor = "rgb(255,255,0)";
                            break;
                        }
                    case "Reserved":
                        {
                            ds.backgroundColor = "rgb(0,0,255,1)";
                            ds.borderColor = "rgb(0,0,255)";
                            break;
                        }
                    case "Repaired":
                        {
                            ds.backgroundColor = "rgb(128,0,128,1)";
                            ds.borderColor = "rgb(128,0,128)";
                            break;
                        }
                }
            }

            return Json(JSonRetFormatted(data));
        }
        public JsonResult ChartBedByStatus()
        {
            var bed = new BedQuery("bed");
            var room = new ServiceRoomQuery("room");
            var std = new AppStandardReferenceItemQuery("std");
            bed.InnerJoin(room).On(bed.RoomID == room.RoomID)
                .InnerJoin(std).On(std.StandardReferenceID == "BedStatus" && std.ItemID == bed.SRBedStatus)
                .Where(bed.IsActive == true, room.IsActive == true, bed.IsTemporary == false)
                .Select(bed.SRBedStatus, std.ItemName.As("BedStatusName"), bed.BedID);

            var dtb = bed.LoadDataTable();

            var oLed = dtb.AsEnumerable().Select(x => new { ID = x["SRBedStatus"].ToString(), Name = x["BedStatusName"].ToString() })
                .Distinct().ToList();

            var data = new data();
            data.labels = oLed.Select(x => x.Name).ToList();

            var ds = new dataset();
            ds.label = "Dataset 1";
            ds.data = data.labels.Select(x =>
                dtb.AsEnumerable().Where(y =>
                    y["BedStatusName"].ToString() == x)
                .Count().ToDouble()).ToList();

            // set color
            List<string> clr = new List<string>();
            foreach (var label in data.labels)
            {
                switch (label)
                {
                    case "Ready":
                        {
                            clr.Add("rgb(0,255,0)");
                            break;
                        }
                    case "Occupied":
                        {
                            clr.Add("rgb(255,0,0)");
                            break;
                        }
                    case "Booked":
                        {
                            clr.Add("rgb(128,0,0)");
                            break;
                        }
                    case "Pending":
                        {
                            clr.Add("rgb(255,128,0)");
                            break;
                        }
                    case "Cleaning":
                        {
                            clr.Add("rgb(255,255,0)");
                            break;
                        }
                    case "Reserved":
                        {
                            clr.Add("rgb(0,0,255)");
                            break;
                        }
                    case "Repaired":
                        {
                            clr.Add("rgb(128,0,128)");
                            break;
                        }
                }
            }
            ds.backgroundColor = clr;
            data.datasets.Add(ds);

            return Json(JSonRetFormatted(data));
        }
        public JsonResult ChartBedByServiceUnit()
        {
            var bed = new BedQuery("bed");
            var room = new ServiceRoomQuery("room");
            var su = new ServiceUnitQuery("su");
            var std = new AppStandardReferenceItemQuery("std");
            bed.InnerJoin(room).On(bed.RoomID == room.RoomID)
                .InnerJoin(su).On(room.ServiceUnitID == su.ServiceUnitID)
                .InnerJoin(std).On(std.StandardReferenceID == "BedStatus" && std.ItemID == bed.SRBedStatus)
                .Where(bed.IsActive == true, room.IsActive == true, bed.IsTemporary == false)
                .Select(su.ServiceUnitID, su.ServiceUnitName, std.ItemName.As("BedStatusName"), bed.BedID
                );

            var dtb = bed.LoadDataTable();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ServiceUnitName"].ToString()).Distinct().ToList();

            var ctgrs = dtb.AsEnumerable().Select(x => x["BedStatusName"].ToString()).Distinct().ToList();

            foreach (var ctgr in ctgrs)
            {
                var ds = new dataset();
                ds.label = ctgr;
                ds.data = data.labels.Select(x =>
                    dtb.AsEnumerable().Where(y =>
                        y["ServiceUnitName"].ToString() == x &&
                        y["BedStatusName"].ToString() == ctgr)
                    .Count().ToDouble()).ToList();

                //ds.backgroundColor = sex == "M" ? "rgb(0,0,255,0.2)" : "rgb(255,0,128,0.2)";
                //ds.borderColor = sex == "M" ? "rgb(0,0,255)" : "rgb(255,0,128)";

                data.datasets.Add(ds);
            }

            // default random
            RandomRGB("line", "0.2", data.datasets);

            foreach (var ds in data.datasets)
            {
                switch (ds.label)
                {
                    case "Ready":
                        {
                            ds.backgroundColor = "rgb(0,255,0,1)";
                            ds.borderColor = "rgb(0,255,0)";
                            break;
                        }
                    case "Occupied":
                        {
                            ds.backgroundColor = "rgb(255,0,0,1)";
                            ds.borderColor = "rgb(255,0,0)";
                            break;
                        }
                    case "Booked":
                        {
                            ds.backgroundColor = "rgb(128,0,0,1)";
                            ds.borderColor = "rgb(128,0,0)";
                            break;
                        }
                    case "Pending":
                        {
                            ds.backgroundColor = "rgb(255,128,0,1)";
                            ds.borderColor = "rgb(255,128,0)";
                            break;
                        }
                    case "Cleaning":
                        {
                            ds.backgroundColor = "rgb(255,255,0,1)";
                            ds.borderColor = "rgb(255,255,0)";
                            break;
                        }
                    case "Reserved":
                        {
                            ds.backgroundColor = "rgb(0,0,255,1)";
                            ds.borderColor = "rgb(0,0,255)";
                            break;
                        }
                    case "Repaired":
                        {
                            ds.backgroundColor = "rgb(128,0,128,1)";
                            ds.borderColor = "rgb(128,0,128)";
                            break;
                        }
                }
            }

            return Json(JSonRetFormatted(data));
        }

        #endregion
        #region hr
        public JsonResult ChartEmployeeByEmployment()
        {
            EmployeeWorkingInfoCollection coll = new EmployeeWorkingInfoCollection();
            DataTable dtb = coll.GetEmployeeByEmployment();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ItemName"].ToString()).Distinct().ToList();

            var ds = new dataset();
            ds.label = "Dataset 1";
            ds.data = data.labels.Select(x =>
                dtb.AsEnumerable().Where(y =>
                    y["ItemName"].ToString() == x)
                .Select(y => Convert.ToDouble(y["ChartCount"])).FirstOrDefault()).ToList();

            data.datasets.Add(ds);

            RandomRGB("doughnut", "0", data.datasets);

            return Json(JSonRetFormatted(data));
        }

        public JsonResult ChartEmployeeByType()
        {
            EmployeeWorkingInfoCollection coll = new EmployeeWorkingInfoCollection();
            DataTable dtb = coll.GetEmployeeByType();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ItemName"].ToString()).Distinct().ToList();

            var ds = new dataset();
            ds.label = "Dataset 1";
            ds.data = data.labels.Select(x =>
                dtb.AsEnumerable().Where(y =>
                    y["ItemName"].ToString() == x)
                .Select(y => Convert.ToDouble(y["ChartCount"])).FirstOrDefault()).ToList();

            data.datasets.Add(ds);

            RandomRGB("doughnut", "0", data.datasets);

            return Json(JSonRetFormatted(data));
        }

        public JsonResult ChartEmployeeByAge()
        {
            EmployeeWorkingInfoCollection coll = new EmployeeWorkingInfoCollection();
            DataTable dtb = coll.GetEmployeeByAge();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ItemName"].ToString()).Distinct().ToList();


            var sexs = new string[] { "M", "F" };

            foreach (var sex in sexs)
            {
                var ds = new dataset();
                ds.label = sex;
                ds.data = data.labels.Select(x =>
                    dtb.AsEnumerable().Where(y =>
                        y["ItemName"].ToString() == x &&
                        y["SRGenderType"].ToString() == sex)
                    .Select(y => Convert.ToDouble(y["ChartCount"])).FirstOrDefault()).ToList();

                ds.backgroundColor = sex == "M" ? "rgb(0,0,255,0.2)" : "rgb(255,0,128,0.2)";
                ds.borderColor = sex == "M" ? "rgb(0,0,255)" : "rgb(255,0,128)";

                data.datasets.Add(ds);
            }
            
            return Json(JSonRetFormatted(data));
        }

        public JsonResult ChartEmployeeByLicense()
        {
            EmployeeWorkingInfoCollection coll = new EmployeeWorkingInfoCollection();
            DataTable dtb = coll.GetEmployeeByLicense();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ItemName"].ToString()).Distinct().ToList();

            var sts = new string[] { "Active", "Expired" };

            foreach (var st in sts)
            {
                var ds = new dataset();
                ds.label = st;
                ds.data = data.labels.Select(x =>
                    dtb.AsEnumerable().Where(y =>
                        y["ItemName"].ToString() == x &&
                        y["StActive"].ToString() == st)
                    .Select(y => Convert.ToDouble(y["ChartCount"])).FirstOrDefault()).ToList();

                ds.backgroundColor = st == "Active" ? "rgb(0,128,0,0.2)" : "rgb(255,0,0,0.2)";
                ds.borderColor = st == "Active" ? "rgb(0,128,0)" : "rgb(255,0,0)";

                data.datasets.Add(ds);
            }

            return Json(JSonRetFormatted(data));
        }

        public JsonResult ChartEmployeeByFieldLabor()
        {
            EmployeeWorkingInfoCollection coll = new EmployeeWorkingInfoCollection();
            DataTable dtb = coll.GetEmployeeByFieldLabor();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["ItemName"].ToString()).ToList();

            var ds = new dataset();
            ds.label = "Count";
            ds.data = data.labels.Select(x =>
                dtb.AsEnumerable().Where(y =>
                    y["ItemName"].ToString() == x)
                .Select(y => Convert.ToDouble(y["ChartCount"])).FirstOrDefault()).ToList();
            data.datasets.Add(ds);

            RandomRGB("line", "1", data.datasets);

            return Json(JSonRetFormatted(data));
        }

        public JsonResult ChartEmployeeByUnit()
        {
            EmployeeWorkingInfoCollection coll = new EmployeeWorkingInfoCollection();
            DataTable dtb = coll.GetEmployeeByUnit();

            var data = new data();
            data.labels = dtb.AsEnumerable().Select(x => x["OrganizationUnitName"].ToString()).ToList();

            var ds = new dataset();
            ds.label = "Count";
            ds.data = data.labels.Select(x =>
                dtb.AsEnumerable().Where(y =>
                    y["OrganizationUnitName"].ToString() == x)
                .Select(y => Convert.ToDouble(y["ChartCount"])).FirstOrDefault()).ToList();
            data.datasets.Add(ds);

            RandomRGB("line", "1", data.datasets);

            return Json(JSonRetFormatted(data));
        }
        #endregion
        #endregion

        #region Toast
        public JsonResult GetToastData(string DateStart, string DateEnd)
        {
            List<ToastMsg> msgs = new List<ToastMsg>(); 

            var userID = AppSession.UserLogin.UserID;
            if (!string.IsNullOrEmpty(userID))
            {
                var user = new AppUser();
                user.LoadByPrimaryKey(userID);
                if (user.PersonID.HasValue) {
                    var lType = new string[] { "STR", "SIP", "SPK", "SPKK" };

                    var coll = new PersonalLicenceCollection();
                    DataTable tbl = coll.GetPersonalLicenseRecap(user.PersonID.Value);

                    foreach (var row in tbl.AsEnumerable().Where(r => lType.Contains(r["LicenseTypeName"]))) {
                        var iRemaining = System.Convert.ToInt32(row["Remaining"]);
                        if (iRemaining < AppSession.Parameter.DayLimitEmployeeLicenseWarning) {
                            if (iRemaining < 0)
                            {
                                msgs.Add(new ToastMsg() { type = 3, msg = string.Format("Your {0} license has expired", row["LicenseTypeName"].ToString()) });
                            }
                            else {
                                msgs.Add(new ToastMsg() { type = 2, msg = string.Format("Your {0} license will be expired in {1} days", row["LicenseTypeName"].ToString(), row["Remaining"].ToString()) });
                            }
                            
                        }
                    }

                    var programId = AppConstant.Program.MedicCredentialApprovalByDirector;
                    var prg = new AppProgram();
                    if (prg.LoadByPrimaryKey(programId) && prg.IsVisible == true)
                    {
                        var ugp = new AppUserGroupProgramQuery("a");
                        var uug = new AppUserUserGroupQuery("b");
                        ugp.InnerJoin(uug).On(uug.UserGroupID == ugp.UserGroupID && uug.UserID == userID);
                        ugp.Where(ugp.ProgramID == programId);
                        var dtb = ugp.LoadDataTable();
                        if (dtb.Rows.Count > 0)
                        {
                            //Medic - Approval By Director
                            var dirQ = new CredentialProcessQuery("a");
                            dirQ.Where(dirQ.SRProfessionGroup == "01", dirQ.IsApproved == true, dirQ.IsVerified == true, dirQ.IsCredentialing == true, dirQ.IsRecommendationLetter == true, 
                                dirQ.Or(dirQ.IsVerified2.IsNull(), dirQ.IsVerified2 == false));
                            dirQ.Select(dirQ.TransactionNo);
                            var dirDtb = dirQ.LoadDataTable();
                            if (dirDtb.Rows.Count > 0)
                            {
                                msgs.Add(new ToastMsg() { type = 1, msg = string.Format("You have {0} outstanding credentials that need to be approved ({1})", dirDtb.Rows.Count.ToString(), prg.ProgramName) });
                            }
                        }

                        //Medic - Approval By Supervisor
                        var spvQ = new CredentialProcessQuery("a");
                        var ewiQ = new EmployeeWorkingInfoQuery("c");
                        spvQ.InnerJoin(ewiQ).On(ewiQ.PersonID == spvQ.PersonID && ewiQ.SupervisorId == user.PersonID.ToInt());
                        spvQ.Select(spvQ.TransactionNo);
                        spvQ.Where(spvQ.SRProfessionGroup == "01", spvQ.IsApproved == true,
                            spvQ.Or(spvQ.IsVerified.IsNull(), spvQ.IsVerified == false));
                        var spvDtb = spvQ.LoadDataTable();
                        if (spvDtb.Rows.Count > 0)
                        {
                            var programId2 = AppConstant.Program.MedicCredentialApprovalBySupervisor;
                            prg = new AppProgram();
                            if (prg.LoadByPrimaryKey(programId2))
                            {
                                msgs.Add(new ToastMsg() { type = 1, msg = string.Format("You have {0} outstanding credentials that need to be approved ({1})", spvDtb.Rows.Count.ToString(), prg.ProgramName) });
                            }
                        }
                    }

                    if (msgs.Count > 0) {
                        return Json(JSonRetFormatted(msgs));
                    }
                }
            }

            return Json(JSonRetFormatted(msgs, false));
        }
        #endregion

        #region Status Proporsi saat approve PAR
        public JsonResult ParGetProgressProporsiJasmed(string InvoiceNo)
        {
            var inv = new Invoices();
            if (inv.LoadByPrimaryKey(InvoiceNo))
            {
                if ((new string[] { "01", "02" }).Contains(inv.SRPhysicianFeeProportionalStatus))
                {
                    // kalau progresnya sudah lewat 1 jam kemungkinan besar gagal, jadi set sebagai gagal
                    var tm = DateTime.Now - inv.LastUpdateDateTime;
                    if (tm.HasValue)
                    {
                        if (tm.Value.TotalHours > 1)
                        {
                            inv.SRPhysicianFeeProportionalStatus = "04"; // set failed supaya bisa diproses ulang
                            inv.PhysicianFeeProportionalErrMessage = "Failed: process takes too long";
                            inv.Save();
                        }
                    }
                }

                return Json(JSonRetFormatted(new { 
                    inv.InvoiceNo, 
                    inv.SRPhysicianFeeProportionalStatus,
                    inv.PhysicianFeeProportionalPercentage,
                    inv.PhysicianFeeProportionalErrMessage
                }));
            }
            else {
                return Json(JSonRetFormatted("Invoice not found", false));
            }
        }

        public JsonResult ParSetProporsiJasmed(string InvoiceNo)
        {
            var UserID = AppSession.UserLogin.UserID;
            System.Threading.Thread worker = new System.Threading.Thread(() => DoJasmed(InvoiceNo, UserID));
            worker.Start();
            

            return Json(JSonRetFormatted("Done"));
        }

        public void DoJasmed(string InvoiceNo, string UserID) {
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.SetInvoicePayment(InvoiceNo, UserID);
            feeColl.Save();
        }
        #endregion 
    }
}