using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.Net;
using System.Net.Security;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS
{
    public class Helper
    {
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;

            var strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string GetEncodedKey(string timeStamp, string consid, string salt, bool isUsingMd5HashonSalt)
        {
            if (isUsingMd5HashonSalt) salt = MD5Hash(salt);

            // Initialize the keyed hash object using the secret key as the key
            var hashObject = new HMACSHA256(Encoding.UTF8.GetBytes(salt));

            // Computes the signature by hashing the salt with the secret key as
            var signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}&{1}", consid, timeStamp)));

            // Base 64 Encode
            return Convert.ToBase64String(signature);
        }

        public static string GetUnixTimeStamp()
        {
            var span = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            var dbl = span.TotalSeconds;
            var integer = Convert.ToInt64(span.TotalSeconds);
            return integer.ToString();
        }

        public static void IgnoreBadCertificates()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
        }

        private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification,
            System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public sealed class WebRequestMethod
        {
            private readonly String name;
            private readonly int value;

            public static readonly WebRequestMethod GET = new WebRequestMethod(1, "GET");
            public static readonly WebRequestMethod POST = new WebRequestMethod(2, "POST");
            public static readonly WebRequestMethod PUT = new WebRequestMethod(3, "PUT");
            public static readonly WebRequestMethod DELETE = new WebRequestMethod(4, "DELETE");
            public static readonly WebRequestMethod PATCH = new WebRequestMethod(5, "PATCH");

            private WebRequestMethod(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public sealed class WebRequestContentType
        {
            private readonly String name;
            private readonly int value;

            public static readonly WebRequestContentType TEXT = new WebRequestContentType(1, "text/plain");
            public static readonly WebRequestContentType FORM = new WebRequestContentType(2, "Application/x-www-form-urlencoded");
            public static readonly WebRequestContentType JSON = new WebRequestContentType(3, "application/json");

            private WebRequestContentType(int value, String name)
            {
                this.name = name;
                this.value = value;
            }

            public override String ToString()
            {
                return name;
            }
        }

        public class EncryptedResponse
        {
            public class Root : Metadata
            {
                [JsonProperty("metaData")]
                public Metadata MetaData;

                [JsonProperty("response")]
                public string Response;
            }
        }
    }
}
