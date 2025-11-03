using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{

    public class PendaftaranPost
    {
        private string _kdTkp;

        [JsonProperty("kdProviderPeserta")]
        public string KdProviderPeserta { get; set; }

        [JsonProperty("tglDaftar")]
        public string TglDaftar { get; set; }

        [JsonProperty("noKartu")]
        public string NoKartu { get; set; }

        [JsonProperty("kdPoli")]
        public string KdPoli { get; set; }

        [JsonProperty("keluhan")]
        public string Keluhan { get; set; }

        [JsonProperty("kunjSakit")]
        public bool KunjSakit { get; set; }

        [JsonProperty("sistole")]
        public int? Sistole { get; set; }

        [JsonProperty("diastole")]
        public int? Diastole { get; set; }

        [JsonProperty("beratBadan")]
        public decimal? BeratBadan { get; set; }

        [JsonProperty("tinggiBadan")]
        public int? TinggiBadan { get; set; }

        [JsonProperty("heartRate")]
        public int? HeartRate { get; set; }

        [JsonProperty("respRate")]
        public int? RespRate { get; set; }

        [JsonIgnore]
        public string KdSadar { get; set; }

        [JsonProperty("kdTkp")]
        public string KdTkp
        {
            get { 
                return string.IsNullOrEmpty(_kdTkp)? "10":_kdTkp; }
            set { _kdTkp = value; }
        }
    }

}


