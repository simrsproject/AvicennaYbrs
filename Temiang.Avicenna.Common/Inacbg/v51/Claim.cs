using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;

namespace Temiang.Avicenna.Common.Inacbg.v51.Claim
{
    public class Create
    {
        public class Data
        {
            public string nomor_kartu { get; set; }
            public string nomor_sep { get; set; }
            public string nomor_rm { get; set; }
            public string nama_pasien { get; set; }
            public string tgl_lahir { get; set; }
            public string gender { get; set; }
        }

        public class RootObject
        {
            public Metadata metadata { get; set; }
            public Data data { get; set; }
        }
    }

    public class Update
    {
        public class Metadata
        {
            public string nomor_sep { get; set; }
        }

        public class Data
        {
            public string nomor_sep { get; set; }
            public string nomor_kartu { get; set; }
            public string tgl_masuk { get; set; }
            public string tgl_pulang { get; set; }
            public string jenis_rawat { get; set; }
            public string kelas_rawat { get; set; }
            public string adl_sub_acute { get; set; }
            public string adl_chronic { get; set; }
            public string icu_indikator { get; set; }
            public string icu_los { get; set; }
            public string ventilator_hour { get; set; }
            public string upgrade_class_ind { get; set; }
            public string upgrade_class_class { get; set; }
            public string upgrade_class_los { get; set; }
            public string add_payment_pct { get; set; }
            public string birth_weight { get; set; }
            public string discharge_status { get; set; }
            public string diagnosa { get; set; }
            public string procedure { get; set; }
            public string tarif_rs { get; set; }
            public string tarif_poli_eks { get; set; }
            public string nama_dokter { get; set; }
            public string kode_tarif { get; set; }
            public string payor_id { get; set; }
            public string payor_cd { get; set; }
            public string cob_cd { get; set; }
            public string coder_nik { get; set; }
        }

        public class RootObject
        {
            public Metadata metadata { get; set; }
            public Data data { get; set; }
        }
    }

    public class Final
    {
        public class Data
        {
            public string nomor_sep { get; set; }
            public string coder_nik { get; set; }
        }

        public class RootObject
        {
            public Metadata metadata { get; set; }
            public Data data { get; set; }
        }

        public class Response
        {
            public class Metadata : Inacbg.Metadata
            {

                [JsonProperty("error_no")]
                public string ErrorNo { get; set; }
            }

            public class Result
            {

                [JsonProperty("metadata")]
                public Metadata Metadata { get; set; }
            }
        }
    }

    public class Edit
    {
        public class Data
        {
            public string nomor_sep { get; set; }
        }

        public class RootObject
        {
            public Metadata metadata { get; set; }
            public Data data { get; set; }
        }

        public class Response
        {
            public class Metadata : Inacbg.Metadata
            {

                [JsonProperty("error_no")]
                public string ErrorNo { get; set; }
            }

            public class Result
            {

                [JsonProperty("metadata")]
                public Metadata Metadata { get; set; }
            }
        }
    }

    public class Send
    {
        public class Collective
        {
            public class Data
            {
                public string start_dt { get; set; }
                public string stop_dt { get; set; }
                public string jenis_rawat { get; set; }
            }

            public class RootObject
            {
                public Metadata metadata { get; set; }
                public Data data { get; set; }
            }
        }

        public class Individual
        {
            public class Data
            {
                public string nomor_sep { get; set; }
            }

            public class RootObject
            {
                public Metadata metadata { get; set; }

                public Data data { get; set; }
            }

            public class Metadata : Inacbg.Metadata
            {

                [JsonProperty("error_no")]
                public string ErrorNo { get; set; }

                [JsonProperty("curl_error_no")]
                public int CurlErrorNo { get; set; }

                [JsonProperty("curl_error_message")]
                public string CurlErrorMessage { get; set; }

                [JsonProperty("curl_error_constant")]
                public string CurlErrorConstant { get; set; }
            }

            public class Datum
            {

                [JsonProperty("no_sep")]
                public string NoSep { get; set; }

                [JsonProperty("tgl_pulang")]
                public string TglPulang { get; set; }

                [JsonProperty("kemkes_dc_status")]
                public string KemkesDcStatus { get; set; }

