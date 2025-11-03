using System;

namespace Temiang.Avicenna
{
    public partial class AccessFailed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["tp"] == "snv") // Signature not valid
                lblMessage.Text = "Your authorization access for this page not yet approved, please contact your supervisor.";
            else
                lblMessage.Text = "You don't have the authorization access for this page, please contact your administrator.";
        }
    }
}
