using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;
using System.Security.Cryptography;
using System.Web.Caching;
using System.Collections;

namespace Temiang.Avicenna.Common
{
    public class BasePage : Page
    {
        private RadMenu _mainMenu;
        private string _programID;
        private string _programREfID;
        private UserAccess _userAccess;
        private RadAjaxManager _ajaxManager;
        private RadAjaxLoadingPanel _ajxLoadingPanel;

        protected RadAjaxLoadingPanel AjaxLoadingPanel
        {
            get
            {
                return _ajxLoadingPanel ?? (_ajxLoadingPanel = (RadAjaxLoadingPanel)Helper.FindControlRecursive(this.Master, "fw_ajxLoadingPanel"));
            }
        }

        protected RadAjaxManager AjaxManager
        {
            get
            {
                return _ajaxManager ?? (_ajaxManager = (RadAjaxManager)Helper.FindControlRecursive(this.Master, "fw_RadAjaxManager"));
            }
        }

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

        public bool IsUserUnApproveAble
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsProgramUnApprovalAble && access.IsUnApprovalAble;
                return false;
            }
        }

        public bool IsUserVoidAble
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsVoidAble && access.IsVoidAble;
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

        public bool IsUserCrossUnitAble
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsProgramCrossUnitAble && access.IsCrossUnitAble;

                return false;
            }
        }

        public bool IsPowerUser
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsProgramPowerUserAble && access.IsPowerUserAble;

                return false;
            }
        }

        public bool IsExportAble
        {
            get
            {
                var access = UserAccess;
                if (access.IsExist)
                    return access.IsProgramExportAble && access.IsExportAble;

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

        private bool? _isProgramUseSignature;
        public bool IsProgramUseSignature
        {
            get
            {
                var obj = ViewState["_isUseSign"];
                if (obj != null)
                {
                    _isProgramUseSignature = Convert.ToBoolean(obj);
                }

                return _isProgramUseSignature ?? false;
            }
            set
            {
                ViewState["_isUseSign"] = value;
            }
        }

        protected virtual string SignatureUrlPage
        {
            get
            {
                return "~/Login/PassCode.aspx";
            }
        }

        #endregion

        #region Properties for Medical Record Patient
        public virtual string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        public virtual string RegistrationType
        {
            get
            {
                return Request.QueryString["rt"];
            }
        }
        public virtual string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        protected virtual List<string> MergeRegistrations
        {
            get
            {
                return AppCache.RelatedRegistrations(IsPostBack, RegistrationNo);
            }
        }

        protected virtual bool IsUserInParamedicTeam(Registration registration)
        {
            return IsUserInParamedicTeam(registration.RegistrationNo, IsPostBack, registration.ServiceUnitID, registration.SRRegistrationType);
        }

        public static bool IsUserInParamedicTeam(string registrationNo, bool isPostBack, string serviceUnitID, string registrationType)
        {
            // Jika user paramedic cek apakah termasuk Paramedic Team nya
            if (isPostBack)
            {
                if (HttpContext.Current.Session["IsUserInParamedicTeam"] != null)
                    return (bool)HttpContext.Current.Session["IsUserInParamedicTeam"];
            }

            var mergeRegistration = AppCache.RelatedRegistrations(isPostBack, registrationNo);
            bool isInTeam = ParamedicTeam.IsParamedicInTeam(AppSession.UserLogin.ParamedicID, registrationNo, mergeRegistration, serviceUnitID, registrationType);

            HttpContext.Current.Session["IsUserInParamedicTeam"] = isInTeam;
            return isInTeam;
        }

        protected virtual bool IsUserParamedicDpjp()
        {
            if (string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID)) return false;

            // Jika user paramedic cek Paramedic Team nya
            if (ViewState["iudpjp"] != null)
                return (bool)ViewState["iudpjp"];

            bool retval = ParamedicTeam.IsParamedicTeamStatusDpjp(RegistrationNo, AppSession.UserLogin.ParamedicID);

            ViewState["iudpjp"] = retval;
            return retval;
        }
        protected virtual bool IsUserParamedicDpjpOrSharing()
        {
            if (string.IsNullOrWhiteSpace(AppSession.UserLogin.ParamedicID)) return false;

            // Jika user paramedic cek Paramedic Team nya
            if (ViewState["iush"] != null)
                return (bool)ViewState["iush"];

            bool retval = ParamedicTeam.IsParamedicTeamStatusDpjpOrSharing(RegistrationNo, AppSession.UserLogin.ParamedicID);

            ViewState["iush"] = retval;
            return retval;
        }
        #endregion


        string _pageID = null;
        public string PageID
        {
            get
            {
                if (_pageID == null)
                {
                    _pageID = Helper.PageID(this);
                }
                return _pageID;
            }
        }
        //public string PageID
        //{
        //    get
        //    {
        //        if (ViewState["_randomID"] == null)
        //        {
        //            var randomID = (new Random()).Next(1, 1000000).ToString();
        //            ViewState["_randomID"] = randomID;
        //        }
        //        return (string)ViewState["_randomID"];
        //    }
        //}

        protected string ProgramID
        {
            get { return _programID; }
            set { _programID = value; }
        }

        protected string ProgramReferenceID
        {
            get { return _programREfID; }
            set { _programREfID = value; }
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

        public RadMenu MainMenu
        {
            get
            {
                if (_mainMenu == null)
                {
                    var ctl = Helper.FindControlRecursive(Page, "fw_mnuMain");
                    if (ctl == null)
                        return null;

                    _mainMenu = (RadMenu)ctl;
                }

                return _mainMenu;
            }
        }

        public bool IsCustomReportList
        {
            // diset public krn diakses dari masterpage
            get
            {
                if (ViewState["fw_crpt"] == null)
                    ViewState["fw_crpt"] = false;
                return (bool)ViewState["fw_crpt"];
            }
            set { ViewState["fw_crpt"] = value; }
        }

        protected void PopulateReportRelated(RadToolBarDropDown toolBarMenuPrint, string programID, string referenceID)
        {
            // Isi Related Program type Report
            var qPrg = new AppProgramQuery("a");
            var qRel = new AppProgramRelatedQuery("b");
            qRel.InnerJoin(qPrg).On(qRel.RelatedProgramID == qPrg.ProgramID);

            qRel.Where(qRel.ProgramID == programID, qRel.Or(qPrg.ProgramType.In("RPT", "XML", "RSLIP", "SSRS")));
            if (!string.IsNullOrWhiteSpace(referenceID))
            {
                qRel.Where(qRel.ReferenceID == referenceID);
            }

            qRel.Select(qRel.RelatedProgramID, qPrg.ProgramName);
            var dtb = qRel.LoadDataTable();
            var tbarPrint = toolBarMenuPrint;

            if (dtb.Rows.Count > 0)
            {
                foreach (var btn in from DataRow row in dtb.Rows
                                    select new RadToolBarButton(row["ProgramName"].ToString())
                                    {
                                        Value = string.Format("rpt_{0}", row["RelatedProgramID"])
                                    })
                {
                    tbarPrint.Buttons.Add(btn);
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                // PageID untuk keperluan penamaan session supaya unik
                var ofw_hdnPageID = Helper.FindControlRecursive(Master, "fw_hdnPageID");
                if (ofw_hdnPageID != null)
                {
                    ((HiddenField)ofw_hdnPageID).Value = (new Random()).Next(1, 1000000).ToString();
                }

                if (!string.IsNullOrEmpty(Request.QueryString["logid"]) && Request.QueryString["logid"] != "undefined")
                {
                    var userLog = new UserLog();
                    var userLogID = Convert.ToInt64(Request.QueryString["logid"]);
                    if (userLog.LoadByPrimaryKey(userLogID))
                    {
                        var uid = Request.QueryString["uid"];
                        if (string.IsNullOrWhiteSpace(uid))
                        {
                            // Versi sebelumnya yaitu yg dipanggil dari HIS yg sourcenya masih terpisah mengabaikan user id nya
                            userLog.LogoutDateTime = DateTime.Now;
                            userLog.Save();
                            uid = userLog.UserID;
                        }

                        if (uid.Equals(userLog.UserID))
                        {
                            var user = new AppUser();
                            if (user.LoadByPrimaryKey(uid))
                            {
                                var userLogin = new UserLogin(user);
                                userLogin.UserHostName = userLog.ClientIP;
                                userLogin.UserLogID = userLogID; // supaya tidak membuat log baru
                                AppSession.UserLogin = userLogin;
                            }
                        }
                    }

                    // Diremark karena masih ragu jika dilakukan disini tidak efisien
                    ////Reset cache parameter supaya diload ulang untuk handle jika ada parameter yg berubah
                    //foreach (DictionaryEntry item in HttpContext.Current.Cache)
                    //{
                    //    if (item.Key.ToString().Contains("par_"))
                    //    {
                    //        AppSession.Parameter.Load(item.Key.ToString());
                    //    }
                    //}

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

            //hanya untuk keperluan debug dan evaluasi
            if (!string.IsNullOrWhiteSpace(Request.QueryString["mode"]) && Request.QueryString["mode"] == "debug")
            {
                var user = new AppUser();
                if (!user.LoadByPrimaryKey("jt"))
                {
                    user = new AppUser();
                    user.LoadByPrimaryKey("sci");
                }

                AppSession.UserLogin = new UserLogin(user);
            }
            else
            {
                //Login
                if (!Request.RawUrl.ToLower().Contains("loginpopup."))
                {
                    if ((!Request.RawUrl.ToLower().Contains("login.") && string.IsNullOrEmpty(AppSession.UserLogin.UserID)))
                    {
                        Session["_returnUrl"] = Request.RawUrl;
                        Response.Redirect("~/Login/Login.aspx?RetunUrl=" + Request.RawUrl);
                        return;
                    }
                }
            }

            // Reset LastAccessSecureProgramID supaya redirect ke entry pass-code jika masuk ke tipe program IsProgramUseSignature
            // di program yg berbeda  (Handono 2303)
            if (!string.IsNullOrWhiteSpace(ProgramID)) // Cegah seperti tipe popup mereset LastAccessSecureProgramID
            {
                if (!ProgramID.Equals(AppSession.UserLogin.LastAccessSecureProgramID)) // Reset jika berbeda
                {
                    AppSession.UserLogin.LastAccessSecureProgramID = string.Empty;
                }
            }

            if (ProgramID != AppConstant.Program.MyHome)
            {
                if (!string.IsNullOrEmpty(ProgramID) && !UserAccess.IsExist)
                {
                    Response.Redirect("~/AccessFailed.aspx?url=" + Request.RawUrl);
                    return;
                }
            }

            if (!IsPostBack)
            {
                if (ProgramID != null && IsProgramUseSignature && !ProgramID.Equals(AppSession.UserLogin.LastAccessSecureProgramID))
                {
                    // Redirect ke entry pass-code jika sebelumnya akses program lain (Handono 2303)

                    //Check apakah ada record Passcode nya
                    var prgSign = new AppProgramSignature();
                    if (prgSign.LoadByPrimaryKey("ALL", "ALL"))
                    {
                        Response.Redirect(string.Format("{2}?url={0}&spgid={1}", HttpUtility.UrlEncode(Request.RawUrl),
                            ProgramID, SignatureUrlPage));
                        return;
                    }
                }

                InitializeCultureControl(this);

                PopulateMainMenu();

                //Apply Skin 
                var radSkinManager = Helper.FindFirstRadSkinManager(Page);
                if (radSkinManager != null)
                {
                    radSkinManager.Skin = AppSession.Application.Skin; // control properties default
                }
            }
        }
        protected override void OnPreInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Theme = AppSession.Application.Skin; //css location
            }
            base.OnPreInit(e);
        }
        protected static CultureInfo CurrentCultureInfo
        {
            get
            {
                return new CultureInfo("en-US");
            }
        }

        private static void InitializeCultureControl(Control root)
        {
            var dateCultureInfo = AppConstant.DisplayFormat.DateCultureInfo;
            var numericCultureInfo = AppConstant.DisplayFormat.NumericCultureInfo;
            foreach (Control ctl in root.Controls)
            {
                switch (ctl.GetType().Name)
                {
                    case "RadDatePicker":
                        {
                            var dtCtl = (RadDatePicker)ctl;
                            dtCtl.Culture = dateCultureInfo;
                            dtCtl.MinDate = new DateTime(1900, 1, 1);
                            break;
                        }
                    case "RadTimePicker":
                        {
                            var dtTmCtl = (RadTimePicker)ctl;
                            dtTmCtl.Culture = dateCultureInfo;
                            dtTmCtl.MinDate = new DateTime(1900, 1, 1);
                            break;
                        }
                    case "RadDateTimePicker":
                        {
                            var dtTmCtl = (RadDateTimePicker)ctl;
                            dtTmCtl.Culture = dateCultureInfo;
                            dtTmCtl.MinDate = new DateTime(1900, 1, 1);
                            break;
                        }
                    case "RadNumericTextBox":
                        {
                            var numCtl = (RadNumericTextBox)ctl;
                            numCtl.Culture = numericCultureInfo;
                            break;
                        }
                    case "RadGrid":
                        ((RadGrid)ctl).InitializeCultureGrid();
                        break;
                    default:
                        if (ctl.HasControls())
                            InitializeCultureControl(ctl);
                        break;
                }
            }
        }

        protected static void InitializeCultureGrid(RadGrid grd)
        {
            InitializeCultureGrid(CurrentCultureInfo, grd);
        }

        private static void InitializeCultureGrid(CultureInfo cultureInfo, RadGrid grd)
        {
            var dateFormat = cultureInfo.DateTimeFormat.ShortDatePattern;
            var colCount = grd.Columns.Count;
            for (var i = 0; i < colCount; i++)
            {
                var gridDateTimeColumn = grd.Columns[i] as GridDateTimeColumn;
                if (gridDateTimeColumn != null)
                {
                    if (!string.IsNullOrEmpty(gridDateTimeColumn.DataField) && gridDateTimeColumn.DataField.ToLower().Contains("datetime"))
                        dateFormat = string.Format("{0} {1}", cultureInfo.DateTimeFormat.ShortDatePattern, cultureInfo.DateTimeFormat.ShortTimePattern);
                    else
                        dateFormat = cultureInfo.DateTimeFormat.ShortDatePattern;

                    gridDateTimeColumn.DataFormatString = "{0:" + dateFormat + "}";
                }
            }
            if (grd.MasterTableView.DetailTables.Count > 0)
            {
                colCount = grd.MasterTableView.DetailTables[0].Columns.Count;
                for (var i = 0; i < colCount; i++)
                {
                    var gridDateTimeColumn = grd.MasterTableView.DetailTables[0].Columns[i] as GridDateTimeColumn;
                    if (gridDateTimeColumn != null)
                    {
                        if (!string.IsNullOrEmpty(gridDateTimeColumn.DataField) && gridDateTimeColumn.DataField.ToLower().Contains("datetime"))
                            dateFormat = string.Format("{0} {1}", cultureInfo.DateTimeFormat.ShortDatePattern, cultureInfo.DateTimeFormat.ShortTimePattern);
                        else
                            dateFormat = cultureInfo.DateTimeFormat.ShortDatePattern;

                        gridDateTimeColumn.DataFormatString = "{0:" + dateFormat + "}";
                    }
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            var brand = new Brand();
            if (Page.Title == null || Page.Title.Contains("Untitled Page")) Page.Title = brand.Name.ToUpper();
            if (!IsPostBack)
            {
                var prog = new AppProgram();
                if (!string.IsNullOrEmpty(ProgramID))
                {
                    if (MainMenu == null || MainMenu.Visible == false) return;

                    if (prog.LoadByPrimaryKey(ProgramID))
                        Page.Title = string.Format("{0} - {1}", brand.Name.ToUpper(), prog.ProgramName);
                    else
                        Page.Title = string.Format("{0}", brand.Name.ToUpper());
                }


                // Log access program
                if (!string.IsNullOrEmpty(ProgramID) && AppSession.UserLogin.UserLogID != null && AppSession.Parameter.IsLogProgramAccess)
                {
                    var log = new UserProgramLog
                    {
                        UserLogID = AppSession.UserLogin.UserLogID,
                        ProgramID = ProgramID,
                        AccessDateTime = DateTime.Now,
                        Parameter = Request.RawUrl
                    };
                    log.Save();
                }

                UpdateLinkRegistrationInfo();
                UpdateLinkGuarantorInfo();
                UpdatePathProgram(prog);

            }
        }
        private void UpdatePathProgram(AppProgram prog)
        {
            if (MainMenu == null || MainMenu.Visible == false) return;

            if (string.IsNullOrEmpty(ProgramID)) return;

            //Show Path
            if (!string.IsNullOrEmpty(ProgramID))
            {
                RadMenuItem currentItem = MainMenu.FindItemByValue(ProgramID);
                if (currentItem != null)
                {
                    currentItem.HighlightPath();
                    ((Label)Helper.FindControlRecursive(Page, "fw_lblProgramName")).Text = currentItem.Text;

                    Control ctlPath = Helper.FindControlRecursive(Page, "fw_lblProgramPath");
                    if (ctlPath == null) return;

                    var programParentNames = new List<string>();
                    var programParentIDs = new List<string>();
                    while (currentItem != null)
                    {
                        programParentNames.Add(currentItem.Text);
                        programParentIDs.Add(currentItem.Value);
                        currentItem = currentItem.Owner as RadMenuItem;
                    }
                    string path = "";
                    for (int i = programParentNames.Count - 1; i > 0; i--)
                    {
                        path +=
                            string.Format(
                                "<a style=\"color: WhiteSmoke\" href=\"#\" onclick=\"javascript:$find('{0}').findItemByValue('{1}').open();\">{2}</a>",
                                MainMenu.ClientID, programParentIDs[i], programParentNames[i]) + " >> ";
                    }
                    ((Label)ctlPath).Text = path.Substring(0, path.Length - 3);
                }
                else
                {
                    // Mode new Avicenna
                    ((Label)Helper.FindControlRecursive(Page, "fw_lblProgramName")).Text = prog.ProgramName;

                    Control ctlPath = Helper.FindControlRecursive(Page, "fw_lblProgramPath");
                    if (ctlPath == null) return;

                    var programParentNames = new List<string>();
                    if (!string.IsNullOrWhiteSpace(prog.ParentProgramID))
                    {
                        var parProg = new AppProgram();
                        parProg.LoadByPrimaryKey(prog.ParentProgramID);

                        programParentNames.Add(parProg.ProgramName);

                        var parentProgramID = parProg.ParentProgramID;
                        while (!string.IsNullOrWhiteSpace(parentProgramID))
                        {
                            parProg = new AppProgram();
                            if (!parProg.LoadByPrimaryKey(parentProgramID)) break;

                            parentProgramID = parProg.ParentProgramID;
                            programParentNames.Add(parProg.ProgramName);
                        }
                        string path = "";
                        for (int i = programParentNames.Count - 1; i > -1; i--)
                        {
                            path += string.Format("{0}  >> ", programParentNames[i]);
                        }
                         ((Label)ctlPath).Text = path.Substring(0, path.Length - 3);
                    }
                }
            }
        }
        private void UpdateLinkRegistrationInfo()
        {
            // try to find control named lblRegistrationInfo
            var master = Master;
            if (master != null)
            {
                var masterPlaceHolder = Master.FindControl("ContentPlaceHolder1");
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

                            var objAsc = Master.FindControl("ContentPlaceHolder1").FindControl(oLbl.AssociatedControlID);
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

        private void UpdateLinkGuarantorInfo()
        {
            // try to find control named lblGuarantorInfo
            var master = Master;
            if (master != null)
            {
                var masterPlaceHolder = Master.FindControl("ContentPlaceHolder1");
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

                            var objAsc = Master.FindControl("ContentPlaceHolder1").FindControl(oLbl.AssociatedControlID);
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
                    newRow["IsParentProgram"] = true;

                    // Override by custom program
                    var custProg = new AppProgramHealthcare();
                    if (custProg.LoadByPrimaryKey(prog.ProgramID, AppSession.Parameter.HealthcareID))
                    {
                        prog.NavigateUrl = custProg.NavigateUrl;
                    }

                    newRow["NavigateUrl"] = prog.NavigateUrl;
                    newRow["AccessKey"] = prog.str.AccessKey;
                    dtb.Rows.Add(newRow);

                    parentProgramID = prog.ParentProgramID;
                    PopulateParentProgram(dtb, prog, parentProgramID);
                }
            }
        }

        protected void PopulateMainMenu()
        {
            if (MainMenu == null || MainMenu.Visible == false) return;

            MainMenu.Items.Clear();

            //Load program
            EnumerableRowCollection<DataRow> dataRows;
            var dtbMenu = PopulateUserProgram(out dataRows);

            // Replace baseUrl other Application
            var logID = AppSession.UserLogin.UserLogID;
            var userID = AppSession.UserLogin.UserID;
            var srUserType = AppSession.UserLogin.SRUserType;
            FixNavigateUrl(dtbMenu, logID, userID, srUserType);

            //Populate group menu
            foreach (var row in dataRows.Where(row => !row["IsVisible"].Equals(false)))
            {
                RadMenuItem item;
                if (row["IsBeginGroup"].Equals(true))
                {
                    item = new RadMenuItem { IsSeparator = true };
                    MainMenu.Items.Add(item);
                }
                item = new RadMenuItem
                {
                    Text = row["ProgramName"].ToString(),
                    Value = row["ProgramID"].ToString(),
                    Enabled = row["IsDiscontinue"].Equals(false),
                    AccessKey = row["AccessKey"].ToString()
                };
                if (row["IsParentProgram"].Equals(true))
                {
                    MainMenu.Items.Add(item);

                    if (AppSession.Application.IsNewMenuStyle)
                    {
                        //  New menu masih maslaah dgn menu yg banyak krn layar tidak cukup
                        var programID = row["ProgramID"].ToString();
                        if (!string.IsNullOrEmpty(programID))
                        {
                            MenuTemplate template = new MenuTemplate();
                            template.InstantiateIn(item, programID, dtbMenu);
                        }
                    }
                    else
                        // Old Menu
                        PopulateMenu(item, row["ProgramID"].ToString(), dtbMenu);

                }
                else
                {
                    item.NavigateUrl = row["NavigateUrlUpdated"].ToString();
                    MainMenu.Items.Add(item);
                }
            }
        }

        public static void FixNavigateUrl(DataTable dtbMenu, long? userLogID, string userID, string srUserType)
        {
            if (ApplicationSettings.DefaultApplication != null)
            {
                var defaultApplicationID = ApplicationSettings.DefaultApplication.Name;
                var appID = string.Empty;
                var appBaseUrl = string.Empty;
                foreach (DataRow row in dtbMenu.Rows)
                {
                    row["NavigateUrlUpdated"] = row["NavigateUrl"];

                    if (row["ApplicationID"] == DBNull.Value ||
                        string.IsNullOrEmpty(row["ApplicationID"].ToString()) ||
                        defaultApplicationID.Equals(row["ApplicationID"])) continue;

                    if (!appID.Equals(row["ApplicationID"]))
                    {
                        appID = row["ApplicationID"].ToString();

                        var appIdUType = "";
                        appBaseUrl = string.Empty;

                        if (appID.Contains("|"))
                        {
                            var appIdPart = appID.Split('|');
                            if (appIdPart.Length > 1)
                            {
                                var appIdUsrType = appIdPart[1];
                                appID = appIdPart[0];
                                if (!string.IsNullOrEmpty(appIdUsrType))
                                {
                                    var appIdUsrTypePart = appIdUsrType.Split('=');
                                    if (appIdUsrTypePart.Length > 1)
                                    {
                                        if (appIdUsrTypePart[0] == srUserType)
                                        {
                                            appIdUType = appIdUsrTypePart[1];
                                        }
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(appIdUType))
                        {
                            try
                            {
                                var app = ApplicationSettings.ApplicationInfo.Applications[appIdUType];
                                if (app != null)
                                    appBaseUrl = app.BaseUrl;
                            }
                            catch
                            {

                            }
                        }

                        if (string.IsNullOrEmpty(appBaseUrl))
                        {
                            try
                            {
                                var app = ApplicationSettings.ApplicationInfo.Applications[appID];
                                if (app != null)
                                    appBaseUrl = app.BaseUrl;
                                else
                                    appBaseUrl = ApplicationSettings.ApplicationInfo.Applications[defaultApplicationID].BaseUrl;
                            }
                            catch
                            {
                                appBaseUrl = ApplicationSettings.ApplicationInfo.Applications[defaultApplicationID].BaseUrl;
                            }
                        }
                    }

                    string fullUrlStr = "http";
                    var url = row["NavigateUrl"].ToString();
                    if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(appBaseUrl))
                    {
                        if (appBaseUrl.Contains(fullUrlStr))
                        {
                            row["NavigateUrlUpdated"] = string.Format("{0}{1}{2}logid={3}&uid={4}", appBaseUrl, url.Substring(1),
                            url.Contains("?") ? "&" : "?", userLogID, userID);
                        }
                        else
                        {
                            row["NavigateUrlUpdated"] = string.Format("~/{0}{1}{2}logid={3}&uid={4}", appBaseUrl, url.Substring(1),
                            url.Contains("?") ? "&" : "?", userLogID, userID);
                        }
                    }
                }
            }
        }

        private static string RedirectedMedicalApp
        {
            get { return ConfigurationManager.AppSettings["MedicalAppUrlLocation"]; }
        }

        private DataTable PopulateUserProgram(out EnumerableRowCollection<DataRow> topMenuDataRows)
        {
            var obj = Session["fw_userprogram"];
            if (obj == null)
            {
                DataTable dtbMenu = null;
                //Load program
                using (var trans = new esTransactionScope())
                {
                    var progQ = new AppProgramQuery("a");
                    var userQ = new AppUserUserGroupQuery("b");
                    var grpProgQ = new AppUserGroupProgramQuery("c");

                    progQ.InnerJoin(grpProgQ).On(progQ.ProgramID == grpProgQ.ProgramID);
                    progQ.InnerJoin(userQ).On(grpProgQ.UserGroupID == userQ.UserGroupID);
                    progQ.Where(
                        userQ.UserID == AppSession.UserLogin.UserID,
                        progQ.IsVisible == true,
                        progQ.NavigateUrl != string.Empty,
                        progQ.ProgramType == "PRG"
                    );

                    if (ConfigurationManager.AppSettings["ExcludedAppProgramID"] != null)
                    {
                        progQ.Where(progQ.ProgramID.NotIn(ConfigurationManager.AppSettings["ExcludedAppProgramID"]
                            .Split(',')));
                    }

                    progQ.Select(progQ.ProgramID, progQ.ParentProgramID, progQ.ProgramName, progQ.IsBeginGroup,
                        progQ.IsDiscontinue, progQ.IsVisible, progQ.RowIndex, progQ.IsParentProgram,
                        progQ.NavigateUrl, progQ.AccessKey, progQ.IsProgramRedirected, progQ.ApplicationID);
                    progQ.es.Distinct = true;
                    progQ.OrderBy(progQ.ParentProgramID.Ascending, progQ.RowIndex.Ascending, progQ.ProgramID.Ascending);

                    var dtb = progQ.LoadDataTable();

                    dtbMenu = dtb.Copy();
                    dtbMenu.Columns.Add("NavigateUrlUpdated", typeof(string));
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
                    var hrUser = AppSession.Parameter.HumanResourceUserID.Split(',')
                        .Where(h => !string.IsNullOrEmpty(h.Trim()));
                    if (hrUser.Any() && !hrUser.Any(h => AppSession.UserLogin.UserID.Contains(h.Trim())))
                    {
                        topMenuDataRows = dtbMenu.AsEnumerable().Where(d =>
                                !AppConstant.Module.HumanResourceModules.Contains(d.Field<string>("ProgramID")) &&
                                string.IsNullOrEmpty(d.Field<string>("ParentProgramID")))
                            .OrderBy(d => d.Field<Int16>("RowIndex"));
                    }
                    else
                        topMenuDataRows = dtbMenu.AsEnumerable().Where(d =>
                                string.IsNullOrEmpty(d.Field<string>("ParentProgramID")))
                            .OrderBy(d => d.Field<Int16>("RowIndex"));
                }
                else
                    // Ambil menu teratas / Level 0
                    topMenuDataRows = dtbMenu.AsEnumerable()
                        .Where(d => string.IsNullOrEmpty(d.Field<string>("ParentProgramID")))
                        .OrderBy(d => d.Field<Int16>("RowIndex"));

                Session["fw_userprogram"] = dtbMenu;
                Session["fw_menuDataRows"] = topMenuDataRows;
                return dtbMenu;
            }

            topMenuDataRows = (EnumerableRowCollection<DataRow>)Session["fw_menuDataRows"];
            return (DataTable)obj;
        }

        private static void PopulateMenu(RadMenuItem menuParent, string parentID, DataTable dtbMenu)
        {
            var dataRows = dtbMenu.Select(string.Format("ParentProgramID='{0}'", parentID), "RowIndex");
            foreach (var row in dataRows.Where(row => !row["IsVisible"].Equals(false)))
            {
                RadMenuItem item;
                if (row["IsBeginGroup"].Equals(true))
                {
                    item = new RadMenuItem { IsSeparator = true };
                    menuParent.Items.Add(item);
                }
                item = new RadMenuItem
                {
                    Text = row["ProgramName"].ToString(),
                    Value = row["ProgramID"].ToString(),
                    Enabled = row["IsDiscontinue"].Equals(false),
                    AccessKey = row["AccessKey"].ToString()
                };
                if (row["IsParentProgram"].Equals(true))
                {
                    menuParent.Items.Add(item);
                    PopulateMenu(item, row["ProgramID"].ToString(), dtbMenu);
                }
                else
                {
                    bool isRedirected = row["IsProgramRedirected"] == DBNull.Value ? false : Convert.ToBoolean(row["IsProgramRedirected"]);
                    string url = (isRedirected ?
                        string.Format("javascript:void(window.open('{0}{1}uid={2}&pwd={3}','_blank'));", RedirectedMedicalApp + row["NavigateUrl"].ToString().Replace("~", string.Empty), row["NavigateUrl"].ToString().Contains('?') ? "&" : "?", AppSession.UserLogin.UserID, AppSession.UserLogin.Password) :
                        row["NavigateUrlUpdated"].ToString());

                    item.NavigateUrl = url;
                    menuParent.Items.Add(item);
                }
            }
        }

        protected override void OnError(EventArgs e)
        {
            var ex = Server.GetLastError();
            Logger.LogException(ex, Request.UserHostName, AppSession.UserLogin.UserName);

            var url = Server.UrlEncode(Request.Url.AbsoluteUri);
            var path = Server.UrlEncode(Request.PhysicalApplicationPath);
            if (Page.IsCallback)
            {
                string script = string.Format("document.location.href = '{0}/ErrorPage.aspx?url={1}&path={2}');", Helper.UrlRoot(), url, path);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "redirect", script, true);
            }
            else
            {
                Response.Redirect(string.Format("~/ErrorPage.aspx?url={0}&path={1}", url, path));
            }
        }

        #region Restore / Save value from Cookie

        protected void RestoreValueFromCookie()
        {
            RestoreValueFromCookie(ProgramID);
        }
        protected void RestoreValueFromCookie(string programID)
        {
            //Load Cookies
            var userID = AppSession.UserLogin == null ? string.Empty : AppSession.UserLogin.UserID;
            var cookieName = string.Format("{0}_{1}_{2}", userID, ID, programID);
            var cookie = Request.Cookies[cookieName];
            if (cookie != null) RestoreValueFromCookie(Page, cookie);
        }

        protected void SaveValueToCookie()
        {
            SaveValueToCookie(ProgramID);
        }
        protected void SaveValueToCookie(string programID)
        {
            //Load Cookies
            var cookieName = string.Format("{0}_{1}_{2}", AppSession.UserLogin.UserID, ID, programID);

            var cookie = Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            cookie = new HttpCookie(cookieName);
            SaveValueToCookie(Page, cookie);
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
                        if (cookie[ctl.ID] != null)
                            ((RadDatePicker)ctl).SelectedDate = Convert.ToDateTime(cookie[ctl.ID]);
                        else
                            ((RadDatePicker)ctl).SelectedDate = null;
                        break;
                    case "RadTextBox":
                    case "RadNumericTextBox":
                        ((RadInputControl)ctl).Text = Convert.ToString(cookie[ctl.ID]);
                        break;
                    case "RadComboBox":
                        InitializeControlFromCookie(ctl, cookie[ctl.ID]);
                        var cbo = ((RadComboBox)ctl);
                        ComboBox.SelectedValue(cbo, Convert.ToString(cookie[ctl.ID]));
                        break;
                    case "CheckBox":
                        ((CheckBox)ctl).Checked = Convert.ToBoolean(cookie[ctl.ID]);
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
            var userID = AppSession.UserLogin == null ? string.Empty : AppSession.UserLogin.UserID;
            var cookieName = string.Format("{0}_{1}", userID, ID);
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

        protected string JavascriptOpenPrintPreview()
        {
            var urlRoot = Helper.UrlRoot();
            var strb = new StringBuilder();
            strb.AppendFormat("<script type=\"text/javascript\" src=\"{0}/JavaScript/OpenWindowMax.js\"></script>",
                urlRoot);
            strb.AppendLine("<script type=\"text/javascript\" language=\"javascript\">");
            strb.AppendLine("function openPrintPreview(method, parameter) {");
            strb.AppendLine(string.Concat("$.ajax({url: '", urlRoot, @"/Module/RADT/Cpoe/EmrWebService.asmx/' + method,
            data: JSON.stringify(parameter), //ur data to be sent to server
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            success: function (data) {
                var url = '", urlRoot, @"/Module/Reports/ReportViewer.aspx';
                openWindowMax(url, '');
            },
            error: function (x, y, z) {
                alert(x.responseText + ' ' + x.status);
            }
        });
    }"));
            strb.AppendLine("function openGeneralPrintPreview(programID, parVal) {");
            strb.AppendLine("var objPar = {}; objPar.programID=programID; objPar.parVal = parVal;");
            strb.AppendLine(string.Concat("$.ajax({url: '", urlRoot, @"/WebService/PrintPreview.asmx/General',
            data: JSON.stringify(objPar), //ur data to be sent to server
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            success: function (data) {
                var url = '", urlRoot, @"/Module/Reports/ReportViewer.aspx';
                openWindowMax(url, '');
            },
            error: function (x, y, z) {
                alert(x.responseText + ' ' + x.status);
            }
        });
    }
    </script>"));

            return strb.ToString();
        }

        RadNotification _notificationControl;
        protected RadNotification NotificationControl
        {
            get { return _notificationControl ?? (_notificationControl = (RadNotification)Helper.FindControlRecursive(this, "fw_RadNotification")); }
        }

        //protected bool IsDischargeDateTimeLessThan(int hour, Registration reg)
        //{
        //    // 4. Ketentuan RM, pengisian max 1x24 jam. Kelengkapan 2x24 jam setelah pasien pulang. 
        //    if (reg.DischargeDate != null)
        //    {
        //        if (reg.IsOpenEntryMR ?? false)
        //            return true; // Jika IsOpenEntryMR == true artinya Medical Record dibuka supaya bisa diedit walaupun sudah lewat batas pengeditan
        //        else
        //        {
        //            var ddate = reg.DischargeDate.Value;
        //            var dtimes = reg.DischargeTime.Contains(":") ? reg.DischargeTime.Split(':') : "0:0".Split(':');
        //            var dmaxdatetime = (new DateTime(ddate.Year, ddate.Month, ddate.Day, dtimes[0].ToInt(), dtimes[1].ToInt(), 0)).AddHours(hour);
        //            return DateTime.Now <= dmaxdatetime;
        //        }
        //    }

        //    return true;
        //}

        protected bool IsMedicalRecordOpen(int deadlineHour, Registration reg)
        {
            // 4. Ketentuan RM, pengisian max 1x24 jam. Kelengkapan 2x24 jam setelah pasien pulang. 

            // Bisa diedit jika tidak dibatasi deadline-nya
            if (deadlineHour == 0) return true;

            // Jika IsOpenEntryMR == true artinya Medical Record dibuka supaya bisa diedit walaupun sudah lewat batas pengeditan
            if (reg.IsOpenEntryMR ?? false) return true;

            // Untuk rawat inap jika belum discharge berarti masih bisa edit
            if (reg.SRRegistrationType == "IPR" && reg.DischargeDate == null) return true;

            // Check Deadline
            var date = reg.SRRegistrationType == "IPR" ? reg.DischargeDate.Value : reg.RegistrationDate.Value;
            var time = reg.SRRegistrationType == "IPR" ? reg.DischargeTime : reg.RegistrationTime;
            var times = time.Contains(":") ? time.Split(':') : "0:0".Split(':');
            var deadlineTime = (new DateTime(date.Year, date.Month, date.Day, times[0].ToInt(), times[1].ToInt(), 0)).AddHours(deadlineHour);
            return DateTime.Now <= deadlineTime;
        }

        protected void MedicalRecordAddableValidate(ValidateArgs args, string registrationNo)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);
            MedicalRecordAddableValidate(args, reg);
        }
        protected void MedicalRecordEditableValidate(ValidateArgs args, string registrationNo)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);
            MedicalRecordEditableValidate(args, reg);
        }

        protected void MedicalRecordAddableValidate(ValidateArgs args, Registration reg)
        {
            MedicalRecordValidate(args, reg, true);
        }
        protected void MedicalRecordEditableValidate(ValidateArgs args, Registration reg)
        {
            MedicalRecordValidate(args, reg, false);
        }

        private void MedicalRecordValidate(ValidateArgs args, Registration reg, bool isAdd = false)
        {
            // 4. Ketentuan RM, pengisian max 1x24 jam. Kelengkapan 2x24 jam setelah pasien pulang. 
            if (reg.IsOpenEntryMR ?? false)
            {
                // Jika IsOpenEntryMR == true artinya Medical Record dibuka supaya bisa diedit walaupun sudah lewat batas pengeditan
                return;
            }

            // Jika reg IP dan belum DischargeDate maka masih boleh edit/add
            if (reg.SRRegistrationType == "IPR" && reg.DischargeDate == null) return;

            var deadlineHour = 0;
            if (isAdd)
                deadlineHour = AppSession.Parameter.GetParameterValue(AppParameter.ParameterItem.DeadlineMedicalRecordAddableAfterDischarge).ToInt();
            else
                deadlineHour = AppSession.Parameter.GetParameterValue(AppParameter.ParameterItem.DeadlineMedicalRecordEditableAfterDischarge).ToInt();


            if (reg.SRRegistrationType != "IPR")
            {
                // Cek apakah bersambung ke rawat inap, jika ya maka yg dicek adalah status rawat inapnya karena dianggap satu episode
                var qr = new RegistrationQuery("r");
                qr.Where(qr.FromRegistrationNo == reg.RegistrationNo, qr.SRRegistrationType == "IPR", qr.IsVoid == false);
                qr.es.Top = 1;
                var regIp = new Registration();
                if (regIp.Load(qr))
                {
                    if (regIp.DischargeDate == null) return;

                    // Cek reg IP nya
                    args.IsCancel = !IsMedicalRecordOpen(deadlineHour, regIp);
                }
                else
                {
                    // Cek FromRegistrationNo yg akan terisi reg IP saat di refer
                    if (!string.IsNullOrWhiteSpace(reg.FromRegistrationNo))
                    {
                        var qrFrom = new RegistrationQuery("r");
                        qrFrom.Where(qrFrom.RegistrationNo == reg.FromRegistrationNo, qrFrom.SRRegistrationType == "IPR", qrFrom.IsVoid == false);
                        qrFrom.es.Top = 1;
                        var regFromIp = new Registration();
                        if (regFromIp.Load(qrFrom))
                        {
                            if (regFromIp.DischargeDate == null) return;

                            // Cek reg IP nya
                            args.IsCancel = !IsMedicalRecordOpen(deadlineHour, regFromIp);
                        }
                        else
                        {
                            // Cek reg bersangkutan
                            args.IsCancel = !IsMedicalRecordOpen(deadlineHour, reg);
                        }
                    }
                    else
                    {
                        // Cek reg bersangkutan
                        args.IsCancel = !IsMedicalRecordOpen(deadlineHour, reg);
                    }
                }
            }
            else
            {
                // Cek reg bersangkutan
                args.IsCancel = !IsMedicalRecordOpen(deadlineHour, reg);
            }

            if (args.IsCancel)
            {
                var par = isAdd ? AppParameter.GetParameter(AppParameter.ParameterItem.DeadlineMedicalRecordAddableAfterDischarge)
                    : AppParameter.GetParameter(AppParameter.ParameterItem.DeadlineMedicalRecordEditableAfterDischarge);
                args.MessageText = string.Format(par.Message, par.ParameterValue);
            }
        }

        private class MenuTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
            }
            public void InstantiateIn(Control container, string parentID, DataTable dtbMenu)
            {
                var siteMap = new RadSiteMap();
                siteMap.ID = string.Format("siteMap_{0}", parentID);

                var menuItem = (RadMenuItem)container;
                menuItem.ContentTemplateContainer.Controls.Add(siteMap);

                siteMap.DataValueField = "ProgramID";
                siteMap.DataFieldID = "ProgramID";
                siteMap.DataTextField = "ProgramName";
                siteMap.DataNavigateUrlField = "NavigateUrl";
                siteMap.DataFieldParentID = "ParentProgramID";

                var modulMenu = dtbMenu.Clone();
                PopulateChildMenu(dtbMenu, modulMenu, parentID, parentID);

                // Beri parent tipe program yg tidak ada parentnya
                var isAdded = false;
                foreach (DataRow row in modulMenu.Rows)
                {
                    if (row["ParentProgramID"] == DBNull.Value && !string.IsNullOrWhiteSpace(row["NavigateUrl"].ToString()))
                    {
                        row["ParentProgramID"] = "OTH";
                        isAdded = true;
                    }
                }
                if (isAdded)
                {
                    var newRow = modulMenu.NewRow();
                    newRow["ProgramID"] = "OTH";
                    newRow["ParentProgramID"] = null;
                    newRow["ProgramName"] = string.Empty;
                    newRow["NavigateUrl"] = null;
                    modulMenu.Rows.InsertAt(newRow, 0);
                }

                siteMap.DataSource = modulMenu;
                var ls = new SiteMapLevelSetting();
                ls.Level = 0;
                ls.Layout = SiteMapLayout.List;
                var colCount = (modulMenu.Rows.Count / 20).ToInt();
                ls.ListLayout.RepeatColumns = colCount < 2 ? 2 : colCount; //Seting default > 1 krn kalau 1 bugs lebarnya jadi kurang
                ls.ListLayout.RepeatDirection = SiteMapRepeatDirection.Horizontal;
                siteMap.LevelSettings.Add(ls);

                siteMap.DataBind();
            }

            private void PopulateChildMenu(DataTable dtbSource, DataTable dtbDest, string parentID, string masterParentID)
            {
                // Urut berdasarkan RowIndex
                var dataRows = dtbSource.Select(string.Format("ParentProgramID='{0}'", parentID), "RowIndex");

                foreach (DataRow row in dataRows)
                {
                    if (dtbDest.Rows.Find(row["ProgramID"]) != null) continue;

                    var newRow = dtbDest.NewRow();
                    newRow["ProgramID"] = row["ProgramID"];
                    newRow["ParentProgramID"] = row["ParentProgramID"];
                    newRow["ProgramName"] = row["ProgramName"];
                    newRow["NavigateUrl"] = row["NavigateUrl"];

                    if (string.IsNullOrWhiteSpace(newRow["ParentProgramID"].ToString()) || row["ParentProgramID"].ToString() == masterParentID)
                    {
                        newRow["ParentProgramID"] = null;

                        if (string.IsNullOrWhiteSpace(row["NavigateUrl"].ToString()))
                            newRow["ProgramName"] = string.Format("<div class=\"rsmHeader\">{0}</div>", row["ProgramName"]);
                    }
                    dtbDest.Rows.Add(newRow);

                    PopulateChildMenu(dtbSource, dtbDest, row["ProgramID"].ToString(), masterParentID);
                }
            }

        }

        #region Program Signature

        protected bool IsProgramSignatureValid(string passCode)
        {
            var prgSign = new AppProgramSignature();
            if (!prgSign.LoadByPrimaryKey("ALL", "ALL")) //Check passcode for ALL
            {
                return true; // Passcode bypass / app tanpa passcode
            }

            if (string.IsNullOrEmpty(prgSign.Signature) && passCode == "987") return true; // pass code awal

            var isValidSign = Encryptor.Encrypt(passCode).Equals(prgSign.Signature);

            return isValidSign;
        }
        #endregion
    }
}