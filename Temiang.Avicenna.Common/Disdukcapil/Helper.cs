using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Disdukcapil
{
    public class Helper
    {
        public static string DukcapilServiceUrlLocation
        {
            get { return ConfigurationManager.AppSettings["InacbgServiceUrlLocation"]; }
        }

        public static string DukcapilUserID
        {
            get { return ConfigurationManager.AppSettings["DukcapilUserID"]; }
        }

        public static string DukcapilPassword
        {
            get { return ConfigurationManager.AppSettings["DukcapilPassword"]; }
        }

        public static string DukcapilHostIP
        {
            get { return ConfigurationManager.AppSettings["DukcapilHostIP"]; }
        }

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

        public static HttpWebRequest PopulateWebRequest(string methodName, Helper.WebRequestMethod requestMethod, Helper.WebRequestContentType contentType, string parameter)
        {
            string url = string.Format("{0}/{1}", DukcapilServiceUrlLocation, methodName);
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            webrequest.Method = requestMethod.ToString();

            if (requestMethod != Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

            var formData = Encoding.UTF8.GetBytes(parameter.ToString());
            webrequest.ContentLength = formData.Length;

            using (var post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            return webrequest;
        }
    }
}
