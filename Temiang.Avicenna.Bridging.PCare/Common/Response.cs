using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.Common
{
    public class Response
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

    }
}
