using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class Poli
    {
        [JsonProperty("kdPoli")]
        public string KdPoli { get; set; }

        [JsonProperty("nmPoli")]
        public string NmPoli { get; set; }

        [JsonProperty("poliSakit")]
        public bool PoliSakit { get; set; }
    }

}
