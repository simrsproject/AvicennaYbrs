using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.Antrol.SisaAntrean
{
    public class Request
    {
        public class Root
        {
            [JsonProperty("kodebooking")]
            public string Kodebooking;
        }
    }

    public class Response
    {
        public class TResponse
        {
            [JsonProperty("nomorantrean")]
            public string Nomorantrean;

            [JsonProperty("namapoli")]
            public string Namapoli;

            [JsonProperty("namadokter")]
            public string Namadokter;

            [JsonProperty("sisaantrean")]
            public int Sisaantrean;

            [JsonProperty("antreanpanggil")]
            public string Antreanpanggil;

            [JsonProperty("waktutunggu")]
            public Int64 Waktutunggu;

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