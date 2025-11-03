using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common.LinkLis
{
    public class Service
    {
        private string _url = "https://rs-demo.linklis.com/api/";
        private string _feedback = "http://192.168.2.100:8090/api/LinkLis/Insert";

        public Service()
        {
            var ws = new WebServiceAccessKey();
            if (ws.LoadByPrimaryKey(AppSession.Parameter.LisInterop)) _url = ws.RequestUrl;
        }

        public Reference.KodePemeriksaan.Root GetKodePemeriksaan()
        {
            using (HttpWebResponse response = Helper.PopulateWebRequest(string.Format("{0}{1}", _url, "pemeriksaan/no_reg"), Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Reference.KodePemeriksaan.Root>(sr.ReadToEnd());
            }
        }

        public Reference.Ruangan.Root GetRuangan()
        {
            using (HttpWebResponse response = Helper.PopulateWebRequest(string.Format("{0}{1}", _url, "fetch/ruangan"), Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Reference.Ruangan.Root>(sr.ReadToEnd());
            }
        }

        public Reference.Dokter.Root GetDokter()
        {
            using (HttpWebResponse response = Helper.PopulateWebRequest(string.Format("{0}{1}", _url, "fetch/dokter"), Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Reference.Dokter.Root>(sr.ReadToEnd());
            }
        }

        public Reference.Analis.Root GetAnalis()
        {
            using (HttpWebResponse response = Helper.PopulateWebRequest(string.Format("{0}{1}", _url, "fetch/analis"), Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Reference.Analis.Root>(sr.ReadToEnd());
            }
        }

        public Reference.DokterPK.Root GetDokterPK()
        {
            using (HttpWebResponse response = Helper.PopulateWebRequest(string.Format("{0}{1}", _url, "fetch/dokterpk"), Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Reference.DokterPK.Root>(sr.ReadToEnd());
            }
        }

        public Reference.Status.Root GetStatus()
        {
            using (HttpWebResponse response = Helper.PopulateWebRequest(string.Format("{0}{1}", _url, "fetch/status"), Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Reference.Status.Root>(sr.ReadToEnd());
            }
        }

        public Reference.ListPemeriksaan.Root GetListPemeriksaan()
        {
            using (HttpWebResponse response = Helper.PopulateWebRequest(string.Format("{0}{1}", _url, "fetch/list_pemeriksaan"), Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Reference.ListPemeriksaan.Root>(sr.ReadToEnd());
            }
        }

        public Reference.ListParameter.Root GetListParameter(string list_pemeriksaan)
        {
            using (HttpWebResponse response = Helper.PopulateWebRequest(string.Format("{0}{1}", _url, "fetch/list_parameter/" + list_pemeriksaan), Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, string.Empty).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Reference.ListParameter.Root>(sr.ReadToEnd());
            }
        }

        public Reference.Response.Root InsertRegistrasiPasien(Object.RegistrasiPasien registrasi, bool isInsert)
        {
            var param = string.Concat(new string[]
                {
                    "no_rm=",registrasi.no_rm,
                    "&nama=",registrasi.nama,
                    "&alamat=",registrasi.alamat,
                    "&tgl_lahir=",registrasi.tgl_lahir,
                    "&jenis_kelamin=",registrasi.jenis_kelamin,
                    "&status=",registrasi.status
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Helper.PopulateWebRequest(string.Format("{0}{1}", _url, isInsert ? "pasien/register" : "pasien/register/" + registrasi.no_rm), Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                var str = sr.ReadToEnd();

                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = string.Empty,
                    UrlAddress = _url,
                    Params = param,
                    Response = str,
                    Totalms = 0
                };
                log.Save();

                return JsonConvert.DeserializeObject<Reference.Response.Root>(str);
            }
        }

        public Reference.Response.Root InsertRegistrasiPemeriksaan(Object.RegistrasiPemeriksaan registrasi, bool isInsert)
        {
            var param = string.Concat(new string[]
                {
                    "kode_pemeriksaan=",registrasi.kode_pemeriksaan,
                    "&no_rm=",registrasi.no_rm,
                    "&id_ruangan=",registrasi.id_ruangan,
                    "&id_dokter=",registrasi.id_dokter,
                    "&id_analis=",registrasi.id_analis,
                    "&id_dokterpk=",registrasi.id_dokterpk,
                    "&id_status=",registrasi.id_status
                });

            var sb = new StringBuilder();
            sb.Append(param);

            using (HttpWebResponse response = Helper.PopulateWebRequest(string.Format("{0}{1}", _url, isInsert ? "pemeriksaan/register" : "pemeriksaan/register/" + registrasi.no_rm), Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                var str = sr.ReadToEnd();

                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = string.Empty,
                    UrlAddress = _url,
                    Params = param,
                    Response = str,
                    Totalms = 0
                };
                log.Save();

                return JsonConvert.DeserializeObject<Reference.Response.Root>(str);
            }
        }

        public Reference.Response.Root InsertRegistrasiParameterPemeriksaan(Object.ParameterPemeriksaan parameter)
        {
            var kode_pemeriksaan = string.Concat(new string[] { "kode_pemeriksaan=", parameter.kode_pemeriksaan });

            var list_pemeriksaan = string.Empty;
            var list_parameter = string.Empty;

            for (int i = 0; i < parameter.list_pemeriksaan.Count; i++)
            {
                list_pemeriksaan += string.Concat(new string[] { string.Format("&list_pemeriksaan[{0}]=", i.ToString()), parameter.list_pemeriksaan[i].list_pemeriksaan });
                //int idx = 0;
                foreach (var e in parameter.list_parameter.Where(p => p.list_pemeriksaan == parameter.list_pemeriksaan[i].list_pemeriksaan))
                {
                    var lp = GetListParameter(e.list_pemeriksaan);
                    var idx = lp.ListParameter.Select((v, n) => new { entity = v, index = n }).First(l => l.entity.Kode == e.list_parameter).index;
                    //var idx = lp.ListParameter.Where(l => l.Kode == e.list_parameter).Select((l, index) => index).SingleOrDefault();

                    list_parameter += string.Concat(new string[] { string.Format("&kode[{0}][{1}]=", e.list_pemeriksaan, idx.ToString()), e.list_parameter });
                    //idx++;
                }
            }

            var ws = new WebServiceAccessKey();
            if (ws.LoadByPrimaryKey(AppSession.Parameter.LisInterop)) _feedback = ws.ResponseUrl;

            var feedback = string.Concat(new string[]
            {
                "&feedback_id=simrs",
                "&feedback_url=",_feedback
            });

            var param = kode_pemeriksaan + list_pemeriksaan + list_parameter + feedback;
            var sb = new StringBuilder();
            sb.Append(param);

            //param = param.Replace("=", "\":\"");
            //param = param.Replace("&", "\",\"");
            //param = "{\"" + param + "\"}";

            var log = new WebServiceAPILog();

            log = new WebServiceAPILog
            {
                DateRequest = DateTime.Now,
                IPAddress = string.Empty,
                UrlAddress = _url,
                Params = param,
                Response = string.Empty,
                Totalms = 0
            };
            log.Save();

            using (HttpWebResponse response = Helper.PopulateWebRequest(string.Format("{0}{1}", _url, "parameter/register"), Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    log = new WebServiceAPILog
                    {
                        DateRequest = DateTime.Now,
                        IPAddress = string.Empty,
                        UrlAddress = _url,
                        Params = param,
                        Response = String.Format("HTTP {0}: {1}", response.StatusCode, response.StatusDescription),
                        Totalms = 0
                    };
                    log.Save();

                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
                }

                var sr = new StreamReader(response.GetResponseStream());
                var str = sr.ReadToEnd();

                log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = string.Empty,
                    UrlAddress = _url,
                    Params = param,
                    Response = str,
                    Totalms = 0
                };
                log.Save();

                return JsonConvert.DeserializeObject<Reference.Response.Root>(str);
            }
        }
    }
}
