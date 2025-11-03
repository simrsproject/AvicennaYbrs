using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //SiAuto.Si.Connections = string.Format("file(append=true,filename={0}\\Error.log)", HttpContext.Current.Server.MapPath("~"));
            ////SiAuto.Si.Connections = "tcp()";

            //SiAuto.Si.AppName = "Avicenna";
            //SiAuto.Si.Enabled = true;


            //---------------------------------------------------------------
            // Assign the Loader
            //---------------------------------------------------------------        
            Dal.Interfaces.esProviderFactory.Factory = new Dal.Loader.esDataProviderFactory();
            //            if (AppSession.Parameter.HealthcareInitial == "RSMM")
            //            {
            //                string msg = @"Application: Airbrake.OutOfMemoryException.exe
            //Framework Version: v4.0.30319
            //Description: The application requested process termination through System.Environment.FailFast(string message).
            //Message: Out of Memory: Insufficient memory to continue the execution of the program.
            //Stack:
            //   at System.Environment.FailFast(System.String)
            //   at Airbrake.OutOfMemoryException.Program.ThrowExample()
            //   at Airbrake.OutOfMemoryException.Program.Main(System.String[])";

            //                AppSession.LastErrorException = new Exception(msg);
            //                Response.Redirect("~/ErrorPage.aspx");
            //            }
            // Application started - Initializing to 0
            //Application["activeVisitors"] = 0;


            // Route Registration (jangan dibalik urutannya akan tidak jalan API nya)

            // 1. HTTP Web API routes (Bridging)
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsOpenAntrianBridging))
                System.Web.Http.GlobalConfiguration.Configure(Temiang.Avicenna.Bridging.WebApiConfig.Register);

            System.Web.Http.GlobalConfiguration.Configure(Temiang.Avicenna.Bridging.WebApiConfig.RegisterBridgingQueueing);

            // 2. MVC routes
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        //protected void Application_PostAuthorizeRequest()
        //{
        //    if (IsWebApiRequest())
        //    {
        //        HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        //    }
        //}

        //private bool IsWebApiRequest()
        //{
        //    return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative);
        //}

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    // Code that runs when an unhandled error occurs
        //    //Logger.LogException(Server.GetLastError().GetBaseException());

        //    // Code that runs when an unhandled error occurs

        //    // Get the exception object.
        //    var exc = AppSession.LastErrorException ?? Server.GetLastError();

        //    // Handle HTTP errors
        //    if (exc.GetType() == typeof(HttpException))
        //    {
        //        // The Complete Error Handling Example generates
        //        // some errors using URLs with "NoCatch" in them;
        //        // ignore these here to simulate what would happen
        //        // if a global.asax handler were not implemented.
        //        if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
        //            return;
        //        //Redirect HTTP errors to HttpError page
        //        Server.Transfer("ErrorPage.aspx");

        //    }


        //    //// For other kinds of errors give the user some information
        //    //// but stay on the default page
        //    //Response.Write("<h2>Global Page Error</h2>\n");
        //    //Response.Write(
        //    //    "<p>" + exc.Message + "</p>\n");
        //    //Response.Write("Return to the <a href='Default.aspx'>" +
        //    //    "Default Page</a>\n");

        //    // Clear the error from the server
        //    Server.ClearError();
        //}

        //protected void Application_End(object sender, EventArgs e)
        //{
        //    SiAuto.Si.Dispose();
        //}

        //void Session_End(object sender, EventArgs e)
        //{
        //    if (Application["activeVisitors"] != null)
        //    {
        //        Application.Lock();
        //        var visitorCount = (int)Application["activeVisitors"];
        //        visitorCount--;
        //        Application["activeVisitors"] = visitorCount;
        //        Application.UnLock();
        //    }
        //}

        //private void Session_Start(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(Request.QueryString["logid"]) && Request.QueryString["logid"] != "undefined")
        //    {
        //        // Cek di table UserLog jika dipanggil dg kiriman sessionid dan jika valid maka tidak dimunculkan login page
        //        var userLog = new UserLog();

        //        // Cek existensi, logout & clientIP harus sama dg yg di log ..asumsi harus dari browser yg sama
        //        if (userLog.LoadByPrimaryKey(Convert.ToInt64(Request.QueryString["logid"])) && userLog.LogoutDateTime == null && userLog.ClientIP == Helper.GetIPAddress())
        //        {
        //            userLog.LogoutDateTime = (new DateTime()).NowAtSqlServer(); 
        //            userLog.Save();
        //            var user = new AppUser();
        //            if (user.LoadByPrimaryKey(userLog.UserID))
        //            {
        //                AppSession.UserLogin = new UserLogin(user);
        //            }

        //        }

        //    }
        //}
    }
}