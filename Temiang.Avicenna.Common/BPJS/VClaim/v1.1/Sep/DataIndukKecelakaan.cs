using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.Sep
{
    public class DataIndukKecelakaan
    {
        public class MetaData
        {
            [JsonProperty("code")]
            public string Code;

            [JsonProperty("message")]
            public string Message;
        }

        public class List
        {
            [JsonProperty("noSEP")]
            public string NoSEP;

            [JsonProperty("tglKejadian")]
            public string TglKejadian;

            [JsonProperty("ppkPelSEP")]
            public string PpkPelSEP;

            [JsonProperty("kdProp")]
            public string KdProp;

            [JsonProperty("kdKab")]
            public string KdKab;

            [JsonProperty("kdKec")]
            public string KdKec;

            [JsonProperty("ketKejadian")]
            public string KetKejadian;

            [JsonProperty("noSEPSuplesi")]
            public string NoSEPSuplesi;
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
}
