using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.Common.Sisrute.Rujukan
{
    public class Get : Common.Response
    {

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("data")]
        public Post.Data[] Data { get; set; }
    }
}
