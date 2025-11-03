using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class Provider
    {
        [JsonProperty("kdProvider")]
        public string KdProvider { get; set; }

        [JsonProperty("nmProvider")]
        public string NmProvider { get; set; }
    }
}
