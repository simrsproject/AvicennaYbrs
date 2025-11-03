using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.Common
{
    public class PostResponse
    {
        [JsonProperty("response")]
        public Response Response { get; set; }

        [JsonProperty("metaData")]
        public MetaData MetaData { get; set; }

        public virtual bool IsOk
        {
            get { return (MetaData.Code == "200" || MetaData.Code == "201"); }
        }
    }
}
