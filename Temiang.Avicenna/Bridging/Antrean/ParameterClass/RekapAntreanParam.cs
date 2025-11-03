using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Temiang.Avicenna.Bridging.Antrean.ParameterClass
{
    public class RekapAntreanParam
    {
        //{
        //    "tanggalperiksa": "2019-12-11",
        //    "kodepoli": "001",
        //    "polieksekutif": 0
        //}
        [JsonProperty("tanggalperiksa")]
        public string tanggalperiksa { get; set; }

        [JsonProperty("kodepoli")]
        public string kodepoli { get; set; }

        [JsonProperty("polieksekutif")]
        public int PoliEksekutif { get; set; }

    }
}