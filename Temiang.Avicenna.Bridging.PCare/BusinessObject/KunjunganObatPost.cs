using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.Common;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class KunjunganObatPost
    {

        [JsonProperty("kdObatSK")]
        public int KdObatSK { get; set; }

        [JsonProperty("noKunjungan")]
        public string NoKunjungan { get; set; }

        [JsonProperty("racikan")]
        public bool Racikan { get; set; }

        [JsonProperty("kdRacikan")]
        public string KdRacikan { get; set; }

        [JsonProperty("obatDPHO")]
        public bool ObatDPHO { get; set; }

        [JsonProperty("kdObat")]
        public string KdObat { get; set; }

        [JsonProperty("signa1")]
        public string Signa1 { get; set; }

        [JsonProperty("signa2")]
        public string Signa2 { get; set; }

        [JsonProperty("jmlObat")]
        public decimal JmlObat { get; set; }

        [JsonProperty("jmlPermintaan")]
        public decimal JmlPermintaan { get; set; }

        [JsonProperty("nmObatNonDPHO")]
        public string NmObatNonDPHO { get; set; }
    }
}
