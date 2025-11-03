using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    public class Period
    {
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public string End { get; set; }
    }
}
