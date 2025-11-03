using System;
using Temiang.Avicenna.Common;
using Newtonsoft.Json;

namespace Temiang.Avicenna.MasterPage
{
    public partial class MasterCustom : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void fw_RadNotification_CallbackUpdate(object sender, Telerik.Web.UI.RadNotificationEventArgs e)
        {
            var ws = new WebService.NotificationService();
            var response = JsonConvert.DeserializeObject<WebService.NotificationServiceResponse>(ws.GetDeceasedPatientContent(string.Empty));
            if (response.Status == "1")
            {
                var img = Helper.UrlRoot() + "/Images/black-ribbon.png";
                litNotification.Text = response.Value.Replace("@", img);
            }
        }
    }
}
