using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.Antrian.Referensi
{
    public class Poli : Metadata
    {
        public class List
        {
            [JsonProperty("nmpoli")]
            public string Nmpoli;

            [JsonProperty("nmsubspesialis")]
            public string Nmsubspesialis;

            [JsonProperty("kdsubspesialis")]
            public string Kdsubspesialis;

            [JsonProperty("kdpoli")]
            public string Kdpoli;
        }

        public class Response
        {
            [JsonProperty("list")]
            public List<List> List;
        }

        public class Root
        {
            [JsonProperty("metadata")]
            public Metadata Metadata;

            [JsonProperty("response")]
            public Response Response;
        }
    }

    public class Dokter : Metadata
    {
        public class List
        {
            [JsonProperty("namadokter")]
            public string Namadokter;

            [JsonProperty("kodedokter")]
            public int Kodedokter;
        }

        public class Response
        {
            [JsonProperty("list")]
            public List<List> List;
        }

        public class Root
        {
            [JsonProperty("metadata")]
            public Metadata Metadata;

            [JsonProperty("response")]
            public Response Response;
        }
    }

    public class JadwalDokter : Metadata
    {
        public class List
        {
            [JsonProperty("kodesubspesialis")]
            public string Kodesubspesialis;

            [JsonProperty("hari")]
            public int Hari;

            [JsonProperty("kapasitaspasien")]
            public int Kapasitaspasien;

            [JsonProperty("libur")]
            public int Libur;

            [JsonProperty("namahari")]
            public string Namahari;

            [JsonProperty("jadwal")]
            public string Jadwal;

            [JsonProperty("namasubspesialis")]
            public string Namasubspesialis;

            [JsonProperty("namadokter")]
            public string Namadokter;

            [JsonProperty("kodepoli")]
            public string Kodepoli;

            [JsonProperty("namapoli")]
            public string Namapoli;

            [JsonProperty("kodedokter")]
            public int Kodedokter;
        }

        public class Response
        {
            [JsonProperty("list")]
            public List<List> List;
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metadata")]
            public Metadata Metadata;
        }
    }

    public class PoliFingerPrint : Metadata
    {
        public class List
        {
            [JsonProperty("kodesubspesialis")]
            public string Kodesubspesialis;

            [JsonProperty("namasubspesialis")]
            public string Namasubspesialis;

            [JsonProperty("kodepoli")]
            public string Kodepoli;

            [JsonProperty("namapoli")]
            public string Namapoli;
        }

        public class Response
        {
            [JsonProperty("list")]
            public List<List> List;
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metadata")]
            public Metadata Metadata;
        }
    }

    public class PasienFingerPrint : Metadata
    {
        public class Response
        {
            [JsonProperty("nomorkartu")]
            public string Nomorkartu;

            [JsonProperty("nik")]
            public string Nik;

            [JsonProperty("tgllahir")]
            public string Tgllahir;

            [JsonProperty("daftarfp")]
            public string Daftarfp;
        }

        public class Root
        {
            [JsonProperty("response")]
            public Response Response;

            [JsonProperty("metadata")]
            public Metadata Metadata;
        }
    }

}
