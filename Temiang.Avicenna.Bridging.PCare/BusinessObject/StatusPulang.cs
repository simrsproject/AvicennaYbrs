using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class StatusPulang
    {
        [JsonProperty("kdStatusPulang")]
        public string KdStatusPulang { get; set; }

        [JsonProperty("nmStatusPulang")]
        public string NmStatusPulang { get; set; }
    }
}
