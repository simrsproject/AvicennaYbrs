using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.Base
{
    public class BaseResponse
    {
        [JsonProperty("metadata")]
        public MetaData MetaData { get; set; }

        public bool IsOk
        {
            get { return (MetaData.Code == "200") || (MetaData.Code == "201"); }
        }
    }
}
