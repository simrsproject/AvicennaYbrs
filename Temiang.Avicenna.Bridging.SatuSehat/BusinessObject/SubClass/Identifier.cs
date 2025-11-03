using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    public class Identifier
    {
        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("use", NullValueHandling = NullValueHandling.Ignore)]
        public string Use;
    }
}
