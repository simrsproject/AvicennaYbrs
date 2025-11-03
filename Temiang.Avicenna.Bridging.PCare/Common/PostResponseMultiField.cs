using System.Collections.Generic;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.Common
{
    public class PostResponseMultiField
    {
        [JsonProperty("response")]
        public List<Response> Response { get; set; }

        [JsonProperty("metaData")]
        public MetaData MetaData { get; set; }

        public bool IsOk
        {
            get { return (MetaData.Code == "200") || (MetaData.Code == "201"); }
        }
    }
}
