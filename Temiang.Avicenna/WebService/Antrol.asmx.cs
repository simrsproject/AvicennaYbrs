using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for Antrol
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Antrol : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public string CancelOutstandingAppointment()
        {
            var appt = new AppointmentCollection();
            appt.Query.Where(
                appt.Query.AppointmentDate.Date() == DateTime.Now.Date.AddDays(-1).Date,
                appt.Query.SRAppointmentStatus.In(AppSession.Parameter.AppointmentStatusOpen, AppSession.Parameter.AppointmentStatusConfirmed)
                );
            appt.Query.Load();

            foreach (var data in appt)
            {
                var svc = new Common.BPJS.Antrian.Service();
                var param = new Common.BPJS.Antrian.Update.BatalAntrian.Request.Root()
                {
                    Kodebooking = data.AppointmentNo,
                    Keterangan = "tidak hadir"
                };
                var respose = svc.BatalAntrian(param);
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "10.200.200.188",
                    UrlAddress = "BatalAntrean",
                    Params = JsonConvert.SerializeObject(param),
                    Response = JsonConvert.SerializeObject(respose),
                    Totalms = 0
                };
                log.Save();
                if (!respose.Metadata.IsAntrolValid) continue;
                var antrian = new Appointment();
                antrian.LoadByPrimaryKey(data.AppointmentNo);
                antrian.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusCancel;
                antrian.LastUpdateByUserID = "WEBSERVICE";
                antrian.LastUpdateDateTime = DateTime.Now;
                antrian.Save();
            }

            return "ok";
        }
    }
}
