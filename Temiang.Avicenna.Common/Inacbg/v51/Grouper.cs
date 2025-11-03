using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Temiang.Avicenna.Common.Inacbg.v51.Grouper
{
    public class Grouper1
    {
        public class Data
        {
            public string nomor_sep { get; set; }
        }

        public class RootObject
        {
            public Data data { get; set; }
        }

        public class Result
        {
            public class Metadata : Inacbg.Metadata
            {

            }

            public class Cbg
            {

                [JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }

                [JsonProperty("tariff")]
                public string Tariff { get; set; }
            }

            public class SubAcute
            {

                [JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }

                [JsonProperty("tariff")]
                public int Tariff { get; set; }
            }

            public class Chronic
            {

                [JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }

                [JsonProperty("tariff")]
                public int Tariff { get; set; }
            }

            public class Episodes
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
                public Episodes[] Episodes { get; set; }

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

            public class Response
            {

                [JsonProperty("cbg")]
                public Cbg Cbg { get; set; }

                [JsonProperty("sub_acute")]
                public SubAcute SubAcute { get; set; }

                [JsonProperty("chronic")]
                public Chronic Chronic { get; set; }

                [JsonProperty("kelas")]
                public string Kelas { get; set; }

                [JsonProperty("add_payment_amt")]
                public string AddPaymentAmt { get; set; }

                [JsonProperty("inacbg_version")]
                public string InacbgVersion { get; set; }

                [JsonProperty("covid19_data")]
                public Covid19Data Covid19Data { get; set; }
            }

            public class ResponseInagrouper
            {
                [JsonProperty("mdc_number")]
                public string MdcNumber { get; set; }

                [JsonProperty("mdc_description")]
                public string MdcDescription { get; set; }

                [JsonProperty("drg_code")]
                public string DrgCode { get; set; }

                [JsonProperty("drg_description")]
                public string DrgDescription { get; set; }
            }

            public class SpecialCmgOption
            {

                [JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }

                [JsonProperty("type")]
                public string Type { get; set; }
            }

            public class TarifAlt
            {

                [JsonProperty("kelas")]
                public string Kelas { get; set; }

                [JsonProperty("tarif_inacbg")]
                public string TarifInacbg { get; set; }
            }

            public class Data
            {

                [JsonProperty("metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("response")]
                public Response Response { get; set; }

                [JsonProperty("response_inagrouper")]
                public ResponseInagrouper ResponseInagrouper { get; set; }

                [JsonProperty("special_cmg_option")]
                public SpecialCmgOption[] SpecialCmgOption { get; set; }

                [JsonProperty("tarif_alt")]
                public TarifAlt[] TarifAlt { get; set; }
            }
        }
    }

    public class Grouper2
    {
        public class Data : Grouper1.Data
        {
            public string special_cmg { get; set; }
        }

        public class RootObject
        {
            public Data data { get; set; }
        }

        public class Result
        {
            public class Metadata : Inacbg.Metadata
            {

            }

            public class Cbg
            {

                [JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }

                [JsonProperty("tariff")]
                public string Tariff { get; set; }
            }

            public class SpecialCmg
            {

                [JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }

                [JsonProperty("tariff")]
                public int Tariff { get; set; }

                [JsonProperty("type")]
                public string Type { get; set; }
            }

            public class Episodes
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
                public Episodes[] Episodes { get; set; }

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

            public class Response
            {

                [JsonProperty("cbg")]
                public Cbg Cbg { get; set; }

                [JsonProperty("special_cmg")]
                public SpecialCmg[] SpecialCmg { get; set; }

                [JsonProperty("kelas")]
                public string Kelas { get; set; }

                [JsonProperty("add_payment_amt")]
                public int AddPaymentAmt { get; set; }

                [JsonProperty("inacbg_version")]
                public string InacbgVersion { get; set; }

                [JsonProperty("covid19_data")]
                public Covid19Data Covid19Data { get; set; }
            }

            public class ResponseInagrouper
            {
                [JsonProperty("mdc_number")]
                public string MdcNumber { get; set; }

                [JsonProperty("mdc_description")]
                public string MdcDescription { get; set; }

                [JsonProperty("drg_code")]
                public string DrgCode { get; set; }

                [JsonProperty("drg_description")]
                public string DrgDescription { get; set; }
            }

            public class SpecialCmgOption
            {

                [JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }

                [JsonProperty("type")]
                public string Type { get; set; }
            }

            public class TarifAlt
            {

                [JsonProperty("kelas")]
                public string Kelas { get; set; }

                [JsonProperty("tarif_inacbg")]
                public string TarifInacbg { get; set; }

                [JsonProperty("tarif_sp")]
                public int TarifSp { get; set; }

                [JsonProperty("tarif_sr")]
                public int TarifSr { get; set; }

                [JsonProperty("tarif_sd")]
                public int TarifSd { get; set; }

                [JsonProperty("tarif_si")]
                public int TarifSi { get; set; }
            }

            public class Data
            {

                [JsonProperty("metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("response")]
                public Response Response { get; set; }

                [JsonProperty("response_inagrouper")]
                public ResponseInagrouper ResponseInagrouper { get; set; }

                [JsonProperty("special_cmg_option")]
                public SpecialCmgOption[] SpecialCmgOption { get; set; }

                [JsonProperty("tarif_alt")]
                public TarifAlt[] TarifAlt { get; set; }
            }
        }
    }
}
