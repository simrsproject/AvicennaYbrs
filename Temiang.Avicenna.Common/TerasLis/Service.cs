using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace Temiang.Avicenna.Common.TerasLis
{
    public class Service
    {
        readonly string _url = "https://demo2.terassekawanbersama.co.id/ws/";
        readonly string _user = "demo";
        readonly string _key = "123qwe";
        readonly string _cid = "4301202030080005";

        private Json.Response.Auth Login()
        {
            var webrequest = (HttpWebRequest)WebRequest.Create(_url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/json";
            webrequest.Headers.Add("x-user", _user);
            webrequest.Headers.Add("x-secret", _key);
            webrequest.Headers.Add("x-mod", "auth");
            webrequest.Headers.Add("x-cid", _cid);

            var response = webrequest.GetResponse() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var sr = new StreamReader(response.GetResponseStream());
            return JsonConvert.DeserializeObject<Json.Response.Auth>(sr.ReadToEnd());
        }

        public Json.Response.Result.Root GetResult(string orderNo)
        {
            var login = Login();
            if (login == null) return null;

            var webrequest = (HttpWebRequest)WebRequest.Create(_url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/json";
            webrequest.Headers.Add("x-token", login.Token);
            webrequest.Headers.Add("x-noo", orderNo);
            webrequest.Headers.Add("x-mod", "get_hasil");

            var response = webrequest.GetResponse() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var sr = new StreamReader(response.GetResponseStream());
            return JsonConvert.DeserializeObject<Json.Response.Result.Root>(sr.ReadToEnd());
        }

        public Json.Response.Order.Root PostOrder(Json.Request.Order.Root root)
        {
            var login = Login();
            if (login == null) return null;

            var webrequest = (HttpWebRequest)WebRequest.Create(_url);
            webrequest.Method = "POST";
            webrequest.ContentType = "application/json";
            webrequest.Headers.Add("x-token", login.Token);
            webrequest.Headers.Add("x-mod", "order");

            byte[] formData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(root));
            webrequest.ContentLength = formData.Length;

            using (var post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            var response = webrequest.GetResponse() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var sr = new StreamReader(response.GetResponseStream());
            return JsonConvert.DeserializeObject<Json.Response.Order.Root>(sr.ReadToEnd());
        }
    }
}