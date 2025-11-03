using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.Common
{
    public class BaseGetResponse
    {
        [JsonProperty("metaData")]
        public MetaData MetaData { get; set; }

        public bool IsOk
        {
            get { return (MetaData.Code == "200") || (MetaData.Code == "201"); }
        }
    }
}
