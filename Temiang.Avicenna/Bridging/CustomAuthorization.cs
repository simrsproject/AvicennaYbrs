using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var msg = new
            {
                metadata = new Base.MetaData("201", "Token Expired")
            };
            var retval = JsonConvert.SerializeObject(msg, Formatting.Indented);
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                //Content = new StringContent("{\"response\":{ },\"metadata\": { \"message\": \"Fail\",\"code\": -1}}", System.Text.Encoding.UTF8, "application/json")
                Content = new StringContent(retval, System.Text.Encoding.UTF8, "application/json")
            };
        }
    }

    public class JakSehatCustomAuthorization : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var msg = new
            {
                metadata = new Base.MetaData("401", "Token Expired")
            };
            var retval = JsonConvert.SerializeObject(msg, Formatting.Indented);
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                //Content = new StringContent("{\"response\":{ },\"metadata\": { \"message\": \"Fail\",\"code\": -1}}", System.Text.Encoding.UTF8, "application/json")
                Content = new StringContent(retval, System.Text.Encoding.UTF8, "application/json")
            };
        }
    }
}