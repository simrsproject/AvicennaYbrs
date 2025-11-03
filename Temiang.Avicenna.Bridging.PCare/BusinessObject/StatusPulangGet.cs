using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.Common;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class StatusPulangGet : BaseGetResponse
    {
        [JsonProperty("response")]
        public Result Response { get; set; }

        #region child class
        public class Result
        {
            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("list")]
            public List<StatusPulang> List { get; set; }
        }

        #endregion
    }
}
