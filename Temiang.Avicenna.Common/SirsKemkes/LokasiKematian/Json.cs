using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.SirsKemkes.LokasiKematian
{
    public class Json
    {
        // http://202.70.136.86:3010/api/lokasikematian?page=1&limit=1000

        public class Datum
        {
            [JsonProperty("id")]
            public int Id;

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
