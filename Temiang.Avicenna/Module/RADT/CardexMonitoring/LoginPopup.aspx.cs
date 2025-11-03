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
    public partial class LoginPopup : BasePageDialog
    {
        private UserLogin _userLogin = new UserLogin();
        protected Brand _brand = new Brand();
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = _brand.Name.ToUpper();
            ProgramID = "LoginPopup";

            // Jika user tekan ENTER 
            this.Form.DefaultButton = ButtonOk.UniqueID;
            ButtonOk.ValidationGroup = "login";

            if (!IsPostBack)
            {
                txtUserID.Attributes.Add("autocomplete", "new-password");
                txtPassword.Attributes.Add("autocomplete", "new-password");

                txtUserID.Focus();

                // Selalu logout
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.RemoveAll();
                HttpContext.Current.Session.Abandon();

                //ClearCookies();
                RestoreValueFromCookie();
            }
        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            if (!Page.IsValid)
                return;

            HttpContext.Current.Session.RemoveAll();

            AppSession.UserLogin = _userLogin;

            SaveValueToCookie(txtPassword);
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oWnd.argument.url = '{0}'", Request.QueryString["url"]);
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
                }

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

    }
}
