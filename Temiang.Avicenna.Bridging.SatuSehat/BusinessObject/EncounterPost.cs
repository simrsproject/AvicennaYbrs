using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class EncounterPost
    {
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("identifier")]
        public List<Identifier> Identifier { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("class")]
        public Class Class { get; set; }

        [JsonProperty("subject")]
        public RefAndDisplay Subject { get; set; }

        [JsonProperty("participant")]
        public List<Participant> Participant { get; set; }

        [JsonProperty("period")]
        public Period Period { get; set; }

        [JsonProperty("location")]
        public List<Location> Location { get; set; }

        [JsonProperty("statusHistory")]
        public List<StatusHistory> StatusHistory { get; set; }

        [JsonProperty("serviceProvider")]
        public ServiceProvider ServiceProvider { get; set; }

        [JsonProperty("hospitalization", NullValueHandling = NullValueHandling.Ignore)]
        public Hospitalization Hospitalization { get; set; }

        [JsonProperty("episodeOfCare", NullValueHandling = NullValueHandling.Ignore)]
        public ServiceProvider EpisodeOfCare { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; set; }
    }





    public class ServiceProvider
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class StatusHistory
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("period")]
        public Period Period { get; set; }
    }

    public class AdmitSource
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }


}
