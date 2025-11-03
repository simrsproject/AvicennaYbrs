using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Details
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Issue
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("details")]
        public Details Details { get; set; }

        [JsonProperty("diagnostics")]
        public string Diagnostics { get; set; }

        [JsonProperty("expression")]
        public List<string> Expression { get; set; }

        [JsonProperty("severity")]
        public string Severity { get; set; }
    }

    public class IssueResponse
    {
        [JsonProperty("issue")]
        public List<Issue> Issue { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }
    }



}
