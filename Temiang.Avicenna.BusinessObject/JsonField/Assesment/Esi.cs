using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class Esi
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("Cons", NullValueHandling = NullValueHandling.Ignore)]
        public string[] ConditionIds { get; set; }
    }
}
