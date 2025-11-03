using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.SirsYankes
{
    public class Service
    {
        private string _url = ConfigurationManager.AppSettings["SirsYankesServiceUrlLocation"];
        private string _cid = ConfigurationManager.AppSettings["SirsYankesConsumerID"];
        private string _key = ConfigurationManager.AppSettings["SirsYankesSaltConsumerID"];

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

        public string InsertDataKunjunganIrj(List<DataKunjungan.Json.Irj> irj, DateTime tanggal)
        {
            _url += "irj/" + tanggal.ToString("d-M-YYYY");

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(irj)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string InsertDataKunjunganIgd(DataKunjungan.Json.Igd igd, DateTime tanggal)
        {
            _url += "igd/" + tanggal.ToString("d-M-YYYY");

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(igd)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string InsertDataKunjunganIri(List<DataKunjungan.Json.Iri> iri, DateTime tanggal)
        {
            _url += "iri/" + tanggal.ToString("d-M-YYYY");

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(iri)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string InsertBedMonitoring(List<BedMonitoring.Json> json)
        {
            _url += "sirsservice/ranap";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(json)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string InsertDiagnosaTerbesarIrj(List<DiagnosaTerbesar.Json> json, DateTime tanggal)
        {
            _url += "diagnosa_irj/" + tanggal.ToString("M-YYYY");

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(json)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string InsertDiagnosaTerbesarIri(List<DiagnosaTerbesar.Json> json, DateTime tanggal)
        {
            _url += "diagnosa_iri/" + tanggal.ToString("M-YYYY");

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(json)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string InsertIndikatorPelayanan(IndikatorPelayanan.Json json, DateTime tanggal)
        {
            _url += "bor/" + tanggal.ToString("M-YYYY");

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(json)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }
    }
}
