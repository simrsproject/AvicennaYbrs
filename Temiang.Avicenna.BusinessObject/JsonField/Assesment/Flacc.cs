using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class Flacc
    {
        [JsonProperty("Fc", NullValueHandling = NullValueHandling.Ignore)]
        public string Face { get; set; }

        [JsonProperty("Lg", NullValueHandling = NullValueHandling.Ignore)]
        public string Legs { get; set; }

        [JsonProperty("Ac", NullValueHandling = NullValueHandling.Ignore)]
        public string Activity { get; set; }

        [JsonProperty("Cr", NullValueHandling = NullValueHandling.Ignore)]
        public string Cry { get; set; }

        [JsonProperty("Cn", NullValueHandling = NullValueHandling.Ignore)]
        public string Consolability { get; set; }

        [JsonProperty("Ts", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalScore { get; }
    }
}
