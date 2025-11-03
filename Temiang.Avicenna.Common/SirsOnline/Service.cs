using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.SirsOnline
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

            webrequest.Headers.Add("X-rs-id", _cid);
            webrequest.Headers.Add("X-pass", _key);

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

        public string DataKunjunganIgd(List<DataKunjungan.Igd> data, string tanggal)
        {
            _url += $"sirsservice/igd/{tanggal}";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string DataKunjunganIrj(List<DataKunjungan.Irj> data, string tanggal)
        {
            _url += $"sirsservice/irj/{tanggal}";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string DataKunjunganIri(List<DataKunjungan.Iri> data, string tanggal)
        {
            _url += $"sirsservice/iri/{tanggal}";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string BedMonitor(BedMonitor data)
        {
            _url += $"sirsservice/ranap";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string DiagnosaTerbesar(List<DiagnosaTerbesar> data, bool isIri, string bulanTahun)
        {
            if (isIri) _url += $"sirsservice/diagnosa_iri/" + bulanTahun;
            else _url += $"sirsservice/diagnosa_irj/" + bulanTahun;

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string IndikatorPelayanan(IndikatorPelayanan data, string bulanTahun)
        {
            _url += $"sirsservice/bor/" + bulanTahun;

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }
    }
}
