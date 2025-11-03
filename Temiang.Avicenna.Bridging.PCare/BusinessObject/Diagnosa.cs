using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class Diagnosa
    {
        [JsonProperty("kdDiag")]
        public string KdDiag { get; set; }

        [JsonProperty("nmDiag")]
        public string NmDiag { get; set; }

        [JsonProperty("nonSpesialis")]
        public bool NonSpesialis { get; set; }

    }

}
