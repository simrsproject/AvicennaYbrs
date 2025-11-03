using System;

namespace Temiang.Avicenna.CustomControl
{
    public partial class HeaderCtl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //imgLogo.Visible = !HttpContext.Current.IsDebuggingEnabled;
            imgLogo.Visible = false;
        }
    }
}