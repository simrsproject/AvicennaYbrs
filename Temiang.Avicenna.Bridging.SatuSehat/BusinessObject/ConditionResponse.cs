using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ConditionResponse: BaseResponse
    {
        [JsonProperty("category")]
        public List<Category> Category { get; set; }

        [JsonProperty("clinicalStatus")]
        public ClinicalStatus ClinicalStatus { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("encounter")]
        public RefAndDisplay Encounter { get; set; }

        [JsonProperty("onsetDateTime")]
        public string OnsetDateTime { get; set; }

        [JsonProperty("recordedDate")]
        public string RecordedDate { get; set; }
    }
}
