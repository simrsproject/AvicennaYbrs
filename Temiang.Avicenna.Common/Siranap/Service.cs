using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;


namespace Temiang.Avicenna.Common.Siranap
{
    public class Service
    {
        private string _cid = ConfigurationManager.AppSettings["SiranapHospitalID"];
        private string _key = ConfigurationManager.AppSettings["SiranapSaltConsumerID"];

        private HttpWebRequest PopulateWebRequest(string url, BPJS.Helper.WebRequestMethod method, BPJS.Helper.WebRequestContentType contentType, string parameter)
        {
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            webrequest.Method = method.ToString();

            if (method != BPJS.Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();
            webrequest.Headers.Add("X-rs-id", _cid);
            webrequest.Headers.Add("X-timestamp", Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds()).ToString());
            webrequest.Headers.Add("X-pass", _key);

            if (!string.IsNullOrWhiteSpace(parameter))
            {
                var formData = Encoding.UTF8.GetBytes(parameter.ToString());
                webrequest.ContentLength = formData.Length;

                using (var post = webrequest.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }
            }

            return webrequest;
        }

        public string SendV21(bool isPost, string param)
        {
            var url = "https://sirs.kemkes.go.id/fo/index.php/Fasyankes";
            using (HttpWebResponse response = PopulateWebRequest(url, isPost ? BPJS.Helper.WebRequestMethod.POST : BPJS.Helper.WebRequestMethod.PUT, BPJS.Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string ReferensiKelas()
        {
            var url = "https://sirs.kemkes.go.id/fo/index.php/Referensi/tempat_tidur";
            using (HttpWebResponse response = PopulateWebRequest(url, BPJS.Helper.WebRequestMethod.GET, BPJS.Helper.WebRequestContentType.FORM, null).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public Json.Select.Root SelectV21()
        {
            var url = "https://sirs.kemkes.go.id/fo/index.php/Fasyankes";
            using (HttpWebResponse response = PopulateWebRequest(url, BPJS.Helper.WebRequestMethod.GET, BPJS.Helper.WebRequestContentType.FORM, null).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Json.Select.Root>(sr.ReadToEnd());
            }
        }

        public string DeleteV21(Json.Delete.Root param)
        {
            var url = "https://sirs.kemkes.go.id/fo/index.php/Fasyankes";
            using (HttpWebResponse response = PopulateWebRequest(url, BPJS.Helper.WebRequestMethod.DELETE, BPJS.Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(param)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }
    }

    public class Response
    {
        [JsonProperty("response")]
        public string response { get; set; }

        [JsonProperty("deskripsi")]
        public string deskripsi { get; set; }
    }
}
