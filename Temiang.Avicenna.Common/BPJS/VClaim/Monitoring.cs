using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.VClaim.Monitoring
{
    public class DataKunjungan
    {

        public class Sep
        {

            [JsonProperty("diagnosa")]
            public string Diagnosa { get; set; }

            [JsonProperty("jnsPelayanan")]
            public string JnsPelayanan { get; set; }

            [JsonProperty("kelasRawat")]
            public string KelasRawat { get; set; }

            [JsonProperty("nama")]
            public string Nama { get; set; }

            [JsonProperty("noKartu")]
            public string NoKartu { get; set; }

            [JsonProperty("noSep")]
            public string NoSep { get; set; }

            [JsonProperty("poli")]
            public object Poli { get; set; }

            [JsonProperty("tglPlgSep")]
            public string TglPlgSep { get; set; }

            [JsonProperty("tglSep")]
            public string TglSep { get; set; }
        }

        public class Data
        {

            [JsonProperty("sep")]
            public Sep[] Sep { get; set; }
        }

        public class Response : Metadata
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Data Data { get; set; }
        }
    }

    public class DataKlaim
    {

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

        public class Data
        {

            [JsonProperty("klaim")]
            public Klaim[] Klaim { get; set; }
        }

        public class Response : Metadata
        {

            [JsonProperty("metaData")]
            public Metadata MetaData { get; set; }

            [JsonProperty("response")]
            public Data Data { get; set; }
        }
    }
}