                [JsonProperty("bpjs_dc_status")]
                public string BpjsDcStatus { get; set; }

                [JsonProperty("cob_dc_status")]
                public string CobDcStatus { get; set; }
            }

            public class Response
            {

                [JsonProperty("data")]
                public Datum[] Data { get; set; }
            }

            public class Result
            {

                [JsonProperty("metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("response")]
                public Response Response { get; set; }
            }
        }
    }

    public class Get
    {
        public class GetGeneral
        {
            public class Data
            {
                public string start_dt { get; set; }
                public string stop_dt { get; set; }
                public string jenis_rawat { get; set; }
            }

            public class RootObject
            {
                public Metadata metadata { get; set; }
                public Data data { get; set; }
            }
        }

        public class GetDetail
        {
            public class Data
            {
                public string nomor_sep { get; set; }
            }

            public class RootObject
            {
                public Data data { get; set; }
            }
        }

        public class GetDetailResponse
        {
            public class Metadata : Inacbg.Metadata
            {

            }

            public class TarifRs
            {

                [JsonProperty("prosedur_non_bedah")]
                public string ProsedurNonBedah { get; set; }

                [JsonProperty("prosedur_bedah")]
                public string ProsedurBedah { get; set; }

                [JsonProperty("konsultasi")]
                public string Konsultasi { get; set; }

                [JsonProperty("tenaga_ahli")]
                public string TenagaAhli { get; set; }

                [JsonProperty("keperawatan")]
                public string Keperawatan { get; set; }

                [JsonProperty("penunjang")]
                public string Penunjang { get; set; }

                [JsonProperty("radiologi")]
                public string Radiologi { get; set; }

                [JsonProperty("laboratorium")]
                public string Laboratorium { get; set; }

                [JsonProperty("pelayanan_darah")]
                public string PelayananDarah { get; set; }

                [JsonProperty("rehabilitasi")]
                public string Rehabilitasi { get; set; }

                [JsonProperty("kamar")]
                public string Kamar { get; set; }

                [JsonProperty("rawat_intensif")]
                public string RawatIntensif { get; set; }

                [JsonProperty("obat")]
                public string Obat { get; set; }

                [JsonProperty("alkes")]
                public string Alkes { get; set; }

                [JsonProperty("bmhp")]
                public string Bmhp { get; set; }

                [JsonProperty("sewa_alat")]
                public string SewaAlat { get; set; }
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

            public class GrouperResponse
            {

                [JsonProperty("cbg")]
                public Cbg Cbg { get; set; }

                [JsonProperty("special_cmg")]
                public SpecialCmg[] SpecialCmg { get; set; }

                [JsonProperty("inacbg_version")]
                public string InacbgVersion { get; set; }
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
            }

            public class Grouper
            {

                [JsonProperty("response")]
                public GrouperResponse Response { get; set; }

                [JsonProperty("tarif_alt")]
                public TarifAlt[] TarifAlt { get; set; }
            }

            public class Data
            {

                [JsonProperty("kode_rs")]
                public string KodeRs { get; set; }

                [JsonProperty("kelas_rs")]
                public string KelasRs { get; set; }

                [JsonProperty("kelas_rawat")]
                public int KelasRawat { get; set; }

                [JsonProperty("kode_tarif")]
                public string KodeTarif { get; set; }

                [JsonProperty("jenis_rawat")]
                public int JenisRawat { get; set; }

                [JsonProperty("tgl_masuk")]
                public string TglMasuk { get; set; }

                [JsonProperty("tgl_pulang")]
                public string TglPulang { get; set; }

                [JsonProperty("tgl_lahir")]
                public string TglLahir { get; set; }

                [JsonProperty("berat_lahir")]
                public string BeratLahir { get; set; }

                [JsonProperty("gender")]
                public int Gender { get; set; }

                [JsonProperty("discharge_status")]
                public int DischargeStatus { get; set; }

                [JsonProperty("diagnosa")]
                public string Diagnosa { get; set; }

                [JsonProperty("procedure")]
                public string Procedure { get; set; }

                [JsonProperty("adl_sub_acute")]
                public int AdlSubAcute { get; set; }

