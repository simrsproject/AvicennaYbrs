using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Temiang.Avicenna.Module.Reports;

namespace Temiang.Avicenna.Bridging.Antrol
{
    public class AmbilAntrean
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("nomorkartu")]
                public string Nomorkartu;

                [JsonProperty("nik")]
                public string Nik;

                [JsonProperty("nohp")]
                public string Nohp;

                [JsonProperty("kodepoli")]
                public string Kodepoli;

                [JsonProperty("norm")]
                public string Norm;

                [JsonProperty("tanggalperiksa")]
                public string Tanggalperiksa;

                [JsonProperty("kodedokter")]
                public int Kodedokter;

                [JsonProperty("jampraktek")]
                public string Jampraktek;

                [JsonProperty("jeniskunjungan")]
                public int Jeniskunjungan;

                [JsonProperty("nomorreferensi")]
                public string Nomorreferensi;

                [JsonProperty("byrujukan")]
                public string Byrujukan;

                [JsonProperty("createdby")]
                public string CreatedBy;

                [JsonProperty("serviceunitid")]
                public string ServiceUnitID;
            }
        }

        public class Response
        {
            public class TResponse
            {
                [JsonProperty("nomorantrean")]
                public string Nomorantrean;

                [JsonProperty("angkaantrean")]
                public int Angkaantrean;

                [JsonProperty("kodebooking")]
                public string Kodebooking;

                [JsonProperty("norm")]
                public string Norm;

                [JsonProperty("namapoli")]
                public string Namapoli;

                [JsonProperty("namadokter")]
                public string Namadokter;

                [JsonProperty("estimasidilayani")]
                public Int64 Estimasidilayani;

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

                [JsonProperty("estimasijam")]
                public string EstimasiJam;

                [JsonProperty("showqrcode")]
                public string ShowQrCode;
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

    public class TambahAntrean
    {
        public class Response
        {
            public class TResponse : AmbilAntrean.Response.TResponse
            {
                [JsonProperty("nomorantreanregistrasi")]
                public string Nomorantreanregistrasi;
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
}