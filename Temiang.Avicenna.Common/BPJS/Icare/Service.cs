using LZStringCSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.BPJS.Icare
{
    public class Service
    {
        private string _url = ConfigurationManager.AppSettings["IcareServiceUrlLocation"];
        private string _consKey = ConfigurationManager.AppSettings["BPJSConsumerID"];
        private string _ppkKey = ConfigurationManager.AppSettings["BPJSHospitalID"];
        private string _salt = ConfigurationManager.AppSettings["BPJSSaltConsumerID"];
        private string _encrypted = ConfigurationManager.AppSettings["BPJSResponseIsEncrypted"];
        private string _userKey = ConfigurationManager.AppSettings["BPJSUserKey"];

        public HttpWebRequest PopulateWebRequest(string url, Helper.WebRequestMethod method, Helper.WebRequestContentType contentType, string parameter, out string timeStamp)
        {
            Helper.IgnoreBadCertificates();

            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = method.ToString();

            if (method != Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

            webrequest.Headers.Add("X-cons-id", _consKey);
            timeStamp = BPJS.Helper.GetUnixTimeStamp();
            webrequest.Headers.Add("X-timestamp", timeStamp);
            webrequest.Headers.Add("X-signature", BPJS.Helper.GetEncodedKey(timeStamp, _consKey, _salt, false));
            webrequest.Headers.Add("user_key", _userKey);

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

        public Json.Response.Root GetToken(Json.Request.Root root)
        {
            _url += "api/rs/validate";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.JSON, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Json.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Json.Response.Root
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = new Json.Response.UrlResponse()
                            {
                                Url = JsonConvert.DeserializeObject<Json.Response.UrlResponse>(decryptResponse).Url
                            }
                        };

                        return entity;
                    }
                    else return new Json.Response.Root()
                    {
                        MetaData = new Metadata()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public string DecryptResponse(string timeStamp, string data)
        {
            byte[] key;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                key = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(_consKey, _salt, timeStamp)));
            }
            byte[] iv = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                iv[i] = key[i];
            }

            string plaintext = string.Empty;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(data)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
