using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.Sep.InsertRequest
{
    public class Rujukan
    {
        public string asalRujukan { get; set; }
        public string tglRujukan { get; set; }
        public string noRujukan { get; set; }
        public string ppkRujukan { get; set; }
    }

    public class Poli
    {
        public string tujuan { get; set; }
        public string eksekutif { get; set; }
    }

    public class Cob
    {
        public string cob { get; set; }
    }

    public class Katarak
    {
        public string katarak { get; set; }
    }

    public class LokasiLaka
    {
        public string kdPropinsi { get; set; }
        public string kdKabupaten { get; set; }
        public string kdKecamatan { get; set; }
    }

    public class Suplesi
    {
        public string suplesi { get; set; }
        public string noSepSuplesi { get; set; }
        public LokasiLaka lokasiLaka { get; set; }
    }

    public class Penjamin
    {
        public string penjamin { get; set; }
        public string tglKejadian { get; set; }
        public string keterangan { get; set; }
        public Suplesi suplesi { get; set; }
    }

    public class Jaminan
    {
        public string lakaLantas { get; set; }
        public Penjamin penjamin { get; set; }
    }

    public class Skdp
    {
        public string noSurat { get; set; }
        public string kodeDPJP { get; set; }
    }

    public class TSep
    {
        public string noKartu { get; set; }
        public string tglSep { get; set; }
        public string ppkPelayanan { get; set; }
        public string jnsPelayanan { get; set; }
        public string klsRawat { get; set; }
        public string noMR { get; set; }
        public Rujukan rujukan { get; set; }
        public string catatan { get; set; }
        public string diagAwal { get; set; }
        public Poli poli { get; set; }
        public Cob cob { get; set; }
        public Katarak katarak { get; set; }
        public Jaminan jaminan { get; set; }
        public Skdp skdp { get; set; }
        public string noTelp { get; set; }
        public string user { get; set; }
    }

    public class Request
    {
        public TSep t_sep { get; set; }
    }

    public class RootObject
    {
        public Request request { get; set; }
    }
}

namespace Temiang.Avicenna.Common.BPJS.VClaim.v20.Sep.InsertRequest
{
    public class KlsRawat
    {
        [JsonProperty("klsRawatHak")]
        public string KlsRawatHak;

        [JsonProperty("klsRawatNaik")]
        public string KlsRawatNaik;

        [JsonProperty("pembiayaan")]
        public string Pembiayaan;

        [JsonProperty("penanggungJawab")]
        public string PenanggungJawab;
    }

    public class Rujukan
    {
        [JsonProperty("asalRujukan")]
        public string AsalRujukan;

        [JsonProperty("tglRujukan")]
        public string TglRujukan;

        [JsonProperty("noRujukan")]
        public string NoRujukan;

        [JsonProperty("ppkRujukan")]
        public string PpkRujukan;
    }

    public class Poli
    {
        [JsonProperty("tujuan")]
        public string Tujuan;

        [JsonProperty("eksekutif")]
        public string Eksekutif;
    }

    public class TCob
    {
        [JsonProperty("cob")]
        public string Cob;
    }

    public class TKatarak
    {
        [JsonProperty("katarak")]
        public string Katarak;
    }

    public class LokasiLaka
    {
        [JsonProperty("kdPropinsi")]
        public string KdPropinsi;

        [JsonProperty("kdKabupaten")]
        public string KdKabupaten;

        [JsonProperty("kdKecamatan")]
        public string KdKecamatan;
    }

    public class TSuplesi
    {
        [JsonProperty("suplesi")]
        public string Suplesi;

        [JsonProperty("noSepSuplesi")]
        public string NoSepSuplesi;

        [JsonProperty("lokasiLaka")]
        public LokasiLaka LokasiLaka;
    }

    public class Penjamin
    {
        [JsonProperty("tglKejadian")]
        public string TglKejadian;

        [JsonProperty("keterangan")]
        public string Keterangan;

        [JsonProperty("suplesi")]
        public TSuplesi Suplesi;
    }

    public class Jaminan
    {
        [JsonProperty("lakaLantas")]
        public string LakaLantas;

        [JsonProperty("noLP")]
        public string NoLp;

        [JsonProperty("penjamin")]
        public Penjamin Penjamin;
    }

    public class Skdp
    {
        [JsonProperty("noSurat")]
        public string NoSurat;

        [JsonProperty("kodeDPJP")]
        public string KodeDPJP;
    }

    public class TSep
    {
        [JsonProperty("noKartu")]
        public string NoKartu;

        [JsonProperty("tglSep")]
        public string TglSep;

        [JsonProperty("ppkPelayanan")]
        public string PpkPelayanan;

        [JsonProperty("jnsPelayanan")]
        public string JnsPelayanan;

        [JsonProperty("klsRawat")]
        public KlsRawat KlsRawat;

        [JsonProperty("noMR")]
        public string NoMR;

        [JsonProperty("rujukan")]
        public Rujukan Rujukan;

        [JsonProperty("catatan")]
        public string Catatan;

        [JsonProperty("diagAwal")]
        public string DiagAwal;

        [JsonProperty("poli")]
        public Poli Poli;

        [JsonProperty("cob")]
        public TCob Cob;

        [JsonProperty("katarak")]
        public TKatarak Katarak;

        [JsonProperty("jaminan")]
        public Jaminan Jaminan;

        [JsonProperty("tujuanKunj")]
        public string TujuanKunj;

        [JsonProperty("flagProcedure")]
        public string FlagProcedure;

        [JsonProperty("kdPenunjang")]
        public string KdPenunjang;

        [JsonProperty("assesmentPel")]
        public string AssesmentPel;

        [JsonProperty("skdp")]
        public Skdp Skdp;

        [JsonProperty("dpjpLayan")]
        public string DpjpLayan;

        [JsonProperty("noTelp")]
        public string NoTelp;

        [JsonProperty("user")]
        public string User;
    }

    public class TRequest
    {
        [JsonProperty("t_sep")]
        public TSep TSep;
    }

    public class Root
    {
        [JsonProperty("request")]
        public TRequest Request;
    }
}