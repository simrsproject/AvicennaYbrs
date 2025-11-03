using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for SystemService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SystemService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public string StoreCurrentWidthAndHeightClientScreen(string width, string height)
        {
            //AppSession.ScreenClient.Width = width.ToInt();
            //AppSession.ScreenClient.Height = height.ToInt();
            return string.Empty;
        }
    }
}
