using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class Jenis
    {
        [JsonProperty("kode")]
        public string Kode { get; set; }

        [JsonProperty("nama")]
        public string Nama { get; set; }
    }
}
