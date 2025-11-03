using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Net;
using System.Xml.Linq;
using Temiang.Avicenna.Common;
using System.Configuration;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.WebService.WSDL.JasaRaharja;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for JasaRaharjaClass
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class JasaRaharja : System.Web.Services.WebService
    {
        private string _url = ConfigurationManager.AppSettings["JasaRaharhaServiceUrlLocation"];
        private string _consumerID = ConfigurationManager.AppSettings["JasaRaharhaConsumerID"];

        private const string _service_path = "JasaRaharja/AppRumahSakit/Enterprise_Services/Proxy_Services/";
        private const string _service_path_surat_jaminan = "JasaRaharja/ECMS/Enterprise_Services/Proxy_Services/";

        private const string _path_insert_kejadian = "es_jrs_kejadian_insert_ps?wsdl";
        private const string _method_insert_kejadian = "jrs_kejadian_insert";

        private const string _path_query_kejadian = "es_jrs_kejadian_query_byrs?wsdl";
        private const string _method_query_kejadian = "jrs_kejadian_query_byrs";

        private const string _path_surat_jaminan = "es_ecms_query_byidregister?wsdl";

        private const string _path_upload_document = "es_jrs_kejadian_klaim_ps?wsdl";
        private const string _method_upload_document = "es_jrs_kejadian_klaim";

        private const string _result_sukses = "0000|SUKSES";
        private const string _tipe_id = "KTP";

        [WebMethod]
        public string HelloWorld()
        {
            return "Web Service is Online";
        }

        [WebMethod]
        public string SEND_KEJADIAN_RS(string nik, string nama_korban, string alamat, string no_telp, string jenis_kelamin, string umur,
            string tgl_kejadian, string tgl_masuk_rs, string lokasi_kejadian, string ruangan, string registration_no)
        {
            var kj = new JasaRaharjaClass();
            kj.KODE_KEJADIAN = registration_no.Replace("/", string.Empty);
            kj.NIK = nik;
            kj.NAMA_KORBAN = nama_korban;
            kj.ALAMAT = alamat;
            kj.NO_TELP = no_telp;
            kj.JENIS_KELAMIN = jenis_kelamin;
            kj.UMUR = umur;
            kj.TIPE_ID = _tipe_id;
            kj.TGL_KEJADIAN = tgl_kejadian;
            kj.TGL_MASUK_RS = tgl_masuk_rs;
            kj.LOKASI_KEJADIAN = lokasi_kejadian;
            kj.RUANGAN = ruangan;

            var param = CreateSoapEnvelope_InsertKejadian(kj);
            string response = GetWebResponse(_url + _service_path + _path_insert_kejadian, _method_insert_kejadian, param);

            var srl = new BusinessObject.Interop.JasaRaharja.SendReceiveLog();
            srl.es.Connection.Name = AppConstant.HIS_INTEROP.JASA_RAHAJA_INTEROP_CONNECTION_NAME;
            srl.OperationType = 0; //SEND
            srl.SendDateTime = DateTime.Now;
            srl.SendParameter = param.OuterXml;
            srl.ReceiveResult = response;
            srl.RegistrationNo = registration_no;
            srl.IsOperationSuccess = response.Contains(_result_sukses);
            srl.Save();

            return response;
        }

        public List<JasaRaharjaClass> GET_KEJADIAN_RS(string registration_no)
        {
            if (string.IsNullOrEmpty(registration_no)) return new List<JasaRaharjaClass>();

            var param = CreateSoapEnvelope_GetKejadian(registration_no.Replace("/", string.Empty));
            string response = GetWebResponse(_url + _service_path + _path_query_kejadian, _method_query_kejadian, param);

            // remove unused xml soap line
            response = response.Replace(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">", string.Empty);
            response = response.Replace(@"<soap-env:Body xmlns:soap-env=""http://schemas.xmlsoap.org/soap/envelope/"">", string.Empty);
            response = response.Replace(@"<es:jrs_kejadian_query_byrsResponse xmlns:es=""http://www.example.org/es_jrs_kejadian_query_byrs/"">", string.Empty);
            response = response.Replace(@"</es:jrs_kejadian_query_byrsResponse>", string.Empty);
            response = response.Replace(@"</soap-env:Body>", string.Empty);
            response = response.Replace(@"</soapenv:Envelope>", string.Empty);

            var xDoc = XDocument.Load(new StringReader(response));

            var list = new List<JasaRaharjaClass>();
            foreach (var item in xDoc.Elements("RESULT"))
            {
                var doc = GET_SURAT_JAMINAN(item.Element("ID_REGISTER").Value);
                WebService.WSDL.JasaRaharja.ContentResultType doc1 = null;
                var uploads = new List<WebService.JasaRaharjaUploadClass>();
                if (doc != null)
                {
                    doc1 = (from d in doc
                            where d.xComments == "61"
                            select d).Take(1).SingleOrDefault();

                    var doc2 = doc.Where(d => d.xComments != "61");
                    if (doc2 != null)
                    {
                        foreach (var d in doc2)
                        {
                            uploads.Add(new JasaRaharjaUploadClass()
                            {
                                NAMA_FILE = d.dDocTitle,
                                FILE_PATH = _url.Substring(0, _url.Length - 1) + d.dWebURL,
                                DESKRIPSI = GetDeskripsi(d.xComments)
                            });
                        }
                    }
                }

                var jrc = new JasaRaharjaClass();
                list.Add(new JasaRaharjaClass
                {
                    ID_REGISTER = item.Element("ID_REGISTER").Value,
                    NAMA_KORBAN = item.Element("NAMA_KORBAN").Value,
                    NIK = item.Element("NIK").Value,
                    TIPE_ID = item.Element("TIPE_ID").Value,
                    ALAMAT = item.Element("ALAMAT").Value,
                    NO_TELP = item.Element("NO_TELP").Value,
                    JENIS_KELAMIN = item.Element("JENIS_KELAMIN").Value,
                    UMUR = item.Element("UMUR").Value,
                    TGL_KEJADIAN = item.Element("TGL_KEJADIAN").Value,
                    KODE_RUMAH_SAKIT = item.Element("KODE_RUMAH_SAKIT").Value,
                    TGL_MASUK_RS = item.Element("TGL_MASUK_RS").Value,
                    RUANGAN = item.Element("RUANGAN").Value,
                    SIFAT_CEDERA = GetSifatCedera(item.Element("SIFAT_CEDERA").Value),
                    JENIS_TINDAKAN = item.Element("JENIS_TINDAKAN").Value,
                    DOKTER_BERWENANG = item.Element("DOKTER_BERWENANG").Value,
                    BIAYA = string.IsNullOrEmpty(item.Element("BIAYA").Value) ? 0 : decimal.Parse(item.Element("BIAYA").Value),
                    JUMLAH_DIBAYARKAN = string.IsNullOrEmpty(item.Element("JUMLAH_DIBAYARKAN").Value) ? 0 : decimal.Parse(item.Element("JUMLAH_DIBAYARKAN").Value),
                    TGL_PROSES = string.IsNullOrEmpty(item.Element("TGL_PROSES").Value) ? string.Empty : DateTime.Parse(item.Element("TGL_PROSES").Value).ToString("MM/dd/yyyy"),
                    STATUS_JAMINAN = GetStatusJaminan(string.IsNullOrEmpty(item.Element("STATUS_JAMINAN").Value) ? string.Empty : item.Element("STATUS_JAMINAN").Value),
                    STATUS_KLAIM = string.IsNullOrEmpty(item.Element("STATUS_KLAIM").Value) ? "false" : item.Element("STATUS_JAMINAN").Value == "1" ? "true" : "false",
                    NO_SURAT_JAMINAN = string.IsNullOrEmpty(item.Element("NO_SURAT_JAMINAN").Value) ? doc1 == null ? string.Empty : doc1.dDocName : item.Element("NO_SURAT_JAMINAN").Value,
                    KODE_KEJADIAN = registration_no,
                    PATH_SURAT_JAMINAN = doc1 == null ? string.Empty : _url.Substring(0, _url.Length - 1) + doc1.dWebURL,
                    UploadClass = uploads
                });
            }

            var srl = new BusinessObject.Interop.JasaRaharja.SendReceiveLog();
            srl.es.Connection.Name = AppConstant.HIS_INTEROP.JASA_RAHAJA_INTEROP_CONNECTION_NAME;
            srl.OperationType = 1; //RECEIVE
            srl.SendDateTime = DateTime.Now;
            srl.SendParameter = param.OuterXml;
            srl.ReceiveResult = list.Count.ToString();
            srl.RegistrationNo = registration_no;
            srl.IsOperationSuccess = list.Count > 0;
            srl.Save();

            return list;
        }

        public ContentResultType[] GET_SURAT_JAMINAN(string id_register)
        {
            es_ecms_query_byidregister es = new es_ecms_query_byidregister(_url + _service_path_surat_jaminan + _path_surat_jaminan);
            QueryResultType response = es.Calles_ecms_query_byidregister(id_register);
            if (response.ContentResult == null) return null;
            return response.ContentResult;
        }

        public bool UPLOAD_DOCUMENT(JasaRaharjaClass jr, string attachments)
        {
            var param = CreateSoapEnvelope_GetUploadBilling(jr, attachments);
            string response = GetWebResponse(_url + _service_path + _path_upload_document, _method_upload_document, param);

            var srl = new BusinessObject.Interop.JasaRaharja.SendReceiveLog();
            srl.es.Connection.Name = AppConstant.HIS_INTEROP.JASA_RAHAJA_INTEROP_CONNECTION_NAME;
            srl.OperationType = 2; //UPLOAD
            srl.SendDateTime = DateTime.Now;
            srl.SendParameter = param.OuterXml;
            srl.ReceiveResult = response;
            srl.RegistrationNo = jr.KODE_KEJADIAN;
            srl.IsOperationSuccess = string.IsNullOrEmpty(attachments) ? true : response.Contains(_result_sukses);
            srl.Save();

            return srl.IsOperationSuccess ?? false;
        }

        public static string GetWebResponse(string url, string method, XmlDocument xmlDocument)
        {
            HttpWebRequest webRequest = CreateWebRequest(url, method);
            InsertSoapEnvelopeIntoWebRequest(xmlDocument, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
            }
            return soapResult;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private XmlDocument CreateSoapEnvelope_InsertKejadian(JasaRaharjaClass jr)
        {
            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(string.Format(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:es=""http://www.example.org/es_jrs_kejadian_insert_wsdl/""> 
<soapenv:Header/> 
<soapenv:Body> 
<es:jrs_kejadian_insert> 
<KODE_RS>{0}</KODE_RS> 
<KODE_KEJADIAN>{1}</KODE_KEJADIAN> 
<NIK>{2}</NIK> 
<NAMA_KORBAN>{3}</NAMA_KORBAN> 
<ALAMAT>{4}</ALAMAT> 
<NO_TELP>{5}</NO_TELP> 
<JENIS_KELAMIN>{6}</JENIS_KELAMIN> 
<UMUR>{7}</UMUR> 
<TIPE_ID>{8}</TIPE_ID> 
<TGL_KEJADIAN>{9}</TGL_KEJADIAN> 
<TGL_MASUK_RS>{10}</TGL_MASUK_RS> 
<LOKASI_KEJADIAN>{11}</LOKASI_KEJADIAN> 
<RUANGAN>{12}</RUANGAN> 
</es:jrs_kejadian_insert> 
</soapenv:Body> 
</soapenv:Envelope>", _consumerID,
        jr.KODE_KEJADIAN,
        jr.NIK,
        jr.NAMA_KORBAN,
        jr.ALAMAT,
        jr.NO_TELP,
        jr.JENIS_KELAMIN,
        jr.UMUR,
        jr.TIPE_ID,
        jr.TGL_KEJADIAN,
        jr.TGL_MASUK_RS,
        jr.LOKASI_KEJADIAN,
        jr.RUANGAN));
            return soapEnvelop;
        }

        private XmlDocument CreateSoapEnvelope_GetKejadian(string registration_no)
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(string.Format(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:es=""http://www.example.org/es_jrs_kejadian_query_byrs/"">
<soapenv:Header/>
<soapenv:Body>
<es:jrs_kejadian_query_byrs>
<KODE_KEJADIAN>{0}</KODE_KEJADIAN>
<KODE_RS>{1}</KODE_RS>
</es:jrs_kejadian_query_byrs>
</soapenv:Body>
</soapenv:Envelope>", registration_no, _consumerID));
            return soapEnvelop;
        }

        private XmlDocument CreateSoapEnvelope_GetUploadBilling(JasaRaharjaClass jr, string attachments)
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(string.Format(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:es=""http://www.example.org/es_jrs_kejadian_klaim/"">
<soapenv:Header/>
<soapenv:Body>
<es:es_jrs_kejadian_klaim>
<ID_REGISTER>{0}</ID_REGISTER>
<SIFAT_CEDERA>{1}</SIFAT_CEDERA>
<JENIS_TINDAKAN>{2}</JENIS_TINDAKAN>
<DOKTER_BERWENANG>{3}</DOKTER_BERWENANG>
<BIAYA>{4}</BIAYA>
<process_file>
{5}
</process_file>
</es:es_jrs_kejadian_klaim>
</soapenv:Body>
</soapenv:Envelope>", jr.ID_REGISTER, jr.SIFAT_CEDERA, jr.JENIS_TINDAKAN, jr.DOKTER_BERWENANG, jr.BIAYA.ToString(), attachments));

            return soapEnvelop;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        public static string GetSifatCedera(string sifatCedera)
        {
            switch (sifatCedera)
            {
                case "01":
                    return "Meninggal Dunia";
                case "02":
                    return "Luka-luka";
                case "04":
                    return "Cacat Tetap";
                default:
                    return string.Empty;
            }
        }

        public static string GetDeskripsi(string deskripsi)
        {
            switch (deskripsi)
            {
                case "06":
                    return "KTP";
                case "09":
                    return "Kartu Keluarga";
                case "12":
                    return "Billing / Kwitansi";
                case "15":
                    return "Surat Kuasa";
                case "22":
                    return "Rekam Medis";
                case "14":
                    return "Surat Rujukan";
                case "16":
                    return "Foto Rontgen";
                case "19":
                    return "Surat Keterangan Cacat";
                default:
                    return string.Empty;
            }
        }

        public static string GetStatusJaminan(string statusJaminan)
        {
            switch (statusJaminan)
            {
                case "0":
                    return "Korban tidak terjamin UU No. 33/34 Tahun 1994";
                case "-1":
                    return "Korban terjamin UU No. 33/34 Tahun 1994 (Respon awal petugas)";
                case "-2":
                    return "Korban belum ditangani kepolisian (Respon awal petugas)";
                case "-3":
                    return "Korban tidak bersedia melapor ke polisi (Respon awal petugas)";
                case "1":
                    return "Korban terjamin UU No. 33/34 Tahun 1994 + Proses santunan kecelakaan";
                default:
                    return string.Empty;
            }
        }
    }

    [Serializable]
    public class JasaRaharjaClass
    {
        public string KODE_KEJADIAN { get; set; }
        public string NIK { get; set; }
        public string NAMA_KORBAN { get; set; }
        public string ALAMAT { get; set; }
        public string NO_TELP { get; set; }
        public string JENIS_KELAMIN { get; set; }
        public string UMUR { get; set; }
        public string TIPE_ID { get; set; }
        public string TGL_KEJADIAN { get; set; }
        public string TGL_MASUK_RS { get; set; }
        public string LOKASI_KEJADIAN { get; set; }
        public string RUANGAN { get; set; }
        public string ID_REGISTER { get; set; }
        public string SIFAT_CEDERA { get; set; }
        public string JENIS_TINDAKAN { get; set; }
        public string DOKTER_BERWENANG { get; set; }
        public decimal BIAYA { get; set; }
        public decimal JUMLAH_DIBAYARKAN { get; set; }
        public string TGL_PROSES { get; set; }
        public string STATUS_JAMINAN { get; set; }
        public string STATUS_KLAIM { get; set; }
        public string NO_SURAT_JAMINAN { get; set; }
        public string PATH_SURAT_JAMINAN { get; set; }
        public string KODE_RUMAH_SAKIT { get; set; }

        public List<JasaRaharjaUploadClass> UploadClass { get; set; }
    }

    [Serializable]
    public class JasaRaharjaUploadClass
    {
        public string ID { get; set; }
        public string ATTACHMENT { get; set; }
        public string NAMA_FILE { get; set; }
        public string DESKRIPSI { get; set; }
        public string DESKRIPSI_NAMA { get; set; }
        public DateTime SEND_DATETIME { get; set; }
        public string FILE_PATH { get; set; }
    }
}
