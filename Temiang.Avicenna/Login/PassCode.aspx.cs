using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Text;
using Newtonsoft.Json;
using Telerik.Web.UI;

namespace Temiang.Avicenna
{
    public partial class PassCode : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                litPassCodeCaption.Text = string.Format("<h3>This program requires a passcode&nbsp;&nbsp;{0}</h3>", LinkEditPassCode());
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            // Isi LastAccessSecureProgramID untuk pengecekan diprogram pemnaggil
            AppSession.UserLogin.LastAccessSecureProgramID = Request.QueryString["spgid"];

            // Redirect kembali ke program pemanggil
            var url = HttpUtility.UrlDecode(Request.QueryString["url"]);
            Response.Redirect(url);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!IsProgramSignatureValid(txtPassword.Text))
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "Passcode not accepted";
                txtPassword.Focus();
            }
        }

        protected string LinkEditPassCode()
        {
            // Hanya power user yg bisa merubah passcode
            var userAccess = new UserAccess(AppSession.UserLogin.UserID, Request.QueryString["spgid"]);

            if (userAccess.IsPowerUserAble)
                return "<a href='#' onclick='javascript:openWindow(\"ChangePassCode.aspx\"); event.cancelBubble = true;if(event.stopPropagation) event.stopPropagation();'><img src='../Images/Toolbar/edit16.png' alt='edit' /></a>";

            return string.Empty;
        }
    }
}
