using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna
{
    public partial class SesssionStretch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string Access(string data)
        {
            return Common.AppSession.UserLogin.UserID + "|" + data;
        }
    }
}