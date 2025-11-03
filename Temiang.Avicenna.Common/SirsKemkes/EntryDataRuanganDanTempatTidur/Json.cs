using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.SirsKemkes.EntryDataRuanganDanTempatTidur
{
    public class Json
    {
        public class Request
        {
            public class Simpan
            {
                [JsonProperty("id_tt")]
                public string IdTt;

                [JsonProperty("ruang")]
                public string Ruang;

                [JsonProperty("jumlah_ruang")]
                public string JumlahRuang;

                [JsonProperty("jumlah")]
                public string Jumlah;

                [JsonProperty("terpakai")]
                public string Terpakai;

                [JsonProperty("prepare")]
                public string Prepare;

                [JsonProperty("prepare_plan")]
                public string PreparePlan;

                [JsonProperty("covid")]
                public int Covid;
            }

            public class Hapus
            {
                [JsonProperty("id_t_tt")]
                public string IdTtt;
            }
        }
    }
}
