using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Detail
    {
        [JsonProperty("errorcode")]
        public string Errorcode { get; set; }
    }

    public class Fault
    {
        [JsonProperty("faultstring")]
        public string Faultstring { get; set; }

        [JsonProperty("detail")]
        public Detail Detail { get; set; }
    }

    public class FaultResponse
    {
        [JsonProperty("fault")]
        public Fault Fault { get; set; }
    }


}
