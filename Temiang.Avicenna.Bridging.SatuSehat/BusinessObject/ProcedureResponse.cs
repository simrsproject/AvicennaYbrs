using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ProcedureResponse: BaseResponse
    {
        [JsonProperty("bodySite")]
        public List<BodySite> BodySite { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("encounter")]
        public RefAndDisplay Encounter { get; set; }

        [JsonProperty("note")]
        public List<Note> Note { get; set; }

        [JsonProperty("performedPeriod")]
        public Period PerformedPeriod { get; set; }

        [JsonProperty("performer")]
        public List<Performer> Performer { get; set; }

        [JsonProperty("reasonCode")]
        public List<ReasonCode> ReasonCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

    }


    public class ReasonCode
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }


}
