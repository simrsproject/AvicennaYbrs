using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.WebService;
using Temiang.Avicenna.Model;
using Temiang.Avicenna.BusinessObject.Common;
using System.Web.Security;
using System.Web.Routing;
using System.Data;

namespace Temiang.Avicenna.Controllers
{
    #region DataTable
    public class jQueryDatatableReturn
    {
        public string status;
        public int draw;
        public int recordsTotal;
        public int recordsFiltered;
        public object data;

        public string Serialize()
        {
            var sRet = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
            return sRet;
        }
    }

    public class jQueryDatatableRequest
    {
        public int limit, start, orderColumn, draw;
        public string searchKey, orderDir;

        public void RetrieveQueryString()
        {
            limit = System.Convert.ToInt32(HttpContext.Current.Request.Params["length"] == null ? "0" : HttpContext.Current.Request.Params["length"]);
            start = System.Convert.ToInt32(HttpContext.Current.Request.Params["start"] == null ? "0" : HttpContext.Current.Request.Params["start"]);
            searchKey = HttpContext.Current.Request.Params["search[value]"] == null ? "" : HttpContext.Current.Request.Params["search[value]"];
            orderColumn = System.Convert.ToInt32(HttpContext.Current.Request.Params["order[0][column]"] == null ? "" : HttpContext.Current.Request.Params["order[0][column]"]);
            orderDir = HttpContext.Current.Request.Params["order[0][dir]"] == null ? "" : HttpContext.Current.Request.Params["order[0][dir]"];
            draw = System.Convert.ToInt32(HttpContext.Current.Request.Params["draw"] == null ? "0" : HttpContext.Current.Request.Params["draw"]);
        }

        public string GetColumnName(int colIndex)
        {
            return HttpContext.Current.Request.Params[string.Format("columns[{0}][name]", colIndex.ToString())] == null ? "" :
                HttpContext.Current.Request.Params[string.Format("columns[{0}][name]", colIndex.ToString())];
        }
    }
    #endregion

    public class BaseController : Controller
    {
        #region Public Properties
        private string _ProgramID;
        public string ProgramID
        {
            get { return _ProgramID; }
            set { _ProgramID = value; }
        }
        #endregion
        
        #region Json Return
        public JsonRetWS.JsonRet JSonRetFormatted(object o)
        {
            return JSonRetFormatted(o, true, "");
        }

        public JsonRetWS.JsonRet JSonRetFormatted(object o, bool status)
        {
            return JSonRetFormatted(o, status, "");
        }

        public JsonRetWS.JsonRet JSonRetFormatted(object o, bool status, string errorCode)
        {
            var ret = new JsonRetWS.JsonRet();
            ret.setValue(status, errorCode, o);
            return ret;
        }

