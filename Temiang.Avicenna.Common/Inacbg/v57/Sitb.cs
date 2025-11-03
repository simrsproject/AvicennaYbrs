using Newtonsoft.Json;
using System.Collections.Generic;

namespace Temiang.Avicenna.Common.Inacbg.v57.Sitb
{
    public class Validate
    {
        public class Data
        {
            public string nomor_sep { get; set; }
            public string nomor_register_sitb { get; set; }
        }

        public class Response
        {
            public class Datum
            {
                [JsonProperty("id")]
                public string Id;

                [JsonProperty("nama")]
                public string Nama;

                [JsonProperty("nik")]
                public string Nik;

                [JsonProperty("jenis_kelamin_id")]
                public string JenisKelaminId;
            }

            public class TResponse
            {
                [JsonProperty("status")]
                public string Status;

                [JsonProperty("detail")]
                public string Detail;

                [JsonProperty("validation")]
                public Validation Validation;
            }

            public class Root
            {
                [JsonProperty("metadata")]
                public Metadata Metadata;

                [JsonProperty("response")]
                public TResponse Response;
            }

            public class Validation
            {
                [JsonProperty("data")]
                public List<Datum> Data;

                [JsonProperty("success")]
                public bool? Success;
            }
        }
    }

    public class Invalidate
    {
        public class Data
        {
            public string nomor_sep { get; set; }
        }

        public class Response
        {
            public class Root
            {
                [JsonProperty("metadata")]
                public Metadata Metadata;
            }
        }
    }
}
