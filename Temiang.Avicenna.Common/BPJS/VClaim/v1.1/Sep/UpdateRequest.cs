using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.Sep.UpdateRequest
{
    public class Update
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

        public class Skdp
        {
            public string noSurat { get; set; }
            public string kodeDPJP { get; set; }
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

        public class TSep
        {
            public string noSep { get; set; }
            public string klsRawat { get; set; }
            public string noMR { get; set; }
            public Rujukan rujukan { get; set; }
            public string catatan { get; set; }
            public string diagAwal { get; set; }
            public Poli poli { get; set; }
            public Cob cob { get; set; }
            public Katarak katarak { get; set; }
            public Skdp skdp { get; set; }
            public Jaminan jaminan { get; set; }
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

    public class UpdateTanggalPulang
    {
        public class TSep
        {
            public string noSep { get; set; }
            public string tglPulang { get; set; }
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
}

namespace Temiang.Avicenna.Common.BPJS.VClaim.v20.Sep.UpdateRequest
{
    public class Update
    {
        public class TCob
        {
            [JsonProperty("cob")]
            public string Cob { get; set; }
        }

        public class Jaminan
        {
            [JsonProperty("lakaLantas")]
            public string LakaLantas { get; set; }

            [JsonProperty("penjamin")]
            public Penjamin Penjamin { get; set; }
        }

        public class TKatarak
        {
            [JsonProperty("katarak")]
            public string Katarak { get; set; }
        }

        public class KlsRawat
        {
            [JsonProperty("klsRawatHak")]
            public string KlsRawatHak { get; set; }

            [JsonProperty("klsRawatNaik")]
            public string KlsRawatNaik { get; set; }

            [JsonProperty("pembiayaan")]
            public string Pembiayaan { get; set; }

            [JsonProperty("penanggungJawab")]
            public string PenanggungJawab { get; set; }
        }

        public class LokasiLaka
        {
            [JsonProperty("kdPropinsi")]
            public string KdPropinsi { get; set; }

            [JsonProperty("kdKabupaten")]
            public string KdKabupaten { get; set; }

            [JsonProperty("kdKecamatan")]
            public string KdKecamatan { get; set; }
        }

        public class Penjamin
        {
            [JsonProperty("tglKejadian")]
            public string TglKejadian { get; set; }

            [JsonProperty("keterangan")]
            public string Keterangan { get; set; }

            [JsonProperty("suplesi")]
            public TSuplesi Suplesi { get; set; }
        }

        public class Poli
        {
            [JsonProperty("tujuan")]
            public string Tujuan { get; set; }

            [JsonProperty("eksekutif")]
            public string Eksekutif { get; set; }
        }

        public class Request
        {
            [JsonProperty("t_sep")]
            public TSep TSep { get; set; }
        }

        public class Root
        {
            [JsonProperty("request")]
            public Request Request { get; set; }
        }

        public class TSuplesi
        {
            [JsonProperty("suplesi")]
            public string Suplesi { get; set; }

            [JsonProperty("noSepSuplesi")]
            public string NoSepSuplesi { get; set; }

            [JsonProperty("lokasiLaka")]
            public LokasiLaka LokasiLaka { get; set; }
        }

        public class TSep
        {
            [JsonProperty("noSep")]
            public string NoSep { get; set; }

            [JsonProperty("klsRawat")]
            public KlsRawat KlsRawat { get; set; }

            [JsonProperty("noMR")]
            public string NoMR { get; set; }

            [JsonProperty("catatan")]
            public string Catatan { get; set; }

            [JsonProperty("diagAwal")]
            public string DiagAwal { get; set; }

            [JsonProperty("poli")]
            public Poli Poli { get; set; }

            [JsonProperty("cob")]
            public TCob Cob { get; set; }

            [JsonProperty("katarak")]
            public TKatarak Katarak { get; set; }

            [JsonProperty("jaminan")]
            public Jaminan Jaminan { get; set; }

            [JsonProperty("dpjpLayan")]
            public string DpjpLayan { get; set; }

            [JsonProperty("noTelp")]
            public string NoTelp { get; set; }

            [JsonProperty("user")]
            public string User { get; set; }
        }
    }

    public class UpdateTanggalPulang
    {
        public class TSep
        {
            [JsonProperty("noSep")]
            public string NoSep;

            [JsonProperty("statusPulang")]
            public string StatusPulang;

            [JsonProperty("noSuratMeninggal")]
            public string NoSuratMeninggal;

            [JsonProperty("tglMeninggal")]
            public string TglMeninggal;

            [JsonProperty("tglPulang")]
            public string TglPulang;

            [JsonProperty("noLPManual")]
            public string NoLPManual;

            [JsonProperty("user")]
            public string User;
        }

        public class Request
        {
            [JsonProperty("t_sep")]
            public TSep TSep;
        }

        public class Root
        {
            [JsonProperty("request")]
            public Request Request;
        }
    }
}