        public static List<Dictionary<string, object>> ConvertDataTabletoObject(DataTable dt)
        {
            DataTableAddColumYMDHMS(dt);

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                rows.Add(ConvertDataRowtoObject(dr));
            }
            return rows;
        }
        private static void DataTableAddColumYMDHMS(DataTable dt)
        {
            List<string> columNamesOfDateTime = new List<string>();
            foreach (System.Data.DataColumn c in dt.Columns)
            {
                if (c.DataType == typeof(DateTime))
                {
                    columNamesOfDateTime.Add(c.ColumnName);
                }
            }
            if (columNamesOfDateTime.Count > 0)
            {
                foreach (var columName in columNamesOfDateTime)
                {
                    var nc = dt.Columns.Add(columName + "_yMdHms", typeof(string));
                    // set value
                    foreach (System.Data.DataRow xrow in dt.Rows)
                    {
                        if (xrow[columName] is DBNull)
                        {
                            //skip, no value to be convert
                        }
                        else
                        {
                            xrow[nc] = ((DateTime)xrow[columName]).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                }

                foreach (var columName in columNamesOfDateTime) {
                    dt.Columns.Remove(dt.Columns[columName]);
                }

                // rename column
                foreach (var columName in columNamesOfDateTime)
                {
                    dt.Columns[columName + "_yMdHms"].ColumnName = columName;
                }
            }
            dt.AcceptChanges();
        }
        private static Dictionary<string, object> ConvertDataRowtoObject(DataRow dr)
        {
            Dictionary<string, object> row;
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dr.Table.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            return row;
        }
        #endregion
        #region Init Page
        public bool IsLoggedIn()
        {
            if (!string.IsNullOrEmpty(AppSession.UserLogin.UserID)) {
                ViewData["UserID"] = AppSession.UserLogin.UserID;
            }
            return !string.IsNullOrEmpty(AppSession.UserLogin.UserID);
        }
        public void InitConstanta()
        {
            ViewData["UrlRoot"] = Helper.UrlRoot2();

            // load constanta appparameter
            ViewData["AppParam_HealthcareInitial"] = AppSession.Parameter.HealthcareInitial;

            ViewData["Brand"] = new Brand();

            Healthcare hc;
            try
            {
                hc = Healthcare.GetHealthcare();
            }
            catch {
                hc = new Healthcare();
            }
            
            ViewData["Healthcare_HealthcareName"] = hc.HealthcareName;
            ViewData["Healthcare_Address"] = string.Join(" ", hc.AddressLine1, hc.AddressLine2, hc.City, hc.ZipCode, "Telp:" + hc.PhoneNo, "Fax:" + hc.FaxNo);
            ViewData["Healthcare_HealthcareLogo"] = hc.HealthcareLogo;
            ViewData["AppParam_QueueDisplayScrollingText"] = AppSession.Parameter.QueueDisplayScrollingText; //"Chkdsk /r finds and attempts to repair corrupted portions of your hard drive. It automatically runs chkdsk /f as part of this process to correct errors on your disk as well. This means that you don’t have to run the command chkdsk /f /r. Chkdsk / r is one of the most valuable check disk commands, because it can alert you to parts of your hard drive that are starting to malfunction.This can serve as a reminder to back up important files so you don’t lose them to hard drive failure. Before running chkdsk / r, you should backup any valuable files to another storage drive, as they may get deleted after the process completes.There’s a small chance that if these files are located near a bad sector, chkdsk / r will identify them as corrupted and get rid of them.";
            ViewData["AppParam_QueueDisplayScrollingDurationText"] = AppSession.Parameter.QueueDisplayScrollingDurationText;
            ViewData["AppParam_QueueDisplaySloganHealthcare"] = AppSession.Parameter.QueueDisplaySloganHealthcare;
        }
        private bool InitStart()
        {
            InitConstanta();
            if (!IsLoggedIn())
            {
                return false;
            }

            // load menu
            var prgColl = new AppProgramCollection();
            //prgColl.LoadMvcByUserID("MVC", AppSession.UserLogin.UserID); //("sci");
            prgColl.LoadPRG(AppSession.UserLogin.UserID);
            //ViewData["AppProgramsTop"] = prgColl.Where(x => string.IsNullOrEmpty(x.ParentProgramID)).OrderBy(x => x.ProgramID);
            ViewData["AppPrograms"] = prgColl;

            return true;
        }
        public bool InitStartPage()
        {
            if (!InitStart()) return false;

            AppProgramCollection prgColl = (AppProgramCollection)ViewData["AppPrograms"];

            if (!string.IsNullOrEmpty(ProgramID) & !prgColl.Where(x => x.ProgramID == ProgramID).Any())
            {
                // no access
                throw new Exception("You have no right to view this page");
            }

            return true;
        }
        public void InitAjaxPage()
        {
            if (!IsLoggedIn()) {
                throw new Exception("Session Expired");
            }
            InitConstanta();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            var model = new ErrorModel();
            model.Message = filterContext.Exception.Message;
            model.Source = filterContext.Exception.Source;
            model.StackTrace = filterContext.Exception.StackTrace;
            TempData["datacontainer"] = model;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = RedirectToAction("ErrorAjax");
            }
            else
            {
                filterContext.Result = RedirectToAction("Error");
            }
        }
        #endregion
        public ActionResult ErrorAjax()
        {
            InitConstanta();
            return View("ErrorAjax");
        }
        public ActionResult Error()
        {
            InitStart();
            return View("Error");
        }
        public ActionResult Login()
        {
            InitConstanta();
            return View("Login");
        }

        [HttpPost]
        public ActionResult Logon(UserLoginModel model, string returnUrl)
        {
            ViewData["ErrMessage"] = "";
            if (ModelState.IsValid)
            {
                if (model.UserID.Equals(string.Empty) || model.Password.Equals(string.Empty))
                {

                }
                else
                {
                    var user = new AppUser();
                    if (user.LoadByPrimaryKey(model.UserID))
                    {
                        var _userLogin = new UserLogin(user);
                        var attempt = Convert.ToInt32(Session["_attemptLogin"]);
                        var keyPair = _userLogin.Validate(user, Encryptor.Encrypt(model.Password), ref attempt);
                        Session["_attemptLogin"] = attempt;
                        switch (keyPair.Key)
                        {
                            case "1":
                                {
                                    ViewData["ErrMessage"] = keyPair.Value;
                                    break;
                                }
                            case "2":
                                {
                                    ViewData["ErrMessage"] = keyPair.Value;
                                    break;
                                }
                            default:
                                {
                                    _userLogin.UserHostName = Common.Helper.ClientIP();
                                    AppSession.UserLogin = _userLogin;
                                    return RedirectToAction("Index", "Home");
                                    break;
                                }
                        }
                    }
                    else
                    {
                        ViewData["ErrMessage"] = "User id is not registered, contact administrator";
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            InitConstanta();
            return View("Login", model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.RemoveAll();
            HttpContext.Session.Abandon();
            return RedirectToAction("Login");
        }

        #region Charts
        public void RandomRGB(string ChartType, string bgOpacity, params List<dataset>[] dss)
        {
            Random rnd = new Random();

            switch (ChartType)
            {
                case "bar":
                case "line":
                    {
                        foreach (var dsx in dss)
                        {
                            Byte[] b = new Byte[3];
                            List<Byte[]> lb = new List<byte[]>();
                            foreach (var ds in dsx)
                            {
                                //rnd.NextBytes(b);
                                b = RandomStrongColor(rnd, b, lb, 0);

                                ds.backgroundColor = string.Format("rgb({0},{1},{2},{3})", b[0], b[1], b[2], bgOpacity);
                                ds.borderColor = string.Format("rgb({0},{1},{2},1)", b[0], b[1], b[2]);
                            }
                        }
                        break;
                    }
                case "doughnut":
                case "pie":
                    {
                        var data = dss[0][0].data;
                        List<string> clr = new List<string>();
                        Byte[] b = new Byte[3];
                        List<Byte[]> lb = new List<byte[]>();
                        foreach (var d in data)
                        {
                            //rnd.NextBytes(b);
                            b = RandomStrongColor(rnd, b, lb, 0);

                            clr.Add(string.Format("rgb({0},{1},{2},1)", b[0], b[1], b[2]));
                        }

                        foreach (var dsx in dss)
                        {
                            foreach (var ds in dsx)
                            {
                                ds.backgroundColor = clr;
                            }
                        }
                        break;
                    }
            }
        }

        public Byte[] RandomStrongColor(Random rnd, Byte[] b, List<byte[]> lb, int depth)
        {
            //rnd.NextBytes(b);
            //return b;

            bool pass = false;
            int i = 0;
            depth = depth + 1;
            if (depth == 30) return b;

            do
            {
                if (i == 30) break;
                rnd.NextBytes(b);
                if (lb.Count == 0)
                {
                    pass = true;
                }
                else
                {
                    var b_r = !lb.Where(x => Math.Abs(x[0] - b[0]) < 15).Any();
                    var b_g = !lb.Where(x => Math.Abs(x[1] - b[1]) < 15).Any();
                    var b_b = !lb.Where(x => Math.Abs(x[2] - b[2]) < 15).Any();
                    var b_a = !lb.Where(x => Math.Abs(x[0] - b[0] + x[1] - b[1] + x[2] - b[2]) < 30).Any();
                    pass = (b_r && b_g && b_b && b_a);
                }
                i++;
            } while (!pass);

            if (b[0] > 128 && b[1] > 128 && b[2] > 128)
            {
                return RandomStrongColor(rnd, b, lb, depth);
            }

            var bPass = new Byte[3];
            bPass[0] = b[0]; bPass[1] = b[1]; bPass[2] = b[2];
            lb.Add(bPass);

            return b;
        }

        public class data
        {
            private List<string> _labels = new List<string>();
            private List<dataset> _datasets = new List<dataset>();
            public List<string> labels
            {
                get { return _labels; }
                set { _labels = value; }
            }
            public List<dataset> datasets
            {
                get { return _datasets; }
                set { _datasets = value; }
            }
        }
        public class dataset
        {
            private string _label;
            private List<double> _data;
            private decimal _borderWidth;
            object _backgroundColor;
            string _borderColor;
            bool _fill;
            public string label
            {
                get { return _label; }
                set { _label = value; }
            }
            public List<double> data
            {
                get { return _data; }
                set { _data = value; }
            }

            public decimal borderWidth
            {
                get { return _borderWidth; }
                set { _borderWidth = value; }
            }
            public object backgroundColor
            {
                get { return _backgroundColor; }
                set { _backgroundColor = value; }
            }
            public string borderColor
            {
                get { return _borderColor; }
                set { _borderColor = value; }
            }
            public bool fill
            {
                get { return _fill; }
                set { _fill = value; }
            }

            public dataset()
            {
                _data = new List<double>();
                _borderWidth = 1;
                _fill = true;
            }
        }
        #endregion
    }
}