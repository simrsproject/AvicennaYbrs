using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using Temiang.Avicenna.BusinessObject;
using Newtonsoft.Json;
using System.Configuration;

namespace Temiang.Avicenna.Common
{
    public class BPJS
    {
        private string _url = ConfigurationManager.AppSettings["BPJSServiceUrlLocation"];

        private string GetEncodedKey()
        {
            string consKey = ConfigurationManager.AppSettings["BPJSConsumerID"];
            string salt = "avicenna";

            // Initialize the keyed hash object using the secret key as the key
            HMACSHA256 hashObject = new HMACSHA256(Encoding.UTF8.GetBytes(consKey));

            // Computes the signature by hashing the salt with the secret key as
            var signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(salt));

            // Base 64 Encode
            return Convert.ToBase64String(signature);
        }

        private string GetUnixTimeStamp()
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = DateTime.Now.ToUniversalTime() - dateStart;
            return Math.Floor(diff.TotalSeconds).ToString();
        }

        public void GetPatientByMemberNo(string memberNo)
        {
            _url += "WSLokalRest/Peserta/peserta/" + memberNo;

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void GetPatientBySSN(string ssnNo)
        {
            _url += "WSLokalRest/Peserta/nik/" + ssnNo;

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void GetPatientHistoryByMemberNo(string memberNo)
        {
            _url += "/WSLokalRest/SEP/sep/peserta/" + memberNo;

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void GetRefferalByReferralNo(string refferalNo)
        {
            _url += "WSLokalRest/Rujukan/rujukan/" + refferalNo;

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void GetRefferalByDate(DateTime dateTime, int startRecordData, int endRecordData)
        {
            _url += string.Format("/WSLokalRest/Rujukan/rujukan/tglrujuk/{0}/query?start={1}&limit={2}", dateTime.ToString("yyyy-MM-dd"), startRecordData.ToString(), endRecordData.ToString());

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void GetRefferalByMemberNo(string memberNo)
        {
            _url += "WSLokalRest/Rujukan/rujukanrs/peserta/nokartu/" + memberNo;

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void CreateSEP(string memberNo, DateTime registrationDate, DateTime refferalDate, string refferalNo, string ppkRefferal,
            string ppkPelayanan, string serviceType, string notes, string initialDiagnose, string serviceUnitID,
            string chargeClass, string incidentCode, string userID, string medicalNo)
        {
            _url += "WSLokalRest/SEP/sep";

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "POST";
            webrequest.ContentType = "Application/x‐www‐form‐urlencoded";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            string request = string.Format(@"<request>
<data>
<t_sep>
<noKartu>{0}</noKartu>
<tglSep>{1}</tglSep>
<tglRujukan>{2}</tglRujukan>
<noRujukan>{3}</noRujukan>
<ppkRujukan>{4}</ppkRujukan>
<ppkPelayanan>{5}</ppkPelayanan>
<jnsPelayanan>{6}</jnsPelayanan>
<catatan>{7}</catatan>
<diagAwal>{8}</diagAwal>
<poliTujuan>{9}</poliTujuan>
<klsRawat>{10}</klsRawat>
<lakaLantas>{11}</lakaLantas>
<user>{12}</user>
<noMr>{13}</noMr>
</t_sep>
</data>
</request>", memberNo, registrationDate.ToString("yyyy-MM-dd HH:mm:ss"), refferalDate.ToString("yyyy-MM-dd HH:mm:ss"), refferalNo,
           ppkRefferal, ppkPelayanan, serviceType, notes, initialDiagnose, serviceUnitID, chargeClass,
           incidentCode, userID, medicalNo);

            byte[] formData = UTF8Encoding.UTF8.GetBytes(request.ToString());
            webrequest.ContentLength = formData.Length;

            using (Stream post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void UpdateDischargeDate(string sepNo, DateTime dischargeDate, string ppkPelayanan)
        {
            _url += "WSLokalRest/Sep/Sep/updtglplg";

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "PUT";
            webrequest.ContentType = "Application/x‐www‐form‐urlencoded";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            string request = string.Format(@"<request
<data
    <t_sep>
    <noSep>{0}</noSep>
    <tglPlg>>{1}</tglPlg>
    <ppkPelayanan>{2}</ppkPelayanan>
    </t_sep>
</data>
</request>", sepNo, dischargeDate.ToString("yyyy-MM-dd HH:mm:ss"), ppkPelayanan);

            byte[] formData = UTF8Encoding.UTF8.GetBytes(request.ToString());
            webrequest.ContentLength = formData.Length;

            using (Stream post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void GetDetailSEP(string sepNo)
        {
            _url += "WSLokalRest/SEP/sep/" + sepNo;

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void DeleteSEP(string sepNo, string ppkPelayanan)
        {
            _url += "WSLokalRest/SEP/sep";

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "DELETE";
            webrequest.ContentType = "Application/x‐www‐form‐urlencoded";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            string request = string.Format(@"<request>
<data>
    <t_sep>
    <noSep>{0}</noSep>
    <ppkPelayanan>{1}</ppkPelayanan>
    </t_sep>
</data>
</request>", sepNo, ppkPelayanan);

            byte[] formData = UTF8Encoding.UTF8.GetBytes(request.ToString());
            webrequest.ContentLength = formData.Length;

            using (Stream post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void MappingSEP(string sepNo, string registrationNo, string noTrans, string ppkPelayanan)
        {
            _url += "WSLokalRest/SEP/sep/map/trans";

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "POST";
            webrequest.ContentType = "Application/x‐www‐form‐urlencoded";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            string request = string.Format(@"<request>
<data>
   <t_map_sep>
     <noSep>{0}</noSep>
     <noTrans>{1}</noTrans>
     <ppkPelayanan>{2}</ppkPelayanan>
   </t_map_sep>
</data>
</request>", sepNo, noTrans, ppkPelayanan);

            byte[] formData = UTF8Encoding.UTF8.GetBytes(request.ToString());
            webrequest.ContentLength = formData.Length;

            using (Stream post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void GetDiagnoseData(string searchFilter)
        {
            _url += "/WSLokalRest/diagnosa/cbg/diagnosa/" + searchFilter;

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void GetProcedureData(string searchFilter)
        {
            _url += "WSLokalRest/prosedur/cbg/" + searchFilter;

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void GetTreatmentData(string searchFilter)
        {
            _url += "WSLokalRest/prosedur/cmg/" + searchFilter;

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        private void CreateInacbgGroupper(bool isFinalize, string medicalNo, string paymentType, string sepNo, string serviceType,
            string chargeClass, DateTime registrationDate, DateTime dischargeDate, string dischargeMethod,
            string paramedicName, decimal bornWeight, decimal tariffPrice, string refferalNotes, string treatmentCase,
            string adl, string memberNo, string patientName, string sex, DateTime birthDate, string procedureCMG,
            string drugsCMG, string investigationCMG, string prothesisCMG, string diagnoseCode1, string diagnoseName1,
            string diagnoseCode2, string diagnoseName2, string procedureCode1, string procedureName1,
            string procedureCode2, string procedureName2)
        {
            _url += isFinalize ? "WSLokalRest/gruper/grouper/save" : "WSLokalRest/gruper/grouper";

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "POST";
            webrequest.ContentType = "Application/x‐www‐form‐urlencoded";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            string request = string.Format(@"
{   ""request"":
{
""noMR"": ""{0}"",
""jnsBayar"": ""{1}"",
""noSep"": ""{2}"",
""jnsPerawatan"": ""{3}"",
""klsPerawatan"": ""{4}"",
""tglMasuk"": ""{5}"",
""tglPulang"": ""{6}"",
""caraPulang"": ""{7}"",
""namaDokter"": ""{8}"",
""beratLahir"": ""{9}"",
""tarifRS"": ""{10}"",
""suratRujukan"": ""{11}"",
""kasusPerawatan"": ""{12}"",
""adl"": ""{13}"",
""peserta"":
{
""noKartu"": ""{14}"",
""namaPeserta"": ""{15}"",
""sex"": ""{16}"",
""tglLahir"": ""{17}""
},
""cmg"":
{
""Procedure"":""{18}"",
""Drugs"":""{19}"",
""Investigation"":""{20}"",
""Prosthesis"":""{21}""
},
""diagnosa"": [
{
""kodeDiagnosa"": ""{22}"",
""namaDiagnosa"": ""{23}"",
""Level"": ""1""
},
{
""kodeDiagnosa"": ""{24}"",
""namaDiagnosa"": ""{25}"",
""Level"": ""2""
},
],
""prosedur"": [
{
""kodeProsedur"": ""{26}"",
""namaProsedur"": ""{27}""
},
{
""kodeProsedur"": ""{28}"",
""namaProsedur"": ""{29}""
},
]
}
}", medicalNo, paymentType, sepNo, serviceType, chargeClass, registrationDate.ToString("yyyy-MM-dd"),
    dischargeDate.ToString("yyyy-MM-dd"), dischargeMethod, paramedicName, bornWeight.ToString(), tariffPrice.ToString(), refferalNotes,
    treatmentCase, adl, memberNo, patientName, sex, birthDate.ToString("yyyy-MM-dd"), procedureCMG, drugsCMG, investigationCMG, prothesisCMG,
    diagnoseCode1, diagnoseName1, diagnoseCode2, diagnoseName2, procedureCode1, procedureName1, procedureCode2, procedureName2);

            byte[] formData = UTF8Encoding.UTF8.GetBytes(request.ToString());
            webrequest.ContentLength = formData.Length;

            using (Stream post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void GetClaimVerification(DateTime registrationDate, DateTime dischargeDate, string serviceClass,
            string serviceType, BPJS_SearchType searchType, string claimStatus)
        {
            _url += string.Format("WSLokalRest/sep/integrated/Kunjungan/tglMasuk/{0}/tglKeluar/{1}/KlsRawat/{2}/Kasus/{3}/Cari/{4}/status/{5}",
                registrationDate.ToString("yyyy-MM-dd"), dischargeDate.ToString("yyyy-MM-dd"), serviceClass, serviceType, searchType.ToString(),
                claimStatus);

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }

        public void GetSEPReport(string sepNo)
        {
            _url += "/WSLokalRest/sep/integrated/Kunjungan/sep/" + sepNo;

            var hc = new Healthcare();
            hc.Query.es.Top = 1;
            hc.Query.Load();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = "GET";
            
            webrequest.Headers.Add("X-cons-id", hc.HospitalCode);
            webrequest.Headers.Add("X-timestamp", GetUnixTimeStamp());
            webrequest.Headers.Add("X-signature", GetEncodedKey());

            using (HttpWebResponse response = webrequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                StreamReader sr = new StreamReader(response.GetResponseStream());
                object objResponse = JsonConvert.DeserializeObject(sr.ReadToEnd());
            }
        }
    }

    //public enum BPJS_JenisPelayanan
    //{
    //    Rawat_Inap = 1,
    //    Rawat_Jalan = 2
    //}

    //public enum BPJS_KelasPerawatan
    //{
    //    Kelas_I = 1,
    //    Kelas_II = 2,
    //    Kelas_III = 3
    //}

    //public enum BPJS_KodeLakaLantas
    //{
    //    Kecelakaan_Lalu_Lintas = 1,
    //    Bukan_Kecelakaan_Lalu_Lintas = 2
    //}

    public enum BPJS_SearchType
    {
        Tanggal_Masuk = 0,
        Tanggal_Keluar = 1
    }

    //public enum BPJS_ClaimStatus
    //{
    //    Klaim_Baru = 0,
    //    Klaim_Terima_CBG = 10,
    //    Klaim_Layak = 21,
    //    Klaim_Tidak_Layak = 22,
    //    Klaim_Pending = 23,
    //    Terverifikasi = 24,
    //    Proses_Cabang = 40
    //}
}
