using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;

namespace Temiang.Avicenna.Common.BPJS.Applicare
{
    public class Service
    {
        private string _url = ConfigurationManager.AppSettings["ApplicareServiceUrlLocation"];
        private string _consKey = ConfigurationManager.AppSettings["ApplicareConsumerID"];
        private string _ppkKey = ConfigurationManager.AppSettings["ApplicareHospitalID"];
        private string _salt = ConfigurationManager.AppSettings["ApplicareSaltConsumerID"];

        private HttpWebRequest PopulateWebRequest(string url, Helper.WebRequestMethod method, Helper.WebRequestContentType contentType, string parameter)
        {
            Helper.IgnoreBadCertificates();

            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = method.ToString();

            if (method != Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

            webrequest.Headers.Add("X-cons-id", _consKey);
            string stamp = BPJS.Helper.GetUnixTimeStamp();

            webrequest.Headers.Add("X-timestamp", stamp);
            webrequest.Headers.Add("X-signature", BPJS.Helper.GetEncodedKey(stamp, _consKey, _salt, false));

            if (method != Helper.WebRequestMethod.GET)
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

        private HttpWebRequest PopulateWebRequest(string url, Helper.WebRequestMethod method)
        {
            Helper.IgnoreBadCertificates();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = method.ToString();

            webrequest.Headers.Add("X-cons-id", _consKey);
            string stamp = BPJS.Helper.GetUnixTimeStamp();
            webrequest.Headers.Add("X-timestamp", stamp);
            webrequest.Headers.Add("X-signature", BPJS.Helper.GetEncodedKey(stamp, _consKey, _salt, false));

            return webrequest;
        }

        public string InsertRuangan(Common.BPJS.Applicare.RuanganBaru.RootObject root)
        {
            _url += "rest/bed/create/" + _ppkKey;

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.JSON, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string UpdateRuangan(Common.BPJS.Applicare.UpdateKetersediaanTempatTidur.RootObject root)
        {
            _url += "rest/bed/update/" + _ppkKey;

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.JSON, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string DeleteRuangan(Common.BPJS.Applicare.HapusRuangan.RootObject root)
        {
            _url += "rest/bed/delete/" + _ppkKey;

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.JSON, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public string ReadRuangan(int resultCount)
        {
            _url += "rest/bed/read/" + _ppkKey + "/1/" + resultCount.ToString();

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public Applicare.ReferensiKelas.Kelas ReadReferensiKelas()
        {
            _url += "rest/ref/kelas";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return fastJSON.JSON.ToObject<Applicare.ReferensiKelas.Kelas>(sr.ReadToEnd());
            }
        }
    }
}
