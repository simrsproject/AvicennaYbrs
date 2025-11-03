using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.Antrol.StatusAntrean
{
    public class Request
    {
        public class Root
        {
            [JsonProperty("kodepoli")]
            public string Kodepoli;

            [JsonProperty("kodedokter")]
            public int Kodedokter;

            [JsonProperty("tanggalperiksa")]
            public string Tanggalperiksa;

            [JsonProperty("jampraktek")]
            public string Jampraktek;
        }
    }

    public class Response
    {
        public class TResponse
        {
            [JsonProperty("namapoli")]
            public string Namapoli;

            [JsonProperty("namadokter")]
            public string Namadokter;

            [JsonProperty("totalantrean")]
            public int Totalantrean;

            [JsonProperty("sisaantrean")]
            public int Sisaantrean;

            [JsonProperty("antreanpanggil")]
            public string Antreanpanggil;

            [JsonProperty("sisakuotajkn")]
            public int Sisakuotajkn;

            [JsonProperty("kuotajkn")]
            public int Kuotajkn;

            [JsonProperty("sisakuotanonjkn")]
            public int Sisakuotanonjkn;

            [JsonProperty("kuotanonjkn")]
            public int Kuotanonjkn;

            [JsonProperty("keterangan")]
            public string Keterangan;
        }

        public class Metadata
        {
            [JsonProperty("message")]
            public string Message;

            [JsonProperty("code")]
            public int Code;
        }

        public class Root
        {
            [JsonProperty("response")]
            public TResponse Response;

            [JsonProperty("metadata")]
            public Metadata metadata;
        }
    }
}