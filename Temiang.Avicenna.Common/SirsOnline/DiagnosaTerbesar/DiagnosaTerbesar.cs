using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.SirsOnline
{
    public class DiagnosaTerbesar
    {
        [JsonProperty("ID_DIAG")]
        public string IDDIAG;

        [JsonProperty("JUMLAH_KASUS")]
        public int JUMLAHKASUS;

        [JsonProperty("TANGGAL")]
        public string TANGGAL;
    }
}
