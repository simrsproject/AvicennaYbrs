using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;

namespace Temiang.Avicenna.BusinessObject.Common.Inacbg.v54.Claim
{
    public class Generate
    {
        public class Data
        {
            public string payor_id { get; set; }
        }

        public class RootObject
        {
            public Data data { get; set; }
        }

        public class ResponseData
        {
            [JsonProperty("claim_number")]
            public string ClaimNumber { get; set; }
        }

        public class Response
        {

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("response")]
            public ResponseData ResponseData { get; set; }
        }
    }

    public class Get
    {
        public class GetDetailResponse
        {
            public class Metadata : Inacbg.Metadata
            {

            }

            public class TarifRs : v51.Claim.Get.GetDetailResponse.TarifRs
            {

                [JsonProperty("obat_kronis")]
                public string ObatKronis { get; set; }

                [JsonProperty("obat_kemoterapi")]
                public string ObatKemoterapi { get; set; }
            }

            public class Cbg : v51.Claim.Get.GetDetailResponse.Cbg
            {
                
            }

            public class SpecialCmg : v51.Claim.Get.GetDetailResponse.SpecialCmg
            {

            }

            public class Episode
            {
                [JsonProperty("episode_id")]
                public string EpisodeId { get; set; }

                [JsonProperty("episode_class_cd")]
                public string EpisodeClassCd { get; set; }

                [JsonProperty("episode_class_nm")]
                public string EpisodeClassNm { get; set; }

                [JsonProperty("los")]
                public string Los { get; set; }

                [JsonProperty("tariff")]
                public string Tariff { get; set; }

                [JsonProperty("order_no")]
                public string OrderNo { get; set; }
            }

            public class PemulasaraanJenazah
            {
                [JsonProperty("pemulasaraan")]
                public string Pemulasaraan { get; set; }

                [JsonProperty("kantong")]
                public string Kantong { get; set; }

                [JsonProperty("peti")]
                public string Peti { get; set; }

                [JsonProperty("plastik")]
                public string Plastik { get; set; }

                [JsonProperty("desinfektan_jenazah")]
                public string DesinfektanJenazah { get; set; }

                [JsonProperty("mobil")]
                public string Mobil { get; set; }

                [JsonProperty("desinfektan_mobil")]
                public string DesinfektanMobil { get; set; }
            }

            public class Covid19Data
            {
                [JsonProperty("no_kartu_t")]
                public string NoKartuT { get; set; }

                [JsonProperty("covid19_status_cd")]
                public string Covid19StatusCd { get; set; }

                [JsonProperty("covid19_status_nm")]
                public string Covid19StatusNm { get; set; }

                [JsonProperty("episodes")]
                public Episode[] Episodes { get; set; }

                [JsonProperty("pemulasaraan_jenazah")]
                public PemulasaraanJenazah PemulasaraanJenazah { get; set; }

                [JsonProperty("cc_ind")]
                public string CcInd { get; set; }

                [JsonProperty("top_up_rawat_gross")]
                public string TopUpRawatGross { get; set; }

                [JsonProperty("top_up_rawat_factor")]
                public string TopUpRawatFactor { get; set; }

                [JsonProperty("top_up_rawat")]
                public string TopUpRawat { get; set; }

                [JsonProperty("top_up_jenazah")]
                public string TopUpJenazah { get; set; }
            }

            public class GrouperResponse
            {
                [JsonProperty("cbg")]
                public Cbg Cbg { get; set; }

                [JsonProperty("special_cmg")]
                public SpecialCmg[] SpecialCmg { get; set; }

                [JsonProperty("inacbg_version")]
                public string InacbgVersion { get; set; }

                [JsonProperty("covid19_data")]
                public Covid19Data Covid19Data { get; set; }
            }

            public class TarifAlt : v51.Claim.Get.GetDetailResponse.TarifAlt
            {

            }

            public class Grouper : v51.Claim.Get.GetDetailResponse.Grouper
            {
                [JsonProperty("response")]
                public GrouperResponse Response { get; set; }
            }

            public class Data : v51.Claim.Get.GetDetailResponse.Data
            {

                [JsonProperty("grouper")]
                public Grouper Grouper { get; set; }

                [JsonProperty("tarif_rs")]
                public TarifRs TarifRs { get; set; }

                [JsonProperty("tarif_poli_eks")]
                public string TarifPoliEks { get; set; }
            }

            public class DataResponse
            {

                [JsonProperty("data")]
                public Data Data { get; set; }
            }

            public class Response
            {

                [JsonProperty("metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("response")]
                public DataResponse DataResponse { get; set; }
            }
        }
    }
}
