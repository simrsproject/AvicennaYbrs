using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class Dokter
    {
        [JsonProperty("kdDokter")]
        public string KdDokter { get; set; }

        [JsonProperty("nmDokter")]
        public string NmDokter { get; set; }
    }
}
