using System;
using System.Data;
using System.Net;
using System.Threading;
using System.Web;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.Security;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject.Common;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace Temiang.Avicenna
{
    public partial class Login : BasePage
    {
        private UserLogin _userLogin = new UserLogin();
        protected Brand _brand = new Brand();
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = _brand.Name.ToUpper();

            // Jika user tekan ENTER 
            this.Form.DefaultButton = btnLogin.UniqueID;

            //dibalikin seperti semula
            //settingan ada di webconfig : <compilation debug="true">

            btnSci.Visible = HttpContext.Current.IsDebuggingEnabled;
            btnSciD.Visible = HttpContext.Current.IsDebuggingEnabled;

            rowUserHostName.Visible = AppSession.Parameter.IsManualUserHostName;

            if (!IsPostBack)
            {
                txtUserID.Attributes.Add("autocomplete", "new-password");
                txtPassword.Attributes.Add("autocomplete", "new-password");

                if (AppSession.Parameter.IsManualUserHostName)
                {
                    var cookie = Request.Cookies["uhn"];
                    if (cookie != null)
                    {
                        var uhn = new UserHostPrinter();
                        if (uhn.LoadByPrimaryKey(cookie.Value))
                        {
                            cboUserHostName.Items.Add(new RadComboBoxItem(uhn.Description, uhn.UserHostName));
                            cboUserHostName.SelectedIndex = 0;
                        }
                    }

                    cboUserHostName.Focus();
                }
                else
                    txtUserID.Focus();

                if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString[0] != null && Page.Request.QueryString[0] == "logout")
                {
                    var userid = string.Empty;
                    try
                    {
                        WebService.KioskQueue.StopQueueCaller(AppSession.UserLogin.UserID);
                    }
                    catch
                    {

                    }

                    FormsAuthentication.SignOut();
                    HttpContext.Current.Session.RemoveAll();
                    HttpContext.Current.Session.Abandon();

                    ClearCookies();
                }
                else
                    ClearCookies();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            AddActiveVisitor();

            if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString[0] != null && !Page.Request.QueryString[0].ToLower().Contains("logout") && Session["_returnUrl"] != null)
            {
                var returnUrl = Session["_returnUrl"].ToString();

                HttpContext.Current.Session.RemoveAll();

                AppSession.UserLogin = _userLogin;

                Page.Response.Redirect(returnUrl);
            }
            else if (txtPassword.Text == AppSession.Parameter.DefaultUserPassword)
            {
                HttpContext.Current.Session.RemoveAll();

                AppSession.UserLogin = _userLogin;
                Page.Response.Redirect("~/Login/ChangePassword.aspx?msg=1&");
            }
            else
            {
                HttpContext.Current.Session.RemoveAll();

                AppSession.UserLogin = _userLogin;

                ReConsolidateRecord();

                Page.Response.Redirect("~/Default.aspx");
            }
        }

        private void ReConsolidateRecord()
        {
            if (!BusinessObject.Util.ConsolidationUtil.IsConsolidation) return;

            // Consolidation get data changes from HeadOffice
            if (Cache["IsConsolidate"] == null)
            {

                // Beri waktu max 10menit untuk konsolidasi untuk mencegah login lain melakukan proses yg sama
                Cache.Insert("IsConsolidate", "true", null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                    new TimeSpan(0, 10, 0));

                // Ambil perubahan data master dari Head Office
                BusinessObject.Util.ConsolidationUtil.UpdateDataMasterFromHeadOffice();


                Cache.Remove("IsConsolidate");

            }

            // Diremark krn membuat web server crash
            //Thread thread1 = new Thread(new ThreadStart(BusinessObject.Util.ConsolidationUtil.UpdateDataMasterFromHeadOffice)); 
            //thread1.Start();
        }

        private void AddActiveVisitor()
        {
            if (Application["activeVisitors"] != null)
            {
                Application.Lock();
                var visitorCount = (int)Application["activeVisitors"];
                visitorCount++;
                Application["activeVisitors"] = visitorCount;
                Application.UnLock();
            }
        }

        protected void btnSci_Click(object sender, EventArgs e)
        {
            //User jt
            //her_ma_one: Password 54321
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            var user = new AppUser();
            if (!user.LoadByPrimaryKey("jt"))
            {
                user = new AppUser();
                user.LoadByPrimaryKey("sci");
            }
            _userLogin = new UserLogin(user);
            if (AppSession.Parameter.IsManualUserHostName)
                _userLogin.UserHostName = cboUserHostName.SelectedValue;
            else
                _userLogin.UserHostName = ClientIP();

            var cultureInfo = new System.Globalization.CultureInfo(_userLogin.SRLanguage);
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;

            AddActiveVisitor();
            if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString[0] != null && !Page.Request.QueryString[0].ToLower().Contains("logout") && Session["_returnUrl"] != null)
            {
                string returnUrl = Session["_returnUrl"].ToString();

                HttpContext.Current.Session.RemoveAll();
                //ClearCookies();

                AppSession.UserLogin = _userLogin;
                Page.Response.Redirect(returnUrl);
            }
            else
            {
                HttpContext.Current.Session.RemoveAll();
                //ClearCookies();

                AppSession.UserLogin = _userLogin;

                ReConsolidateRecord();

                Page.Response.Redirect("~/Default.aspx");
            }
        }

        protected void btnSciD_Click(object sender, EventArgs e)
        {
            var user = new AppUser();
            user.LoadByPrimaryKey(!string.IsNullOrEmpty(txtUserID.Text) ? txtUserID.Text : "scid");

            _userLogin = new UserLogin(user);
            if (AppSession.Parameter.IsManualUserHostName)
                _userLogin.UserHostName = cboUserHostName.SelectedValue;
            else
                _userLogin.UserHostName = ClientIP();

            var cultureInfo = new System.Globalization.CultureInfo(_userLogin.SRLanguage);
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;

            AddActiveVisitor();
            if (Page.Request.QueryString.Count > 0 && Page.Request.QueryString[0] != null && !Page.Request.QueryString[0].ToLower().Contains("logout") && Session["_returnUrl"] != null)
            {
                string returnUrl = Session["_returnUrl"].ToString();

                HttpContext.Current.Session.RemoveAll();
                //ClearCookies();

                AppSession.UserLogin = _userLogin;
                Page.Response.Redirect(returnUrl);
            }
            else
            {
                HttpContext.Current.Session.RemoveAll();
                //ClearCookies();

                AppSession.UserLogin = _userLogin;
                Page.Response.Redirect("~/Default.aspx");
            }
        }

        protected void customValidator_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            if (txtUserID.Text.Equals(string.Empty) || txtPassword.Text.Equals(string.Empty))
                return;
            var user = new AppUser();
            if (user.LoadByPrimaryKey(txtUserID.Text))
            {
                _userLogin = new UserLogin(user);
                var attempt = Convert.ToInt32(Session["_attemptLogin"]);
                var keyPair = _userLogin.Validate(user, Encryptor.Encrypt(txtPassword.Text), ref attempt);
                Session["_attemptLogin"] = attempt;
                switch (keyPair.Key)
                {
                    case "1":
                        {
                            customValidator.ErrorMessage = keyPair.Value;
                            txtUserID.Focus();
                            args.IsValid = false;
                            break;
                        }
                    case "2":
                        {
                            customValidator.ErrorMessage = keyPair.Value;
                            txtPassword.Focus();
                            args.IsValid = false;
                            break;
                        }
                    default:
                        {
                            if (AppSession.Parameter.IsManualUserHostName && string.IsNullOrEmpty(cboUserHostName.SelectedValue))
                            {
                                customValidator.ErrorMessage = "Computer id required";
                                cboUserHostName.Focus();
                                args.IsValid = false;
                            }
                            else
                            {
                                if (AppSession.Parameter.IsManualUserHostName)
                                    _userLogin.UserHostName = cboUserHostName.SelectedValue;
                                else
                                    _userLogin.UserHostName = ClientIP();

                                args.IsValid = true;
                            }
                            break;
                        }
                }

                //if (user.IsLocked ?? false)
                //{
                //    customValidator.ErrorMessage = "User id is locked, contact administrator";
                //    txtPassword.Focus();
                //    args.IsValid = false;
                //}
                //else if (!user.Password.Equals(Encryptor.Encrypt(txtPassword.Text)))
                //{
                //    Session["_attemptLogin"] = Convert.ToInt32(Session["_attemptLogin"]) + 1;

                //    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingPasswordPolicy).ToLower() == "yes" && Convert.ToInt32(Session["_attemptLogin"]) >= Convert.ToInt32(AppParameter.GetParameterValue(AppParameter.ParameterItem.MaxAttemptFailedLogin)))
                //    {
                //        customValidator.ErrorMessage = string.Format("Failed login attempt is {0} of {1}, user id will deactivated or locked by system, contact administrator", Convert.ToInt32(Session["_attemptLogin"]), Convert.ToInt32(AppParameter.GetParameterValue(AppParameter.ParameterItem.MaxAttemptFailedLogin)));
                //        user.IsLocked = true;
                //        user.Save();
                //        args.IsValid = false;
                //    }
                //    else if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingPasswordPolicy).ToLower() == "yes" && Convert.ToInt32(Session["_attemptLogin"]) < Convert.ToInt32(AppParameter.GetParameterValue(AppParameter.ParameterItem.MaxAttemptFailedLogin)))
                //    {
                //        customValidator.ErrorMessage = string.Format("Password is not accepted or false, Failed login attempt is {0} of {1}", Convert.ToInt32(Session["_attemptLogin"]), Convert.ToInt32(AppParameter.GetParameterValue(AppParameter.ParameterItem.MaxAttemptFailedLogin)));
                //        txtPassword.Focus();
                //        args.IsValid = false;
                //    }
                //    else
                //    {
                //        customValidator.ErrorMessage = "Password is not accepted or false";
                //        txtPassword.Focus();
                //        args.IsValid = false;
                //    }
                //}
                //else if (user.ExpireDate.Value < DateTime.Now)
                //{
                //    customValidator.ErrorMessage = "User id has expired, contact administrator";
                //    txtUserID.Focus();
                //    args.IsValid = false;
                //}
                //else if (AppSession.Parameter.IsManualUserHostName && string.IsNullOrEmpty(cboUserHostName.SelectedValue))
                //{
                //    customValidator.ErrorMessage = "Computer id required";
                //    cboUserHostName.Focus();
                //    args.IsValid = false;
                //}
                //else
                //{
                //    if (AppSession.Parameter.IsManualUserHostName)
                //        _userLogin.UserHostName = cboUserHostName.SelectedValue;
                //    else
                //        _userLogin.UserHostName = ClientIP();

                //    args.IsValid = true;
                //}
            }
            else
            {
                customValidator.ErrorMessage = "User id is not registered, contact administrator";
                txtUserID.Focus();
                args.IsValid = false;
            }
        }

        private void ClearCookies()
        {
            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;

                if (cookieName.Split('_')[0] == "themes") continue;

                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(aCookie);
            }
        }

        protected void cboUserHostName_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var host = new UserHostPrinterQuery();
            host.Where(host.Description.Like(e.Text.Trim() + "%"));
            DataTable tbl = host.LoadDataTable();
            cboUserHostName.DataSource = tbl;
            cboUserHostName.DataBind();
        }

        protected void cboUserHostName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserHostName"].ToString();
        }


        private string ClientIP()
        {
            return Common.Helper.ClientIP();
        }

        protected string ApplicationLastBuildTime()
        {
            const int peHeaderOffset = 60;
            const int linkerTimestampOffset = 8;
            byte[] bytes = new byte[2048];
            using (FileStream file = new FileStream(typeof(Temiang.Avicenna.Login).Assembly.Location, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                file.Read(bytes, 0, bytes.Length);
            }
            Int32 headerPos = BitConverter.ToInt32(bytes, peHeaderOffset);
            Int32 secondsSince1970 = BitConverter.ToInt32(bytes, headerPos + linkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime dateTimeUTC = dt.AddSeconds(secondsSince1970);
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUTC, TimeZoneInfo.Local);
            return localTime.ToString("dd-MMM-yyyy HH:mm:ss"); //+ " " + TimeZoneInfo.Local.Id;
        }
    }
}
