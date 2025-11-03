using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.SirsKemkes.KasusKematian
{
    public class Json
    {
        // http://202.70.136.86:3010/api/kasuskematian?page=1&limit=1000

        public class Datum
        {
            [JsonProperty("id")]
            public string Id;

            [JsonProperty("nama")]
            public string Nama;
        }

        public class Root
        {
            [JsonProperty("status")]
            public bool Status;

            [JsonProperty("message")]
            public string Message;

            [JsonProperty("data")]
            public List<Datum> Data;
        }
    }
}
