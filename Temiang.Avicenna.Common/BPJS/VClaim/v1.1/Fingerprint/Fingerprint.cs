using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v20.Fingerprint
{
    public class Select
    {
        public class MetaData
        {
            public bool IsValid { get { return Code == "200"; } }

            [JsonProperty("code")]
            public string Code;

            [JsonProperty("message")]
            public string Message;
        }

        public class List
        {
            [JsonProperty("noKartu")]
            public string NoKartu;

            [JsonProperty("noSEP")]
            public string NoSEP;
        }

        public class Response
        {
            [JsonProperty("list")]
            public List<List> List;
        }

        public class Root
        {
            [JsonProperty("metaData")]
            public MetaData MetaData;

            [JsonProperty("response")]
            public Response Response;
        }
    }
}
