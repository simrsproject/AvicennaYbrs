using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    public class Interpretation
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
