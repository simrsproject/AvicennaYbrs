using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.DiagnosaPRB
{
    public class MetaData
    {
        public bool IsValid { get { return Code == "200"; } }

        [JsonProperty("code")]
        public string Code;

        [JsonProperty("message")]
        public string Message;
    }

    public class List
    {
        [JsonProperty("kode")]
        public string Kode;

        [JsonProperty("nama")]
        public string Nama;
    }

    public class Response
    {
        [JsonProperty("list")]
        public List<List> List;
    }

    public class Root
    {
        [JsonProperty("metaData")]
        public MetaData MetaData;

        [JsonProperty("response")]
        public Response Response;
    }
}
