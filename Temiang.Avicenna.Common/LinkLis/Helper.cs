using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common.LinkLis
{
    public class Helper
    {
        public sealed class WebRequestMethod
        {
            private readonly String name;
            private readonly int value;

            public static readonly WebRequestMethod GET = new WebRequestMethod(1, "GET");
            public static readonly WebRequestMethod POST = new WebRequestMethod(2, "POST");
            public static readonly WebRequestMethod PUT = new WebRequestMethod(3, "PUT");
            public static readonly WebRequestMethod DELETE = new WebRequestMethod(4, "DELETE");

            private WebRequestMethod(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public sealed class WebRequestContentType
        {
            private readonly String name;
            private readonly int value;

            public static readonly WebRequestContentType TEXT = new WebRequestContentType(1, "text/plain");
            public static readonly WebRequestContentType FORM = new WebRequestContentType(2, "Application/x-www-form-urlencoded");
            public static readonly WebRequestContentType JSON = new WebRequestContentType(3, "application/json; charset=utf-8");
            public static readonly WebRequestContentType DATA = new WebRequestContentType(3, "application/form-data");


            private WebRequestContentType(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public static HttpWebRequest PopulateWebRequest(string url, WebRequestMethod requestMethod, WebRequestContentType contentType, string parameter)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            webrequest.Method = requestMethod.ToString();

            var ws = new WebServiceAccessKey();
            ws.LoadByPrimaryKey(AppSession.Parameter.LisInterop);
            webrequest.Headers.Add("Key", ws.AccessKey);

            if (!string.IsNullOrEmpty(parameter))
            {
                if (requestMethod != WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

                var formData = Encoding.UTF8.GetBytes(parameter.ToString());
                webrequest.ContentLength = formData.Length;

                using (var post = webrequest.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }
            }

            return webrequest;
        }
    }
}
