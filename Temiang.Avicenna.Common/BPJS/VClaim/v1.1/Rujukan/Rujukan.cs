using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.Rujukan
{
    public class Select
    {
        public class MetaData
        {
            public bool IsValid { get { return Code == "200"; } }

            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }
        }

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

        public class Rujukan2
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
            public Rujukan2 Rujukan { get; set; }
        }

        public class ResponseList
        {

            [JsonProperty("rujukan")]
            public Rujukan2[] Rujukan { get; set; }
        }

        public class Rujukan
        {

            [JsonProperty("metaData")]
            public MetaData MetaData { get; set; }

            [JsonProperty("response")]
            public Response Response { get; set; }
        }

        public class RujukanList
        {

            [JsonProperty("metaData")]
            public MetaData MetaData { get; set; }

            [JsonProperty("response")]
            public ResponseList Response { get; set; }
        }
    }

    public class Insert
    {
        public class Request
        {
            public class TRujukan
            {
                [JsonProperty("noSep")]
                public string NoSep;

                [JsonProperty("tglRujukan")]
                public string TglRujukan;

                [JsonProperty("ppkDirujuk")]
                public string PpkDirujuk;

                [JsonProperty("jnsPelayanan")]
                public string JnsPelayanan;

                [JsonProperty("catatan")]
                public string Catatan;

                [JsonProperty("diagRujukan")]
                public string DiagRujukan;

                [JsonProperty("tipeRujukan")]
                public string TipeRujukan;

                [JsonProperty("poliRujukan")]
                public string PoliRujukan;

                [JsonProperty("user")]
                public string User;
            }

            public class TRequest
            {
                [JsonProperty("t_rujukan")]
                public TRujukan TRujukan;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class AsalRujukan
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class Diagnosa
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
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

            public class PoliTujuan
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class TujuanRujukan
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class Rujukan
            {
                [JsonProperty("AsalRujukan")]
                public AsalRujukan AsalRujukan;

                [JsonProperty("diagnosa")]
                public Diagnosa Diagnosa;

                [JsonProperty("noRujukan")]
                public string NoRujukan;

                [JsonProperty("peserta")]
                public Peserta Peserta;

                [JsonProperty("poliTujuan")]
                public PoliTujuan PoliTujuan;

                [JsonProperty("tglRujukan")]
                public string TglRujukan;

                [JsonProperty("tujuanRujukan")]
                public TujuanRujukan TujuanRujukan;
            }

            public class TResponse
            {
                [JsonProperty("rujukan")]
                public Rujukan Rujukan;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public Select.MetaData MetaData;

                [JsonProperty("response")]
                public TResponse Response;
            }
        }
    }

    public class Update
    {
        public class Request
        {
            public class TRujukan
            {
                [JsonProperty("noRujukan")]
                public string NoRujukan;

                [JsonProperty("ppkDirujuk")]
                public string PpkDirujuk;

                [JsonProperty("tipe")]
                public string Tipe;

                [JsonProperty("jnsPelayanan")]
                public string JnsPelayanan;

                [JsonProperty("catatan")]
                public string Catatan;

                [JsonProperty("diagRujukan")]
                public string DiagRujukan;

                [JsonProperty("tipeRujukan")]
                public string TipeRujukan;

                [JsonProperty("poliRujukan")]
                public string PoliRujukan;

                [JsonProperty("user")]
                public string User;
            }

            public class TRequest
            {
                [JsonProperty("t_rujukan")]
                public TRujukan TRujukan;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class Root
            {
                [JsonProperty("metaData")]
                public Select.MetaData MetaData;

                [JsonProperty("response")]
                public string Response;
            }
        }
    }

    public class Delete
    {
        public class Request
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
            public class TRujukan
            {
                [JsonProperty("noRujukan")]
                public string NoRujukan;

                [JsonProperty("user")]
                public string User;
            }

            public class TRequest
            {
                [JsonProperty("t_rujukan")]
                public TRujukan TRujukan;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }
    }
}

namespace Temiang.Avicenna.Common.BPJS.VClaim.v20.Rujukan
{
    public class Insert
    {
        public class Request
        {
            public class TRujukan
            {
                [JsonProperty("noSep")]
                public string NoSep;

                [JsonProperty("tglRujukan")]
                public string TglRujukan;

                [JsonProperty("tglRencanaKunjungan")]
                public string TglRencanaKunjungan;

                [JsonProperty("ppkDirujuk")]
                public string PpkDirujuk;

                [JsonProperty("jnsPelayanan")]
                public string JnsPelayanan;

                [JsonProperty("catatan")]
                public string Catatan;

                [JsonProperty("diagRujukan")]
                public string DiagRujukan;

                [JsonProperty("tipeRujukan")]
                public string TipeRujukan;

                [JsonProperty("poliRujukan")]
                public string PoliRujukan;

                [JsonProperty("user")]
                public string User;
            }

            public class TRequest
            {
                [JsonProperty("t_rujukan")]
                public TRujukan TRujukan;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class MetaData
            {
                public bool IsValid { get { return Code == "200"; } }

                [JsonProperty("code")]
                public string Code;

                [JsonProperty("message")]
                public string Message;
            }

            public class AsalRujukan
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class Diagnosa
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class Peserta
            {
                [JsonProperty("asuransi")]
                public string Asuransi;

                [JsonProperty("hakKelas")]
                public object HakKelas;

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

            public class PoliTujuan
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class TujuanRujukan
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class Rujukan
            {
                [JsonProperty("AsalRujukan")]
                public AsalRujukan AsalRujukan;

                [JsonProperty("diagnosa")]
                public Diagnosa Diagnosa;

                [JsonProperty("noRujukan")]
                public string NoRujukan;

                [JsonProperty("peserta")]
                public Peserta Peserta;

                [JsonProperty("poliTujuan")]
                public PoliTujuan PoliTujuan;

                [JsonProperty("tglBerlakuKunjungan")]
                public string TglBerlakuKunjungan;

                [JsonProperty("tglRencanaKunjungan")]
                public string TglRencanaKunjungan;

                [JsonProperty("tglRujukan")]
                public string TglRujukan;

                [JsonProperty("tujuanRujukan")]
                public TujuanRujukan TujuanRujukan;
            }

            public class TResponse
            {
                [JsonProperty("rujukan")]
                public Rujukan Rujukan;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public TResponse Response;
            }
        }
    }

    public class Update
    {
        public class Request
        {
            public class TRujukan
            {
                [JsonProperty("noRujukan")]
                public string NoRujukan;

                [JsonProperty("tglRujukan")]
                public string TglRujukan;

                [JsonProperty("tglRencanaKunjungan")]
                public string TglRencanaKunjungan;

                [JsonProperty("ppkDirujuk")]
                public string PpkDirujuk;

                [JsonProperty("jnsPelayanan")]
                public string JnsPelayanan;

                [JsonProperty("catatan")]
                public string Catatan;

                [JsonProperty("diagRujukan")]
                public string DiagRujukan;

                [JsonProperty("tipeRujukan")]
                public string TipeRujukan;

                [JsonProperty("poliRujukan")]
                public string PoliRujukan;

                [JsonProperty("user")]
                public string User;
            }

            public class TRequest
            {
                [JsonProperty("t_rujukan")]
                public TRujukan TRujukan;
            }

            public class Root
            {
                [JsonProperty("request")]
                public TRequest Request;
            }
        }

        public class Response
        {
            public class MetaData
            {
                public bool IsValid { get { return Code == "200"; } }

                [JsonProperty("code")]
                public string Code;

                [JsonProperty("message")]
                public string Message;
            }

            public class AsalRujukan
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class Diagnosa
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class Peserta
            {
                [JsonProperty("asuransi")]
                public string Asuransi;

                [JsonProperty("hakKelas")]
                public object HakKelas;

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

            public class PoliTujuan
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class TujuanRujukan
            {
                [JsonProperty("kode")]
                public string Kode;

                [JsonProperty("nama")]
                public string Nama;
            }

            public class Rujukan
            {
                [JsonProperty("AsalRujukan")]
                public AsalRujukan AsalRujukan;

                [JsonProperty("diagnosa")]
                public Diagnosa Diagnosa;

                [JsonProperty("noRujukan")]
                public string NoRujukan;

                [JsonProperty("peserta")]
                public Peserta Peserta;

                [JsonProperty("poliTujuan")]
                public PoliTujuan PoliTujuan;

                [JsonProperty("tglBerlakuKunjungan")]
                public string TglBerlakuKunjungan;

                [JsonProperty("tglRencanaKunjungan")]
                public string TglRencanaKunjungan;

                [JsonProperty("tglRujukan")]
                public string TglRujukan;

                [JsonProperty("tujuanRujukan")]
                public TujuanRujukan TujuanRujukan;
            }

            public class TResponse
            {
                [JsonProperty("rujukan")]
                public Rujukan Rujukan;
            }

            public class Root
            {
                [JsonProperty("metaData")]
                public MetaData MetaData;

                [JsonProperty("response")]
                public TResponse Response;
            }
        }
    }

    public class SelectDataJumlahSEPRujukan
    {
        public class MetaData
        {
            public bool IsValid { get { return Code == "200"; } }

            [JsonProperty("code")]
            public string Code;

            [JsonProperty("message")]
            public string Message;
        }

        public class TResponse
        {
            [JsonProperty("jumlahSEP")]
            public string JumlahSEP;
        }

        public class Root
        {
            [JsonProperty("metaData")]
            public MetaData MetaData;

            [JsonProperty("response")]
            public TResponse Response;
        }
    }
}