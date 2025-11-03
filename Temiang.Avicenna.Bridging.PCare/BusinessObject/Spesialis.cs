using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.Common;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class Spesialis
    {
        [JsonProperty("kdSpesialis")]
        public string KdSpesialis { get; set; }

        [JsonProperty("nmSpesialis")]
        public string NmSpesialis { get; set; }
    }

    public class SpesialisGet : BaseGetResponse
    {
        [JsonProperty("response")]
        public Result Response { get; set; }

        #region child class
        public class Result
        {
            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("list")]
            public List<Spesialis> List { get; set; }
        }

        #endregion
    }
}
