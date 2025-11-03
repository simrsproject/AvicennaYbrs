using System;
using System.Text;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Generators;
using Newtonsoft.Json;
using RestSharp;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Configuration;
using System.Web;
using System.Net;
using System.Linq;
using System.EnterpriseServices.Internal;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class SatuSehatKYC : BasePageDialog
    {
        #region Helper Methods

        private readonly string _clientID = SatuSehatKey("SatuSehatClientID");
        private readonly string _secretKey = SatuSehatKey("SatuSehatClientSecretKey");
        private readonly string _baseUrl = SatuSehatKey("SatuSehatBaseUrl");
        private readonly string _authUrl = SatuSehatKey("SatuSehatAuthUrl");
        private readonly string _organizationID = SatuSehatKey("SatuSehatOrganizationID");
        private static string SatuSehatKey(string key)
        {
            string configKey = string.Empty;
            var entity = new AppParameter();
            if (entity.LoadByPrimaryKey(key))
            {
                configKey = entity.ParameterValue;
            }
            else
            {
                configKey = ConfigurationManager.AppSettings[key];
                if (!HttpContext.Current.IsDebuggingEnabled)
                {
                    entity = new AppParameter
                    {
                        ParameterID = key,
                        ParameterName = key,
                        ParameterValue = configKey,
                        ParameterType = string.Empty,
                        IsUsedBySystem = true
                    };
                    entity.Save();
                }
            }
            return configKey;
        }

        private Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.TokenResponse GetToken()
        {
            var url = $"{_authUrl}/accesstoken?grant_type=client_credentials";
            var client = new RestClient(url);
            var request = new RestRequest { Method = Method.Post };
            var timeOutPar = AppParameter.GetParameterValue(AppParameter.ParameterItem.PCareTimeOutInSecond);
            int timeOut = Convert.ToInt16(timeOutPar) * 1000;
            request.Timeout = timeOut;
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", _clientID);
            request.AddParameter("client_secret", _secretKey);
            var response = client.Execute(request);
            try
            {
                if (response.Content.IsValidJson())
                {
                    var tokenResponse = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.TokenResponse>(response.Content);
                    return tokenResponse;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}{Environment.NewLine}{response.Content}", ex);
            }
        }
        #endregion

        #region RSA Key Generation & PEM Handling
        public static (string publicKey, string privateKey) GenerateKey()
        {
            var rsaKeyGen = new RsaKeyPairGenerator();
            rsaKeyGen.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
            AsymmetricCipherKeyPair keyPair = rsaKeyGen.GenerateKeyPair();

            string publicKeyPem;
            using (var sw = new StringWriter())
            {
                var pemWriter = new PemWriter(sw);
                pemWriter.WriteObject(keyPair.Public);
                pemWriter.Writer.Flush();
                publicKeyPem = sw.ToString();
            }

            string privateKeyPem;
            using (var sw = new StringWriter())
            {
                var pemWriter = new PemWriter(sw);
                pemWriter.WriteObject(keyPair.Private);
                pemWriter.Writer.Flush();
                privateKeyPem = sw.ToString();
            }
            return (publicKeyPem, privateKeyPem);
        }
        #endregion

        #region Chunk Split and PEM Formatting
        public static string ChunkSplit(string body, int chunkLen = 76, string end = "\r\n")
        {
            if (chunkLen < 1) return body;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < body.Length; i += chunkLen)
            {
                int len = Math.Min(chunkLen, body.Length - i);
                sb.Append(body.Substring(i, len));
                sb.Append(end);
            }
            return sb.ToString();
        }

        // Formats the payload as PEM with headers/footers
        public static string FormatMessage(byte[] data)
        {
            string base64 = Convert.ToBase64String(data);
            string chunked = ChunkSplit(base64, 76, "\r\n");
            return "-----BEGIN ENCRYPTED MESSAGE-----\r\n" +
                   chunked +
                   "-----END ENCRYPTED MESSAGE-----";
        }

        // Parses the PEM-formatted message (strips headers/footers and decodes base64)
        public static byte[] ParseFormattedMessage(string message)
        {
            string begin = "-----BEGIN ENCRYPTED MESSAGE-----";
            string end = "-----END ENCRYPTED MESSAGE-----";
            int startIndex = message.IndexOf(begin) + begin.Length;
            int endIndex = message.IndexOf(end);
            string base64 = message.Substring(startIndex, endIndex - startIndex)
                                   .Replace("\r", "")
                                   .Replace("\n", "")
                                   .Trim();
            return Convert.FromBase64String(base64);
        }
        #endregion

        #region Symmetric Key Generation, AES-GCM Encryption/Decryption

        public static byte[] GenerateSymmetricKey()
        {
            byte[] key = new byte[32];
            new SecureRandom().NextBytes(key);
            return key;
        }

        // AES-256-GCM encryption
        public static byte[] AesEncrypt(byte[] data, byte[] symmetricKey)
        {
            int ivLength = 12;
            byte[] iv = new byte[ivLength];
            new SecureRandom().NextBytes(iv);

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(symmetricKey), 128, iv, null);
            cipher.Init(true, parameters);

            byte[] output = new byte[cipher.GetOutputSize(data.Length)];
            int len = cipher.ProcessBytes(data, 0, data.Length, output, 0);
            cipher.DoFinal(output, len);

            // Concatenate IV, ciphertext, and tag
            byte[] result = new byte[iv.Length + output.Length];
            Array.Copy(iv, 0, result, 0, iv.Length);
            Array.Copy(output, 0, result, iv.Length, output.Length);
            return result;
        }

        // AES-256-GCM decryption
        public static byte[] AesDecrypt(byte[] encryptedData, byte[] symmetricKey)
        {
            int ivLength = 12;
            byte[] iv = new byte[ivLength];
            Array.Copy(encryptedData, 0, iv, 0, iv.Length);
            int cipherTextLength = encryptedData.Length - iv.Length;
            byte[] cipherTextWithTag = new byte[cipherTextLength];
            Array.Copy(encryptedData, iv.Length, cipherTextWithTag, 0, cipherTextLength);

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(symmetricKey), 128, iv, null);
            cipher.Init(false, parameters);

            byte[] output = new byte[cipher.GetOutputSize(cipherTextWithTag.Length)];
            int len = cipher.ProcessBytes(cipherTextWithTag, 0, cipherTextWithTag.Length, output, 0);
            cipher.DoFinal(output, len);
            return output;
        }
        #endregion

        #region RSA Encryption/Decryption (OAEP with SHA-256)

        public static byte[] RsaEncrypt(byte[] data, string publicKeyPEM)
        {
            var reader = new PemReader(new StringReader(publicKeyPEM));
            AsymmetricKeyParameter pubKey = (AsymmetricKeyParameter)reader.ReadObject();
            var engine = new OaepEncoding(new RsaEngine(), new Sha256Digest());
            engine.Init(true, pubKey);
            return engine.ProcessBlock(data, 0, data.Length);
        }

        public static byte[] RsaDecrypt(byte[] data, string privateKeyPEM)
        {
            var reader = new PemReader(new StringReader(privateKeyPEM));
            object keyObj = reader.ReadObject();
            AsymmetricKeyParameter privKey = keyObj is AsymmetricCipherKeyPair pair ? pair.Private : (AsymmetricKeyParameter)keyObj;
            var engine = new OaepEncoding(new RsaEngine(), new Sha256Digest());
            engine.Init(false, privKey);
            return engine.ProcessBlock(data, 0, data.Length);
        }
        #endregion

        #region Encrypt/Decrypt Message

        // Encrypts a message
        public static string EncryptMessage(string message, string pubPEM)
        {
            byte[] symmetricKey = GenerateSymmetricKey();
            byte[] wrappedKey = RsaEncrypt(symmetricKey, pubPEM);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] encryptedMessage = AesEncrypt(messageBytes, symmetricKey);
            byte[] payload = new byte[wrappedKey.Length + encryptedMessage.Length];
            Array.Copy(wrappedKey, 0, payload, 0, wrappedKey.Length);
            Array.Copy(encryptedMessage, 0, payload, wrappedKey.Length, encryptedMessage.Length);
            return FormatMessage(payload);
        }

        // Decrypts a message
        public static string DecryptMessage(string formattedMessage, string privateKeyPEM)
        {
            byte[] payload = ParseFormattedMessage(formattedMessage);
            int wrappedKeyLength = 256; // 256 bytes for a 2048-bit RSA key
            byte[] wrappedKey = new byte[wrappedKeyLength];
            Array.Copy(payload, 0, wrappedKey, 0, wrappedKeyLength);
            int aesEncryptedLength = payload.Length - wrappedKeyLength;
            byte[] encryptedMessage = new byte[aesEncryptedLength];
            Array.Copy(payload, wrappedKeyLength, encryptedMessage, 0, aesEncryptedLength);
            byte[] symmetricKey = RsaDecrypt(wrappedKey, privateKeyPEM);
            byte[] decryptedBytes = AesDecrypt(encryptedMessage, symmetricKey);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        #endregion

        #region Generate URL
        public static string GenerateUrl(string agentName, string agentNik, string accessToken)
        {
            // Generate RSA key pair for the agent.
            var keyPair = GenerateKey();
            string publicKey = keyPair.publicKey;
            string privateKey = keyPair.privateKey;

            // API URL and the API's public key.
            string IsKYCProd = ConfigurationManager.AppSettings["SatuSehatKYCProd"];
            string apiUrl = (IsKYCProd?.ToLower() == "true")
                ? "https://api-satusehat.kemkes.go.id/kyc/v1/generate-url"
                : "https://api-satusehat-stg.dto.kemkes.go.id/kyc/v1/generate-url";

            string pubPEM;

            if (IsKYCProd?.ToLower() == "true") // Production environment
            {
                pubPEM = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxLwvebfOrPLIODIxAwFp
4Qhksdtn7bEby5OhkQNLTdClGAbTe2tOO5Tiib9pcdruKxTodo481iGXTHR5033I
A5X55PegFeoY95NH5Noj6UUhyTFfRuwnhtGJgv9buTeBa4pLgHakfebqzKXr0Lce
/Ff1MnmQAdJTlvpOdVWJggsb26fD3cXyxQsbgtQYntmek2qvex/gPM9Nqa5qYrXx
8KuGuqHIFQa5t7UUH8WcxlLVRHWOtEQ3+Y6TQr8sIpSVszfhpjh9+Cag1EgaMzk+
HhAxMtXZgpyHffGHmPJ9eXbBO008tUzrE88fcuJ5pMF0LATO6ayXTKgZVU0WO/4e
iQIDAQAB
-----END PUBLIC KEY-----";
            }
            else // Sandbox environment
            {
                pubPEM = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwqoicEXIYWYV3PvLIdvB
qFkHn2IMhPGKTiB2XA56enpPb0UbI9oHoetRF41vfwMqfFsy5Yd5LABxMGyHJBbP
+3fk2/PIfv+7+9/dKK7h1CaRTeT4lzJBiUM81hkCFlZjVFyHUFtaNfvQeO2OYb7U
kK5JrdrB4sgf50gHikeDsyFUZD1o5JspdlfqDjANYAhfz3aam7kCjfYvjgneqkV8
pZDVqJpQA3MHAWBjGEJ+R8y03hs0aafWRfFG9AcyaA5Ct5waUOKHWWV9sv5DQXmb
EAoqcx0ZPzmHJDQYlihPW4FIvb93fMik+eW8eZF3A920DzuuFucpblWU9J9o5w+2
oQIDAQAB
-----END PUBLIC KEY-----";
            }

            // Create the payload.
            var data = new
            {
                agent_name = agentName,
                agent_nik = agentNik,
                public_key = publicKey
            };
            string jsonData = JsonConvert.SerializeObject(data);

            // Encrypt the JSON payload.
            string encryptedPayload = EncryptMessage(jsonData, pubPEM);
            string encryptedPayload2 = EncryptMessage(jsonData, publicKey);

            // Create and configure the HttpWebRequest.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "POST";
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.ContentType = "text/plain; charset=UTF-8";
            request.Accept = "*/*";

            // Convert the encrypted payload into a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(encryptedPayload);
            request.ContentLength = byteArray.Length;

            try
            {
                // Write the payload to the request stream.
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                // Get the response from the server.
                string responseContent;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
                // Decrypt the response
                string decryptedResponse = DecryptMessage(responseContent, privateKey);

                // Deserialize into a strongly typed object.
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(decryptedResponse);

                // Extract the URL.
                string kycUrl = apiResponse.data.url;

                // Return only the URL.
                return kycUrl;

            }
            catch (Exception ex)
            {
                string errorMsg = string.Format(ex.Message);
                return errorMsg;
            }
        }
        #endregion

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            string agen = txtName.Text;
            string nik_agen = txtPatSSN.Text;
            try
            {
                var token = GetToken();
                var kycUrl = GenerateUrl(agen, nik_agen, token.AccessToken);
                hfKYCUrl.Value = kycUrl;
                btnOpenNewWindow.Style["display"] = "inline-block";
            }
            catch (Exception ex)
            {
                ShowInformationHeader(ex.Message);
            }
        }

        #region class
        public class ResponseMetadata
        {
            public string code { get; set; }
            public string message { get; set; }
        }

        public class ResponseData
        {
            public string agent_name { get; set; }
            public string agent_nik { get; set; }
            public string token { get; set; }
            public string url { get; set; }
        }

        public class ApiResponse
        {
            public ResponseMetadata metadata { get; set; }
            public ResponseData data { get; set; }
        }
        #endregion

    }
}