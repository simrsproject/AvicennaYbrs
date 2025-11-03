using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.Common.BPJS.VClaim.Rujukan
{
    public class Search
    {

        public class Diagnosa
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Pelayanan
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Cob
        {

            [JsonProperty("nmAsuransi")]
            public string NmAsuransi { get; set; }

            [JsonProperty("noAsuransi")]
            public string NoAsuransi { get; set; }

            [JsonProperty("tglTAT")]
            public string TglTAT { get; set; }

            [JsonProperty("tglTMT")]
            public string TglTMT { get; set; }
        }

        public class HakKelas
        {

            [JsonProperty("keterangan")]
            public string Keterangan { get; set; }

            [JsonProperty("kode")]
            public string Kode { get; set; }
        }

        public class Informasi
        {

            [JsonProperty("dinsos")]
            public string Dinsos { get; set; }

            [JsonProperty("noSKTM")]
            public string NoSKTM { get; set; }

            [JsonProperty("prolanisPRB")]
            public string ProlanisPRB { get; set; }
        }

        public class JenisPeserta
        {

            [JsonProperty("keterangan")]
            public string Keterangan { get; set; }

            [JsonProperty("kode")]
            public string Kode { get; set; }
        }

        public class Mr
        {

            [JsonProperty("noMR")]
            public string NoMR { get; set; }

            [JsonProperty("noTelepon")]
            public string NoTelepon { get; set; }
        }

        public class ProvUmum
        {

            [JsonProperty("kdProvider")]
            public string KdProvider { get; set; }

            [JsonProperty("nmProvider")]
            public string NmProvider { get; set; }
        }

        public class StatusPeserta
        {

            [JsonProperty("keterangan")]
            public string Keterangan { get; set; }

            [JsonProperty("kode")]
            public string Kode { get; set; }
        }

        public class Umur
        {

            [JsonProperty("umurSaatPelayanan")]
            public string UmurSaatPelayanan { get; set; }

            [JsonProperty("umurSekarang")]
            public string UmurSekarang { get; set; }
        }

        public class Peserta
        {

            [JsonProperty("cob")]
            public Cob Cob { get; set; }

            [JsonProperty("hakKelas")]
            public HakKelas HakKelas { get; set; }

            [JsonProperty("informasi")]
            public Informasi Informasi { get; set; }

            [JsonProperty("jenisPeserta")]
            public JenisPeserta JenisPeserta { get; set; }

            [JsonProperty("mr")]
            public Mr Mr { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }

            [JsonProperty("nik")]
            public string Nik { get; set; }

            [JsonProperty("noKartu")]
            public string NoKartu { get; set; }

            [JsonProperty("pisa")]
            public string Pisa { get; set; }

            [JsonProperty("provUmum")]
            public ProvUmum ProvUmum { get; set; }

            [JsonProperty("sex")]
            public string Sex { get; set; }

            [JsonProperty("statusPeserta")]
            public StatusPeserta StatusPeserta { get; set; }

            [JsonProperty("tglCetakKartu")]
            public string TglCetakKartu { get; set; }

            [JsonProperty("tglLahir")]
            public string TglLahir { get; set; }

            [JsonProperty("tglTAT")]
            public string TglTAT { get; set; }

            [JsonProperty("tglTMT")]
            public string TglTMT { get; set; }

            [JsonProperty("umur")]
            public Umur Umur { get; set; }
        }

        public class PoliRujukan
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class ProvPerujuk
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Rujukan
        {

            [JsonProperty("diagnosa")]
            public Diagnosa Diagnosa { get; set; }

            [JsonProperty("keluhan")]
            public string Keluhan { get; set; }

            [JsonProperty("noKunjungan")]
            public string NoKunjungan { get; set; }

            [JsonProperty("pelayanan")]
            public Pelayanan Pelayanan { get; set; }

            [JsonProperty("peserta")]
            public Peserta Peserta { get; set; }

            [JsonProperty("poliRujukan")]
            public PoliRujukan PoliRujukan { get; set; }

            [JsonProperty("provPerujuk")]
            public ProvPerujuk ProvPerujuk { get; set; }

            [JsonProperty("tglKunjungan")]
            public string TglKunjungan { get; set; }
        }

        public class Response
        {

            [JsonProperty("rujukan")]
            public Rujukan Rujukan { get; set; }
        }

        public class Result : Metadata
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class Insert
    {

        public class TRujukan
        {
            public string noSep { get; set; }
            public string tglRujukan { get; set; }
            public string ppkDirujuk { get; set; }
            public string jnsPelayanan { get; set; }
            public string catatan { get; set; }
            public string diagRujukan { get; set; }
            public string tipeRujukan { get; set; }
            public string poliRujukan { get; set; }
            public string user { get; set; }
        }

        public class Request
        {
            public TRujukan t_rujukan { get; set; }
        }

        public class RootObject
        {
            public Request request { get; set; }
        }

        public class AsalRujukan
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Diagnosa
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
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

        public class PoliTujuan
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class TujuanRujukan
        {

            [JsonProperty("kode")]
            public string Kode { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }
        }

        public class Rujukan
        {

            [JsonProperty("AsalRujukan")]
            public AsalRujukan AsalRujukan { get; set; }

            [JsonProperty("diagnosa")]
            public Diagnosa Diagnosa { get; set; }

            [JsonProperty("noRujukan")]
            public string NoRujukan { get; set; }

            [JsonProperty("peserta")]
            public Peserta Peserta { get; set; }

            [JsonProperty("poliTujuan")]
            public PoliTujuan PoliTujuan { get; set; }

            [JsonProperty("tglRujukan")]
            public string TglRujukan { get; set; }

            [JsonProperty("tujuanRujukan")]
            public TujuanRujukan TujuanRujukan { get; set; }
        }

        public class Response
        {

            [JsonProperty("rujukan")]
            public Rujukan Rujukan { get; set; }
        }

        public class Result : Metadata
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }
    }

    public class Update
    {

        public class TRujukan
        {
            public string noRujukan { get; set; }
            public string ppkDirujuk { get; set; }
            public string tipe { get; set; }
            public string jnsPelayanan { get; set; }
            public string catatan { get; set; }
            public string diagRujukan { get; set; }
            public string tipeRujukan { get; set; }
            public string poliRujukan { get; set; }
            public string user { get; set; }
        }

        public class Request
        {
            public TRujukan t_rujukan { get; set; }
        }

        public class RootObject
        {
            public Request request { get; set; }
        }

        public class Response : Metadata
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public string Data { get; set; }
        }
    }
}
