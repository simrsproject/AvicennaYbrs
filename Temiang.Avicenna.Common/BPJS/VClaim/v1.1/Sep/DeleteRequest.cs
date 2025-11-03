using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11.Sep.DeleteRequest
{
    public class TSep
    {
        [JsonProperty("noSep")]
        public string NoSep;

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
