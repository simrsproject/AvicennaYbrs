using System;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class PesertaGet : BaseGetResponse
    {
        [JsonProperty("response")]
        public Peserta Response { get; set; }
    }
}
