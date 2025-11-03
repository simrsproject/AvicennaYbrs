using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.Antrol.JadwalOperasiPasien
{
    public class Request
    {
        public class Root
        {
            [JsonProperty("nopeserta")]
            public string Nopeserta;
        }
    }

    public class Response
    {
        public class List
        {
            [JsonProperty("kodebooking")]
            public string Kodebooking;

            [JsonProperty("tanggaloperasi")]
            public string Tanggaloperasi;

            [JsonProperty("jenistindakan")]
            public string Jenistindakan;

            [JsonProperty("kodepoli")]
            public string Kodepoli;

            [JsonProperty("namapoli")]
            public string Namapoli;

            [JsonProperty("terlaksana")]
            public int Terlaksana;
        }

        public class TResponse
        {
            [JsonProperty("list")]
            public List<List> List;
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