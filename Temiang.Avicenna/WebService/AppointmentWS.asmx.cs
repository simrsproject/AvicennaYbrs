using System.ComponentModel;
using System.Web.Services;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for AppointmentWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AppointmentWS : V1_0.AppointmentWS
    {
        
    }
}
