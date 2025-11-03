using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Configuration;

namespace Temiang.Avicenna.Common.Sisrute.Common
{
    public class Response
    {

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }
    }

    public class Data
    {

        [JsonProperty("KODE")]
        public string KODE { get; set; }

        [JsonProperty("NAMA")]
        public string NAMA { get; set; }
    }
}
