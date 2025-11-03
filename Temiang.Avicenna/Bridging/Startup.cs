using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Text;
using Temiang.Avicenna.BusinessObject;

[assembly: OwinStartup(typeof(Temiang.Avicenna.Bridging.Startup))]
namespace Temiang.Avicenna.Bridging
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Diganti menggunakan AuthenticationMiddleware
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            //{
            //    AllowInsecureHttp = true,

            //    //The Path For generating the Toekn
            //    TokenEndpointPath = new PathString("/token"),

            //    ////Setting the Token Expired Time (24 hours)
            //    //AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),

            //    //Setting the Token Expired Time (10 Minutes)
            //    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),


            //    //AuthorizationServerProvider class will validate the user credentials
            //    Provider = new AuthorizationServerProvider()
            //};

            ////Token Generations
            //app.UseOAuthAuthorizationServer(options);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsOpenAntrianBridging))
                app.Use(typeof(AuthenticationMiddleware));
        }
    }

}
