using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging;

namespace Temiang.Avicenna.Bridging.Antrean.ResponseClass
{
    public class KodeBookingOperasi
    {
        //{
        //             "kodebooking": "123456ZXC",
        //             "tanggaloperasi": "2019-12-11",
        //             "jenistindakan": "operasi gigi",
        //             "kodepoli": "001",
        //             "namapoli": "Poli Bedah Mulut",
        //             "terlaksana": 0 
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
    }
}