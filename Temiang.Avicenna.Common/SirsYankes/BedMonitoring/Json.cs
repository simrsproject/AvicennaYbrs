using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.SirsYankes.BedMonitoring
{
    public class Json
    {
        [JsonProperty("kode_ruang")]
        public string KodeRuang;

        [JsonProperty("tipe_pasien")]
        public string TipePasien;

        [JsonProperty("total_tt")]
        public string TotalTt;

        [JsonProperty("terpakaiMale")]
        public string TerpakaiMale;

        [JsonProperty("terpakaiFemale")]
        public string TerpakaiFemale;

        [JsonProperty("kosongMale")]
        public string KosongMale;

        [JsonProperty("kosongFemale")]
        public string KosongFemale;

        [JsonProperty("waiting")]
        public string Waiting;

        [JsonProperty("tgl_update")]
        public string TglUpdate;
    }
}
