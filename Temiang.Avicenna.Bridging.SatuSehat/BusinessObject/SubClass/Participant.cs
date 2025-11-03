using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    public class Participant
    {
        [JsonProperty("type")]
        public List<Code> Type { get; set; }

        [JsonProperty("individual")]
        public Individual Individual { get; set; }
    }
}
