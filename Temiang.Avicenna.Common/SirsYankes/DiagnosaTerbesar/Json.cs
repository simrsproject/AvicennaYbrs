using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.SirsYankes.DiagnosaTerbesar
{
    public class Json
    {
        [JsonProperty("ID_DIAG")]
        public string IDDIAG;

        [JsonProperty("JUMLAH_KASUS")]
        public string JUMLAHKASUS;

        [JsonProperty("TANGGAL")]
        public string TANGGAL;
    }
}
