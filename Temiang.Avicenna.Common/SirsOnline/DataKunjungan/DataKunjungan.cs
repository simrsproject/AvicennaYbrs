using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.SirsOnline
{
    public class DataKunjungan
    {
        public class Irj
        {
            [JsonProperty("KLINIK")]
            public string KLINIK;

            [JsonProperty("JKN")]
            public int JKN;

            [JsonProperty("NON JKN")]
            public int NONJKN;
        }

        public class Igd
        {
            [JsonProperty("JKN")]
            public int JKN;

            [JsonProperty("NON JKN")]
            public int NONJKN;
        }

        public class Iri
        {
            [JsonProperty("CONTENT")]
            public string CONTENT;

            [JsonProperty("JLH")]
            public string JLH;
        }
    }
}
