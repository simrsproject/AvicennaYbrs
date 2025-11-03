using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.Common.Inacbg
{
    public class Metadata
    {
        public bool IsValid { get { return Code == "200"; } }

        public bool IsDuplicate { get { return Code == HttpStatusCode.BadRequest.ToString(); } }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
