using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Temiang.Avicenna.Common.BPJS.VClaim.v11;

namespace Temiang.Avicenna.Common.SirsKemkes
{
    public class Service
    {
        private string _url = ConfigurationManager.AppSettings["SirsKemkesServiceUrlLocation"];
        private string _cid = ConfigurationManager.AppSettings["SirsKemkesConsumerID"];
        private string _key = ConfigurationManager.AppSettings["SirsKemkesSaltConsumerID"];
        private string _url2 = ConfigurationManager.AppSettings["SirsKemkesLaporanKematianServiceUrlLocation"];

        private string _urlSitb = ConfigurationManager.AppSettings["SitbServiceUrlLocation"];
        private string _cidSitb = ConfigurationManager.AppSettings["SitbConsumerID"];
        private string _keySitb = ConfigurationManager.AppSettings["SitbSaltConsumerID"];
        private string _hospitalSitb = ConfigurationManager.AppSettings["SitbHospitalID"];

        private HttpWebRequest PopulateWebRequest(string url, BPJS.Helper.WebRequestMethod method, BPJS.Helper.WebRequestContentType contentType, string parameter)
        {
            BPJS.Helper.IgnoreBadCertificates();

            var webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = method.ToString();

            if (method != BPJS.Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

            webrequest.Headers.Add("X-rs-id", _cid);
            string stamp = BPJS.Helper.GetUnixTimeStamp();
            webrequest.Headers.Add("X-Timestamp", stamp);
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

        private HttpWebRequest PopulateWebRequestSitb(string url, BPJS.Helper.WebRequestMethod method, BPJS.Helper.WebRequestContentType contentType, string parameter)
        {
            BPJS.Helper.IgnoreBadCertificates();

            var webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = method.ToString();

            if (method != BPJS.Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

            webrequest.Headers.Add("X-rs-id", _cidSitb);
            string stamp = BPJS.Helper.GetUnixTimeStamp();
            webrequest.Headers.Add("X-Timestamp", stamp);
            webrequest.Headers.Add("X-pass", _keySitb);

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

        private HttpWebRequest PopulateWebRequestLogin(string url, BPJS.Helper.WebRequestMethod method, BPJS.Helper.WebRequestContentType contentType, string parameter)
        {
            BPJS.Helper.IgnoreBadCertificates();

            var webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = method.ToString();

            if (method != BPJS.Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

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

        private HttpWebRequest PopulateWebRequestToken(string url, BPJS.Helper.WebRequestMethod method, BPJS.Helper.WebRequestContentType contentType, string token, string parameter)
        {
            BPJS.Helper.IgnoreBadCertificates();

            var webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = method.ToString();
            webrequest.Headers.Add("Authorization", $"Bearer {token}");

            if (new[] { BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestMethod.PATCH }.Contains(method))
            {
                webrequest.ContentType = contentType.ToString();

                byte[] formData = Encoding.UTF8.GetBytes(parameter.ToString());
                webrequest.ContentLength = formData.Length;

                using (var post = webrequest.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }
            }

            return webrequest;
        }

        public EntryDataPasien.Response.RekapPasienMasuk.Root RekapPasienMasuk(EntryDataPasien.Request.RekapPasienMasuk data)
        {
            _url += "LapV2/PasienMasuk";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<EntryDataPasien.Response.RekapPasienMasuk.Root>(sr.ReadToEnd());
            }
        }

        public EntryDataPasien.Response.RekapPasienDirawatDenganKomorbid.Root RekapPasienDirawatDenganKomorbid(EntryDataPasien.Request.RekapPasienDirawatDenganKomorbid data)
        {
            _url += "LapV2/PasienDirawatKomorbid";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<EntryDataPasien.Response.RekapPasienDirawatDenganKomorbid.Root>(sr.ReadToEnd());
            }
        }

        public EntryDataPasien.Response.RekapPasienDirawatTanpaKomorbid.Root RekapPasienDirawatTanpaKomorbid(EntryDataPasien.Request.RekapPasienDirawatTanpaKomorbid data)
        {
            _url += "LapV2/PasienDirawatTanpaKomorbid";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<EntryDataPasien.Response.RekapPasienDirawatTanpaKomorbid.Root>(sr.ReadToEnd());
            }
        }

        public EntryDataPasien.Response.RekapPasienKeluar.Root RekapPasienKeluar(EntryDataPasien.Request.RekapPasienKeluar.Simpan data)
        {
            _url += "LapV2/PasienDirawatTanpaKomorbid";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<EntryDataPasien.Response.RekapPasienKeluar.Root>(sr.ReadToEnd());
            }
        }

        public string EntryDataRuangandanTempatTidur(EntryDataPasien.Request.EntryDataRuangandanTempatTidur data)
        {
            _url += "/Fasyankes";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(data)).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public Login.Json.Response.Root PostLogin()
        {
            _url2 += "api/rslogin";

            using (var response = PopulateWebRequestLogin(_url2, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(new Login.Json.Request.Root()
            {
                KodeRs = _cid,
                Password = _key
            })).GetResponse() as HttpWebResponse)
            {
                if (!new[] { HttpStatusCode.OK, HttpStatusCode.Created }.Contains(response.StatusCode)) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Login.Json.Response.Root>(sr.ReadToEnd());
            }
        }

        public Kelurahan.Json.Root GetKelurahan(string token, int page, int limit)
        {
            _url2 += $"api/kelurahan?page={page}&limit={limit}";

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.GET, BPJS.Helper.WebRequestContentType.JSON, token, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Kelurahan.Json.Root>(sr.ReadToEnd());
            }
        }

        public Kecamatan.Json.Root GetKecamatan(string token, int page, int limit)
        {
            _url2 += $"api/kecamatan?page={page}&limit={limit}";

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.GET, BPJS.Helper.WebRequestContentType.JSON, token, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Kecamatan.Json.Root>(sr.ReadToEnd());
            }
        }