                [JsonProperty("adl_chronic")]
                public int AdlChronic { get; set; }

                [JsonProperty("tarif_rs")]
                public TarifRs TarifRs { get; set; }

                [JsonProperty("los")]
                public string Los { get; set; }

                [JsonProperty("icu_indikator")]
                public int IcuIndikator { get; set; }

                [JsonProperty("icu_los")]
                public string IcuLos { get; set; }

                [JsonProperty("ventilator_hour")]
                public string VentilatorHour { get; set; }

                [JsonProperty("upgrade_class_ind")]
                public string UpgradeClassInd { get; set; }

                [JsonProperty("upgrade_class_class")]
                public string UpgradeClassClass { get; set; }

                [JsonProperty("upgrade_class_los")]
                public string UpgradeClassLos { get; set; }

                [JsonProperty("add_payment_pct")]
                public string AddPaymentPct { get; set; }

                [JsonProperty("add_payment_amt")]
                public string AddPaymentAmt { get; set; }

                [JsonProperty("nama_pasien")]
                public string NamaPasien { get; set; }

                [JsonProperty("nomor_rm")]
                public string NomorRm { get; set; }

                [JsonProperty("umur_tahun")]
                public int UmurTahun { get; set; }

                [JsonProperty("umur_hari")]
                public string UmurHari { get; set; }

                [JsonProperty("nama_dokter")]
                public string NamaDokter { get; set; }

                [JsonProperty("nomor_sep")]
                public string NomorSep { get; set; }

                [JsonProperty("nomor_kartu")]
                public string NomorKartu { get; set; }

                [JsonProperty("payor_id")]
                public string PayorId { get; set; }

                [JsonProperty("payor_nm")]
                public string PayorNm { get; set; }

                [JsonProperty("coder_nm")]
                public string CoderNm { get; set; }

                [JsonProperty("coder_nik")]
                public string CoderNik { get; set; }

                [JsonProperty("patient_id")]
                public string PatientId { get; set; }

                [JsonProperty("admission_id")]
                public string AdmissionId { get; set; }

                [JsonProperty("hospital_admission_id")]
                public string HospitalAdmissionId { get; set; }

                [JsonProperty("grouping_count")]
                public string GroupingCount { get; set; }

                [JsonProperty("grouper")]
                public Grouper Grouper { get; set; }

                [JsonProperty("kemenkes_dc_status_cd")]
                public string KemenkesDcStatusCd { get; set; }

                [JsonProperty("kemenkes_dc_sent_dttm")]
                public string KemenkesDcSentDttm { get; set; }

                [JsonProperty("bpjs_dc_status_cd")]
                public string BpjsDcStatusCd { get; set; }

                [JsonProperty("bpjs_dc_sent_dttm")]
                public string BpjsDcSentDttm { get; set; }

                [JsonProperty("klaim_status_cd")]
                public string KlaimStatusCd { get; set; }

                [JsonProperty("bpjs_klaim_status_cd")]
                public string BpjsKlaimStatusCd { get; set; }

                [JsonProperty("bpjs_klaim_status_nm")]
                public string BpjsKlaimStatusNm { get; set; }
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

    public class Response
    {
        public class Metadata : Inacbg.Metadata
        {
            [JsonProperty("error_no")]
            public string ErrorNo { get; set; }
        }

        public class _200
        {

            [JsonProperty("patient_id")]
            public int PatientId { get; set; }

            [JsonProperty("admission_id")]
            public int AdmissionId { get; set; }

            [JsonProperty("hospital_admission_id")]
            public int HospitalAdmissionId { get; set; }
        }

        public class _400
        {

            [JsonProperty("nama_pasien")]
            public string NamaPasien { get; set; }

            [JsonProperty("nomor_rm")]
            public string NomorRm { get; set; }

            [JsonProperty("tgl_masuk")]
            public string TglMasuk { get; set; }
        }

        public class Result
        {

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("response")]
            public _200 Response { get; set; }

            [JsonProperty("duplicate")]
            public _400[] Duplicate { get; set; }
        }
    }

    public class Print
    {
        public class Metadata : Inacbg.Metadata
        {

        }

        public class Response
        {

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("data")]
            public string Data { get; set; }
        }
    }
}
