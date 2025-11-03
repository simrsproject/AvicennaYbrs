using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    public class Location
    {
        [JsonProperty("location")]
        public RefDisplay LocationItem { get; set; }

        [JsonProperty("extension")]
        public List<ExtensionLoc> Extension { get; set; }

        [JsonProperty("period", NullValueHandling = NullValueHandling.Ignore)]
        public Period Period { get; set; }
    }
    public class RefDisplay
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }
    }
    public class ExtensionLoc
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("extension")]
        public List<ExtensionItem> ExtensionItem { get; set; }
    }

    public class ExtensionItem
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("valueCodeableConcept")]
        public Code ValueCodeableConcept { get; set; }
    }

}