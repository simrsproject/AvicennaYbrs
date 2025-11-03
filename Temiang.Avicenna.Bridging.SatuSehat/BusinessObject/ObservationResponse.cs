using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    public class ObservationResponse : BaseResponse
    {
        [JsonProperty("category")]
        public List<Category> Category { get; set; }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("effectiveDateTime")]
        public string EffectiveDateTime { get; set; }

        [JsonProperty("encounter")]
        public RefAndDisplay Encounter { get; set; }

        [JsonProperty("issued")]
        public DateTime Issued { get; set; }

        [JsonProperty("performer")]
        public List<Performer> Performer { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("valueQuantity")]
        public ValueQuantity ValueQuantity { get; set; }
    }
}
