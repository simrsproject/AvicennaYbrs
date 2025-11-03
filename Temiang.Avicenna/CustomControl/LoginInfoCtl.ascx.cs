using System;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Web;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.CustomControl
{
    public partial class LoginInfoCtl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            var displayUserName = string.IsNullOrEmpty(AppSession.UserLogin.ParamedicName) ? AppSession.UserLogin.UserName : AppSession.UserLogin.ParamedicName;

            lblUserName.Text = string.Concat(displayUserName," [",AppSession.UserLogin.UserID, "] {", Helper.GetUserHostName(), "}");

            //if (Application["activeVisitors"] != null)
            //    lblActiveVisitor.Text =string.Format(" [Active User : {0}  ]", Application["activeVisitors"]);
            //else
            //    lblActiveVisitor.Text = "";

            //cboThemes.SelectedValue = Request.Cookies["themes_" + AppSession.UserLogin.UserID]["themes"];

            var style = System.Configuration.ConfigurationSettings.AppSettings["DefaultStyle"];
            cboThemes.Visible = string.IsNullOrEmpty(style);
        }

        protected void cboThemes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cookie = Request.Cookies["themes_" + AppSession.UserLogin.UserID];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            cookie = new HttpCookie("themes_" + AppSession.UserLogin.UserID);
            cookie["themes"] = e.Value;
            Response.Cookies.Add(cookie);

            Response.Redirect(Request.Url.ToString());
        }
    }
}