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

namespace Temiang.Avicenna.Module.Reports.ESign
{
    public partial class ESignLogin : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonOk.ValidationGroup = "entry";

            if (!Page.IsPostBack)
            {
                txtPassword.Attributes.Add("autocomplete", "new-password");
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            var user = new AppUser();
            user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
            return String.Format("oWnd.argument.esignkey = '{0}_{1}';", user.ESignNik, txtPassword.Text);

        }
        public override bool OnButtonOkClicked()
        {
            return string.IsNullOrEmpty(customValidator.ErrorMessage);
        }

        protected void customValidator_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            customValidator.ErrorMessage = string.Empty;
            var user = new AppUser();
            user.LoadByPrimaryKey(AppSession.UserLogin.UserID);
            var eSignNik = user.ESignNik; 

            if (string.IsNullOrWhiteSpace(eSignNik) )
            {
                args.IsValid = false;
                customValidator.ErrorMessage = "NIK still empty, please contact IT Department";
                return;
            }

            // Check user status
            var url = string.Format("{0}/api/user/status/{1}", AppParameter.GetParameterValue(AppParameter.ParameterItem.ESignUrlRoot), eSignNik);
            var userId = AppParameter.GetParameterValue(AppParameter.ParameterItem.ESignUserId);
            var pwd = AppParameter.GetParameterValue(AppParameter.ParameterItem.ESignPassword);

            var client = new RestClient(url);
            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(userId, pwd);

            var request = new RestSharp.RestRequest();
            request.Method = Method.Get;

            var timeOutPar = AppParameter.GetParameterValue(AppParameter.ParameterItem.PCareTimeOutInSecond);
            var timeOut = Convert.ToInt16(timeOutPar) * 1000;
            request.Timeout = timeOut;

            var response = client.Execute(request);
            if (response != null)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
                {
                    args.IsValid = false;
                    customValidator.ErrorMessage = "NIK not registered, please check NIK";
                    return;
                }
                if (response.ResponseStatus== RestSharp.ResponseStatus.TimedOut)
                {
                    args.IsValid = false;
                    customValidator.ErrorMessage = "Can not connect to ESign server, please try again";
                    return;
                }

                // Check status
                if (!string.IsNullOrEmpty(response.Content))
                {
                    var respStat = JsonConvert.DeserializeObject<EsignResponseStatus>(response.Content);
                    if (respStat != null )
                    {
                        if (respStat.StatusCode== 1110)
                        {
                            args.IsValid = false;
                            customValidator.ErrorMessage = "NIK not registered, please check NIK";
                        }
                    }
                }
            }
        }
    }

    public class EsignResponseStatus
    {
        [JsonProperty("status_code")]
        public int StatusCode;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("message")]
        public string Message;
    }
}
