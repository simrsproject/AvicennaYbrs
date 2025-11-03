using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.BusinessObject.Common.Inacbg.v58.Claim
{
    public class Get
    {
        public class GetDetailResponse
        {
            public class Covid19Data : v54.Claim.Get.GetDetailResponse.Covid19Data
            {

            }

            public class Data
            {
                [JsonProperty("kode_rs")]
                public string KodeRs { get; set; }

                [JsonProperty("kelas_rs")]
                public string KelasRs { get; set; }

                [JsonProperty("kelas_rawat")]
                public int? KelasRawat { get; set; }

                [JsonProperty("kode_tarif")]
                public string KodeTarif { get; set; }

                [JsonProperty("jenis_rawat")]
                public int? JenisRawat { get; set; }

                [JsonProperty("tgl_masuk")]
                public string TglMasuk { get; set; }

                [JsonProperty("tgl_pulang")]
                public string TglPulang { get; set; }

                [JsonProperty("cara_masuk")]
                public string CaraMasuk { get; set; }

                [JsonProperty("tgl_lahir")]
                public string TglLahir { get; set; }

                [JsonProperty("berat_lahir")]
                public string BeratLahir { get; set; }

                [JsonProperty("gender")]
                public int? Gender { get; set; }

                [JsonProperty("discharge_status")]
                public int? DischargeStatus { get; set; }

                [JsonProperty("diagnosa")]
                public string Diagnosa { get; set; }

                [JsonProperty("procedure")]
                public string Procedure { get; set; }

                [JsonProperty("diagnosa_inagrouper")]
                public string DiagnosaInagrouper { get; set; }

                [JsonProperty("procedure_inagrouper")]
                public string ProcedureInagrouper { get; set; }

                [JsonProperty("adl_sub_acute")]
                public int? AdlSubAcute { get; set; }

                [JsonProperty("adl_chronic")]
                public int? AdlChronic { get; set; }

                [JsonProperty("tarif_rs")]
                public TarifRs TarifRs { get; set; }

                [JsonProperty("los")]
                public string Los { get; set; }

                [JsonProperty("icu_indikator")]
                public int? IcuIndikator { get; set; }

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

                [JsonProperty("upgrade_class_payor")]
                public string UpgradeClassPayor { get; set; }

                [JsonProperty("nama_pasien")]
                public string NamaPasien { get; set; }

                [JsonProperty("nomor_rm")]
                public string NomorRm { get; set; }

                [JsonProperty("umur_tahun")]
                public int? UmurTahun { get; set; }

                [JsonProperty("umur_hari")]
                public string UmurHari { get; set; }

                [JsonProperty("tarif_poli_eks")]
                public string TarifPoliEks { get; set; }

                [JsonProperty("dializer_single_use")]
                public string DializerSingleUse { get; set; }

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

            public class Grouper
            {
                [JsonProperty("response")]
                public GResponse GResponse { get; set; }

                [JsonProperty("response_inagrouper")]
                public ResponseInagrouper ResponseInagrouper { get; set; }

                [JsonProperty("tarif_alt")]
                public TarifAlt[] TarifAlt { get; set; }
            }

            public class Cbg : v51.Claim.Get.GetDetailResponse.Cbg
            {

            }

            public class SpecialCmg : v51.Claim.Get.GetDetailResponse.SpecialCmg
            {

            }

            public class TarifAlt : v51.Claim.Get.GetDetailResponse.TarifAlt
            {

            }

            public class GResponse
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

            public class Metadata : Inacbg.Metadata
            {

            }

            public class TResponse
            {
                [JsonProperty("data")]
                public Data Data { get; set; }
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

            public class Root
            {
                [JsonProperty("metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("response")]
                public TResponse Response { get; set; }
            }

            public class TarifRs
            {
                [JsonProperty("prosedur_non_bedah")]
                public int? ProsedurNonBedah { get; set; }

                [JsonProperty("prosedur_bedah")]
                public int? ProsedurBedah { get; set; }

                [JsonProperty("konsultasi")]
                public int? Konsultasi { get; set; }

                [JsonProperty("tenaga_ahli")]
                public int? TenagaAhli { get; set; }

                [JsonProperty("keperawatan")]
                public int? Keperawatan { get; set; }

                [JsonProperty("penunjang")]
                public int? Penunjang { get; set; }

                [JsonProperty("radiologi")]
                public int? Radiologi { get; set; }

                [JsonProperty("laboratorium")]
                public int? Laboratorium { get; set; }

                [JsonProperty("pelayanan_darah")]
                public int? PelayananDarah { get; set; }

                [JsonProperty("rehabilitasi")]
                public int? Rehabilitasi { get; set; }

                [JsonProperty("kamar")]
                public int? Kamar { get; set; }

                [JsonProperty("rawat_intensif")]
                public int? RawatIntensif { get; set; }

                [JsonProperty("obat")]
                public int? Obat { get; set; }

                [JsonProperty("obat_kronis")]
                public int? ObatKronis { get; set; }

                [JsonProperty("obat_kemoterapi")]
                public int? ObatKemoterapi { get; set; }

                [JsonProperty("alkes")]
                public int? Alkes { get; set; }

                [JsonProperty("bmhp")]
                public int? Bmhp { get; set; }

                [JsonProperty("sewa_alat")]
                public int? SewaAlat { get; set; }
            }
        }
    }
}
