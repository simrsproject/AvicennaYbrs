using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.Antrol.CheckIn
{
    public class Request
    {
        public class Root
        {
            [JsonProperty("kodebooking")]
            public string Kodebooking;

            [JsonProperty("waktu")]
            public Int64 Waktu;
        }

        public class SelfChechkIn
        {
            [JsonProperty("norujukan")]
            public string NoRujukan;

            [JsonProperty("tipe")]
            public string Tipe;

            [JsonProperty("param")]
            public string Param;
        }
    }

    public class Response
    {
        public class Metadata
        {
            [JsonProperty("message")]
            public string Message;

            [JsonProperty("code")]
            public int Code;
        }

        public class Root
        {
            [JsonProperty("metadata")]
            public Metadata metadata;
        }
    }

    public class Request64String
    {
        public class Root
        {
            [JsonProperty("nokapst")]
            public string Nokapst;

            [JsonProperty("kodeBooking")]
            public string KodeBooking;

            [JsonProperty("noRujukan")]
            public string NoRujukan;

            [JsonProperty("norm")]
            public string Norm;

            [JsonProperty("ketKunjungan")]
            public string KetKunjungan;

            [JsonProperty("namaFaskesAsalRujuk")]
            public string NamaFaskesAsalRujuk;

            [JsonProperty("namaPoli")]
            public string NamaPoli;

            [JsonProperty("namaDokter")]
            public string NamaDokter;

            [JsonProperty("nomorAntrean")]
            public string NomorAntrean;
        }
    }
}