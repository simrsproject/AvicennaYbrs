using System;
using Temiang.Avicenna.Common;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using System.IO;
using System.Linq;

namespace Temiang.Avicenna
{
    public partial class ErrorPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            //Get error
            Exception ex = null; ;
            if (AppSession.LastErrorException != null)
                ex = AppSession.LastErrorException;

            if (ex == null)
            {
                var lastEx = Server.GetLastError();
                if (lastEx != null)
                    ex = lastEx.GetBaseException();
            }

            //Reset history
            AppSession.LastErrorException = null;

            var customMessage = new MessageUtil.Message { IsError = false };
            if (ex is MessageException)
            {
                customMessage = MessageUtil.GetMessage((MessageException)ex);
            }
            else if (ex is SqlException)
            {
                customMessage = MessageUtil.GetMessage((SqlException)ex);
            }
            else
            {
                customMessage.MessageText = AppMessage.GetMessageText(AppMessage.MessageIdEnum.CommonMsg);
                customMessage.IsError = true;
            }

            lblErrorMessage.Text = customMessage.MessageText;

            if (customMessage.IsError)
            {
                var url = Server.UrlDecode(Request.QueryString["url"]);
                var path = Server.UrlDecode(Request.QueryString["path"]);

                var hcName = string.Empty;
                try
                {
                    // Coba ambil info Healthcare
                    var par = new AppParameter();
                    if (par.LoadByPrimaryKey("HealthcareID"))
                    {
                        var hc = new Healthcare();
                        hc.LoadByPrimaryKey(par.ParameterValue);
                        hcName = hc.HealthcareName;
                    }
                }
                catch (Exception err)
                {
                    // Nothing
                }

                txtErrorTrace.Text = string.Concat("Host Info: ", Environment.NewLine,"=========="
                    , Environment.NewLine,"Site: ", hcName, Environment.NewLine, "Url: ", url
                    , Environment.NewLine, "App Path: ", path
                    , Environment.NewLine, "Built Time: ", ApplicationLastBuildTime()
                    , Environment.NewLine, Logger.ErrorMessage(ex, Request.UserHostName, AppSession.UserLogin.UserName));
            }
            else
            {
                clpErrorDetail.Visible = false;
            }

        }

        private string ApplicationLastBuildTime()
        {
            const int peHeaderOffset = 60;
            const int linkerTimestampOffset = 8;
            byte[] bytes = new byte[2048];
            using (FileStream file = new FileStream(typeof(Temiang.Avicenna.Login).Assembly.Location, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                file.Read(bytes, 0, bytes.Length);
            }
            Int32 headerPos = BitConverter.ToInt32(bytes, peHeaderOffset);
            Int32 secondsSince1970 = BitConverter.ToInt32(bytes, headerPos + linkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime dateTimeUTC = dt.AddSeconds(secondsSince1970);
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUTC, TimeZoneInfo.Local);
            return localTime.ToString("dd-MMM-yyyy HH:mm:ss"); //+ " " + TimeZoneInfo.Local.Id;
        }
    }
}
