using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Actor
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }
    }

    public class BodySite
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }

    public class Note
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Performer
    {
        [JsonProperty("actor")]
        public Actor Actor { get; set; }
    }

    public class ProcedurePost
    {
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("subject")]
        public RefAndDisplay Subject { get; set; }

        [JsonProperty("encounter")]
        public RefAndDisplay Encounter { get; set; }

        [JsonProperty("performedPeriod")]
        public Period PerformedPeriod { get; set; }

        [JsonProperty("performer")]
        public List<Performer> Performer { get; set; }

        [JsonProperty("reasonCode")]
        public List<Code> ReasonCode { get; set; }

        [JsonProperty("bodySite")]
        public List<BodySite> BodySite { get; set; }

        [JsonProperty("note")]
        public List<Note> Note { get; set; }
    }

}
