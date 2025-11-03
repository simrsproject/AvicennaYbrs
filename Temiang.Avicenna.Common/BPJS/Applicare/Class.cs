using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.Common.BPJS.Applicare
{
    public class UpdateKetersediaanTempatTidur
    {
        public class RootObject
        {
            public string kodekelas { get; set; }
            public string koderuang { get; set; }
            public string namaruang { get; set; }
            public string kapasitas { get; set; }
            public string tersedia { get; set; }
            public string tersediapria { get; set; }
            public string tersediawanita { get; set; }
            public string tersediapriawanita { get; set; }
        }
    }

    public class RuanganBaru
    {
        public class RootObject
        {
            public string kodekelas { get; set; }
            public string koderuang { get; set; }
            public string namaruang { get; set; }
            public string kapasitas { get; set; }
            public string tersedia { get; set; }
            public string tersediapria { get; set; }
            public string tersediawanita { get; set; }
            public string tersediapriawanita { get; set; }
        }
    }

    public class KetersediaanKamarRS
    {
        public class Metadata : BPJS.Metadata
        {
            [JsonProperty("totalitems")]
            public int Totalitems { get; set; }
        }

        public class List
        {
            [JsonProperty("tersedia")]
            public int Tersedia { get; set; }

            [JsonProperty("kodekelas")]
            public string Kodekelas { get; set; }

            [JsonProperty("namakelas")]
            public string Namakelas { get; set; }

            [JsonProperty("lastupdateall")]
            public string Lastupdateall { get; set; }

            [JsonProperty("tersediapria")]
            public int Tersediapria { get; set; }

            [JsonProperty("tersediawanita")]
            public int Tersediawanita { get; set; }

            [JsonProperty("koderuang")]
            public string Koderuang { get; set; }

            [JsonProperty("kodeppk")]
            public string Kodeppk { get; set; }

            [JsonProperty("tersediapriawanita")]
            public int Tersediapriawanita { get; set; }

            [JsonProperty("namaruang")]
            public string Namaruang { get; set; }

            [JsonProperty("rownumber")]
            public int Rownumber { get; set; }

            [JsonProperty("kapasitas")]
            public int Kapasitas { get; set; }

            [JsonProperty("lastupdate")]
            public string Lastupdate { get; set; }
        }

        public class Response
        {
            [JsonProperty("list")]
            public List[] List { get; set; }
        }

        public class KetersediaanKamar
        {

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class HapusRuangan
    {
        public class RootObject
        {
            public string kodekelas { get; set; }
            public string koderuang { get; set; }
        }
    }

    public class ReferensiKelas
    {
        public class Metadata : BPJS.Metadata
        {

            [JsonProperty("totalitems")]
            public int Totalitems { get; set; }
        }

        public class List
        {

            [JsonProperty("kodekelas")]
            public string Kodekelas { get; set; }

            [JsonProperty("namakelas")]
            public string Namakelas { get; set; }
        }

        public class Response
        {

            [JsonProperty("list")]
            public List[] List { get; set; }
        }

        public class Kelas
        {

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }
}
