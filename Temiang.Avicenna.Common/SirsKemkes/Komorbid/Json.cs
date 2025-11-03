using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.SirsKemkes.Komorbid
{
    public class Json
    {
        // http://202.70.136.86:3010/api/komorbid?page=1&limit=1000

        public class Pagination
        {
            [JsonProperty("total_number_of_pages")]
            public int TotalNumberOfPages;

            [JsonProperty("current_page")]
            public int CurrentPage;
        }

        [Serializable]
        public class Datum
        {
            [JsonProperty("id")]
            public string Id;

            [JsonProperty("description")]
            public string Description;
        }

        public class Root
        {
            [JsonProperty("status")]
            public bool Status;

            [JsonProperty("message")]
            public string Message;

            [JsonProperty("pagination")]
            public Pagination Pagination;

            [JsonProperty("data")]
            public List<Datum> Data;
        }
    }
}
