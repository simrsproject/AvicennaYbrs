using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.BusinessObject;
using Temiang.Avicenna.Bridging.PCare.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Bridging.PCare
{

    /// Versi 3.0
    /// Ref: https://dvlp.bpjs-kesehatan.go.id:9081/pcare-rest-v3.0/#p_pengguna
    /// 
    /// Perubahan url versi dari yang sebelumnya /v1 atau /v2 menjadi /pcare-rest-v3.0
    /// Response Add Pendaftaran : field noUrut dari 1 menjadi A1
    /// Parameter Add Kunjungan : penambahan field kdPoli
    /// Response Get Pendaftaran Provider : perubahan field noUrut dari Integer menjadi String
    /// URL Get Pendaftaran : perubahan no urut 1 menjadi A1
    /// Response Get Pendaftaran : perubahan field noUrut dari Integer menjadi String
    /// URL Delete Pendaftaran : perubahan no urut 1 menjadi A1
    /// Penambahan Service pada katalog Spesialis
    /// Update Service Add Kunjungan
    /// Update Service Edit Kunjungan
    /// Validasi TACC service Kunjungan
    /// Validasi Pemeriksaan Fisik service Kunjungan
    /// Penambahan rujukan kondisi khusus JIWA, KUSTA, TB-MDR, SARANA KEMOTERAPI, SARANA RADIOTERAPI, HIV-ODHA
    /// Perubahan rujukan kondisi khusus THALASEMIA dan HEMOFILI dengan subspesialis HEMATOLOGI - ONKOLOGI MEDIK dan ANAK HEMATOLOGI ONKOLOGI
    /// Penghapusan subspesialis 2 dan 3 pada Pencarian Faskes Rujukan Subspesialis
    /// Penambahan field kapasitas pada Pencarian Faskes Rujukan Subspesialis
    /// Referensi tindakan dibedakan rawat inap, rawat jalan, dan pomprev
    /// Parameter Pendaftaran ditambahkan field kdTkp sebagai pengganti rawatInap
    /// Penambahan Parameter tglEstRujuk pada Pencarian Faskes Rujukan Sub Spesialis dan Khusus
    /// Penambahan field persentase pada response Pencarian Faskes Rujukan Sub Spesialis dan Khusus
    /// Penambahan parameter kdPoli pada saat delete pendaftaran


    public class Utils
    {
        private readonly string _consumerID = ConfigurationManager.AppSettings["PCareConsumerID"];
        private readonly string _consumerSecretKey = ConfigurationManager.AppSettings["PCareConsumerSecretKey"];
        private readonly string _baseUrl = ConfigurationManager.AppSettings["PCareApiBaseUrl"];
        private readonly string _applicationID = ConfigurationManager.AppSettings["PCareApplicationID"];
        private readonly string _userID = ConfigurationManager.AppSettings["PCareUserID"];



        #region WebRequest Function
        private string GetXSignature(string data, string secretKey)
        {
            // Initialize the keyed hash object using the secretkey as the key
            HMACSHA256 hashObject = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));

            // Computes the signature by hashing the salt withthe secret key as the key
            var signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(data));

            // Base 64 Encode
            var encodedSignature = Convert.ToBase64String(signature);

            // URLEncode
            //encodedSignature = System.Web.HttpUtility.UrlEncode(encodedSignature);

            return encodedSignature;
        }

        private string GetUnixTimeStamp()
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = DateTime.Now.ToUniversalTime() - dateStart;
            return Math.Floor(diff.TotalSeconds).ToString();
        }


        private HttpWebRequest InitializedHttpWebRequestHeader(string serviceUrl)
        {
            var tstamp = GetUnixTimeStamp();
            var data = string.Format("{0}&{1}", _consumerID, tstamp);
            var signature = GetXSignature(data, _consumerSecretKey);
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(serviceUrl);
            webrequest.Headers.Add("X-cons-id", _consumerID);
            webrequest.Headers.Add("X-Timestamp", tstamp);
            webrequest.Headers.Add("X-Signature", signature);
            //webrequest.Headers.Add("X-Authorization", _authorization);

            // authorization
            byte[] authorization = Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}", _userID, AppParameter.GetParameterValue(AppParameter.ParameterItem.PCarePassword), _applicationID));
            webrequest.Headers.Add("X-Authorization", string.Format("Basic {0}", Convert.ToBase64String(authorization)));


            var timeOutPar = AppParameter.GetParameterValue(AppParameter.ParameterItem.PCareTimeOutInSecond);
            var timeOut = Convert.ToInt16(timeOutPar) * 1000;
            webrequest.Timeout = timeOut;

            return webrequest;
        }

        private string HttpWebRequestExecute(string serviceUrl, string postData, string method)
        {
            var readResult = string.Empty;
            try
            {
                var webrequest = InitializedHttpWebRequestHeader(serviceUrl);
                webrequest.Method = method;

                if (method == "POST" || method == "PUT")
                {
                    // Set the ContentType property of the WebRequest.
                    webrequest.ContentType = "application/json;";

                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                    // Set the ContentLength property of the WebRequest.
                    webrequest.ContentLength = byteArray.Length;

                    // Get the request stream.
                    var dataStream = webrequest.GetRequestStream();

                    // Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    // Close the Stream object.
                    dataStream.Close();
                }

                // Get the request stream.
                using (var response = webrequest.GetResponse() as HttpWebResponse)
                {
                    if (response != null && response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode,
                            response.StatusDescription));


                    if (response != null)
                    {
                        // Get the stream containing all content returned by the requested server.
                        var sr = new StreamReader(response.GetResponseStream());
                        readResult = sr.ReadToEnd();
                        sr.Close();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                readResult =
                    string.Format(
                        "HTTP_ERROR :: The second HttpWebRequest object has raised an Argument Exception as 'Connection' Property is set to 'Close' :: {0}",
                        ex.Message);
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    // Get the stream containing all content returned by the requested server.
                    var sr = new StreamReader(ex.Response.GetResponseStream());
                    readResult = sr.ReadToEnd();
                    sr.Close();
                }
                else
                    readResult = string.Format("HTTP_ERROR :: WebException raised! :: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                readResult = string.Format("HTTP_ERROR :: Exception raised! :: {0}", ex.Message);
            }
            return readResult;
        }


        private string HttpGet(string serviceUrl)
        {
            return HttpWebRequestExecute(serviceUrl, string.Empty, "GET");
        }
        private PostResponse HttpDelete(string serviceUrl)
        {
            var retval = new PostResponse();
            var readResult = HttpWebRequestExecute(serviceUrl, string.Empty, "DELETE");

            try
            {
                return JsonConvert.DeserializeObject<PostResponse>(readResult);
            }
            catch
            {
                retval.MetaData = new MetaData() { Code = "", Message = readResult };
            }

            return retval;
        }
        private PostResponse HttpPost(string serviceUrl, string postData)
        {
            var retval = new PostResponse();
            var readResult = HttpWebRequestExecute(serviceUrl, postData, "POST");

            try
            {
                return JsonConvert.DeserializeObject<PostResponse>(readResult);
            }
            catch
            {
                retval.MetaData = new MetaData() { Code = "", Message = readResult };
            }

            return retval;
        }
        private PostResponse HttpPut(string serviceUrl, string postData)
        {
            var retval = new PostResponse();

            var readResult = HttpWebRequestExecute(serviceUrl, postData, "PUT");

            try
            {
                return JsonConvert.DeserializeObject<PostResponse>(readResult);
            }
            catch
            {
                retval.MetaData = new MetaData() { Code = "", Message = readResult };
            }

            return retval;
        }
        private PostResponseMultiField HttpPostMultiFieldResult(string serviceUrl, string postData)
        {
            var retval = new PostResponseMultiField();
            var readResult = HttpWebRequestExecute(serviceUrl, postData, "POST");

            try
            {
                return JsonConvert.DeserializeObject<PostResponseMultiField>(readResult);
            }
            catch
            {
                retval.MetaData = new MetaData() { Code = "", Message = readResult };
            }

            return retval;
        }

        #endregion


        //public void PopulateSignature()
        //{
        //    var tstamp = GetUnixTimeStamp();
        //    var data = string.Format("{0}&{1}", _consumerID, tstamp);
        //    var signature = GetXSignature(data, _consumerSecretKey);
        //    Console.WriteLine("X-cons-id: {0}", _consumerID);
        //    Console.WriteLine("X-Timestamp: {0}", tstamp);
        //    Console.WriteLine("X-Signature: {0}", signature);
        //    Console.WriteLine("X-Authorization: {0}", _authorization);

        //}

        public PesertaGet Peserta(string memberNo)
        {
            PesertaGet pesertaResult = null;
            var serviceUrl = string.Concat(_baseUrl, "/peserta/", memberNo);
            var readResult = HttpGet(serviceUrl);
            try
            {
                pesertaResult = JsonConvert.DeserializeObject<PesertaGet>(readResult);
            }
            catch
            {
                throw new Exception(readResult);
            }
            return pesertaResult;
        }
        public PesertaGet PesertaByNik(string nik)
        {
            PesertaGet pesertaResult = null;
            var serviceUrl = string.Concat(_baseUrl, "/peserta/nik/", nik);
            var readResult = HttpGet(serviceUrl);
            try
            {
                pesertaResult = JsonConvert.DeserializeObject<PesertaGet>(readResult);
            }
            catch
            {
                throw new Exception(readResult);
            }
            return pesertaResult;
        }
        #region Reference Master
        public KesadaranGet KesadaranList()
        {
            KesadaranGet result = null;
            var serviceUrl = string.Concat(_baseUrl, "/kesadaran");
            var readResult = HttpGet(serviceUrl);
            result = JsonConvert.DeserializeObject<KesadaranGet>(readResult);
            return result;
        }

        public DiagnosaGet DiagnosaList(string keyword, string start, string limit)
        {
            DiagnosaGet result = null;
            var serviceUrl = string.Concat(_baseUrl, string.Format("/diagnosa/{0}/{1}/{2}", keyword, start, limit));
            var readResult = HttpGet(serviceUrl);
            result = JsonConvert.DeserializeObject<DiagnosaGet>(readResult);
            return result;
        }

        public DokterGet DokterList(string start, string limit)
        {
            DokterGet result = null;
            var serviceUrl = string.Concat(_baseUrl, string.Format("/dokter/{0}/{1}", start, limit));
            var readResult = HttpGet(serviceUrl);
            result = JsonConvert.DeserializeObject<DokterGet>(readResult);
            return result;
        }

        public ObatGet ObatList(string keyword, string start, string limit)
        {
            ObatGet result = null;
            var serviceUrl = string.Concat(_baseUrl, string.Format("/obat/dpho/{0}/{1}/{2}", keyword, start, limit));
            var readResult = HttpGet(serviceUrl);
            result = JsonConvert.DeserializeObject<ObatGet>(readResult);
            return result;
        }

        /// <summary>
        /// Fasilitas Kesehatan Tingkat Pertama
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public PoliGet PoliFktpList(string start, string limit)
        {
            PoliGet result = null;
            var serviceUrl = string.Concat(_baseUrl, string.Format("/poli/fktp/{0}/{1}", start, limit));
            var readResult = HttpGet(serviceUrl);
            result = JsonConvert.DeserializeObject<PoliGet>(readResult);
            return result;
        }

        /// <summary>
        /// Fasilitas Kesehatan Tingkat Lanjut
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public PoliGet PoliFktlList(string start, string limit)
        {
            PoliGet result = null;
            var serviceUrl = string.Concat(_baseUrl, string.Format("/poli/fktl/{0}/{1}", start, limit));
            var readResult = HttpGet(serviceUrl);
            result = JsonConvert.DeserializeObject<PoliGet>(readResult);
            return result;
        }

        public ProviderGet ProviderList(string start, string limit)
        {
            ProviderGet result = null;
            var serviceUrl = string.Concat(_baseUrl, string.Format("/provider/{0}/{1}", start, limit));
            var readResult = HttpGet(serviceUrl);
            result = JsonConvert.DeserializeObject<ProviderGet>(readResult);
            return result;
        }

        public StatusPulangGet StatusPulangList(bool isRawatInap)
        {
            StatusPulangGet result = null;
            var serviceUrl = string.Concat(_baseUrl, string.Format("/statuspulang/rawatInap/{0}", isRawatInap));
            var readResult = HttpGet(serviceUrl);
            result = JsonConvert.DeserializeObject<StatusPulangGet>(readResult);
            return result;
        }

        public TindakanGet TindakanList(string start, string limit)
        {
            TindakanGet result = null;
            var serviceUrl = string.Concat(_baseUrl, string.Format("/tindakan/kdTkp/10/{0}/{1}", start, limit));
            var readResult = HttpGet(serviceUrl);
            result = JsonConvert.DeserializeObject<TindakanGet>(readResult);
            return result;
        }
        #endregion


        #region Patient transaction
        #region Pendaftaran
        public PostResponse PendaftaranAdd(PendaftaranPost postData)
        {
            var serviceUrl = string.Concat(_baseUrl, "/pendaftaran");
            var response = HttpPost(serviceUrl, JsonConvert.SerializeObject(postData));
            return response;
        }

        public string PendaftaranGet(int noUrut, DateTime tglDaftar)
        {
            var pcareTgl = tglDaftar.ToString(PCare.Common.Constant.DateFormatPCare);
            var serviceUrl = string.Concat(_baseUrl,
                string.Format("/pendaftaran/noUrut/{0}/tglDaftar/{1}", noUrut, pcareTgl));

            var response = HttpGet(serviceUrl);
            return response;
        }
        public string PendaftaranGet(string noKartu, DateTime tglDaftar)
        {
            var pcareTgl = tglDaftar.ToString(PCare.Common.Constant.DateFormatPCare);
            var serviceUrl = string.Concat(_baseUrl,
    string.Format("/pendaftaran/noKartu/{0}/tglDaftar/{1}", noKartu, pcareTgl));

            var response = HttpGet(serviceUrl);
            return response;
        }
        public string PendaftaranGetByProvider(DateTime tglDaftar, int start, int limit)
        {
            var pcareTgl = tglDaftar.ToString(PCare.Common.Constant.DateFormatPCare);
            var serviceUrl = string.Concat(_baseUrl,
                string.Format("/pendaftaran/tglDaftar/{0}/{1}/{2}", pcareTgl, start, limit));

            var response = HttpGet(serviceUrl);
            return response;
        }
        public PostResponse PendaftaranDelete(string noKartu, DateTime tglDaftar, string noUrut, string kdPoli)
        {
            var pcareTgl = tglDaftar.ToString(PCare.Common.Constant.DateFormatPCare);
            var serviceUrl = string.Concat(_baseUrl,
                string.Format("/pendaftaran/peserta/{0}/tglDaftar/{1}/noUrut/{2}/kdPoli/{3}",
                    noKartu, pcareTgl, noUrut, kdPoli));

            var response = HttpDelete(serviceUrl);
            return response;
        }
        #endregion

        #region Kunjungan
        public PostResponse KunjunganAdd(KunjunganPost postData)
        {
            var serviceUrl = string.Concat(_baseUrl, "/kunjungan");
            var response = HttpPost(serviceUrl, JsonConvert.SerializeObject(postData));
            return response;
        }
        public PostResponse KunjunganEdit(KunjunganPost postData)
        {
            var serviceUrl = string.Concat(_baseUrl, "/kunjungan");
            var response = HttpPut(serviceUrl, JsonConvert.SerializeObject(postData));
            return response;
        }

        public PostResponse KunjunganDelete(string noKunjungan)
        {
            var serviceUrl = string.Concat(_baseUrl,
                string.Format("/kunjungan/{0}", noKunjungan));

            var response = HttpDelete(serviceUrl);
            return response;
        }
        public KunjunganGet KunjunganGet(string nokartu)
        {
            var serviceUrl = string.Concat(_baseUrl,
                string.Format("/kunjungan/peserta/{0}", nokartu));

            var response = HttpGet(serviceUrl);
            return JsonConvert.DeserializeObject<KunjunganGet>(response);
        }
        #endregion

        #region Kunjungan Tindakan
        public PostResponse KunjunganTindakanAdd(TindakanPost postData)
        {
            var serviceUrl = string.Concat(_baseUrl, "/tindakan");
            var response = HttpPost(serviceUrl, JsonConvert.SerializeObject(postData));
            return response;
        }
        public PostResponse KunjunganTindakanEdit(TindakanPost postData)
        {
            var serviceUrl = string.Concat(_baseUrl, "/tindakan");
            var response = HttpPut(serviceUrl, JsonConvert.SerializeObject(postData));
            return response;
        }
        public PostResponse KunjunganTindakanDelete(int kdTindakanSK, string noKunjungan)
        {
            var serviceUrl = string.Concat(_baseUrl,
                string.Format("/tindakan/{0}/kunjungan/{1}", kdTindakanSK, noKunjungan));

            var response = HttpDelete(serviceUrl);
            return response;
        }
        public KunjunganTindakanGet KunjunganTindakanGet(string noKunjungan)
        {
            var serviceUrl = string.Concat(_baseUrl,
                string.Format("/tindakan/kunjungan/{0}", noKunjungan));

            var response = HttpGet(serviceUrl);
            return JsonConvert.DeserializeObject<KunjunganTindakanGet>(response);
        }
        #endregion

        #region Kunjungan Obat
        public PostResponseMultiField KunjunganObatAdd(KunjunganObatPost postData)
        {
            var serviceUrl = string.Concat(_baseUrl, "/obat/kunjungan");
            var response = HttpPostMultiFieldResult(serviceUrl, JsonConvert.SerializeObject(postData));
            return response;
        }
        public PostResponseMultiField KunjunganObatEdit(KunjunganObatPost postData)
        {
            // Delete
            KunjunganObatDelete(postData.KdObatSK, postData.NoKunjungan);

            // Add
            postData.KdObatSK = 0;
            var response = KunjunganObatAdd(postData);
            return response;
        }
        public PostResponse KunjunganObatDelete(int kdObatSK, string noKunjungan)
        {
            var serviceUrl = string.Concat(_baseUrl,
                string.Format("/obat/{0}/kunjungan/{1}", kdObatSK, noKunjungan));

            var response = HttpDelete(serviceUrl);
            return response;
        }
        public KunjunganObatGet KunjunganObatGet(string noKunjungan)
        {
            var serviceUrl = string.Concat(_baseUrl,
                string.Format("/obat/kunjungan/{0}", noKunjungan));

            var response = HttpGet(serviceUrl);
            return JsonConvert.DeserializeObject<KunjunganObatGet>(response);
        }
        #endregion

        #region Kunjungan Tindakan
        public PostResponse KunjunganMcuAdd(McuPost postData)
        {
            var serviceUrl = string.Concat(_baseUrl, "/mcu");
            var response = HttpPost(serviceUrl, JsonConvert.SerializeObject(postData));
            return response;
        }
        public PostResponse KunjunganMcuEdit(McuPost postData)
        {
            var serviceUrl = string.Concat(_baseUrl, "/mcu");
            var response = HttpPut(serviceUrl, JsonConvert.SerializeObject(postData));
            return response;
        }
        public PostResponse KunjunganMcuDelete(int kdmcu, string noKunjungan)
        {
            var serviceUrl = string.Concat(_baseUrl,
                string.Format("/mcu/{0}/kunjungan/{1}", kdmcu, noKunjungan));

            var response = HttpDelete(serviceUrl);
            return response;
        }
        public string KunjunganMcuGet(string noKunjungan)
        {
            var serviceUrl = string.Concat(_baseUrl,
                string.Format("/mcu/kunjungan/{0}", noKunjungan));

            var response = HttpGet(serviceUrl);
            return response;
        }
        #endregion
        #endregion

        public SpesialisGet SpesialisList(string start, string limit)
        {
            SpesialisGet result = null;
            var serviceUrl = string.Concat(_baseUrl, string.Format("/pcare-rest-v3.0/spesialis"));
            var readResult = HttpGet(serviceUrl);
            result = JsonConvert.DeserializeObject<SpesialisGet>(readResult);
            return result;
        }
    }
}
