using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.Monitoring.Klaim
{
    public class MetaData
    {
        public bool IsValid { get { return Code == "200"; } }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class Inacbg
    {

        [JsonProperty("kode")]
        public string Kode { get; set; }

        [JsonProperty("nama")]
        public string Nama { get; set; }
    }

    public class Biaya
    {

        [JsonProperty("byPengajuan")]
        public string ByPengajuan { get; set; }

        [JsonProperty("bySetujui")]
        public string BySetujui { get; set; }

        [JsonProperty("byTarifGruper")]
        public string ByTarifGruper { get; set; }

        [JsonProperty("byTarifRS")]
        public string ByTarifRS { get; set; }

        [JsonProperty("byTopup")]
        public string ByTopup { get; set; }
    }

    public class Peserta
    {

        [JsonProperty("nama")]
        public string Nama { get; set; }

        [JsonProperty("noKartu")]
        public string NoKartu { get; set; }

        [JsonProperty("noMR")]
        public string NoMR { get; set; }
    }

    public class Klaim
    {

        [JsonProperty("Inacbg")]
        public Inacbg Inacbg { get; set; }

        [JsonProperty("biaya")]
        public Biaya Biaya { get; set; }

        [JsonProperty("kelasRawat")]
        public string KelasRawat { get; set; }

        [JsonProperty("noFPK")]
        public string NoFPK { get; set; }

        [JsonProperty("noSEP")]
        public string NoSEP { get; set; }

        [JsonProperty("peserta")]
        public Peserta Peserta { get; set; }

        [JsonProperty("poli")]
        public string Poli { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("tglPulang")]
        public string TglPulang { get; set; }

        [JsonProperty("tglSep")]
        public string TglSep { get; set; }
    }

    public class Response
    {

        [JsonProperty("klaim")]
        public Klaim[] Klaim { get; set; }
    }

    public class DataKlaim
    {

        [JsonProperty("metaData")]
        public MetaData MetaData { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }
    }

}
