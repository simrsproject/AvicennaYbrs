// Diganti menggunakan AuthenticationMiddleware

//using System.Linq;
//using System.Web.Http;
//using System.Security.Claims;
//using System.Net.Http;
//using System.Net;
//using System.Collections.Generic;
//using Temiang.Avicenna.Bridging.Antrean.ResponseClass;
//using Temiang.Avicenna.Bridging.Antrean.ParameterClass;
//using Temiang.Avicenna.Bridging.Base;
//using Newtonsoft.Json;
//using System.Net.Http.Headers;

//namespace Temiang.Avicenna.Bridging
//{
//    public class TokenController : ApiController
//    {
//        private class Token
//        {
//            [JsonProperty("access_token")]
//            public string AccessToken { get; set; }

//            [JsonProperty("token_type")]
//            public string TokenType { get; set; }

//            [JsonProperty("expires_in")]
//            public int ExpiresIn { get; set; }
//        }

//        [HttpPost]
//        [Route("antrol_bpjs/gettoken")]
//        public HttpResponseMessage GetToken(TokenParam parVal)
//        {
//            Token token = new Token();
//            HttpClientHandler handler = new HttpClientHandler();
//            HttpClient client = new HttpClient(handler);
//            var requestBody = new Dictionary<string, string>
//                {
//                {"grant_type", "password"},
//                {"username", parVal.Username},
//                {"password", parVal.Password},
//                };
//            var tokenResponse = client.PostAsync(Common.Helper.UrlRoot2() + "/token", new FormUrlEncodedContent(requestBody)).Result;

//            var metadata = new MetaData();
//            if (tokenResponse.IsSuccessStatusCode)
//            {
//                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
//                token = JsonConvert.DeserializeObject<Token>(JsonContent);
//            }
//            else
//            {
//                metadata.Message = "Unable to generate Access Token Invalid username or password from " + Common.Helper.UrlRoot2() + ": " + tokenResponse.Content;
//                metadata.Code = "201";
//            }

//            var response = new
//            {
//                response = new
//                {
//                    //token = token.AccessToken
//                    token = string.Format("{0} {1}", token.TokenType, token.AccessToken)
//                },
//                metadata = metadata
//            };

//            return Request.CreateResponse(HttpStatusCode.Created, response);
//        }
//    }
//}