using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class Kesadaran
    {
        [JsonProperty("kdSadar")]
        public string KdSadar { get; set; }

        [JsonProperty("nmSadar")]
        public string NmSadar { get; set; }
    }

}
