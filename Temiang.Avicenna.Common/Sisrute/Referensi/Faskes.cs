using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.Common.Sisrute.Referensi
{
    public class Faskes : Common.Response
    {

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("data")]
        public Common.Data[] Data { get; set; }
    }

    public class FaskesByCode : Common.Response
    {

        [JsonProperty("data")]
        public Common.Data Data { get; set; }
    }
}
