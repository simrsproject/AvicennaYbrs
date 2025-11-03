using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class AbNormalAndNotes
    {
        public bool IsAbNormal { get; set; }
        public string Notes { get; set; }
    }

    /// <summary>
    /// AbNormalAndNotes yang IsAbNormal nya nullable
    /// </summary>
    public class AbNormalAndNotes2
    {
        public bool? IsAbNormal { get; set; }
        public string Notes { get; set; }
    }

    public class AbNormalAndReason
    {
        public bool? IsAbNormal { get; set; }

        [JsonProperty("Rsn", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Reasons { get; set; }
    }
}
