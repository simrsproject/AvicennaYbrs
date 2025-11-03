using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.Monitoring.Kunjungan
{

    public class MetaData
    {
        public bool IsValid { get { return Code == "200"; } }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class Response
    {
        [JsonProperty("sep")]
        public List<Sep> Sep { get; set; }
    }

    public class DataKunjungan
    {
        [JsonProperty("metaData")]
        public MetaData MetaData { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }
    }

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

        [JsonProperty("noRujukan")]
        public string NoRujukan { get; set; }

        [JsonProperty("poli")]
        public string Poli { get; set; }

        [JsonProperty("tglPlgSep")]
        public string TglPlgSep { get; set; }

        [JsonProperty("tglSep")]
        public string TglSep { get; set; }
    }
}
