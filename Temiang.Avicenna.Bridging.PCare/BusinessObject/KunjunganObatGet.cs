using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.Common;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class KunjunganObatGet : BaseGetResponse
    {
        [JsonProperty("response")]
        public Result Response { get; set; }

        #region child class
        public class Result
        {
            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("list")]
            public List<KunjunganObat> List { get; set; }
        }

        public class KunjunganObat
        {
            [JsonProperty("kdObatSK")]
            public int KdObatSk { get; set; }

            [JsonProperty("kdRacikan")]
            public string KdRacikan { get; set; }

            [JsonProperty("obat")]
            public Obat Obat { get; set; }

            [JsonProperty("signa1")]
            public string Signa1 { get; set; }

            [JsonProperty("signa2")]
            public string Signa2 { get; set; }

            [JsonProperty("jmlObat")]
            public decimal JmlObat { get; set; }

            [JsonProperty("jmlHari")]
            public decimal JmlHari { get; set; }

            [JsonProperty("kekuatan")]
            public decimal Kekuatan { get; set; }

            [JsonProperty("jmlPermintaan")]
            public decimal JmlPermintaan { get; set; }

            [JsonProperty("jmlObatRacikan")]
            public decimal JmlObatRacikan { get; set; }
        }
        #endregion
    }
}
