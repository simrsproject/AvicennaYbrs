using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.Antrol.BatalAntrean
{
    public class Request
    {
        public class Root
        {
            [JsonProperty("kodebooking")]
            public string Kodebooking;

            [JsonProperty("keterangan")]
            public string Keterangan;
        }
    }

    public class Response
    {
        public class Metadata
        {
            [JsonProperty("message")]
            public string Message;

            [JsonProperty("code")]
            public int Code;
        }

        public class Root
        {
            [JsonProperty("metadata")]
            public Metadata metadata;
        }
    }
}