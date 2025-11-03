using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class Asuransi
    {
        [JsonProperty("kdAsuransi")]
        public string KdAsuransi { get; set; }

        [JsonProperty("nmAsuransi")]
        public string NmAsuransi { get; set; }

        [JsonProperty("noAsuransi")]
        public string NoAsuransi { get; set; }
    }
}
