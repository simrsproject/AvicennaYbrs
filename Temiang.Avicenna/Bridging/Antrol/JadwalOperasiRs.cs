using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.Antrol.JadwalOperasiRs
{
    public class Request
    {
        public class Root
        {
            [JsonProperty("tanggalawal")]
            public string Tanggalawal;

            [JsonProperty("tanggalakhir")]
            public string Tanggalakhir;
        }

        public class OkadocRoot
        {
            [JsonProperty("datestart")]
            public string DateStart;

            [JsonProperty("dateend")]
            public string DateEnd;

            [JsonProperty("jknno")]
            public string JknNo;
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

            [JsonProperty("nopeserta")]
            public string Nopeserta;

            [JsonProperty("lastupdate")]
            public Int64 Lastupdate;
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