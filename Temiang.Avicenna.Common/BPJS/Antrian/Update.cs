using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.Antrian.Update
{
    public class JadwalDokter
    {
        public class Request
        {
            public class Jadwal
            {
                [JsonProperty("hari")]
                public string Hari;

                [JsonProperty("buka")]
                public string Buka;

                [JsonProperty("tutup")]
                public string Tutup;
            }

            public class Root
            {
                [JsonProperty("kodepoli")]
                public string Kodepoli;

                [JsonProperty("kodesubspesialis")]
                public string Kodesubspesialis;

                [JsonProperty("kodedokter")]
                public int Kodedokter;

                [JsonProperty("jadwal")]
                public List<Jadwal> Jadwal;
            }
        }

        public class Response : Metadata
        {

        }
    }

    public class WaktuAntrian
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("kodebooking")]
                public string Kodebooking;

                [JsonProperty("taskid")]
                public int Taskid;

                [JsonProperty("waktu")]
                public Int64 Waktu;

                [JsonProperty("jenisresep")]
                public string Jenisresep;
            }
        }

        public class Response : Metadata
        {

        }
    }

    public class WaktuAntrianManual
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("kodebooking")]
                public string Kodebooking;

                [JsonProperty("taskid")]
                public int Taskid;

                [JsonProperty("waktu")]
                public Int64 Waktu;
            }
        }

        public class Response : Metadata
        {

        }
    }

    public class BatalAntrian
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("kodebooking")]
                public string Kodebooking;

                [JsonProperty("keterangan")]
                public string Keterangan;
            }
        }

        public class Response : Metadata
        {

        }
    }
}
