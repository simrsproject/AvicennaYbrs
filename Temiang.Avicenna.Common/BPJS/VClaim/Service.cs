using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using LZStringCSharp;

namespace Temiang.Avicenna.Common.BPJS.VClaim
{
    public class Service
    {
        private string _url = ConfigurationManager.AppSettings["BPJSServiceUrlLocation"];
        private string _consKey = ConfigurationManager.AppSettings["BPJSConsumerID"];
        private string _ppkKey = ConfigurationManager.AppSettings["BPJSHospitalID"];
        private string _salt = ConfigurationManager.AppSettings["BPJSSaltConsumerID"];
        private string _encrypted = ConfigurationManager.AppSettings["BPJSResponseIsEncrypted"];

        //private HttpWebRequest PopulateWebRequest(string url, Helper.WebRequestMethod method, Helper.WebRequestContentType contentType, string parameter)
        //{
        //    var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
        //    webrequest.Method = method.ToString();

        //    if (method != Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

        //    webrequest.Headers.Add("X-cons-id", _consKey);
        //    string stamp = BPJS.Helper.GetUnixTimeStamp();
        //    webrequest.Headers.Add("X-timestamp", stamp);
        //    webrequest.Headers.Add("X-signature", BPJS.Helper.GetEncodedKey(stamp, _consKey, _salt, false));

        //    if (method != Helper.WebRequestMethod.GET)
        //    {
        //        byte[] formData = Encoding.UTF8.GetBytes(parameter.ToString());
        //        webrequest.ContentLength = formData.Length;

        //        using (var post = webrequest.GetRequestStream())
        //        {
        //            post.Write(formData, 0, formData.Length);
        //        }
        //    }

        //    return webrequest;
        //}

        //private HttpWebRequest PopulateWebRequest(string url, Helper.WebRequestMethod method)
        //{
        //    HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
        //    webrequest.Method = method.ToString();

        //    webrequest.Headers.Add("X-cons-id", _consKey);
        //    string stamp = BPJS.Helper.GetUnixTimeStamp();
        //    webrequest.Headers.Add("X-timestamp", stamp);
        //    webrequest.Headers.Add("X-signature", BPJS.Helper.GetEncodedKey(stamp, _consKey, _salt, false));

        //    return webrequest;
        //}

        //public Referensi.Diagnosa.Result GetDiagnosa(string text)
        //{
        //    _url += "referensi/diagnosa/" + text;

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Referensi.Diagnosa.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Referensi.Poli.Result GetPoli(string text)
        //{
        //    _url += "referensi/poli/" + text;

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Referensi.Poli.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Referensi.Faskes.Result GetFaskes(string text, Enum.JenisFaskes jenisFaskes)
        //{
        //    _url += string.Format("referensi/faskes/{0}/{1}", text, jenisFaskes.ToString());

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Referensi.Faskes.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Referensi.Procedure.Result GetProcedure(string text)
        //{
        //    _url += "referensi/procedure/" + text;

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Referensi.Procedure.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Referensi.KelasRawat.Result GetKelasRawat()
        //{
        //    _url += "referensi/kelasrawat";

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Referensi.KelasRawat.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Referensi.Dokter.Result GetDokter(string text)
        //{
        //    _url += "referensi/dokter/" + text;

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Referensi.Dokter.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Referensi.Spesialistik.Result GetSpesialistik()
        //{
        //    _url += "referensi/spesialistik";

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Referensi.Spesialistik.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Referensi.RuangRawat.Result GetRuangRawat()
        //{
        //    _url += "referensi/ruangrawat";

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Referensi.RuangRawat.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Referensi.CaraKeluar.Result GetCaraKeluar()
        //{
        //    _url += "referensi/carakeluar";

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Referensi.CaraKeluar.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Referensi.PascaPulang.Result GetPascaPulang()
        //{
        //    _url += "referensi/pascapulang";

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Referensi.PascaPulang.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Peserta.Result GetPeserta(Enum.SearchPeserta searchPeserta, string text, DateTime tglSEP)
        //{
        //    _url += string.Format("Peserta/{0}/{1}/tglSEP/{2}", searchPeserta.ToString(), text, tglSEP.ToString("yyyy-MM-dd"));

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Peserta.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Sep.Insert.Feedback.Result Insert(Sep.Insert.TSep tsep)
        //{
        //    _url += "SEP/insert";

        //    var root = new Common.BPJS.VClaim.Sep.Insert.RootObject();
        //    root.request = new Common.BPJS.VClaim.Sep.Insert.Request { t_sep = tsep };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Sep.Insert.Feedback.Result>(sr.ReadToEnd());
        //    }
        //}

        //public Sep.Update.Feedback Update(Sep.Update.TSep tsep)
        //{
        //    _url += "Sep/Update";

        //    var root = new Common.BPJS.VClaim.Sep.Update.RootObject();
        //    root.request = new Common.BPJS.VClaim.Sep.Update.Request { t_sep = tsep };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Sep.Update.Feedback>(sr.ReadToEnd());
        //    }
        //}

        //public Sep.Delete.Feedback Delete(Sep.Delete.TSep tsep)
        //{
        //    _url += "SEP/Delete";

        //    var root = new Common.BPJS.VClaim.Sep.Delete.RootObject();
        //    root.request = new Common.BPJS.VClaim.Sep.Delete.Request { t_sep = tsep };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Sep.Delete.Feedback>(sr.ReadToEnd());
        //    }
        //}

