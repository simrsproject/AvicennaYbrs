using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.Common;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class KunjunganTindakanGet : BaseGetResponse
    {
        [JsonProperty("response")]
        public Result Response { get; set; }

        #region child class
        public class Result
        {
            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("list")]
            public List<KunjunganTindakan> List { get; set; }
        }

        public class KunjunganTindakan
        {
            [JsonProperty("kdTindakanSK")]
            public int KdTindakanSk { get; set; }

            [JsonProperty("noKunjungan")]
            public string NoKunjungan { get; set; }

            [JsonProperty("nmTindakan")]
            public string NmTindakan { get; set; }

            [JsonProperty("biaya")]
            public decimal Biaya { get; set; }

            [JsonProperty("keterangan")]
            public string Keterangan { get; set; }

            [JsonProperty("hasil")]
            public decimal Hasil { get; set; }


        }
        #endregion
    }
}
