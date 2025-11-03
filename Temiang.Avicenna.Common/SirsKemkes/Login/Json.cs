using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.SirsKemkes.Login
{
    public class Json
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("kode_rs")]
                public string KodeRs;

                [JsonProperty("password")]
                public string Password;
            }
        }

        public class Response
        {
            public class Data
            {
                [JsonProperty("access_token")]
                public string AccessToken;

                [JsonProperty("issued_at")]
                public string IssuedAt;

                [JsonProperty("expired_at")]
                public string ExpiredAt;

                [JsonProperty("expires_in")]
                public string ExpiresIn;
            }

            public class Root
            {
                [JsonProperty("status")]
                public bool Status;

                [JsonProperty("message")]
                public string Message;

                [JsonProperty("data")]
                public Data Data;
            }
        }
    }
}
