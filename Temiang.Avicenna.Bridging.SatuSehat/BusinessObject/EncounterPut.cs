using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class EncounterFinishPut
    {
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("diagnosis")]
        public List<Diagnosis> Diagnosis;

        [JsonProperty("id")]
        public string ID { get; set; }

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

        [JsonProperty("episodeOfCare", NullValueHandling = NullValueHandling.Ignore)]
        public ServiceProvider EpisodeOfCare { get; set; }

        [JsonProperty("hospitalization", NullValueHandling = NullValueHandling.Ignore)]
        public Hospitalization Hospitalization { get; set; }
    }


    public class Diagnosis
    {
        [JsonProperty("condition")]
        public Condition Condition;

        [JsonProperty("rank")]
        public int Rank;

        [JsonProperty("use")]
        public Use Use;
    }

    public class Condition
    {
        [JsonProperty("display")]
        public string Display;

        [JsonProperty("reference")]
        public string Reference;
    }

    public class Use
    {
        [JsonProperty("coding")]
        public List<Coding> Coding;
    }
    
    public class DischargeDisposition
    {
        [JsonProperty("coding")]
        public List<Coding> Coding;

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Hospitalization
    {
        [JsonProperty("dischargedisposition")]
        public List<DischargeDisposition> DischargeDisposition;

        [JsonProperty("admitsource")]
        public AdmitSource AdmitSource;
    }
}
