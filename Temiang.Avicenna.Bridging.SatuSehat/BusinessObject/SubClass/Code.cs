using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    public class Code
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }
}
