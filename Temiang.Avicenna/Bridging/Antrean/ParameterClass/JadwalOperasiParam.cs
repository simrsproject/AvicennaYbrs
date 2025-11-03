using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Temiang.Avicenna.Bridging.Antrean.ParameterClass
{
    public class JadwalOperasiParam
    {
        //{
        //    "tanggalawal": "2019-12-11",
        //    "tanggalakhir": "2019-12-13"
        //}
        [JsonProperty("tanggalawal")]
        public string TanggalAwal { get; set; }

        [JsonProperty("tanggalakhir")]
        public string TanggalAkhir { get; set; }

    }
}