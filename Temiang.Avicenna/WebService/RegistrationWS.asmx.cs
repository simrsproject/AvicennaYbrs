using System.ComponentModel;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for RegistrationWS
    ///  fj ljsfjasdf jasdfjasdlfj asdfjasdf jsdjf als
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RegistrationWS : V1_0.RegistrationWS
    {
        [WebMethod]
        public string GetPasienByNikDukcapil(string nik)
        {
            var patient = new Patient();
            patient.Query.Where(patient.Query.Ssn == nik, patient.Query.IsSyncWithDukcapil == true);
            return patient.Query.Load() ? patient.PatientID : string.Empty;
        }
    }
}
