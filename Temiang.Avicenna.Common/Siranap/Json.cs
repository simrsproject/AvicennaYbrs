using Newtonsoft.Json;
using System.Collections.Generic;

namespace Temiang.Avicenna.Common.Siranap
{
    public class Json
    {
        public class Select
        {
            public class Fasyanke
            {
                [JsonProperty("id_tt")]
                public string IdTt;

                [JsonProperty("tt")]
                public string Tt;

                [JsonProperty("ruang")]
                public string Ruang;

                [JsonProperty("kode_siranap")]
                public string KodeSiranap;

                [JsonProperty("jumlah_ruang")]
                public string JumlahRuang;

                [JsonProperty("jumlah")]
                public string Jumlah;

                [JsonProperty("terpakai")]
                public string Terpakai;

                [JsonProperty("terpakai_suspek")]
                public string TerpakaiSuspek;

                [JsonProperty("terpakai_konfirmasi")]
                public string TerpakaiKonfirmasi;

                [JsonProperty("antrian")]
                public string Antrian;

                [JsonProperty("prepare")]
                public string Prepare;

                [JsonProperty("prepare_plan")]
                public string PreparePlan;

                [JsonProperty("kosong")]
                public string Kosong;

                [JsonProperty("covid")]
                public string Covid;

                [JsonProperty("id_t_tt")]
                public string IdTTt;

                [JsonProperty("tglupdate")]
                public string Tglupdate;
            }

            public class Root
            {
                [JsonProperty("fasyankes")]
                public List<Fasyanke> Fasyankes;
            }
        }

        public class Delete
        {
            public class Root
            {
                [JsonProperty("id_t_tt")]
                public string IdTTt;

                [JsonProperty("id_tt")]
                public string IdTt;
            }
        }
    }
}
