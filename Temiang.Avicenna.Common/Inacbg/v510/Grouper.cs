using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Inacbg.v510.Gruoper
{
    public class importcoding
    {
        // ===== Request payload =====
        public class Data          // { "nomor_sep": "..." }
        {
            [JsonProperty("nomor_sep")]
            public string nomor_sep { get; set; }
        }

        public class RootObject    // { "data": { "nomor_sep": "..." } }
        {
            [JsonProperty("data")]
            public Data data { get; set; }
        }

        // ===== Response payload =====
        public class Result        // top-level response: { "metadata": {...}, "data": {...} }
        {
            // metadata di level atas (punya method juga)
            public class Metadata : Inacbg.Metadata
            {
                [JsonProperty("method")]
                public string Method { get; set; }
            }

            // metadata di item expanded (punya error_no optional)
            public class ItemMetadata
            {
                [JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("error_no")]
                public string ErrorNo { get; set; }

                [JsonProperty("message")]
                public string Message { get; set; }
            }

            // 1 item pada array expanded
            public class ExpandedItem
            {
                [JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("display")]
                public string Display { get; set; }

                [JsonProperty("no")]
                public string No { get; set; }

                [JsonProperty("validcode")]
                public string ValidCode { get; set; }

                [JsonProperty("metadata")]
                public ItemMetadata Metadata { get; set; }
            }

            // blok diagnosa/procedure
            public class Section
            {
                // field bernama "string" di JSON → kita map ke "Joined"
                [JsonProperty("string")]
                public string Joined { get; set; }

                [JsonProperty("expanded")]
                public List<ExpandedItem> Expanded { get; set; }
            }

            // data: { diagnosa: {...}, procedure: {...} }
            public class DataResponse
            {
                [JsonProperty("diagnosa")]
                public Section Diagnosa { get; set; }

                [JsonProperty("procedure")]
                public Section Procedure { get; set; }
            }

            [JsonProperty("metadata")]
            public Metadata Meta { get; set; }

            [JsonProperty("data")]
            public DataResponse Data { get; set; }
        }
    }

    public class IdrgGrouper
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

            public class Idrg
            {
                [JsonProperty("mdc_number")]
                public string MdcNumber { get; set; }

                [JsonProperty("mdc_description")]
                public string MdcDescription { get; set; }

                [JsonProperty("drg_code")]
                public string DrgCode { get; set; }

                [JsonProperty("drg_description")]
                public string DrgDescription { get; set; }

                [JsonProperty("script_version")]
                public string ScriptVersion { get; set; }

                [JsonProperty("logic_version")]
                public string LogicVersion { get; set; }
            }

            [JsonProperty("metadata")]
            public Metadata Meta { get; set; }

            [JsonProperty("response_idrg")]
            public Idrg ResponseIdrg { get; set; }
        }
    }

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

                [JsonProperty("base_tariff")]
                public string BaseTariff { get; set; }

                [JsonProperty("tariff")]
                public string Tariff { get; set; }
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

                [JsonProperty("response_inacbg")]
                public Response ResponseInacbg { get; set; }

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

                // ⬇️ ini yang penting: nama harus "response_inacbg"
                [JsonProperty("response_inacbg")]
                public Response ResponseInacbg { get; set; }

                [JsonProperty("special_cmg_option")]
                public SpecialCmgOption[] SpecialCmgOption { get; set; }

                [JsonProperty("tarif_alt")]
                public TarifAlt[] TarifAlt { get; set; }
            }
        }
    }
}
