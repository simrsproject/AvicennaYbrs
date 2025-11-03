using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.Common;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class TindakanGet : BaseGetResponse
    {
        [JsonProperty("response")]
        public Result Response { get; set; }

        #region child class
        public class Result
        {
            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("list")]
            public List<Tindakan> List { get; set; }
        }

        public class Tindakan
        {
            [JsonProperty("kdTindakan")]
            public string KdTindakan { get; set; }

            [JsonProperty("nmTindakan")]
            public string NmTindakan { get; set; }

            [JsonProperty("maxTarif")]
            public double MaxTarif { get; set; }

            [JsonProperty("withValue")]
            public bool WithValue { get; set; }
        }
        #endregion
    }
}
