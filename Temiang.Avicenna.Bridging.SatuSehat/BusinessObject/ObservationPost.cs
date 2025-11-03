using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<ObservationPost>(myJsonResponse);

    public class ObservationPost
    {
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("category")]
        public List<Category> Category { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("subject")]
        public RefAndDisplay Subject { get; set; }

        [JsonProperty("performer")]
        public List<RefAndDisplay> Performer { get; set; }
        
        [JsonProperty("encounter")]
        public RefAndDisplay Encounter { get; set; }

        [JsonProperty("effectiveDateTime")]
        public string EffectiveDateTime { get; set; }

        [JsonProperty("bodySite", NullValueHandling = NullValueHandling.Ignore)]
        public Code BodySite { get; set; }

        [JsonProperty("valueQuantity")]
        public ValueQuantity ValueQuantity { get; set; }

        [JsonProperty("interpretation", NullValueHandling = NullValueHandling.Ignore)]
        public List<Interpretation> Interpretation { get; set; }

        [JsonProperty("identifier", NullValueHandling = NullValueHandling.Ignore)]
        public List<Identifier> Identifier { get; set; }

        [JsonProperty("issued", NullValueHandling = NullValueHandling.Ignore)]
        public string Issued { get; set; }
    }

}
