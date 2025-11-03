using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging;

namespace Temiang.Avicenna.Bridging.Antrean.ResponseClass
{
    public class JadwalOperasi
    {
        //{
        //             "kodebooking": "123456ZXC",
        //             "tanggaloperasi": "2019-12-11",
        //             "jenistindakan": "operasi gigi",
        //             "kodepoli": "001",
        //             "namapoli": "Poli Bedah Mulut",
        //             "terlaksana": 1,
        //             "nopeserta": "0000000924782",
        //             "lastupdate": 1577417743000 
        //        }


        [JsonProperty("kodebooking")]
        public string KodeBooking { get; set; }

        [JsonProperty("tanggaloperasi")]
        public string TanggalOperasi { get; set; }

        [JsonProperty("jenistindakan")]
        public string JenisTindakan { get; set; }

        [JsonProperty("kodepoli")]
        public string KodePoli { get; set; }

        [JsonProperty("namapoli")]
        public string NamaPoli { get; set; }

        [JsonProperty("terlaksana")]
        public int Terlaksana { get; set; }

        [JsonProperty("nopeserta")]
        public string NoPeserta { get; set; }

        [JsonProperty("lastupdate")]
        public long LastUpdate { get; set; }
    }
}