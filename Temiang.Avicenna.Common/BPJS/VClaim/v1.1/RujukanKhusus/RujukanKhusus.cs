using Newtonsoft.Json;
using System.Collections.Generic;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v20.RujukanKhusus
{
    public class MetaData
    {
        public bool IsValid { get { return Code == "200"; } }

        [JsonProperty("code")]
        public string Code;

        [JsonProperty("message")]
        public string Message;
    }

    public class Select
    {
        public class Response
        {
            [JsonProperty("rujukan")]
            public List<Rujukan> Rujukan;
        }

        public class Root
        {
            [JsonProperty("metaData")]
            public MetaData MetaData;

            [JsonProperty("response")]
            public Response Response;
        }

        public class Rujukan
        {
            [JsonProperty("idrujukan")]
            public string Idrujukan;

            [JsonProperty("norujukan")]
            public string Norujukan;

            [JsonProperty("nokapst")]
            public string Nokapst;

            [JsonProperty("nmpst")]
            public string Nmpst;

            [JsonProperty("diagppk")]
            public string Diagppk;

            [JsonProperty("tglrujukan_awal")]
            public string TglrujukanAwal;

            [JsonProperty("tglrujukan_berakhir")]
            public string TglrujukanBerakhir;
        }
    }
}