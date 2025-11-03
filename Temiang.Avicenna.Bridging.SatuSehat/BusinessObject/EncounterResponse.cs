using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class EncounterResponse
    {
        [JsonProperty("class")]
        public Class Class { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("identifier")]
        public List<Identifier> Identifier { get; set; }

        [JsonProperty("location")]
        public List<Location> Location { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("participant")]
        public List<Participant> Participant { get; set; }

        [JsonProperty("period")]
        public Period Period { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("serviceProvider")]
        public ServiceProvider ServiceProvider { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("statusHistory")]
        public List<StatusHistory> StatusHistory { get; set; }

        [JsonProperty("subject")]
        public RefAndDisplay Subject { get; set; }
    }
}
