using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.Common;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class TindakanPost
    {

        [JsonProperty("kdTindakanSK")]
        public int KdTindakanSK { get; set; }

        [JsonProperty("noKunjungan")]
        public string NoKunjungan { get; set; }

        [JsonProperty("kdTindakan")]
        public string KdTindakan { get; set; }

        [JsonProperty("biaya")]
        public decimal Biaya { get; set; }

        [JsonProperty("keterangan")]
        public string Keterangan { get; set; }

        [JsonProperty("hasil")]
        public int Hasil { get; set; }
    }
}