        //public Sep.Search.Feedback GetSep(string text)
        //{
        //    _url += "SEP/" + text;

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Sep.Search.Feedback>(sr.ReadToEnd());
        //    }
        //}

        //public Sep.Approve.Feedback Approve(Enum.ApproveType approveType, Sep.Approve.TSep tsep)
        //{
        //    _url += string.Format("Sep/{0}", approveType.ToString());

        //    var root = new Common.BPJS.VClaim.Sep.Approve.RootObject();
        //    root.request = new Common.BPJS.VClaim.Sep.Approve.Request { t_sep = tsep };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Sep.Approve.Feedback>(sr.ReadToEnd());
        //    }
        //}

        //public Sep.UpdateTglPulang.Feedback UpdateTglPulang(Sep.UpdateTglPulang.TSep tsep)
        //{
        //    _url += "Sep/updtglplg";

        //    var root = new Common.BPJS.VClaim.Sep.UpdateTglPulang.RootObject();
        //    root.request = new Common.BPJS.VClaim.Sep.UpdateTglPulang.Request { t_sep = tsep };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Sep.UpdateTglPulang.Feedback>(sr.ReadToEnd());
        //    }
        //}

        //public Klaim.Insert.Response Insert(Klaim.Insert.TLpk tlpk)
        //{
        //    _url += "LPK/insert";

        //    var root = new Common.BPJS.VClaim.Klaim.Insert.RootObject();
        //    root.request = new Common.BPJS.VClaim.Klaim.Insert.Request { t_lpk = tlpk };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Klaim.Insert.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Klaim.Update.Response Update(Klaim.Update.TLpk tlpk)
        //{
        //    _url += "LPK/update";

        //    var root = new Common.BPJS.VClaim.Klaim.Update.RootObject();
        //    root.request = new Common.BPJS.VClaim.Klaim.Update.Request { t_lpk = tlpk };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Klaim.Update.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Klaim.Delete.Response Delete(Klaim.Delete.TLpk tlpk)
        //{
        //    _url += "LPK/delete";

        //    var root = new Common.BPJS.VClaim.Klaim.Delete.RootObject();
        //    root.request = new Common.BPJS.VClaim.Klaim.Delete.Request { t_lpk = tlpk };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Klaim.Delete.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Klaim.Search.Response GetKlaim(DateTime tglMasuk, Enum.JenisPelayanan jenisPelayanan)
        //{
        //    _url += string.Format("LPK/TglMasuk/{0}/JnsPelayanan/{1}", tglMasuk.ToString("yyyy-MM-dd"), jenisPelayanan.ToString());

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Klaim.Search.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Monitoring.DataKunjungan.Response GetMonitoring(DateTime tglMasuk, Enum.JenisPelayanan jenisPelayanan)
        //{
        //    _url += string.Format("Monitoring/Kunjungan/Tanggal/{0}/JnsPelayanan/{1}", tglMasuk.ToString("yyyy-MM-dd"), jenisPelayanan.ToString());

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Monitoring.DataKunjungan.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Monitoring.DataKlaim.Response GetMonitoring(DateTime tglMasuk, Enum.JenisPelayanan jenisPelayanan, Enum.StatusKlaim statusKlaim)
        //{
        //    _url += string.Format("Monitoring/Klaim/Tanggal/{0}/JnsPelayanan/{1}/Status/{2}", tglMasuk.ToString("yyyy-MM-dd"), jenisPelayanan.ToString(), statusKlaim.ToString());

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Monitoring.DataKlaim.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Rujukan.Insert.Response Insert(Rujukan.Insert.TRujukan trujukan)
        //{
        //    _url += "Rujukan/insert";

        //    var root = new Common.BPJS.VClaim.Rujukan.Insert.RootObject();
        //    root.request = new Common.BPJS.VClaim.Rujukan.Insert.Request { t_rujukan = trujukan };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Rujukan.Insert.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Rujukan.Update.Response Update(Rujukan.Update.TRujukan trujukan)
        //{
        //    _url += "Rujukan/update";

        //    var root = new Common.BPJS.VClaim.Rujukan.Update.RootObject();
        //    root.request = new Common.BPJS.VClaim.Rujukan.Update.Request { t_rujukan = trujukan };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Rujukan.Update.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Rujukan.Update.Response Delete(Rujukan.Update.TRujukan trujukan)
        //{
        //    _url += "Rujukan/delete";

        //    var root = new Common.BPJS.VClaim.Rujukan.Update.RootObject();
        //    root.request = new Common.BPJS.VClaim.Rujukan.Update.Request { t_rujukan = trujukan };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return fastJSON.JSON.ToObject<Rujukan.Update.Response>(sr.ReadToEnd());
        //    }
        //}

        public Rujukan.Search.Result GetRujukan(string text, Enum.SearchRujukan searchRujukan)
        {
            if (searchRujukan == Enum.SearchRujukan.NoRujukan) _url += string.Format("Rujukan/RS/{0}", text);
            else _url += string.Format("Rujukan/RS/Peserta/{0}", text);

            using (var response = new v11.Service().PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return fastJSON.JSON.ToObject<Rujukan.Search.Result>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(new v11.Service().DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Rujukan.Search.Result
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<Rujukan.Search.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return null;
                }
            }
        }
    }
}
