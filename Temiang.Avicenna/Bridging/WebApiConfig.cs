using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace Temiang.Avicenna.Bridging
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "antrol_bpjs/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "jaki/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            if (ConfigurationManager.AppSettings["IsApiUsingMultipartFormData"] != null && ConfigurationManager.AppSettings["IsApiUsingMultipartFormData"] == "true")
                config.Formatters.XmlFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));
        }

        public static void RegisterBridgingQueueing(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Routes.MapHttpRoute(
                name: "queuingGetQuota",
                routeTemplate: "api/{accesskey}/{controller}/GetQuota/{date}"
            );
            config.Routes.MapHttpRoute(
                name: "queuingGetQueueBeenCalled",
                routeTemplate: "api/{accesskey}/{controller}/GetQueueBeenCalled/{date1}"
            );

            config.Routes.MapHttpRoute(
                name: "queuingSetQueue1",
                routeTemplate: "api/{accesskey}/{controller}/SetQueue/{date}/{serviceUnitId}/{paramedicId}"
            );
            config.Routes.MapHttpRoute(
                name: "queuingSetQueue2",
                routeTemplate: "api/{accesskey}/{controller}/SetQueue/{date}/{serviceUnitId}/{paramedicId}/{queNumber}"
            );

            config.Routes.MapHttpRoute(
                name: "queuingSetQueueByType1",
                routeTemplate: "api/{accesskey}/{controller}/SetQueueByType/{date}/{serviceUnitId}/{paramedicId}/{type}"
            );
            config.Routes.MapHttpRoute(
               name: "queuingSetQueueByType2",
               routeTemplate: "api/{accesskey}/{controller}/SetQueueByType/{date}/{serviceUnitId}/{paramedicId}/{type}/{queNumber}"
            );

            config.Routes.MapHttpRoute(
                name: "queuingGetListQueueRegistrationBeenCalled",
                routeTemplate: "api/{accesskey}/{controller}/GetListQueueRegistrationBeenCalled/{date2}"
            );
            config.Routes.MapHttpRoute(
                name: "queuingGetListQueuePolyclinicBeenCalled",
                routeTemplate: "api/{accesskey}/{controller}/GetListQueuePolyclinicBeenCalled/{date3}"
            );
            config.Routes.MapHttpRoute(
                name: "queuingGetListQueuePrefix",
                routeTemplate: "api/{accesskey}/{controller}/GetListQueuePrefix/{date1}/{prefix}"
            );
            config.Routes.MapHttpRoute(
                name: "queuingGetListQueueBeingServedByPrefix",
                routeTemplate: "api/{accesskey}/{controller}/GetListQueueBeingServedByPrefix/{date2}/{prefix}"
            );

        }
    }
}
