using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.SirsKemkes.Kelurahan
{
    public class Json
    {
        // http://202.70.136.86:3010/api/kelurahan?page=1&limit=1000

        public class Next
        {
            [JsonProperty("page")]
            public int Page;

            [JsonProperty("limit")]
            public int Limit;
        }

        public class Pagination
        {
            [JsonProperty("total_number_of_pages")]
            public int TotalNumberOfPages;

            [JsonProperty("current_page")]
            public int CurrentPage;

            [JsonProperty("next")]
            public Next Next;
        }

        public class Datum
        {
            [JsonProperty("id")]
            public string Id;

            [JsonProperty("nama")]
            public string Nama;

            [JsonProperty("kecamatan_id")]
            public string KecamatanId;
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
