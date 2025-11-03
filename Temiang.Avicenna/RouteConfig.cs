using System.Web.Mvc;
using System.Web.Routing;

namespace Temiang.Avicenna
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            //routes.IgnoreRoute("{resource}.asmx/{*pathInfo}");
            routes.IgnoreRoute("{*allasmx}", new { allasmx = @".*\.asmx(/.*)?" });
            //routes.IgnoreRoute("{*allmap}", new { allmap = @".*\.map(/.*)?" });
            routes.IgnoreRoute("");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "None", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
