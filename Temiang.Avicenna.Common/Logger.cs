using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Web;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common
{
    public class Logger
    {
        public static void LogException(Exception ex, string userHostName, string userName)
        {
            AppSession.LastErrorException = ex; //for show at ErrorPage
            //SiAuto.Main.LogException(ex);
            //HoptoadGateway.GetHoptoadGateway().Notify(HttpContext.Current.Request, ex);

            // Send Mail
            try
            {
                if (userHostName != "127.0.0.1") // Email for non local / non development proccess
                {
                    var subject = string.Format("[{3} {0} {1}] {2}",
                        AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitialAppsVersion), DateTime.Now,
                        ex.Message, ApplicationSettings.DefaultApplication.Name);
                    var toAddress = AppParameter.GetParameterValue(AppParameter.ParameterItem.AplicationErrorEmailAddress);

                    if (!string.IsNullOrWhiteSpace(toAddress))
                        Mail.SendMailUseOtherThread(toAddress, subject, ErrorMessage(ex, userHostName, userName));
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static string ErrorMessage(Exception ex, string userHostName, string userName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("User Host : {0}  User : {1} Time: {2}", userHostName, userName, DateTime.Now);
            sb.AppendLine("");
            sb.AppendLine("");
            sb.Append(ErrorTrace(ex));
            if (ex != null && ex.InnerException != null)
            {
                sb.AppendLine("");
                sb.AppendLine("INNER EXCEPTION :");

                sb.Append(ErrorTrace(ex.InnerException));
            }
            return sb.ToString();
        }

        private static StringBuilder ErrorTrace(Exception exception)
        {
            StringBuilder sb = new StringBuilder();
            if (exception == null) return sb;

            sb.AppendLine("Error Message :");
            sb.AppendLine("===============");
            sb.AppendLine(exception.Message);
            sb.AppendLine();
            sb.AppendLine("Error Location :");
            sb.AppendLine("================");

            var trace = new StackTrace(exception, true);
            for (int j = 0; j < trace.FrameCount; ++j)
            {
                var fileName = trace.GetFrame(j).GetFileName();
                if (!string.IsNullOrEmpty(fileName))
                {
                    var lineNumber = trace.GetFrame(j).GetFileLineNumber().ToString();
                    var methodName = trace.GetFrame(j).GetMethod().Name;
                    sb.AppendFormat("File: {0}, Line: {1}, Method: {2}", fileName, lineNumber, methodName);
                    sb.AppendLine();
                }
            }

            var sqlEx = exception as SqlException;
            if (sqlEx != null)
            {
                sb.AppendLine("Sql Server Error :");
                sb.AppendLine("==================");
                foreach (SqlError error in sqlEx.Errors)
                {
                    sb.AppendFormat("Message: {0}, Error: {1}, Line: {2}, Procedure: {3}, Server: {4}, Source: {5}", error.Message, error.Number, error.LineNumber, error.Procedure, error.Server, error.Source);
                    sb.AppendLine();
                }
            }

            sb.AppendLine();
            sb.AppendLine();

            sb.AppendLine("Stack Trace :");
            sb.AppendLine("=============");
            sb.AppendLine(exception.StackTrace);
            return sb;
        }

        public static void LogError(string title)
        {
        }

        public static void LogMessage(string title)
        {
        }

        public static void EnterMethod(string methodName)
        {
        }

        public static void EnterMethod(object instance, string methodName)
        {
        }

        public static void LeaveMethod(string methodName)
        {
        }

        public static void LeaveMethod(object instance, string methodName)
        {
        }

        public static void EnterProcess(string processName)
        {
        }

        public static void LeaveProcess(string processName)
        {
        }

        public static void LogObject(string title, object instance)
        {
        }
    }
}
