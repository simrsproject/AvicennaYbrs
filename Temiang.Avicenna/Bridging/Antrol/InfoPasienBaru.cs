using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.Antrol.InfoPasienBaru
{
    public class Request
    {
        public class Root
        {
            [JsonProperty("nomorkartu")]
            public string Nomorkartu;

            [JsonProperty("nik")]
            public string Nik;

            [JsonProperty("nomorkk")]
            public string Nomorkk;

            [JsonProperty("nama")]
            public string Nama;

            [JsonProperty("jeniskelamin")]
            public string Jeniskelamin;

            [JsonProperty("tanggallahir")]
            public string Tanggallahir;

            [JsonProperty("nohp")]
            public string Nohp;

            [JsonProperty("alamat")]
            public string Alamat;

            [JsonProperty("kodeprop")]
            public string Kodeprop;

            [JsonProperty("namaprop")]
            public string Namaprop;

            [JsonProperty("kodedati2")]
            public string Kodedati2;

            [JsonProperty("namadati2")]
            public string Namadati2;

            [JsonProperty("kodekec")]
            public string Kodekec;

            [JsonProperty("namakec")]
            public string Namakec;

            [JsonProperty("kodekel")]
            public string Kodekel;

            [JsonProperty("namakel")]
            public string Namakel;

            [JsonProperty("rw")]
            public string Rw;

            [JsonProperty("rt")]
            public string Rt;
        }
    }

    public class Response
    {
        public class TResponse
        {
            [JsonProperty("norm")]
            public string Norm;
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