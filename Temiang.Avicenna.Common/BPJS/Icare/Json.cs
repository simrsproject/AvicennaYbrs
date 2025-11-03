using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.BPJS.Icare.Json
{
    public class Request
    {
        public class Root
        {
            [JsonProperty("param")]
            public string Param;

            [JsonProperty("kodedokter")]
            public int? Kodedokter;
        }
    }

    public class Response
    {
        public class Root
        {
            [JsonProperty("response")]
            public UrlResponse Response;

            [JsonProperty("metaData")]
            public Metadata MetaData;
        }

        public class UrlResponse
        {
            [JsonProperty("url")]
            public string Url;
        }
    }
}
