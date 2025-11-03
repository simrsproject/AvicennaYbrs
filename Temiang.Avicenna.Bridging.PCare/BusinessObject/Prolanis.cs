using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class Prolanis
    {
        [JsonProperty("kdProgram")]
        public string KdProgram { get; set; }

        [JsonProperty("nmProgram")]
        public string NmProgram { get; set; }
    }

}
