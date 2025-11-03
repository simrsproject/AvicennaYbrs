using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Temiang.Avicenna.Bridging.Antrean.ParameterClass
{
    public class KodeBookingOperasiParam
    {
        //{
        //    "nopeserta": "0000000000123"
        //}
        [JsonProperty("nopeserta")]
        public string Nopeserta { get; set; }
    }
}