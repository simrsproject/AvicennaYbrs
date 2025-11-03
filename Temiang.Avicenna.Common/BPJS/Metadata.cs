using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Temiang.Avicenna.Common.BPJS
{
    public class Metadata
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Code)) return false;
                return new[] { "200" }.Contains(Code.ToString());
            }
        }

        public bool IsAntrolValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Code)) return false;
                return new[] { "200", "1" }.Contains(Code.ToString());
            }
        }

        public bool IsApolValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Code)) return false;
                return new[] { "200", "1" }.Contains(Code.ToString());
            }
        }
    }

    public class MetadataResponse
    {
        [JsonProperty("metaData")]
        //[JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
}
