using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.SirsDinkes
{
    public class Service
    {
        private string _url = ConfigurationManager.AppSettings["EisKetersediaanBedServiceUrlLocation"];
        private string _cid = ConfigurationManager.AppSettings["EisKetersediaanBedConsumerID"];
        private string _key = ConfigurationManager.AppSettings["EisKetersediaanBedSaltConsumerID"];

        private HttpWebRequest PopulateWebRequest(string url, BPJS.Helper.WebRequestMethod method, BPJS.Helper.WebRequestContentType contentType, string parameter)
        {
            BPJS.Helper.IgnoreBadCertificates();

            var webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = method.ToString();

            if (method != BPJS.Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

            webrequest.Headers.Add("Api-Bed-User", _cid);
            webrequest.Headers.Add("Api-Bed-Key", _key);

            if (method != BPJS.Helper.WebRequestMethod.GET)
            {
                byte[] formData = Encoding.UTF8.GetBytes(parameter.ToString());
                webrequest.ContentLength = formData.Length;

                using (var post = webrequest.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }
            }

            return webrequest;
        }

        public KetersediaanBed.Response.Root KetersediaanBed(KetersediaanBed.Request.Root data)
        {
            _url += "apibedv2/bed";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<KetersediaanBed.Response.Root>(sr.ReadToEnd());
            }
        }
    }
}
