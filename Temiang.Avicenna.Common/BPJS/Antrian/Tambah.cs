using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.Antrian
{
    public class Tambah
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("kodebooking")]
                public string Kodebooking;

                [JsonProperty("jenispasien")]
                public string Jenispasien;

                [JsonProperty("nomorkartu")]
                public string Nomorkartu;

                [JsonProperty("nik")]
                public string Nik;

                [JsonProperty("nohp")]
                public string Nohp;

                [JsonProperty("kodepoli")]
                public string Kodepoli;

                [JsonProperty("namapoli")]
                public string Namapoli;

                [JsonProperty("pasienbaru")]
                public int Pasienbaru;

                [JsonProperty("norm")]
                public string Norm;

                [JsonProperty("tanggalperiksa")]
                public string Tanggalperiksa;

                [JsonProperty("kodedokter")]
                public int Kodedokter;

                [JsonProperty("namadokter")]
                public string Namadokter;

                [JsonProperty("jampraktek")]
                public string Jampraktek;

                [JsonProperty("jeniskunjungan")]
                public int Jeniskunjungan;

                [JsonProperty("nomorreferensi")]
                public string Nomorreferensi;

                [JsonProperty("nomorantrean")]
                public string Nomorantrean;

                [JsonProperty("angkaantrean")]
                public int Angkaantrean;

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
            }
        }

        public class Response : Metadata
        {

        }
    }

    public class Farmasi
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("kodebooking")]
                public string Kodebooking;

                [JsonProperty("jenisresep")]
                public string Jenisresep;

                [JsonProperty("nomorantrean")]
                public string Nomorantrean;

                [JsonProperty("keterangan")]
                public string Keterangan;
            }
        }

        public class Response : Metadata
        {

        }
    }
}
