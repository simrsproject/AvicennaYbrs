using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class KunjunganPost
    {
        [JsonProperty("noKunjungan")]
        public string NoKunjungan { get; set; }

        [JsonProperty("noKartu")]
        public string NoKartu { get; set; }

        [JsonProperty("tglDaftar")]
        public string TglDaftar { get; set; }

        [JsonProperty("kdPoli")]
        public string KdPoli { get; set; }

        [JsonProperty("keluhan")]
        public string Keluhan { get; set; }

        [JsonProperty("kdSadar")]
        public string KdSadar { get; set; }

        [JsonProperty("sistole")]
        public int? Sistole { get; set; }

        [JsonProperty("diastole")]
        public int? Diastole { get; set; }

        [JsonProperty("beratBadan")]
        public decimal? BeratBadan { get; set; }

        [JsonProperty("tinggiBadan")]
        public int? TinggiBadan { get; set; }

        [JsonProperty("respRate")]
        public int? RespRate { get; set; }

        [JsonProperty("heartRate")]
        public int? HeartRate { get; set; }

        [JsonProperty("lingkarPerut", NullValueHandling = NullValueHandling.Ignore)]
        public int? LingkarPerut { get; set; }

        [JsonProperty("terapi")]
        public string Terapi { get; set; }

        [JsonProperty("kdStatusPulang")]
        public string KdStatusPulang { get; set; }

        [JsonProperty("tglPulang")]
        public string TglPulang { get; set; }

        [JsonProperty("kdDokter")]
        public string KdDokter { get; set; }

        [JsonProperty("kdDiag1")]
        public string KdDiag1 { get; set; }

        [JsonProperty("kdDiag2")]
        public string KdDiag2 { get; set; }

        [JsonProperty("kdDiag3")]
        public string KdDiag3 { get; set; }

        [JsonProperty("kdPoliRujukInternal")]
        public string KdPoliRujukInternal { get; set; }

        [JsonProperty("rujukLanjut")]
        public KunjunganRujukLanjut RujukLanjut { get; set; }

        //  Ref_TACC : [
        //  { "kdTacc": "1", "nmTacc": "Time", alasanTacc:["< 3 Hari", ">= 3 - 7 Hari", ">= 7 Hari"] },
        //  { "kdTacc": "2", "nmTacc": "Age", alasanTacc:["< 1 Bulan", ">= 1 Bulan s/d < 12 Bulan", ">= 1 Tahun s/d < 5 Tahun",">= 5 Tahun s/d < 12 Tahun", ">= 12 Tahun s/d < 55 Tahun", ">= 55 Tahun"]   },
        //  { "kdTacc": "3", "nmTacc": "Complication", alasanTacc:(format : kdDiagnosa + " - " + NamaDiagnosa, contoh : "A09 - Diarrhoea and gastroenteritis of presumed infectious origin")  },
        //  { "kdTacc": "4", "nmTacc": "Comorbidity", alasanTacc:["< 3 Hari", ">= 3 - 7 Hari", ">= 7 Hari"]  }
        //]
        [JsonProperty("kdTacc")]
        public string KdTacc { get; set; }

        [JsonProperty("alasanTacc")]
        public string AlasanTacc { get; set; }

    }

    public class KunjunganRujukLanjut
    {
        [JsonProperty("tglEstRujuk")]
        public string TglEstRujuk { get; set; }

        [JsonProperty("kdppk")]
        public string Kdppk { get; set; }

        [JsonProperty("subSpesialis")]
        public SubSpesialisSarana SubSpesialis { get; set; }

        [JsonProperty("khusus")]
        public KunjunganRujukLanjutKhusus Khusus { get; set; }

    }
    public class SubSpesialisSarana
    {
        [JsonProperty("kdSubSpesialis1")]
        public string KdSubSpesialis1 { get; set; }

        [JsonProperty("kdSarana")]
        public string KdSarana { get; set; }
    }
    public class KunjunganRujukLanjutKhusus
    {
        [JsonProperty("kdKhusus")]
        public string KdKhusus { get; set; }

        [JsonProperty("kdSubSpesialis")]
        public string KdSubSpesialis { get; set; }

        [JsonProperty("catatan")]
        public string Catatan { get; set; }
    }
}
