using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.Bridging.PCare.Common;

namespace Temiang.Avicenna.Bridging.PCare.BusinessObject
{
    public class KunjunganGet : BaseGetResponse
    {
        [JsonProperty("response")]
        public Result Response { get; set; }

        #region child class
        public class Result
        {
            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("list")]
            public List<Kunjungan> List { get; set; }
        }


        public class Kunjungan
        {
            [JsonProperty("noKunjungan")]
            public string NoKunjungan { get; set; }

            [JsonProperty("tglKunjungan")]
            public string TglKunjungan { get; set; }

            [JsonProperty("providerPelayanan")]
            public Provider ProviderPelayanan { get; set; }

            [JsonProperty("peserta")]
            public Provider Peserta { get; set; }

            [JsonProperty("poli")]
            public Poli Poli { get; set; }

            [JsonProperty("progProlanis")]
            public Prolanis ProgProlanis { get; set; }

            [JsonProperty("keluhan")]
            public string Keluhan { get; set; }

            [JsonProperty("diagnosa1")]
            public Diagnosa Diagnosa1 { get; set; }

            [JsonProperty("diagnosa2")]
            public Diagnosa Diagnosa2 { get; set; }

            [JsonProperty("diagnosa3")]
            public Diagnosa Diagnosa3 { get; set; }

            [JsonProperty("kesadaran")]
            public Kesadaran Kesadaran { get; set; }

            [JsonProperty("sistole")]
            public decimal Sistole { get; set; }

            [JsonProperty("diastole")]
            public decimal Diastole { get; set; }

            [JsonProperty("beratBadan")]
            public decimal BeratBadan { get; set; }

            [JsonProperty("tinggiBadan")]
            public int TinggiBadan { get; set; }

            [JsonProperty("respRate")]
            public decimal RespRate { get; set; }

            [JsonProperty("heartRate")]
            public decimal HeartRate { get; set; }

            [JsonProperty("catatan")]
            public string Catatan { get; set; }

            [JsonProperty("rujukBalik")]
            public int RujukBalik { get; set; }

            [JsonProperty("providerAsalRujuk")]
            public Provider ProviderAsalRujuk { get; set; }

            [JsonProperty("providerRujukLanjut")]
            public Provider ProviderRujukLanjut { get; set; }

            [JsonProperty("pemFisikLain")]
            public string PemFisikLain { get; set; }

            [JsonProperty("dokter")]
            public Dokter Dokter { get; set; }

            [JsonProperty("statusPulang")]
            public StatusPulang StatusPulang { get; set; }

            [JsonProperty("tkp")]
            public Tkp Tkp { get; set; }

            [JsonProperty("poliRujukInternal")]
            public Poli PoliRujukInternal { get; set; }

            [JsonProperty("poliRujukLanjut")]
            public Poli PoliRujukLanjut { get; set; }

            [JsonProperty("tglPulang")]
            public string TglPulang { get; set; }

        }

        public class Tkp
        {
            [JsonProperty("kdTkp")]
            public string KdTkp { get; set; }

            [JsonProperty("nmTkp")]
            public string NmTkp { get; set; }

        }
        #endregion

    }

}