        public KabKota.Json.Root GetKabKota(string token, int page, int limit)
        {
            _url2 += $"api/kabkota?page={page}&limit={limit}";

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.GET, BPJS.Helper.WebRequestContentType.JSON, token, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<KabKota.Json.Root>(sr.ReadToEnd());
            }
        }

        public Provinsi.Json.Root GetProvinsi(string token, int page, int limit)
        {
            _url2 += $"api/provinsi?page={page}&limit={limit}";

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.GET, BPJS.Helper.WebRequestContentType.JSON, token, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Provinsi.Json.Root>(sr.ReadToEnd());
            }
        }

        public LokasiKematian.Json.Root GetLokasiKematian(string token, int page, int limit)
        {
            _url2 += $"api/lokasikematian?page={page}&limit={limit}";

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.GET, BPJS.Helper.WebRequestContentType.JSON, token, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<LokasiKematian.Json.Root>(sr.ReadToEnd());
            }
        }

        public PenyebabKematianLangsung.Json.Root GetPenyebabKematianLangsung(string token, int page, int limit)
        {
            _url2 += $"api/penyebabkematianlangsung?page={page}&limit={limit}";

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.GET, BPJS.Helper.WebRequestContentType.JSON, token, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<PenyebabKematianLangsung.Json.Root>(sr.ReadToEnd());
            }
        }

        public KasusKematian.Json.Root GetKasusKematian(string token, int page, int limit)
        {
            _url2 += $"api/kasuskematian?page={page}&limit={limit}";

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.GET, BPJS.Helper.WebRequestContentType.JSON, token, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<KasusKematian.Json.Root>(sr.ReadToEnd());
            }
        }

        public Komorbid.Json.Root GetKomorbid(string token, int page, int limit)
        {
            _url2 += $"api/komorbid?page={page}&limit={limit}";

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.GET, BPJS.Helper.WebRequestContentType.JSON, token, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Komorbid.Json.Root>(sr.ReadToEnd());
            }
        }

        public LaporanKematian.Json.Insert.Response.Root PostInsert(string token, LaporanKematian.Json.Insert.Request.Root root)
        {
            _url2 += "api/kematian";

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, token, JsonConvert.SerializeObject(root)).GetResponse() as HttpWebResponse)
            {
                if (!new[] { HttpStatusCode.OK, HttpStatusCode.Created }.Contains(response.StatusCode)) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<LaporanKematian.Json.Insert.Response.Root>(sr.ReadToEnd());
            }
        }

        public LaporanKematian.Json.Update.Response.Root PostUpdate(string token, int id, LaporanKematian.Json.Update.Request.Root root)
        {
            _url2 += "api/kematian/" + id;

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.PATCH, BPJS.Helper.WebRequestContentType.JSON, token, JsonConvert.SerializeObject(root)).GetResponse() as HttpWebResponse)
            {
                if (!new[] { HttpStatusCode.OK, HttpStatusCode.Created }.Contains(response.StatusCode)) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<LaporanKematian.Json.Update.Response.Root>(sr.ReadToEnd());
            }
        }

        public LaporanKematian.Json.Delete.Response.Root PostDelete(string token, int id)
        {
            _url2 += "api/kematian/" + id;

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.DELETE, BPJS.Helper.WebRequestContentType.JSON, token, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (!new[] { HttpStatusCode.OK, HttpStatusCode.Created }.Contains(response.StatusCode)) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<LaporanKematian.Json.Delete.Response.Root>(sr.ReadToEnd());
            }
        }

        public LaporanKematian.Json.Select.Root GetSelect(string token, int page, int limit)
        {
            _url2 += $"api/kematian?page={page}&limit={limit}";

            using (var response = PopulateWebRequestToken(_url2, BPJS.Helper.WebRequestMethod.GET, BPJS.Helper.WebRequestContentType.JSON, token, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (!new[] { HttpStatusCode.OK, HttpStatusCode.Created }.Contains(response.StatusCode)) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<LaporanKematian.Json.Select.Root>(sr.ReadToEnd());
            }
        }

        public Tuberkulosis.Json.ResponseInsert PostInsertSitb(Tuberkulosis.Json.Request root)
        {
            using (var response = PopulateWebRequestSitb(_urlSitb, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(root)).GetResponse() as HttpWebResponse)
            {
                if (!new[] { HttpStatusCode.OK, HttpStatusCode.Created }.Contains(response.StatusCode)) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                var str = sr.ReadToEnd();
                if (str.Contains("jadwa")) str = str.Replace("jadwa", string.Empty);
                return JsonConvert.DeserializeObject<Tuberkulosis.Json.ResponseInsert>(str);
            }
        }

        public Tuberkulosis.Json.ResponseUpdate PostUpdateSitb(Tuberkulosis.Json.Request root)
        {
            using (var response = PopulateWebRequestSitb(_urlSitb, BPJS.Helper.WebRequestMethod.PUT, BPJS.Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(root)).GetResponse() as HttpWebResponse)
            {
                if (!new[] { HttpStatusCode.OK, HttpStatusCode.Created }.Contains(response.StatusCode)) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                var str = sr.ReadToEnd();
                if (str.Contains("jadwa")) str = str.Replace("jadwa", string.Empty);
                return JsonConvert.DeserializeObject<Tuberkulosis.Json.ResponseUpdate>(str);
            }
        }

        public string GetHeaderValueSitb()
        {
            var timeStamp = BPJS.Helper.GetUnixTimeStamp();

            var str = new JsonHeader
            {
                rs_id = _cidSitb,
                timestamp = timeStamp,
                pass = _keySitb
            };
            return JsonConvert.SerializeObject(str);
        }

        public class JsonHeader
        {
            [JsonProperty("X-rs-id")]
            public string rs_id { get; set; }
            [JsonProperty("X-Timestamp")]
            public string timestamp { get; set; }
            [JsonProperty("X-pass")]
            public string pass { get; set; }
        }
    }
}
