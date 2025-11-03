using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Inacbg.v510.Detail
{
    public class Metadata
    {
        public string nomor_sep { get; set; }
    }

    public class TarifRs
    {
        public string prosedur_non_bedah { get; set; }
        public string prosedur_bedah { get; set; }
        public string konsultasi { get; set; }
        public string tenaga_ahli { get; set; }
        public string keperawatan { get; set; }
        public string penunjang { get; set; }
        public string radiologi { get; set; }
        public string laboratorium { get; set; }
        public string pelayanan_darah { get; set; }
        public string rehabilitasi { get; set; }
        public string kamar { get; set; }
        public string rawat_intensif { get; set; }
        public string obat { get; set; }
        public string alkes { get; set; }
        public string bmhp { get; set; }
        public string sewa_alat { get; set; }
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
        public TarifRs tarif_rs { get; set; }
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

    public class Datas : Data
    {
        public string cara_masuk { get; set; }
        public string use_ind { get; set; }
        public string start_dttm { get; set; }
        public string stop_dttm { get; set; }
        public string upgrade_class_payor { get; set; }
        public string sistole { get; set; }
        public string diastole { get; set; }
        public string dializer_single_use { get; set; }
        public string kantong_darah { get; set; }
        public string menit_1_appearance { get; set; }
        public string menit_1_pulse { get; set; }
        public string menit_1_grimace { get; set; }
        public string menit_1_activity { get; set; }
        public string menit_1_respiration { get; set; }
        public string menit_5_appearance { get; set; }
        public string menit_5_pulse { get; set; }
        public string menit_5_grimace { get; set; }
        public string menit_5_activity { get; set; }
        public string menit_5_respiration { get; set; }
        public string usia_kehamilan { get; set; }
        public string gravida { get; set; }
        public string partus { get; set; }
        public string abortus { get; set; }
        public string onset_kontraksi { get; set; }
        public string delivery { get; set; }
    }

    public class Covid19PenunjangPengurang
    {
        public string lab_asam_laktat { get; set; }

        public string lab_procalcitonin { get; set; }

        public string lab_crp { get; set; }

        public string lab_kultur { get; set; }

        public string lab_d_dimer { get; set; }

        public string lab_pt { get; set; }

        public string lab_aptt { get; set; }

        public string lab_waktu_pendarahan { get; set; }

        public string lab_anti_hiv { get; set; }

        public string lab_analisa_gas { get; set; }

        public string lab_albumin { get; set; }

        public string rad_thorax_ap_pa { get; set; }
    }

    public class TarifRss : TarifRs
    {
        public string obat_kronis { get; set; }

        public string obat_kemoterapi { get; set; }
    }

    public class Datass : Datas
    {

        public TarifRss tarif_rs { get; set; }

        public string pemulasaraan_jenazah { get; set; }

        public string kantong_jenazah { get; set; }

        public string peti_jenazah { get; set; }

        public string plastik_erat { get; set; }

        public string desinfektan_jenazah { get; set; }

        public string mobil_jenazah { get; set; }

        public string desinfektan_mobil_jenazah { get; set; }

        public string covid19_status_cd { get; set; }

        public string nomor_kartu_t { get; set; }

        public string episodes { get; set; }

        public string covid19_cc_ind { get; set; }

        public string covid19_rs_darurat_ind { get; set; }

        public string covid19_co_insidense_ind { get; set; }

        public Covid19PenunjangPengurang covid19_penunjang_pengurang { get; set; }

        public string terapi_konvalesen { get; set; }

        public string akses_naat { get; set; }

        public string isoman_ind { get; set; }

        public string bayi_lahir_status_cd { get; set; }
    }
}
