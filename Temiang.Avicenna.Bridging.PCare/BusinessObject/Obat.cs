using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class Obat
    {
        [JsonProperty("kdObat")]
        public string KdObat { get; set; }

        [JsonProperty("nmObat")]
        public string NmObat { get; set; }

        [JsonProperty("sedia")]
        public int Sedia { get; set; }

    }
}
