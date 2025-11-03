using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Temiang.Avicenna.Common.Inacbg.v51.Procedure
{

    public class Update
    {
        public class Metadata
        {
            public string method { get; set; }
            public string nomor_sep { get; set; }
        }

        public class Data
        {
            public string procedure { get; set; }
            public string coder_nik { get; set; }
        }

        public class RootObject
        {
            public Metadata metadata { get; set; }
            public Data data { get; set; }
        }
    }

    public class Search
    {
        public class Metadata
        {
            public string method { get; set; }
        }

        public class Data
        {
            public string keyword { get; set; }
        }

        public class RootObject
        {
            public Metadata metadata { get; set; }
            public Data data { get; set; }
        }
    }

    public class Result
    {
        public class Metadata : Inacbg.Metadata
        {

        }

        public class Response
        {

            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("data")]
            public string[][] Data { get; set; }
        }

        public class Data
        {

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }

        public class Data2
        {

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("response")]
            public Response2 Response { get; set; }
        }

        public class Response2
        {

            [JsonProperty("count")]
            public int Count { get; set; }
        }
    }
}
