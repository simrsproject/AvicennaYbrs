using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using System.Configuration;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.Common
{
    public class BasePageBootstrap : Page
    {

        private string _programID;
        private UserAccess _userAccess;

        #region User Access

        public UserAccess UserAccess
        {
            get
            {
                if (_userAccess != null)
                    return _userAccess;

                if (ProgramID == string.Empty)
                    return new UserAccess();

                var obj = ViewState["_userAccess"];
                if (obj == null)
                {
                    _userAccess = new UserAccess(AppSession.UserLogin.UserID, ProgramID);
                    ViewState["_userAccess"] = _userAccess;
                    return _userAccess;
                }

                return (UserAccess)obj;
            }
        }

        public bool IsUserAddAble
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsProgramAddAble && access.IsAddAble;

                return false;
            }
        }

        public bool IsUserEditAble
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsProgramEditAble && access.IsEditAble;
                return false;
            }
        }

        public bool IsUserApproveAble
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsProgramApprovalAble && access.IsApprovalAble;
                return false;
            }
        }

        public bool IsUserVoidAble
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsProgramVoidAble && access.IsVoidAble;
                return false;
            }
        }

        public bool IsUserDeleteAble
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsProgramDeleteAble && access.IsDeleteAble;
                return false;
            }
        }

        public bool IsListLoadRecordIfFiltered
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsListLoadRecordIfFiltered;
                return false;
            }
        }

        public bool IsListLoadRecordOnInit
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsListLoadRecordOnInit;
                return false;
            }
        }

        #endregion


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsShowValueFromCookie)
            {
                if (IsPostBack)
                    SaveValueToCookie();
                else
                    RestoreValueFromCookie();
            }
        }

        public string PageID
        {
            get
            {
                if (ViewState["_randomID"] == null)
                {
                    var randomID = (new Random()).Next(1, 1000000).ToString();
                    ViewState["_randomID"] = randomID;
                }
                return (string)ViewState["_randomID"];
            }
        }
        protected bool IsShowValueFromCookie
        {
            get
            {
                if (ViewState["_aufc"] == null)
                {
                    ViewState["_aufc"] = false;
                }
                return (bool)ViewState["_aufc"];
            }
            set
            {
                ViewState["_aufc"] = value;
            }
        }

        protected string ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }

        protected string SessionNameForList
        {
            get
            {
                //Bila ada query string pgID maka nilai PageID diambil dari query string
                //string pageID = Page.Request.QueryString["pgID"];
                return string.Format("_list.{0}", _programID);
                //return string.Format("_list.{0}.{1}", _programID, pageID ?? PageID);
            }
        }

        protected string SessionNameForQuery
        {
            get
            {
                //Bila ada query string pgID maka nilai PageID diambil dari query string
                //string pageID = Page.Request.QueryString["pgID"];
                return string.Format("_que.{0}", _programID);
                //return string.Format("_que.{0}.{1}", _programID, pageID ?? PageID);
            }
        }

        //protected override void OnInit(EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        if (!string.IsNullOrEmpty(Request.QueryString["logid"]) && Request.QueryString["logid"] != "undefined")
        //        {
        //            // Cek di table UserLog jika dipanggil dg kiriman sessionid dan jika valid maka tidak dimunculkan login page
        //            var userLog = new UserLog();

        //            //// Cek existensi, logout & clientIP harus sama dg yg di log 
        //            //var ipAddress = Helper.GetUserHostName();
        //            //if (userLog.LoadByPrimaryKey(Convert.ToInt64(Request.QueryString["logid"]))
        //            //    && userLog.LogoutDateTime == null
        //            //    && (userLog.ClientIP == ipAddress || (ipAddress == "::1" && userLog.ClientIP == "127.0.0.1") || (ipAddress == "127.0.0.1" && userLog.ClientIP == "::1")))
        //            //{


        //            if (userLog.LoadByPrimaryKey(Convert.ToInt64(Request.QueryString["logid"])) && userLog.LogoutDateTime == null)
        //            {
        //                userLog.LogoutDateTime = DateTime.Now;
        //                userLog.Save();
        //                var user = new AppUser();
        //                if (user.LoadByPrimaryKey(userLog.UserID))
        //                {
        //                    var userLogin = new UserLogin(user);
        //                    userLogin.UserHostName = userLog.ClientIP;

        //                    AppSession.UserLogin = userLogin;
        //                }
        //            }

        //            // Redirect dgn menghilangkan QueryString logid untuk mencegah code diatas dijalankan kembali
        //            var nvc = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        //            var qsExludeLogId = string.Empty;
        //            foreach (var key in nvc.AllKeys)
        //            {
        //                if (key == "logid") continue;
        //                qsExludeLogId = string.Concat(qsExludeLogId, "&", key, "=", Request.QueryString[key]);
        //            }

        //            var url = string.Format("{0}{1}", Request.AppRelativeCurrentExecutionFilePath, string.IsNullOrEmpty(qsExludeLogId) ? string.Empty : "?" + qsExludeLogId.Substring(1));
        //            Response.Redirect(url);
        //        }
        //    }
        //    base.OnInit(e);
        //}

        protected override void OnInit(EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["logid"]) && Request.QueryString["logid"] != "undefined")
                {
                    var userLog = new UserLog();
                    var userLogID = Convert.ToInt64(Request.QueryString["logid"]);
                    if (userLog.LoadByPrimaryKey(userLogID))
                    {
                        if (userLog.UserID.Equals(Request.QueryString["uid"]))
                        {
                            // Set Logout prev login
                            //userLog.LogoutDateTime = DateTime.Now;
                            //userLog.Save();

                            var user = new AppUser();
                            if (user.LoadByPrimaryKey(userLog.UserID))
                            {
                                var userLogin = new UserLogin(user);
                                userLogin.UserHostName = userLog.ClientIP;
                                userLogin.UserLogID = userLogID; // supaya tidak membuat log baru
                                AppSession.UserLogin = userLogin;
                            }
                        }
                    }

                    // Redirect dgn menghilangkan QueryString logid untuk mencegah code diatas dijalankan kembali
                    var nvc = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    var qsExludeLogId = string.Empty;
                    foreach (var key in nvc.AllKeys)
                    {
                        if (key == "logid" || key == "uid") continue;
                        qsExludeLogId = string.Concat(qsExludeLogId, "&", key, "=", Request.QueryString[key]);
                    }

                    var url = string.Format("{0}{1}", Request.AppRelativeCurrentExecutionFilePath, string.IsNullOrEmpty(qsExludeLogId) ? string.Empty : "?" + qsExludeLogId.Substring(1));
                    Response.Redirect(url);
                }
            }
            base.OnInit(e);
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            //Login
            if (!Request.RawUrl.ToLower().Contains("login.") && string.IsNullOrEmpty(AppSession.UserLogin.UserID))
            {
                Session["_returnUrl"] = Request.RawUrl;
                Response.Redirect("~/Login/Login.aspx?RetunUrl=" + Request.RawUrl);
            }

            if (ProgramID != AppConstant.Program.MyHome)
            {
                if (!string.IsNullOrEmpty(ProgramID) && !UserAccess.IsExist)
                {
                    Response.Redirect("~/AccessFailed.aspx?url=" + Request.RawUrl);
                }
            }

            if (!IsPostBack)
            {

            }
        }

        protected static CultureInfo CurrentCultureInfo
        {
            get
            {
                return new CultureInfo("en-US");
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            try
            {
                //if (this.Title.ToLower().Equals("untitled page")) Page.Title = "AVICENNA";
                var brand = new Brand();
                if (Page.Title == null || Page.Title.Contains("Untitled Page")) Page.Title = brand.Name.ToUpper();

                // current menu
                var currApp = new AppProgram();
                currApp.LoadByPrimaryKey(ProgramID);
                ((Label)Helper.FindControlRecursive(Page, "lblPageTitle")).Text = currApp.ProgramName;

                // current login user
                var displayUserName = string.IsNullOrEmpty(AppSession.UserLogin.ParamedicName) ? AppSession.UserLogin.UserName : AppSession.UserLogin.ParamedicName;

                ((Label)Helper.FindControlRecursive(Page, "lblUserName")).Text = string.Concat(displayUserName, "<br />{", Helper.GetUserHostName(), "}");
            }
            catch (Exception ex)
            {
                // nothing
            }




            if (IsPostBack)
            {

            }

            UpdateLinkRegistrationInfo();
            UpdateLinkGuarantorInfo();

            if (string.IsNullOrEmpty(ProgramID)) return;
        }

        private void UpdateLinkRegistrationInfo()
        {
            try
            {
                // try to find control named lblRegistrationInfo
                var master = this.Master;
                if (master != null)
                {
                    var masterPlaceHolder = this.Master.FindControl("ContentPlaceHolder1");
                    if (masterPlaceHolder != null)
                    {
                        var lbl = masterPlaceHolder.FindControl("lblRegistrationInfo"); //FindControl("lbl123");
                        if (lbl != null)
                        {
                            // update ctl text
                            try
                            {
                                var oLbl = (Label)lbl;

                                // find AssociatedControlID
                                //string regID = oLbl.AssociatedControlID;

                                var objAsc = this.Master.FindControl("ContentPlaceHolder1").FindControl(oLbl.AssociatedControlID);
                                string regNo = "";
                                if (objAsc is TextBox) regNo = (objAsc as TextBox).Text;
                                if (objAsc is RadTextBox) regNo = (objAsc as RadTextBox).Text;
                                if (objAsc is Label) regNo = (objAsc as Label).Text;

                                // find info count
                                // IF YOU HAVE ERROR HERE PLEASE UPDATE BUSINESSOBJECT
                                var qInfo = new RegistrationInfoQuery();
                                var cInfo = new RegistrationInfoCollection();
                                qInfo.Where(qInfo.RegistrationNo == regNo);
                                cInfo.Load(qInfo);

                                oLbl.Text = (cInfo.Count == 0) ? "" : cInfo.Count.ToString();
                            }
                            catch (Exception ex)
                            {
                                // error casting the control
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error casting the control
            }
        }

        private void UpdateLinkGuarantorInfo()
        {
            // try to find control named lblGuarantorInfo
            var master = this.Master;
            if (master != null)
            {
                var masterPlaceHolder = this.Master.FindControl("ContentPlaceHolder1");
                if (masterPlaceHolder != null)
                {
                    var lbl = masterPlaceHolder.FindControl("lblGuarantorInfo"); //FindControl("lbl123");
                    if (lbl != null)
                    {
                        // update ctl text
                        try
                        {
                            var oLbl = (Label)lbl;

                            // find AssociatedControlID
                            //string regID = oLbl.AssociatedControlID;

                            var objAsc = this.Master.FindControl("ContentPlaceHolder1").FindControl(oLbl.AssociatedControlID);
                            string guarId = "";
                            if (objAsc is TextBox) guarId = (objAsc as TextBox).Text;
                            if (objAsc is RadTextBox) guarId = (objAsc as RadTextBox).Text;
                            if (objAsc is Label) guarId = (objAsc as Label).Text;
                            if (objAsc is RadComboBox) guarId = (objAsc as RadComboBox).SelectedValue;

                            // find info count
                            // IF YOU HAVE ERROR HERE PLEASE UPDATE BUSINESSOBJECT
                            var qInfo = new GuarantorInfoQuery();
                            var cInfo = new GuarantorInfoCollection();
                            qInfo.Where(qInfo.GuarantorID == guarId);
                            cInfo.Load(qInfo);

                            oLbl.Text = (cInfo.Count == 0) ? "" : cInfo.Count.ToString();
                        }
                        catch (Exception ex)
                        {
                            // error casting the control
                        }
                    }
                }
            }
        }

        private static void PopulateParentProgram(DataTable dtb, AppProgram prog, string parentProgramID)
        {
            if (parentProgramID != string.Empty && dtb.Rows.Find(parentProgramID) == null)
            {
                prog.QueryReset();
                if (prog.LoadByPrimaryKey(parentProgramID))
                {
                    DataRow newRow = dtb.NewRow();
                    newRow["ProgramID"] = prog.ProgramID;
                    newRow["ParentProgramID"] = prog.ParentProgramID;
                    newRow["ProgramName"] = prog.ProgramName;
                    newRow["IsBeginGroup"] = prog.IsBeginGroup;
                    newRow["IsDiscontinue"] = prog.IsDiscontinue;
                    newRow["IsVisible"] = prog.IsVisible;
                    newRow["RowIndex"] = prog.RowIndex;
                    newRow["IsParentProgram"] = prog.IsParentProgram;
                    newRow["NavigateUrl"] = prog.NavigateUrl;
                    newRow["AccessKey"] = prog.str.AccessKey;
                    dtb.Rows.Add(newRow);

                    parentProgramID = prog.ParentProgramID;
                    PopulateParentProgram(dtb, prog, parentProgramID);
                }
            }
        }

        private static string RedirectedMedicalApp
        {
            get { return ConfigurationManager.AppSettings["MedicalAppUrlLocation"]; }
        }

        private DataTable PopulateUserProgram(out EnumerableRowCollection<DataRow> menuDataRows)
        {
            var obj = Session["fw_userprogram"];
            if (obj == null)
            {
                DataTable dtbMenu = null;
                //Load program
                using (var trans = new esTransactionScope())
                {
                    //Load program
                    var progQ = new AppProgramQuery("a");
                    var userQ = new AppUserUserGroupQuery("b");
                    var grpProgQ = new AppUserGroupProgramQuery("c");

                    progQ.InnerJoin(grpProgQ).On(progQ.ProgramID == grpProgQ.ProgramID);
                    progQ.InnerJoin(userQ).On(grpProgQ.UserGroupID == userQ.UserGroupID);
                    progQ.Where(
                        userQ.UserID == AppSession.UserLogin.UserID,
                        progQ.IsVisible == true,
                        progQ.ProgramType == "PRG"
                        );

                    if (ConfigurationManager.AppSettings["ExcludedAppProgramID"] != null)
                    {
                        progQ.Where(progQ.ProgramID.NotIn(ConfigurationManager.AppSettings["ExcludedAppProgramID"].Split(',')));
                    }

                    progQ.Select(progQ.ProgramID, progQ.ParentProgramID, progQ.ProgramName, progQ.IsBeginGroup,
                                 progQ.IsDiscontinue, progQ.IsVisible, progQ.RowIndex, progQ.IsParentProgram,
                                 progQ.NavigateUrl, progQ.AccessKey, progQ.IsProgramRedirected, progQ.ApplicationID);
                    progQ.es.Distinct = true;
                    progQ.OrderBy(progQ.ParentProgramID.Ascending, progQ.RowIndex.Ascending);

                    var dtb = progQ.LoadDataTable();

                    // Replace baseUrl other Application
                    if (ApplicationSettings.DefaultApplication != null)
                    {
                        var defaultApplicationID = ApplicationSettings.DefaultApplication.Name;
                        var appID = string.Empty;
                        var appBaseUrl = string.Empty;
                        var logID = AppSession.UserLogin.UserLogID;
                        var uid = AppSession.UserLogin.UserID;
                        foreach (DataRow row in dtb.Rows)
                        {
                            if (row["ApplicationID"] == DBNull.Value ||
                                string.IsNullOrEmpty(row["ApplicationID"].ToString()) ||
                                defaultApplicationID.Equals(row["ApplicationID"])) continue;

                            if (!appID.Equals(row["ApplicationID"]))
                            {
                                appID = row["ApplicationID"].ToString();
                                appBaseUrl = ApplicationSettings.ApplicationInfo.Applications[appID].BaseUrl;
                            }

                            var url = row["NavigateUrl"].ToString();
                            if (!string.IsNullOrEmpty(url))
                            {
                                row["NavigateUrl"] = string.Format("{0}{1}{2}logid={3}&uid={4}", appBaseUrl, url.Substring(1),
                                    url.Contains("?") ? "&" : "?", logID, uid);
                            }
                        }

                    }

                    dtbMenu = dtb.Copy();
                    dtbMenu.PrimaryKey = new[] { dtbMenu.Columns["ProgramID"] };

                    //Load Parent Program
                    var prog = new AppProgram();
                    var parentProgramID = string.Empty;

                    foreach (var row in from DataRow row in dtb.Rows
                                        where !row["ParentProgramID"].Equals(parentProgramID)
                                        where dtbMenu.Rows.Find(row["ParentProgramID"]) == null
                                        select row)
                    {
                        parentProgramID = row["ParentProgramID"].ToString();
                        PopulateParentProgram(dtbMenu, prog, parentProgramID);
                    }
                }

                if (!string.IsNullOrEmpty(AppSession.Parameter.HumanResourceUserID))
                {
                    var hrUser = AppSession.Parameter.HumanResourceUserID.Split(',').Where(h => !string.IsNullOrEmpty(h.Trim()));
                    if (hrUser.Any() && !hrUser.Any(h => AppSession.UserLogin.UserID.Contains(h.Trim())))
                    {
                        menuDataRows = dtbMenu.AsEnumerable().Where(d => !AppConstant.Module.HumanResourceModules.Contains(d.Field<string>("ProgramID")) &&
                                                                     string.IsNullOrEmpty(d.Field<string>("ParentProgramID")))
                                                         .OrderBy(d => d.Field<Int16>("RowIndex"));
                    }
                    else
                        menuDataRows = dtbMenu.AsEnumerable().Where(d => string.IsNullOrEmpty(d.Field<string>("ParentProgramID")))
                                                         .OrderBy(d => d.Field<Int16>("RowIndex"));
                }
                else
                    menuDataRows = dtbMenu.AsEnumerable().Where(d => string.IsNullOrEmpty(d.Field<string>("ParentProgramID")))
                                                     .OrderBy(d => d.Field<Int16>("RowIndex"));
                Session["fw_userprogram"] = dtbMenu;
                Session["fw_menuDataRows"] = menuDataRows;
                return dtbMenu;
            }

            menuDataRows = (EnumerableRowCollection<DataRow>)Session["fw_menuDataRows"];
            return (DataTable)obj;
        }

        protected override void OnError(EventArgs e)
        {
            var ex = Server.GetLastError();
            Logger.LogException(ex, Request.UserHostName, AppSession.UserLogin.UserName);

            //Page.Response.Redirect("~/ErrorPage.aspx");
            if (Page.IsCallback)
            {
                string script = string.Format("document.location.href = '{0}');", "~/ErrorPage.aspx");
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "redirect", script, true);
            }
            else
            {
                Response.Redirect("~/ErrorPage.aspx");
            }
        }

        #region Restore / Save value from Cookie

        protected void RestoreValueFromCookie()
        {
            RestoreValueFromCookie(Page);
        }

        protected void RestoreValueFromCookie(Control rooControl)
        {
            //Load Cookies
            var cookieName = string.Format("{0}_{1}_{2}", AppSession.UserLogin.UserID, ID, ProgramID);
            var cookie = Request.Cookies[cookieName];
            if (cookie != null) RestoreValueFromCookie(rooControl, cookie);
        }

        protected void SaveValueToCookie()
        {
            SaveValueToCookie(Page);
        }

        protected void SaveValueToCookie(Control rooControl)
        {
            //Load Cookies
            var cookieName = string.Format("{0}_{1}_{2}", AppSession.UserLogin.UserID, ID, ProgramID);

            var cookie = Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            cookie = new HttpCookie(cookieName);
            SaveValueToCookie(rooControl, cookie);
            Response.Cookies.Add(cookie);
        }

        protected void SaveValueToCookie(params Control[] exclude)
        {
            //Load Cookies
            var cookieName = string.Format("{0}_{1}_{2}", AppSession.UserLogin.UserID, ID, ProgramID);

            var cookie = Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            cookie = new HttpCookie(cookieName);
            SaveValueToCookie(Page, cookie, exclude);
            Response.Cookies.Add(cookie);
        }

        private void RestoreValueFromCookie(Control root, HttpCookie cookie)
        {
            foreach (Control ctl in root.Controls)
            {
                switch (ctl.GetType().Name)
                {
                    case "RadTimePicker":
                    case "RadDatePicker":
                        var dt = (RadDatePicker)ctl;
                        if (string.IsNullOrEmpty(cookie[ctl.ID]))
                            dt.Clear();
                        else
                            dt.SelectedDate = Convert.ToDateTime(cookie[ctl.ID]);
                        break;
                    case "RadTextBox":
                    case "RadNumericTextBox":
                        ((RadInputControl)ctl).Text = Convert.ToString(cookie[ctl.ID]);
                        break;
                    case "RadComboBox":
                        InitializeControlFromCookie(ctl, cookie[ctl.ID]);
                        var cbo = (RadComboBox)ctl;
                        ComboBox.SelectedValue(cbo, Convert.ToString(cookie[ctl.ID]));
                        break;
                    case "CheckBox":
                        ((CheckBox)ctl).Checked = Convert.ToBoolean(cookie[ctl.ID]);
                        break;
                    case "TextBox":
                        ((TextBox)ctl).Text = Convert.ToString(cookie[ctl.ID]);
                        break;
                    case "RadGrid":
                        var grd = (RadGrid)ctl;
                        if (grd.AllowPaging)
                        {
                            if (cookie[ctl.ID] != null)
                            {
                                var vals = cookie[ctl.ID].Split('|');
                                grd.PageSize = vals[0].ToInt();
                                grd.CurrentPageIndex = vals[1].ToInt();
                            }
                        }
                        break;
                    default:
                        if (ctl.HasControls())
                            RestoreValueFromCookie(ctl, cookie);
                        break;
                }
            }
        }

        protected string GetValueFromCookie(Control ctl)
        {
            //Load Cookies
            var cookieName = string.Format("{0}_{1}", AppSession.UserLogin.UserID, ID);
            var cookie = Request.Cookies[cookieName];
            return cookie != null ? cookie[ctl.ID] : string.Empty;
        }

        protected virtual void InitializeControlFromCookie(Control ctl, object value)
        {

        }

        private static void SaveValueToCookie(Control root, HttpCookie cookie)
        {
            foreach (Control ctl in root.Controls)
            {
                switch (ctl.GetType().Name)
                {
                    case "RadTimePicker":
                    case "RadDatePicker":
                        var dt = (RadDatePicker)ctl;
                        if (dt.IsEmpty)
                            cookie[ctl.ID] = string.Empty;
                        else
                            cookie[ctl.ID] = dt.SelectedDate.ToString();
                        break;
                    case "RadTextBox":
                    case "RadNumericTextBox":
                        if (!string.IsNullOrEmpty(((RadInputControl)ctl).Text)) cookie[ctl.ID] = ((RadInputControl)ctl).Text;
                        break;
                    case "RadComboBox":
                        if (!string.IsNullOrEmpty(((RadComboBox)ctl).SelectedValue)) cookie[ctl.ID] = ((RadComboBox)ctl).SelectedValue;
                        break;
                    case "CheckBox":
                        cookie[ctl.ID] = ((CheckBox)ctl).Checked.ToString();
                        break;
                    case "TextBox":
                        if (!string.IsNullOrEmpty(((TextBox)ctl).Text)) cookie[ctl.ID] = ((TextBox)ctl).Text;
                        break;
                    case "RadGrid":
                        var grd = (RadGrid)ctl;
                        if (grd.AllowPaging)
                        {
                            cookie[ctl.ID] = string.Format("{0}|{1}", grd.PageSize, grd.CurrentPageIndex);
                        }
                        break;
                    default:
                        if (ctl.HasControls())
                            SaveValueToCookie(ctl, cookie);
                        break;
                }
            }
        }

        private static void SaveValueToCookie(Control root, HttpCookie cookie, params Control[] exclude)
        {
            foreach (Control ctl in root.Controls)
            {
                var xx = exclude.SingleOrDefault(e => e.ID == ctl.ID);
                if (xx != null) continue;

                switch (ctl.GetType().Name)
                {
                    case "RadTimePicker":
                    case "RadDatePicker":
                        if (!((RadDatePicker)ctl).IsEmpty) cookie[ctl.ID] = ((RadDatePicker)ctl).SelectedDate.ToString();
                        break;
                    case "RadTextBox":
                    case "RadNumericTextBox":
                        if (!string.IsNullOrEmpty(((RadInputControl)ctl).Text)) cookie[ctl.ID] = ((RadInputControl)ctl).Text;
                        break;
                    case "RadComboBox":
                        if (!string.IsNullOrEmpty(((RadComboBox)ctl).SelectedValue)) cookie[ctl.ID] = ((RadComboBox)ctl).SelectedValue;
                        break;
                    case "CheckBox":
                        cookie[ctl.ID] = ((CheckBox)ctl).Checked.ToString();
                        break;
                    default:
                        if (ctl.HasControls())
                            SaveValueToCookie(ctl, cookie, exclude);
                        break;
                }
            }
        }

        # endregion

        #region LOG
        public void LogError(Exception ex)
        {
            try
            {
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Message: {0}", ex.Message);
                message += Environment.NewLine;
                message += string.Format("StackTrace: {0}", ex.StackTrace);
                message += Environment.NewLine;
                message += string.Format("Source: {0}", ex.Source);
                message += Environment.NewLine;
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;

                string path = Server.MapPath("~/ErrorLog/");
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                var dir = new System.IO.DirectoryInfo(@path);
                string fileName = "ErrorLog.txt";

                path = System.IO.Path.Combine(dir.FullName, fileName);

                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path);
                    System.IO.TextWriter tw = new System.IO.StreamWriter(path);
                    tw.WriteLine(message);
                    tw.Close();
                }
                else
                {
                    System.IO.TextWriter tw = new System.IO.StreamWriter(path, true);
                    tw.WriteLine(message);
                    tw.Close();
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        public string GetBasePath()
        {
            return string.Format("{0}://{1}{2}",
                    HttpContext.Current.Request.Url.Scheme,
                    HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
                    (HttpContext.Current.Request.ApplicationPath.Equals("/")) ? string.Empty : HttpContext.Current.Request.ApplicationPath
                    );
        }
    }
}