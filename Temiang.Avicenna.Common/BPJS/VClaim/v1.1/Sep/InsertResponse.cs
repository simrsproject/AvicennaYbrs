using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.Sep.InsertResponse
{

    public class MetaData
    {
        public bool IsValid { get { return Code == "200"; } }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class Peserta
    {

        [JsonProperty("asuransi")]
        public string Asuransi { get; set; }

        [JsonProperty("hakKelas")]
        public string HakKelas { get; set; }

        [JsonProperty("jnsPeserta")]
        public string JnsPeserta { get; set; }

        [JsonProperty("kelamin")]
        public string Kelamin { get; set; }

        [JsonProperty("nama")]
        public string Nama { get; set; }

        [JsonProperty("noKartu")]
        public string NoKartu { get; set; }

        [JsonProperty("noMr")]
        public string NoMr { get; set; }

        [JsonProperty("tglLahir")]
        public string TglLahir { get; set; }
    }

    public class Informasi
    {

        [JsonProperty("Dinsos")]
        public object Dinsos { get; set; }

        [JsonProperty("prolanisPRB")]
        public object ProlanisPRB { get; set; }

        [JsonProperty("noSKTM")]
        public object NoSKTM { get; set; }
    }

    public class Sep
    {

        [JsonProperty("catatan")]
        public string Catatan { get; set; }

        [JsonProperty("diagnosa")]
        public string Diagnosa { get; set; }

        [JsonProperty("jnsPelayanan")]
        public string JnsPelayanan { get; set; }

        [JsonProperty("kelasRawat")]
        public string KelasRawat { get; set; }

        [JsonProperty("noSep")]
        public string NoSep { get; set; }

        [JsonProperty("penjamin")]
        public string Penjamin { get; set; }

        [JsonProperty("peserta")]
        public Peserta Peserta { get; set; }

        [JsonProperty("informasi:")]
        public Informasi Informasi { get; set; }

        [JsonProperty("poli")]
        public string Poli { get; set; }

        [JsonProperty("poliEksekutif")]
        public string PoliEksekutif { get; set; }

        [JsonProperty("tglSep")]
        public string TglSep { get; set; }
    }

    public class Response
    {

        [JsonProperty("sep")]
        public Sep Sep { get; set; }
    }

    public class Insert
    {

        [JsonProperty("metaData")]
        public MetaData MetaData { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }
    }

}

namespace Temiang.Avicenna.Common.BPJS.VClaim.v20.Sep.InsertResponse
{
    public class MetaData
    {
        public bool IsValid { get { return Code == "200"; } }

        [JsonProperty("code")]
        public string Code;

        [JsonProperty("message")]
        public string Message;
    }

    public class Peserta
    {
        [JsonProperty("asuransi")]
        public string Asuransi;

        [JsonProperty("hakKelas")]
        public string HakKelas;

        [JsonProperty("jnsPeserta")]
        public string JnsPeserta;

        [JsonProperty("kelamin")]
        public string Kelamin;

        [JsonProperty("nama")]
        public string Nama;

        [JsonProperty("noKartu")]
        public string NoKartu;

        [JsonProperty("noMr")]
        public string NoMr;

        [JsonProperty("tglLahir")]
        public string TglLahir;
    }

    public class Informasi
    {
        [JsonProperty("Dinsos")]
        public object Dinsos;

        [JsonProperty("prolanisPRB")]
        public object ProlanisPRB;

        [JsonProperty("noSKTM")]
        public object NoSKTM;
    }

    public class Sep
    {
        [JsonProperty("catatan")]
        public string Catatan;

        [JsonProperty("diagnosa")]
        public string Diagnosa;

        [JsonProperty("jnsPelayanan")]
        public string JnsPelayanan;

        [JsonProperty("kelasRawat")]
        public string KelasRawat;

        [JsonProperty("noSep")]
        public string NoSep;

        [JsonProperty("penjamin")]
        public string Penjamin;

        [JsonProperty("peserta")]
        public Peserta Peserta;

        [JsonProperty("informasi:")]
        public Informasi Informasi;

        [JsonProperty("poli")]
        public string Poli;

        [JsonProperty("poliEksekutif")]
        public string PoliEksekutif;

        [JsonProperty("tglSep")]
        public string TglSep;
    }

    public class Response
    {
        [JsonProperty("sep")]
        public Sep Sep;
    }

    public class Root
    {
        [JsonProperty("metaData")]
        public MetaData MetaData;

        [JsonProperty("response")]
        public Response Response;
    }
}